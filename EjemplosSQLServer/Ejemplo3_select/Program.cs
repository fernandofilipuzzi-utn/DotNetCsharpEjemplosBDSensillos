using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejemplo3_select
{
    class Program
    {
        static void Main(string[] args)
        {
            #region parámetros
            //string servidor = "TSP\\SQLEXPRESS";//para sql server express
            string servidor = "TSP";//para sql server developer
            string baseDatos = "envios";
            #endregion

            // Cadena de conexión para SQL Server con autenticación de Windows
            string cadenaConexion = $"Data Source={servidor};Initial Catalog={baseDatos};Integrated Security=True;";

            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(cadenaConexion);
                conn.Open();

                string sql = "select id, nombre from productos order by id asc";

                using (var command = new SqlCommand(sql, conn))
                {
                    SqlDataReader dataReader = command.ExecuteReader();
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
                if (conn != null) conn.Close();
            }

            Console.ReadKey();
        }
    }
}
