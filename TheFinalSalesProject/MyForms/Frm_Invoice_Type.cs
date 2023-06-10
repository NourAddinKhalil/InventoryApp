using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.MyForms
{
    public class Frm_Buy_Bill : Frm_Invoice
    {
        public Frm_Buy_Bill() : base(bill_Type.Buy)
        {

        }
        public Frm_Buy_Bill(int id) : base(bill_Type.Buy, id)
        {

        }
    }
    public class Frm_Sale_Bill : Frm_Invoice
    {
        public Frm_Sale_Bill() : base(bill_Type.Sale)
        {

        }
        public Frm_Sale_Bill(int id) : base(bill_Type.Sale, id)
        {

        }
    }
    public class Frm_Sale_Bill_Return : Frm_Invoice
    {
        public Frm_Sale_Bill_Return() : base(bill_Type.SaleReturn)
        {
            
        }
        public Frm_Sale_Bill_Return(int id) : base(bill_Type.SaleReturn, id)
        {

        }
    }
    public class Frm_Buy_Bill_Return : Frm_Invoice
    {
        public Frm_Buy_Bill_Return() : base(bill_Type.BuyReturn)
        {

        }
        public Frm_Buy_Bill_Return(int id) : base(bill_Type.BuyReturn, id)
        {

        }
    }
    public class Frm_Buy_Bill_Return_List : Frm_Invoice_List
    {
        public Frm_Buy_Bill_Return_List() : base(bill_Type.BuyReturn)
        {

        }
    }
    public class Frm_Sale_Bill_Return_List : Frm_Invoice_List
    {
        public Frm_Sale_Bill_Return_List() : base(bill_Type.SaleReturn)
        {

        }
    }
    public class Frm_Sale_Bill_List : Frm_Invoice_List
    {
        public Frm_Sale_Bill_List() : base(bill_Type.Sale)
        {

        }
    }
    public class Frm_Buy_Bill_List : Frm_Invoice_List
    {
        public Frm_Buy_Bill_List() : base(bill_Type.Buy)
        {

        }
    }
}
