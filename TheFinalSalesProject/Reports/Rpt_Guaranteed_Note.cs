using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using TheFinalSalesProject.Classes;
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.Reports
{
    public partial class Rpt_Guaranteed_Note : DevExpress.XtraReports.UI.XtraReport
    {
        public Rpt_Guaranteed_Note(bool isCashIn)
        {
            InitializeComponent();
            CompanyName.Text = Classes.Session.Company_Info.Name;
            CompanyAddress.Text = Classes.Session.Company_Info.Address;
            CompanyAddress.Text = Classes.Session.Company_Info.Phone;
            if (Classes.Session.Company_Info.Logo != null)
                CompanyPic.Image = Classes.Master_Class.Convert_Byte_To_Image(Classes.Session.Company_Info.Logo);
            GuarabteedLabel.Text = (isCashIn) ? " سند قبض " : " سند صرف ";
        }
        private void Bind_Data()
        {
            invoiceNumberLbl.DataBindings.Add("Text", this.DataSource, "ID");
            xrBarCode1.DataBindings.Add("Text", this.DataSource, "Code");
            invoiceDateLbl.DataBindings.Add("Text", this.DataSource, "Date");
            StoreNameLbl.DataBindings.Add("Text", this.DataSource, "StoreName");
            PersonName.DataBindings.Add("Text", this.DataSource, "PersonName");
            Paid.DataBindings.Add("Text", this.DataSource, "Amount");
            Discount.DataBindings.Add("Text", this.DataSource, "Discount_Val");
            UsernNameLbl.DataBindings.Add("Text", this.DataSource, "Real_Name");
            PersonPhone.DataBindings.Add("Text", this.DataSource, "Phone");
            PersonMobile.DataBindings.Add("Text", this.DataSource, "Mobile");
            PaidLetter.DataBindings.Add("Text", this.DataSource, "Total_Lett");
            Notes.DataBindings.Add("Text", this.DataSource, "Note");
            InvID.DataBindings.Add("Text", this.DataSource, "Invoice_ID");
            InvType.DataBindings.Add("Text", this.DataSource, "inv_Type_Name");
        }
        public static void Print(object dS , bool isCashIn)
        {
            Reports.Rpt_Guaranteed_Note rpt_Note = new Reports.Rpt_Guaranteed_Note(isCashIn);
            rpt_Note.DataSource = dS;
            rpt_Note.Bind_Data();
            switch (Session.Printer_Settings.PrintMode)
            {
                case Print_Mode.Directly:
                    rpt_Note.Print();
                    break;
                case Print_Mode.ShowDialog:
                    rpt_Note.PrintDialog();
                    break;
                case Print_Mode.ShowPreview:
                    rpt_Note.ShowPreview();
                    break;
                default:
                    break;
            }
        }
    }
}
