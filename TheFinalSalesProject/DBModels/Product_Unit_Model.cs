using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class Product_Unit : InterFaces.IProduct_Unit_Model
    {
        public int ID { get; set; }
        public int Pro_ID { get; set; }
        public int Unit_ID { get; set; }
        public double Factor { get; set; }
        public double Buy_Price { get; set; }
        public double Sell_Price { get; set; }
        public double Sell_Discount { get; set; }
        public string BarCode { get; set; }
    }
}
