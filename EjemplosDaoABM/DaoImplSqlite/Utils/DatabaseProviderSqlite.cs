using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaoImplSqlite.Utils
{
    public class DatabaseProviderSqlite
    {
        static public SQLiteConnection GetConexion()
        {
            var databaseProviderSection = ConfigurationManager.GetSection("DatabaseProviderSqlite") as NameValueCollection;

            SQLiteConnection conn = null;

            if (databaseProviderSection != null)
            {
                string pathDB = databaseProviderSection["PathDB"];

                string cadenaConexion = $"Data Source={pathDB};Version=3;";
                conn = new SQLiteConnection(cadenaConexion);
                CrearEsquema(conn);
            }
            else
            {
                throw new Exception("Error!, no te digo porqué, jaja! ");
            }

            return conn;
        }

        public static void CrearEsquema(SQLiteConnection conn)
        {
            try
            {
                conn.Open();

                List<string> sqls = new List<string>
                {
                    "CREATE TABLE IF NOT EXISTS lotes (id INTEGER PRIMARY KEY AUTOINCREMENT, numero INTEGER, fechaproduccion DATETIME);",
                    "CREATE TABLE IF NOT EXISTS productos (id INTEGER PRIMARY KEY AUTOINCREMENT, nombre TEXT, imagen BLOB);",
                    "CREATE TABLE IF NOT EXISTS lotes_productos (idlote INTEGER ,idproducto INTEGER );"
                };

                Int32 rowsaffected = 0;
                foreach (string sql in sqls)
                {
                    using (var query = new SQLiteCommand(sql, conn))
                    {
                        rowsaffected += query.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (conn != null) if (conn != null) conn.Close();
            }
        }
    }
}
