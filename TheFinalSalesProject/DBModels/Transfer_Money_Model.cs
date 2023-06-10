using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class Transfer_Money
    {
        public int ID { get; set; }
        public int From_Acc_ID { get; set; }
        public int To_Acc_ID { get; set; }
        public DateTime Date { get; set; }
        public byte Type { get; set; }
        public double Amount { get; set; }
        public string Notes { get; set; }
        public int User_ID { get; set; }
    }
}
