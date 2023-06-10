using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
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

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Products_List : Frm_Master_List
    {
        public Frm_Products_List()
        {
            InitializeComponent();
            Refresh_Data();
        }
        private void Frm_Products_List_Load(object sender, EventArgs e)
        {
            //SaveBtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //DeleteBtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            ListGrdViw.CustomColumnDisplayText += new CustomColumnDisplayTextEventHandler(ListGrdViw_CustomColumnDisplayText);
            ListGrdCtrl.ViewRegistered += new ViewOperationEventHandler(ListGrdCtrl_ViewRegistered);
            ListGrdViw.OptionsDetail.ShowDetailTabs = false;
            ListGrdViw.Grid_View_Translate_Column("Product");
            ListGrdViw.BestFitColumns();
            
        }
        private void ListGrdViw_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "Type")
            {
                e.DisplayText = Master_Class.product_Type.Single(x => x.ID == Convert.ToByte(e.Value)).Name;
            }
            else if (e.Column.FieldName == "Cost_Calc_Method")
            {
                e.DisplayText = Master_Class.Cost_Calc_Method_List.Single(x => x.ID == Convert.ToByte(e.Value)).Name;
            }
            /*
             هذا الحدث يسمح لنا بعرض قيمة لحقل معه جدول ثاني او نعرض له قيمة ثانية
             */
        }
        void ListGrdCtrl_ViewRegistered(object sender, ViewOperationEventArgs e)
        {
            if (e.View.LevelName == "pro_Unit")
            {
                GridView view = e.View as GridView;
                view.BestFitColumns();
                view.OptionsView.ShowViewCaption = true;
                view.ViewCaption = "وحدات القياس";
                view.Columns["pro_Un_ID"].Visible = false;
                view.Columns["Unit_ID"].Visible = false;
                view.Columns["Unit_Name"].Caption = "إسم الوحدة";
                view.Columns["Factor"].Caption = "المعامل";
                view.Columns["Buy_Price"].Caption = "سعر الشراء";
                view.Columns["Sell_Price"].Caption = "سعر البيع";
                view.Columns["Sell_Discount"].Caption = "خصم البيع";
                view.Columns["BarCode"].Caption = "الباركود";
            }
        }
        protected override void Refresh_Data()
        {
            ListGrdCtrl.DataSource = Session.Full_products;
            base.Refresh_Data();
        }
        protected override void New()
        {
            Frm_Main_Window.OpenForms(nameof(Frm_Add_Product));
            base.New();
        }
        protected override void Open_Form(int id)
        {
            Frm_Main_Window.OpenForm(new Frm_Add_Product(id));
        }
        protected override void Open_New_Window()
        {
            Frm_Main_Window.OpenForm(new Frm_Products_List(), false);
        }
    }
}
