using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static TheFinalSalesProject.Classes.Enum_Choices;
using TheFinalSalesProject.MyForms;

namespace TheFinalSalesProject.Classes
{
    public static class Screens
    {
        public static User_Screen_Access main_Page = new User_Screen_Access("elm_Main_Page")
        {
            Screen_Caption = "الصفحة الرئيسية",
            Actions = new List<Screen_Actions>()
            {Screen_Actions.Show }
        };
        public static User_Screen_Access branches = new User_Screen_Access("elm_Branches")
        {
            Screen_Caption = "الأفرع",
            Actions = new List<Screen_Actions>() { Screen_Actions.Show }
        };
        public static User_Screen_Access add_Branches = new User_Screen_Access(nameof(Frm_Add_Store), branches)
        {
            Screen_Caption = "إضافة فرع/مخزن",
            Actions = new List<Screen_Actions>()
            {Screen_Actions.Show,
             Screen_Actions.Open,
            Screen_Actions.Add,
            Screen_Actions.Delete,
            Screen_Actions.Edit}
        };
        public static User_Screen_Access branches_List = new User_Screen_Access(nameof(Frm_Stores_List), branches)
        {
            Screen_Caption = "قائمة الأفرع /المخازن",
            Actions = new List<Screen_Actions>()
            {Screen_Actions.Show,
             Screen_Actions.Open,
            Screen_Actions.Add,
            Screen_Actions.Print}
        };
        public static User_Screen_Access products = new User_Screen_Access("elm_Product")
        {
            Screen_Caption = "الأصناف",
            Actions = new List<Screen_Actions>()
            {Screen_Actions.Show,}
        };
        public static User_Screen_Access Add_Product = new User_Screen_Access(nameof(Frm_Add_Product), products)
        {
            Screen_Caption = "إضافة صنف",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
            }
        };
        public static User_Screen_Access product_Cate_ = new User_Screen_Access(nameof(Frm_Categories), products)
        {
            Screen_Caption = "مجموعات الأصناف",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
                Screen_Actions.Print
            }
        };
        public static User_Screen_Access product_List = new User_Screen_Access(nameof(Frm_Products_List), products)
        {
            Screen_Caption = "قائمة الأصناف",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Print
            }
        };
        public static User_Screen_Access customer = new User_Screen_Access("elm_Customer")
        {
            Screen_Caption = "العملاء",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
            }
        };
        public static User_Screen_Access add_Customer = new User_Screen_Access(nameof(Customer), customer)
        {
            Screen_Caption = "إضافة عميل",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
                Screen_Actions.Print
            }
        };
        public static User_Screen_Access customers_List = new User_Screen_Access(nameof(Customer_List), customer)
        {
            Screen_Caption = "قائمة العملاء",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Print
            }
        };
        public static User_Screen_Access supplier = new User_Screen_Access("elm_Supplier")
        {
            Screen_Caption = " الموردين",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
            }
        };
        public static User_Screen_Access add_Supplier = new User_Screen_Access(nameof(Supplier), supplier)
        {
            Screen_Caption = "إضافة مورد",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
            }
        };
        public static User_Screen_Access supplier_List = new User_Screen_Access(nameof(Supplier_List), supplier)
        {
            Screen_Caption = "قائمة الموردين",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Print
            }
        };
        public static User_Screen_Access drawers = new User_Screen_Access("elm_Drawer")
        {
            Screen_Caption = " الخزائن",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
            }
        };
        public static User_Screen_Access add_Drawer = new User_Screen_Access(nameof(Frm_Add_Drawer), drawers)
        {
            Screen_Caption = "إضافة خزينة",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
            }
        };
        public static User_Screen_Access drawer_List = new User_Screen_Access(nameof(Frm_Drawer_List), drawers)
        {
            Screen_Caption = "قائمة الخزائن",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Print
            }
        };
        public static User_Screen_Access Reports = new User_Screen_Access("elm_Reports")
        {
            Screen_Caption = "التقارير",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
            }
        };
        public static User_Screen_Access bills = new User_Screen_Access("elm_Bill")
        {
            Screen_Caption = " الفواتير",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
            }
        };
        public static User_Screen_Access buy_bills = new User_Screen_Access("elm_Buy_Bill", Reports)
        {
            Screen_Caption = " فواتيرالمشتريات",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
            }
        };
        public static User_Screen_Access add_Buy_Bill = new User_Screen_Access(nameof(Frm_Buy_Bill), bills)
        {
            Screen_Caption = "إضافة فاتورة شراء",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
                Screen_Actions.Print
            }
        };
        public static User_Screen_Access Buy_Bill_List = new User_Screen_Access(nameof(Frm_Buy_Bill_List), buy_bills)
        {
            Screen_Caption = "قائمة فواتير المشتريات",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
                Screen_Actions.Print
            }
        };
        public static User_Screen_Access add_Buy_Return_Bill = new User_Screen_Access(nameof(Frm_Buy_Bill_Return), bills)
        {
            Screen_Caption = "إضافة فاتورة مرتجع شراء",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
                Screen_Actions.Print
            }
        };
        public static User_Screen_Access Buy_Return_Bill_List = new User_Screen_Access(nameof(Frm_Buy_Bill_Return_List), buy_bills)
        {
            Screen_Caption = "قائمة فواتير مرتجع الشراء",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
                Screen_Actions.Print
            }
        };
        public static User_Screen_Access sale_bills = new User_Screen_Access("elm_Sale_Bill", Reports)
        {
            Screen_Caption = " فواتير المبيعات",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
            }
        };
        public static User_Screen_Access add_Sale_Bill = new User_Screen_Access(nameof(Frm_Sale_Bill), bills)
        {
            Screen_Caption = "إضافة فاتورة بيع",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
                Screen_Actions.Print
            }
        };
        public static User_Screen_Access Sale_Bill_List = new User_Screen_Access(nameof(Frm_Sale_Bill_List), sale_bills)
        {
            Screen_Caption = "قائمة فواتير المبيعات",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
                Screen_Actions.Print
            }
        };
        public static User_Screen_Access add_Sale_Return_Bill = new User_Screen_Access(nameof(Frm_Sale_Bill_Return), bills)
        {
            Screen_Caption = "إضافة فاتورة مرتجع بيع",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
                Screen_Actions.Print
            }
        };
        public static User_Screen_Access Sale_Return_Bill_List = new User_Screen_Access(nameof(Frm_Sale_Bill_Return_List), sale_bills)
        {
            Screen_Caption = "قائمة فواتير مرتجع البيع",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
                Screen_Actions.Print
            }
        };
        public static User_Screen_Access money = new User_Screen_Access("elm_Money")
        {
            Screen_Caption = "المالية",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
            }
        };
        public static User_Screen_Access cash_Note_In = new User_Screen_Access(nameof(Frm_Cash_Note_In), money)
        {
            Screen_Caption = "إضافة سند قبض نقدي",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
                Screen_Actions.Print
            }
        };
        public static User_Screen_Access cash_Note_Out = new User_Screen_Access(nameof(Frm_Cash_Note_Out), money)
        {
            Screen_Caption = "إضافة سند صرف نقدي",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
                Screen_Actions.Print
            }
        };
        public static User_Screen_Access users = new User_Screen_Access("elm_User")
        {
            Screen_Caption = "المستخدمين",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
            }
        };
        public static User_Screen_Access add_User = new User_Screen_Access(nameof(Frm_Add_User), users)
        {
            Screen_Caption = "إضافة مستخدم",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
            }
        };
        public static User_Screen_Access users_List = new User_Screen_Access(nameof(Frm_User_List), users)
        {
            Screen_Caption = " قائمة المستخدمين",
            Actions = new List<Screen_Actions>()
            {
                 Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
            }
        };
        public static User_Screen_Access settings = new User_Screen_Access("elm_Settings")
        {
            Screen_Caption = "الإعدادات",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
            }
        };
        public static User_Screen_Access company_Info = new User_Screen_Access(nameof(Frm_Company_Info), settings)
        {
            Screen_Caption = "بيانات الشركة",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Edit,
            }
        };
        public static User_Screen_Access user_Settings = new User_Screen_Access("elm_User_Settings",settings)
        {
            Screen_Caption = "الإعدادات",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
            }
        };
        public static User_Screen_Access user_Setting_Prope_Profile = new User_Screen_Access(nameof(Frm_Add_User_Setting_Prope), user_Settings)
        {
            Screen_Caption = "إضافة نموذج صلاحيات",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
            }
        };
        public static User_Screen_Access user_Screen_Access = new User_Screen_Access(nameof(Frm_Add_User_Screen_Access), user_Settings)
        {
            Screen_Caption = "إضافة نموذج وصول",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
            }
        };
        public static User_Screen_Access user_Setting_Prope_Profile_List = new User_Screen_Access(nameof(Frm_User_Profile_Prope_List), user_Settings)
        {
            Screen_Caption = "قائمة نماذج الصلاحيات",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
            }
        };
        public static User_Screen_Access user_Screen_Access_List = new User_Screen_Access(nameof(Frm_Screen_Access_List), user_Settings)
        {
            Screen_Caption = "قائمة نماذج الوصول",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
            }
        };
        public static User_Screen_Access Accounts_Tree = new User_Screen_Access(nameof(Frm_Accounts_Tree), money)
        {
            Screen_Caption = "دليل الحسابات",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
            }
        };
        public static User_Screen_Access Store_Open_Balance = new User_Screen_Access(nameof(Frm_Store_Opening_Balance), branches)
        {
            Screen_Caption = "الأرصدة الإفتتاحية للمخازن",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
                Screen_Actions.Print,
            }
        };
        public static User_Screen_Access Store_Destruct_Balance = new User_Screen_Access(nameof(Frm_Store_Destructor_Balance), branches)
        {
            Screen_Caption = " سند إهلاك للأصناف ",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
                Screen_Actions.Print,
            }
        };
        public static User_Screen_Access Transfer_Balance_Between_Stores = new User_Screen_Access(nameof(Frm_Transfer_Balance_Between_Stores), branches)
        {
            Screen_Caption = " تحويل رصيد بين المخازن ",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
                Screen_Actions.Print,
            }
        };
        public static User_Screen_Access Customers_Account_List = new User_Screen_Access(nameof(Customers_Accounts_List), customer)
        {
            Screen_Caption = " كشف حسابات العملاء ",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Print,
            }
        };
        public static User_Screen_Access Suppiers_Account_List = new User_Screen_Access(nameof(Suppliers_Accounts_List), supplier)
        {
            Screen_Caption = " كشف حسابات الموردين ",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Print,
            }
        };
        public static User_Screen_Access Drawers_Account_List = new User_Screen_Access(nameof(Drawers_Accounts_List), drawers)
        {
            Screen_Caption = " كشف حسابات الخزائن ",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Print,
            }
        };
        public static User_Screen_Access Cash_Note_In_List = new User_Screen_Access(nameof(Frm_Cash_Note_In_List), money)
        {
            Screen_Caption = " كشف سندات القبض ",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Print,
            }
        };
        public static User_Screen_Access Cash_Note_Out_List = new User_Screen_Access(nameof(Frm_Cash_Note_Out_List), money)
        {
            Screen_Caption = " كشف سندات الدفع ",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Print,
            }
        };
        public static User_Screen_Access Exchange_Open_Balance = new User_Screen_Access(nameof(Frm_Exchange_Balances), drawers)
        {
            Screen_Caption = " رصيد إفتتاحي / تحويل أرصدة ",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
                Screen_Actions.Print,
            }
        };
        public static User_Screen_Access Add_Rigister_Manually = new User_Screen_Access(nameof(Frm_Manually_Rigister), money)
        {
            Screen_Caption = " إضافة قيد يدوي ",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Edit,
                Screen_Actions.Print,
            }
        };
        public static User_Screen_Access Product_Movement = new User_Screen_Access(nameof(Frm_Products_Balances_List), products)
        {
            Screen_Caption = " حركة الأصناف ",
            Actions = new List<Screen_Actions>()
            {
                Screen_Actions.Show,
                Screen_Actions.Open,
                Screen_Actions.Add,
                Screen_Actions.Delete,
                Screen_Actions.Print,
            }
        };
        public static List<User_Screen_Access> Get_Screens
        {
            get
            {
                Type type = typeof(Screens);
                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
                var list = new List<User_Screen_Access>();
                foreach (var item in fields)
                {
                    var obj = item.GetValue(null);//becuase its static and staic doesn't need object
                    if (obj != null && obj.GetType() == typeof(User_Screen_Access))
                    {
                        list.Add((User_Screen_Access)obj);
                    }
                }
                return list;
            }
        }
    }
}
