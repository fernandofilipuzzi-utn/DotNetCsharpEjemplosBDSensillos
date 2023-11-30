using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Ejemplo1_ddl
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

            List<string> ddls = new List<string> {"create table lotes(id serial, fechaproduccion date);",
                                                " create table productos(id serial, nombre character varying(52));",
                                                " create table lotes_productos(idlote integer, idproducto integer);" };

            string cadenaConexion = $"Server={host};Port={puerto};UserId={Usuario};Password={password};Database={baseDatos};";

            NpgsqlConnection conn = null;

            try
            {
                conn = new NpgsqlConnection(cadenaConexion);
                conn.Open();

                foreach (string ddl in ddls)
                {
                    using (var query = new NpgsqlCommand(ddl, conn))
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
                if (conn != null) conn.Close();
            }

            Console.ReadKey();
        }
    }
}
