using DevExpress.Utils;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.Classes
{
    public class User_Settings_Propes
    {
        int Profile_ID { get; set; }
        public User_Settings_Propes(int profile_ID)
        {
            Profile_ID = profile_ID;
            General_Settings = new General_Settings(Profile_ID);
            Invoice_Settings = new Invoices_Settings(Profile_ID);
            Sale_Settings = new Sales_Invoices_Settings(Profile_ID);
            Purchase_Settings = new Purchase_Invoice_Settings(Profile_ID);
            Cash_In_Settings = new Cash_In_Guaranteed_Settings(Profile_ID);
            Cash_Out_Settings = new Cash_Out_Guaranteed_Settings(Profile_ID);
        }
        public General_Settings General_Settings { get; set; }
        public Invoices_Settings Invoice_Settings { get; set; }
        public Sales_Invoices_Settings Sale_Settings { get; set; }
        public Purchase_Invoice_Settings Purchase_Settings { get; set; }
        public Cash_In_Guaranteed_Settings Cash_In_Settings { get; set; }
        public Cash_Out_Guaranteed_Settings Cash_Out_Settings { get; set; }

        public static string Get_Property_Name(string prop_Name)
        {
            User_Settings_Propes ins;
            switch (prop_Name)
            {
                case nameof(General_Settings):
                    return "إعدادت عامة ";
                case nameof(Invoice_Settings):
                    return "إعدادت الفواتير ";
                case nameof(Sale_Settings):
                    return "إعدادت المبيعات ";
                case nameof(Purchase_Settings):
                    return "إعدادت المشتريات ";
                case nameof(Cash_In_Settings):
                    return "إعدادت سندات القبض ";
                case nameof(Cash_Out_Settings):
                    return "إعدادت سندات الدفع ";
                case nameof(ins.General_Settings.DefualtStore):
                    return " المخزن الإفتراضي";
                case nameof(ins.General_Settings.CanChangeCustomer):
                    return "هل يمكنه تغيير العميل";
                case nameof(ins.General_Settings.CanChangeDrawer):
                    return "هل يمكنه تغيير الخزينة";
                case nameof(ins.General_Settings.CanChangeStore):
                    return "هل يمكنه تغيير المخزن";
                case nameof(ins.General_Settings.CanChangeSupplier):
                    return "هل يمكنه تغيير المورد";
                case nameof(ins.General_Settings.CanSeeDocumentHistory):
                    return "هل يمكنه رؤية سجلات الفواتير";
                case nameof(ins.General_Settings.DefualtBranch):
                    return "مخزن الخامات الإفتراضي";
                case nameof(ins.General_Settings.DefualtCustomer):
                    return "العميل الإفتراضي";
                case nameof(ins.General_Settings.DefualtDrawer):
                    return "الخزينة الإفتراضية";
                case nameof(ins.General_Settings.DefualtSupplier):
                    return "المورد الإفتراضي";
                case nameof(ins.General_Settings.When_Transfer_Balanece_More_Than_Exsist_Between_Stores):
                    return "عند تحويل كمية من مخزن أكبر من الموجودة";
                case nameof(ins.General_Settings.When_Transfer_Money_More_Than_Exsist_Between_Accounts):
                    return "عند تحويل رصيد من حساب أكبر من الرصيد الموجود";
                case nameof(ins.Invoice_Settings.CanChangeDeleteItemsInBills):
                    return "هل يمكنه حذف صنف في الفاتورة";
                case nameof(ins.Invoice_Settings.CanChangeTax):
                    return "هل يمكنه تغيير قيمة الضرائب";
                case nameof(ins.Purchase_Settings.CanBuyFromCustomer):
                    return "هل يمكنه الشراء من عميل";
                case nameof(ins.Purchase_Settings.CanChangeBuyBillDate):
                    return "هل يمكنه تغيير تاريخ الفاتورة";
                case nameof(ins.Purchase_Settings.CanChangeItemPriceInBuy):
                    return "هل يمكنه تغيير سعر المنتج";
                case nameof(ins.Sale_Settings.CanChangeItemPriceInSales):
                    return "هل يمكنه تغيير سعر المنتج";
                case nameof(ins.Sale_Settings.CanChangePaidInSales):
                    return "هل يمكنه تغيير قيمة المدفوع";
                case nameof(ins.Sale_Settings.CanChangeQuantityInSales):
                    return "هل يمكنه تغيير الكمية";
                case nameof(ins.Sale_Settings.CanChangeSalesBillDate):
                    return "هل يمكنه تغيير تاريخ الفاتورة";
                case nameof(ins.Sale_Settings.CanPostToStoreInSales):
                    return "هل يمكنه البيع بدون تصريف من المخزن";
                case nameof(ins.Sale_Settings.CanSellToSupplier):
                    return "هل يمكنه البيع لمورد";
                case nameof(ins.Sale_Settings.DefualtPayMethodInSales):
                    return "طريقة الدفع الإفتراضية";
                case nameof(ins.Sale_Settings.HideCostInSales):
                    return "إخفاء سعر التكلفة";
                case nameof(ins.Sale_Settings.MaxDiscountLevelInBills):
                    return "أقصى حد لقيمة الخصم على الفاتورة";
                case nameof(ins.Sale_Settings.MaxDiscountLevelPerItem):
                    return "أقصى حد لقيمة الخصم على كل منتج";
                case nameof(ins.Sale_Settings.WhenSellingItemReachedReorderLimit):
                    return "عند بيع منتج يصل إلى حد الطلب";
                case nameof(ins.Sale_Settings.WhenSellingItemWithPriceLessThanCostPrice):
                    return "عند بيع منتج بسعر أقل من سعر التكلفة";
                case nameof(ins.Invoice_Settings.WhenSellingItemWithQuantityMoreThanAvailable):
                    return "عند بيع منتج كميته أكبر من الكمية في المخزن";
                case nameof(ins.Sale_Settings.WhenSellingToCustomerOverInsuranceLimit):
                    return "عند البيع ل عميل وصل للحد الأقصى للسلفة";
                case nameof(ins.Cash_In_Settings.Can_Change_Note_In_Date):
                case nameof(ins.Cash_Out_Settings.Can_Change_Note_Date):
                    return "هل يمكنه تغيير تاريخ السند";
                case nameof(ins.Cash_In_Settings.Can_Make_Note_In_For_More_Than_One_Invoice):
                case nameof(ins.Cash_Out_Settings.Can_Make_Note_For_More_Than_One_Invoice):
                    return "هل يمكنه عمل سند لأكثر من فاتورة ";
                case nameof(ins.Cash_In_Settings.Can_Make_Note_In_For_Supplier):
                    return "هل يمكنه عمل سند قبض من مورد ";
                case nameof(ins.Cash_In_Settings.Max_Note_In_Discount):
                    return "أقصى حد لقيمة الخصم على السند ";
                case nameof(ins.Cash_Out_Settings.Can_Make_Note_For_Customer):
                    return "هل يمكنه عمل سند دفع ل عميل ";
                default:
                    return $"@{prop_Name}@ ";
            }
        }
        public static BaseEdit Get_Property_Control(string prop_name, object PropValue)
        {
            User_Settings_Propes ins;
            BaseEdit edit = null;
            switch (prop_name)
            {
                case nameof(ins.General_Settings.CanChangeDrawer):
                case nameof(ins.General_Settings.CanChangeStore):
                case nameof(ins.General_Settings.CanChangeSupplier):
                case nameof(ins.General_Settings.CanSeeDocumentHistory):
                case nameof(ins.General_Settings.CanChangeCustomer):
                case nameof(ins.Invoice_Settings.CanChangeDeleteItemsInBills):
                case nameof(ins.Invoice_Settings.CanChangeTax):
                case nameof(ins.Sale_Settings.CanChangeItemPriceInSales):
                case nameof(ins.Sale_Settings.CanChangePaidInSales):
                case nameof(ins.Sale_Settings.CanChangeQuantityInSales):
                case nameof(ins.Sale_Settings.CanChangeSalesBillDate):
                case nameof(ins.Sale_Settings.CanPostToStoreInSales):
                case nameof(ins.Sale_Settings.CanSellToSupplier):
                case nameof(ins.Sale_Settings.HideCostInSales):
                case nameof(ins.Purchase_Settings.CanBuyFromCustomer):
                case nameof(ins.Purchase_Settings.CanChangeBuyBillDate):
                case nameof(ins.Purchase_Settings.CanChangeItemPriceInBuy):
                case nameof(ins.Cash_In_Settings.Can_Change_Note_In_Date):
                case nameof(ins.Cash_In_Settings.Can_Make_Note_In_For_More_Than_One_Invoice):
                case nameof(ins.Cash_In_Settings.Can_Make_Note_In_For_Supplier):
                case nameof(ins.Cash_Out_Settings.Can_Change_Note_Date):
                case nameof(ins.Cash_Out_Settings.Can_Make_Note_For_Customer):
                case nameof(ins.Cash_Out_Settings.Can_Make_Note_For_More_Than_One_Invoice):
                    edit = new ToggleSwitch();
                    ((ToggleSwitch)edit).Properties.OnText = "نعم";
                    ((ToggleSwitch)edit).Properties.OffText = "لا";
                    break;
                case nameof(ins.General_Settings.DefualtBranch):
                case nameof(ins.General_Settings.DefualtStore):
                    edit = new LookUpEdit();
                    ((LookUpEdit)edit).Initilaze_Data(Session.Stores, clear: true);
                    break;
                case nameof(ins.General_Settings.DefualtDrawer):
                    edit = new LookUpEdit();
                    ((LookUpEdit)edit).Initilaze_Data(Session.Drawers, clear: true);
                    break;
                case nameof(ins.General_Settings.DefualtCustomer):
                    edit = new LookUpEdit();
                    ((LookUpEdit)edit).Initilaze_Data(Session.Customers, clear: true);
                    break;
                case nameof(ins.General_Settings.DefualtSupplier):
                    edit = new LookUpEdit();
                    ((LookUpEdit)edit).Initilaze_Data(Session.Suppliers, clear: true);
                    break;
                case nameof(ins.Sale_Settings.DefualtPayMethodInSales):
                    edit = new LookUpEdit();
                    ((LookUpEdit)edit).Initilaze_Data(Master_Class.Pay_Method_List, clear: true);
                    break;
                case nameof(ins.Sale_Settings.WhenSellingItemReachedReorderLimit):
                case nameof(ins.Sale_Settings.WhenSellingItemWithPriceLessThanCostPrice):
                case nameof(ins.Invoice_Settings.WhenSellingItemWithQuantityMoreThanAvailable):
                case nameof(ins.Sale_Settings.WhenSellingToCustomerOverInsuranceLimit):
                case nameof(ins.General_Settings.When_Transfer_Balanece_More_Than_Exsist_Between_Stores):
                case nameof(ins.General_Settings.When_Transfer_Money_More_Than_Exsist_Between_Accounts):
                    edit = new LookUpEdit();
                    ((LookUpEdit)edit).Initilaze_Data(Master_Class.Warning_Handel_List, clear: true);
                    break;
                case nameof(ins.Sale_Settings.MaxDiscountLevelInBills):
                case nameof(ins.Sale_Settings.MaxDiscountLevelPerItem):
                case nameof(ins.Cash_In_Settings.Max_Note_In_Discount):
                    edit = new SpinEdit();
                    ((SpinEdit)edit).Properties.Increment = 0.01m;
                    ((SpinEdit)edit).Properties.Mask.EditMask = "p";
                    ((SpinEdit)edit).Properties.Mask.UseMaskAsDisplayFormat = true;
                    ((SpinEdit)edit).Properties.MaxValue = 1;
                    ((SpinEdit)edit).Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                    ((SpinEdit)edit).Properties.Appearance.TextOptions.VAlignment = VertAlignment.Center;

                    break;
                default:
                    break;
            }
            if (edit != null)
            {
                edit.Name = prop_name;
                edit.Properties.NullText = "";
                edit.EditValue = PropValue;
            }
            return edit;
        }
    }
}
