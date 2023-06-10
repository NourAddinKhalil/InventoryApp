using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class User_Profile_Property
    {
        public int ID { get; set; }
        public int Profile_ID { get; set; }
        public string Prope_Name { get; set; }
        public byte[] Prope_Value { get; set; }
    }
}
