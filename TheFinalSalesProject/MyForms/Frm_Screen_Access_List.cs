using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Screen_Access_List : Frm_Master_List
    {
        public Frm_Screen_Access_List()
        {
            InitializeComponent();
            Refresh_Data();
        }

        private void Frm_Screen_Access_List_Load(object sender, EventArgs e)
        {
            ListGrdViw.Columns["ID"].Visible = false;
            ListGrdViw.Columns["Name"].Caption = "إسم نموذج الصلاحية";
        }
        protected override void Refresh_Data()
        {
            ListGrdCtrl.DataSource = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Screen_Roles_Name>("Select * From Screen_Roles_Name");
            base.Refresh_Data();
        }
        protected override void New()
        {
            Frm_Main_Window.OpenForms(nameof(Frm_Add_User_Screen_Access));
            Refresh_Data();
            base.New();
        }
        protected override void Open_Form(int id)
        {
            Frm_Main_Window.OpenForm(new Frm_Add_User_Screen_Access(id));
        }
        protected override void Open_New_Window()
        {
            Frm_Main_Window.OpenForm(new Frm_Screen_Access_List(), false);
        }
    }
}
