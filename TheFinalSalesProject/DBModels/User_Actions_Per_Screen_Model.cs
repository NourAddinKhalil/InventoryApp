using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class User_Actions_Per_Screen
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public DateTime Date { get; set; }
        public int Screen_ID { get; set; }
        public byte Type { get; set; }
        public int Changed_Source_ID { get; set; }
        public string Changed_Source_Name { get; set; }
    }
}
