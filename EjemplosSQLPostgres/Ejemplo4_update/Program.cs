﻿using ModelClassLibrary.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Ejemplo4_update
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Producto> productos = new List<Producto> { new Producto { ID = 1, Nombre = "tomate" }, 
                                                            new Producto { ID = 2, Nombre = "tomate" } };

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

                string sql = "update productos set nombre=@nombre where id=@id";

                Int32 rowsaffected = 0;
                foreach (Producto producto in productos)
                {
                    using (var command = new NpgsqlCommand(sql, conn))
                    {
                        command.Parameters.Add(new NpgsqlParameter("nombre", NpgsqlTypes.NpgsqlDbType.Text));
                        command.Parameters["nombre"].Value = producto.Nombre;

                        command.Parameters.Add(new NpgsqlParameter( "id", NpgsqlTypes.NpgsqlDbType.Integer));
                        command.Parameters["Id"].Value = producto.ID;

                        rowsaffected += command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Cantidad de líneas actualizadas: {0}", rowsaffected);
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
