using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.DBModels
{
    public class User
    {
        public int ID { get; set; }
        public string Real_Name { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public byte Type { get; set; }
        public int Setting_Profile_ID { get; set; }
        public int Setting_Screen_ID { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Online { get; set; }

    }
}
