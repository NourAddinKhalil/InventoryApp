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
    public partial class Frm_Customer_Supplier_List : Frm_Master_List
    {
        bool is_customer;
        public Frm_Customer_Supplier_List(bool is_customer)
        {
            InitializeComponent();
            this.is_customer = is_customer;
            this.Text = is_customer ? "قائمة العملاء" : "قائمة الموردين";
            Refresh_Data();
            ListGrdViw.Grid_View_Translate_Column("Person");
        }
        protected override void New()
        {
            Frm_Main_Window.OpenForms((is_customer) ? nameof(Customer) : nameof(Supplier));
            //Refresh_Data();
            base.New();
        }
        protected override void Refresh_Data()
        {
            ListGrdCtrl.DataSource = (is_customer) ? Session.Customers : Session.Suppliers;
            base.Refresh_Data();
        }
        protected override void Open_Form(int id)
        {
            if (is_customer)
                Frm_Main_Window.OpenForm(new Customer(id));
            else
                Frm_Main_Window.OpenForm(new Supplier(id));
        }
    }
}
