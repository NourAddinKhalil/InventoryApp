using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class Invoice
    {
        public int ID { get; set; }
        public byte Type { get; set; }
        public string Code { get; set; }
        public byte Person_Type { get; set; }
        public int Person_ID { get; set; }
        public DateTime Date { get; set; }
        public DateTime? Delivary_Date { get; set; }
        public int Store_ID { get; set; }
        public string Notes { get; set; }
        public bool Is_Posted_To_Store { get; set; }
        public DateTime? Post_Date { get; set; }
        public double Total { get; set; }
        public double Discount_Val { get; set; }
        public double Discount_Perec { get; set; }
        public double Tax_Val { get; set; }
        public double Tax_Perce { get; set; }
        public double Expences { get; set; }
        public double Net { get; set; }
        public double Paid { get; set; }
        public int Drawer_ID { get; set; }
        public double Remaining { get; set; }
        public string Shipping_Address { get; set; }
        public int Invoice_Back_ID { get; set; }
        public int User_ID { get; set; }
    }
}
