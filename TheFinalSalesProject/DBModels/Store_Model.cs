using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class Store
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Sales_Account_ID { get; set; }
        public int Sales_Return_Account_ID { get; set; }
        public int Inventory_Account_ID { get; set; }
        public int Cost_Of_Sold_Good_Account_ID { get; set; }
        public int Discount_Received_Account_ID { get; set; }
        public int Discount_Allowed_Account_ID { get; set; }
    }
}
