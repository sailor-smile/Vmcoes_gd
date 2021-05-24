using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vmcoes_gd.Utils
{
    public class DbConfigurator
    {
        public static string defConnection;
        public static string emrConnection;
        public static void SetConnection(string connection)
        {
            defConnection = connection;
        }
    }
}
