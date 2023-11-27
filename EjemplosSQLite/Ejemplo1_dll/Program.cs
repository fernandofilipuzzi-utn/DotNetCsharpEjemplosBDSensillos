using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Ejemplo1_dll
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

                string sql = "CREATE TABLE IF NOT EXISTS productos (id INTEGER PRIMARY KEY AUTOINCREMENT, nombre TEXT)";
                using (var query = new SQLiteCommand(sql, conn))
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
