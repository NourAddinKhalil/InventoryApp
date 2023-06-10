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
    public partial class Frm_Drawer_List : Frm_Master_List
    {
        public Frm_Drawer_List()
        {
            InitializeComponent();
            Refresh_Data();
        }
        protected override void New()
        {
            Frm_Main_Window.OpenForms(nameof(Frm_Add_Drawer));
            base.New();
        }
        protected override void Refresh_Data()
        {
            ListGrdCtrl.DataSource = Session.Drawers;
            base.Refresh_Data();
        }
        private void Frm_Drawer_List_Load(object sender, EventArgs e)
        {
            DeleteBtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            SaveBtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            ListGrdViw.Grid_View_Translate_Column("Drawer");
            ListGrdViw.OptionsView.ColumnAutoWidth = true;
            ListGrdViw.BestFitColumns();
        }
        protected override void Open_Form(int id)
        {
            Frm_Main_Window.OpenForm(new Frm_Add_Drawer(id));
        }
    }
}
