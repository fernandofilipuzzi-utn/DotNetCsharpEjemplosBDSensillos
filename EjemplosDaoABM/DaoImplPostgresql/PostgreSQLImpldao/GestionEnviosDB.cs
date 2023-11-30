using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System.Data;
using System.Data.OleDb;

using System.Drawing;
using ModelsLibClass.Models;
using Npgsql;

namespace DaoImplPostgresql.PostgreSQLImpldao
{
    public class GestionEnviosDB : IGestionEnviosDao
    {
        //ejemplo cadena de conexión local
        string cadenaConexion = "Server=localhost;Port=5432;UserId=postgres;Password=postgres;Database=envios;";
        
        public void AgregarNuevoProducto(Producto nuevoProducto)
        {
            NpgsqlConnection conn = new NpgsqlConnection(cadenaConexion);
            NpgsqlCommand command = null;

            try
            {
                conn.Open();

                string sql = "insert into productos (nombre, imagen)" +
                                   " values (:nombre, :imagen) ";

                command = new NpgsqlCommand(sql, conn);
                command.Parameters.Add(new NpgsqlParameter("nombre",
                                                     NpgsqlTypes.NpgsqlDbType.Text));
                command.Parameters.Add(new NpgsqlParameter("imagen",
                                                     NpgsqlTypes.NpgsqlDbType.Bytea));
                command.Parameters[0].Value = nuevoProducto.Nombre;

                ImageConverter _imageConverter = new ImageConverter();
                byte[] imageByte =
                            (byte[])_imageConverter.ConvertTo(nuevoProducto.Imagen,
                            typeof(byte[]));

                command.Parameters[1].Value = imageByte;

                Int32 rowsaffected = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Producto> ListarProductos()
        {
            List<Producto> productos = new List<Producto>();

            NpgsqlConnection conn = new NpgsqlConnection(cadenaConexion);
            NpgsqlCommand command = null;
            try
            {
                conn.Open();

                string sql = "select p.id, p.nombre, p.imagen " +
                             " from productos as p " +
                             " order by p.nombre asc  ";

                command = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    //id
                    int id = 0;
                    if (dataReader[0] != null)
                        id = (int)dataReader[0];

                    //nombre
                    string nombre = "";
                    if (dataReader[1] != null)
                        nombre = (string)dataReader[1];

                    //imagen
                    Image imagen = null;
                    byte[] imagenByte = dataReader[2] as byte[];
                    if (imagenByte != null)
                    {
                        using (MemoryStream imageStream =
                                                  new System.IO.MemoryStream(imagenByte))
                        {
                            ImageConverter imageConverter =
                                                       new System.Drawing.ImageConverter();
                            imagen =
                              imageConverter.ConvertFrom(imagenByte) as System.Drawing.Image;
                        }
                    }

                    productos.Add(new Producto(id, nombre, imagen));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }

            return productos;
        }

        public List<Producto> ListarProductoPorNombre(string nombreProducto)
        {
            List<Producto> productos = new List<Producto>();

            NpgsqlConnection conn = new NpgsqlConnection(cadenaConexion);
            NpgsqlCommand command = null;
            try
            {
                conn.Open();

                string sql = " select p.id, p.nombre " +
                                   " from productos as p " +
                                   " where p.nombre like :Nombre ";


                command = new NpgsqlCommand(sql, conn);
                command.Parameters.Add(new NpgsqlParameter("Nombre",
                                               NpgsqlTypes.NpgsqlDbType.Text));
                command.Parameters[0].Value = nombreProducto.Trim();
                NpgsqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    //id
                    int id = 0;
                    if (dataReader[0] != null)
                        id = (int)dataReader[0];

                    //nombre
                    string nombre = "";
                    if (dataReader[1] != null)
                        nombre = (string)dataReader[1];

                    productos.Add(new Producto(id, nombre));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }

            return productos;
        }

        public List<Lote> ListarLotes()
        {
            List<Lote> lotes = new List<Lote>();
            NpgsqlConnection conn = new NpgsqlConnection(cadenaConexion);

            NpgsqlCommand command = null;
            try
            {
                conn.Open();

                string sql = "select id, numero, fechaproduccion " +
                                  " from lotes " +
                                  " order by id asc";

                command = new NpgsqlCommand(sql, conn);

                NpgsqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read() == true)
                {
                    //id
                    int id = 0;
                    if (dataReader[0] != null)
                        id = (int)dataReader[0];

                    //numero
                    int numero = 1;
                    if (dataReader[1] != DBNull.Value)
                        numero = (int)dataReader[1];

                    //fecha
                    DateTime fecha = new DateTime();
                    if (dataReader[2] != DBNull.Value)
                        fecha = (DateTime)dataReader[2];

                    Lote buscado = new Lote(id, numero, fecha);
                    lotes.Add(buscado);
                    buscado.Productos.AddRange(ListarProductosPorLote(id));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }

            return lotes;
        }

        public void AgregarNuevoLote(Lote nuevoLote)
        {
            //hay que hacer dos metodos uno para agregar y otro para actualizar
            if (nuevoLote.ID == 0)
            {
                NpgsqlConnection conn = new NpgsqlConnection(cadenaConexion);

                //NpgsqlCommand command = null;
                try
                {
                    conn.Open();

                    string sql = "insert into lotes (numero, fechaproduccion) " +
                                         " values (:NumeroLote, :FechaProduccion) " +
                                         " returning id";

                    using (var transaction =
                                   conn.BeginTransaction(IsolationLevel.Serializable))

                    using (var command = new NpgsqlCommand(sql, conn, transaction))
                    {
                        command.Parameters.Add(
                                            new NpgsqlParameter("NumeroLote",
                                                       NpgsqlTypes.NpgsqlDbType.Integer));
                        command.Parameters.Add(
                                              new NpgsqlParameter("FechaProduccion",
                                              NpgsqlTypes.NpgsqlDbType.Date));
                        command.Parameters[0].Value = nuevoLote.Numero;
                        command.Parameters[1].Value = nuevoLote.FechaProduccion;

                        //consigue la id generada para vincularla con los productos.
                        Int32 id = (int)command.ExecuteScalar();
                        nuevoLote.ID = id;

                        foreach (Producto p in nuevoLote.Productos)
                        {
                            AgregarProductoALote(p, nuevoLote);
                        }

                        transaction.Commit();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                ActualizarNuevoLote(nuevoLote);
            }
        }

        public void ActualizarNuevoLote(Lote lote)
        {
            NpgsqlConnection conn = new NpgsqlConnection(cadenaConexion);

            NpgsqlCommand command = null;
            try
            {
                conn.Open();

                string sql = " update lotes set numero=:NumeroLote," +
                                  " fechaproduccion=:FechaProduccion " +
                                   " where id=:IdLote";

                command = new NpgsqlCommand(sql, conn);

                command.Parameters.Add(new NpgsqlParameter("NumeroLote",
                                               NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters.Add(new NpgsqlParameter("FechaProduccion",
                                               NpgsqlTypes.NpgsqlDbType.Date));
                command.Parameters.Add(new NpgsqlParameter("IdLote",
                                               NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters[0].Value = lote.Numero;
                command.Parameters[1].Value = lote.FechaProduccion;
                command.Parameters[2].Value = lote.ID;

                Int32 id = (int)command.ExecuteNonQuery();

                //quitando  los lotes eliminados 

                //busco la lista de lotes en la bd
                List<Producto> quitarProductosDeLotes = new List<Producto>();
                foreach (Producto p1 in ListarProductosPorLote(lote.ID))
                {
                    Producto buscado = null;
                    int n = 0;
                    while (n < lote.Productos.Count && buscado == null)
                    {
                        if (p1 == lote.Productos[n])
                        {
                            buscado = lote.Productos[n];
                        }
                        n++;
                    }
                    if (buscado != null)
                        quitarProductosDeLotes.Add(buscado);
                }
                foreach (Producto p in quitarProductosDeLotes)
                {
                    QuitarProductoDeLote(p, lote);
                }

                //y agregando los nuevos vinculos entre producto y lotes
                List<Producto> vincularProductosALote = new List<Producto>();

                foreach (Producto p2 in lote.Productos)
                {
                    Producto buscado = null;
                    int n = 0;
                    List<Producto> productosPorlote = ListarProductosPorLote(lote.ID);
                    while (n < productosPorlote.Count && buscado == null)
                    {
                        if (p2 == productosPorlote[n])
                        {
                            buscado = productosPorlote[n];
                        }
                        n++;
                    }
                    if (buscado != null)
                        vincularProductosALote.Add(buscado);
                }
                foreach (Producto p in vincularProductosALote)
                {
                    AgregarProductoALote(p, lote);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public void BorrarTodosLosLotes()
        {
            NpgsqlConnection conn = new NpgsqlConnection(cadenaConexion);

            NpgsqlCommand command = null;
            try
            {
                conn.Open();

                string sql = " delete from lotes; " +
                                   " delete from lotes_productos;";

                command = new NpgsqlCommand(sql, conn);

                Int32 rowsaffected = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public void AgregarProductoALote(Producto producto, Lote lote)
        {
            NpgsqlConnection conn = new NpgsqlConnection(cadenaConexion);
            NpgsqlCommand command = null;
            try
            {
                conn.Open();

                string sql = " insert into lotes_productos (idlote, idproducto) " +
                                   " values (:IdLote, :IdProducto)";

                command = new NpgsqlCommand(sql, conn);

                command.Parameters.Add(new NpgsqlParameter("IdLote",
                                               NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters.Add(new NpgsqlParameter("IdProducto",
                                               NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters[0].Value = lote.ID;
                command.Parameters[1].Value = producto.ID;

                Int32 rowsaffected = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public void QuitarProductoDeLote(Producto producto, Lote lote)
        {
            NpgsqlConnection conn = new NpgsqlConnection(cadenaConexion);

            NpgsqlCommand command = null;
            try
            {
                conn.Open();
                string sql = " delete from lotes_productos " +
                                  " where idlote=:IdLote and idProducto=:IdProducto";

                command = new NpgsqlCommand(sql, conn);

                command.Parameters.Add(new NpgsqlParameter("IdLote",
                                   NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters.Add(new NpgsqlParameter("IdProducto",
                                   NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters[0].Value = lote.ID;
                command.Parameters[1].Value = producto.ID;

                Int32 rowsaffected = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public Lote ListarLotePorNumero(int numeroLote)
        {
            List<Lote> lotes = new List<Lote>();
            NpgsqlConnection conn = new NpgsqlConnection(cadenaConexion);

            NpgsqlCommand command = null;
            try
            {
                conn.Open();

                string sql = " select lot.id, lot.numero, lot.fechaproduccion " +
                                   " from lotes as lot " +
                                   " where lot.numero = :NroLote";

                command = new NpgsqlCommand(sql, conn);

                command.Parameters.Add(new NpgsqlParameter("NroLote",
                                                 NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters[0].Value = numeroLote;

                NpgsqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    //id
                    int id = 0;
                    if (dataReader[0] != null)
                        id = (int)dataReader[0];

                    //numero
                    int numero = 1;
                    if (dataReader[1] != DBNull.Value)
                        numero = (int)dataReader[1];

                    //fecha
                    DateTime fecha = new DateTime();
                    if (dataReader[2] != DBNull.Value)
                        fecha = (DateTime)dataReader[2];

                    Lote lote = new Lote(id, numero, fecha);
                    lotes.Add(lote);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }

            if (lotes.Count > 0)
                return lotes[0];

            return null;
        }

        public Lote BuscarLotePorId(int ID)
        {
            List<Lote> lotes = new List<Lote>();
            NpgsqlConnection conn = new NpgsqlConnection(cadenaConexion);

            NpgsqlCommand command = null;
            try
            {
                conn.Open();

                string sql = " select lot.id, lot.numero, lot.fechaproduccion " +
                                   " from lotes as lot " +
                                   " where lot.id=:IdLote " +
                                   " order by lot.numero asc ";

                command = new NpgsqlCommand(sql, conn);

                command.Parameters.Add(new NpgsqlParameter("IdLote",
                                      NpgsqlTypes.NpgsqlDbType.Integer));

                command.Parameters[0].Value = ID;

                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    //id
                    int id = 0;
                    if (dataReader[0] != null)
                        id = (int)dataReader[0];

                    //número
                    int numero = 0;
                    if (dataReader[1] != null)
                        numero = (int)dataReader[1];

                    //fecha
                    DateTime fecha = DateTime.Now;
                    if (dataReader[2] != null)
                        fecha = (DateTime)dataReader[2];

                    Lote buscado = new Lote(id, numero, fecha);
                    lotes.Add(buscado);

                    buscado.Productos.AddRange(ListarProductosPorLote(id));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }

            return lotes[0];
        }

        protected List<Producto> ListarProductosPorLote(int idLote)
        {
            List<Producto> productos = new List<Producto>();
            NpgsqlConnection conn = new NpgsqlConnection(cadenaConexion);

            NpgsqlCommand command = null;
            try
            {
                conn.Open();

                string sql = " select p.id, p.nombre, p.imagen " +
                                   " from productos as p " +
                                   " join lotes_productos as lp " +
                                   "     on lp.idproducto=p.id and lp.idlote=:IdLote" +
                                   " order by p.nombre asc ";

                command = new NpgsqlCommand(sql, conn);

                command.Parameters.Add(new NpgsqlParameter("IdLote",
                                           NpgsqlTypes.NpgsqlDbType.Integer));
                command.Parameters[0].Value = idLote;

                NpgsqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    //id
                    int id = 0;
                    if (dataReader[0] != null)
                        id = (int)dataReader[0];

                    //nombre
                    string nombre = "";
                    if (dataReader[1] != DBNull.Value)
                        nombre = (string)dataReader[1];

                    //imagen
                    Image imagen = null;
                    byte[] imagenByte = dataReader[2] as byte[];
                    if (imagenByte != null)
                    {
                        using (MemoryStream imageStream =
                                                    new System.IO.MemoryStream(imagenByte))
                        {
                            ImageConverter imageConverter =
                                                      new System.Drawing.ImageConverter();
                            imagen = imageConverter.ConvertFrom(imagenByte)
                                                           as System.Drawing.Image;
                        }
                    }

                    productos.Add(new Producto(id, nombre, imagen));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }

            return productos;
        }

    }
}
