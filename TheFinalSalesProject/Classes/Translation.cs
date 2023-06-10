using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.Classes
{
    public static class Translation
    {
        public static void Grid_View_Translate_Column(this GridView view, string tableName)
        {
            int index = 0;
            view.BestFitColumns();
            switch (tableName)
            {
                case "Pro_Balance":
                    index = 0;
                    #region Pro_Balance
                    view.Columns["ID"].Visible = false;
                    view.Columns["Cate_ID"].Visible = false;
                    view.Columns["ProductName"].Caption = "إسم الصنف";
                    view.Columns["CateName"].Caption = "إسم المجموعة";
                    view.Columns["Code"].Caption = "كود الصنف";
                    view.Columns["Product_ID"].Visible = false;
                    view.Columns["Store_ID"].Visible = false;
                    view.Columns["User_ID"].Visible = false;
                    view.Columns["Real_Name"].Caption = "إسم المستخدم";
                    view.Columns["Source_Type"].Caption = "نوع السند";
                    view.Columns["Source_ID"].Caption = "رقم السند";
                    view.Columns["Export_Qty"].Caption = "الكمية الصادرة";
                    view.Columns["Import_Qty"].Caption = "الكمية الواردة";
                    view.Columns["Cost_Value"].Caption = "سعر التكلفة";
                    view.Columns["Insert_Date"].Caption = "تاريخ الحركة";
                    view.Columns["Notes"].Caption = "ملاحظات";
                    view.Columns["StoreName"].Caption = "المخزن";

                    view.Columns["StoreName"].VisibleIndex = index++;
                    view.Columns["CateName"].VisibleIndex = index++;
                    view.Columns["ProductName"].VisibleIndex = index++;
                    view.Columns["Code"].VisibleIndex = index++;
                    view.Columns["Cost_Value"].VisibleIndex = index++;
                    view.Columns["Source_Type"].VisibleIndex = index++;
                    view.Columns["Source_ID"].VisibleIndex = index++;
                    view.Columns["Insert_Date"].VisibleIndex = index++;
                    view.Columns["Export_Qty"].VisibleIndex = index++;
                    view.Columns["Import_Qty"].VisibleIndex = index++;
                    view.Columns["Notes"].VisibleIndex = index++;
                    view.Columns["Real_Name"].VisibleIndex = index++;
                    #endregion
                    break;
                case "Accounts":
                    view.Columns["ID"].Visible = false;
                    view.Columns["Name"].Caption = "الإسم";
                    view.Columns["Code"].Caption = "الكود";
                    view.Columns["Code"].VisibleIndex = 0;
                    view.Columns["Name"].VisibleIndex = 1;
                    view.Columns["Parent_ID"].Visible = false;
                    break;
                case "Type":
                    view.Columns["ID"].Visible = false;
                    view.Columns["Name"].Caption = "الإسم";
                    break;
                case "FullCoupes":
                    index = 0;
                    #region FullCoupes
                    view.Columns["ID"].Visible = false;
                    view.Columns["Code"].Caption = "الكود";
                    view.Columns["Account_ID"].Caption = "رقم الحساب";
                    view.Columns["Debit"].Caption = "مدين";
                    view.Columns["Credit"].Caption = "دائن";
                    view.Columns["Insert_Date"].Caption = "تاريخ الحركة";
                    view.Columns["Source_Type"].Caption = "نوع الحركة";
                    view.Columns["Source_ID"].Caption = "رقم المستند";
                    view.Columns["Notes"].Caption = "ملاحظات";
                    view.Columns["Move_Name"].Caption = "إسم الحركة";
                    view.Columns["User_ID"].Visible = false;
                    view.Columns["Name"].Caption = "إسم الحساب";
                    view.Columns["Real_Name"].Caption = "إسم المستخدم";
                    view.Columns["Number"].VisibleIndex = index++;
                    view.Columns["Name"].VisibleIndex = index++;
                    view.Columns["Account_ID"].VisibleIndex = index++;
                    view.Columns["Code"].VisibleIndex = index++;
                    view.Columns["Insert_Date"].VisibleIndex = index++;
                    view.Columns["Source_Type"].VisibleIndex = index++;
                    view.Columns["Debit"].VisibleIndex = index++;
                    view.Columns["Credit"].VisibleIndex = index++;
                    view.Columns["Source_ID"].VisibleIndex = index++;
                    view.Columns["Notes"].VisibleIndex = index++;
                    view.Columns["Move_Name"].VisibleIndex = index++;
                    view.Columns["Real_Name"].VisibleIndex = index++;
                    #endregion
                    break;
                case "Coupes":
                    index = 0;
                    #region Coupes
                    view.Columns["ID"].Visible = false;
                    view.Columns["User_ID"].Visible = false;
                    view.Columns["Code"].Caption = "الكود";
                    view.Columns["Account_ID"].Caption = "إسم الحساب";
                    view.Columns["Debit"].Caption = "مدين";
                    view.Columns["Credit"].Caption = "دائن";
                    view.Columns["Insert_Date"].Caption = "تاريخ الحركة";
                    view.Columns["Source_Type"].Caption = "نوع الحركة";
                    view.Columns["Source_ID"].Caption = "رقم المستند";
                    view.Columns["Notes"].Caption = "ملاحظات";
                    view.Columns["Move_Name"].Caption = "إسم الحركة";
                    view.Columns["Number"].VisibleIndex = index++;
                    view.Columns["Code"].VisibleIndex = index++;
                    view.Columns["Account_ID"].VisibleIndex = index++;
                    view.Columns["Debit"].VisibleIndex = index++;
                    view.Columns["Credit"].VisibleIndex = index++;
                    view.Columns["Insert_Date"].VisibleIndex = index++;
                    view.Columns["Source_Type"].VisibleIndex = index++;
                    view.Columns["Move_Name"].VisibleIndex = index++;
                    view.Columns["Notes"].VisibleIndex = index++;
                    #endregion
                    break;
                case "Prope_Names":
                    view.Columns["Name"].Caption = "الإسم";
                    view.Columns["ID"].Visible = false;
                    break;
                case "Invoice_List":
                    index = 0;
                    #region Invoice_List
                    view.Columns["ID"].Caption = "رقم الفاتورة";
                    view.Columns["Code"].Caption = "الكود";
                    view.Columns["Type"].Caption = "نوع الفاتورة";
                    view.Columns["Person_Type"].Caption = "نوع العميل";
                    view.Columns["PersonName"].Caption = "إسم العميل / المورد";
                    view.Columns["Date"].Caption = "تاريخ الفاتورة";
                    view.Columns["Delivary_Date"].Caption = "تاريخ التوصيل";
                    view.Columns["StoreName"].Caption = "إسم المخزن";
                    view.Columns["Notes"].Caption = "ملاحظات";
                    view.Columns["Is_Posted_To_Store"].Caption = "مرحّل للمخزن";
                    view.Columns["Post_Date"].Caption = "تاريخ الترحيل";
                    view.Columns["Total"].Caption = "الإجمالي";
                    view.Columns["Discount_Val"].Caption = "قيمة الخصم";
                    view.Columns["Discount_Perec"].Caption = "نسبة الخصم";
                    view.Columns["Tax_Val"].Caption = "قيمة الضريبة";
                    view.Columns["Tax_Perce"].Caption = "نسبة الضريبة";
                    view.Columns["Expences"].Caption = "المصروفات";
                    view.Columns["Net"].Caption = "الصافي";
                    view.Columns["Paid"].Caption = "المدفوع";
                    view.Columns["DrawerName"].Caption = "إسم الخزنة";
                    view.Columns["Remaining"].Caption = "المتبقي";
                    view.Columns["Shipping_Address"].Caption = "عنوان الشحن";
                    //view.Columns["PayStuts"].Caption = "حالة الفاتورة";
                    //view.Columns["Invoice_Back_ID"].Caption = "مصدر الفاتورة";
                    view.Columns["Invoice_Back_ID"].Visible = false;
                    view.Columns["Real_Name"].Caption = "إسم المستخدم";
                    ///Visable Index
                    view.Columns["Number"].VisibleIndex = index++;
                    view.Columns["ID"].VisibleIndex = index++;
                    view.Columns["Code"].VisibleIndex = index++;
                    view.Columns["Type"].VisibleIndex = index++;
                    view.Columns["Person_Type"].VisibleIndex = index++;
                    view.Columns["PersonName"].VisibleIndex = index++;
                    view.Columns["Date"].VisibleIndex = index++;
                    view.Columns["Delivary_Date"].VisibleIndex = index++;
                    view.Columns["StoreName"].VisibleIndex = index++;
                    view.Columns["Notes"].VisibleIndex = index++;
                    view.Columns["Is_Posted_To_Store"].VisibleIndex = index++;
                    view.Columns["Post_Date"].VisibleIndex = index++;
                    view.Columns["Total"].VisibleIndex = index++;
                    view.Columns["Discount_Val"].VisibleIndex = index++;
                    view.Columns["Discount_Perec"].VisibleIndex = index++;
                    view.Columns["Tax_Val"].VisibleIndex = index++;
                    view.Columns["Tax_Perce"].VisibleIndex = index++;
                    view.Columns["Expences"].VisibleIndex = index++;
                    view.Columns["Net"].VisibleIndex = index++;
                    view.Columns["Paid"].VisibleIndex = index++;
                    view.Columns["DrawerName"].VisibleIndex = index++;
                    view.Columns["Remaining"].VisibleIndex = index++;
                    view.Columns["Shipping_Address"].VisibleIndex = index++;
                    view.Columns["Real_Name"].VisibleIndex = index++;
                    ///visable
                    view.Columns["Discount_Perec"].Visible = false;
                    view.Columns["Drawer_ID"].Visible = false;
                    view.Columns["User_ID"].Visible = false;
                    view.Columns["Tax_Perce"].Visible = false;
                    view.Columns["Type"].Visible = false;
                    view.Columns["Person_Type"].Visible = false;
                    view.Columns["Person_ID"].Visible = false;
                    view.Columns["Store_ID"].Visible = false;
#endregion
                    break;
                case "Person":
                    view.Columns["ID"].Caption = "الكود";
                    view.Columns["Name"].Caption = "الإسم";
                    view.Columns["Phone"].Caption = "الجوال";
                    view.Columns["Mobile"].Caption = "الهاتف";
                    view.Columns["Address"].Caption = "العنوان";
                    view.Columns["Account_ID"].Caption = "رقم الحساب";
                    view.Columns["Type"].Caption = "النوع";
                    view.Columns["Max_Credit"].Caption = "حد الإئتمان";
                    view.Columns["Opening_Balance"].Caption = "رصيد إفتتاحي";
                    view.Columns["ID"].Visible =
                    view.Columns["Type"].Visible = false;
                    break;
                case "Drawer":
                    view.Columns["ID"].Visible = false;
                    view.Columns["Name"].Caption = "إسم الخزينة";
                    view.Columns["Account_ID"].Caption = " رقم حساب الخزينة";
                    break;
                case "Product":
                    view.Columns["Cate_Name"].Caption = " إسم الفئة";
                    view.Columns["Code"].Caption = "كود الصنف ";
                    view.Columns["Is_Active"].Caption = " نشط أم لا";
                    view.Columns["Discribtion"].Caption = "الوصف";
                    view.Columns["Name"].Caption = "إسم الصنف";
                    view.Columns["Type"].Caption = " النوع";
                    view.Columns["Cate_ID"].Visible = false;
                    view.Columns["ID"].Visible = false;
                    view.Columns["Cost_Calc_Method"].Caption = "طريقة التوزيع";
                    view.Columns["Order_Limit"].Caption = "حد الطلب";
                    break;
                case "Store":
                    view.Columns["ID"].Visible = false;
                    view.Columns["Name"].Caption = "إسم المخزن";
                    view.Columns["Sales_Account_ID"].Caption = "رقم حساب المبيعات";
                    view.Columns["Inventory_Account_ID"].Caption = "رقم حساب المخزن";
                    view.Columns["Cost_Of_Sold_Good_Account_ID"].Caption = "رقم حساب تكلفة البضاعة المباعة";
                    view.Columns["Discount_Received_Account_ID"].Caption = "رقم حساب الخصم المحصل عليه";
                    view.Columns["Discount_Allowed_Account_ID"].Caption = "رقم حساب الخصم المسموح به";
                    view.Columns["Sales_Return_Account_ID"].Caption = "رقم حساب مرتجع المبيعات";
                    break;
                case "User":
                    view.Columns["ID"].Visible = false;
                    view.Columns["Real_Name"].Caption = "الإسم";
                    view.Columns["User_Name"].Caption = "إسم المستخدم";
                    view.Columns["Password"].Caption = "كلمة السر";
                    view.Columns["Type"].Caption = "النوع";
                    view.Columns["Setting_Profile_ID"].Visible = false;
                    view.Columns["Setting_Screen_ID"].Visible = false;
                    view.Columns["Is_Active"].Caption = "نشط";
                    view.Columns["Is_Online"].Caption = "متصل";
                    break;
                case "Unit":
                    view.Columns["ID"].Caption = "الرقم";
                    view.Columns["Name"].Caption = "الإسم";
                    break;
                case "GuaraNote":
                    index = 0;
                    #region GuaraNote
                    view.Columns["ID"].Caption = "رقم السند";
                    view.Columns["Number"].VisibleIndex = index++;
                    view.Columns["ID"].VisibleIndex = index++;
                    view.Columns["Is_Cash_In"].Visible = false;
                    view.Columns["Drawer_ID"].Visible = false;
                    view.Columns["Store_ID"].Visible = false;
                    view.Columns["Part_ID"].Visible = false;
                    view.Columns["Code"].Caption = "الكود";
                    view.Columns["Code"].VisibleIndex = index++;
                    view.Columns["Invoice_ID"].Caption = "رقم الفاتورة";
                    view.Columns["Invoice_ID"].VisibleIndex = index++;
                    view.Columns["Invoice_Type"].Visible = false;
                    view.Columns["inv_Type_Name"].Caption = "نوع الفاتورة";
                    view.Columns["inv_Type_Name"].VisibleIndex = index++;
                    view.Columns["PersonName"].Caption = "الإسم";
                    view.Columns["PersonName"].VisibleIndex = index++;
                    view.Columns["Part_Type"].Caption = "نوع الطرف";
                    view.Columns["Part_Type"].VisibleIndex = index++;
                    view.Columns["Date"].Caption = "التاريخ";
                    view.Columns["Date"].VisibleIndex = index++;
                    view.Columns["StoreName"].Caption = "الفرع";
                    view.Columns["StoreName"].VisibleIndex = index++;
                    view.Columns["Amount"].Caption = "المبلغ";
                    view.Columns["Amount"].VisibleIndex = index++;
                    view.Columns["Discount_Val"].Caption = "قيمة الخصم";
                    view.Columns["Discount_Val"].VisibleIndex = index++;
                    view.Columns["Note"].Caption = "ملاحظات";
                    view.Columns["Note"].VisibleIndex = index++;
                    view.Columns["User_ID"].Visible = false;
                    view.Columns["DrawerName"].Caption = "الخزينة";
                    view.Columns["DrawerName"].VisibleIndex = index++;
                    view.Columns["Real_Name"].Caption = "إسم المستخدم";
                    view.Columns["Real_Name"].VisibleIndex = index++;
                    view.Columns["Phone"].Visible = false;
                    view.Columns["Mobile"].Visible = false;
                    view.Columns["Total_Lett"].Visible = false;
                    #endregion
                    break;
                default:
                    break;
            }
        }
        public static void Look_Up_Edit_Translate_Column(this LookUpEdit lkp, string tableName)
        {
            //lkp.Properties.PopulateColumns();
            switch (tableName)
            {
                case "Types":
                    lkp.Properties.Columns["ID"].Caption = "الرقم";
                    lkp.Properties.Columns["Name"].Caption = "الإسم";
                    break;
                case "Invoic":
                    break;
                case "Person":
                    lkp.Properties.Columns["ID"].Caption = "الكود";
                    lkp.Properties.Columns["Name"].Caption = "الإسم";
                    lkp.Properties.Columns["Phone"].Caption = "الجوال";
                    lkp.Properties.Columns["Mobile"].Caption = "الهاتف";
                    lkp.Properties.Columns["Address"].Caption = "العنوان";
                    lkp.Properties.Columns["Account_ID"].Caption = "رقم الحساب";
                    lkp.Properties.Columns["Type"].Caption = "النوع";
                    lkp.Properties.Columns["Max_Credit"].Caption = "حد الإئتمان";
                    lkp.Properties.Columns["Opening_Balance"].Caption = "رصيد إفتتاحي";
                    break;
                case "Drawer":
                    lkp.Properties.Columns["ID"].Caption = "الكود";
                    lkp.Properties.Columns["Name"].Caption = "الإسم";
                    lkp.Properties.Columns["Account_ID"].Caption = "رقم الحساب";
                    lkp.Properties.Columns["ID"].Visible = false;
                    lkp.Properties.Columns["Account_ID"].Visible = false;
                    break;
                case "Store":
                   lkp.Properties.Columns["Sales_Account_ID"].Visible =
                   lkp.Properties.Columns["Sales_Return_Account_ID"].Visible =
                   lkp.Properties.Columns["Inventory_Account_ID"].Visible =
                   lkp.Properties.Columns["Cost_Of_Sold_Good_Account_ID"].Visible =
                   lkp.Properties.Columns["Discount_Received_Account_ID"].Visible =
                   lkp.Properties.Columns["Discount_Allowed_Account_ID"].Visible = false;
                   lkp.Properties.Columns["ID"].Caption = "الرقم";
                   lkp.Properties.Columns["Name"].Caption = "الإسم";
                    break;
                case "Unit":
                    lkp.Properties.Columns["ID"].Caption = "الرقم";
                    lkp.Properties.Columns["Name"].Caption = "الإسم";
                    break;
                case "Accounts":
                    lkp.Properties.Columns["ID"].Visible = false;
                    lkp.Properties.Columns["Name"].Caption = "الإسم";
                    lkp.Properties.Columns["Code"].Caption = "الكود";
                    lkp.Properties.Columns["Parent_ID"].Visible = false;
                    break;
                default:
                    break;
            }
        }
        public static void Repo_Look_Up_Edit_Translate_Column(this RepositoryItemLookUpEdit rlkp, string tableName)
        {
            //lkp.Properties.PopulateColumns();
            switch (tableName)
            {
                case "Type":
                    rlkp.Columns["ID"].Caption = "الكود";
                    rlkp.Columns["Name"].Caption = "الإسم";
                    break;
                case "Invoic":
                    break;
                case "Person":
                    rlkp.Columns["ID"].Caption = "الكود";
                    rlkp.Columns["Name"].Caption = "الإسم";
                    rlkp.Columns["Phone"].Caption = "الجوال";
                    rlkp.Columns["Mobile"].Caption = "الهاتف";
                    rlkp.Columns["Address"].Caption = "العنوان";
                    rlkp.Columns["Account_ID"].Caption = "رقم الحساب";
                    rlkp.Columns["Type"].Caption = "النوع";
                    rlkp.Columns["Max_Credit"].Caption = "حد الإئتمان";
                    rlkp.Columns["Opening_Balance"].Caption = "رصيد إفتتاحي";
                    break;
                case "Drawer":
                    rlkp.Columns["ID"].Caption = "الكود";
                    rlkp.Columns["Name"].Caption = "الإسم";
                    rlkp.Columns["Account_ID"].Caption = "رقم الحساب";
                    rlkp.Columns["ID"].Visible = false;
                    rlkp.Columns["Account_ID"].Visible = false;
                    break;
                case "Store":
                    rlkp.Columns["Sales_Account_ID"].Visible =
                    rlkp.Columns["Sales_Return_Account_ID"].Visible =
                    rlkp.Columns["Inventory_Account_ID"].Visible =
                    rlkp.Columns["Cost_Of_Sold_Good_Account_ID"].Visible =
                    rlkp.Columns["Discount_Received_Account_ID"].Visible =
                    rlkp.Columns["Discount_Allowed_Account_ID"].Visible =
                    rlkp.Columns["ID"].Visible = false;
                    rlkp.Columns["Name"].Caption = "الإسم";
                    break;
                case "Unit":
                    rlkp.Columns["ID"].Visible =false;
                    rlkp.Columns["Name"].Caption = "الإسم";
                    break;
                default:
                    break;
            }
        }
    }
}