using System.Collections.Generic;

namespace TheFinalSalesProject.DBModels
{
    public class Full_Invoice : Invoice
    {
        public string DrawerName { get; set; }
        public string StoreName { get; set; }
        public string PersonName { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string RealName { get; set; }
        public List<Detail> Details { get; set; } = new List<Detail>();
    }
    public class Detail 
    {
        public int DetID { get; set; }
        public int Invoice_ID { get; set; }
        public int Product_ID { get; set; }
        public int Unit_ID { get; set; }
        public double Qty { get; set; }
        public double Price { get; set; }
        public double Total_Price { get; set; }
        public double Cost { get; set; }
        public double Total_Cost { get; set; }
        public double DDiscount_Val { get; set; }
        public double Discount_Prece { get; set; }
        public int DStore_ID { get; set; }
        public int Source_Back_Row_ID { get; set; }
        public string ProductName { get; set; }
        public string UnitName { get; set; }
    }
}
