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
    public partial class Frm_Stores_List : Frm_Master_List
    {
        public Frm_Stores_List()
        {
            InitializeComponent();
            Refresh_Data();
        }

        private void Frm_Stores_List_Load(object sender, EventArgs e)
        {
            ListGrdViw.Grid_View_Translate_Column("Store");
            ListGrdViw.OptionsView.ColumnAutoWidth = true;
            ListGrdViw.BestFitColumns();
        }
        protected override void New()
        {
            Frm_Main_Window.OpenForms(nameof(Frm_Add_Store));
            base.New();
        }
        protected override void Refresh_Data()
        {
            ListGrdCtrl.DataSource = Session.Stores;
            base.Refresh_Data();
        }
        protected override void Open_Form(int id)
        {
            Frm_Main_Window.OpenForm(new Frm_Add_Store(id));
        }
        protected override void Open_New_Window()
        {
            Frm_Main_Window.OpenForm(new Frm_Stores_List(), false);
        }
    }
}
