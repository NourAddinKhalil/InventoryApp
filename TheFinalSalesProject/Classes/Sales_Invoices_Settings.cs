using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.Classes
{
    public class Sales_Invoices_Settings
    {
        private int Profile_ID { get; set; }
        public Sales_Invoices_Settings(int profile_Id)
        {
            Profile_ID = profile_Id;
        }
        public bool CanChangePaidInSales { get { return Master_Class.From_Byte_Array_To_AnyType<bool>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public bool CanPostToStoreInSales { get { return Master_Class.From_Byte_Array_To_AnyType<bool>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public bool CanChangeItemPriceInSales { get { return Master_Class.From_Byte_Array_To_AnyType<bool>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public bool HideCostInSales { get { return Master_Class.From_Byte_Array_To_AnyType<bool>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public bool CanSellToSupplier { get { return Master_Class.From_Byte_Array_To_AnyType<bool>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public bool CanChangeSalesBillDate { get { return Master_Class.From_Byte_Array_To_AnyType<bool>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public bool CanChangeQuantityInSales { get { return Master_Class.From_Byte_Array_To_AnyType<bool>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public decimal MaxDiscountLevelInBills { get { return Master_Class.From_Byte_Array_To_AnyType<decimal>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public decimal MaxDiscountLevelPerItem { get { return Master_Class.From_Byte_Array_To_AnyType<decimal>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public Pay_Mode DefualtPayMethodInSales { get { return Master_Class.From_Byte_Array_To_AnyType<Pay_Mode>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public Warining_Handel WhenSellingToCustomerOverInsuranceLimit { get { return Master_Class.From_Byte_Array_To_AnyType<Warining_Handel>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public Warining_Handel WhenSellingItemReachedReorderLimit { get { return Master_Class.From_Byte_Array_To_AnyType<Warining_Handel>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
        public Warining_Handel WhenSellingItemWithPriceLessThanCostPrice { get { return Master_Class.From_Byte_Array_To_AnyType<Warining_Handel>((Master_Class.Get_Property_Value(Master_Class.Get_Property_Name(), Profile_ID))); } }
    }
}
