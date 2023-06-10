using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class Product
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public byte Type { get; set; }
        public bool Is_Active { get; set; }
        public double Order_Limit { get; set; }
        public byte[] Image { get; set; }
        public int Cate_ID { get; set; }
        public byte Cost_Calc_Method { get; set; }
        public string Discribtion { get; set; }
        public bool Has_Opening_Balance { get; set; }
    }
}
