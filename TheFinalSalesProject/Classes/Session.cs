using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.Classes
{
    public static class Session
    {
        public static class Printer_Settings
        {
            public static Print_Mode PrintMode 
            {
                get =>
                    (Properties.Settings.Default.PrintMode == Convert.ToByte(Print_Mode.Directly))
                    ? Print_Mode.Directly :
                    (Properties.Settings.Default.PrintMode == Convert.ToByte(Print_Mode.ShowDialog))
                    ? Print_Mode.ShowDialog :
                    (Properties.Settings.Default.PrintMode == Convert.ToByte(Print_Mode.ShowPreview))
                    ? Print_Mode.ShowPreview : Print_Mode.ShowPreview; }
        }
        public static class Default
        {
            public static int DrawerID { get => 1; }
            /// <summary>
            /// هذا الفرع والمخزن في نفس الوقت
            /// </summary>
            public static int StoreID { get => 3; }
            public static int CustomerID { get => 1; }
            public static int SupplierID { get => 2; }
            public static int BranchID { get => 3; }
            public static int Discount_Allowed_Accoun_ID { get => Properties.Settings.Default.Discount_Allowed_Accoun_ID; }
            public static int Discount_Received_Account_ID { get => Properties.Settings.Default.Discount_Received_Account_ID; }
            public static int Sales_Tax_Account_ID { get => Properties.Settings.Default.Sales_Tax_Account_ID; }
            public static int Buy_Tax_Account_ID { get => Properties.Settings.Default.Buy_Tax_Account_ID; }
            public static int Buy_Expenese_Account_ID { get => Properties.Settings.Default.Buy_Expenese_Account_ID; }
        }
        public static class Barcode_Setting
        {//20-00009-00180-9
            public static bool Read_From_Scale_Barcode { get => Properties.Settings.Default.Read_From_Scale_Barcode; }
            public static string Scale_Barcode_PreFix { get => Properties.Settings.Default.Scale_Barcode_PreFix; }
            public static byte Product_Code_Length { get => Properties.Settings.Default.Product_Code_Length; }
            public static byte BarCode_Length { get => Properties.Settings.Default.BarCode_Length; }
            public static byte Value_Code_Length { get => Properties.Settings.Default.Value_Code_Length; }
            public static Read_Value_Mode Read_Mode 
            {
                get => 
                    (Properties.Settings.Default.Read_Mode == Convert.ToByte(Read_Value_Mode.Price)) 
                    ? Read_Value_Mode.Price 
                    : Read_Value_Mode.Weight; 
            }
            public static bool Ignore_Check_Digit { get => Properties.Settings.Default.Ignore_Check_Digit; }
            public static byte Divide_Value_By { get => Properties.Settings.Default.Divide_Value_By; }
            public enum Read_Value_Mode : byte
            {
                Price = 1,
                Weight,
            }
        }
        private static User_Settings_Propes current_User_Setting;
        public static User_Settings_Propes Current_User_Settings_Prope
        {
            get
            {
                if (current_User_Setting == null)
                {
                    //هنا حيث اليوزر الحالي جهزنا حقه الاعدادات بحيث رسلنا قيمة ال آيدي
                    current_User_Setting = new User_Settings_Propes(user.Setting_Profile_ID);
                }
                return current_User_Setting;
            }
        }

        private static DBModels.CompanyInfo companyInfo;
        public static DBModels.CompanyInfo Company_Info
        {
            get
            {
                if (companyInfo == null)
                {
                    companyInfo = DAL.Impelement_Stored_Procedure.SelectData<DBModels.CompanyInfo>("Select * From [FinalSalesDB].[dbo].[Company_Info]").FirstOrDefault();
                }
                return companyInfo;
            }
        }
        public static void Reset_Company()
        {
            companyInfo = null;
        }
        private static BindingList<DBModels.User_Profile_Property> role_Prope_Settings;
        public static BindingList<DBModels.User_Profile_Property> Role_Prope_Settings
        {
            get
            {
                if (role_Prope_Settings == null)
                {
                    role_Prope_Settings = new BindingList<DBModels.User_Profile_Property>(DAL.Impelement_Stored_Procedure.SelectData<DBModels.User_Profile_Property>("Select * From [FinalSalesDB].[dbo].[User_Profile_Property]"));
                }
                return role_Prope_Settings;
            }
        }

        private static BindingList<DBModels.Product> product;
        public static BindingList<DBModels.Product> products
        {
            get
            {
                if (product == null)
                {
                    product = new BindingList<DBModels.Product>(DAL.Impelement_Stored_Procedure.SelectData<DBModels.Product>(
                        "Select * From [FinalSalesDB].[dbo].[Product]"));
                    DataBaseWatchTower.products = new TableDependency.SqlClient.SqlTableDependency<DBModels.Product>(Properties.Settings.Default.ConnectionString);
                    DataBaseWatchTower.products.OnChanged += DataBaseWatchTower.Product_Changed;
                    DataBaseWatchTower.products.Start();
                }
                return product;
            }
        }

        private static BindingList<Table_View.Product_And_Category_And_Units_View> full_Product;
        public static BindingList<Table_View.Product_And_Category_And_Units_View> Full_products
        {
            get
            {
                if (full_Product == null)
                {
                    var data = DAL.Impelement_Stored_Procedure.Get_The_Full_Pro();
                    full_Product = new BindingList<Classes.Table_View.Product_And_Category_And_Units_View>(data.ToList());
                }
                return full_Product;
            }
        }

        private static BindingList<DBModels.Product_Unit> product_Unit;
        public static BindingList<DBModels.Product_Unit> Product_Units
        {
            get
            {
                if (product_Unit == null)
                {
                    product_Unit = new BindingList<DBModels.Product_Unit>(DAL.Impelement_Stored_Procedure.SelectData<DBModels.Product_Unit>("Select * From [FinalSalesDB].[dbo].[Product_Unit]"));
                    DataBaseWatchTower.product_Units = new TableDependency.SqlClient.SqlTableDependency<DBModels.Product_Unit>(Properties.Settings.Default.ConnectionString);
                    DataBaseWatchTower.product_Units.OnChanged += DataBaseWatchTower.Product_Units_Changed;
                    DataBaseWatchTower.product_Units.Start();
                }
                return product_Unit;
            }
        }

        private static BindingList<Product_Balance> pro_Balance;
        public static BindingList<Product_Balance> Pro_Balance
        {
            get
            {//هذا الاستعلام يرجع لي الاصناف المتاحة للبيع لكل صنف حسب رقم المخزن والمنتج
                if (pro_Balance == null)
                {
                    var data = from proMove in DAL.Impelement_Stored_Procedure.SelectData<DBModels.Pro_Store_Movement>("Select * From [FinalSalesDB].[dbo].[Pro_Store_Movement]")
                               group proMove by new { proMove.Product_ID, proMove.Store_ID }
                                   into gr
                               select new Product_Balance
                               {
                                   Balance =
                                   (gr.Sum(x => (double?)x.Import_Qty) ?? 0) -
                                   (gr.Sum(x => (double?)x.Export_Qty) ?? 0),
                                   Product_ID = gr.Key.Product_ID,
                                   Store_ID = gr.Key.Store_ID
                               };
                    pro_Balance = new BindingList<Product_Balance>(data.ToList());
                    DataBaseWatchTower.proMovement = new TableDependency.SqlClient.SqlTableDependency<DBModels.Pro_Store_Movement>(Properties.Settings.Default.ConnectionString);
                    DataBaseWatchTower.proMovement.OnChanged += DataBaseWatchTower.ProMovement_Changed;
                    DataBaseWatchTower.proMovement.Start();
                }
                return pro_Balance;
            }
        }

        private static BindingList<DBModels.Unit> unit;
        public static BindingList<DBModels.Unit> Units
        {
            get
            {
                if (unit == null)
                {
                    unit = new BindingList<DBModels.Unit>(DAL.Impelement_Stored_Procedure.SelectData<DBModels.Unit>("Select * From [FinalSalesDB].[dbo].[Unit]"));
                    DataBaseWatchTower.unit = new TableDependency.SqlClient.SqlTableDependency<DBModels.Unit>(Properties.Settings.Default.ConnectionString);
                    DataBaseWatchTower.unit.OnChanged += DataBaseWatchTower.Unit_Changed;
                    DataBaseWatchTower.unit.Start();
                }
                return unit;
            }
        }

        private static BindingList<DBModels.Customer_Supplier> customer;
        public static BindingList<DBModels.Customer_Supplier> Customers
        {
            get
            {
                if (customer == null)
                {
                    //p = new DynamicParameters();
                    //p.Add("@type1", Convert.ToByte(Enum_Choices.Part_Type.Customer));
                    //p.Add("@type2", Convert.ToByte(Enum_Choices.Part_Type.Cus_Supp));
                    customer = new BindingList<DBModels.Customer_Supplier>(
                        DAL.Impelement_Stored_Procedure.SelectData<DBModels.Customer_Supplier>
                        (@"Select * From dbo.Customer_Supplier 
                                Where Type In (@type1, @type2 )", new 
                        { 
                            type1 = Convert.ToByte(Enum_Choices.Part_Type.Customer), 
                            type2 = Convert.ToByte(Enum_Choices.Part_Type.Cus_Supp) 
                        }));
                    DataBaseWatchTower.customer = new TableDependency.SqlClient.SqlTableDependency
                        <DBModels.Customer_Supplier>(Properties.Settings.Default.ConnectionString,
                        filter: new DataBaseWatchTower.Customer_Only());
                    DataBaseWatchTower.customer.OnChanged += DataBaseWatchTower.Customer_Changed;
                    DataBaseWatchTower.customer.Start();
                }
                return customer;
            }
        }
        private static BindingList<DBModels.Customer_Supplier> supplier;
        public static BindingList<DBModels.Customer_Supplier> Suppliers
        {
            get
            {
                if (supplier == null)
                {
                    //p = new DynamicParameters();
                    //p.Add("@type1", Convert.ToByte(Enum_Choices.Part_Type.Supplier));
                    //p.Add("@type2", Convert.ToByte(Enum_Choices.Part_Type.Cus_Supp));
                    supplier = new BindingList<DBModels.Customer_Supplier>(
                        DAL.Impelement_Stored_Procedure.SelectData<DBModels.Customer_Supplier>(
                            @"Select * From dbo.Customer_Supplier 
                                Where Type In (@type1, @type2 )",new 
                            { 
                                type1 = Convert.ToByte(Enum_Choices.Part_Type.Supplier), 
                                type2 = Convert.ToByte(Enum_Choices.Part_Type.Cus_Supp) 
                            }));
                    DataBaseWatchTower.supplier = new TableDependency.SqlClient.SqlTableDependency
                        <DBModels.Customer_Supplier>(Properties.Settings.Default.ConnectionString,
                        filter: new DataBaseWatchTower.Suppliers_Only());
                    DataBaseWatchTower.supplier.OnChanged += DataBaseWatchTower.Supplier_Changed;
                    DataBaseWatchTower.supplier.Start();
                }
                return supplier;
            }
        }

        private static BindingList<DBModels.Accounts> account;
        public static BindingList<DBModels.Accounts> Accounts
        {
            get
            {
                if (account == null)
                {
                    account = new BindingList<DBModels.Accounts>(DAL.Impelement_Stored_Procedure.SelectData<DBModels.Accounts>("Select * From dbo.Accounts"));
                    DataBaseWatchTower.account = new TableDependency.SqlClient.SqlTableDependency<DBModels.Accounts>(Properties.Settings.Default.ConnectionString);
                    DataBaseWatchTower.account.OnChanged += DataBaseWatchTower.Account_Changed;
                    DataBaseWatchTower.account.Start();
                }
                return account;
            }
        }

        private static BindingList<DBModels.Drawer> drawer;
        public static BindingList<DBModels.Drawer> Drawers
        {
            get
            {
                if (drawer == null)
                {
                    drawer = new BindingList<DBModels.Drawer>(DAL.Impelement_Stored_Procedure.SelectData<DBModels.Drawer>("Select * From dbo.Drawer"));
                    //DataBaseWatcher.products = new TableDependency.SqlClient.SqlTableDependency<DAL.Product>(Properties.Settings.Default.ConnectionString);
                    //DataBaseWatcher.products.OnChanged += DataBaseWatcher.Product_Changed;
                    //DataBaseWatcher.products.Start();
                }
                return drawer;
            }
        }

        private static BindingList<DBModels.Store> store;
        public static BindingList<DBModels.Store> Stores
        {
            get
            {
                if (store == null)
                {
                    store = new BindingList<DBModels.Store>(DAL.Impelement_Stored_Procedure.SelectData<DBModels.Store>(
                        @"Select * From [FinalSalesDB].[dbo].[Store]"));
                    DataBaseWatchTower.store = new TableDependency.SqlClient.SqlTableDependency<DBModels.Store>(Properties.Settings.Default.ConnectionString);
                    DataBaseWatchTower.store.OnChanged += DataBaseWatchTower.Store_Changed;
                    DataBaseWatchTower.store.Start();
                }
                return store;
            }
        }

        public static DBModels.User user;
        public static void Set_User(DBModels.User us)
        {
            user = us;
            List<DBModels.Screen_Roles_Detail> screenAccess = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Screen_Roles_Detail>("Select * From [dbo].[Screen_Roles_Detail]");

            screen_Access_Role = (from s in Screens.Get_Screens
                                  from db in screenAccess
                           .Where(x => x.Screen_Role_ID == user.Setting_Screen_ID
                            && x.Screen_ID == s.Screen_ID).DefaultIfEmpty()
                                  select new User_Screen_Access(s.Screen_Name)
                                  {
                                      Actions = s.Actions,
                                      Can_Add = (db == null) ? false : db.Can_Add,
                                      Can_Delete = (db == null) ? false : db.Can_Delete,
                                      Can_Edit = (db == null) ? false : db.Can_Edit,
                                      Can_Open = (db == null) ? false : db.Can_Open,
                                      Can_Print = (db == null) ? false : db.Can_Print,
                                      Can_Show = (db == null) ? false : db.Can_Show,
                                      Screen_Name = s.Screen_Name,
                                      Screen_Caption = s.Screen_Caption,
                                      Screen_ID = s.Screen_ID,
                                      Parent_Screen_ID = s.Parent_Screen_ID
                                  }).ToList();
        }

        private static List<Classes.User_Screen_Access> screen_Access_Role;
        public static List<Classes.User_Screen_Access> Screen_Access_Role
        {
            get
            {
                return screen_Access_Role;
            }
        }
    }
}