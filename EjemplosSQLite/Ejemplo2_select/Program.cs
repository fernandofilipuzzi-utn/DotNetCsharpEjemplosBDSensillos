using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data.SQLite;
using ModelClassLibrary.Models;

namespace Ejemplo3_insert
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Producto> productos = new List<Producto> { new Producto { Nombre = "tomate" },
                                                             new Producto { Nombre = "mandarina" } ,
                                                             new Producto {  Nombre = "arroz" }};

            string connectionString = "Data Source=../../../mydatabase.db;Version=3;";

            SQLiteConnection conn =null;
            try
            {
                conn = new SQLiteConnection(connectionString);
                conn.Open();

                string sql = "select id, nombre from productos ";

                using (var query = new SQLiteCommand(sql, conn))
                {
                    SQLiteDataReader dataReader = query.ExecuteReader();
                    while (dataReader.Read())
                    {
                        #region ID
                        int id = 0;
                        if (dataReader[0] != DBNull.Value)
                            id = (int)dataReader[0];
                        #endregion

                        #region nombre
                        string nombre = "";
                        if (dataReader[1] != DBNull.Value)
                            nombre = dataReader[1] as string;
                        #endregion

                        Console.WriteLine($"\t\t{id,10} | {nombre,20} ");
                    }
                }
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
