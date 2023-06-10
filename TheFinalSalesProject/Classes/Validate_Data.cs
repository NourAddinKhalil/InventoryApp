using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.Classes
{
    public static class Validate_Data
    {
        public static void Initilaze_Data(this LookUpEdit lkp, object dataSource, string valueMember = "ID", string disblayMember = "Name", bool clear = false)
        {
            lkp.Properties.DataSource = dataSource;
            lkp.Properties.DisplayMember = disblayMember;
            lkp.Properties.ValueMember = valueMember;
            lkp.Properties.PopulateColumns();
            lkp.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            //lkp.Properties.Columns[valueMember].Visible = false;
            lkp.Properties.NullText = "";
            if (clear)
            {//عملنا هكذا عشان الاعدادات ذي يختار المخزن الافتراضي
                //لانو ما عنقدرش نختار من تيك الغجة ذي هانك
                lkp.Properties.Columns.Clear();
                lkp.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                {
                    FieldName = disblayMember,
                });
                //lkp.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[]
                //{
                //    //new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                //    //{
                //    //    FieldName = valueMember,
                //    //},
                //    new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
                //    {
                //        FieldName = disblayMember,
                //    }
                //});
                lkp.Properties.ShowHeader = false;
            }
        }
        public static void Initilaze_Data(this GridLookUpEdit glkp, object dataSource, string valueMember = "ID", string disblayMember = "Name")
        {
            glkp.Properties.DataSource = null;
            glkp.Properties.DataSource = dataSource;
            glkp.Properties.DisplayMember = disblayMember;
            glkp.Properties.ValueMember = valueMember;
            glkp.Properties.NullText = "";
            glkp.Properties.ValidateOnEnterKey = true;//when pressed enter key it'll added dirictly on tool
            glkp.Properties.AllowNullInput = DefaultBoolean.False;
            glkp.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            glkp.Properties.ImmediatePopup = true;//auto complete
            GridView repglkp = glkp.Properties.View;
            repglkp.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;
            repglkp.OptionsSelection.UseIndicatorForSelection = true;
            repglkp.OptionsView.ShowAutoFilterRow = true;
            repglkp.BestFitColumns();
            repglkp.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.ShowAlways;
            repglkp.PopulateColumns(glkp.Properties.DataSource);
            repglkp.Columns[valueMember].Visible = false;
        }
        public static void Initilaze_Data(this RepositoryItemLookUpEditBase repoItem, object dataSource, GridControl gCtrl, GridColumn gClmn, string valueMember = "ID", string disblayMember = "Name")
        {
            //if (repoItem == null)
            //    repoItem = new RepositoryItemLookUpEdit();
            repoItem.DataSource = dataSource;
            repoItem.ValueMember = valueMember;
            repoItem.DisplayMember = disblayMember;
            repoItem.NullText = "";
            repoItem.BestFitMode = BestFitMode.BestFitResizePopup;
            if (gClmn != null)
                gClmn.ColumnEdit = repoItem;
            if (gCtrl != null)
                gCtrl.RepositoryItems.Add(repoItem);
            repoItem.ValidateOnEnterKey = true;//when pressed enter key it'll added dirictly on tool
            repoItem.AllowNullInput = DefaultBoolean.False;
            repoItem.BestFitMode = BestFitMode.BestFitResizePopup;
            repoItem.ImmediatePopup = true;//auto complete
            repoItem.TextEditStyle = TextEditStyles.Standard;
            repoItem.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
            if (repoItem is RepositoryItemGridLookUpEdit)
            {
                GridView repoProlkpV = ((RepositoryItemGridLookUpEdit)repoItem).View;
                repoProlkpV.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;
                repoProlkpV.OptionsSelection.UseIndicatorForSelection = true;
                repoProlkpV.OptionsView.ShowAutoFilterRow = true;
                repoProlkpV.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.ShowAlways;
            }
        }
        public static bool Is_The_Text_Valid(this TextEdit txt , bool showTxt = true)
        {
            if (txt.Text.Trim() == string.Empty)
            {
                if(showTxt)
                txt.ErrorText = Messages.Necessary_Field;
                return false;
            }
            return true;
        }
        public static bool Is_The_Lkp_Edit_Value_Of_Type_Int(this LookUpEditBase lkp)
        {
            return (lkp.EditValue is int || lkp.EditValue is byte);
        }
        public static bool Is_The_Lkp_Text_Valid(this LookUpEditBase lkp, bool showError = true)
        {
            if (lkp.Is_The_Lkp_Edit_Value_Of_Type_Int() == false || Convert.ToInt32(lkp.EditValue) == 0)
            {
                if (showError)
                    lkp.ErrorText = Messages.Necessary_Field;
                return false;
            }
            return true;
        }
        public static bool Is_The_Date_Valid(this DateEdit dt , bool showError = true)
        {
            if (dt.DateTime.Year < 1950)
            {
                if (showError)
                    dt.ErrorText = Messages.Necessary_Field;
                return false;
            }
            return true;
        }
        public static bool Is_The_Edit_Value_Less_Than_Zero(this BaseEdit edit, bool showError = true)
        {
            if (edit.EditValue == null || Convert.ToDouble(edit.EditValue) < 0)
            {
                if (showError)
                    edit.ErrorText = "يجب أن تكون القيمة موجبة";
                return false;
            }
            return true;
        }
        public static bool Is_The_Edit_Value_More_Than_Zero(this BaseEdit edit, bool showError = true)
        {
            if (edit.EditValue == null || Convert.ToDouble(edit.EditValue) <= 0)
            {
                if (showError)
                    edit.ErrorText = "يجب أن تكون القيمة أكبر من الصفر";
                return false;
            }
            return true;
        }
    }
}
