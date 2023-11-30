using ModelClassLibrary.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Ejemplo3_insert
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Producto> productos = new List<Producto> { new Producto { Nombre = "tomate" },
                                                             new Producto { Nombre = "mandarina" } ,
                                                             new Producto {  Nombre = "arroz" }};

            #region parámetros
            string host = "localhost";
            int puerto = 5432;
            string baseDatos = "envios";
            string Usuario = "postgres";
            string password = "postgres";
            #endregion

            string cadenaConexion = $"Server={host};Port={puerto};UserId={Usuario};Password={password};Database={baseDatos};";

            NpgsqlConnection conn = null;
            try
            {
                conn = new NpgsqlConnection(cadenaConexion);
                conn.Open();

                string sql = "insert into productos (nombre) values (:nombre) ";

                Int32 rowsaffected = 0;
                foreach (Producto producto in productos)
                {
                    using (var command = new NpgsqlCommand(sql, conn))
                    {
                        command.Parameters.Add(new NpgsqlParameter("@nombre", NpgsqlDbType.Varchar));
                        command.Parameters[0].Value = producto.Nombre;
                        rowsaffected += command.ExecuteNonQuery();
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
                if (conn != null) conn.Close();
            }

            Console.ReadKey();
        }
    }
}
