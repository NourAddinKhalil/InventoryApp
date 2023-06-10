using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class Coupes_Of_Account
    {
        public long  ID { get; set; }
        public string Code { get; set; }
        public int Account_ID { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public DateTime Insert_Date { get; set; }
        public byte Source_Type { get; set; }
        public int Source_ID { get; set; }
        public string Notes { get; set; }
        public byte Move_Name { get; set; }
        public int User_ID { get; set; }
    }
}
