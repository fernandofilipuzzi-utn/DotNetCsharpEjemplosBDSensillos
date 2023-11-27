using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data.SQLite;
using ModelClassLibrary.Models;
using System.Data;

namespace Ejemplo3_insert
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Producto> productos = new List<Producto> { new Producto { Nombre = "tomate" },
                                                             new Producto { Nombre = "mandarina" } ,
                                                             new Producto {  Nombre = "arroz" }};

            string cadenaConexion= "Data Source=../../../mydatabase.db;Version=3;";

            SQLiteConnection conn = null;
            try
            {
                conn = new SQLiteConnection(cadenaConexion);
                conn.Open();

                Int32 rowsaffected = 0;
                foreach (Producto producto in productos)
                {
                    string sql = "insert into productos (nombre) values (@nombre) ";

                    using (var query = new SQLiteCommand(sql, conn))
                    {
                        query.Parameters.AddWithValue("@nombre", producto.Nombre);
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
                if (conn != null) conn.Close();
            }

            Console.ReadKey();
        }
    }
}
