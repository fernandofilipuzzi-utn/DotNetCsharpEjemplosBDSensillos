using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;

namespace Ejemplo1_select
{
    class Program
    {
        static void Main(string[] args)
        {
            #region parámetros
            string servidor = "TSP\\SQLEXPRESS";
            string baseDatos = "envios";
            #endregion

            List<string> ddls = new List<string> {"create table lotes( id int identity(1,1) PRIMARY KEY, numero int, fechaproduccion date );",
                                                " create table productos( id int identity(1, 1) PRIMARY KEY, nombre varchar(54), imagen VARBINARY(MAX) );",
                                                " create table lotes_productos( idlote int, idproducto int ); " };

            // Cadena de conexión para SQL Server con autenticación de Windows
            string cadenaConexion = $"Data Source={servidor};Initial Catalog={baseDatos};Integrated Security=True;";

            SqlConnection conn = null;
            try
            {
                conn=new SqlConnection(cadenaConexion);
                conn.Open();

                foreach(string ddl in ddls)
                {
                    using (var query = new SqlCommand(ddl, conn))
                    {
                        query.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\n{e.StackTrace.ToString()}");
            }
            finally
            {
                if(conn != null) conn.Close();
            }

            Console.ReadKey();
        }
    }
}
