using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
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
using static TheFinalSalesProject.Classes.Translation;

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Manually_Rigister : Frm_Master
    {
        private DBModels.Manually_Rigister rigister;
        private byte manually_Type = Convert.ToByte(Classes.Enum_Choices.Source_Type.Manually_Rigister);
        private RepositoryItemGridLookUpEdit sourceTypeRepo = new RepositoryItemGridLookUpEdit();
        private RepositoryItemGridLookUpEdit moveNameRepo = new RepositoryItemGridLookUpEdit();
        private RepositoryItemGridLookUpEdit accountsRepo = new RepositoryItemGridLookUpEdit();

        public Frm_Manually_Rigister()
        {
            InitializeComponent();
            ManuallyGridCtrl.ProcessGridKey += ManuallyGridCtrl_ProcessGridKey;
            ManuallyGridView.InvalidRowException += ManuallyGridView_InvalidRowException;
            ManuallyGridView.ValidateRow += ManuallyGridView_ValidateRow;
            ManuallyGridView.CellValueChanging += ManuallyGridView_CellValueChanging;
            ManuallyGridView.CellValueChanged += ManuallyGridView_CellValueChanged;
            New();
        }
        private void Frm_Manually_Rigister_Load(object sender, EventArgs e)
        {
            sourceTypeRepo.Initilaze_Data(Master_Class.Source_Type_List.Where(x => x.ID == manually_Type).ToList(), ManuallyGridCtrl,
               ManuallyGridView.Columns["Source_Type"]);
            sourceTypeRepo.View.PopulateColumns(sourceTypeRepo.DataSource);
            sourceTypeRepo.View.Grid_View_Translate_Column("Type");
            moveNameRepo.Initilaze_Data(Master_Class.Moves_Name_List, ManuallyGridCtrl,
                ManuallyGridView.Columns["Move_Name"]);
            moveNameRepo.View.PopulateColumns(moveNameRepo.DataSource);
            moveNameRepo.View.Grid_View_Translate_Column("Type");
            accountsRepo.Initilaze_Data(Session.Accounts, ManuallyGridCtrl,
                 ManuallyGridView.Columns["Account_ID"]);
            accountsRepo.View.PopulateColumns(accountsRepo.DataSource);
            accountsRepo.View.Grid_View_Translate_Column("Accounts");
            ManuallyGridView.Add_NumberColumn_To_GridView(ManuallyGridCtrl);
            ManuallyGridView.Grid_View_Translate_Column("Coupes");
            ManuallyGridView.Add_Spn_Repo_Value(ManuallyGridCtrl,"Debit");
            ManuallyGridView.Add_Spn_Repo_Value(ManuallyGridCtrl, "Credit");
            ManuallyGridView.Add_DeleteColumns_To_GridView(ManuallyGridCtrl);
            ManuallyGridView.Columns["Source_ID"].Visible = false;
            ManuallyGridView.Columns["Notes"].Width = 65;
            ManuallyGridView.Columns["Notes"].MinWidth = 60;
            ManuallyGridView.Columns["Source_Type"].OptionsColumn.AllowFocus = false;
        }
        protected override void New()
        {
            rigister = new DBModels.Manually_Rigister()
            {
                Date = DateTime.Now
            };
            base.New();
        }
        protected override void Get_Data()
        {
            ManuallyIDTxt.Text = rigister.ID.ToString();
            ManuallyDate.DateTime = rigister.Date;
            ManuallyMemo.Text = rigister.Notes;

            var data = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Coupes_Of_Account>(
                @"Select * From [FinalSalesDB].[dbo].[Coupes_Of_Account]
                  Where Source_ID = @sID And Source_Type = @stype",
                new
                {
                    sID = rigister.ID,
                    stype = manually_Type
                });
            BindingList<DBModels.Coupes_Of_Account> list = new BindingList<DBModels.Coupes_Of_Account>(data);
            ManuallyGridCtrl.DataSource = list;
            base.Get_Data();
        }
        protected override void Set_Data()
        {
            rigister.Date = ManuallyDate.DateTime;
            rigister.Notes = ManuallyMemo.Text.Trim();
            base.Set_Data();
        }
        protected override bool IsDataValid()
        {
            int numError = 0;
            int rowCount = ManuallyGridView.DataRowCount;
            if (rowCount <= 0)
            {
                numError++;
                XtraMessageBox.Show("أضف قيود أولاُ", " فشل الحفظ ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            var rows = ManuallyGridView.DataSource as BindingList<DBModels.Coupes_Of_Account>;
            int indx = 0;
            foreach (var row in rows)
            {
                ManuallyGridView_ValidateRow(null, new ValidateRowEventArgs(indx, row));
            }
            if (ManuallyGridView.HasColumnErrors)
                numError++;
            numError += ManuallyDate.Is_The_Date_Valid() ? 0 : 1;
            return (numError == 0);
        }
        protected override void Save()
        {
            if (rigister.ID == 0)
            {
                rigister.ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>(
                             @"INSERT INTO [FinalSalesDB].[dbo].[Manually_Rigister]
                                     ([Date]
                                     ,[Notes])
                               VALUES
                                     (@date,@note)", 
                             new
                             {
                                 date = rigister.Date,
                                 note = rigister.Notes
                             }).FirstOrDefault().ID;
            }
            else
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce(
                    @"UPDATE [FinalSalesDB].[dbo].[Manually_Rigister]
                       SET [Date] = @date
                          ,[Notes] = @note
                     WHERE [ID] = @id",
                     new
                     {
                         date = rigister.Date,
                         note = rigister.Notes,
                         id = rigister.ID
                     });
            }
            Delete_Data.Delete_Coupe_Of_Accounts(rigister.ID, manually_Type);
            var coupAccount = ManuallyGridView.DataSource as BindingList<DBModels.Coupes_Of_Account>;
            foreach (var item in coupAccount)
            {
                item.Source_ID = rigister.ID;
                item.Source_Type = manually_Type;
                item.User_ID = Session.user.ID;
            }
            var coupes = new
            {
                coupes = Convert_List_To_DataTable.Convert_Coupes_List_To_Table(bdata: coupAccount).
                AsTableValuedParameter("Account_Coupes_List_Type")
            };
            DAL.Impelement_Stored_Procedure.Excute_Proce("Add_List_Of_Coupes", coupes, isCommandText: false);
            base.Save();
            ManuallyIDTxt.Text = rigister.ID.ToString();
        }
        protected override void Delete()
        {
            Delete_Data.Delete_Coupe_Of_Accounts(rigister.ID, manually_Type);
            DAL.Impelement_Stored_Procedure.Excute_Proce(
                @"Delete From [FinalSalesDB].[dbo].[Manually_Rigister] Where [ID] = @id",
                new
                {
                    id = rigister.ID
                });
            base.Delete();
        }
        #region GridViewEvent
        private void ManuallyGridCtrl_ProcessGridKey(object sender, KeyEventArgs e)
        {
            GridControl control = sender as GridControl;
            if (control == null) return;
            GridView view = control.FocusedView as GridView;
            if (view == null) return;
            if (view.FocusedColumn == null) return;
            ManuallyGridView_ValidateRow(null, new ValidateRowEventArgs(
                view.FocusedRowHandle, view.GetRow(view.FocusedRowHandle)));
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                string focusedColumn = view.FocusedColumn.FieldName;
                if (view.FocusedRowHandle < 0)
                {
                    if (ManuallyGridView.HasColumnErrors)
                        return;
                    view.AddNewRow();
                    view.FocusedColumn = view.Columns[focusedColumn];
                }
                if (view.FocusedColumn.FieldName == "Code" || view.FocusedColumn.FieldName == "Account_ID")
                {
                    ManuallyGridCtrl_ProcessGridKey(sender, new KeyEventArgs(Keys.Tab));
                }
                e.Handled = true;
            }
            //هنا بنعمل حدث يدوي عشان يضيف الصنف لانو هذا الحدث بيتفعل قبل القريد فيو فلهذا 
            //بنعمل حدث ونخليه يسير التاب وبهدها يرجع ينفذ حق الانتر
            //طبعا المتغير عملنا عشان يعمل فوكس على العمود ذي تفعل الحدث منه اذا كان كود او صنف
            else if (e.KeyCode == Keys.Tab)
            {
                view.FocusedColumn = view.VisibleColumns[view.FocusedColumn.VisibleIndex + 1];
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                if (view.FocusedRowHandle >= 0)
                {
                    view.DeleteSelectedRows();
                }
            }
        }
        private void ManuallyGridView_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            if (e.Row == null || (e.Row as DBModels.Coupes_Of_Account).Account_ID == 0)
            {
                ManuallyGridView.DeleteRow(e.RowHandle);
                e.ExceptionMode = ExceptionMode.Ignore;
                return;
            }
            e.ExceptionMode = ExceptionMode.NoAction;
        }
        private void ManuallyGridView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            var row = e.Row as DBModels.Coupes_Of_Account;
            if (row == null || row.Account_ID == 0)
            {
                e.Valid = false;
                return;
            }
            if(row.Credit < 0)
            {
                e.Valid = false;
                ManuallyGridView.SetColumnError(ManuallyGridView.Columns["Credit"], Messages.More_Than_Zero);
                return;
            }
            if (row.Debit < 0)
            {
                e.Valid = false;
                ManuallyGridView.SetColumnError(ManuallyGridView.Columns["Debit"], Messages.More_Than_Zero);
                return;
            }
            if (row.Credit == 0 && row.Debit == 0)
            {
                e.Valid = false;
                ManuallyGridView.SetColumnError(ManuallyGridView.Columns["Debit"], Messages.More_Than_Zero);
                return;
            }
            if (row.Credit > 0 && row.Debit > 0)
            {
                e.Valid = false;
                ManuallyGridView.SetColumnError(ManuallyGridView.Columns["Debit"], Messages.Can_Not_Be_Credit_And_Debit_Zero);
                return;
            }
        }
        private void ManuallyGridView_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            BeginInvoke(new Action(delegate
            {
                var view = sender as GridView;
                view.PostEditor();
                //view.UpdateCurrentRow();
            }));

            if (e.Column.FieldName == "Account_ID")
            {
                var row = ManuallyGridView.GetRow(e.RowHandle) as DBModels.Coupes_Of_Account;
                if (row == null) return;
                //هانا الفاليو هي القيمة الجديدة
                //وال روو هو القيمة القديمة 
                //don't remove the (row != null &&)
                if (e.Value != null && row.Account_ID != 0 && e.Value.Equals(row.Account_ID) == false)//ToDO حتى يتم تغيير الوحدة عندما يتم تغيير صنف مضاف مسبقا
                {
                    row.Source_Type = manually_Type;
                }
            }
        }
        private void ManuallyGridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var row = ManuallyGridView.GetRow(e.RowHandle) as DBModels.Coupes_Of_Account;
            if (row == null) return;
            switch (e.Column.FieldName)
            {
                case "Code":
                    if (string.IsNullOrEmpty(row.Code))
                        row.Code = "90";
                    break;
                case "Account_ID":
                    if (row.Account_ID == 0)
                    {
                        ManuallyGridView.DeleteRow(e.RowHandle);
                        return;
                    }
                    ManuallyGridView_CellValueChanged(sender, new CellValueChangedEventArgs(
                        e.RowHandle, ManuallyGridView.Columns["Insert_Date"], row.Insert_Date));
                    ManuallyGridView_CellValueChanged(sender, new CellValueChangedEventArgs(
                        e.RowHandle, ManuallyGridView.Columns["Source_Type"], row.Source_Type));
                    ManuallyGridView_CellValueChanged(sender, new CellValueChangedEventArgs(
                        e.RowHandle, ManuallyGridView.Columns["Code"], row.Code));
                    break;
                    case "Credit":
                    break;
                    case "Debit":
                    break;
                    case "Insert_Date":
                    if (row.Insert_Date == default(DateTime))
                        row.Insert_Date = DateTime.Now;
                    ManuallyGridView_CellValueChanged(sender, new CellValueChangedEventArgs(
                        e.RowHandle, ManuallyGridView.Columns["Move_Name"], row.Move_Name));
                    break;
                    case "Move_Name":
                    if (row.Move_Name == 0)
                        row.Move_Name = Convert.ToByte(Enum_Choices.Account_Move_Name.Exchange);
                    ManuallyGridView_CellValueChanged(sender, new CellValueChangedEventArgs(
                        e.RowHandle, ManuallyGridView.Columns["Notes"], row.Notes));
                    break;
                    case "Source_Type":
                    if (row.Source_Type == 0)
                        row.Source_Type = manually_Type;
                    break;
                    case "Notes":
                    if (string.IsNullOrEmpty(row.Notes))
                    {
                        var move = Master_Class.Moves_Name_List.FirstOrDefault(x => x.ID == row.Move_Name);
                        if (move != null)
                            row.Notes = move.Name;
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
