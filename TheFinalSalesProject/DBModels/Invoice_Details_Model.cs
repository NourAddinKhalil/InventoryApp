using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class Invoice_Details
    {
        public int ID { get; set; }
        public int Invoice_ID { get; set; }
        public int Product_ID { get; set; }
        public int Unit_ID { get; set; }
        public double Qty { get; set; }
        public double Price { get; set; }
        public double Total_Price { get; set; }
        public double Cost { get; set; }
        public double Total_Cost { get; set; }
        public double Discount_Val { get; set; }
        public double Discount_Prece { get; set; }
        public int Store_ID { get; set; }
        public int Source_Back_Row_ID { get; set; }
    }
}
