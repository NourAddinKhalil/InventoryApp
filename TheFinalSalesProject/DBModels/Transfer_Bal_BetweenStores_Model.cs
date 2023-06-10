using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class Transfer_Bal_BetweenStores
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public int From_Store { get; set; }
        public int To_Store { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public string Notes { get; set; }
    }
}
