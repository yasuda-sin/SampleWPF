using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Helpers
{
    internal static class DBHelper
    {
        public static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SqlServerTaskDB"].ToString();
    }
}
