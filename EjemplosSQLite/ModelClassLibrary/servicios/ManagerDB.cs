using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClassLibrary.servicios
{
    public class ManagerDb
    {
        static string connectionString = "Data Source=./App_Data/mydatabase.db;Version=3;";

        static ManagerDb instance;
        private SQLiteConnection connection;

        public SQLiteConnection Connection { get { return connection; } }

        static public ManagerDb Instance
        {
            get
            {
                if (instance == null)
                    instance = new ManagerDb();
                return instance;
            }
        }

        private ManagerDb()
        {
            connection = new SQLiteConnection(connectionString);
            connection.Open();
        }

    }
}
