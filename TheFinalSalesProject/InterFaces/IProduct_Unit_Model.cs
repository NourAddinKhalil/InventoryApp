using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.InterFaces
{
    interface IProduct_Unit_Model
    {
        int Pro_ID { get; set; }
        double Factor { get; set; }
        double Buy_Price { get; set; }
        double Sell_Price { get; set; }
        double Sell_Discount { get; set; }
        string BarCode { get; set; }
    }
}
