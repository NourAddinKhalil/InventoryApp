using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.Classes
{
    public static class Messages
    {
        public static string Necessary_Field
        {
            get
            {
                return "This Field Is Necessary";
            }
        }
        public static string Name_Exist
        {
            get
            {
                return "The Name Is Already Exist";
            }
        }
        public static string Code_Exist
        {
            get
            {
                return "The Code Is Already Exist";
            }
        }
        public static string BaraCode_Exist
        {
            get
            {
                return "The BaraCode Is Already Exist";
            }
        }
        public static string Factor_Exist
        {
            get
            {
                return "Factor Already Exist";
            }
        }
        public static string Unit_Exist
        {
            get
            {
                return "Unit Name Already Exist";
            }
        }
        public static string Same_Vlaue
        {
            get
            {
                return "Can Not Transfer To The Same Store";
            }
        }
        public static string More_Than_Zero
        {
            get
            {
                return "The Value Must Be More Than Zero ";
            }
        }
        public static string Qty_More_Than_Balance
        {
            get
            {
                return "The Quantity More Than Balance ";
            }
        }
        public static string Can_Not_Be_Credit_And_Debit_Same_Time
        {
            get
            {
                return "Account Can Not Be Credit And Debit In The Same Time ";
            }
        }
        public static string Can_Not_Be_Credit_And_Debit_Zero
        {
            get
            {
                return "Account Can Not Hold Zero Value Credit And Debit In The Same Time ";
            }
        }
    }
}
