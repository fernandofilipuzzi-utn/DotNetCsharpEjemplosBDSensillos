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
            try
            {
                string sql = "CREATE TABLE IF NOT EXISTS Parametros (Nombre TEXT UNIQUE, Valor TEXT)";
                using (var query = new SQLiteCommand(sql, ManagerDb.Instance.Connection))
                {
                    query.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}\n{ex.StackTrace.ToString()}");
            }

            Console.ReadKey();
        }
    }
}
