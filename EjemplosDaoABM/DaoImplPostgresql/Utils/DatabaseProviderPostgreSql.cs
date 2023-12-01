using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaoImplPostgreSql.Utils
{
    public class DatabaseProviderPostgreSql
    {
        

        static public NpgsqlConnection GetConexion()
        {
            var databaseProviderSection = ConfigurationManager.GetSection("DatabaseProviderPostgreSql") as NameValueCollection;

            NpgsqlConnection conn = null;

            if (databaseProviderSection != null)
            {
                string host = databaseProviderSection["Host"];
                string port = databaseProviderSection["Port"];
                string username = databaseProviderSection["UserName"];
                string password = databaseProviderSection["Password"];
                string BaseDatosNombre = databaseProviderSection["BaseDatosNombre"];

                string cadenaConexion = $"Server={host};Port={port};UserId={username};Password=postgres;Database={BaseDatosNombre};";
                conn = new NpgsqlConnection(cadenaConexion);
            }
            else
            {
                throw new Exception("Error!, no te digo porqué, jaja! ");
            }

            return conn;
        }
    }
}
