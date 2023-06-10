using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class Customer_Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public int Account_ID { get; set; }
        public byte Type { get; set; }
        public double Max_Credit { get; set; }
        public bool Opening_Balance { get; set; }
    }
}
