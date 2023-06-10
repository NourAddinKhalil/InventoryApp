using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.Classes
{
    public class Cash_Out_Guaranteed_Settings
    {
        private int Profile_ID { get; set; }
        public Cash_Out_Guaranteed_Settings(int profile_ID)
        {
            Profile_ID = profile_ID;
        }
        public bool Can_Make_Note_For_Customer
        {
            get
            {
                return Master_Class.From_Byte_Array_To_AnyType<bool>
                    ((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID)
                    ));
            }
        }
        public bool Can_Change_Note_Date
        {
            get
            {
                return Master_Class.From_Byte_Array_To_AnyType<bool>
                    ((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID)
                    ));
            }
        }
        public bool Can_Make_Note_For_More_Than_One_Invoice
        {
            get
            {
                return Master_Class.From_Byte_Array_To_AnyType<bool>
                    ((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID)
                    ));
            }
        }
    }
}
