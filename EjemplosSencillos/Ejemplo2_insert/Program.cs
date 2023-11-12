using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;

namespace Ejemplo2
{
    class Program
    {
        static void Main(string[] args)
        {
            #region parámetros
            string servidor = "TSP\\SQLEXPRESS";
            string baseDatos = "envios";
            #endregion

            // Cadena de conexión para SQL Server con autenticación de Windows
            string cadenaConexion = $"Data Source={servidor};Initial Catalog={baseDatos};Integrated Security=True;";

            SqlConnection conn = new SqlConnection(cadenaConexion);
            SqlCommand command = null;
            try
            { 
                conn.Open();
                
                string sql = "insert into productos (nombre) values (@nombre) ";

                command = new SqlCommand(sql, conn);
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
                command.Parameters[0].Value = "tomates";
                Int32 rowsaffected = command.ExecuteNonQuery();

                Console.WriteLine("Cantidad de líneas insertadas {0}:", rowsaffected);
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
