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
    public partial class Frm_Add_Customer_Supplier : Frm_Master
    {
        DBModels.Customer_Supplier customer_Supplier;
        private bool closAfterSave;
        private bool is_Customer;
        private string msg = "رصيد إفتتاحي ";
        private DBModels.Coupes_Of_Account coupes = new DBModels.Coupes_Of_Account();
        public static int Add_New_Person(bool is_Customer)
        {
            using (Frm_Add_Customer_Supplier cusSup = new Frm_Add_Customer_Supplier(is_Customer))
            {
                cusSup.closAfterSave = true;
                cusSup.ShowDialog();
                return cusSup.customer_Supplier.ID;
            }
        }
        public Frm_Add_Customer_Supplier(bool is_Customer)
        {
            InitializeComponent();
            this.is_Customer = is_Customer;
            SetUp_Screen_Name();
            Refresh_Data();
            New();
        }
        public Frm_Add_Customer_Supplier(bool is_Customer, int person_Id)
        {
            InitializeComponent();
            this.is_Customer = is_Customer;
            Refresh_Data();
            SetUp_Screen_Name(true);
            Load_Person(person_Id);
        }
        private void Load_Person(int pid)
        {
            customer_Supplier = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Customer_Supplier>(
                @"Select * From Customer_Supplier Where ID = @id", 
                new 
                {
                    id = pid 
                }).SingleOrDefault();
            if (customer_Supplier.Opening_Balance)
            {
                coupes = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Coupes_Of_Account>(@"Select * From Coupes_Of_Account 
                          Where Notes = @note And Source_ID = 0 And Move_Name = @move
                          And Account_ID != @accID",
                          new
                          {
                              note = $"{msg}{customer_Supplier.Name}",
                              move = (byte)Enum_Choices.Account_Move_Name.Opening_Balance,
                              accID = customer_Supplier.Account_ID
                          }).SingleOrDefault();
                if (coupes != null)
                {
                    DrawerLkp.EditValue = Session.Drawers.SingleOrDefault(x => x.Account_ID == coupes.Account_ID).ID;
                    AmountSpn.EditValue = (coupes.Credit == 0) ? coupes.Debit : coupes.Credit;
                    CreditRdoBtn.Checked = !(coupes.Debit == 0);
                }
                else
                    AddOpeningBalaChk.Checked = false;
            }
            Get_Data();
        }
        private void SetUp_Screen_Name(bool edit = false)
        {
            this.Name = (is_Customer) ? Screens.add_Customer.Screen_Name : Screens.add_Supplier.Screen_Name;
            this.Text = (is_Customer) ? (!edit) ? " إضافة عميل " : " تعديل عميل " : (!edit) ? " إضافة مورد " : " تعديل مورد ";
            msg += (is_Customer) ? " لعميل " : " لمورد ";
            CreditRdoBtn.Checked = !is_Customer;
        }
        public void Frm_Add_Customer_Supplier_Load(object sender, EventArgs e)
        {
            //DrawerLkp.Look_Up_Edit_Translate_Column("Drawer");
        }
        protected override void New()
        {
            customer_Supplier = new DBModels.Customer_Supplier();
            customer_Supplier.Max_Credit = 10000;
            AmountSpn.EditValue = 0;
            base.New();
        }
        protected override void Get_Data()
        {
            PersonNametxt.Text = customer_Supplier.Name;
            PersonPhonetxt.Text = customer_Supplier.Phone;
            PersonMobiletxt.Text = customer_Supplier.Mobile;
            PersonAddresstxt.Text = customer_Supplier.Address;
            MaxCreditSpn.EditValue = customer_Supplier.Max_Credit;
            PersonAccountNumbertxt.Text = customer_Supplier.Account_ID.ToString();
            if (coupes != null)
                AddOpeningBalaChk.Checked = customer_Supplier.Opening_Balance;
            if (AmountSpn.EditValue == null || Convert.ToDouble(AmountSpn.EditValue) == 0)
                AmountSpn.EditValue = 0;
            the_Changed_Source_ID = customer_Supplier.ID;
            the_Changed_Source_Name = customer_Supplier.Name;
            base.Get_Data();
        }
        protected override void Set_Data()
        {
            customer_Supplier.Name = PersonNametxt.Text.Trim();
            customer_Supplier.Phone = PersonPhonetxt.Text.Trim();
            customer_Supplier.Mobile = PersonMobiletxt.Text.Trim();
            customer_Supplier.Address = PersonAddresstxt.Text.Trim();
            customer_Supplier.Type = Convert.ToByte((is_Customer ? Enum_Choices.Part_Type.Customer : Enum_Choices.Part_Type.Supplier));
            customer_Supplier.Max_Credit = Convert.ToDouble(MaxCreditSpn.EditValue);
            customer_Supplier.Opening_Balance = AddOpeningBalaChk.Checked;
            base.Set_Data();
        }
        protected override bool IsDataValid()
        {
            int numError = 0;
            numError += PersonNametxt.Is_The_Text_Valid() ? 0 : 1;
            numError += MaxCreditSpn.Is_The_Edit_Value_More_Than_Zero() ? 0 : 1;
            if (DAL.Impelement_Stored_Procedure.SelectData<DBModels.Customer_Supplier>(
                @"Select * From Customer_Supplier 
                           Where Name = @name And Type = @type And ID != @id",
                new 
                {
                    name = PersonNametxt.Text.Trim(),
                    type = Convert.ToByte((is_Customer ? Enum_Choices.Part_Type.Customer : Enum_Choices.Part_Type.Supplier)),
                    id = customer_Supplier.ID }).Count > 0)
            {
                numError++;
                PersonNametxt.ErrorText = Messages.Name_Exist;
            }
            if (AddOpeningBalaChk.Checked)
            {
                numError += AmountSpn.Is_The_Edit_Value_More_Than_Zero() ? 0 : 1;
                numError += DrawerLkp.Is_The_Lkp_Text_Valid() ? 0 : 1;
            }
            return (numError == 0);
            /*
             هذا الدالة تقوم بالتحقق من الشروط الموضحة أعلاه
             الشرط الكبير يقوم باسترجاع البيانات بحيث الإسم لايساوي أسم موجود مسبقا
             ونوع المزود او العميل لازم يكون متشابة بمعنى يمكن يكون عميل ومورد يمتلكون نفس الإسم
             وآخر شرط عملناه عشان التعديل بحيث ما يجيش خطأ عندما يعدل على البيانات الموجودة مسبقا 
             */
        }
        protected override void Refresh_Data()
        {
            DrawerLkp.Initilaze_Data(Session.Drawers);
            DrawerLkp.EditValue = Session.Drawers.FirstOrDefault().ID;
            base.Refresh_Data();
        }
        protected override void Save()
        {
            string oldPersonName = customer_Supplier.Name;
            if (customer_Supplier.ID == 0)
            {
                customer_Supplier.Account_ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>(
                    @"Insert Into Accounts Values 
                                 (@name ,@code,@parent)", 
                    new 
                    {
                        name = customer_Supplier.Name, 
                        code = "10", 
                        parent = 0 
                    }).FirstOrDefault().ID;//todo comehere
                customer_Supplier.ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>(
                    @"Insert Into Customer_Supplier Values 
                                (@name ,@phone,@mobile,@address,@account_ID,@type,@max,@open)",
                new
                {
                    name = customer_Supplier.Name,
                    phone = customer_Supplier.Phone,
                    mobile = customer_Supplier.Mobile,
                    address = customer_Supplier.Address,
                    account_ID = customer_Supplier.Account_ID,
                    type = customer_Supplier.Type,
                    max = customer_Supplier.Max_Credit,
                    open = customer_Supplier.Opening_Balance
                }).FirstOrDefault().ID;
            }
            else
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce(@"Update Customer_Supplier Set 
                Name = @name ,Phone = @phone,Mobile = @mobile,Address = @address,
                Account_ID = @account_ID,Type = @type,Max_Credit = @max,Opening_Balance = @open
                Where ID = @id",
                new
                {
                    name = customer_Supplier.Name,
                    phone = customer_Supplier.Phone,
                    mobile = customer_Supplier.Mobile,
                    address = customer_Supplier.Address,
                    account_ID = customer_Supplier.Account_ID,
                    type = customer_Supplier.Type,
                    max = customer_Supplier.Max_Credit,
                    open = customer_Supplier.Opening_Balance,
                    id = customer_Supplier.ID
                });
                DAL.Impelement_Stored_Procedure.Excute_Proce(@"Update Accounts Set 
                Name = @name ,Code = @code,Parent_ID = @parent
                Where ID = @id", 
                new
                {
                    name = customer_Supplier.Name,
                    code = "10", parent = 0,
                    id = customer_Supplier.Account_ID
                });//todo comehere
            }

            //هنا اول شي يحذف الحساب 
            DAL.Impelement_Stored_Procedure.Excute_Proce(@"Delete From Coupes_Of_Account 
                Where  Move_Name = @movename 
                And Source_Type = 0 And Source_ID = 0 And Notes = @note",
               new
               {
                   movename = (byte)Enum_Choices.Account_Move_Name.Opening_Balance,
                   note = msg + oldPersonName
               });

            if (AddOpeningBalaChk.Checked)
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce(@"[dbo].[Add_List_Of_Coupes]",
                    new
                    {
                        coupes = Set_Openig_Accounts().AsTableValuedParameter("Account_Coupes_List_Type"),
                    },
                    isCommandText: false);
            }
            the_Changed_Source_ID = customer_Supplier.ID;
            the_Changed_Source_Name = customer_Supplier.Name;
            base.Save();
            if (closAfterSave)
                this.Close();
            /*
            الدلة تقوم بالجفظ بحيث إذا كان العميل أو المورد جديد يقوم بعمل نسخة جديدة من
            الداتا كونتكست وعمل حساب جديد للمورد أو العميل
            وإذا كان قديم أي نشتي نعدل عليه نقوم بربط الداتا كونتكست مع البيانات الحالية 
            بحيث نعدل عليها اذا كان اسم أو غير ذلك والحساب بنعمل جملة إسترجاع بحيث يكون 
             رقم الحساب يساوي الرقم الموجود في جدول الوزعين أو الموردين
             */
        }
        private DataTable Set_Openig_Accounts()
        {
            using (DataTable table =new DataTable())
            {
                List<DBModels.Coupes_Of_Account> coupes = new List<DBModels.Coupes_Of_Account>();
                var drawerID = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Drawer>(
                    @"Select * From Drawer 
                               Where ID = @id", 
                    new 
                    {
                        id = Convert.ToInt32(DrawerLkp.EditValue) 
                    }).SingleOrDefault();
                coupes.Add(new DBModels.Coupes_Of_Account()
                {
                    Code = "9",//temp
                    Account_ID = customer_Supplier.Account_ID,
                    Debit = (DebitRdoBtn.Checked ? Convert.ToDouble(AmountSpn.EditValue) : 0),
                    Credit = (!DebitRdoBtn.Checked ? Convert.ToDouble(AmountSpn.EditValue) : 0),
                    Insert_Date = DateTime.Now,
                    Source_ID = 0,
                    Source_Type = 0,
                    Notes = msg + customer_Supplier.Name,
                    Move_Name = (byte)Enum_Choices.Account_Move_Name.Opening_Balance,
                    User_ID = Session.user.ID
                });
                coupes.Add(new DBModels.Coupes_Of_Account()
                {
                    Code = "9",//temp
                    Account_ID = drawerID.Account_ID,
                    Debit = (!DebitRdoBtn.Checked ? Convert.ToDouble(AmountSpn.EditValue) : 0),
                    Credit = (DebitRdoBtn.Checked ? Convert.ToDouble(AmountSpn.EditValue) : 0),
                    Insert_Date = DateTime.Now,
                    Source_ID = 0,
                    Source_Type = 0,
                    Notes = msg + customer_Supplier.Name ,
                    Move_Name = (byte)Enum_Choices.Account_Move_Name.Opening_Balance,
                    User_ID = Session.user.ID
                });

                table.Columns.Add("Code",typeof(string));
                table.Columns.Add("Account_ID",typeof(int));
                table.Columns.Add("Debit", typeof(double));
                table.Columns.Add("Credit", typeof(double));
                table.Columns.Add("Insert_Date", typeof(DateTime));
                table.Columns.Add("Source_Type", typeof(byte));
                table.Columns.Add("Source_ID", typeof(int));
                table.Columns.Add("Note", typeof(string));
                table.Columns.Add("Move_Name", typeof(byte));
                table.Columns.Add("User_ID", typeof(int));
                coupes.ForEach(x =>
                {
                    table.Rows.Add(x.Code, x.Account_ID, x.Debit, x.Credit, x.Insert_Date,
                        x.Source_Type, x.Source_ID, x.Notes, x.Move_Name, x.User_ID);
                });
                return table;
            }
        }
        private void AddOpeningBalaChk_CheckedChanged(object sender, EventArgs e)
        {
            if (AddOpeningBalaChk.Checked)
                OpeningBalanceLYCG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            else
                OpeningBalanceLYCG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        protected override void Delete()
        {
            if (DAL.Impelement_Stored_Procedure.SelectData<DBModels.Invoice>(
                @"Select * From Invoice 
                           Where Person_ID = @pID", 
                new 
                {
                    pID = customer_Supplier.ID 
                }).Count > 0)
            {
                XtraMessageBox.Show("لا يمكن الحذف لأن هناك سندات متصلة بالمورد /العميل", "فشل الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
            }
            DAL.Impelement_Stored_Procedure.Excute_Proce(@"Delete From Customer_Supplier 
                Where ID = @id",
              new
              {
                  id = customer_Supplier.ID
              });
            DAL.Impelement_Stored_Procedure.Excute_Proce(@"Delete From Accounts 
                Where ID = @id",
               new
               {
                   id = customer_Supplier.Account_ID
               });

            DAL.Impelement_Stored_Procedure.Excute_Proce(@"Delete From Coupes_Of_Account 
                Where Account_ID In(@perAccID , @drawAccID) And Move_Name = @movename 
                And Source_Type = 0 And Source_ID = 0 And Notes = @note",
               new
               {
                   perAccID = customer_Supplier.Account_ID,
                   drawAccID = coupes.Account_ID,
                   movename = (byte)Enum_Choices.Account_Move_Name.Opening_Balance,
                   note = msg + customer_Supplier.Name
               });
            
            base.Delete();
            New();
        }
    }
}
