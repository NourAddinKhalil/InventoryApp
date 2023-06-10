using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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
using static TheFinalSalesProject.Classes.Enum_Choices;
using static TheFinalSalesProject.Classes.Finance;
using static TheFinalSalesProject.Classes.Translation;
using static TheFinalSalesProject.Classes.Delete_Data;
using static TheFinalSalesProject.Classes.Convert_List_To_DataTable;

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Cashes_Note_Guaranteed : Frm_Master
    {
        private bool is_Cash_In;
        private DBModels.Guaranteed_Notes guaranteed_Note;
        private BindingList<DBModels.Invoice> invoiceList = new BindingList<DBModels.Invoice>();
        private RepositoryItemLookUpEdit repoStore = new RepositoryItemLookUpEdit();
        private RepositoryItemLookUpEdit repoDrawer = new RepositoryItemLookUpEdit();
        private RepositoryItemLookUpEdit repoInvoiceType = new RepositoryItemLookUpEdit();

        public Frm_Cashes_Note_Guaranteed(bool is_Cash_In)
        {
            InitializeComponent();
            this.is_Cash_In = is_Cash_In;
            Set_Form_Type();
            PartTypelokUpEdt.EditValueChanged += PartTypelokUpEdt_EditValueChanged;
            PartIDGridLokUpEdt.EditValueChanged += PartIDGridLokUpEdt_EditValueChanged;
            New();
            Refresh_Data();
        }
        public Frm_Cashes_Note_Guaranteed(bool is_Cash_In , int id)
        {
            InitializeComponent();
            this.is_Cash_In = is_Cash_In;
            Set_Form_Type();
            PartTypelokUpEdt.EditValueChanged += PartTypelokUpEdt_EditValueChanged;
            PartIDGridLokUpEdt.EditValueChanged += PartIDGridLokUpEdt_EditValueChanged;
            guaranteed_Note = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Guaranteed_Notes>(
                @"Select * From [FinalSalesDB].[dbo].[Guaranteed_Notes] 
                  Where [ID] = @id And Is_Cash_In = @isIn",
                new
                {
                    id = id,
                    isIn = is_Cash_In
                }).FirstOrDefault();
            Refresh_Data();
            Get_Data();
            isNew = false;
        }
        private void Set_Form_Type()
        {
            if (is_Cash_In)
            {
                this.Text = " سند قبض ";
                this.Name = Screens.cash_Note_In.Screen_Name;
            }
            else
            {
                this.Text = " سند دفع ";
                this.Name = Screens.cash_Note_Out.Screen_Name;
            }
        }
        public void Frm_Guaranteed_Notes_Load(object sender, EventArgs e)
        {
            PrintBtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            GuaraGridView.Add_DeleteColumns_To_GridView(GuaraGridCtrl);
            GuaraGridView.Add_NumberColumn_To_GridView(GuaraGridCtrl);
            Grid_View_Transulate();
            GuaraGridView.BestFitColumns();
            repoStore.Initilaze_Data(Session.Stores, GuaraGridCtrl, GuaraGridView.Columns["Store_ID"]);
            repoStore.PopulateColumns();
            repoStore.Repo_Look_Up_Edit_Translate_Column("Store");
            repoDrawer.Initilaze_Data(Session.Drawers, GuaraGridCtrl, GuaraGridView.Columns["Drawer_ID"]);
            repoDrawer.PopulateColumns();
            repoDrawer.Repo_Look_Up_Edit_Translate_Column("Drawer");
            repoInvoiceType.Initilaze_Data(Master_Class.invoices_Type_List, GuaraGridCtrl, GuaraGridView.Columns["Type"]);
            repoInvoiceType.PopulateColumns();
            repoInvoiceType.Repo_Look_Up_Edit_Translate_Column("Type");
            Read_User_Settings();
        }
        protected override void Refresh_Data()
        {
            PartTypelokUpEdt.Initilaze_Data(Master_Class.part_Type_List, clear: true);
            DrawerLokUpEdt.Initilaze_Data(Session.Drawers, clear: true);
            BranchlokUpEdt.Initilaze_Data(Session.Stores, clear: true);
            base.Refresh_Data();
        }
        private void PartTypelokUpEdt_EditValueChanged(object sender, EventArgs e)
        {
            if (PartTypelokUpEdt.Is_The_Lkp_Edit_Value_Of_Type_Int())
            {
                byte type = Convert.ToByte(PartTypelokUpEdt.EditValue);
                if (type == Convert.ToByte(Part_Type.Customer))
                {
                    PartIDGridLokUpEdt.Initilaze_Data(Session.Customers);
                    PartIDGridLokUpEdt.EditValue = Session.Default.CustomerID;
                }
                else if (type == Convert.ToByte(Part_Type.Supplier))
                {
                    PartIDGridLokUpEdt.Initilaze_Data(Session.Suppliers);
                    PartIDGridLokUpEdt.EditValue = Session.Default.SupplierID;
                }
                //else if (type == Convert.ToByte(Part_Type.Account))
                //{
                //    PartIDGridLokUpEdt.Initilaze_Data(Session.Accounts);
                //    PartIDGridLokUpEdt.EditValue = null;
                //}
            }
        }
        Account_Balance account_Balance;
        private void PartIDGridLokUpEdt_EditValueChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(PartIDGridLokUpEdt.EditValue);
            if (id > 0)
            {
                DBModels.Customer_Supplier person = null;
                if (PartTypelokUpEdt.EditValue.Equals(Convert.ToByte(Part_Type.Supplier)))
                {
                    person = Session.Suppliers.FirstOrDefault(x => x.ID == id);
                    PartIDGridLokUpEdt.Properties.View.Grid_View_Translate_Column("Person");
                }
                else if (PartTypelokUpEdt.EditValue.Equals(Convert.ToByte(Part_Type.Customer)))
                {
                    person = Session.Customers.FirstOrDefault(x => x.ID == id);
                    PartIDGridLokUpEdt.Properties.View.Grid_View_Translate_Column("Person");
                }//todo
                invoiceList = new BindingList<DBModels.Invoice>(
                    DAL.Impelement_Stored_Procedure.SelectData<DBModels.Invoice>(
                    @"Select * From [FinalSalesDB].[dbo].[Invoice] Where [Person_ID] = @pID
                               And [Person_Type] = @pType And Type In (@iType1,@itype2)
							    And ([Remaining] - 
								[FinalSalesDB].[dbo].[Get_The_Sum_Of_The_Already_Paid_Invoices](
								[FinalSalesDB].[dbo].[Invoice].[ID]) > 0)",
                    new
                    {
                        pID = person.ID,
                        pType = person.Type,
                        iType1 = Convert.ToByte(is_Cash_In ? bill_Type.Sale : bill_Type.Buy),
                        itype2 = Convert.ToByte(is_Cash_In ? bill_Type.SaleReturn : bill_Type.BuyReturn)
                    }));
                //else if (PartTypelokUpEdt.EditValue.Equals(Convert.ToByte(Part_Type.Account)))
                //{
                //    person = new DBModels.Customer_Supplier() { Account_ID = id };

                //}//todo
                if (person == null) goto EMPTYACC;
                PartIDLayCtrlGrp.Expanded = true;
                PartAddressTxt.Text = person.Address;
                PartPhoneTxt.Text = person.Mobile;
                MaxMoneySpn.EditValue = person.Max_Credit;
                account_Balance = Get_Account_Balance(person.Account_ID);
                CurrentBalanceTxt.Text = account_Balance.Balance_Note;
                return;
            }
            else
            {
                goto EMPTYACC;
            }
            EMPTYACC:
            PartAddressTxt.Text =
            PartPhoneTxt.Text =
            CurrentBalanceTxt.Text = "";
            MaxMoneySpn.EditValue = 0;
        }
        private string Get_New_Code()
        {
            string maxCode;
            var s = DAL.Impelement_Stored_Procedure.SelectData<DBModels.ID_Model>(
                @"Select [Code] From  [FinalSalesDB].[dbo].[Guaranteed_Notes] 
                  Where [Is_Cash_In] = @isIn Order By [Date] Desc ",
                new
                {
                    isIn = is_Cash_In
                })?.FirstOrDefault();
            if (s == null)
            {
                s = new DBModels.ID_Model();
                s.Code = "0";
            }
            maxCode = string.IsNullOrEmpty(s.Code) ? "0" : s.Code;
            return Master_Class.Get_Next_Number_InTheString(maxCode);
        }
        protected override void New()
        {
            guaranteed_Note = new DBModels.Guaranteed_Notes()
            {
                Code = Get_New_Code(),
                Is_Cash_In = this.is_Cash_In,
                Date = DateTime.Now,
                Drawer_ID = Session.Current_User_Settings_Prope.General_Settings.DefualtDrawer,
                Store_ID = Session.Current_User_Settings_Prope.General_Settings.DefualtStore,
                Part_Type = (is_Cash_In) ? Convert.ToByte(Enum_Choices.Part_Type.Customer) : 
                                           Convert.ToByte(Enum_Choices.Part_Type.Supplier)
            };
            base.New();
        }
        protected override void Get_Data()
        {
            GuaraIDtxt.Text = guaranteed_Note.ID.ToString();
            GuaraCodeTxt.Text = guaranteed_Note.Code;
            GuaraDateDateEdit.DateTime = guaranteed_Note.Date;
            DiscountValueSpnEdt.EditValue = guaranteed_Note.Discount_Val;
            PaidSpnEdt.EditValue = guaranteed_Note.Amount;
            BranchlokUpEdt.EditValue = guaranteed_Note.Store_ID;
            DrawerLokUpEdt.EditValue = guaranteed_Note.Drawer_ID;
            PartTypelokUpEdt.EditValue = guaranteed_Note.Part_Type;
            PartIDGridLokUpEdt.EditValue = guaranteed_Note.Part_ID;
            NotesMemEdt.Text = guaranteed_Note.Note;
            GuaraGridCtrl.DataSource =new BindingList<DBModels.Invoice>(
                DAL.Impelement_Stored_Procedure.SelectData<DBModels.Invoice>(
                @"SELECT [ID],[Type],[Code],[Date],[Store_ID],[Total],[Discount_Val],[Tax_Val],[Net]
                        ,[Paid],[Drawer_ID],[Remaining] FROM [FinalSalesDB].[dbo].[Invoice]
                         Where ID In (Select [Invoice_ID] From [FinalSalesDB].[dbo].[Guaranteed_Notes]
                         Where [ID] = @gID)",
                new
                {//todo fix this query
                    gID = guaranteed_Note.ID
                }));
            base.Get_Data();
        }
        private void Grid_View_Transulate()
        {
            GuaraGridView.Columns["ID"].Visible = false;
            GuaraGridView.Columns["Type"].Caption = "نوع الفاتورة";
            GuaraGridView.Columns["Code"].Caption = "الكود";
            GuaraGridView.Columns["Date"].Caption = "التاريخ";
            GuaraGridView.Columns["Store_ID"].Caption = "المخزن";
            GuaraGridView.Columns["Total"].Caption = "الإجمالي";
            GuaraGridView.Columns["Discount_Val"].Caption = "الخصم";
            GuaraGridView.Columns["Tax_Val"].Caption = "الضريبة";
            GuaraGridView.Columns["Net"].Caption = "الصافي";
            GuaraGridView.Columns["Paid"].Caption = "المدفوع";
            GuaraGridView.Columns["Drawer_ID"].Caption = "الخزينة";
            GuaraGridView.Columns["Remaining"].Caption = "الباقي";
            GuaraGridView.Columns["Person_Type"].Visible = false;
            GuaraGridView.Columns["Person_ID"].Visible = false;
            GuaraGridView.Columns["Delivary_Date"].Visible = false;
            GuaraGridView.Columns["Notes"].Visible = false;
            GuaraGridView.Columns["Is_Posted_To_Store"].Visible = false;
            GuaraGridView.Columns["Post_Date"].Visible = false;
            GuaraGridView.Columns["Discount_Perec"].Visible = false;
            GuaraGridView.Columns["Tax_Perce"].Visible = false;
            GuaraGridView.Columns["Expences"].Visible = false;
            GuaraGridView.Columns["Shipping_Address"].Visible = false;
            GuaraGridView.Columns["Invoice_Back_ID"].Visible = false;
            GuaraGridView.Columns["User_ID"].Visible = false;
            int indx = 0;
            //GuaraGridView.Columns["Delete"].VisibleIndex = 0;
            GuaraGridView.Columns["Number"].VisibleIndex = indx++;
            GuaraGridView.Columns["Type"].VisibleIndex = indx++;
            GuaraGridView.Columns["Code"].VisibleIndex = indx++;
            GuaraGridView.Columns["Date"].VisibleIndex = indx++;
            GuaraGridView.Columns["Store_ID"].VisibleIndex = indx++;
            GuaraGridView.Columns["Total"].VisibleIndex = indx++;
            GuaraGridView.Columns["Discount_Val"].VisibleIndex = indx++;
            GuaraGridView.Columns["Tax_Val"].VisibleIndex = indx++;
            GuaraGridView.Columns["Net"].VisibleIndex = indx++;
            GuaraGridView.Columns["Paid"].VisibleIndex = indx++;
            GuaraGridView.Columns["Drawer_ID"].VisibleIndex = indx++;
            GuaraGridView.Columns["Remaining"].VisibleIndex = indx++;
            GuaraGridView.Columns["Delete"].OptionsColumn.AllowFocus = true;
            GuaraGridView.Columns["Number"].OptionsColumn.AllowFocus = false; 
            GuaraGridView.Columns["Type"].OptionsColumn.AllowFocus = false;  
            GuaraGridView.Columns["Code"].OptionsColumn.AllowFocus = false;
            GuaraGridView.Columns["Date"].OptionsColumn.AllowFocus = false;
            GuaraGridView.Columns["Store_ID"].OptionsColumn.AllowFocus = false;
            GuaraGridView.Columns["Total"].OptionsColumn.AllowFocus = false;
            GuaraGridView.Columns["Discount_Val"].OptionsColumn.AllowFocus = false;
            GuaraGridView.Columns["Tax_Val"].OptionsColumn.AllowFocus = false;
            GuaraGridView.Columns["Net"].OptionsColumn.AllowFocus = false;
            GuaraGridView.Columns["Paid"].OptionsColumn.AllowFocus = false;
            GuaraGridView.Columns["Drawer_ID"].OptionsColumn.AllowFocus = false;
            GuaraGridView.Columns["Remaining"].OptionsColumn.AllowFocus = false;
        }
        protected override void Set_Data()
        {
            DBModels.Invoice invoice = (GuaraGridCtrl.DataSource as BindingList<DBModels.Invoice>).FirstOrDefault();
            guaranteed_Note.Amount = Convert.ToDouble(PaidSpnEdt.EditValue);
            guaranteed_Note.Code = GuaraCodeTxt.Text.Trim();
            guaranteed_Note.Discount_Val = Convert.ToDouble(DiscountValueSpnEdt.EditValue);
            guaranteed_Note.Drawer_ID = Convert.ToInt32(DrawerLokUpEdt.EditValue);
            guaranteed_Note.Store_ID = Convert.ToInt32(BranchlokUpEdt.EditValue);
            guaranteed_Note.Part_ID = Convert.ToInt32(PartIDGridLokUpEdt.EditValue);
            guaranteed_Note.Part_Type = Convert.ToByte(PartTypelokUpEdt.EditValue);
            guaranteed_Note.Note = NotesMemEdt.Text.Trim();
            guaranteed_Note.Is_Cash_In = is_Cash_In;
            guaranteed_Note.Date = GuaraDateDateEdit.DateTime;
            if (invoice != null)
            {
                guaranteed_Note.Invoice_ID = invoice.ID;
                guaranteed_Note.Invoice_Type = invoice.Type;
                guaranteed_Note.inv_Type_Name = Master_Class.invoices_Type_List.FirstOrDefault(x => x.ID == invoice.Type).Name;
            }
            guaranteed_Note.Total_Lett = Number_To_Text.ConvertMoneyToArabicText(guaranteed_Note.Amount.ToString());
            guaranteed_Note.User_ID = Session.user.ID;
            base.Set_Data();
        }
        private bool Is_Code_Exist()
        {
            var data = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Guaranteed_Notes>(
                @"Select * From [FinalSalesDB].[dbo].[Guaranteed_Notes] 
                      Where [ID] != @id And [Is_Cash_In] = @isIn And [Code] = @code",
                new
                {
                    id = guaranteed_Note.ID,
                    isIn = is_Cash_In,
                    code = GuaraCodeTxt.Text.Trim()
                }).Count;
            if (data > 0)
                GuaraCodeTxt.ErrorText = Messages.Code_Exist;
            return (data <= 0);
        }
        protected override bool IsDataValid()
        {
            int numError = 0;
            numError += GuaraCodeTxt.Is_The_Text_Valid() ? 0 : 1;
            numError += Is_Code_Exist() ? 0 : 1;
            numError += PartTypelokUpEdt.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += PartIDGridLokUpEdt.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += DrawerLokUpEdt.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += BranchlokUpEdt.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += GuaraDateDateEdit.Is_The_Date_Valid() ? 0 : 1;
            numError += DiscountValueSpnEdt.Is_The_Edit_Value_Less_Than_Zero() ? 0 : 1;
            numError += PaidSpnEdt.Is_The_Edit_Value_More_Than_Zero() ? 0 : 1;
            if (GuaraGridView.DataRowCount > 1)
            {
                if(is_Cash_In && !Session.Current_User_Settings_Prope.Cash_In_Settings.Can_Make_Note_In_For_More_Than_One_Invoice)
                {
                    XtraMessageBox.Show("ليس مصرح لك عمل سند لأكثر من فاتورة", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    numError++;
                }
                else if(!is_Cash_In && !Session.Current_User_Settings_Prope.Cash_Out_Settings.Can_Make_Note_For_More_Than_One_Invoice)
                {
                    XtraMessageBox.Show("ليس مصرح لك عمل سند لأكثر من فاتورة", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    numError++;
                }
            }
            if (Convert.ToDouble(PaidSpnEdt.EditValue) > Convert.ToDouble(MoneyWantedSpn.EditValue))
            {
                PaidSpnEdt.ErrorText = "المدفوع أكبر من المبلغ المطلوب";
                numError++;
            }
            return (numError == 0);
        }
        protected override void Save()
        {
            string msg = "";
            if (guaranteed_Note.ID == 0)
            {
                guaranteed_Note.ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.Guaranteed_Notes>(
              @"Insert Into [FinalSalesDB].[dbo].[Guaranteed_Notes] Values (
                  @isIn,@code,@invID,@invType,@invName,@strID,@drwID,@partID,@partType,@amount,@disv,@date,
                  @note,@lett,@uID)",
              new
              {
                  isIn = guaranteed_Note.Is_Cash_In,
                  code = guaranteed_Note.Code,
                  invID = guaranteed_Note.Invoice_ID,
                  invType = guaranteed_Note.Invoice_Type,
                  invName = guaranteed_Note.inv_Type_Name,
                  strID = guaranteed_Note.Store_ID,
                  drwID = guaranteed_Note.Drawer_ID,
                  partID = guaranteed_Note.Part_ID,
                  partType = guaranteed_Note.Part_Type,
                  amount = guaranteed_Note.Amount,
                  disv = guaranteed_Note.Discount_Val,
                  date = guaranteed_Note.Date,
                  note = guaranteed_Note.Note,
                  lett = guaranteed_Note.Total_Lett,
                  uID = guaranteed_Note.User_ID
              }).FirstOrDefault().ID;
            }
            else
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce(
                  @"UPDATE [dbo].[Guaranteed_Notes]
                    SET [Is_Cash_In] = @isIn
                       ,[Code] = @code
                       ,[Invoice_ID] = @invID
                       ,[Invoice_Type] = @invType
                       ,[inv_Type_Name] = @invName
                       ,[Store_ID] = @strID
                       ,[Drawer_ID] = @drwID
                       ,[Part_ID] = @partID
                       ,[Part_Type] = @partType
                       ,[Amount] = @amount
                       ,[Discount_Val] = @disv
                       ,[Date] = @date
                       ,[Note] = @note
                       ,[Total_Lett] = @lett
                       ,[User_ID] = @uID
                  WHERE [ID] = @id",
              new
              {
                  isIn = guaranteed_Note.Is_Cash_In,
                  code = guaranteed_Note.Code,
                  invID = guaranteed_Note.Invoice_ID,
                  invType = guaranteed_Note.Invoice_Type,
                  invName = guaranteed_Note.inv_Type_Name,
                  strID = guaranteed_Note.Store_ID,
                  drwID = guaranteed_Note.Drawer_ID,
                  partID = guaranteed_Note.Part_ID,
                  partType = guaranteed_Note.Part_Type,
                  amount = guaranteed_Note.Amount,
                  disv = guaranteed_Note.Discount_Val,
                  date = guaranteed_Note.Date,
                  note = guaranteed_Note.Note,
                  lett = guaranteed_Note.Total_Lett,
                  uID = guaranteed_Note.User_ID,
                  id = guaranteed_Note.ID
              });
            }
            Handel_Coupes();
            base.Save();
            if (msg != "")
                XtraMessageBox.Show(msg, "ملاحظة", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Handel_Coupes()
        {
            List<DBModels.Coupes_Of_Account> coupesAcc = new List<DBModels.Coupes_Of_Account>();
            Delete_Coupe_Of_Accounts(guaranteed_Note.ID,
                    Convert.ToByte((is_Cash_In ? Guaranteed_Types.CashNoteIn : Guaranteed_Types.CashNoteOut)));
            int drawerAccID = Session.Drawers.Where(x => x.ID == Convert.ToInt32(DrawerLokUpEdt.EditValue)).FirstOrDefault().Account_ID;
            int personAccID = ((Convert.ToByte(PartTypelokUpEdt.EditValue) == Convert.ToByte(Part_Type.Customer)) ?
                Session.Customers.Where(x => x.ID == Convert.ToInt32(PartIDGridLokUpEdt.EditValue)).FirstOrDefault().Account_ID :
                Session.Suppliers.Where(x => x.ID == Convert.ToInt32(PartIDGridLokUpEdt.EditValue)).FirstOrDefault().Account_ID);
            string coupesmsg = (is_Cash_In ? "- سند قبض من -" : "-  سند صرف ل-");
            coupesmsg += $" {PartTypelokUpEdt.Text.Trim()} {PartIDGridLokUpEdt.Text.Trim()}";
            coupesAcc.Add(new DBModels.Coupes_Of_Account()
            {
                Code = "10",//todo code
                Account_ID = drawerAccID,
                Debit = (is_Cash_In ? guaranteed_Note.Amount : 0),
                Credit = (!is_Cash_In ? guaranteed_Note.Amount : 0),
                Insert_Date = DateTime.Now.Date,
                Source_Type = Convert.ToByte((is_Cash_In ? Guaranteed_Types.CashNoteIn :
                                                           Guaranteed_Types.CashNoteOut)),
                Source_ID = guaranteed_Note.ID,
                Notes = coupesmsg,
                Move_Name = Convert.ToByte((is_Cash_In ? Account_Move_Name.CashNoteIn : Account_Move_Name.CashNoteOut)),
                User_ID = Session.user.ID
            });
            coupesAcc.Add(new DBModels.Coupes_Of_Account()
            {
                Code = "10",//todo code
                Account_ID = personAccID,
                Debit = (!is_Cash_In ? guaranteed_Note.Amount : 0),
                Credit = (is_Cash_In ? guaranteed_Note.Amount : 0),
                Insert_Date = DateTime.Now.Date,
                Source_Type = Convert.ToByte((is_Cash_In ? Guaranteed_Types.CashNoteIn :
                                                           Guaranteed_Types.CashNoteOut)),
                Source_ID = guaranteed_Note.ID,
                Notes = coupesmsg,
                Move_Name = Convert.ToByte((is_Cash_In ? Account_Move_Name.CashNoteIn : Account_Move_Name.CashNoteOut)),
                User_ID = Session.user.ID
            });
            var coupes = new
            {
                coupes = Convert_Coupes_List_To_Table(coupesAcc).AsTableValuedParameter("Account_Coupes_List_Type")
            };
            DAL.Impelement_Stored_Procedure.Excute_Proce("[FinalSalesDB].[dbo].[Add_List_Of_Coupes]", coupes, isCommandText: false, commit: true);
        }
        private void ChooseBillsBtn_Click(object sender, EventArgs e)
        {
            if (!PartIDGridLokUpEdt.Is_The_Lkp_Text_Valid())
            {
                XtraMessageBox.Show("!!يجب إختيار عميل أو مورد أولاً", "فشل فتح النافذة", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            using (XtraForm frm = new XtraForm()
            {
                Size = new Size(750, 300),
                StartPosition = FormStartPosition.CenterScreen,
                Name = "Frm_Select_Invoice",
                Text = "إختر فواتير للتسديد ",
                RightToLeft = this.RightToLeft,
                RightToLeftLayout = this.RightToLeftLayout,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            })
            {
                GridControl control = new GridControl();
                control.Dock = DockStyle.Fill;
                GridView view = new GridView();
                control.MainView = view;
                view.OptionsBehavior.Editable = false;
                view.OptionsView.ShowGroupPanel = false;
                view.OptionsCustomization.AllowColumnMoving = false;
                view.OptionsCustomization.AllowQuickHideColumns = false;
                view.OptionsSelection.MultiSelect = true;
                view.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                view.OptionsView.ShowIndicator = false;
                view.OptionsCustomization.AllowSort = false;
                view.OptionsBehavior.AutoPopulateColumns = false;
                view.SelectionChanged += (vsender, ve) =>
                {
                    if (ve.ControllerRow < 0) return;
                    DBModels.Invoice selectedRows = this.invoiceList[ve.ControllerRow];
                    BindingList<DBModels.Invoice> source = GuaraGridView.DataSource as BindingList<DBModels.Invoice>;
                    if (source == null) return;
                    if (ve.Action == CollectionChangeAction.Add)
                    {
                        if (source.Where(x => x.ID == selectedRows.ID).Count() == 0)
                        {
                            source.Add(new DBModels.Invoice()
                            {
                                ID = selectedRows.ID,
                                Code = selectedRows.Code,
                                Store_ID = selectedRows.Store_ID,
                                Discount_Val = selectedRows.Discount_Val,
                                Tax_Val = selectedRows.Tax_Val,
                                Total = selectedRows.Total,
                                Net = selectedRows.Net,
                                Paid = selectedRows.Paid,
                                Drawer_ID = selectedRows.Drawer_ID,
                                Remaining = selectedRows.Remaining,
                                Type = selectedRows.Type,
                                Date = selectedRows.Date
                            });
                            frm.Close();
                        }
                    }
                    else if (ve.Action == CollectionChangeAction.Remove)
                    {
                        if (source.Where(x => x.ID == selectedRows.ID).Count() > 0)
                        {
                            source.Remove(source.FirstOrDefault(x => x.ID == selectedRows.ID));
                        }
                    }
                    GuaraGridView.RefreshData();
                };
                RepositoryItemLookUpEdit drawer = new RepositoryItemLookUpEdit();
                RepositoryItemLookUpEdit stores = new RepositoryItemLookUpEdit();
                RepositoryItemLookUpEdit type = new RepositoryItemLookUpEdit();
                control.RepositoryItems.AddRange(new RepositoryItem[] { drawer, stores, type });
                view.Columns.AddField("ID").Visible = false;
                view.Columns.AddField("Code").Caption = "الكود";
                GridColumn typeColumn = view.Columns.AddField("Type");
                typeColumn.Visible = true;
                typeColumn.Caption = "نوع الفاتورة";
                view.Columns.AddField("Date").Caption = "التاريخ";
                GridColumn storeColumn = view.Columns.AddField("Store_ID");
                storeColumn.Visible = true;
                storeColumn.Caption = "المخزن";
                view.Columns.AddField("Discount_Val").Caption = "الخصم";
                view.Columns.AddField("Tax_Val").Caption = "الضريبة";
                view.Columns.AddField("Total").Caption = "الإجمالي";
                view.Columns.AddField("Net").Caption = "الصافي";
                view.Columns.AddField("Paid").Caption = "المدفوع";
                view.Columns.AddField("Remaining").Caption = "الباقي";
                GridColumn drawerColumn = view.Columns.AddField("Drawer_ID");
                drawerColumn.Visible = true;
                drawerColumn.Caption = "الخزينة";
                view.Columns["Code"].Visible = true;
                view.Columns.AddField("Date").Visible = true;
                view.Columns["Discount_Val"].Visible = true;
                view.Columns["Tax_Val"].Visible = true;
                view.Columns["Total"].Visible = true;
                view.Columns["Net"].Visible = true;
                view.Columns["Paid"].Visible = true;
                view.Columns["Remaining"].Visible = true;
                
                control.DataSource = this.invoiceList;
                drawer.Initilaze_Data(Session.Drawers, control, drawerColumn);
                stores.Initilaze_Data(Session.Stores, control, storeColumn);
                type.Initilaze_Data(Master_Class.invoices_Type_List, control, typeColumn);
                frm.Controls.Add(control);
                view.BestFitColumns();
                control.ForceInitialize();
                BindingList<DBModels.Invoice> source1 = GuaraGridView.DataSource as BindingList<DBModels.Invoice>;
                if (source1 == null) return;
                for (int i = 0; i < invoiceList.Count(); i++)
                {
                    if (source1.Where(x => x.ID == invoiceList[i].ID).Count() > 0)
                    {
                        view.SelectRow(i);
                    }
                }
                frm.ShowDialog();
            }
        }
        private void GuaraGridView_RowCountChanged(object sender, EventArgs e)
        {
            var rows = GuaraGridView.DataSource as BindingList<DBModels.Invoice>;
            if (rows.Count > 0)
            {
                var sum = DAL.Impelement_Stored_Procedure.SelectData<DBModels.ID_Model>(
               @"Select ISNULL(SUM([Amount]),0) AS Sum From [FinalSalesDB].[dbo].[Guaranteed_Notes]
                 Where Invoice_ID = @invID And ID != @id ",
               new
               {
                   invID = rows.FirstOrDefault().ID,
                   id = guaranteed_Note.ID
               }).FirstOrDefault();
                if (sum != null)
                {
                    AlreadPaidSpn.EditValue = sum.Sum;
                    MoneyWantedSpn.EditValue = rows.Sum(x => (double?)x.Remaining ?? 0) - sum.Sum;
                }
            }
        }
        private void TotalBillSpnEdt_DoubleClick(object sender, EventArgs e)
        {
            PaidSpnEdt.EditValue = MoneyWantedSpn.EditValue;
        }
        protected override void Delete()
        {
            Delete_Coupe_Of_Accounts(guaranteed_Note.ID, Convert.ToByte((is_Cash_In ? Guaranteed_Types.CashNoteIn : Guaranteed_Types.CashNoteOut)));
            DAL.Impelement_Stored_Procedure.Excute_Proce(
                @"Delete From [FinalSalesDB].[dbo].[Guaranteed_Notes] Where ID = @id", new { id = guaranteed_Note.ID });
            base.Delete();
        }
        public override void Print()
        {
            if (guaranteed_Note.ID <= 0) return;
            Print(guaranteed_Note.ID, is_Cash_In, this.Name);
        }
        public static void Print(int id, bool type, string callerScreenName)
        {
            Print(new List<int> { id }, type, callerScreenName);
        }
        public static void Print(List<int> ids, bool isCachIn, string callerScreenName)
        {
            string name = "";
            if (isCachIn)
                name = Screens.cash_Note_In.Screen_Name;
            else
                name = Screens.cash_Note_Out.Screen_Name;

            if (Is_Authorized(name, Screen_Actions.Print) == false)
                return;
            var guaraID = new
            {
                guaraID = Convert_List_To_DataTable.Convert_ID_List_To_Table(ids).
                AsTableValuedParameter("[dbo].[IDs_List_Type]")
            };
            var Data = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Full_Guarnteed_Note>(
                "[FinalSalesDB].[dbo].[Get_Single_Full_Guarantee_Note]", guaraID, isCommandText: false);
            //foreach (var items in Data)
            //{
            //    foreach (var item in items.Link)
            //    {
            //        item.Type_Name = (item.Invoice_Type == Convert.ToByte(bill_Type.Buy)) ? "فاتورة مشتريات" :
            //                        (item.Invoice_Type == Convert.ToByte(bill_Type.Sale)) ? "فاتورة مبيعات" :
            //                        (item.Invoice_Type == Convert.ToByte(bill_Type.BuyReturn)) ? "فاتورة مرتجع شراء" :
            //                        (item.Invoice_Type == Convert.ToByte(bill_Type.SaleReturn)) ? "فاتورة مرتجع بيع" : "UNknown";
            //    }
            //}
            Data.ForEach(x =>
            {
                Frm_Master.Insert_User_Action(User_Actions.Print, x.ID, x.Code + " - " + x.PersonName, callerScreenName);
            });
            Reports.Rpt_Guaranteed_Note.Print(Data, isCachIn);
        }
        private void Read_User_Settings()
        {
            if (is_Cash_In)
            {
                GuaraDateDateEdit.Enabled = Session.Current_User_Settings_Prope.Cash_In_Settings.Can_Change_Note_In_Date;
                PartIDGridLokUpEdt.Enabled = Session.Current_User_Settings_Prope.Cash_In_Settings.Can_Make_Note_In_For_Supplier;
            }
            else
            {
                GuaraDateDateEdit.Enabled = Session.Current_User_Settings_Prope.Cash_Out_Settings.Can_Change_Note_Date;
                PartIDGridLokUpEdt.Enabled = Session.Current_User_Settings_Prope.Cash_Out_Settings.Can_Make_Note_For_Customer;
            }
            //DrawerLokUpEdt.EditValue = Session.Current_User_Settings_Prope.General_Settings.DefualtDrawer;
            DrawerLokUpEdt.Enabled = Session.Current_User_Settings_Prope.General_Settings.CanChangeDrawer;
        }
    }
}
