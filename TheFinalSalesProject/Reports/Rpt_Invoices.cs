using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using TheFinalSalesProject.Classes;
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.Reports
{
    public partial class Rpt_Invoices : DevExpress.XtraReports.UI.XtraReport
    {
        //bill_Type type;
        public Rpt_Invoices(bill_Type type)
        {
            InitializeComponent();
            //this.type = type;
            CompanyNameLbl.Text = Classes.Session.Company_Info.Name;
            CompanyAddressLbl.Text = Classes.Session.Company_Info.Address;
            CompanyPhoneLbl.Text = Classes.Session.Company_Info.Phone;
            BillTypeLbl.Text = (type == bill_Type.Buy) ? "فاتورة مشتريات" : 
                               (type == bill_Type.Sale) ? "فاتورة مبيعات" :
                               (type == bill_Type.BuyReturn) ? "فاتورة مرتجع شراء" : 
                               (type == bill_Type.SaleReturn) ? "فاتورة مرتجع بيع" : "Unkown";
        }
        private void Bind_Data()
        {
            CodeCell.DataBindings.Add("Text", this.DataSource, "ID");
            xrBarCode1.DataBindings.Add("Text", this.DataSource, "Code");
            DateCell.DataBindings.Add("Text", this.DataSource, "Date");
            BillDisCell.DataBindings.Add("Text", this.DataSource, "Discount_Val");
            TaxCell.DataBindings.Add("Text", this.DataSource, "Tax_Val");
            ExpencesCell.DataBindings.Add("Text", this.DataSource, "Expences");
            NetCell.DataBindings.Add("Text", this.DataSource, "Net");
            BillTotalPriCell.DataBindings.Add("Text", this.DataSource, "Total");
            PaidCell.DataBindings.Add("Text", this.DataSource, "Paid");
            RemainingCell.DataBindings.Add("Text", this.DataSource, "Remaining");
            BranchCell.DataBindings.Add("Text", this.DataSource, "StoreName");
            PartTypeNameCell.DataBindings.Add("Text", this.DataSource, "PersonName");
            PersonPhoneCell.DataBindings.Add("Text", this.DataSource, "Mobile");
            //هنا عملنا هكسبرشن لان هذي البيانات عيتكررين
            ProductNameCell.ExpressionBindings.Add(
                new ExpressionBinding("BeforePrint", "Text", "ProductName"));
            UnitNameCell.ExpressionBindings.Add(
                new ExpressionBinding("BeforePrint", "Text", "UnitName"));
            PriceCell.ExpressionBindings.Add(
                new ExpressionBinding("BeforePrint", "Text", "Price"));
            DiscountCell.ExpressionBindings.Add(
                new ExpressionBinding("BeforePrint", "Text", "DDiscount_Val"));
            TotalCell.ExpressionBindings.Add(
                new ExpressionBinding("BeforePrint", "Text", "Total_Price"));
            QuantityCell.ExpressionBindings.Add(
                new ExpressionBinding("BeforePrint", "Text", "Qty"));

            SourceIDCell.ExpressionBindings.Add(
                new ExpressionBinding("BeforePrint", "Visible", "Iif(Invoice_Back_ID > 0 , true , false)"));
            //SourceCaptionCel.ExpressionBindings.Add(
            //    new ExpressionBinding("BeforePrint", "Visible", "Iif(Invoice_Back_ID > 0 , true , false)"));
            SourceIDCell.ExpressionBindings.Add(
                new ExpressionBinding("BeforePrint", "Text", "Invoice_Back_ID"));
        }
        public static void Print(object dS , bill_Type type)
        {
            Reports.Rpt_Invoices rpt_Bills = new Reports.Rpt_Invoices(type);
            rpt_Bills.DataSource = dS;
            rpt_Bills.DetailReport.DataSource = rpt_Bills.DataSource;
            rpt_Bills.DetailReport.DataMember = "Details";
            rpt_Bills.Bind_Data();
            switch (Session.Printer_Settings.PrintMode)
            {
                case Print_Mode.Directly:
                    rpt_Bills.Print();
                    break;
                case Print_Mode.ShowDialog:
                    rpt_Bills.PrintDialog();
                    break;
                case Print_Mode.ShowPreview:
                    rpt_Bills.ShowPreview();
                    break;
                default:
                    break;

            }
        }
    }
}
