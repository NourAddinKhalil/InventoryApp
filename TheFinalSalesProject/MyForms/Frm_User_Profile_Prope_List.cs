using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TheFinalSalesProject.Classes.Translation;

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_User_Profile_Prope_List : Frm_Master_List
    {
        public Frm_User_Profile_Prope_List()
        {
            InitializeComponent();
            Refresh_Data();
        }
        protected override void Open_New_Window()
        {
            Frm_Main_Window.OpenForm(new Frm_User_Profile_Prope_List(), false);
        }
        protected override void Refresh_Data()
        {
            ListGrdCtrl.DataSource = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Profile_Prope_Name>(
                @"Select * From Profile_Prope_Name");
            base.Refresh_Data();
        }
        protected override void New()
        {
            Frm_Main_Window.OpenForm(new Frm_Add_User_Setting_Prope(), false);
            base.New();
        }
        private void Frm_User_Profile_Prope_List_Load(object sender, EventArgs e)
        {
            ListGrdViw.Grid_View_Translate_Column("Prope_Names");
            ListGrdViw.BestFitColumns();
        }
        protected override void Open_Form(int id)
        {
            Frm_Main_Window.OpenForm(new Frm_Add_User_Setting_Prope(id), false);
        }
    }
}
