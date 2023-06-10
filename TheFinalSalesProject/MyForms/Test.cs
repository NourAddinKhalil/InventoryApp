using DevExpress.XtraEditors.Repository;
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

namespace TheFinalSalesProject.MyForms
{
    public partial class Test : DevExpress.XtraEditors.XtraForm
    {
        private RepositoryItemLookUpEdit repoUnitsLokEdit = new RepositoryItemLookUpEdit();
        public Test()
        {
            InitializeComponent();
            gridControl1.ViewRegistered += GridControl1_ViewRegistered;
            gridControl1.DataSource = DAL.Impelement_Stored_Procedure.Get_The_Max_Invoice("[dbo].[Get_The_Max_Invoice]");
            // gridView1.OptionsView.ShowViewCaption = true;
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
        }

        private void GridControl1_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            var view = e.View as GridView;
            view.OptionsView.ShowViewCaption = true;
            //view.OptionsDetail.EnableMasterViewMode = true;
            view.OptionsDetail.EnableDetailToolTip = true;
            view.OptionsDetail.ShowDetailTabs = true;
            //view.OptionsDetail.ShowEmbeddedDetailIndent = DevExpress.Utils.DefaultBoolean.True;
            if (e.View.LevelName == "InvoiceDetials")
                e.View.ViewCaption = "الأصناف";
            else if (e.View.LevelName == "InvoicesBack")
                e.View.ViewCaption = "المراجع";
        }

        private void Test_Load(object sender, EventArgs e)
        {
            //repoUnitsLokEdit.Initilaze_Data(DAL.Impelement_Stored_Procedure.SelectData<DBModels.Unit>(
            //    "Select * From Unit"), gridControl1, gridView1.Columns["Unit_ID"]);
            //repoUnitsLokEdit.PopulateColumns();
            //repoUnitsLokEdit.Repo_Look_Up_Edit_Translate_Column("Unit");
        }
    }
}
