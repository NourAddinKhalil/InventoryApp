using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.Classes
{
    public class User_Screen_Access
    {
        private static int max_Sc_ID = 1;
        public User_Screen_Access(string name, User_Screen_Access parent = null)
        {
            if (parent != null)
            {
                Parent_Screen_ID = parent.Screen_ID;
            }
            else
            {
                Parent_Screen_ID = 0;
            }
            Screen_Name = name;
            Screen_ID = max_Sc_ID++;
            Actions = new List<Screen_Actions>();
            //{
            //    Screen_Actions.Open,
            //    Screen_Actions.Show,
            //    Screen_Actions.Add,
            //    Screen_Actions.Delete,
            //    Screen_Actions.Edit,
            //    Screen_Actions.Print,
            //};
            //Actions = parent.Actions;
        }
        public int Screen_ID { get; set; }
        public int Parent_Screen_ID { get; set; }
        public string Screen_Name { get; set; }
        public string Screen_Caption { get; set; }
        public bool Can_Show { get; set; }
        public bool Can_Open { get; set; }
        public bool Can_Edit { get; set; }
        public bool Can_Delete { get; set; }
        public bool Can_Print { get; set; }
        public bool Can_Add { get; set; }
        public List<Screen_Actions> Actions { get; set; }
    }
}
