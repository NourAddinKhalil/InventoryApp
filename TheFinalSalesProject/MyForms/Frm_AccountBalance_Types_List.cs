using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.MyForms
{
    public class Customers_Accounts_List: Frm_Accounts_Balances_List
    {
        public Customers_Accounts_List() : base(Classes.Enum_Choices.Accounts_Balance_Type.Customer)
        {
            this.Text = " كشف حسابات العملاء ";
        }
    }
    public class Suppliers_Accounts_List : Frm_Accounts_Balances_List
    {
        public Suppliers_Accounts_List() : base(Classes.Enum_Choices.Accounts_Balance_Type.Supplier)
        {
            this.Text = " كشف حسابات الموردين ";
        }
    }
    public class Employees_Accounts_List : Frm_Accounts_Balances_List
    {
        public Employees_Accounts_List() : base(Classes.Enum_Choices.Accounts_Balance_Type.Employee)
        {
            this.Text = " كشف حسابات الموظفين ";
        }
    }
    public class Drawers_Accounts_List : Frm_Accounts_Balances_List
    {
        public Drawers_Accounts_List() : base(Classes.Enum_Choices.Accounts_Balance_Type.Drawer)
        {
            this.Text = " كشف حسابات الخزائن ";
        }
    }
}
