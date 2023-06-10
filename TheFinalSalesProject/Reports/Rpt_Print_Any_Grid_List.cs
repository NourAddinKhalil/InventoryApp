using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;

namespace TheFinalSalesProject.Reports
{
    public partial class Rpt_Print_Any_Grid_List : DevExpress.XtraReports.UI.XtraReport
    {
        public Rpt_Print_Any_Grid_List()
        {
            InitializeComponent();
            CmpnameLbl.Text = Classes.Session.Company_Info.Name;
            CmpnAddrLbl.Text = Classes.Session.Company_Info.Address;
            CmpPhonLbl.Text = Classes.Session.Company_Info.Phone + "/" + Classes.Session.Company_Info.Mobile;
            if (Classes.Session.Company_Info.Logo != null)
                CmpPic.Image = Classes.Master_Class.Convert_Byte_To_Image(Classes.Session.Company_Info.Logo);
        }
        public static void Print(GridControl control, string rptName, string filter)
        {
            GridView view = control.MainView as GridView;
            GridMultiSelectMode mode = view.OptionsSelection.MultiSelectMode;
            //view.Columns["Number"].MaxWidth = 20;
            view.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            view.AppearancePrint.EvenRow.BackColor = view.Appearance.EvenRow.BackColor;
            view.AppearancePrint.OddRow.BackColor = view.Appearance.OddRow.BackColor;
            view.OptionsPrint.EnableAppearanceEvenRow =
            view.OptionsPrint.EnableAppearanceOddRow = true;
            view.AppearancePrint.HeaderPanel.BackColor = Color.FromArgb(0, 0, 192);
            view.AppearancePrint.HeaderPanel.Font = new Font(view.AppearancePrint.HeaderPanel.Font, FontStyle.Bold);
            view.AppearancePrint.HeaderPanel.ForeColor = Color.WhiteSmoke;
            view.AppearancePrint.HeaderPanel.Options.UseBackColor = true;
            view.AppearancePrint.HeaderPanel.Options.UseFont = true;
            view.AppearancePrint.HeaderPanel.Options.UseForeColor = true;
            view.OptionsPrint.AllowMultilineHeaders = true;//wordrap
            view.OptionsPrint.AutoWidth = true;
            view.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            view.AppearancePrint.Lines.BackColor = Color.Honeydew;
            view.AppearancePrint.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            view.OptionsPrint.UsePrintStyles = true;
            view.OptionsPrint.PrintFooter = false;
            Rpt_Print_Any_Grid_List listPrint = new Rpt_Print_Any_Grid_List();
            view.AppearancePrint.Row.Font = listPrint.Detail.Font;
            view.AppearancePrint.HeaderPanel.Font = new Font(view.AppearancePrint.HeaderPanel.Font.Name, listPrint.Detail.Font.Size);
            //view.ColumnPanelRowHeight = 70;
            //Rpt_Print_Any_Grid_List listPrint = new Rpt_Print_Any_Grid_List();
            listPrint.FilterCel.Text = filter;
            listPrint.ReportName.Text = rptName;
            PrintableComponentLink link = new PrintableComponentLink();
            link.Component = control;
            listPrint.printableComponentContainer1.PrintableComponent = link;
            listPrint.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            listPrint.ShowRibbonPreviewDialog();
            view.OptionsSelection.MultiSelectMode = mode;
        }
    }
}
