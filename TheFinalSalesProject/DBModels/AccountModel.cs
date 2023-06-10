using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class Accounts
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Parent_ID { get; set; }
    }
}
