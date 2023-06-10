using Dapper;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TheFinalSalesProject.Classes;
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Accounts_Balances_List : Frm_Master_List
    {
        private Accounts_Balance_Type type;
        private string rptName = " تقارير ";
        public Frm_Accounts_Balances_List(Accounts_Balance_Type type)
        {
            InitializeComponent();
            this.type = type;
            Refresh_Data();
            ListGrdViw.CustomColumnDisplayText += ListGrdViw_CustomColumnDisplayText;
            ListGrdViw.RowCountChanged += ListGrdViw_RowCountChanged;
        }
        private void Get_Coupes(List<int> ids)
        {
            var accountID = new
            {
                accountID = Convert_List_To_DataTable.Convert_ID_List_To_Table(ids).
                AsTableValuedParameter("[dbo].[IDs_List_Type]")
            };
            ListGrdCtrl.DataSource = DAL.Impelement_Stored_Procedure.SelectData<Table_View.Full_Coupes_Account>(
                "[FinalSalesDB].[dbo].[Get_Full_Coupes_Of_Account]", accountID, false);
        }
        protected override void Refresh_Data()
        {
            switch (type)
            {
                case Accounts_Balance_Type.Supplier:
                    Get_Coupes(Session.Suppliers.Select(x => x.Account_ID).ToList());
                    rptName += "حسابات الموردين ";
                    break;
                case Accounts_Balance_Type.Customer:
                    Get_Coupes(Session.Customers.Select(x => x.Account_ID).ToList());
                    rptName += "حسابات العملاء ";
                    break;
                case Accounts_Balance_Type.Employee:
                    rptName += "حسابات الموظفين ";
                    break;
                case Accounts_Balance_Type.Drawer:
                    Get_Coupes(Session.Drawers.Select(x => x.Account_ID).ToList());
                    rptName += "حسابات الخزائن ";
                    break;
                default:
                    break;
            }
            base.Refresh_Data();
        }
        private void Frm_Accounts_Balances_List_Load(object sender, EventArgs e)
        {
            ListGrdCtrl.BringToFront();
            ListGrdViw.Add_NumberColumn_To_GridView(ListGrdCtrl);
            ListGrdViw.Grid_View_Translate_Column("FullCoupes");
            ListGrdViw.OptionsView.ColumnAutoWidth = true;
            ListGrdViw.Add_Sum_Summary_To_Grid_Footer("Credit");
            ListGrdViw.Add_Sum_Summary_To_Grid_Footer("Debit");
            ListGrdViw.Add_Sum_Group_Summary_To_Grid_Footer("Debit");
            ListGrdViw.Add_Sum_Group_Summary_To_Grid_Footer("Credit");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "Notes");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "Name");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "Source_Type");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "Move_Name");
            ListGrdViw_RowCountChanged(null, null);
            ListGrdViw.OptionsView.ShowFooter = false;
            ListGrdViw.BestFitColumns();
        }
        private void ListGrdViw_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "Source_Type")
            {
                e.DisplayText = Master_Class.Source_Type_List.FirstOrDefault(x => x.ID == Convert.ToByte(e.Value)).Name;
            }
            else if (e.Column.FieldName == "Move_Name")
            {
                e.DisplayText = Master_Class.Moves_Name_List.FirstOrDefault(x => x.ID == Convert.ToByte(e.Value)).Name;
            }
        }
        private void ListGrdViw_RowCountChanged(object sender, EventArgs e)
        {
            SpnCreditSum.EditValue = Convert.ToDouble(ListGrdViw.Columns["Credit"].SummaryItem.SummaryValue);
            SpnDebitSum.EditValue = Convert.ToDouble(ListGrdViw.Columns["Debit"].SummaryItem.SummaryValue);
            SpnNet.EditValue = Convert.ToDouble(SpnCreditSum.EditValue) - Convert.ToDouble(SpnDebitSum.EditValue);
        }
        public override void Print()
        {
            Reports.Rpt_Print_Any_Grid_List.Print(ListGrdCtrl, rptName, "No Filters");
            base.Print();
        }
    }
}
