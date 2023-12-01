using ModelsLibClass.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibClass.Utils
{
    public class DatabaseProviderDao
    {
        static public IGestionEnviosDao GetInstancia()
        {
            string ddaoImpl = ConfigurationManager.AppSettings["DaoImplClass"];
            string ddaoILib = ConfigurationManager.AppSettings["DaoImplLib"];

            Type type = Type.GetType($"{ddaoImpl}, {ddaoILib}");
            IGestionEnviosDao instanccia = Activator.CreateInstance(type) as IGestionEnviosDao;

            return instanccia;
        }
    }
}
