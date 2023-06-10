using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.MyForms
{
    public class Customer: Frm_Add_Customer_Supplier
    {
        public Customer() : base(true)
        {

        }
        public Customer(int person_Id) : base(true, person_Id)
        {

        }
    }
    public class Supplier : Frm_Add_Customer_Supplier
    {
        public Supplier() : base(false)
        {

        }
        public Supplier(int person_Id) : base(false, person_Id)
        {

        }
    }
    public class Customer_List : Frm_Customer_Supplier_List
    {
        public Customer_List() : base(true)
        {

        }
    }
    public class Supplier_List : Frm_Customer_Supplier_List
    {
        public Supplier_List() : base(false)
        {

        }
    }
}
