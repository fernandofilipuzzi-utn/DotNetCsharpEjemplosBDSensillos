using ModelClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejemplo5_delete
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Producto> productos = new List<Producto> { new Producto { ID = 1  }, new Producto { ID = 2 } };

            #region parámetros
            //string servidor = "TSP\\SQLEXPRESS";//para sql server express
            string servidor = "TSP";//para sql server developer
            string baseDatos = "envios";
            #endregion

            // Cadena de conexión para SQL Server con autenticación de Windows
            string cadenaConexion = $"Data Source={servidor};Initial Catalog={baseDatos};Integrated Security=True;";

            SqlConnection conn = new SqlConnection(cadenaConexion);
            try
            {
                conn.Open();

                string sql = "delete from productos where id=@id";

                Int32 rowsaffected = 0;
                foreach (Producto producto in productos)
                {
                    using (var command = new SqlCommand(sql, conn))
                    {

                        command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                        command.Parameters["@id"].Value = producto.ID;

                        rowsaffected += command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine($"Cantidad de líneas borradas: {rowsaffected}");
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
