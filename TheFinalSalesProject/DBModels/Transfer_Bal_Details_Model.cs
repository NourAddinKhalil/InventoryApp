﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class Transfer_Bal_Details
    {
        public int ID { get; set; }
        public int Transfer_ID { get; set; }
        public int Product_ID { get; set; }
        public int Unit_ID { get; set; }
        public double Qty { get; set; }
        public double Price { get; set; }
        public double Total_Price { get; set; }
    }
}
