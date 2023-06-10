using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TheFinalSalesProject.Classes;
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Invoice_List : Frm_Master_List
    {
        private bill_Type type;
        public Frm_Invoice_List(bill_Type type)
        {
            InitializeComponent();
            PartTypelokUpEdt.EditValueChanged += PartTypelokUpEdt_EditValueChanged;
            ListGrdViw.PopupMenuShowing += ListGrdViw_PopupMenuShowing;
            ListGrdCtrl.ViewRegistered += ListGrdCtrl_ViewRegistered;
            ListGrdViw.RowCellStyle += ListGrdViw_RowCellStyle;
            ListGrdViw.RowCountChanged += ListGrdViw_RowCountChanged;
            this.type = type;
            FromDateEdt.EditValue =
            ToDateEdt.EditValue = DateTime.Now;
            ListGrdViw.Add_Button_To_Group_Header(Properties.Resources.editfilter, ListGrdViw_OpenFilter, this.RightToLeftLayout);
            Refresh_Data();
            Set_Form_Type();
        }
        private void Frm_Invoice_List_Load(object sender, EventArgs e)
        {
            layoutControl2.SendToBack();
            ListGrdCtrl.BringToFront();
            ListGrdViw.Add_NumberColumn_To_GridView(ListGrdCtrl);
            ListGrdViw.Grid_View_Translate_Column("Invoice_List");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl,"Type");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "PersonName");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "StoreName");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "DrawerName");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "Shipping_Address");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "Notes");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "Real_Name");

            ListGrdViw.Add_Spn_Repo_Value(ListGrdCtrl, "Expences");
            ListGrdViw.Add_Spn_Repo_Value(ListGrdCtrl, "Discount_Val");
            ListGrdViw.Add_Spn_Repo_Value(ListGrdCtrl, "Tax_Val");
            ListGrdViw.Add_Spn_Repo_Value(ListGrdCtrl, "Net");
            ListGrdViw.Add_Spn_Repo_Value(ListGrdCtrl, "Paid");
            ListGrdViw.Add_Spn_Repo_Value(ListGrdCtrl, "Remaining");
            ListGrdViw.Add_Spn_Repo_Value(ListGrdCtrl, "Total");

            ListGrdViw.Add_Sum_Summary_To_Grid_Footer("Expences");
            ListGrdViw.Add_Sum_Summary_To_Grid_Footer("Discount_Val");
            ListGrdViw.Add_Sum_Summary_To_Grid_Footer("Tax_Val");
            ListGrdViw.Add_Sum_Summary_To_Grid_Footer("Net");
            ListGrdViw.Add_Sum_Summary_To_Grid_Footer("Paid");
            ListGrdViw.Add_Sum_Summary_To_Grid_Footer("Remaining");

            ListGrdViw.Add_Sum_Group_Summary_To_Grid_Footer("Expences");
            ListGrdViw.Add_Sum_Group_Summary_To_Grid_Footer("Discount_Val");
            ListGrdViw.Add_Sum_Group_Summary_To_Grid_Footer("Tax_Val");
            ListGrdViw.Add_Sum_Group_Summary_To_Grid_Footer("Net");
            ListGrdViw.Add_Sum_Group_Summary_To_Grid_Footer("Paid");
            ListGrdViw.Add_Sum_Group_Summary_To_Grid_Footer("Remaining");

            RefreshBtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            PartTypelokUpEdt.Initilaze_Data(Master_Class.part_Type_List, clear: true);
            DrawerLokUpEdt.Initilaze_Data(Session.Drawers, clear: true);
            BranchlokUpEdt.Initilaze_Data(Session.Stores, clear: true);
            ApplyBtn.ActiveForecolor = Color.Transparent;
            ApplyBtn.ActiveLineColor = Color.FromArgb(0, 0, 192);
            ApplyBtn.ActiveFillColor = Color.FromArgb(0, 0, 192);
            ApplyBtn.IdleFillColor = Color.Transparent;
            ApplyBtn.IdleForecolor = Color.Blue;
            ApplyBtn.IdleLineColor = Color.FromArgb(0, 0, 192);

            ClearFilterBtn.ActiveForecolor = Color.Transparent;
            ClearFilterBtn.ActiveLineColor = Color.FromArgb(192, 0, 0);
            ClearFilterBtn.ActiveFillColor = Color.FromArgb(192, 0, 0);
            ClearFilterBtn.IdleFillColor = Color.Transparent;
            ClearFilterBtn.IdleForecolor = Color.FromArgb(192, 0, 0);
            ClearFilterBtn.IdleLineColor = Color.FromArgb(192, 0, 0);

            CloseFlyBtn.ActiveForecolor = Color.Transparent;
            CloseFlyBtn.ActiveLineColor = Color.FromArgb(192, 0, 0);
            CloseFlyBtn.ActiveFillColor = Color.FromArgb(192, 0, 0);
            CloseFlyBtn.IdleFillColor = Color.Transparent;
            CloseFlyBtn.IdleForecolor = Color.FromArgb(192, 0, 0);
            CloseFlyBtn.IdleLineColor = Color.FromArgb(192, 0, 0);
            FromDateEdt.Add_Button_In_Control();
            ToDateEdt.Add_Button_In_Control();
            PartTypelokUpEdt.Add_Button_In_Control();
            PartIDGridLokUpEdt.Add_Button_In_Control();
            BranchlokUpEdt.Add_Button_In_Control();
            DrawerLokUpEdt.Add_Button_In_Control();
            ListGrdViw.OptionsSelection.MultiSelect = true;
            ListGrdViw.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            ListGrdViw.OptionsView.ShowFooter = false;
            ListGrdViw_RowCountChanged(null, null);
            ListGrdViw.OptionsView.ShowAutoFilterRow = true;
        }
        private void Set_Form_Type()
        {
            switch (type)
            {
                case bill_Type.Buy:
                    this.Text = "قائمة فواتير المشتريات";
                    this.Name = Screens.Buy_Bill_List.Screen_Name;
                    break;
                case bill_Type.Sale:
                    this.Text = "قائمة فواتير المبيعات";
                    this.Name = Screens.Sale_Bill_List.Screen_Name;
                    break;
                case bill_Type.BuyReturn:
                    this.Text = "قائمة فواتير مرتجع الشراء";
                    this.Name = Screens.Buy_Return_Bill_List.Screen_Name;
                    break;
                case bill_Type.SaleReturn:
                    this.Text = "قائمة فواتير مرتجع البيع";
                    this.Name = Screens.Sale_Return_Bill_List.Screen_Name;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        protected override void Refresh_Data()
        {
            List<DBModels.Max_Invoice> data = new List<DBModels.Max_Invoice>();
            DateTime? from = null;
            DateTime? to = null;
            byte? perType = null;
            int? perID = null;
            int? strID = null;
            int? drID = null;
            if (FromDateEdt.Is_The_Date_Valid(false))
                from = FromDateEdt.DateTime;
            if (ToDateEdt.Is_The_Date_Valid(false))
                to = ToDateEdt.DateTime;
            if (PartTypelokUpEdt.Is_The_Lkp_Text_Valid(false))
                perType = Convert.ToByte(PartTypelokUpEdt.EditValue);
            if (PartIDGridLokUpEdt.Is_The_Lkp_Text_Valid(false))
                perID = Convert.ToInt32(PartIDGridLokUpEdt.EditValue);
            if (BranchlokUpEdt.Is_The_Lkp_Text_Valid(false))
                strID = Convert.ToInt32(BranchlokUpEdt.EditValue);
            if (DrawerLokUpEdt.Is_The_Lkp_Text_Valid(false))
                drID = Convert.ToInt32(DrawerLokUpEdt.EditValue);
            data = DAL.Impelement_Stored_Procedure.Get_The_Max_Invoice("[FinalSalesDB].[dbo].[Get_The_Max_Invoice]");
            //else
            //{
            //    data = DAL.Impelement_Stored_Procedure.Get_The_Max_Invoice(
            //        "[FinalSalesDB].[dbo].[Filter_Invoice]",
            //        //new 
            //        //{
            //        //    id = 0,
            //        //    invType = 2,
            //        //    perID = Convert.ToInt32(PartIDGridLokUpEdt.EditValue ?? 0),
            //        //    perType = Convert.ToInt32(PartTypelokUpEdt.EditValue ?? 0),
            //        //    strID = Convert.ToInt32(BranchlokUpEdt.EditValue ?? 0),
            //        //    drID = Convert.ToInt32(DrawerLokUpEdt.EditValue ?? 0),
            //        //    usID = 1,
            //        //}
            //        Parameters
            //        );
            //}
            //filter
            data = data.Where(x => x.Type == Convert.ToByte(this.type)).ToList();
            if (from != null)
                data = data.Where(x => x.Date.Date >= from.Value.Date).ToList();
            if (to != null)
                data = data.Where(x => x.Date.Date <= to.Value.Date).ToList();
            if (strID != null)
                data = data.Where(x => x.Store_ID <= strID).ToList();
            if (drID != null)
                data = data.Where(x => x.Drawer_ID <= drID).ToList();
            if (perID != null)
                data = data.Where(x => x.Person_ID <= perID).ToList();
            if (perType != null)
                data = data.Where(x => x.Person_Type <= perType).ToList();
            ListGrdCtrl.DataSource = data;
            base.Refresh_Data();
        }
        protected override void New()
        {
            Frm_Main_Window.OpenForm(new Frm_Invoice(type));
            base.New();
        }
        protected override void Open_New_Window()
        {
            Frm_Main_Window.OpenForm(new Frm_Invoice_List(this.type), false);
        }
        protected override void Open_Form(int id)
        {
            Frm_Main_Window.OpenForm(new Frm_Invoice(type, id));
        }
        private void ListGrdViw_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            double remain = Convert.ToDouble(ListGrdViw.GetRowCellValue(e.RowHandle, "Remaining"));
            if (e.Column.FieldName == "PayStuts")
            {
                if (remain == 0)
                {
                    e.Column.AppearanceCell.BackColor = DXSkinColors.FillColors.Success;
                }
                else if (remain > 0)
                {
                    double net = Convert.ToDouble(ListGrdViw.GetRowCellValue(e.RowHandle, "Net"));
                    if (remain == net)
                    {
                        e.Column.AppearanceCell.BackColor = DXSkinColors.FillColors.Danger;
                    }
                    else if (remain < net)
                    {
                        e.Column.AppearanceCell.BackColor = DXSkinColors.FillColors.Warning;
                    }
                }
            }
        }
        private void ListGrdCtrl_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            var view = e.View as GridView;
            view.OptionsView.ShowViewCaption = true;
            if (view != null && view.LevelName == "InvoiceDetail")
            {
                view.ViewCaption = "الأصناف";
                view.Columns["DetID"].Visible = false;
                view.Columns["Invoice_ID"].Visible = false;
                view.Columns["Product_ID"].Caption = "رقم الصنف";
                view.Columns["PCode"].Caption = "كود الصنف";
                view.Columns["ProName"].Caption = "إسم الصنف";
                view.Columns["Unit_ID"].Caption = "رقم الوحدة";
                view.Columns["Unit_ID"].Visible = false;
                view.Columns["UnitName"].Caption = "إسم الوحدة";
                view.Columns["Price"].Caption = "السعر";
                view.Columns["Qty"].Caption = "الكمية";
                view.Columns["Total_Price"].Caption = "إجمالي السعر";
                view.Columns["DDiscount_Val"].Caption = " نسبة الخصم";
                view.Columns["Cost"].Caption = "التكلفة";
                view.Columns["Total_Cost"].Caption = " إجمالي التكلفة";
                view.Columns["DStore_ID"].Caption = "رقم المخزن";
                view.Columns["DStore_ID"].Visible = false;
                view.Columns["DStoreName"].Caption = "إسم المخزن";
            }
            else if(view != null && view.LevelName == "InvoiceBack")
            {
                view.ViewCaption = "الفواتير المتعلقة بهذه الفاتورة";
                view.Columns["BID"].Caption = "رقم الفاتورة";
                view.Columns["BCode"].Caption = "الكود";
                view.Columns["BDate"].Caption = "التاريخ";
                view.Columns["BStoreName"].Caption = "المخزن";
                view.Columns["BTotal"].Caption = "الإجمالي";
                view.Columns["BTax_Val"].Caption = "قيمة الضريبة";
                view.Columns["BDiscount_Val"].Caption = "قيمة الخصم";
                view.Columns["BExpences"].Caption = "مصروفات أخرى";
                view.Columns["BNet"].Caption = "الصافي";
                view.Columns["BPaid"].Caption = "المدفوع";
                view.Columns["BDrawerName"].Caption = "الخزينة";
                view.Columns["BRemaining"].Caption = "المتبقي";
                view.Columns["BReal_Name"].Caption = "إسم المستخدم";
            }
            view.BestFitColumns();
        }
        /// <summary>
        /// this event create menu strip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListGrdViw_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow || e.HitInfo.InRowCell)
            {
                var dxPrintBtn = new DevExpress.Utils.Menu.DXMenuItem() { Caption = "طباعة" };
                dxPrintBtn.ImageOptions.SvgImage = Properties.Resources.print;
                dxPrintBtn.Click += DxPrintBtn_Click;
                e.Menu.Items.Add(dxPrintBtn);

                var dxDeletBtn = new DevExpress.Utils.Menu.DXMenuItem() { Caption = "حذف" };
                dxDeletBtn.ImageOptions.SvgImage = Properties.Resources.removesheetrows;
                dxDeletBtn.Click += DxDelteBtn_Click;
                e.Menu.Items.Add(dxDeletBtn);
            }
        }
        private void DxDelteBtn_Click(object sender, EventArgs e)
        {
            List<int> ids = Get_Selected_IDs();
            if (ids != null)
                Frm_Invoice.Delete(ids, type, this.Name);
            Refresh_Data();
        }
        private List<int> Get_Selected_IDs()
        {
            var selectRows = ListGrdViw.GetSelectedRows();
            List<int> ids = new List<int>();
            foreach (var item in selectRows)
            {
                ids.Add(Convert.ToInt32(ListGrdViw.GetRowCellValue(item, "ID")));
            }
            if (ids.Count <= 0)
            {
                XtraMessageBox.Show("يرجى إختيار صفوف أولاً", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return null;
            }
            return ids;
        }
        private void DxPrintBtn_Click(object sender, EventArgs e)
        {
            List<int> ids = Get_Selected_IDs();
            if (ids != null)
                Frm_Invoice.Print(ids, type, this.Name);
        }
        private bool alreadyShowed;
        private void ListGrdViw_OpenFilter(object sender, EventArgs e)
        {
            if (alreadyShowed)
                Frm_Invoice_List_KeyDown(sender, new KeyEventArgs(Keys.Control|Keys.F9));
            else
                Frm_Invoice_List_KeyDown(sender, new KeyEventArgs(Keys.F9));

            //FilterFlyotPnl.ShowPopup();
        }
        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            Refresh_Data();
            FilterFlyotPnl.HidePopup();
        }
        private void PartTypelokUpEdt_EditValueChanged(object sender, EventArgs e)
        {
            if (PartTypelokUpEdt.Is_The_Lkp_Edit_Value_Of_Type_Int())
            {
                int type = Convert.ToByte(PartTypelokUpEdt.EditValue);
                if (type == (byte)Part_Type.Customer)
                {
                    PartIDGridLokUpEdt.Initilaze_Data(Session.Customers);
                }
                else if (type == (byte)Part_Type.Supplier)
                {
                    PartIDGridLokUpEdt.Initilaze_Data(Session.Suppliers);
                }
            }
        }
        private void CloseFlyBtn_Click(object sender, EventArgs e)
        {
            FilterFlyotPnl.HidePopup();
        }
        private void ClearFilterBtn_Click(object sender, EventArgs e)
        {
            FromDateEdt.EditValue =
            ToDateEdt.EditValue =
            PartTypelokUpEdt.EditValue =
            PartIDGridLokUpEdt.EditValue =
            BranchlokUpEdt.EditValue =
            DrawerLokUpEdt.EditValue = null;
        }
        public override void Print()
        {
            string namerpt = (type == bill_Type.Buy) ? "كشف فواتير المشتريات" : (type == bill_Type.Sale) ? "كشف فواتير المبيعات" :
                (type == bill_Type.BuyReturn) ? "كشف فواتير مرتجع الشراء" : (type == bill_Type.SaleReturn) ? "كشف فواتير مرتجع البيع" : "Unknown";
            Reports.Rpt_Print_Any_Grid_List.Print(ListGrdCtrl, namerpt, Get_Filter());
            base.Print();
        }
        protected override void Delete()
        {
            DxDelteBtn_Click(null, null);
        }
        private string Get_Filter()
        {
            string filt = "";
            if (FromDateEdt.EditValue != null)
                filt += From_lyc.Text + ":" + FromDateEdt.Text;
            if (ToDateEdt.EditValue != null)
                filt += " / " + To_Lyc.Text + ":" + ToDateEdt.Text;
            if (BranchlokUpEdt.EditValue != null)
                filt += " / " + Branch_Lyc.Text + ":" + BranchlokUpEdt.Text;
            if (DrawerLokUpEdt.EditValue != null)
                filt += " / " + Drawer_Lyc.Text + ":" + DrawerLokUpEdt.Text;
            if (PartTypelokUpEdt.EditValue != null)
                filt += " / " + Part_Type_Lyc.Text + ":" + PartTypelokUpEdt.Text;
            if (PartIDGridLokUpEdt.EditValue != null)
                filt += " / " + PartID_Lyc.Text + ":" + PartIDGridLokUpEdt.Text;
            return filt;
        }
        private void Frm_Invoice_List_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F9)
            {
                FilterFlyotPnl.HidePopup();
                alreadyShowed = false;
            }
            else if (e.KeyCode == Keys.F9)
            {
                FilterFlyotPnl.ShowPopup();
                alreadyShowed = true;
            }
        }
        protected override void ListGrdViw_Click_Helper(int index)
        {
            //List<DBModels.Invoice> view = ListGrdViw.DataSource as List<DBModels.Invoice>;
            //if (view != null && view.Count > 0)
            //{
            //    //BillMemo.Text = view[index].Notes;
            //    //ShippingAddrMemo.Text = view[index].Shipping_Address;
            //}
            //if (index.Count() <= 0) return;
            //List<DBModels.Invoice> tempInv = view.DataSource as List<DBModels.Invoice>;
            //if (tempInv != null)
            //{
            //    BillMemo.Text = tempInv[index].Notes;
            //}
        }
        private void ListGrdViw_RowCountChanged(object sender, EventArgs e)
        {
            if (ListGrdViw.DataRowCount <= 0) return;
            TotalDiscountSpn.EditValue = ListGrdViw.Columns["Discount_Val"].SummaryItem.SummaryValue;
            TotalTaxSpn.EditValue = ListGrdViw.Columns["Tax_Val"].SummaryItem.SummaryValue;
            TotalExpenceSpn.EditValue = ListGrdViw.Columns["Expences"].SummaryItem.SummaryValue;
            TotalNetSpn.EditValue = ListGrdViw.Columns["Net"].SummaryItem.SummaryValue;
            TotalPaidSpn.EditValue = ListGrdViw.Columns["Paid"].SummaryItem.SummaryValue;
            TotalRemainSpn.EditValue = ListGrdViw.Columns["Remaining"].SummaryItem.SummaryValue;
        }
    }
}
