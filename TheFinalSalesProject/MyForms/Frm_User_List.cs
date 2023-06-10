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
    public partial class Frm_User_List : Frm_Master_List
    {
        public Frm_User_List()
        {
            InitializeComponent();
            Refresh_Data();
            ListGrdViw.CustomColumnDisplayText += ListGrdViw_CustomColumnDisplayText;
        }
        private void ListGrdViw_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if(e.Column.FieldName == "Type")
            {
                e.DisplayText = Master_Class.User_Type_List.Single(x => x.ID == Convert.ToByte(e.Value)).Name;
            }
            if(e.Column.FieldName == "Password")
            {
                e.DisplayText = "**************";
            }
        }
        protected override void Refresh_Data()
        {
            ListGrdCtrl.DataSource = DAL.Impelement_Stored_Procedure.SelectData<DBModels.User>(
                @"Select * From [FinalSalesDB].[dbo].[User]");
            base.Refresh_Data(); 
        }
        protected override void New()
        {
            Frm_Main_Window.OpenForms(nameof(Frm_Add_User));
            Refresh_Data();
            base.New();
        }
        protected override void Open_New_Window()
        {
            Frm_Main_Window.OpenForm(new Frm_User_List(), false);
        }
        protected override void Open_Form(int id)
        {
            Frm_Main_Window.OpenForm(new Frm_Add_User(id));
            Refresh_Data();
        }
        private void Frm_User_List_Load(object sender, EventArgs e)
        {
            ListGrdViw.Grid_View_Translate_Column("User");
        }
    }
}
