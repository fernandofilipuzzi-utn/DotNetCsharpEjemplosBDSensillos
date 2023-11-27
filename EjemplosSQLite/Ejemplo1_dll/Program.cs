using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using ModelClassLibrary.servicios;
using System.Data.SQLite;

namespace Ejemplo1_select
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=../../../mydatabase.db;Version=3;";

            SQLiteConnection conn = null;

            try
            {
                conn = new SQLiteConnection(connectionString);
                conn.Open();

                string sql = "CREATE TABLE IF NOT EXISTS productos (id int unique, nombre TEXT)";
                using (var query = new SqliteCommand(sql, conn))
                {
                    query.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}\n{ex.StackTrace.ToString()}");
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            Console.ReadKey();
        }
    }
}
