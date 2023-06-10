using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class Opening_And_Destuctor_Product
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public byte Type { get; set; }
        public int Store_ID { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public string Notes { get; set; }
    }
}
