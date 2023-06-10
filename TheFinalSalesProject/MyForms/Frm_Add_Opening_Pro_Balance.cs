using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TheFinalSalesProject.Classes.Master_Class;
using static TheFinalSalesProject.Classes.Validate_Data;
using TheFinalSalesProject.Classes;
using DevExpress.XtraEditors.Controls;
using static TheFinalSalesProject.Classes.Enum_Choices;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using Dapper;

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Add_Opening_Pro_Balance : Frm_Master
    {
        RepositoryItemGridLookUpEdit repoProductGridLookUpEdit = new RepositoryItemGridLookUpEdit();
        RepositoryItemLookUpEdit repoProductLkpAll = new RepositoryItemLookUpEdit();//the explainig exist in the customrowedit
        RepositoryItemLookUpEdit repoUnitsLokEdit = new RepositoryItemLookUpEdit();
        private DBModels.Opening_And_Destuctor_Product open_Dest_Pro;
        private Store_Balance_Type type = Store_Balance_Type.Opening_Account;
        private string msg ;
        public Frm_Add_Opening_Pro_Balance(Store_Balance_Type type)
        {
            InitializeComponent();
            this.type = type;
            Load_Screen();
        }
        private void Load_Screen()
        {
            BranchlokUpEdt.Initilaze_Data(Session.Stores, clear: true);
            BranchlokUpEdt.EditValue = Session.Default.StoreID;
            InvoiceDetailsGridCtrl.ProcessGridKey += InvoiceDetailsGridCtrl_ProcessGridKey;
            InvoiceDetailsGridView.CellValueChanged += InvoiceDetailsGridView_CellValueChanged;
            InvoiceDetailsGridView.CustomRowCellEditForEditing += InvoiceDetailsGridView_CustomRowCellEditForEditing;
            InvoiceDetailsGridView.CustomUnboundColumnData += InvoiceDetailsGridView_CustomUnboundColumnData;
            InvoiceDetailsGridView.InvalidRowException += InvoiceDetailsGridView_InvalidRowException;
            InvoiceDetailsGridView.RowUpdated += InvoiceDetailsGridView_RowUpdated;
            InvoiceDetailsGridView.RowCountChanged += InvoiceDetailsGridView_RowCountChanged;
            InvoiceDetailsGridView.CellValueChanging += InvoiceDetailsGridView_CellValueChanging;
            InvoiceDetailsGridView.ValidateRow += InvoiceDetailsGridView_ValidateRow;
        }
        private void Frm_Add_Opening_Pro_Balance_Load(object sender, EventArgs e)
        {
            NewBtn.Visibility = BarItemVisibility.Never;
            InvoiceDetailsGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            InvoiceDetailsGridView.Add_DeleteColumns_To_GridView(InvoiceDetailsGridCtrl);
            InvoiceDetailsGridView.Add_NumberColumn_To_GridView(InvoiceDetailsGridCtrl);

            repoProductGridLookUpEdit.Initilaze_Data(Session.Full_products.Where(x=>x.Is_Active == true).ToList()
                , InvoiceDetailsGridCtrl, InvoiceDetailsGridView.Columns["Product_ID"]);
            repoProductGridLookUpEdit.View.PopulateColumns(repoProductGridLookUpEdit.DataSource);
            repoUnitsLokEdit.Initilaze_Data(Session.Units, InvoiceDetailsGridCtrl, InvoiceDetailsGridView.Columns["Unit_ID"]);
            repoProductLkpAll.Initilaze_Data(Session.Full_products, InvoiceDetailsGridCtrl, InvoiceDetailsGridView.Columns["Product_ID"]);

            InvoiceDetailsGridView.Add_Spn_Repo_Value(InvoiceDetailsGridCtrl, "Qty");
            InvoiceDetailsGridView.Add_Spn_Repo_Value(InvoiceDetailsGridCtrl, "Price");
            InvoiceDetailsGridView.Add_Spn_Repo_Value(InvoiceDetailsGridCtrl, "Total_Price");
            InvoiceDetailsGridView.Columns.Add(new GridColumn()
            { Name = "clmnName", FieldName = "Code", Caption = "الكود", UnboundType = UnboundColumnType.String, VisibleIndex = 1 });
            InvoiceDetailsGridView.Columns["Number"].VisibleIndex = 0;
            InvoiceDetailsGridView.Columns["Code"].VisibleIndex = 1;
            InvoiceDetailsGridView.Columns["Product_ID"].VisibleIndex = 2;
            InvoiceDetailsGridView.Columns["Unit_ID"].VisibleIndex = 3;
            InvoiceDetailsGridView.Columns["Qty"].VisibleIndex = 4;
            InvoiceDetailsGridView.Columns["Price"].VisibleIndex = 5;
            InvoiceDetailsGridView.Columns["Total_Price"].VisibleIndex = 6;
            InvoiceDetailsGridView.Columns.Add(new GridColumn()
            { Name = "clmnBalance", FieldName = "Balance", Caption = "الرصيد", UnboundType = UnboundColumnType.Decimal, VisibleIndex = 7 });
            InvoiceDetailsGridView.Columns["ID"].Visible = false;
            InvoiceDetailsGridView.Columns["Open_Dest_ID"].Visible = false;
            InvoiceDetailsGridView.Columns["Product_ID"].Caption = "الصنف";
            InvoiceDetailsGridView.Columns["Unit_ID"].Caption = "الوحدة";
            InvoiceDetailsGridView.Columns["Qty"].Caption = "الكمية";
            InvoiceDetailsGridView.Columns["Price"].Caption = "السعر";
            InvoiceDetailsGridView.Columns["Total_Price"].Caption = "إجمالي السعر";
            InvoiceDetailsGridView.Columns["Total_Price"].OptionsColumn.AllowFocus = false;
            InvoiceDetailsGridView.Columns["Balance"].OptionsColumn.AllowFocus = false;
            InvoiceDetailsGridView.BestFitColumns();
            repoProductGridLookUpEdit.View.Grid_View_Translate_Column("Product");
            repoUnitsLokEdit.PopulateColumns();
            repoUnitsLokEdit.Repo_Look_Up_Edit_Translate_Column("Unit");
        }
        private void InvoiceDetailsGridCtrl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            GridControl control = sender as GridControl;
            if (control == null) return;
            GridView view = control.FocusedView as GridView;
            if (view == null) return;
            if (view.FocusedColumn == null) return;
            InvoiceDetailsGridView_ValidateRow(null, new ValidateRowEventArgs(
                view.FocusedRowHandle, view.GetRow(view.FocusedRowHandle)));
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                string focusedColumn = view.FocusedColumn.FieldName;
                if (view.FocusedRowHandle < 0)
                {
                    if (InvoiceDetailsGridView.HasColumnErrors)
                        return;
                    view.AddNewRow();
                    view.FocusedColumn = view.Columns[focusedColumn];
                }
                if (view.FocusedColumn.FieldName == "Code" || view.FocusedColumn.FieldName == "Product_ID")
                {
                    InvoiceDetailsGridCtrl_ProcessGridKey(sender, new KeyEventArgs(Keys.Tab));
                }
                e.Handled = true;
            }
            //هنا بنعمل حدث يدوي عشان يضيف الصنف لانو هذا الحدث بيتفعل قبل القريد فيو فلهذا 
            //بنعمل حدث ونخليه يسير التاب وبهدها يرجع ينفذ حق الانتر
            //طبعا المتغير عملنا عشان يعمل فوكس على العمود ذي تفعل الحدث منه اذا كان كود او صنف
            else if (e.KeyCode == Keys.Tab)
            {
                view.FocusedColumn = view.VisibleColumns[view.FocusedColumn.VisibleIndex + 1];
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (view.FocusedRowHandle >= 0)
                {
                    view.DeleteSelectedRows();
                }
            }
        }
        private void InvoiceDetailsGridView_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            if (e.Row == null || (e.Row as DBModels.Opening_Destructor_Details).Product_ID == 0)
            {
                InvoiceDetailsGridView.DeleteRow(e.RowHandle);
                e.ExceptionMode = ExceptionMode.Ignore;
                return;
            }
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        private int currentRowPos = 0;
        private void InvoiceDetailsGridView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            var row = e.Row as DBModels.Opening_Destructor_Details;
            if (row == null || row.Product_ID == 0)
            {
                e.Valid = false;
                return;
            }
        }
        private void InvoiceDetailsGridView_RowUpdated(object sender, RowObjectEventArgs e)
        {
            if (currentRowPos <= InvoiceDetailsGridView.RowCount)
            {
                //هنا يدي لي الصفوف المتشابهه في السعر والوحدة والمخزن ويجمعهن لي
                var rows = (InvoiceDetailsGridView.DataSource as BindingList<DBModels.Opening_Destructor_Details>);
                var lastRow = rows.LastOrDefault();
                var row = rows.FirstOrDefault(x => x.Product_ID == lastRow.Product_ID
                && x.Price == lastRow.Price
                && x.Unit_ID == lastRow.Unit_ID && x != lastRow);
                if (row != null)
                {
                    row.Qty += lastRow.Qty;
                    //هنا عملنا داله ترجع لنا الصف ذي اضفنا فيه الكمية عشان نعدل السعر
                    InvoiceDetailsGridView_CellValueChanged(sender, new
                        CellValueChangedEventArgs(InvoiceDetailsGridView.Find_Handel_Row_By_Object(row)
                        , InvoiceDetailsGridView.Columns["Qty"], row.Qty));
                    rows.Remove(lastRow);
                }
            }
            var items = InvoiceDetailsGridView.DataSource as BindingList<DBModels.Opening_Destructor_Details>;
            if (items == null)
                TotalSpnEdt.EditValue = 0;
            else
                TotalSpnEdt.EditValue = items.Sum(x => x.Total_Price);
        }
        private void InvoiceDetailsGridView_RowCountChanged(object sender, EventArgs e)
        {
            currentRowPos = InvoiceDetailsGridView.RowCount;
            InvoiceDetailsGridView_RowUpdated(sender, null);
        }
        private void InvoiceDetailsGridView_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Unit_ID")
            {
                RepositoryItemLookUpEdit innerRepoUnit = new RepositoryItemLookUpEdit();
                innerRepoUnit.NullText = "";
                e.RepositoryItem = innerRepoUnit;
                var row = InvoiceDetailsGridView.GetRow(e.RowHandle) as DBModels.Opening_Destructor_Details;
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
            else if (e.Column.FieldName == "Product_ID")
            {
                e.RepositoryItem = repoProductGridLookUpEdit;
            }
        }
        private string WrittenCode;
        private void InvoiceDetailsGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var row = InvoiceDetailsGridView.GetRow(e.RowHandle) as DBModels.Opening_Destructor_Details;
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
                            row.Qty = value / proUnitID.Buy_Price;
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
                    InvoiceDetailsGridView_CellValueChanged(sender,
                        new CellValueChangedEventArgs(e.RowHandle, InvoiceDetailsGridView.Columns["Product_ID"]
                        , row.Product_ID));
                    //عملنا ذيه الحدث لان احنا عالجنا نازل عندما يتغير رقم المنتج يدي لنا
                    //الوحدات الخاصة به
                    InvoiceDetailsGridView_CellValueChanged(sender,
                        new CellValueChangedEventArgs(e.RowHandle, InvoiceDetailsGridView.Columns["Unit_ID"]
                        , row.Unit_ID));
                    //هنا بعدما قد معا المنتج الوحدات حقه يختار لنا اول وحدة ويديها لنا 
                    //بكل بياناتها
                    WrittenCode = string.Empty;
                    return;
                }
                WrittenCode = string.Empty;
            }
            if (row.Product_ID == 0)
                return;
            product = Session.Full_products.FirstOrDefault(x => x.ID == row.Product_ID);
            if (product == null)
                return;
            if (row.Unit_ID == 0)
            {
                row.Unit_ID = product.pro_Unit.FirstOrDefault().Unit_ID;
                InvoiceDetailsGridView_CellValueChanged(sender, new
                    CellValueChangedEventArgs(e.RowHandle,
                    InvoiceDetailsGridView.Columns["Unit_ID"], row.Unit_ID));
            }
            //هنا فيها كل حاجات عن الوحدة وقيمتها
            proUnitID = product.pro_Unit.FirstOrDefault(x => x.Unit_ID == row.Unit_ID);
            switch (e.Column.FieldName)
            {
                case nameof(row.Product_ID):
                    if (row.Product_ID == 0)
                    {
                        InvoiceDetailsGridView.DeleteRow(e.RowHandle);
                        return;
                    }
                    InvoiceDetailsGridView_CellValueChanged(sender, new CellValueChangedEventArgs(
                        e.RowHandle, InvoiceDetailsGridView.Columns["Price"], row.Price));
                    break;
                case nameof(row.Unit_ID):
                    row.Price = proUnitID.Buy_Price;

                    if (row.Qty == 0)
                        row.Qty = 1;
                    InvoiceDetailsGridView_CellValueChanged(sender, new CellValueChangedEventArgs(
                        e.RowHandle, InvoiceDetailsGridView.Columns["Price"], row.Price));
                    //todo calculate balance for current item
                    break;
                case nameof(row.Qty):
                case nameof(row.Price):
                    row.Total_Price = (row.Price * row.Qty);
                    break;
            }
        }
        private void InvoiceDetailsGridView_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            BeginInvoke(new Action(delegate
            {
                var view = sender as GridView;
                view.PostEditor();
                //view.UpdateCurrentRow();
            }));

            if (e.Column.FieldName == "Product_ID")
            {
                var row = InvoiceDetailsGridView.GetRow(e.RowHandle) as DBModels.Opening_Destructor_Details;
                if (row == null) return;
                //هانا الفاليو هي القيمة الجديدة
                //وال روو هو القيمة القديمة 
                if (e.Value != null && row.Product_ID != 0 && e.Value.Equals(row.Product_ID) == false)//ToDO حتى يتم تغيير الوحدة عندما يتم تغيير صنف مضاف مسبقا
                {
                    row.Unit_ID = 0;
                }
            }
        }
        private void InvoiceDetailsGridView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
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
                int store_ID = Convert.ToInt32(BranchlokUpEdt.EditValue);
                var row = e.Row as DBModels.Opening_Destructor_Details;
                int rowIndx = e.ListSourceRowIndex;
                if (row == null || row.Product_ID <= 0 || store_ID <= 0 || row.Unit_ID <= 0)
                {
                    e.Value = null;
                    return;
                }
                var proBalance = Session.Pro_Balance.
                    FirstOrDefault(x => x.Product_ID == row.Product_ID && x.Store_ID == store_ID);
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
                var rows = (InvoiceDetailsGridView.DataSource as BindingList<DBModels.Opening_Destructor_Details>).
                    Take(rowIndx).Where(x => x.Product_ID == row.Product_ID && x.Unit_ID == row.Unit_ID);
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
        }
        protected override void Get_Data()
        {
            BillCodeTxt.Text = open_Dest_Pro.Code;
            BillDateDateEdit.EditValue = open_Dest_Pro.Date;
            NotesMemEdt.Text = open_Dest_Pro.Notes;
            TotalSpnEdt.EditValue = open_Dest_Pro.Total;
            BillIDTxt.Text = open_Dest_Pro.ID.ToString();
            var data = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Opening_Destructor_Details>(
                @"Select * From Opening_Destructor_Details Where Open_Dest_ID = @id ",
                new { id = open_Dest_Pro.ID });
            BindingList<DBModels.Opening_Destructor_Details> list = new BindingList<DBModels.Opening_Destructor_Details>(data);
            InvoiceDetailsGridCtrl.DataSource = list;
            base.Get_Data();
        }
        protected override void Set_Data()
        {
            open_Dest_Pro.Code = BillCodeTxt.Text.Trim();
            open_Dest_Pro.Date = BillDateDateEdit.DateTime;
            open_Dest_Pro.Notes = NotesMemEdt.Text.Trim();
            open_Dest_Pro.Total = Convert.ToDouble(TotalSpnEdt.EditValue);
            open_Dest_Pro.Type = Convert.ToByte(type);
            open_Dest_Pro.Store_ID = Convert.ToInt32(BranchlokUpEdt.EditValue);
            base.Set_Data();
        }
        protected override void Refresh_Data()
        {
            if (!BranchlokUpEdt.Is_The_Lkp_Text_Valid()) return;
            int storeID = Convert.ToInt32(BranchlokUpEdt.EditValue);
            open_Dest_Pro = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Opening_And_Destuctor_Product>(
                @"Select * From Opening_And_Destuctor_Product Where Store_ID = @sID And Type = @type",
                new { sID = storeID, type = Convert.ToByte(type) }).FirstOrDefault();
            if (open_Dest_Pro == null)
                open_Dest_Pro = new DBModels.Opening_And_Destuctor_Product()
                {
                    Code = Get_New_Bill_Code(),
                    Date = DateTime.Now.Date
                };
            Get_Data();
            base.Refresh_Data();
        }
        private string Get_New_Bill_Code()
        {
            string maxCode;
            {
                var s = DAL.Impelement_Stored_Procedure.SelectData<DBModels.ID_Model>(
                    @"Select  ISNUll(Max(Code),'0') AS ID From Opening_And_Destuctor_Product").
                    SingleOrDefault().ID.ToString();
                maxCode = string.IsNullOrEmpty(s) ? "0" : s;
                return Master_Class.Get_Next_Number_InTheString(maxCode);
            }
        }
        private void BranchlokUpEdt_EditValueChanged(object sender, EventArgs e)
        {
            Refresh_Data();
        }
        private bool Is_Code_Exist()
        {
            var data = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Opening_And_Destuctor_Product>(
                @"Select * From Opening_And_Destuctor_Product 
                           Where ID != @id And Type = @type And Code = @code", new { id = open_Dest_Pro.ID, type = (byte)type, code = BillCodeTxt.Text.Trim() }).Count;
            if (data > 0)
                BillCodeTxt.ErrorText = Messages.Code_Exist;
            return (data <= 0);
        }
        protected override bool IsDataValid()
        {
            int numError = 0;
            if (InvoiceDetailsGridView.RowCount <= 0)
            {
                numError++;
                XtraMessageBox.Show("الرجاء إضافة أصناف أولاً ", "فشل حفظ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            numError += BranchlokUpEdt.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += Is_Code_Exist() ? 0 : 1;
            numError += BillDateDateEdit.Is_The_Date_Valid() ? 0 : 1;
            numError += TotalSpnEdt.Is_The_Edit_Value_More_Than_Zero() ? 0 : 1;
            return (numError == 0);
        }
        protected override void Save()
        {
            if (open_Dest_Pro.ID == 0)
            {
                open_Dest_Pro.ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>(
                    @"INSERT INTO [FinalSalesDB].[dbo].[Opening_And_Destuctor_Product]
                           ([Code]
                           ,[Type]
                           ,[Store_ID]
                           ,[Date]
                           ,[Total]
                           ,[Notes])
                     VALUES(@code,@type,@sID,@date,@tot,@note)",
                    new
                    {
                        code = open_Dest_Pro.Code,
                        type = open_Dest_Pro.Type,
                        sID = open_Dest_Pro.Store_ID,
                        date = open_Dest_Pro.Date,
                        tot = open_Dest_Pro.Total,
                        note = open_Dest_Pro.Notes
                    }).FirstOrDefault().ID;
            }
            else
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce(
                    @"UPDATE [FinalSalesDB].[dbo].[Opening_And_Destuctor_Product]
                       SET [Code] = @code
                          ,[Type] = @type
                          ,[Store_ID] = @sID
                          ,[Date] = @date
                          ,[Total] = @tot
                          ,[Notes] = @note
                     WHERE [ID] = @id",
                    new
                    {
                        code = open_Dest_Pro.Code,
                        type = open_Dest_Pro.Type,
                        sID = open_Dest_Pro.Store_ID,
                        date = open_Dest_Pro.Date,
                        tot = open_Dest_Pro.Total,
                        note = open_Dest_Pro.Notes,
                        id = open_Dest_Pro.ID
                    });
            }
            var list = (InvoiceDetailsGridView.DataSource as BindingList<DBModels.Opening_Destructor_Details>);
            Delete_Data.Delete_Pro_Store_Move_Details_For_OpenDestruct(open_Dest_Pro.ID, type);
            Delete_Data.Delete_OpenDestrct_Details(open_Dest_Pro.ID);
            foreach (var item in list)
            {
                item.ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>(
                @"INSERT INTO [FinalSalesDB].[dbo].[Opening_Destructor_Details]
                   ([Open_Dest_ID]
                   ,[Product_ID]
                   ,[Unit_ID]
                   ,[Qty]
                   ,[Price]
                   ,[Total_Price])
                    VALUES(@odID,@proID,@uID,@qty,@price,@totp)",
                new
                {
                    odID = open_Dest_Pro.ID,
                    proID = item.Product_ID,
                    uID = item.Unit_ID,
                    qty = item.Qty,
                    price = item.Price,
                    totp = item.Total_Price
                }).FirstOrDefault().ID;
            }
            var moveProStore = new List<DBModels.Pro_Store_Movement>();
            msg = ((type == Store_Balance_Type.Opening_Account) ? " رصيد إفتتاحي للصنف" : " إهلاك رصيد الصنف ");
            foreach (var row in list)
            {
                var unitV = Session.Full_products.FirstOrDefault(x => x.ID == row.Product_ID).
                                    pro_Unit.FirstOrDefault(x => x.Unit_ID == row.Unit_ID);
                moveProStore.Add(new DBModels.Pro_Store_Movement()
                {
                    Product_ID = row.Product_ID,
                    Insert_Date = open_Dest_Pro.Date,
                    Source_ID = row.ID,
                    Source_Type = open_Dest_Pro.Type,
                    Notes = msg,//في حالة الشرء ترو وحالة البيع فولس
                    Import_Qty = (type == Store_Balance_Type.Opening_Account) ? (row.Qty * unitV.Factor) : 0,
                    Export_Qty = (!(type == Store_Balance_Type.Opening_Account)) ? (row.Qty * unitV.Factor) : 0,
                    Store_ID = open_Dest_Pro.Store_ID,
                    Cost_Value = row.Price / unitV.Factor,
                    User_ID = Session.user.ID
                });
            }
            var proMove = new
            {
                proMove = Convert_List_To_DataTable.Convert_ProMove_List_To_Table(moveProStore).
                AsTableValuedParameter("Pro_Movement_List_Type")
            };
            DAL.Impelement_Stored_Procedure.Excute_Proce(
                "FinalSalesDB.dbo.Add_List_Of_Pro_Movement", proMove, isCommandText: false);
            base.Save();
        }
        protected override void Delete()
        {
            Delete_Data.Delete_Pro_Store_Move_Details_For_OpenDestruct(open_Dest_Pro.ID, type);
            Delete_Data.Delete_OpenDestrct_Details(open_Dest_Pro.ID);
            DAL.Impelement_Stored_Procedure.Excute_Proce(
                @"Delete From [FinalSalesDB].[dbo].[Opening_And_Destuctor_Product] Where ID = @id ", 
                new { id = open_Dest_Pro.ID });
            base.Delete();
            Refresh_Data();
            DeleteBtn.Enabled = true;
        }
    }
}
