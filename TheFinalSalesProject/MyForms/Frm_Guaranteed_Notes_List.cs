using DevExpress.XtraEditors.Repository;
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
    public partial class Frm_Guaranteed_Notes_List : Frm_Master_List
    {
        //private RepositoryItemLookUpEdit repoBranchLokEdit = new RepositoryItemLookUpEdit();
        //private RepositoryItemLookUpEdit repoDrawerLokEdit = new RepositoryItemLookUpEdit();
        //private RepositoryItemLookUpEdit repoPersonLokEdit = new RepositoryItemLookUpEdit();
        //private RepositoryItemLookUpEdit repoPersonTypeLokEdit = new RepositoryItemLookUpEdit();
        //private RepositoryItemLookUpEdit repoUsersLokEdit = new RepositoryItemLookUpEdit();
        private bool Is_Cash_In;
        public Frm_Guaranteed_Notes_List(bool Is_Cash_In)
        {
            InitializeComponent();
            this.Is_Cash_In = Is_Cash_In;
            Set_Form_Type();
            ListGrdViw.CustomColumnDisplayText += ListGrdViw_CustomColumnDisplayText;
            ListGrdCtrl.DataSource = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Full_Guarnteed_Note>(
                @"Select * From [FinalSalesDB].[dbo].[FullGuaranteedView]
                  Where [Is_Cash_In] = @isIn",
                new
                {
                    isIn = Is_Cash_In
                });
        }
        private void Frm_Guaranteed_Notes_List_Load(object sender, EventArgs e)
        {
            ListGrdViw.Add_NumberColumn_To_GridView(ListGrdCtrl);
            ListGrdViw.Grid_View_Translate_Column("GuaraNote");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "PersonName");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "Part_Type");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "StoreName");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "inv_Type_Name");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "Note");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "Real_Name");
            ListGrdViw.Add_Memo_Repo_Item(ListGrdCtrl, "DrawerName");
        }
        private void ListGrdViw_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "Part_Type")
            {
                e.DisplayText = Master_Class.part_Type_List.FirstOrDefault(x => x.ID == Convert.ToByte(e.Value)).Name;
            }
        }
        private void Set_Form_Type()
        {
            if (Is_Cash_In)
            {
                this.Text = " كشف سندات القبض ";
            }
            else
            {
                this.Text = " كشف سندات الدفع ";

            }
        }
        protected override void New()
        {
            Frm_Main_Window.OpenForm(new Frm_Cashes_Note_Guaranteed(Is_Cash_In));
            base.New();
        }
        protected override void Open_New_Window()
        {
            Frm_Main_Window.OpenForm(new Frm_Guaranteed_Notes_List(this.Is_Cash_In), false);
        }
        protected override void Open_Form(int id)
        {
            Frm_Main_Window.OpenForm(new Frm_Cashes_Note_Guaranteed(Is_Cash_In, id));
        }
    }
}
