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
    public partial class Frm_Products_Balances_List : Frm_Master_List
    {
        public Frm_Products_Balances_List()
        {
            InitializeComponent();
            ListGrdViw.CustomColumnDisplayText += ListGrdViw_CustomColumnDisplayText;
            Refresh_Data();
        }
        protected override void Refresh_Data()
        {
            ListGrdCtrl.DataSource = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Full_Pro_Store_Move>(
               @"[FinalSalesDB].[dbo].[Get_Full_Pro_Store_Move]", isCommandText: false);
            base.Refresh_Data();
        }
        private void Frm_Products_Balances_List_Load(object sender, EventArgs e)
        {
            ListGrdViw.Grid_View_Translate_Column("Pro_Balance");

            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "Notes");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "Real_Name");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "ProductName");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "StoreName");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "Source_Type");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "CateName");

            ListGrdViw.Add_Spn_Repo_Value(ListGrdCtrl, "Import_Qty");
            ListGrdViw.Add_Spn_Repo_Value(ListGrdCtrl, "Export_Qty");
            ListGrdViw.Add_Spn_Repo_Value(ListGrdCtrl, "Cost_Value");

            ListGrdViw.Add_Sum_Summary_To_Grid_Footer("Import_Qty");
            ListGrdViw.Add_Sum_Summary_To_Grid_Footer("Export_Qty");
            ListGrdViw.Add_Sum_Group_Summary_To_Grid_Footer("Export_Qty");
            ListGrdViw.Add_Sum_Group_Summary_To_Grid_Footer("Export_Qty");
            ListGrdViw.OptionsView.ShowAutoFilterRow = true;
        }
        private void ListGrdViw_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value == null) return;
            if (e.Column.FieldName == "Source_Type")
            {
                e.DisplayText = Master_Class.Source_Type_List.FirstOrDefault(x => x.ID == Convert.ToByte(e.Value)).Name;
            }
        }
        public override void Print()
        {
            Reports.Rpt_Print_Any_Grid_List.Print(ListGrdCtrl, "تقرير حركة الأصناف", "");
            base.Print();
        }
    }
}
