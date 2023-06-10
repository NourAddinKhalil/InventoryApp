using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class Pro_Store_Movement
    {
        public int ID { get; set; }
        public byte Source_Type { get; set; }
        public int Source_ID { get; set; }
        public int Product_ID { get; set; }
        public int Store_ID { get; set; }
        public double Export_Qty { get; set; }
        public double Import_Qty { get; set; }
        public double Cost_Value { get; set; }
        public DateTime Insert_Date { get; set; }
        public string Notes { get; set; }
        public int User_ID { get; set; }
    }
}
