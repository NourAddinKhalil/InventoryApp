using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.Classes
{
    public class Invoices_Settings
    {
        int Profile_ID { get; set; }
        public Invoices_Settings(int profile_Id)
        {
            Profile_ID = profile_Id;
        }
        public bool CanChangeDeleteItemsInBills { get { return Master_Class.From_Byte_Array_To_AnyType<bool>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public bool CanChangeTax { get { return Master_Class.From_Byte_Array_To_AnyType<bool>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public Warining_Handel WhenSellingItemWithQuantityMoreThanAvailable { get { return Master_Class.From_Byte_Array_To_AnyType<Warining_Handel>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
    }
}
