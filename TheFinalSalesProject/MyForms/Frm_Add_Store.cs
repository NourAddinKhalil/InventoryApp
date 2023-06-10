using Dapper;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheFinalSalesProject.Classes;

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Add_Store : Frm_Master
    {
        private DBModels.Store store;
        private string Cost_Of_Sold;
        private string Inventory;
        private string Sales;
        private string Sales_Return;
        public Frm_Add_Store()
        {
            InitializeComponent();
            New();
        }
        public Frm_Add_Store(int store_ID)
        {
            InitializeComponent();
            store = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Store>("Select * From Store Where ID = @id", new { id = store_ID }).SingleOrDefault();
            //lambda expression to retrieve the row where id =storeid the we want to edit 
            Get_Data();
            isNew = false;
            //now the store object contain the element we want to edit
        }
        protected override bool IsDataValid()
        {
            int numError = 0;
            numError += StoreNametxt.Is_The_Text_Valid() ? 0 : 1;
            if (Session.Stores.Where(x => x.Name == StoreNametxt.Text.Trim() && x.ID != store.ID).Count() > 0)
            {
                StoreNametxt.ErrorText = Messages.Name_Exist;
                numError++;
            }
            return (numError == 0);
        }
        protected override void Save()
        {
            string old_storeName = store.Name;
            var Add_Account = new
            {
                Acc_List = Table().AsTableValuedParameter("Account_List_Type")
            };
            if (store.ID == 0)
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce("Add_List_Of_Accounts", Add_Account, isCommandText: false);
                //عنبحث على اسماء الحسابات عشان نضيفهن في حسابات المخزن
                List<DBModels.Accounts> acc = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Accounts>(
                    @"Select * From Accounts 
                               Where Name IN (Select Name From @Acc_List)", Add_Account);
                store.Cost_Of_Sold_Good_Account_ID = acc.Single(x => x.Name == Cost_Of_Sold).ID;
                store.Inventory_Account_ID = acc.Single(x => x.Name == Inventory).ID;
                store.Sales_Account_ID = acc.Single(x => x.Name == Sales).ID;
                store.Sales_Return_Account_ID = acc.Single(x => x.Name == Sales_Return).ID;
                //these accounts are common for all stores and we just need to create them once 
                store.Discount_Received_Account_ID = Classes.Session.Default.Discount_Received_Account_ID;
                store.Discount_Allowed_Account_ID = Classes.Session.Default.Discount_Allowed_Accoun_ID;
                store.ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>(
                    @"Insert Into Store Values
                            (@name,@saleAcc,@SaleReAcc,@invAcc,@costAcc,@disReAcc,@disAllAcc)", new
                    {
                        name = store.Name,
                        saleAcc = store.Sales_Account_ID,
                        SaleReAcc = store.Sales_Return_Account_ID,
                        invAcc = store.Inventory_Account_ID,
                        costAcc = store.Cost_Of_Sold_Good_Account_ID,
                        disReAcc = store.Discount_Received_Account_ID,
                        disAllAcc = store.Discount_Allowed_Account_ID
                    }).FirstOrDefault().ID;
            }
            else//update
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce(
                    @"Update Store Set Name = @name Where ID = @id", 
                    new 
                    {
                        name = store.Name, 
                        id = store.ID 
                    });
                DAL.Impelement_Stored_Procedure.Excute_Proce(@"Update_List_Of_Accounts", Add_Account, isCommandText: false);
            }
            the_Changed_Source_ID = store.ID;
            the_Changed_Source_Name = store.Name;
            base.Save();
        }
        protected override void Get_Data()
        {
            StoreNametxt.Text = store.Name;
            the_Changed_Source_ID = store.ID;
            the_Changed_Source_Name = store.Name;
        }
        protected override void Set_Data()
        {
            store.Name = StoreNametxt.Text;
            Cost_Of_Sold = store.Name + " تكلفة البضاعة المباعة ";
            Inventory = store.Name + " المخزون ";
            Sales = store.Name + " مبيعات ";
            Sales_Return = store.Name + " مرتجع مبيعات ";
        }
        protected override void Open_New_Window()
        {
            Frm_Main_Window.OpenForm(new Frm_Add_Store(), false);
        }
        protected override void New()
        {
            store = new DBModels.Store();
            base.New();
        }
        protected override void Delete()
        {
            using (DataTable table = new DataTable())
            {
                table.Columns.Add("Id", typeof(int));
                table.Rows.Add(store.Cost_Of_Sold_Good_Account_ID);
                table.Rows.Add(store.Inventory_Account_ID);
                table.Rows.Add(store.Sales_Return_Account_ID);
                table.Rows.Add(store.Sales_Account_ID);

                if (StoreNametxt.Text.Trim() == string.Empty)
                {
                    StoreNametxt.ErrorText = "Can't Delete Empty Item";
                    return;
                }
                if (XtraMessageBox.Show(" هل فعلاَ تريد الحذف؟ ", " تأكيد الحذف ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                int storeMovement = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Pro_Store_Movement>(
                    @"Select * From Pro_Store_Movement 
                               Where Store_ID = @id", 
                    new 
                    {
                        id = store.ID 
                    }).Count;
                int StorAccCoupes = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Coupes_Of_Account>(
                    @"Select * From Coupes_Of_Account Where
                               Account_ID = @costID OR Account_ID = @invID OR 
                               Account_ID = @saleID OR Account_ID = @salreID",
                                    new
                                    {
                                        costID = store.Cost_Of_Sold_Good_Account_ID,
                                        invID = store.Inventory_Account_ID,
                                        saleID = store.Sales_Account_ID,
                                        salreID = store.Sales_Return_Account_ID
                                    }).Count;
                if (storeMovement + StorAccCoupes > 0)
                {
                    //يعني ما عيحذف المخزن الا اذا مابش معه حركات ولا قد معه كشف حسابات
                    XtraMessageBox.Show(" عفواً لا يمكن حذف المخزن لأنه تم إستخدامه بالفعل ", " فشل الحذف ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //This connect with old store becouse we weant to delete exisit item
                DAL.Impelement_Stored_Procedure.Excute_Proce(@"Delete From Accounts 
                Where ID In (Select ID From @IDs)", 
                new 
                {
                    IDs = table.AsTableValuedParameter("IDs_List_Type") 
                });
                DAL.Impelement_Stored_Procedure.Excute_Proce(@"Delete From Store 
                Where ID = @id", 
                new 
                {
                    id = store.ID 
                });
                // XtraMessageBox.Show(" Deleted Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                New();
                base.Delete();
            }
        }
        private DataTable Table()
        {
            using (DataTable table = new DataTable()) 
            {
                table.Columns.Add("ID", typeof(int));
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Code", typeof(string));
                table.Columns.Add("Parent_ID", typeof(int));
                table.Rows.Add(store.Sales_Account_ID, Sales, "9", 0);
                table.Rows.Add(store.Sales_Return_Account_ID, Sales_Return, "9", 0);
                table.Rows.Add(store.Inventory_Account_ID, Inventory, "9", 0);
                table.Rows.Add(store.Cost_Of_Sold_Good_Account_ID, Cost_Of_Sold, "9", 0);
                return table;
            }
        }
    }
}
