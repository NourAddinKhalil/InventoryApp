using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    class Full_Pro_Store_Move : DBModels.Pro_Store_Movement
    {
        public string ProductName { get; set; }
        public string Code { get; set; }
        public string StoreName { get; set; }
        public string Real_Name { get; set; }
        public int Cate_ID { get; set; }
        public string CateName { get; set; }
    }
}
