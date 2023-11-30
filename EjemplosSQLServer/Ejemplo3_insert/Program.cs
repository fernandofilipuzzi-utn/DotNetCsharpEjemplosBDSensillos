using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using ModelClassLibrary.Models;

namespace Ejemplo2_insert
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Producto> productos = new List<Producto> { new Producto { Nombre = "tomate" },
                                                             new Producto { Nombre = "mandarina" } ,
                                                             new Producto {  Nombre = "arroz" }};

            #region parámetros
            string servidor = "TSP\\SQLEXPRESS";
            string baseDatos = "envios";
            #endregion

            // Cadena de conexión para SQL Server con autenticación de Windows
            string cadenaConexion = $"Data Source={servidor};Initial Catalog={baseDatos};Integrated Security=True;";

            SqlConnection conn = new SqlConnection(cadenaConexion);
            try
            { 
                conn.Open();

                string sql = "insert into productos (nombre) values (@nombre) ";

                Int32 rowsaffected = 0;
                foreach (Producto producto in productos)
                {
                    using (var query = new SqlCommand(sql, conn))
                    {
                        query.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
                        query.Parameters[0].Value = producto.Nombre;
                        rowsaffected += query.ExecuteNonQuery();
                    }
                }

                Console.WriteLine($"Cantidad de líneas insertadas: {rowsaffected}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n{e.StackTrace.ToString()}");
            }
            finally
            {
                if(conn!=null) conn.Close();
            }

            Console.ReadKey();
        }
    }
}
