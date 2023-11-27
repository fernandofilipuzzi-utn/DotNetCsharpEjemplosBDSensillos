using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data.SQLite;
using ModelClassLibrary.Models;
using System.Data;

namespace Ejemplo4_update
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Producto> productos = new List<Producto> { new Producto { ID = 1, Nombre = "tomate" }, new Producto { ID = 2, Nombre = "tomate" } };

            string cadenaConexion = "Data Source=../../../mydatabase.db;Version=3;";

            SQLiteConnection conn = null;
            try
            {
                conn = new SQLiteConnection(cadenaConexion);
                conn.Open();

                Int32 rowsaffected = 0;
                foreach (Producto producto in productos)
                {
                    string sql = "update productos set nombre=@nombre where id=@id";
                    using (var query = new SQLiteCommand(sql, conn))
                    {
                        query.Parameters.AddWithValue("@id", producto.ID);
                        query.Parameters.AddWithValue("@nombre", producto.Nombre);

                        rowsaffected += query.ExecuteNonQuery();
                    }
                }

                Console.WriteLine($"Cantidad de líneas actualizadas: {rowsaffected}");
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
