using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.Classes
{
    public class Purchase_Invoice_Settings
    {
        int Profile_ID { get; set; }
        public Purchase_Invoice_Settings(int profile_Id)
        {
            Profile_ID = profile_Id;
        }
        public bool CanChangeItemPriceInBuy { get { return Master_Class.From_Byte_Array_To_AnyType<bool>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public bool CanBuyFromCustomer { get { return Master_Class.From_Byte_Array_To_AnyType<bool>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public bool CanChangeBuyBillDate { get { return Master_Class.From_Byte_Array_To_AnyType<bool>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
    }
}
