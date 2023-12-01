using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaoImplSqlServer.Utils
{
    public class DatabaseProviderSqlServer
    {
        static public SqlConnection GetConexion()
        {
            /*
            string databaseProvider = ConfigurationManager.AppSettings["DatabaseProviderSqlServer"];
            string host = ConfigurationManager.AppSettings["Host"];
            string BaseDatosNombre = ConfigurationManager.AppSettings["BaseDatosNombre"];
            */

            var databaseProviderSection = ConfigurationManager.GetSection("DatabaseProviderSqlServer") as NameValueCollection;

            SqlConnection conn = null;

            if (databaseProviderSection != null)
            {
                string host = databaseProviderSection["Host"];
                string port = databaseProviderSection["Port"];
                string BaseDatosNombre = databaseProviderSection["BaseDatosNombre"];

                string cadenaConexion = $"Data Source={host};Initial Catalog={BaseDatosNombre};Integrated Security=True;";
                conn = new SqlConnection(cadenaConexion);
            }
            else
            {
                throw new Exception("Error!, no te digo porqué, jaja! ");
            }

            return conn;
        }
    }
}
