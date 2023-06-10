using System;
using System.Collections.Generic;

namespace TheFinalSalesProject.DBModels
{
    public class Max_Invoice : Invoice
    {
        public string PersonName { get; set; }
        public string StoreName { get; set; }
        public string DrawerName { get; set; }
        public string Real_Name { get; set; }
        public List<FullInvoiceBack> InvoiceBack { get; set; } = new List<FullInvoiceBack>();
        public List<FullInvoiceDetail> InvoiceDetail { get; set; } = new List<FullInvoiceDetail>();
    }
    public class FullInvoiceBack
    {
        public int BID { get; set; }
        public string BCode { get; set; }
        public DateTime BDate { get; set; }
        public string BStoreName { get; set; }
        public double BTotal { get; set; }
        public double BDiscount_Val { get; set; }
        public double BTax_Val { get; set; }
        public double BExpences { get; set; }
        public double BNet { get; set; }
        public double BPaid { get; set; }
        public string BDrawerName { get; set; }
        public double BRemaining { get; set; }
        public string BReal_Name { get; set; }
    }
    public class FullInvoiceDetail
    {
        public int DetID { get; set; }
        public int Invoice_ID { get; set; }
        public int Product_ID { get; set; }
        public string PCode { get; set; }
        public string ProName { get; set; }
        public int Unit_ID { get; set; }
        public string UnitName { get; set; }
        public double Qty { get; set; }
        public double Price { get; set; }
        public double Total_Price { get; set; }
        public double Cost { get; set; }
        public double Total_Cost { get; set; }
        public double DDiscount_Val { get; set; }
        public int DStore_ID { get; set; }
        public string DStoreName { get; set; }
    }
}
