using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class Guaranteed_Notes
    {
        public int ID { get; set; }
        public bool Is_Cash_In { get; set; }
        public string Code { get; set; }
        public int Invoice_ID { get; set; }
        public byte Invoice_Type { get; set; }
        public string inv_Type_Name { get; set; }
        public int Store_ID { get; set; }
        public int Drawer_ID { get; set; }
        public int Part_ID { get; set; }
        public byte Part_Type { get; set; }
        public double Amount { get; set; }
        public double Discount_Val { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string Total_Lett { get; set; }
        public int User_ID { get; set; }
    }
}
