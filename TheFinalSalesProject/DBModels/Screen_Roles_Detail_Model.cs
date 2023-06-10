using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class Screen_Roles_Detail
    {
        public int ID { get; set; }
        public int Screen_Role_ID { get; set; }
        public int Screen_ID { get; set; }
        public bool Can_Show { get; set; }
        public bool Can_Open { get; set; }
        public bool Can_Add { get; set; }
        public bool Can_Edit { get; set; }
        public bool Can_Delete { get; set; }
        public bool Can_Print { get; set; }
    }
}
