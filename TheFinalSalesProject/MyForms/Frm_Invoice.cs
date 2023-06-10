using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
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
using static TheFinalSalesProject.Classes.Delete_Data;
using static TheFinalSalesProject.Classes.Convert_List_To_DataTable;
using TheFinalSalesProject.MyForms;
using System.Collections.ObjectModel;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using System.Diagnostics;
using Dapper;
using DevExpress.XtraLayout.Utils;

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Invoice : Frm_Master
    {
        protected DBModels.Invoice invc;
        private bool valid = true;
        protected bill_Type bill_Type = bill_Type.Buy;//temp
        private RepositoryItemGridLookUpEdit repoProductGridLookUpEdit;
        private RepositoryItemLookUpEdit repoProductLkpAll;//the explainig exist in the customrowedit
        private RepositoryItemLookUpEdit repoUnitsLokEdit;
        private RepositoryItemLookUpEdit repoStoreLokEdit;
        private Account_Balance account_Balance;
        private readonly DBModels.Invoice_Details invcDetail;
        private List<DBModels.Invoice_Details> returnedSourceDetail = new List<DBModels.Invoice_Details>();
        public Frm_Invoice(bill_Type bill_Type)
        {
            InitializeComponent();
            this.bill_Type = bill_Type;
            Screen_Events();
            Set_Form_Type();
            Refresh_Data();
            New();
        }
        public Frm_Invoice(bill_Type bill_Type, int bid)
        {
            InitializeComponent();
            this.bill_Type = bill_Type;
            Screen_Events();
            Set_Form_Type();
            Refresh_Data();
            invc = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Invoice>(
                @"Select * From Invoice 
                           Where ID = @id", new { id = bid }).SingleOrDefault();
            Get_Data();
            isNew = false;
        }
        private void Set_Form_Type()
        {
            switch (bill_Type)
            {
                case bill_Type.Buy:
                    this.Text = " فاتورة مشتريات";
                    this.Name = Screens.add_Buy_Bill.Screen_Name;
                    break;
                case bill_Type.Sale:
                    this.Text = " فاتورة بيع";
                    this.Name = Screens.add_Sale_Bill.Screen_Name;
                    break;
                case bill_Type.BuyReturn:
                    this.Text = " فاتورة مرتجع شراء";
                    this.Name = Screens.add_Buy_Return_Bill.Screen_Name;
                    break;
                case bill_Type.SaleReturn:
                    this.Text = " فاتورة مرتجع بيع";
                    this.Name = Screens.add_Sale_Return_Bill.Screen_Name;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        private void Add_Returned_Cloumns()
        {
            BillDetailsGridView.Columns.Add(new GridColumn()
            {
                Name = "clmnSourceQty",
                FieldName = "SourceQty",
                UnboundType = UnboundColumnType.Decimal,
                VisibleIndex = BillDetailsGridView.Columns[nameof(invcDetail.Qty)].VisibleIndex - 1,
                Caption = "الكمية الأساسية",
            });
            BillDetailsGridView.Columns.Add(new GridColumn()
            {
                Name = "clmnReturnedQty",
                FieldName = "ReturnedQty",
                UnboundType = UnboundColumnType.Decimal,
                VisibleIndex = BillDetailsGridView.Columns[nameof(invcDetail.Qty)].VisibleIndex - 1,
                Caption = "الكمية المرجعة مسبقاً",
            });
            BillDetailsGridView.Columns["ReturnedQty"].OptionsColumn.AllowEdit =
            BillDetailsGridView.Columns["SourceQty"].OptionsColumn.AllowEdit = false;
            //todo focuse
            BillDetailsGridView.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            empty2.Visibility =
            AddProRetLyc.Visibility =
            empty1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            empty1.Location = new Point(598, 0);
            AddProRetLyc.Location = new Point(397, 230);
            empty2.Location = new Point(0, 0);
        }
        private void Screen_Events()
        {
            PartIDGridLokUpEdt.ButtonClick += PartIDGridLokUpEdt_ButtonClick;
            PartTypelokUpEdt.EditValueChanged += PartTypelokUpEdt_EditValueChanged;
            BillDetailsGridView.InvalidRowException += new InvalidRowExceptionEventHandler(BillDetailsGridView_InvalidRowException);
            BillDetailsGridView.ValidateRow += new ValidateRowEventHandler(BillDetailsGridView_ValidateRow);
            PartIDGridLokUpEdt.EditValueChanged += PartIDGridLokUpEdt_EditValueChanged;
            BillSourceIDGrdLkp.EditValueChanged += BillSourceIDGrdLkp_EditValueChanged;
            //we put the event here because we assign the value of this tool before the
            //event created i moved the grid view event into initialize component
            #region handelMoney Events
            DiscountValueSpnEdt.Enter += new EventHandler(DiscountValueSpnEdt_Enter);
            DiscountValueSpnEdt.Leave += new EventHandler(DiscountValueSpnEdt_Leave);
            DiscountValueSpnEdt.EditValueChanged += new EventHandler(DiscountValueSpnEdt_EditValueChanged);
            DiscountRotationSpnEdt.EditValueChanged += new EventHandler(DiscountValueSpnEdt_EditValueChanged);

            TaxValueSpnEdt.Enter += new EventHandler(TaxValueSpnEdt_Enter);
            TaxValueSpnEdt.Leave += new EventHandler(TaxValueSpnEdt_Leave);
            TaxValueSpnEdt.EditValueChanged += new EventHandler(TaxValueSpnEdt_EditValueChanged);
            TaxRotationSpnEdt.EditValueChanged += new EventHandler(TaxValueSpnEdt_EditValueChanged);
            TaxValueSpnEdt.EditValueChanged += Calculate_Sum_Net;
            DiscountValueSpnEdt.EditValueChanged += Calculate_Sum_Net;
            TotalSpnEdt.EditValueChanged += Calculate_Sum_Net;
            ExpencesSpnEdt.EditValueChanged += Calculate_Sum_Net;
            PaidSpnEdt.EditValueChanged += Calculate_Remaining;
            NetSpnEdt.EditValueChanged += Calculate_Remaining;
            NetSpnEdt.EditValueChanging += NetSpnEdt_EditValueChanging;
            NetSpnEdt.DoubleClick += NetSpnEdt_DoubleClick;
            #endregion

            //#region datagridViewEvent
            BillDetailsGridCtrl.ProcessGridKey += new KeyEventHandler(BillDetailsGridCtrl_ProcessGridKey);
            BillDetailsGridView.CustomUnboundColumnData += new CustomColumnDataEventHandler(BillDetailsGridView_CustomUnboundColumnData);
            BranchlokUpEdt.EditValueChanging += new ChangingEventHandler(BranchlokUpEdt_EditValueChanging);
            BillDetailsGridView.RowUpdated += new RowObjectEventHandler(BillDetailsGridView_RowUpdated);
            BillDetailsGridView.RowCountChanged += new EventHandler(BillDetailsGridView_RowCountChanged);
            BillDetailsGridView.CellValueChanged += new CellValueChangedEventHandler(BillDetailsGridView_CellValueChanged);
            BillDetailsGridView.CustomRowCellEditForEditing += new CustomRowCellEditEventHandler(BillDetailsGridView_CustomRowCellEditForEditing);
            BillDetailsGridView.CellValueChanging += BillDetailsGridView_CellValueChanging;
            BillDetailsGridView.MouseLeave += BillDetailsGridView_MouseLeave;
            //#endregion
            ReturnedBillsGrdViw.RowCountChanged += ReturnedBillsGrdViw_RowCountChanged;
        }
        public void Frm_Invoice_Load(object sender, EventArgs e)
        {
            switch (bill_Type)
            {
                case bill_Type.Buy:
                    this.IsPostedChckEdt.Enabled = false;
                    this.IsPostedChckEdt.Checked = true;
                    BillDetailsGridView.Columns[nameof(invcDetail.Store_ID)].OptionsColumn.AllowFocus = false;
                    break;
                case bill_Type.Sale:
                    break;
                case bill_Type.BuyReturn:
                    BillSourceLyc.Visibility = LayoutVisibility.Always;
                    this.IsPostedChckEdt.Enabled = false;
                    this.IsPostedChckEdt.Checked = true;
                    BillDetailsGridView.Columns[nameof(invcDetail.Store_ID)].OptionsColumn.AllowFocus = false;
                    BillDetailsGridView.Columns[nameof(invcDetail.Product_ID)].OptionsColumn.AllowFocus = false;
                    BillDetailsGridView.Columns[nameof(invcDetail.Unit_ID)].OptionsColumn.AllowFocus = false;
                    Add_Returned_Cloumns();
                    break;
                case bill_Type.SaleReturn:
                    BillSourceLyc.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    BillDetailsGridView.Columns[nameof(invcDetail.Product_ID)].OptionsColumn.AllowFocus = false;
                    BillDetailsGridView.Columns[nameof(invcDetail.Unit_ID)].OptionsColumn.AllowFocus = false;
                    Add_Returned_Cloumns();
                    break;
                default:
                    throw new NotImplementedException();
            }
            //هنا بنحدد كيف عيعدل المستخدم في الواجهة
            BillslyC.CustomizationMode = CustomizationModes.Quick;
            BillslyC.OptionsCustomizationForm.ShowLoadButton =
            BillslyC.OptionsCustomizationForm.ShowSaveButton = false;
            BillslyC.OptionsCustomizationForm.ShowPropertyGrid = false;
            foreach (BaseLayoutItem item in BillslyC.Items)
            {//هذي عشان لايحذفش المستخدم اي اداة في الواجهة
                item.AllowHide = false;
            }
            PrintBtn.Visibility = BarItemVisibility.Always;
            PartTypelokUpEdt.Initilaze_Data(Master_Class.part_Type_List);
            PartTypelokUpEdt.Properties.PopulateColumns();
            PartTypelokUpEdt.Look_Up_Edit_Translate_Column("Types");
           // PartTypelokUpEdt.Properties.Columns["ID"].Visible = false;

           
            repoProductGridLookUpEdit = new RepositoryItemGridLookUpEdit();
            repoProductGridLookUpEdit.Initilaze_Data(Session.Full_products.Where(x => x.Is_Active = true).ToList(), BillDetailsGridCtrl, BillDetailsGridView.Columns[nameof(invcDetail.Product_ID)]);
            GridView repoProlkpV = repoProductGridLookUpEdit.View;
            repoProlkpV.PopulateColumns(repoProductGridLookUpEdit.DataSource);
            
            repoProlkpV.Grid_View_Translate_Column("Product");
            //
            //بما ان الاده اعلاه تحتوي على قريد كنترول وفيو فنستطيع التعامل معها كذلك
            repoProductLkpAll = new RepositoryItemLookUpEdit();//todo
            repoProductLkpAll.Initilaze_Data(Session.Full_products, BillDetailsGridCtrl, BillDetailsGridView.Columns[nameof(invcDetail.Product_ID)]);
            //
            repoUnitsLokEdit = new RepositoryItemLookUpEdit();
            repoUnitsLokEdit.Initilaze_Data(Session.Units, BillDetailsGridCtrl, BillDetailsGridView.Columns[nameof(invcDetail.Unit_ID)]);
            repoUnitsLokEdit.PopulateColumns();
            repoUnitsLokEdit.Repo_Look_Up_Edit_Translate_Column("Unit");
            repoUnitsLokEdit.Columns["ID"].Visible = false;
            repoStoreLokEdit = new RepositoryItemLookUpEdit();
            repoStoreLokEdit.Initilaze_Data(Session.Stores, BillDetailsGridCtrl, BillDetailsGridView.Columns[nameof(invcDetail.Store_ID)]);
            repoStoreLokEdit.PopulateColumns();
            repoStoreLokEdit.Repo_Look_Up_Edit_Translate_Column("Store");
            //
            BillDetailsGridView.Columns[nameof(invcDetail.ID)].Visible = false;
            BillDetailsGridView.Columns[nameof(invcDetail.Invoice_ID)].Visible = false;
            BillDetailsGridView.Add_Spn_Repo_Ratio(BillDetailsGridCtrl, "Discount_Prece");
            BillDetailsGridView.Add_Spn_Repo_Value(BillDetailsGridCtrl, "Discount_Val");
            BillDetailsGridView.Add_Spn_Repo_Value(BillDetailsGridCtrl, "Total_Price");
            BillDetailsGridView.Add_Spn_Repo_Value(BillDetailsGridCtrl, "Qty");
            BillDetailsGridView.Add_Spn_Repo_Value(BillDetailsGridCtrl, "Price");
            BillDetailsGridView.Add_Spn_Repo_Value(BillDetailsGridCtrl, "Cost");
            BillDetailsGridView.Add_Spn_Repo_Value(BillDetailsGridCtrl, "Total_Cost");
            //
            #region gridViewThings
            BillDetailsGridView.Add_NumberColumn_To_GridView(BillDetailsGridCtrl);
            BillDetailsGridView.Add_CodeColumn_To_GridView(BillDetailsGridCtrl);
            BillDetailsGridView.Add_BalanceColumn_To_GridView(BillDetailsGridCtrl);
            BillDetailsGridView.Columns[nameof(invcDetail.Product_ID)].Caption = "الصنف";
            BillDetailsGridView.Columns[nameof(invcDetail.Unit_ID)].Caption = "الوحدة";
            BillDetailsGridView.Columns[nameof(invcDetail.Price)].Caption = "السعر";
            if (bill_Type == bill_Type.BuyReturn || bill_Type == bill_Type.SaleReturn)
            {
                BillDetailsGridView.Columns[nameof(invcDetail.Qty)].Caption = "الكمية المرتجعة";
                BillDetailsGridView.Columns["SourceQty"].VisibleIndex = BillDetailsGridView.Columns[nameof(invcDetail.Qty)].VisibleIndex - 1;
                BillDetailsGridView.Columns["ReturnedQty"].VisibleIndex = BillDetailsGridView.Columns[nameof(invcDetail.Qty)].VisibleIndex - 1;
            }
            else
                BillDetailsGridView.Columns[nameof(invcDetail.Qty)].Caption = "الكمية";
            BillDetailsGridView.Columns[nameof(invcDetail.Cost)].Caption = "التكلفة";
            BillDetailsGridView.Columns[nameof(invcDetail.Total_Cost)].Caption = "إجمالي التكلفة";
            BillDetailsGridView.Columns[nameof(invcDetail.Discount_Prece)].Caption = "نسبة الخصم";
            BillDetailsGridView.Columns[nameof(invcDetail.Discount_Val)].Caption = "قيمة الخصم";
            BillDetailsGridView.Columns[nameof(invcDetail.Total_Price)].Caption = "إجمالي السعر";
            BillDetailsGridView.Columns[nameof(invcDetail.Store_ID)].Caption = "المخزن";
            BillDetailsGridView.BestFitColumns();

            BillDetailsGridView.Columns["Number"].OptionsColumn.AllowFocus = false;
            BillDetailsGridView.Columns["Balance"].OptionsColumn.AllowFocus = false;
            BillDetailsGridView.Columns[nameof(invcDetail.Cost)].OptionsColumn.AllowFocus = false;
            BillDetailsGridView.Columns[nameof(invcDetail.Total_Cost)].OptionsColumn.AllowFocus = false;
            BillDetailsGridView.Columns[nameof(invcDetail.Total_Price)].OptionsColumn.AllowFocus = false;
            BillDetailsGridView.Columns[nameof(invcDetail.Source_Back_Row_ID)].Visible = false;
            int indx = 0;
            BillDetailsGridView.Columns["Number"].VisibleIndex = indx++;
            BillDetailsGridView.Columns["Code"].VisibleIndex = indx++;
            BillDetailsGridView.Columns[nameof(invcDetail.Product_ID)].MinWidth = 125;
            BillDetailsGridView.Columns[nameof(invcDetail.Product_ID)].VisibleIndex = indx++;
            BillDetailsGridView.Columns[nameof(invcDetail.Unit_ID)].VisibleIndex = indx++;
            BillDetailsGridView.Columns[nameof(invcDetail.Qty)].VisibleIndex = indx++;
            if (bill_Type == bill_Type.BuyReturn || bill_Type == bill_Type.SaleReturn)
            {
                BillDetailsGridView.Columns["SourceQty"].VisibleIndex = indx++;
                BillDetailsGridView.Columns["ReturnedQty"].VisibleIndex = indx++;
            }
            BillDetailsGridView.Columns[nameof(invcDetail.Price)].VisibleIndex = indx++;
            BillDetailsGridView.Columns[nameof(invcDetail.Discount_Prece)].VisibleIndex = indx++;
            BillDetailsGridView.Columns[nameof(invcDetail.Discount_Val)].VisibleIndex = indx++;
            BillDetailsGridView.Columns[nameof(invcDetail.Total_Price)].VisibleIndex = indx++;
            BillDetailsGridView.Columns[nameof(invcDetail.Cost)].VisibleIndex = indx++;
            BillDetailsGridView.Columns[nameof(invcDetail.Total_Cost)].VisibleIndex = indx++;
            BillDetailsGridView.Columns[nameof(invcDetail.Store_ID)].VisibleIndex = indx++;
            BillDetailsGridView.Columns["Balance"].VisibleIndex = indx++;
            BillDetailsGridView.Add_DeleteColumns_To_GridView(BillDetailsGridCtrl);
            #endregion                                                                          
            //
            BranchlokUpEdt.Properties.PopulateColumns();
            BranchlokUpEdt.Look_Up_Edit_Translate_Column("Store");
            DrawerLokUpEdt.Properties.PopulateColumns();
            DrawerLokUpEdt.Look_Up_Edit_Translate_Column("Drawer");

            this.Activated += Frm_Invoice_Activated;
            Read_User_Settings();
            if (!Debugger.IsAttached)
            {
                BillDetailsGridView.Load_Lyout(this.Name);
                BillslyC.Save_Group_Layout(this.Name);
            }
            ReturnedBillsGrdViw.Columns["ID"].Caption = "الرقم";
            ReturnedBillsGrdViw.Columns["Code"].Caption = "الكود";
            ReturnedBillsGrdViw.Columns["Date"].Caption = "التاريخ";
        }
        /// <summary>
        /// هذا الحدث يحدث عندما يظهر الفورم مش وهو بيتحمل
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_Invoice_Activated(object sender, EventArgs e)
        {
            Move_Focuse_To_GridControl();
        }
        /// <summary>
        /// this make the first the form show it moves the focuse into gridcontrol
        /// </summary>
        private void Move_Focuse_To_GridControl(bool codeFocuse = true)
        {
            this.BillDetailsGridCtrl.Focus();
            BillDetailsGridView.FocusedColumn = codeFocuse ? BillDetailsGridView.Columns["Code"] : BillDetailsGridView.Columns["Product_ID"];
            BillDetailsGridView.AddNewRow();
            BillDetailsGridView.UpdateCurrentRow();
        }
        public void IsPostedChckEdt_CheckedChanged(object sender, EventArgs e)
        {
            
        }
        private string Get_New_Bill_Code()
        {
            string maxCode;
            {
                var s = DAL.Impelement_Stored_Procedure.SelectData<DBModels.ID_Model>(@"Select  ISNUll(Max(Code),'0') AS ID From Invoice").
                    SingleOrDefault().ID.ToString();
                maxCode = string.IsNullOrEmpty(s) ? "0" : s;
                return Master_Class.Get_Next_Number_InTheString(maxCode);
            }
        }
        protected override void New()
        {
            invc = new DBModels.Invoice()
            {
                Drawer_ID = Session.Current_User_Settings_Prope.General_Settings.DefualtDrawer,
                Date = DateTime.Now,
                Is_Posted_To_Store = true,
                Post_Date = DateTime.Now,
                Store_ID = Session.Current_User_Settings_Prope.General_Settings.DefualtBranch,
                Code = Get_New_Bill_Code(),
                //initial Values
            };
            switch (bill_Type)
            {
                case bill_Type.Buy:
                    invc.Person_Type = (byte)Part_Type.Supplier;
                    invc.Person_ID = Session.Current_User_Settings_Prope.General_Settings.DefualtSupplier;
                    break;
                case bill_Type.Sale:
                    invc.Person_Type = (byte)Part_Type.Customer;
                    invc.Person_ID = Session.Current_User_Settings_Prope.General_Settings.DefualtCustomer;
                    break;
                case bill_Type.SaleReturn:
                    invc.Person_Type = (byte)Part_Type.Customer;
                    invc.Person_ID = Session.Current_User_Settings_Prope.General_Settings.DefualtCustomer;
                    break;
                case bill_Type.BuyReturn:
                    invc.Person_Type = (byte)Part_Type.Supplier;
                    invc.Person_ID = Session.Current_User_Settings_Prope.General_Settings.DefualtSupplier;
                    break;
                default:
                    throw new NotImplementedException();
            }
            base.New();
        }
        protected override void Refresh_Data()
        {
            DrawerLokUpEdt.Initilaze_Data(Session.Drawers);
            BranchlokUpEdt.Initilaze_Data(Session.Stores);
            base.Refresh_Data();
        }
        protected override void Get_Data()
        {
            BranchlokUpEdt.EditValue = invc.Store_ID;
            DrawerLokUpEdt.EditValue = invc.Drawer_ID;
            PartTypelokUpEdt.EditValue = invc.Person_Type;
            PartIDGridLokUpEdt.EditValue = invc.Person_ID;
            BillCodeTxt.Text = invc.Code;
            ShippingAddressMemEdt.Text = invc.Shipping_Address;
            NotesMemEdt.Text = invc.Notes;
            BillDateDateEdit.DateTime = invc.Date;
            DelivaryDateEdt.EditValue = invc.Delivary_Date as DateTime?;
            PostedDateEdt.EditValue = invc.Post_Date as DateTime?;
            IsPostedChckEdt.Checked = invc.Is_Posted_To_Store;
            TotalSpnEdt.EditValue = invc.Total;
            DiscountValueSpnEdt.EditValue = invc.Discount_Val;
            DiscountRotationSpnEdt.EditValue = invc.Discount_Perec;
            TaxValueSpnEdt.EditValue = invc.Tax_Val;
            TaxRotationSpnEdt.EditValue = invc.Tax_Perce;
            ExpencesSpnEdt.EditValue = invc.Expences;
            NetSpnEdt.EditValue = invc.Net;
            PaidSpnEdt.EditValue = invc.Paid;//todo get user name
            RemainingSpnEdt.EditValue = invc.Remaining;
            BillSourceIDGrdLkp.EditValue = invc.Invoice_Back_ID;

            BindingList<DBModels.Invoice_Details> list = new BindingList<DBModels.Invoice_Details>(
                DAL.Impelement_Stored_Procedure.SelectData<DBModels.Invoice_Details>(
                @"Select * From Invoice_Details 
                           Where Invoice_ID = @id", new { id = invc.ID }));

            BillDetailsGridCtrl.DataSource = list;
            //هذا اذا شي معا الفاتورة مرتجعات
            ReturnedBillsGrdCtrl.DataSource = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Invoice>(
                @"Select ID , Code , Date From Invoice
                                     Where Invoice_Back_ID = @invID 
                                     And Invoice_Back_ID != 0", new { invID = invc.ID })
                .Select(x => new { x.ID, x.Code, x.Date }).ToList();
            //relate to user movement
            the_Changed_Source_ID = invc.ID;
            the_Changed_Source_Name = invc.Code + " - " + PartIDGridLokUpEdt.Text;
            base.Get_Data();
        }
        protected override void Set_Data()
        {
            invc.Store_ID = Convert.ToInt32(BranchlokUpEdt.EditValue);
            invc.Drawer_ID = Convert.ToInt32(DrawerLokUpEdt.EditValue);
            invc.Person_Type = Convert.ToByte(PartTypelokUpEdt.EditValue);
            invc.Person_ID = Convert.ToInt32(PartIDGridLokUpEdt.EditValue);
            invc.Code = BillCodeTxt.Text;
            invc.Shipping_Address = ShippingAddressMemEdt.Text;
            invc.Notes = NotesMemEdt.Text;
            invc.Date = BillDateDateEdit.DateTime;
            invc.Delivary_Date = DelivaryDateEdt.EditValue as DateTime?;
            invc.Post_Date = PostedDateEdt.EditValue as DateTime?;
            invc.Is_Posted_To_Store = IsPostedChckEdt.Checked;
            invc.Total = Convert.ToDouble(TotalSpnEdt.EditValue);
            invc.Discount_Val = Convert.ToDouble(DiscountValueSpnEdt.EditValue);
            invc.Discount_Perec = Convert.ToDouble(DiscountRotationSpnEdt.EditValue);
            invc.Tax_Val = Convert.ToDouble(TaxValueSpnEdt.EditValue);
            invc.Tax_Perce = Convert.ToDouble(TaxRotationSpnEdt.EditValue);
            invc.Expences = Convert.ToDouble(ExpencesSpnEdt.EditValue);
            invc.Net = Convert.ToDouble(NetSpnEdt.EditValue);
            invc.Paid = Convert.ToDouble(PaidSpnEdt.EditValue);
            invc.Remaining = Convert.ToDouble(RemainingSpnEdt.EditValue);
            invc.Type = (byte)bill_Type;
            invc.Invoice_Back_ID = Convert.ToInt32(BillSourceIDGrdLkp.EditValue);
            invc.User_ID = Session.user.ID;
            base.Set_Data();
        }
        protected override void Get_Screen_History()
        {
            if (isNew) return;
            var mainScreenID = Classes.Screens.Get_Screens.FirstOrDefault(x => x.Screen_Name == this.Name).Screen_ID;
            var listScreens = Classes.Screens.Get_Screens.FirstOrDefault(x => x.Screen_Name == this.Name + "_Bill_List");
            int listScreenID = mainScreenID;
            if (listScreens != null)
                listScreenID = listScreens.Screen_ID;
            var data = DAL.Impelement_Stored_Procedure.SelectData<DBModels.User_Actions_Per_Screen>(
                @"Select * From User_Actions_Per_Screen Where Changed_Source_ID = @chSID
                      And (Screen_ID = @mScnID Or Screen_ID = @lScnID)",
                new
                {
                    chSID = invc.ID,
                    mScnID = mainScreenID,
                    lScnID = listScreenID
                });//todo make db join
            UserMoveGrdCtrl.DataSource = (from d in data.OrderByDescending(x => x.Date)
                                          join u in DAL.Impelement_Stored_Procedure.SelectData<DBModels.User>(
                                              @"Select * From [User]") on d.User_ID equals u.ID
                                          select new
                                          {
                                              u.Real_Name,
                                              d.Date,
                                              action = d.Type == (byte)User_Actions.Add ? "إضافة" :
                                                       d.Type == (byte)User_Actions.Delete ? "حذف" :
                                                       d.Type == (byte)User_Actions.Edit ? "تعديل" :
                                                       d.Type == (byte)User_Actions.Print ? "طباعة" : "؟؟؟؟"
                                          }).ToList();
            UserMoveGrdViw.Columns["Real_Name"].Caption = "إسم المستخدم";
            UserMoveGrdViw.Columns["Date"].Caption = "تاريخ الحركة";
            UserMoveGrdViw.Columns["Date"].DisplayFormat.FormatType = FormatType.Custom;
            UserMoveGrdViw.Columns["Date"].DisplayFormat.FormatString = "yyyy/MM/dd hh:mm:ss tt";
            UserMoveGrdViw.Columns["action"].Caption = "نوع الحركة";
        }
        protected override void Open_New_Window()
        {
            Frm_Main_Window.OpenForm(new Frm_Invoice(this.bill_Type), false);
        }
        private bool Is_Code_Exist()
        {
            var data = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Invoice>(@"Select * From Invoice 
                           Where ID != @id And Type = @type And Code = @code", new { id = invc.ID, type = (byte)bill_Type, code = BillCodeTxt.Text.Trim() }).Count;
            if (data > 0)
                BillCodeTxt.ErrorText = Messages.Code_Exist;
            return (data <= 0);
        }
        protected override bool IsDataValid()
        {
            valid = true;
            int numError = 0;
            if (BillDetailsGridView.RowCount == 0)
            {
                numError++;
                XtraMessageBox.Show("الرجاء إضافة أصناف أولاً ", "فشل حفظ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            numError += BillCodeTxt.Is_The_Text_Valid() ? 0 : 1;
            numError += Is_Code_Exist() ? 0 : 1;
            numError += PartTypelokUpEdt.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += PartIDGridLokUpEdt.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += DrawerLokUpEdt.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += BranchlokUpEdt.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += BillDateDateEdit.Is_The_Date_Valid() ? 0 : 1;
            numError += DiscountValueSpnEdt.Is_The_Edit_Value_Less_Than_Zero() ? 0 : 1;
            numError += ExpencesSpnEdt.Is_The_Edit_Value_Less_Than_Zero() ? 0 : 1;
            numError += PaidSpnEdt.Is_The_Edit_Value_Less_Than_Zero() ? 0 : 1;
            numError += TaxValueSpnEdt.Is_The_Edit_Value_Less_Than_Zero() ? 0 : 1;

            if (IsPostedChckEdt.Checked)
            {
                numError += PostedDateEdt.Is_The_Date_Valid() ? 0 : 1;
            }
            switch (bill_Type)
            {
                case bill_Type.Buy:
                    break;
                case bill_Type.Sale:
                    if (DiscountRotationSpnEdt.EditValue != null)
                        if (invc.Discount_Perec != Convert.ToDouble(DiscountRotationSpnEdt.EditValue) &&
                            Session.Current_User_Settings_Prope.Sale_Settings.MaxDiscountLevelInBills < 
                            Convert.ToDecimal(DiscountRotationSpnEdt.EditValue))
                        {
                            DiscountRotationSpnEdt.ErrorText = "هذه النسبة أكبر من المسموح لك";
                            numError++;
                        }
                    if (invc.ID == 0)
                    {
                        int id = Convert.ToInt32(PartIDGridLokUpEdt.EditValue);
                        if (id > 0)
                        {//هنا نفحص حد الإئتمان
                            DBModels.Customer_Supplier account;//todo come here
                            if (PartTypelokUpEdt.EditValue.Equals(Convert.ToByte(Part_Type.Supplier)))
                            {
                                account = Session.Suppliers.FirstOrDefault(x => x.ID == id);
                            }
                            else
                            {
                                account = Session.Customers.FirstOrDefault(x => x.ID == id);
                            }
                            if (account != null && account.Max_Credit <= account_Balance.Balance_Amount && 
                                account_Balance.Balance_Type == Account_Balance.Balance_Types.Debit)
                            {
                                switch (Session.Current_User_Settings_Prope.Sale_Settings.WhenSellingToCustomerOverInsuranceLimit)
                                {
                                    case Warining_Handel.Do_Not_Interrupt:
                                        break;
                                    case Warining_Handel.Show_Warning:
                                        if (XtraMessageBox.Show("هذا العميل تجاوز حد الإ ئتمان هل تريد المتابعة ؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                                        {
                                            numError++;
                                        }
                                        break;
                                    case Warining_Handel.Prevent:
                                        XtraMessageBox.Show("لا يكمن المتابعة لأن العميل تجاوز حد الإ ئتمان", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                        numError++;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                    break;
                case bill_Type.BuyReturn:
                case bill_Type.SaleReturn:
                    int indx = 0;
                    foreach (var item in BillDetailsGridView.DataSource as BindingList<DBModels.Invoice_Details>)
                    {
                        BillDetailsGridView_ValidateRow(BillDetailsGridView, new ValidateRowEventArgs
                            (BillDetailsGridView.GetRowHandle(indx), item));
                        indx++;
                        if (valid == false)
                            numError++;
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            return (numError == 0);
        }
        protected override void Save()
        {
            using (DataTable table = new DataTable())
            {
                List<DBModels.Pro_Store_Movement> moveProStore = new List<DBModels.Pro_Store_Movement>();
                List<DBModels.Coupes_Of_Account> coupAccount = new List<DBModels.Coupes_Of_Account>();
                BillDetailsGridView.UpdateCurrentRow();
                BindingList<DBModels.Invoice_Details> gridData = BillDetailsGridView.DataSource as BindingList<DBModels.Invoice_Details>;
                if (invc.ID == 0)
                {
                    invc.ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>
                         (@"Insert Into Invoice Values(
                           @type,@code,@perType,@perID,@date,@delDate,@strID,@notes,@isposted,@postDate,@tot,
                           @disv,@disp,@taxv,@taxp,@expen,@net,@paid,@drawer,@remain,@shipAdd,@invBack,@userID)",
                     new
                     {
                         type = invc.Type,
                         code = invc.Code,
                         perType = invc.Person_Type,
                         perID = invc.Person_ID,
                         date = invc.Date,
                         delDate = invc.Delivary_Date,
                         strID = invc.Store_ID,
                         notes = invc.Notes,
                         isposted = invc.Is_Posted_To_Store,
                         postDate = invc.Post_Date,
                         tot = invc.Total,
                         disv = invc.Discount_Val,
                         disp = invc.Discount_Perec,
                         taxv = invc.Tax_Val,
                         taxp = invc.Tax_Perce,
                         expen = invc.Expences,
                         net = invc.Net,
                         paid = invc.Paid,
                         drawer = invc.Drawer_ID,
                         remain = invc.Remaining,
                         shipAdd = invc.Shipping_Address,
                         invBack = invc.Invoice_Back_ID,
                         userID = invc.User_ID
                     }).SingleOrDefault().ID;
                }
                else
                {
                    DAL.Impelement_Stored_Procedure.Excute_Proce(@"dbo.Update_Invoice",
                       new
                       {
                           type = invc.Type,
                           code = invc.Code,
                           perType = invc.Person_Type,
                           perID = invc.Person_ID,
                           date = invc.Date,
                           delDate = invc.Delivary_Date,
                           strID = invc.Store_ID,
                           notes = invc.Notes,
                           isposted = invc.Is_Posted_To_Store,
                           postDate = invc.Post_Date,
                           tot = invc.Total,
                           disv = invc.Discount_Val,
                           disp = invc.Discount_Perec,
                           taxv = invc.Tax_Val,
                           taxp = invc.Tax_Perce,
                           expen = invc.Expences,
                           net = invc.Net,
                           paid = invc.Paid,
                           drawer = invc.Drawer_ID,
                           remain = invc.Remaining,
                           shipAdd = invc.Shipping_Address,
                           invBack = invc.Invoice_Back_ID,
                           userID = invc.User_ID,
                           id = invc.ID
                       }, isCommandText: false);
                }
                //هنا جمعنا ذي في القريد فيو عشان ندخلهن
                switch (bill_Type)
                {
                    case bill_Type.Buy:
                        if (invc.Expences > 0)
                        {
                            double totalPrice = gridData.Sum(x => x.Total_Price);
                            double totalQuantity = gridData.Sum(x => x.Qty);
                            double costByPrice = invc.Expences / totalPrice;//سعر التكلفة لكل ريال
                            double costByQuantity = invc.Expences / totalQuantity;//سعر التكلفة لكل وحدة
                            //هنا دخلنا اليوزر كنترول لل xtradialog
                            XtraDialogArgs args = new XtraDialogArgs();
                            ((XtraBaseArgs)args).Buttons = new DialogResult[] { DialogResult.OK };
                            UserControl.Distribiute_Method_Choose method_Choose = new UserControl.Distribiute_Method_Choose();
                            args.Content = method_Choose;
                            args.Showing += Args_Showing;//
                            XtraDialog.Show(args);
                            foreach (var item in gridData)
                            {
                                if (method_Choose.Selected_Buttun == Cost_Distribute.Price)
                                    item.Cost = (item.Total_Price / item.Qty) + (costByPrice * item.Price);//توزيع حسب السعر
                                else
                                    item.Cost = (item.Total_Price / item.Qty) + (costByQuantity);//توزيع حسب الكمية
                                item.Total_Cost = item.Qty * item.Cost;
                                item.Invoice_ID = invc.ID;
                            }
                        }
                        else
                        {
                            //هذا عشان اذا المستخدم حفظ على المصروفات وبعدا حذف المصروفات وحفظ مرة ثانية
                            foreach (var item in gridData)
                            {
                                item.Cost = item.Total_Price / item.Qty;
                                item.Total_Cost = item.Total_Price;
                                item.Invoice_ID = invc.ID;
                            }
                        }
                        break;
                    case bill_Type.Sale://todo make sure that buyReturn is right While Saving
                        break;
                    case bill_Type.BuyReturn:
                    case bill_Type.SaleReturn:
                        break;
                    default:
                        throw new NotImplementedException();
                }

                #region Coupes
                /*
                 هنا غنعمل القيود مثلا الفاتورة فاتورة مشتريات وقيمتها الف
                 والخصم مية والضرايب ميتين وصرفت على الفاتورة خمسين ذلحين عندما اجي اعمل الفاتورة
                 يجب ان يكون قيمة المدين نفس قيمة الداين
                 الداين هو ذي دينًي المعطي والمدين هو الذي ياخذ 
                 ذلحين في هذي الفاتورة المورد داين للمخزن بقيمة الف +ميتين+خمسين
                 لانو انا ماقد حاسبته يعني انا مدين للمورد بالف وميتين وخمسين لاما اسددها
                 عيوقع هكذا 
                 هذا في حساب صاحب المحل
                        حساب المخزون            مدين  بالف وخمسين       
                        حساب المخزون ضريبة مضافة            مدين  ميتين
             حساب المورد            داين بالف وميتين وخمسين
                           حساب الخصم            دائن  مية
                           حساب المورد            مدين  مية
                يعني هانا قد المورد شل مية عيبقى له الف ومية وخمسين
                عند السداد عيوقع هكذا 
                        حساب المخزون            داين  بالف وخمسين       
                        حساب المخزون ضريبة مضافة            داين  ميتين
                           حساب المورد                     مدين بالف 
                           حساب الخصم                      دائن  مية
                 لما نعمل كشف حساب للمورد عيقع هكذا

                          حساب المورد            داين بالف وميتين وخمسين
                          حساب المورد                           مدين بميه
                          حساب المورد                           مدين بالف 
                         حساب المورد                    داين بميه وخمسين
                         هذا في حال كانت الفاتورة آجل عتبقى للمورد ميه وخمسين
                         ونفس الخبر لحساب الخزنة عتوقع الخزنة متدينه بمية وخمسين

                 ملاحظة اذا نشتي نعرف اذا حقي المحاسبة تمام يجب ان يكون اجمالي التكلفة للاصناف
                 مساوي للحساب في المخزن inventory
                 وحساب المصروف عنحملها في حساب المخزن لان احنا قد وزعنا المصروف على الاصناف
                 */
                //هذا اذا كانت الفاتورة موجودة ضروري نعمل حذف للاولات عشان مايجيش ثنتين فواتير متكررات

                //في فاتورة المبيعات عيوقع العكس اي ان الدائن المخزن والمدين العميل والحركات 
                //نفس الحركات بتغييرات بسيطة لكن هناك قيد زيادة وهو 
                //         حساب المخزون             دائن بقيمة الفاتورة
                //         حساب تكلفة البضاعة المباعة             مدين بقيمة الفاتورة
                //      حساب خصم نقدي مسموح به                   مدين بقيمة الخصم
                //      حساب العميل                   داين بقيمة الخصم
                //باقي معانا قيد اذا كان المستخدم بيبيع بضاعة من مخزن غير المخزن الحالي
                //فضروري قبل ما نبيع ننقل البضاعة من المخزن ذي نشتي نبيع منه للمخزن الحالي
                // بحيث يكون المخزن الحالي مدين بيقيمة البضاعة والمخزن ذي حولنا منه دائن بيقمة البضاعة
                // بحيث يكون المخزن الحالي مدين بيقيمة البضاعة والمخزن ذي حولنا منه دائن بيقمة البضاعة


                Delete_Coupe_Of_Accounts(invc.ID, Convert.ToByte(bill_Type));
                int PartAccountID = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Customer_Supplier>(
                    @"Select * From Customer_Supplier 
                            Where ID = @id", new { id = invc.Person_ID }).SingleOrDefault().Account_ID;
                var storeIDs = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Store>(
                    @"Select * From Store 
                            Where ID = @id", new { id = invc.Store_ID }).SingleOrDefault();
                var drawerID = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Drawer>(
                    @"Select * From Drawer 
                            Where ID = @id", new { id = invc.Drawer_ID }).SingleOrDefault();
                int storeAccount;
                int taxAccount;
                int partAccountID;
                int DiscountAccount;
                bool isPartIDCredit;
                bool isIN;//هل صادرة ام واردة
                bool insertCostOfSoldGoodJornal;//يعني هل شاعمل القيود الاثنين ذي طالع ولا لا
                byte move_Name;
                string discountMsg = "- خصم ";
                partAccountID = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Customer_Supplier>(
                    @"Select * From Customer_Supplier 
                            Where ID = @id", new { id = invc.Person_ID }).FirstOrDefault().Account_ID;
                string msg;
                switch (bill_Type)
                {
                    case bill_Type.Buy:
                        //هذا حساب المخزون حق المخزن ذي اشتيرنا ليه او بعنا منه
                        storeAccount = storeIDs.Inventory_Account_ID;
                        taxAccount = Session.Default.Buy_Tax_Account_ID;
                        DiscountAccount = Session.Default.Discount_Received_Account_ID;
                        isPartIDCredit = true;
                        isIN = true;
                        insertCostOfSoldGoodJornal = false;//لان العملية شراء مابش فيها تكلفة البضاعة المباعة
                        msg = $"فاتورة مشتريات رقم {invc.Code} لعميل /مورد {PartIDGridLokUpEdt.Text}";
                        move_Name = Convert.ToByte(Enum_Choices.Account_Move_Name.Purchase_Bill);
                        discountMsg += "شراء -";
                        break;
                    case bill_Type.Sale:
                        storeAccount = storeIDs.Sales_Account_ID;
                        taxAccount = Session.Default.Sales_Tax_Account_ID;
                        DiscountAccount = Session.Default.Discount_Allowed_Accoun_ID;
                        isPartIDCredit = false;
                        isIN = false;
                        insertCostOfSoldGoodJornal = true;
                        msg = $"فاتورة مبيعات رقم {invc.Code} لعميل /مورد {PartIDGridLokUpEdt.Text}";
                        move_Name = Convert.ToByte(Enum_Choices.Account_Move_Name.Sales_Bill);
                        discountMsg += "بيع -";
                        break;
                    case bill_Type.BuyReturn:
                        storeAccount = storeIDs.Inventory_Account_ID;
                        taxAccount = Session.Default.Buy_Tax_Account_ID;
                        DiscountAccount = Session.Default.Discount_Received_Account_ID;
                        isPartIDCredit = false;
                        isIN = false;
                        insertCostOfSoldGoodJornal = false;//لان العملية شراء مابش فيها تكلفة البضاعة المباعة
                        msg = $"فاتورة مرتجع مشتريات رقم {invc.Code} لعميل /مورد {PartIDGridLokUpEdt.Text}";
                        move_Name = Convert.ToByte(Enum_Choices.Account_Move_Name.Purchase_Return_Bill);
                        discountMsg += "مرتجع شراء -";
                        break;
                    case bill_Type.SaleReturn:
                        storeAccount = storeIDs.Sales_Return_Account_ID;
                        taxAccount = Session.Default.Sales_Tax_Account_ID;
                        DiscountAccount = Session.Default.Discount_Allowed_Accoun_ID;
                        isPartIDCredit = true;
                        isIN = true;
                        insertCostOfSoldGoodJornal = false;
                        msg = $"فاتورة مرتجع مبيعات رقم {invc.Code} لعميل /مورد {PartIDGridLokUpEdt.Text}";
                        move_Name = Convert.ToByte(Enum_Choices.Account_Move_Name.Sales_Return_Bill);
                        discountMsg += "مرتجع بيع -";
                        break;
                    default:
                        throw new NotImplementedException();
                }
                coupAccount.Add(new DBModels.Coupes_Of_Account()//part custo/suppl
                {
                    Account_ID = PartAccountID,
                    Code = "5",
                    Credit = (isPartIDCredit) ? invc.Total + invc.Tax_Val + invc.Expences : 0,//داين
                    Debit = (!isPartIDCredit) ? invc.Total + invc.Tax_Val + invc.Expences : 0,
                    Insert_Date = invc.Date,
                    Notes = msg,
                    Source_ID = invc.ID,
                    Source_Type = (byte)bill_Type,
                    Move_Name = move_Name,
                    User_ID = Session.user.ID
                });
                coupAccount.Add(new DBModels.Coupes_Of_Account()//inventory store
                {
                    Account_ID = storeAccount,
                    Code = "5",
                    Credit = (!isPartIDCredit) ? invc.Total + invc.Expences : 0,//داين
                    Debit = (isPartIDCredit) ? invc.Total + invc.Expences : 0,// مدين بقيمة الفاتورةوالمصروفات
                    Insert_Date = invc.Date,
                    Notes = msg,
                    Source_ID = invc.ID,
                    Source_Type = (byte)bill_Type,
                    Move_Name = move_Name,
                    User_ID = Session.user.ID
                });
                if (invc.Tax_Val > 0)
                    coupAccount.Add(new DBModels.Coupes_Of_Account()//Tax store
                    {
                        Account_ID = taxAccount,
                        Code = "5",
                        Credit = (!isPartIDCredit) ? invc.Tax_Val : 0,//داين
                        Debit = (isPartIDCredit) ? invc.Tax_Val : 0,//مدين بقيمة الضريبة
                        Insert_Date = invc.Date,
                        Notes = msg + "- ضريبة مضافة -",
                        Source_ID = invc.ID,
                        Source_Type = (byte)bill_Type,
                        Move_Name = move_Name,
                        User_ID = Session.user.ID
                    });
                if (invc.Discount_Val > 0)
                {
                    coupAccount.Add(new DBModels.Coupes_Of_Account()//discount store
                    {
                        Account_ID = DiscountAccount,
                        Code = "5",
                        Credit = (!isPartIDCredit) ? invc.Discount_Val : 0,//داين
                        Debit = (isPartIDCredit) ? invc.Discount_Val : 0,//مدين بخصم
                        Insert_Date = invc.Date,
                        Notes = msg + discountMsg,
                        Source_ID = invc.ID,
                        Source_Type = (byte)bill_Type,
                        Move_Name = move_Name,
                        User_ID = Session.user.ID
                    });
                    coupAccount.Add(new DBModels.Coupes_Of_Account()//discount store
                    {
                        Account_ID = PartAccountID,
                        Code = "5",
                        Credit = (isPartIDCredit) ? invc.Discount_Val : 0,//داين
                        Debit = (!isPartIDCredit) ? invc.Discount_Val : 0,//مدين بخصم
                        Insert_Date = invc.Date,
                        Notes = msg + discountMsg,
                        Source_ID = invc.ID,
                        Source_Type = (byte)bill_Type,
                        Move_Name = move_Name,
                        User_ID = Session.user.ID
                    });
                }
                if (insertCostOfSoldGoodJornal)//حساب تكلفة البضاعة المباعة
                {
                    //هنا نتاكد ان به بضاعة من مخزن ثاني ولا لا
                    if (gridData.Where(x => x.Store_ID != invc.Store_ID).Count() > 0)
                    {
                        var otherStores = gridData.Where(x => x.Store_ID != invc.Store_ID).Select(x => x.Store_ID).Distinct();
                        //حساب المخزون الاخر عيوقع دائن
                        //حساب المخزون الخاص بالفرع او  الحالي عيوقع مدين
                        foreach (var item in otherStores)
                        {
                            var totCost = gridData.Where(x => x.Store_ID == item).Sum(x => x.Total_Cost);
                            coupAccount.Add(new DBModels.Coupes_Of_Account()//other store
                            {//حساب المخزون للفرع او المخزن الحالي مدين بقيمة البضاعة المنقولة
                                Account_ID = storeIDs.Inventory_Account_ID,
                                Code = "5",//temp
                                Credit = (isPartIDCredit) ? totCost : 0,//داين
                                Debit = (!isPartIDCredit) ? totCost : 0,//مدين بالمدفوع
                                Insert_Date = invc.Date,
                                Notes = msg + "- نقل بضاعة للبيع -",
                                Source_ID = invc.ID,
                                Source_Type = (byte)bill_Type,
                                Move_Name = move_Name,
                                User_ID = Session.user.ID
                            });
                            coupAccount.Add(new DBModels.Coupes_Of_Account()//inventory
                            {//هنا بحيث يكون حساب المخزون للفرع او المخزن الثاني دائن
                                Account_ID = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Store>(
                                            @"Select * From Store 
                                                         Where ID = @id", 
                                            new 
                                            {
                                                id = item 
                                            }).FirstOrDefault().Inventory_Account_ID,
                                Code = "5",//temp
                                Credit = (!isPartIDCredit) ? totCost : 0,//داين
                                Debit = (isPartIDCredit) ? totCost : 0,//مدين بالمدفوع
                                Insert_Date = invc.Date,
                                Notes = msg + "- نقل بضاعة للبيع -",
                                Source_ID = invc.ID,
                                Source_Type = (byte)bill_Type,
                                Move_Name = move_Name,
                                User_ID = Session.user.ID
                            });
                        }
                    }
                    double totalCost = gridData.Sum(x => x.Total_Cost);
                    coupAccount.Add(new DBModels.Coupes_Of_Account()
                    {
                        Account_ID = storeIDs.Inventory_Account_ID,
                        Code = "5",//temp
                        Credit = (!isPartIDCredit) ? totalCost : 0,//داين
                        Debit = (isPartIDCredit) ? totalCost : 0,//مدين بالمدفوع
                        Insert_Date = invc.Date,
                        Notes = msg + "- تكلفة البضاعة المباعة -",
                        Source_ID = invc.ID,
                        Source_Type = (byte)bill_Type,
                        Move_Name = move_Name,
                        User_ID = Session.user.ID
                    });
                    coupAccount.Add(new DBModels.Coupes_Of_Account()//SolsgoodCost
                    {
                        Account_ID = storeIDs.Cost_Of_Sold_Good_Account_ID,
                        Code = "5",//temp
                        Credit = (isPartIDCredit) ? totalCost : 0,//داين
                        Debit = (!isPartIDCredit) ? totalCost : 0,//مدين بالمدفوع
                        Insert_Date = invc.Date,
                        Notes = msg + "- تكلفة البضاعة المباعة -",
                        Source_ID = invc.ID,
                        Source_Type = (byte)bill_Type,
                        Move_Name = move_Name,
                        User_ID = Session.user.ID
                    });
                }
                if (invc.Paid > 0)
                {
                    coupAccount.Add(new DBModels.Coupes_Of_Account()//part paid
                    {
                        Account_ID = PartAccountID,
                        Code = "5",//temp
                        Credit = (!isPartIDCredit) ? invc.Paid : 0 ,//داين
                        Debit = (isPartIDCredit) ? invc.Paid : 0,//مدين بالمدفوع
                        Insert_Date = invc.Date,
                        Notes = msg + ((isPartIDCredit) ? "- سداد شراء -" : "- سداد بيع -"),
                        Source_ID = invc.ID,
                        Source_Type = (byte)bill_Type,
                        Move_Name = move_Name,
                        User_ID = Session.user.ID
                    });
                    coupAccount.Add(new DBModels.Coupes_Of_Account()//part paid
                    {
                        Account_ID = drawerID.Account_ID,
                        Code = "5",//temp
                        Credit = (isPartIDCredit) ? invc.Paid : 0,//داين
                        Debit = (!isPartIDCredit) ? invc.Paid : 0,//مدين بالمدفوع
                        Insert_Date = invc.Date,
                        Notes = msg + ((isPartIDCredit) ? "- سداد شراء -" : "- سداد بيع -"),
                        Source_ID = invc.ID,
                        Source_Type = (byte)bill_Type,
                        Move_Name = move_Name,
                        User_ID = Session.user.ID
                    });
                }
                var coupes = new
                {
                    coupes = Convert_Coupes_List_To_Table(coupAccount).AsTableValuedParameter("Account_Coupes_List_Type")
                };
                DAL.Impelement_Stored_Procedure.Excute_Proce("Add_List_Of_Coupes", coupes, isCommandText: false);
                #endregion
               
                Delete_Pro_Store_Move_Details_For_Invoices(invc.ID, bill_Type);
                Delete_Invoice_Details(invc.ID);
                string insertCommand = @"Insert Into [dbo].[Invoice_Details]
                              ([Invoice_ID]
                              ,[Product_ID]
                              ,[Unit_ID]
                              ,[Qty]
                              ,[Price]
                              ,[Total_Price]
                              ,[Cost]
                              ,[Total_Cost]
                              ,[Discount_Val]
                              ,[Discount_Prece]
                              ,[Store_ID]
                              )
                              Values (
                              @invID,@prdID,@unitID,@qty,@prc,@totprc,@cost,@totcost,@disv,@disp,@strID)";
                if (bill_Type == bill_Type.BuyReturn||bill_Type== bill_Type.SaleReturn)
                {
                    insertCommand = @"Insert Into [dbo].[Invoice_Details]
                              ([Invoice_ID]
                              ,[Product_ID]
                              ,[Unit_ID]
                              ,[Qty]
                              ,[Price]
                              ,[Total_Price]
                              ,[Cost]
                              ,[Total_Cost]
                              ,[Discount_Val]
                              ,[Discount_Prece]
                              ,[Store_ID]
                              ,[Source_Back_Row_ID]
                              )
                              Values (
                              @invID,@prdID,@unitID,@qty,@prc,@totprc,@cost,@totcost,@disv,@disp,@strID,@retID)";
                }
                foreach (var row in gridData)//todo its temp till we add return invoice
                {
                    if (bill_Type == bill_Type.BuyReturn || bill_Type == bill_Type.SaleReturn)
                    {
                        row.ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>(
                       insertCommand,
                       new
                       {
                           invID = invc.ID,
                           prdID = row.Product_ID,
                           unitID = row.Unit_ID,
                           qty = row.Qty,
                           prc = row.Price,
                           totprc = row.Total_Price,
                           cost = row.Cost,
                           totcost = row.Total_Cost,
                           disv = row.Discount_Val,
                           disp = row.Discount_Prece,
                           strID = row.Store_ID,
                           retID = row.Source_Back_Row_ID
                       }).SingleOrDefault().ID;
                    }
                    else
                    {
                        row.ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>(
                        insertCommand,
                        new
                        {
                            invID = invc.ID,
                            prdID = row.Product_ID,
                            unitID = row.Unit_ID,
                            qty = row.Qty,
                            prc = row.Price,
                            totprc = row.Total_Price,
                            cost = row.Cost,
                            totcost = row.Total_Cost,
                            disv = row.Discount_Val,
                            disp = row.Discount_Prece,
                            strID = row.Store_ID,
                        }).SingleOrDefault().ID;
                    }
                }
                if (invc.Is_Posted_To_Store)
                {
                    foreach (var row in gridData)
                    {
                        var unitV = Session.Full_products.Single(x => x.ID == row.Product_ID).
                                    pro_Unit.Single(x => x.Unit_ID == row.Unit_ID);
                        moveProStore.Add(new DBModels.Pro_Store_Movement()
                        {
                            Product_ID = row.Product_ID,
                            Insert_Date = invc.Post_Date.Value,
                            Source_ID = row.ID,
                            Source_Type = (byte)bill_Type,
                            Notes = msg,//في حالة الشرء ترو وحالة البيع فولس
                            Import_Qty = (isIN) ? (row.Qty * unitV.Factor) : 0,
                            Export_Qty = (!isIN) ? (row.Qty * unitV.Factor) : 0,
                            Store_ID = row.Store_ID,
                            Cost_Value = row.Cost / unitV.Factor,
                            User_ID = Session.user.ID
                        });
                    }
                    var proMove = new
                    {
                        proMove = Convert_ProMove_List_To_Table(moveProStore).
                        AsTableValuedParameter("Pro_Movement_List_Type")
                    };
                    DAL.Impelement_Stored_Procedure.Excute_Proce(
                        "[FinalSalesDB].[dbo].[Add_List_Of_Pro_Movement]", proMove, isCommandText: false);
                }
                the_Changed_Source_ID = invc.ID;
                the_Changed_Source_Name = invc.Code + " - " + PartIDGridLokUpEdt.Text;
                //هذا إذا كان واحد فاتح القوايم حق الفواتير يحدث محتوياتها
                var forms = Application.OpenForms.Cast<Form>().Where(x => x.Name == this.Name + "_Bill_List").ToList();
                foreach (var frm in forms)
                {
                    if (frm != null && frm is Frm_Master)
                        ((Frm_Master)frm).Impelment_RefreshData();
                }
                BillDetailsGridView.RefreshData();
            }
            base.Save();
        }
        public override void Print()
        {
            if (invc.ID <= 0) return;
            Print(invc.ID, bill_Type, this.Name);
            //base.Print();
        }
        public static void Print(int id, bill_Type type, string callerScreenName)
        {
            Print(new List<int> { id }, type, callerScreenName);
        }
        public static void Print(List<int> ids, bill_Type type, string callerScreenName)
        {
            string name = "";
            switch (type)
            {
                case bill_Type.Buy:
                    name = Screens.add_Buy_Bill.Screen_Name;
                    break;
                case bill_Type.Sale:
                    name = Screens.add_Sale_Bill.Screen_Name;
                    break;
                case bill_Type.BuyReturn:
                    name = Screens.add_Buy_Return_Bill.Screen_Name;
                    break;
                case bill_Type.SaleReturn:
                    name = Screens.add_Sale_Return_Bill.Screen_Name;
                    break;
                default:
                    throw new NotImplementedException();
            }
            if (Is_Authorized(name, Screen_Actions.Print) == false)
                return;
            var invIDs = new
            {
                invIDs = Convert_List_To_DataTable.Convert_ID_List_To_Table(ids).
               AsTableValuedParameter("[dbo].[IDs_List_Type]")
            };
            var Data = DAL.Impelement_Stored_Procedure.Get_The_Full_Invoice_For_Print(
                "[FinalSalesDB].[dbo].[Get_Full_Invoice_To_Print]", invIDs);
            Data.ForEach(x =>
            {
                Frm_Master.Insert_User_Action(User_Actions.Print, x.ID, x.Code + " - " + x.PersonName, callerScreenName);
            });
            Reports.Rpt_Invoices.Print(Data, type);
        }
        protected override void Delete()
        {
            string msg;
            if (!Delete(invc.ID, out msg))
            {
                XtraMessageBox.Show(msg, "فشل الحذف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            NewBtn.PerformClick();
            base.Delete();
        }
        /// <summary>
        /// this is the main thing in it
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool Delete(int id, out string msg)
        {
            if (DAL.Impelement_Stored_Procedure.SelectData<DBModels.Invoice>(
                @"Select ID From Invoice Where Invoice_Back_ID = @bID",
                new { bID = id }).Count() > 0)
            {
                msg = $"لا يمكن حذف هذه الفاتورة رقم {id} لأنها مرتبطة بمصادر أخرى";
                return false;
            }
            DBModels.Invoice invoice = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Invoice>(
                @"Select ID From Invoice Where ID = @id",
                new { id = id }).FirstOrDefault();
            Delete_Pro_Store_Move_Details_For_Invoices(id, (bill_Type)invoice.Type);
            Delete_Coupe_Of_Accounts(id, Convert.ToByte(invoice.Type));
            Delete_Invoice_Details(id);
            DAL.Impelement_Stored_Procedure.Excute_Proce(
                @"Delete From Invoice Where ID = @id",
                new { id = id });
            msg = $"تم حذف الفاتورة رقم {id} بنجاح";
            return true;
        }
        public static void Delete(List<int> ids, bill_Type type, string callerScreenName)
        {
            string name = "";
            switch (type)
            {
                case bill_Type.Buy:
                    name = Screens.add_Buy_Bill.Screen_Name;
                    break;
                case bill_Type.Sale:
                    name = Screens.add_Sale_Bill.Screen_Name;
                    break;
                case bill_Type.BuyReturn:
                    name = Screens.add_Buy_Return_Bill.Screen_Name;
                    break;
                case bill_Type.SaleReturn:
                    name = Screens.add_Sale_Return_Bill.Screen_Name;
                    break;
                default:
                    throw new NotImplementedException();
            }
            if (Is_Authorized(name, Screen_Actions.Delete) == false)
                return;
            if (!Ask_For_Delete())
                return;
            var billView =
               (
                from invc in DAL.Impelement_Stored_Procedure.SelectData<DBModels.Invoice>(
                    @"Select ID ,Code , Person_ID From Invoice")
                from per in DAL.Impelement_Stored_Procedure.SelectData<DBModels.Customer_Supplier>(
                    @"Select ID ,Name From Customer_Supplier").Where(x => x.ID == invc.Person_ID).DefaultIfEmpty()
                where ids.Contains(invc.ID)
                select new
                {
                    invc.ID,
                    invc.Code,
                    per.Name
                }).ToList();
            string wholemsg = "";
            foreach (var item in billView)
            {
                string msg;
                if (Delete(item.ID, out msg))
                {
                    Frm_Master.Insert_User_Action(User_Actions.Delete, item.ID, item.Code + " - " + item.Name, callerScreenName);
                }
                wholemsg += msg + Environment.NewLine;
            }
            XtraMessageBox.Show(wholemsg, "تأكيد الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// في هذا الحدث بينعبي بيانات العميل او المورد
        /// وفي حالة المرتجع بينعبي بيانات فاتورة او كم معه فواتير
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PartIDGridLokUpEdt_EditValueChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(PartIDGridLokUpEdt.EditValue);
            if (id > 0)
            {
                DBModels.Customer_Supplier account;
                if (PartTypelokUpEdt.EditValue.Equals((byte)Part_Type.Supplier))
                {
                    account = Session.Suppliers.SingleOrDefault(x => x.ID == id);
                }
                else
                {
                    account = Session.Customers.SingleOrDefault(x => x.ID == id);
                }//todo
                if (account == null) goto EMPTYACC;
                PartAddressTxt.Text = account.Address;
                PartPhoneTxt.Text = account.Mobile;
                MaxMoneySpn.EditValue = account.Max_Credit;
                account_Balance = Get_Account_Balance(account.Account_ID);
                CurrentBalanceTxt.Text = account_Balance.Balance_Note;
                if (bill_Type == bill_Type.SaleReturn || bill_Type == bill_Type.BuyReturn)
                {
                    var sourceBill = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Invoice>(
                        @"Select ID ,Code From Invoice
                                         Where Type = @type And Person_Type = @partType 
                                         And Person_ID = @partID",
                                     new
                                     {
                                         type = ((bill_Type == bill_Type.SaleReturn) ? (byte)bill_Type.Sale : (byte)bill_Type.Buy),
                                         partType = (byte)PartTypelokUpEdt.EditValue,
                                         partID = id
                                     }).Select(x => new { x.ID, x.Code }).ToList();
                    BillSourceIDGrdLkp.Initilaze_Data(sourceBill, "ID", "Code");
                    BillSourceIDGrdLkp.Properties.View.Columns["ID"].Visible = true;
                    BillSourceIDGrdLkp.Properties.View.Columns["ID"].Caption = "الرقم";
                    BillSourceIDGrdLkp.Properties.View.Columns["Code"].Caption = "الكود";
                    BillSourceIDGrdLkp.EditValue = null;
                }
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
        /// <summary>
        /// هذا الحدث يدي اسم العميل او المورد
        /// بمجرد تغير قيمة طرف التعامل
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PartTypelokUpEdt_EditValueChanged(object sender, EventArgs e)
        {
            if (PartTypelokUpEdt.Is_The_Lkp_Edit_Value_Of_Type_Int())
            {
                byte type = Convert.ToByte(PartTypelokUpEdt.EditValue);
                if (type == (byte)Part_Type.Customer)
                {
                    PartIDGridLokUpEdt.Initilaze_Data(Session.Customers);
                    PartIDGridLokUpEdt.EditValue = Session.Default.CustomerID;
                }
                else if (type == (byte)Part_Type.Supplier)
                {
                    PartIDGridLokUpEdt.Initilaze_Data(Session.Suppliers);
                    PartIDGridLokUpEdt.EditValue = Session.Default.SupplierID;
                }
                PartIDGridLokUpEdt.Properties.View.PopulateColumns(PartIDGridLokUpEdt.Properties.DataSource);
                PartIDGridLokUpEdt.Properties.View.Grid_View_Translate_Column("Person");
            }
        }
        #region MoneyCalc
        /// <summary>
        /// هذا الحدث يغير قيمة المدفوع اذا المستخدم ما قد غير المدفوع
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NetSpnEdt_EditValueChanging(object sender, ChangingEventArgs e)
        {

            if (bill_Type == bill_Type.Sale && Session.Current_User_Settings_Prope.Sale_Settings.DefualtPayMethodInSales == Pay_Mode.Cash)
            {
                if (Convert.ToDouble(e.OldValue) == Convert.ToDouble(PaidSpnEdt.EditValue))
                {
                    PaidSpnEdt.EditValue = Convert.ToDouble(e.NewValue);
                }
            }
            else if (bill_Type == bill_Type.Sale && Session.Current_User_Settings_Prope.Sale_Settings.DefualtPayMethodInSales == Pay_Mode.Credit)
            {
                PaidSpnEdt.EditValue = 0;
            }
        }
        /// <summary>
        /// هذا الحدث يحسب نسبة الضريبة
        /// وقيمة الضريبة بناء على المتغير 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaxValueSpnEdt_EditValueChanged(object sender, EventArgs e)
        {
            double total = Convert.ToDouble(TotalSpnEdt.EditValue);
            double taxValue = Convert.ToDouble(TaxValueSpnEdt.EditValue);
            double taxRotat = Convert.ToDouble(TaxRotationSpnEdt.EditValue);
            if (is_Tax_ValueFocused)
            {
                TaxRotationSpnEdt.EditValue = (taxValue / total);
            }
            else
            {
                TaxValueSpnEdt.EditValue = (taxRotat * total);
            }
        }
        /// <summary>
        /// هذا الحدث عشان اذا ضغط المستخدم مرتين ينقل قيمته للمدفوع
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NetSpnEdt_DoubleClick(object sender, EventArgs e)
        {
            PaidSpnEdt.EditValue = NetSpnEdt.EditValue;
        }
        /// <summary>
        /// هذا الحدث يحسب نسبة الخصم وقيمته بناء على المتغير
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DiscountValueSpnEdt_EditValueChanged(object sender, EventArgs e)
        {
            var total = Convert.ToDouble(TotalSpnEdt.EditValue);
            var discountVal = Convert.ToDouble(DiscountValueSpnEdt.EditValue);
            var discountRotat = Convert.ToDouble(DiscountRotationSpnEdt.EditValue);
            if (is_Discount_ValueFocused)
            {
                DiscountRotationSpnEdt.EditValue = (discountVal / total);
            }
            else
            {
                DiscountValueSpnEdt.EditValue = (discountRotat * total);
            }
        }
        /// <summary>
        /// هذا الحدث يعرض شاشة الموردين اذا تم الضغط على زر الزايد
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PartIDGridLokUpEdt_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Plus)
            {
                if (PartTypelokUpEdt.EditValue == null) return;
                bool isCustomer = Convert.ToByte(PartTypelokUpEdt.EditValue) == (byte)Part_Type.Customer;
                var newPersonId = Frm_Add_Customer_Supplier.Add_New_Person(isCustomer);
                if (newPersonId <= 0) return;
                PartIDGridLokUpEdt.EditValue = newPersonId;
                //Refresh_Data();
            }
        }
        private bool is_Discount_ValueFocused;
        private void DiscountValueSpnEdt_Leave(object sender, EventArgs e)
        {
            is_Discount_ValueFocused = false;
        }
        private void DiscountValueSpnEdt_Enter(object sender, EventArgs e)
        {
            is_Discount_ValueFocused = true;
        }
        bool is_Tax_ValueFocused;
        private void TaxValueSpnEdt_Enter(object sender, EventArgs e)
        {
            is_Tax_ValueFocused = true;
        }
        private void TaxValueSpnEdt_Leave(object sender, EventArgs e)
        {
            is_Tax_ValueFocused = false;
        }
        /// <summary>
        /// هذا الحدث يحسب الصافي
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calculate_Sum_Net(object sender, EventArgs e)
        {
            var total = Convert.ToDouble(TotalSpnEdt.EditValue);
            TaxValueSpnEdt_EditValueChanged(sender, e);
            DiscountValueSpnEdt_EditValueChanged(sender, e);
            var taxValue = Convert.ToDouble(TaxValueSpnEdt.EditValue);
            var discountVal = Convert.ToDouble(DiscountValueSpnEdt.EditValue);
            var expences = Convert.ToDouble(ExpencesSpnEdt.EditValue);
            NetSpnEdt.EditValue = total + taxValue + expences - discountVal;
        }
        /// <summary>
        /// هذا الحدث يحسب المتبقي
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Calculate_Remaining(object sender, EventArgs e)
        {
            var net = Convert.ToDouble(NetSpnEdt.EditValue);
            var paid = Convert.ToDouble(PaidSpnEdt.EditValue);
            RemainingSpnEdt.EditValue = net - paid;
        }
        #endregion
        /// <summary>
        /// هنا نفتح للمستخدم شاشة إضافة صنف
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region GridViewEvents
        private void BillDetailsGridView_MouseLeave(object sender, EventArgs e)
        {
            //BillCodeTxt.Focus();
        }
        /// <summary>
        /// هذا الحدث بيعالج ضغطة الزر قبل ما يسير القريد فيو
        /// وعنعالج فيه اذا ضغط المستخدم انتر يضيف السطر وينتقل الى واحد جديد
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BillDetailsGridCtrl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            GridControl control = sender as GridControl;
            if (control == null) return;
            GridView view = control.FocusedView as GridView;
            if (view == null) return;
            if (view.FocusedColumn == null) return;
            BillDetailsGridView_ValidateRow(null, new ValidateRowEventArgs(
                view.FocusedRowHandle, view.GetRow(view.FocusedRowHandle)));
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                string focusedColumn = view.FocusedColumn.FieldName;
                if (view.FocusedRowHandle < 0)
                {
                    if (BillDetailsGridView.HasColumnErrors)
                        return;
                    view.AddNewRow();
                    view.FocusedColumn = view.Columns[focusedColumn];
                }
                if (view.FocusedColumn.FieldName == "Code" || view.FocusedColumn.FieldName == nameof(invcDetail.Product_ID))
                {
                    BillDetailsGridCtrl_ProcessGridKey(sender, new KeyEventArgs(Keys.Tab));
                }
                e.Handled = true;
            }
            //هنا بنعمل حدث يدوي عشان يضيف الصنف لانو هذا الحدث بيتفعل قبل القريد فيو فلهذا 
            //بنعمل حدث ونخليه يسير التاب وبهدها يرجع ينفذ حق الانتر
            //طبعا المتغير عملنا عشان يعمل فوكس على العمود ذي تفعل الحدث منه اذا كان كود او صنف
            else if (e.KeyCode == Keys.Tab)
            {
                view.FocusedColumn = view.VisibleColumns[view.FocusedColumn.VisibleIndex - 1];
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (view.FocusedRowHandle >= 0)
                {
                    if (Session.Current_User_Settings_Prope.Invoice_Settings.CanChangeDeleteItemsInBills == false && invc.ID != 0)
                    {
                        XtraMessageBox.Show(" غير مصرح لك حذف عناصر من الفاتورة ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    view.DeleteSelectedRows();
                }
            }
            //BillDetailsGridView.RefreshData();
        }
        /// <summary>
        ///هذا اذا كان به فواتير مرتجع يعمل شو ماشي يخفيها
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnedBillsGrdViw_RowCountChanged(object sender, EventArgs e)
        {
            ReturnedBillsLytCG.Visibility =
                (ReturnedBillsGrdViw.RowCount > 0) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        /// <summary>
        /// هذا الحدث عشان نعالج الرسالة ذي بتظهر كل ما وقع خطا في القريد فيو
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BillDetailsGridView_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            if (e.Row == null || (e.Row as DBModels.Invoice_Details).Product_ID == 0)
            {
                BillDetailsGridView.DeleteRow(e.RowHandle);
                e.ExceptionMode = ExceptionMode.Ignore;
                return;
            }
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        /// <summary>
        /// هذا الحدث عشان نضمن ان المستخدم ما يدخلش صفوف فارغة
        /// وعشان مايتعداش حقه الصلاحيات
        /// وحاجات ثانية
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BillDetailsGridView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            var row = e.Row as DBModels.Invoice_Details;
            if (row == null || row.Product_ID == 0)
            {
                e.Valid = false;
                return;
            }
            switch (bill_Type)
            {
                case bill_Type.Buy:
                    break;
                case bill_Type.Sale:
                    if (row.Cost > 0)
                    {
                        if (row.Cost > row.Price)
                        {
                            switch (Session.Current_User_Settings_Prope.Sale_Settings.WhenSellingItemWithPriceLessThanCostPrice)
                            {
                                case Warining_Handel.Do_Not_Interrupt:
                                    break;
                                case Warining_Handel.Show_Warning:
                                    if (XtraMessageBox.Show("سعر بيع المنتج أقل من سعر التكلفة هل تريد المتابعة ؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                                    {
                                        e.Valid = false;
                                        BillDetailsGridView.SetColumnError(BillDetailsGridView.Columns[nameof(row.Price)], "سعر بيع المنتج أقل من سعر التكلفة");
                                    }
                                    break;
                                case Warining_Handel.Prevent:
                                    e.Valid = false;
                                    BillDetailsGridView.SetColumnError(BillDetailsGridView.Columns[nameof(row.Price)], " لا يمكن المتابعة لأن سعر بيع المنتج أقل من سعر التكلفة");
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    if (Convert.ToDecimal(row.Discount_Prece) > Session.Current_User_Settings_Prope.Sale_Settings.MaxDiscountLevelPerItem)
                    {
                        e.Valid = false;
                        BillDetailsGridView.SetColumnError(BillDetailsGridView.Columns[nameof(row.Discount_Prece)], "نسبة الخصم أكبر من المسموح لك");
                    }
                    break;
                case bill_Type.SaleReturn:
                case bill_Type.BuyReturn:
                    double? returnedQty = BillDetailsGridView.GetRowCellValue(e.RowHandle, "ReturnedQty") as double?;
                    double? basicQty = BillDetailsGridView.GetRowCellValue(e.RowHandle, "SourceQty") as double?;
                    if (row.Qty > (((basicQty ?? 0) - (returnedQty) ?? 0)))
                    {
                        e.Valid = false;
                        BillDetailsGridView.SetColumnError(BillDetailsGridView.Columns["Qty"], "الكمية أكبر من الكمية الأساسية المتاحة");
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        /// <summary>
        /// هذا الحدث عشان يكن يظهر الكود عندما نكتبه في القريد فيو
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BillDetailsGridView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Code")
            {
                if (e.IsSetData)
                {
                    WrittenCode = e.Value.ToString();
                }
                else if (e.IsGetData)
                {
                    e.Value = WrittenCode;
                }
            }
            else if (e.Column.FieldName == "Balance")
            {
                var row = e.Row as DBModels.Invoice_Details;
                int rowIndx = e.ListSourceRowIndex;
                if (row == null || row.Product_ID <= 0 || row.Store_ID <= 0 || row.Unit_ID <= 0)
                {
                    e.Value = null;
                    return;
                }
                var proBalance = Session.Pro_Balance.
                    FirstOrDefault(x => x.Product_ID == row.Product_ID && x.Store_ID == row.Store_ID);
                if (proBalance == null)
                {
                    e.Value = 0;
                    return;
                }
                double balance = proBalance.Balance;
                var product = Session.Full_products.FirstOrDefault(x => x.ID == row.Product_ID);
                double factor = product.pro_Unit.FirstOrDefault(x => x.Unit_ID == row.Unit_ID).Factor;
                //هنا بنختار الاصناف ذي القريد فيو المتشابهة في الصنف والوحدة والمخزن
                //ونخرج حقها الكمية والرصيد وننقصها من الرصيد الاصلي
                var rows = (BillDetailsGridView.DataSource as BindingList<DBModels.Invoice_Details>).
                    Take(rowIndx).Where(x => x.Product_ID == row.Product_ID && x.Unit_ID == row.Unit_ID 
                                          && x.Store_ID == row.Store_ID);
                double otherBalance = 0;
                foreach (var item in rows)
                {
                    //هنا نخرج المعامل حق كل صف في القريد فيو ونضربه في الكمية حقه عشان نخرج كم معانا كمية
                    double otherFactor = product.pro_Unit.FirstOrDefault(x => x.Unit_ID == item.Unit_ID).Factor;
                    //هنا كان احنا بنرجع الرصيد على ماهو
                    otherBalance += item.Qty * otherFactor;
                }//هنا لان الوحدات هن سوا فلهذا قسمناهن في الاخير على المعامل ذي خرجناه طااالع
                e.Value = (balance - otherBalance) / factor;
            }
            else if (e.Column.FieldName == "SourceQty")
            {
                var returnedRow = e.Row as DBModels.Invoice_Details;
                if (returnedRow == null) return;
                if (e.IsGetData)
                {
                    var sourceRow = returnedSourceDetail.FirstOrDefault(x => x.ID == returnedRow.Source_Back_Row_ID);
                    if (sourceRow != null)
                        e.Value = sourceRow.Qty;
                    else
                        e.Value = 0;
                }
            }
            else if (e.Column.FieldName == "ReturnedQty")
            {
                DBModels.Invoice_Details returnedRow = e.Row as DBModels.Invoice_Details;
                if (returnedRow == null) return;
                if (e.IsGetData)
                {
                    //todo focuse here
                    var otherreturnedSourceRows = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Invoice_Details>(
                        @"Select * From Invoice_Details
                                   Where Source_Back_Row_ID = @backID And ID != @id", 
                        new 
                        { 
                            backID = returnedRow.Source_Back_Row_ID, 
                            id = returnedRow.ID 
                        }).Sum(x => (double?)x.Qty) ?? 0;
                    e.Value = otherreturnedSourceRows;
                }
            }
        }
        /// <summary>
        /// هذا الحدث عشان يغير الفروع او المخازن ذي تشبهه في القريد فيو
        /// ملاحظة المخازن والفروع هن نفس الشيء
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BranchlokUpEdt_EditValueChanging(object sender, ChangingEventArgs e)
        {
            var items = BillDetailsGridView.DataSource as BindingList<DBModels.Invoice_Details>;
            if (e.OldValue is int && e.NewValue is int)
                foreach (var item in items)
                {
                    if (item.Store_ID == ((int?)e.OldValue ?? 0))
                        item.Store_ID = Convert.ToInt32(e.NewValue);
                }
        }
        /// <summary>
        /// هذا الحدث يجمع قيمة إجمالي السعر ذي في القريد فيو ويحطه في
        /// إجمالي القيمة حق الفاتورة
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BillDetailsGridView_RowUpdated(object sender, RowObjectEventArgs e)
        {
            //if (e == null) return;
            //var row = BillDetailsGridView.GetRow(e.RowHandle) as DBModels.Invoice_Details;
            //if (row == null) return;
            //var data = BillDetailsGridView.DataSource as BindingList<DBModels.Invoice_Details>;
            //if (data == null) return;
            //var simRow = data.FirstOrDefault(x => x.Product_ID == row.Product_ID
            //                        && x.Store_ID == row.Store_ID
            //                        && x.Unit_ID == row.Unit_ID);
            //if (simRow != null)
            //{
            //    int indx = data.IndexOf(data.FirstOrDefault(x => x.Product_ID == row.Product_ID
            //                        && x.Store_ID == row.Store_ID
            //                        && x.Unit_ID == row.Unit_ID));
            //    data.RemoveAt(indx);
            //    simRow.Qty += row.Qty;
            //    data.Insert(indx, simRow);
            //}
            if (currentRowPos <= BillDetailsGridView.RowCount)
            {
                //هنا يدي لي الصفوف المتشابهه في السعر والوحدة والمخزن ويجمعهن لي
                var rows = (BillDetailsGridView.DataSource as BindingList<DBModels.Invoice_Details>);
                var lastRow = rows.LastOrDefault();
                var row = rows.FirstOrDefault(x => x.Product_ID == lastRow.Product_ID
                && x.Store_ID == lastRow.Store_ID && x.Price == lastRow.Price
                && x.Unit_ID == lastRow.Unit_ID && x != lastRow);
                if (row != null)
                {
                    row.Qty += lastRow.Qty;
                    //هنا عملنا داله ترجع لنا الصف ذي اضفنا فيه الكمية عشان نعدل السعر
                    BillDetailsGridView_CellValueChanged(sender, new
                        CellValueChangedEventArgs(BillDetailsGridView.Find_Handel_Row_By_Object(row)
                        , BillDetailsGridView.Columns[nameof(invcDetail.Qty)], row.Qty));
                    rows.Remove(lastRow);
                }
            }
            var items = BillDetailsGridView.DataSource as BindingList<DBModels.Invoice_Details>;
            if (items == null)
                TotalSpnEdt.EditValue = 0;
            else
                TotalSpnEdt.EditValue = items.Sum(x => x.Total_Price);
        }
        private int currentRowPos = 0;
        /// <summary>
        /// هذا الحدث يستدعي الحدث ذي فوقه وعنستخدمه لكي 
        /// نضيف الكمية لاثنين اصناف متشابهين يعني لاشي اثنين اصناف 
        /// متشابهين نضيفه فوق الصنف الاول
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BillDetailsGridView_RowCountChanged(object sender, EventArgs e)
        {
            //if (currentRowPos <= BillDetailsGridView.RowCount)
            //{
            //    //هنا يدي لي الصفوف المتشابهه في السعر والوحدة والمخزن ويجمعهن لي
            //    var rows = (BillDetailsGridView.DataSource as BindingList<DBModels.Invoice_Details>);
            //    var lastRow = rows.LastOrDefault();
            //    var row = rows.FirstOrDefault(x => x.Product_ID == lastRow.Product_ID
            //    && x.Store_ID == lastRow.Store_ID && x.Price == lastRow.Price
            //    && x.Unit_ID == lastRow.Unit_ID && x != lastRow);
            //    if (row != null)
            //    {
            //        row.Qty += lastRow.Qty;
            //        //هنا عملنا داله ترجع لنا الصف ذي اضفنا فيه الكمية عشان نعدل السعر
            //        BillDetailsGridView_CellValueChanged(sender, new
            //            CellValueChangedEventArgs(BillDetailsGridView.Find_Handel_Row_By_Object(row)
            //            , BillDetailsGridView.Columns[nameof(invcDetail.Qty)], row.Qty));
            //        rows.Remove(lastRow);
            //    }
            //}
            currentRowPos = BillDetailsGridView.RowCount;
            BillDetailsGridView_RowUpdated(sender, null);
        }
        /// <summary>
        /// هذا الحدث عشان اذا المستخدم غير منتج في صف غسر الصف ذي بنضيف فيه
        /// فيرجع يهيئ الوحدات حق الصنف الجديد
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BillDetailsGridView_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            BeginInvoke(new Action(delegate
            {
                var view = sender as GridView;
                view.PostEditor();
                //view.UpdateCurrentRow();
            }));
            
            if (e.Column.FieldName == "Product_ID")
            {
                var row = BillDetailsGridView.GetRow(e.RowHandle) as DBModels.Invoice_Details;
                if (row == null) return;
                //هانا الفاليو هي القيمة الجديدة
                //وال روو هو القيمة القديمة 
                if (e.Value != null && row.Product_ID != 0 && e.Value.Equals(row.Product_ID) == false)//ToDO حتى يتم تغيير الوحدة عندما يتم تغيير صنف مضاف مسبقا
                {
                    row.Unit_ID = 0;
                }
            }
        }
        private string WrittenCode;
        /// <summary>
        /// في هذا الحدث نعالج جميع حسابات الكمية والخصم 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BillDetailsGridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            //اول شي حولنا السطر ذي بنتعامل معه لا نوع الفاتورة
            var row = BillDetailsGridView.GetRow(e.RowHandle) as DBModels.Invoice_Details;
            if (row == null)
                return;//هنا عاد المستخدم دخل الكود وعادو  ماقدبش اصناف
            Table_View.Product_And_Category_And_Units_View product = null;
            Table_View.Product_And_Category_And_Units_View.Product_Unit proUnitID = null;
            if (e.Column.FieldName == "Code")
            {
                if (e.Value == null) return;
                string itemCode = e.Value.ToString();
                if (Session.Barcode_Setting.Read_From_Scale_Barcode
                    && itemCode.Length == Session.Barcode_Setting.BarCode_Length
                    && itemCode.StartsWith(Session.Barcode_Setting.Scale_Barcode_PreFix))
                {
                    string itemCodeString = e.Value.ToString().Substring(Session.Barcode_Setting.Scale_Barcode_PreFix.Length
                        , Session.Barcode_Setting.Product_Code_Length);
                    itemCode = Convert.ToInt64(itemCodeString).ToString();
                    string readValue = e.Value.ToString().Substring(
                        Session.Barcode_Setting.Scale_Barcode_PreFix.Length +
                        Session.Barcode_Setting.Product_Code_Length);
                    if (Session.Barcode_Setting.Ignore_Check_Digit)
                    {
                        readValue = readValue.Remove(readValue.Length - 1, 1);
                    }
                    double value = Convert.ToDouble(readValue);
                    value = value / (Math.Pow(10, Session.Barcode_Setting.Divide_Value_By));
                    if (Session.Barcode_Setting.Read_Mode == Session.Barcode_Setting.Read_Value_Mode.Weight)
                    {
                        row.Qty = value;
                    }
                    else if (Session.Barcode_Setting.Read_Mode == Session.Barcode_Setting.Read_Value_Mode.Price)
                    {
                        product = Session.Full_products.FirstOrDefault(x => x.pro_Unit.Select(u => u.BarCode).Contains(itemCode));
                        if (product != null)
                        {
                            proUnitID = product.pro_Unit.First(x => x.BarCode == itemCode);
                            switch (bill_Type)
                            {
                                case bill_Type.BuyReturn:
                                case bill_Type.Buy:
                                    row.Qty = value / proUnitID.Buy_Price;
                                    break;
                                case bill_Type.Sale:
                                case bill_Type.SaleReturn:
                                    row.Qty = value / proUnitID.Sell_Price;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                if (product == null)
                    product = Session.Full_products.FirstOrDefault(x => x.Code.Contains(itemCode));
                //هنا ذيه السطر بيستدعي اول كود يشبه الكود المطروح في الفاليو او تحديدا ذي دخله المستخدم 
                //هذا الاستعلام كأن احنا عملنا استعلام فرعي بحيث رجع لي المنتج بحقه الكود
                if (product != null)
                {
                    row.Product_ID = product.ID;
                    if (proUnitID == null)
                        proUnitID = product.pro_Unit.FirstOrDefault();
                    //هنا شلينا اول صف من الوحدات عشان نعرض سعره وكل حاجاته
                    row.Unit_ID = proUnitID.Unit_ID;
                    BillDetailsGridView_CellValueChanged(sender,
                        new CellValueChangedEventArgs(e.RowHandle, BillDetailsGridView.Columns[nameof(invcDetail.Product_ID)]
                        , row.Product_ID));
                    //عملنا ذيه الحدث لان احنا عالجنا نازل عندما يتغير رقم المنتج يدي لنا
                    //الوحدات الخاصة به
                    BillDetailsGridView_CellValueChanged(sender,
                        new CellValueChangedEventArgs(e.RowHandle, BillDetailsGridView.Columns[nameof(invcDetail.Unit_ID)]
                        , row.Unit_ID));
                    //هنا بعدما قد معا المنتج الوحدات حقه يختار لنا اول وحدة ويديها لنا 
                    //بكل بياناتها
                    WrittenCode = string.Empty;
                    return;
                }
                WrittenCode = string.Empty;
            }
            //هنا بعدما تم الخبر ذي طالع او المستخدم اختار صنف من خلية الاصناف 
            //عيتاكد لنا اذا لقي اصناف او ماشي اذا كانت صفر ما لقيش فلهذا يخرج
            if (row.Product_ID == 0)
                return;
            product = Session.Full_products.FirstOrDefault(x => x.ID == row.Product_ID);
            if (product == null)
                return;
            if (row.Unit_ID == 0)
            {
                row.Unit_ID = product.pro_Unit.FirstOrDefault().Unit_ID;
                BillDetailsGridView_CellValueChanged(sender, new 
                    CellValueChangedEventArgs(e.RowHandle, 
                    BillDetailsGridView.Columns[nameof(invcDetail.Unit_ID)], row.Unit_ID));
            }
            //هنا فيها كل حاجات عن الوحدة وقيمتها
            proUnitID = product.pro_Unit.FirstOrDefault(x => x.Unit_ID == row.Unit_ID);
            switch (e.Column.FieldName)
            {
                case nameof(row.Product_ID):
                    if (row.Product_ID == 0)
                    {
                        BillDetailsGridView.DeleteRow(e.RowHandle);
                        return;
                    }
                    if (row.Store_ID == 0 && BranchlokUpEdt.Is_The_Lkp_Text_Valid())
                        row.Store_ID = (int?)BranchlokUpEdt.EditValue ?? 0;
                    BillDetailsGridView_CellValueChanged(sender, new CellValueChangedEventArgs(
                        e.RowHandle, BillDetailsGridView.Columns[nameof(invcDetail.Price)], row.Price));
                    break;
                case nameof(row.Unit_ID):
                    row.Price = Return_The_Product_Price(proUnitID, row);

                    if (row.Qty == 0)
                        row.Qty = 1;
                    BillDetailsGridView_CellValueChanged(sender, new CellValueChangedEventArgs(
                        e.RowHandle, BillDetailsGridView.Columns[nameof(invcDetail.Price)], row.Price));
                    //todo calculate balance for current item
                    break;
                case nameof(row.Price):
                case nameof(row.Discount_Prece):
                case nameof(row.Qty):
                    //todo يجب جلب سعر التكلفة عند تغييركمية الصنف او الصنف نفسه
                    row.Discount_Val = row.Discount_Prece * (row.Price * row.Qty);
                    //
                    row.Total_Price = (row.Price * row.Qty) - row.Discount_Val;//
                    //
                    //BillDetailsGridView_CellValueChanged(sender, new CellValueChangedEventArgs(e.RowHandle, BillDetailsGridView.Columns[nameof(tempbillditl.Discount_Value)], row.Discount_Value));
                    goto case nameof(row.Discount_Val);

                case nameof(row.Discount_Val):
                    if (BillDetailsGridView.FocusedColumn.FieldName == nameof(row.Discount_Val))
                    {
                        row.Discount_Prece = row.Discount_Val / (row.Price * row.Qty);
                        //BillDetailsGridView_CellValueChanged(sender, new CellValueChangedEventArgs(e.RowHandle, BillDetailsGridView.Columns[nameof(tempbillditl.Discount)], row.Discount));
                    }
                    row.Total_Price = (row.Price * row.Qty) - row.Discount_Val;
                    goto case nameof(row.Store_ID);

                case nameof(row.Store_ID):
                    //بيغير سعر التكلفة عندما نغير المخزن
                    switch (bill_Type)
                    {
                        case bill_Type.Buy://في الشراء عادي يجيب سعر التكلفة من البضاعة المشتراه
                            row.Cost = row.Total_Price / row.Qty;
                            row.Total_Cost = row.Total_Price;
                            break;
                        case bill_Type.Sale://اما هانا يجيب سعر التكلفة من المخزن 
                            ////
                            int store = (row.Store_ID == 0) ? Convert.ToInt32(BranchlokUpEdt.EditValue) : row.Store_ID;
                            //هنا بندي كم تكلفة المنتج حسب الطريقة المحددة
                            var costPerMainUnit = Inventory_Calculations.Get_Cost_Of_Next_Product(row.Product_ID, store, row.Qty);
                            row.Cost = costPerMainUnit / proUnitID.Factor;
                            row.Total_Cost = row.Cost * row.Qty;
                            break;
                        case bill_Type.BuyReturn:
                        case bill_Type.SaleReturn:
                            var returnedSourceRow = returnedSourceDetail.Where(x => x.ID == row.Source_Back_Row_ID).SingleOrDefault();
                            if (returnedSourceRow != null)
                            {
                                row.Cost = returnedSourceRow.Cost;
                                row.Total_Cost = row.Cost * row.Qty;
                            }
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// هذا الحدث عشان نعمل لكل سطر lkp لحاله 
        /// لان معانا كل صنف وحدات خاصة به
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BillDetailsGridView_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            /*
             ذلحين عنفعل لكل حقل لوك اب اديت لحاله عشان يكن يجي الصنف بحقه الوحدات
             */
            if (e.Column.FieldName == nameof(invcDetail.Unit_ID))
            {
                RepositoryItemLookUpEdit innerRepoUnit = new RepositoryItemLookUpEdit();
                innerRepoUnit.NullText = "";
                e.RepositoryItem = innerRepoUnit;
                var row = BillDetailsGridView.GetRow(e.RowHandle) as DBModels.Invoice_Details;
                //بهذا السطر ادينا الحقل ذي بنتعامل معه وحولناه لا كولكشن لانو يعيد قيمة من
                //تيه النوع على شكل مجموعة (سطر) 
                if (row == null)
                    return;
                var itemUnitV = Session.Full_products.FirstOrDefault(x => x.ID == row.Product_ID);
                //ordfualtهنا عملنا 
                //عشان السينقل وحدها عتطلع خطأ اذا المستخدم كتب كود مشو موجود
                if (itemUnitV == null)
                    return;
                innerRepoUnit.Initilaze_Data(itemUnitV.pro_Unit, null, null, "Unit_ID", "Unit_Name");
                innerRepoUnit.Columns.Clear();
                innerRepoUnit.Columns.Add(new LookUpColumnInfo("Unit_Name"));
                innerRepoUnit.ShowHeader = false;
            }
            else if (e.Column.FieldName == nameof(invcDetail.Product_ID))
            {
                /*
                عملنا ذيه عشان عندما المستخدم يختار صنف ما يجي له الا النشط 
                واذا ما دخلش الحقل ذيه ذي فيه الاداه يكون يعرض الكل لانو معانا ثنتين ادوات بتعرض
                المنتجات واحدة تعرض الكل والثاني ماتعرض الا النشطه فعندما المستخدم يجي يختار
                مايجي له الا النشط واذا مافتحش الاداه يجي الكل وعملنا تيه الحركة عشان
                المنتج مابيظهرش عندما يختار المستخدم لانو تغير نوعه بالشرط عندما عرفناه طالع 
                من قوئم مترابطة الى ienumarable
                 */
                //repoProductLkpAll.Initilaze_Data(Session.Full_products, BillDetailsGridCtrl, BillDetailsGridView.Columns[nameof(tempbillditl.Product_ID)]);
                // repoProductGridLookUpEdit.Initilaze_Data(Session.Full_products.Where(x => x.Is_Active = true).ToList(), null, null);//BillDetailsGridCtrl, BillDetailsGridView.Columns[nameof(tempbillditl.Product_ID)]);
                e.RepositoryItem = repoProductGridLookUpEdit;
            }
        }
        #endregion
        public void Frm_Invoice_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        /// <summary>
        /// هذي الدالة ترجع لي قيمة المنتج حسب نوع الفاتورة
        /// </summary>
        /// <param name="unitID"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private double Return_The_Product_Price(Table_View.Product_And_Category_And_Units_View.Product_Unit unitID, DBModels.Invoice_Details row)
        {
            switch (bill_Type)
            {
                case bill_Type.Buy:
                    return unitID.Buy_Price;
                case bill_Type.Sale:
                    return unitID.Sell_Price;
                case bill_Type.BuyReturn:
                case bill_Type.SaleReturn:
                    //هنا بحيث يرجع لنا السعر ذي بعنا به او اشترينا به
                    var returnedSourceRow = returnedSourceDetail.Where(x => x.ID == row.Source_Back_Row_ID).SingleOrDefault();
                    if (returnedSourceRow != null)
                    {
                        return returnedSourceRow.Price;
                    }
                    return -200;
                default:
                    return -200;
            }
        }
        private void Args_Showing(object sender, XtraMessageShowingArgs e)
        {
            e.Form.ControlBox = false;
            e.Buttons[DialogResult.OK].Text = "متابعة وحفظ";
            e.Form.Height = 125;
        }
        private void AddProReturnBtn_Click(object sender, EventArgs e)
        {
            if (BillSourceIDGrdLkp.EditValue == null || (int)BillSourceIDGrdLkp.EditValue <= 0)
            {
                XtraMessageBox.Show("!!يجب إختيار مصدر أولاً", "فشل فتح النافذة", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            using (XtraForm frm = new XtraForm()
            {
                Size = new Size(650, 300),
                StartPosition = FormStartPosition.CenterScreen,
                Name = "Frm_Select_Pro",
                Text = "إختر أصناف للإرجاع",
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
                    DBModels.Invoice_Details selectedRows = this.returnedSourceDetail[ve.ControllerRow];
                    BindingList<DBModels.Invoice_Details> source = BillDetailsGridView.DataSource as BindingList<DBModels.Invoice_Details>;
                    if (source == null) return;
                    if (ve.Action == CollectionChangeAction.Add)
                    {
                        if (source.Where(x => x.Source_Back_Row_ID == selectedRows.ID).Count() == 0)
                        {
                            source.Add(new DBModels.Invoice_Details()
                            {
                                Cost = selectedRows.Cost,
                                Total_Cost = selectedRows.Total_Cost,
                                Discount_Prece = selectedRows.Discount_Prece,
                                Discount_Val = selectedRows.Discount_Val,
                                Product_ID = selectedRows.Product_ID,
                                Unit_ID = selectedRows.Unit_ID,
                                Price = selectedRows.Price,
                                Total_Price = selectedRows.Total_Price,
                                Qty = selectedRows.Qty,
                                Store_ID = (BranchlokUpEdt.EditValue is int storeId) ? storeId : selectedRows.Store_ID,
                                Source_Back_Row_ID = selectedRows.ID,
                            });
                        }
                    }
                    else if (ve.Action == CollectionChangeAction.Remove)
                    {
                        if (source.Where(x => x.Source_Back_Row_ID == selectedRows.ID).Count() > 0)
                        {
                            source.Remove(source.Single(x => x.Source_Back_Row_ID == selectedRows.ID));
                        }
                    }
                    BillDetailsGridView.RefreshData();
                };

                RepositoryItemLookUpEdit products = new RepositoryItemLookUpEdit();
                RepositoryItemLookUpEdit proUnits = new RepositoryItemLookUpEdit();
                RepositoryItemLookUpEdit stores = new RepositoryItemLookUpEdit();
                control.RepositoryItems.AddRange(new RepositoryItem[] { products, proUnits, stores });
                view.Columns.AddField("ID").Visible = false;
                GridColumn proColumn = view.Columns.AddField("Product_ID");
                proColumn.Visible = true;
                proColumn.Caption = "الصنف";
                GridColumn unitColumn = view.Columns.AddField("Unit_ID");
                unitColumn.Visible = true;
                unitColumn.Caption = "الوحدة";
                view.Columns.AddField("Qty").Caption = "الكمية";
                view.Columns.AddField("Cost").Caption = "تكلفة الصنف";
                view.Columns.AddField("Total_Cost").Caption = "إجمالي التكلفة";
                view.Columns.AddField("Discount_Prece").Caption = "الخصم";
                view.Columns["Qty"].Visible = true;
                view.Columns["Cost"].Visible = true;
                view.Columns["Total_Cost"].Visible = true;
                view.Columns["Discount_Prece"].Visible = true;
                GridColumn storeColumn = view.Columns.AddField("Store_ID");
                storeColumn.Visible = true;
                storeColumn.Caption = "المخزن";
                control.DataSource = this.returnedSourceDetail;
                products.Initilaze_Data(Session.Full_products, control, proColumn);
                proUnits.Initilaze_Data(Session.Units, control, unitColumn);
                stores.Initilaze_Data(Session.Stores, control, storeColumn);
                frm.Controls.Add(control);
                control.ForceInitialize();
                BindingList<DBModels.Invoice_Details> source1 = BillDetailsGridView.DataSource as BindingList<DBModels.Invoice_Details>;
                if (source1 == null) return;
                for (int i = 0; i < returnedSourceDetail.Count(); i++)
                {
                    if (source1.Where(x => x.Source_Back_Row_ID == returnedSourceDetail[i].ID).Count() > 0)
                    {
                        view.SelectRow(i);
                    }
                }
                frm.ShowDialog();
            }
        }
        private void BillSourceIDGrdLkp_EditValueChanged(object sender, EventArgs e)
        {
            BillDetailsGridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            BillDetailsGridView.OptionsSelection.MultiSelect = true;
            BillDetailsGridView.SelectAll();
            BillDetailsGridView.DeleteSelectedRows();
            BillDetailsGridView.OptionsSelection.MultiSelect = false;
            if (BillSourceIDGrdLkp.EditValue is int sourceID && sourceID != 0)
            {
                //هنا خرجنا تفاصيل الفاتورة
                returnedSourceDetail = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Invoice_Details>(
                    @"Select * From Invoice_Details Where Invoice_ID = @invID",
                    new
                    {
                        invID = sourceID
                    });
                //هنا خرجنا الفاتورة نفسها عشان اذا شي خصم يطرحه على ماهو
                DBModels.Invoice sourceInvoice = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Invoice>(
                    @"Select * From Invoice Where ID = @invID",
                    new
                    {
                        invID = sourceID
                    }).FirstOrDefault();
                if (sourceInvoice != null)
                {
                    invc.Invoice_Back_ID = sourceInvoice.ID;
                    TotalSpnEdt.EditValue = sourceInvoice.Total;
                    DiscountValueSpnEdt.EditValue = sourceInvoice.Discount_Val;
                    TaxValueSpnEdt.EditValue = sourceInvoice.Tax_Val;
                    TaxRotationSpnEdt.EditValue = sourceInvoice.Tax_Perce;
                    DiscountRotationSpnEdt.EditValue = sourceInvoice.Discount_Perec;
                    AddProReturnBtn.PerformClick();
                }
            }
        }
        private void Read_User_Settings()
        {
            switch (bill_Type)
            {
                case bill_Type.Buy:
                    PartTypelokUpEdt.Enabled = Session.Current_User_Settings_Prope.Purchase_Settings.CanBuyFromCustomer;
                    BillDateDateEdit.Enabled = Session.Current_User_Settings_Prope.Purchase_Settings.CanChangeBuyBillDate;
                    BillDetailsGridView.Columns[nameof(invcDetail.Price)].OptionsColumn.AllowEdit =
                    BillDetailsGridView.Columns[nameof(invcDetail.Price)].OptionsColumn.AllowFocus = Session.Current_User_Settings_Prope.Purchase_Settings.CanChangeItemPriceInBuy;
                    PartIDGridLokUpEdt.Enabled = Session.Current_User_Settings_Prope.General_Settings.CanChangeSupplier;
                    break;
                case bill_Type.Sale:
                    PaidSpnEdt.Enabled = Session.Current_User_Settings_Prope.Sale_Settings.CanChangePaidInSales;
                    IsPostedChckEdt.Enabled = Session.Current_User_Settings_Prope.Sale_Settings.CanPostToStoreInSales;
                    BillDetailsGridView.Columns[nameof(invcDetail.Price)].OptionsColumn.AllowFocus = Session.Current_User_Settings_Prope.Sale_Settings.CanChangeItemPriceInSales;
                    BillDetailsGridView.Columns[nameof(invcDetail.Qty)].OptionsColumn.AllowFocus = Session.Current_User_Settings_Prope.Sale_Settings.CanChangeQuantityInSales;
                    BillDetailsGridView.Columns[nameof(invcDetail.Cost)].OptionsColumn.ShowInCustomizationForm =
                    BillDetailsGridView.Columns[nameof(invcDetail.Total_Cost)].OptionsColumn.ShowInCustomizationForm =
                    BillDetailsGridView.Columns[nameof(invcDetail.Cost)].Visible =
                    BillDetailsGridView.Columns[nameof(invcDetail.Total_Cost)].Visible = !Session.Current_User_Settings_Prope.Sale_Settings.HideCostInSales;
                    PartTypelokUpEdt.Enabled = Session.Current_User_Settings_Prope.Sale_Settings.CanSellToSupplier;
                    BillDateDateEdit.Enabled = Session.Current_User_Settings_Prope.Sale_Settings.CanChangeSalesBillDate;
                    PartIDGridLokUpEdt.Enabled = Session.Current_User_Settings_Prope.General_Settings.CanChangeCustomer;
                    break;
                case bill_Type.BuyReturn:
                    PartIDGridLokUpEdt.Enabled = Session.Current_User_Settings_Prope.General_Settings.CanChangeSupplier;
                    break;
                case bill_Type.SaleReturn:
                    PartIDGridLokUpEdt.Enabled = Session.Current_User_Settings_Prope.General_Settings.CanChangeCustomer;
                    break;
                default:
                    throw new NotImplementedException();
            }
            TaxRotationSpnEdt.Enabled =
            TaxValueSpnEdt.Enabled = Session.Current_User_Settings_Prope.Invoice_Settings.CanChangeTax;
            DrawerLokUpEdt.Enabled = Session.Current_User_Settings_Prope.General_Settings.CanChangeDrawer;
            BranchlokUpEdt.Enabled = Session.Current_User_Settings_Prope.General_Settings.CanChangeStore;
            UserHistoryInScreenLCG.Visibility =
                (Session.Current_User_Settings_Prope.General_Settings.CanSeeDocumentHistory) ?
                         LayoutVisibility.Always :
                         LayoutVisibility.Never;
        }
    }
}