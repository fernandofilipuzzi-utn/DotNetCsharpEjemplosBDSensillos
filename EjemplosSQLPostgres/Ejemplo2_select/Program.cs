using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ejemplo2_select
{
    class Program
    {
        static void Main(string[] args)
        {
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

                string sql = "select id, nombre from productos order by id asc";

                using (var command = new NpgsqlCommand(sql, conn))
                {
                    NpgsqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        //id
                        int id = 0;
                        if (dataReader[0] != null)
                            id = (int)dataReader[0];

                        //nombre
                        string nombre = "";
                        if (dataReader[0] != null)
                            nombre = (string)dataReader[1];

                        Console.WriteLine("\t\t{0,10} | {1,20} ", id, nombre);
                    }
                }
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
