using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DAL
{
    public static class GetConnection
    {
        public static string Connection
        {
            get
            {
                return Properties.Settings.Default.ConnectionString;
            }
        }
    }
}
