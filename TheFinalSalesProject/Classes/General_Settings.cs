using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.Classes
{
    public class General_Settings
    {
        int Profile_ID { get; set; }
        public General_Settings(int profile_Id)
        {
            Profile_ID = profile_Id;
        }
        public int DefualtBranch { get { return Master_Class.From_Byte_Array_To_AnyType<int>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public int DefualtStore { get { return Master_Class.From_Byte_Array_To_AnyType<int>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public int DefualtDrawer { get { return Master_Class.From_Byte_Array_To_AnyType<int>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public int DefualtCustomer { get { return Master_Class.From_Byte_Array_To_AnyType<int>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public int DefualtSupplier { get { return Master_Class.From_Byte_Array_To_AnyType<int>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public bool CanChangeStore { get { return Master_Class.From_Byte_Array_To_AnyType<bool>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public bool CanChangeDrawer { get { return Master_Class.From_Byte_Array_To_AnyType<bool>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public bool CanChangeCustomer { get { return Master_Class.From_Byte_Array_To_AnyType<bool>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public bool CanChangeSupplier { get { return Master_Class.From_Byte_Array_To_AnyType<bool>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public bool CanSeeDocumentHistory { get { return Master_Class.From_Byte_Array_To_AnyType<bool>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public Warining_Handel When_Transfer_Balanece_More_Than_Exsist_Between_Stores 
        { 
            get 
            { 
                return Master_Class.From_Byte_Array_To_AnyType<Warining_Handel>
                    ((
                    Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID)
                    )); 
            } 
        }
        public Warining_Handel When_Transfer_Money_More_Than_Exsist_Between_Accounts 
        { 
            get 
            { 
                return Master_Class.From_Byte_Array_To_AnyType<Warining_Handel>
                    ((
                    Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID)
                    )); 
            } 
        }
    }
}
