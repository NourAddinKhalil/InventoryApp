using Dapper;
using DevExpress.XtraEditors.Repository;
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
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Add_User_Screen_Access : Frm_Master
    {
        DBModels.Screen_Roles_Name screen_Roles_Name;
        bool myNew;
        public Frm_Add_User_Screen_Access()
        {
            InitializeComponent();
            treeList1.CustomNodeCellEdit += TreeList1_CustomNodeCellEdit;
            New();
            Get_Data();
        }
        public Frm_Add_User_Screen_Access(int screen_Role_ID)
        {
            InitializeComponent();
            treeList1.CustomNodeCellEdit += TreeList1_CustomNodeCellEdit;
            screen_Roles_Name = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Screen_Roles_Name>("Select * From Screen_Roles_Name Where ID=@screen_Role_ID ", new { screen_Role_ID = screen_Role_ID }).SingleOrDefault();
            ScreenRoleNameTxt.Text = screen_Roles_Name.Name;
            screen_Roles_Name.ID = screen_Role_ID;
            Get_Data();
        }
        RepositoryItemCheckEdit checkEdit;
        public void Frm_User_Screen_Access_Load(object sender, EventArgs e)
        {
            treeList1.KeyFieldName = nameof(ins.Screen_ID);
            treeList1.ParentFieldName = nameof(ins.Parent_Screen_ID);
            treeList1.Columns[nameof(ins.Screen_Name)].Visible = false;
            treeList1.Columns[nameof(ins.Screen_Name)].OptionsColumn.AllowEdit = false;
            treeList1.Columns[nameof(ins.Screen_Caption)].OptionsColumn.AllowEdit = false;
            treeList1.BestFitColumns();
            treeList1.Columns[nameof(ins.Screen_Caption)].Caption = "الإسم";
            treeList1.Columns[nameof(ins.Can_Add)].Caption = "إضافة";
            treeList1.Columns[nameof(ins.Can_Edit)].Caption = "تعديل";
            treeList1.Columns[nameof(ins.Can_Delete)].Caption = "حذف";
            treeList1.Columns[nameof(ins.Can_Print)].Caption = "طباعة";
            treeList1.Columns[nameof(ins.Can_Show)].Caption = "عرض";
            treeList1.Columns[nameof(ins.Can_Open)].Caption = "فتح";

            checkEdit = new RepositoryItemCheckEdit();
            checkEdit.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgCheckBox1;
            treeList1.RepositoryItems.Add(checkEdit);

            treeList1.Columns[nameof(ins.Can_Add)].ColumnEdit =
            treeList1.Columns[nameof(ins.Can_Edit)].ColumnEdit =
            treeList1.Columns[nameof(ins.Can_Delete)].ColumnEdit =
            treeList1.Columns[nameof(ins.Can_Print)].ColumnEdit =
            treeList1.Columns[nameof(ins.Can_Show)].ColumnEdit =
            treeList1.Columns[nameof(ins.Can_Open)].ColumnEdit = checkEdit;

            treeList1.Columns[nameof(ins.Can_Add)].Width =
            treeList1.Columns[nameof(ins.Can_Edit)].Width =
            treeList1.Columns[nameof(ins.Can_Delete)].Width =
            treeList1.Columns[nameof(ins.Can_Print)].Width =
            treeList1.Columns[nameof(ins.Can_Show)].Width =
            treeList1.Columns[nameof(ins.Can_Open)].Width = 55;
        }
        private readonly Classes.User_Screen_Access ins;
        private void TreeList1_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
        {
            if (e.Node.Id >= 0)
            {
                var row = treeList1.GetRow(e.Node.Id) as Classes.User_Screen_Access;
                if (row != null)
                {
                    if (e.Column.FieldName == nameof(ins.Can_Add) &&
                        row.Actions.Contains(Screen_Actions.Add) == false)
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItem();
                    }
                    else if (e.Column.FieldName == nameof(ins.Can_Delete) &&
                         row.Actions.Contains(Screen_Actions.Delete) == false)
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItem();
                    }
                    else if (e.Column.FieldName == nameof(ins.Can_Edit) &&
                        row.Actions.Contains(Screen_Actions.Edit) == false)
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItem();
                    }
                    else if (e.Column.FieldName == nameof(ins.Can_Print) &&
                        row.Actions.Contains(Screen_Actions.Print) == false)
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItem();
                    }
                    else if (e.Column.FieldName == nameof(ins.Can_Show) &&
                       row.Actions.Contains(Screen_Actions.Show) == false)
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItem();
                    }
                    else if (e.Column.FieldName == nameof(ins.Can_Open) &&
                      row.Actions.Contains(Screen_Actions.Open) == false)
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItem();
                    }
                }
            }
        }
        protected override void New()
        {
            screen_Roles_Name = new DBModels.Screen_Roles_Name();
            ScreenRoleNameTxt.Text = screen_Roles_Name.Name;
            myNew = true;
        }
        protected override void Get_Data()
        {
            {
                Dapper.DynamicParameters p = new Dapper.DynamicParameters();
                var data = (from s in Screens.Get_Screens
                            from db in DAL.Impelement_Stored_Procedure.SelectData<DBModels.Screen_Roles_Detail>
                            ("Select * From Screen_Roles_Detail Where Screen_Role_ID = @screen_Role_ID  And Screen_ID = @screen_ID ",
                            new { screen_Role_ID=screen_Roles_Name.ID , Screen_ID =s.Screen_ID}).DefaultIfEmpty()
                     //           dataContext.User_Screen_Role_Details
                     //.Where(x => x.Screen_Role_ID == screen_Role_Name.Screen_Role_ID
                     // && x.Screen_ID == s.Screen_ID).DefaultIfEmpty()
                            select new User_Screen_Access(s.Screen_Name)
                            {
                                Can_Add = (db == null) ? true : db.Can_Add,
                                Can_Delete = (db == null) ? true : db.Can_Delete,
                                Can_Edit = (db == null) ? true : db.Can_Edit,
                                Can_Open = (db == null) ? true : db.Can_Open,
                                Can_Print = (db == null) ? true : db.Can_Print,
                                Can_Show = (db == null) ? true : db.Can_Show,
                                Actions = s.Actions,
                                Screen_Name = s.Screen_Name,
                                Screen_Caption = s.Screen_Caption,
                                Screen_ID = s.Screen_ID,
                                Parent_Screen_ID = s.Parent_Screen_ID
                            }).ToList();
                treeList1.DataSource = data;
            }

            the_Changed_Source_ID = screen_Roles_Name.ID;
            the_Changed_Source_Name = screen_Roles_Name.Name;
            base.Get_Data();
        }
        protected override void Open_New_Window()
        {
            Frm_Main_Window.OpenForm(new Frm_Add_User_Screen_Access(), false);
        }
        protected override void Save()
        {
            if (string.IsNullOrEmpty(ScreenRoleNameTxt.Text.Trim())) return;
            Dapper.DynamicParameters p = new Dapper.DynamicParameters();
            if (myNew == false)
                p.Add("@id", screen_Roles_Name.ID, DbType.Int32);

            screen_Roles_Name.Name = ScreenRoleNameTxt.Text.Trim();
            p.Add("@name", screen_Roles_Name.Name, DbType.String);
            if (myNew == true)
            {
                screen_Roles_Name.ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>(
                    @"Insert Into Screen_Roles_Name Values(@name)", p).FirstOrDefault().ID;
            }
            else
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce(
                    @"Update Screen_Roles_Name Set Name = @name Where ID = @id", p ,false);
            }
            p = new Dapper.DynamicParameters();
            p.Add("@Screen_Role_ID", screen_Roles_Name.ID, DbType.Int32);
            DAL.Impelement_Stored_Procedure.Excute_Proce(
                @"Delete From Screen_Roles_Detail Where Screen_Role_ID = @Screen_Role_ID ", p);

            var Detial = new
            {
                Detial = Table().AsTableValuedParameter("Add_Users_Screen_Access_Type")
            };
            
            DAL.Impelement_Stored_Procedure.Excute_Proce("[dbo].[Add_Screen_Roles_Detaile]", Detial, isCommandText: false);

            the_Changed_Source_ID = screen_Roles_Name.ID;
            the_Changed_Source_Name = screen_Roles_Name.Name;
            base.Save();
        }
        private DataTable Table()
        {
            using (DataTable dataTable = new DataTable())
            {
                List<User_Screen_Access> data = treeList1.DataSource as List<User_Screen_Access>;
                dataTable.Columns.Add("ID", typeof(int));
                dataTable.Columns.Add("Screen_Role_ID" ,typeof(int));
                dataTable.Columns.Add("Screen_ID", typeof(int));
                dataTable.Columns.Add("Can_Show", typeof(bool));
                dataTable.Columns.Add("Can_Open", typeof(bool));
                dataTable.Columns.Add("Can_Add", typeof(bool));
                dataTable.Columns.Add("Can_Edit", typeof(bool));
                dataTable.Columns.Add("Can_Delete", typeof(bool));
                dataTable.Columns.Add("Can_Print", typeof(bool));
                foreach(var item in data)
                {//todo comehere
                    dataTable.Rows.Add(9,screen_Roles_Name.ID, item.Screen_ID,item.Can_Show
                        ,item.Can_Open,item.Can_Add,item.Can_Edit,item.Can_Delete,item.Can_Print);
                };
                return dataTable;
            }
        }
    }
}
