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

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Categories : Frm_Master
    {
        DBModels.Category category;
        public Frm_Categories()
        {
            InitializeComponent();
            CategoriesTreLst.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(CategoriesTreLst_FocusedNodeChanged);
            New();
            Refresh_Data();
        }
        public void Frm_Categories_Load(object sender, EventArgs e)
        {
            CategoryGropsLkUpEdt.Properties.DisplayMember = nameof(category.Name);
            CategoryGropsLkUpEdt.Properties.ValueMember = nameof(category.ID);
            CategoriesTreLst.ParentFieldName = nameof(category.Parent_ID);
            CategoriesTreLst.KeyFieldName = nameof(category.ID);
            CategoriesTreLst.OptionsBehavior.Editable = false;
            CategoriesTreLst.Columns[nameof(category.Name)].Caption = "الإسم";
        }
        protected override void New()
        {
            category = new DBModels.Category();
            base.New();
        }
        protected override void Refresh_Data()
        {
            var cate_Grops = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Category>(
                @"Select * From [FinalSalesDB].[dbo].[Category]");
            CategoryGropsLkUpEdt.Properties.DataSource = cate_Grops;
            CategoriesTreLst.DataSource = cate_Grops;
            CategoriesTreLst.ExpandAll();
            base.Refresh_Data();
        }
        protected override void Get_Data()
        {
            CategoryNameTxt.Text = category.Name;
            CategoryGropsLkUpEdt.EditValue = category.Parent_ID;
            the_Changed_Source_ID = category.ID;
            the_Changed_Source_Name = category.Name;
            base.Get_Data();
        }
        protected override void Set_Data()
        {
            category.Name = CategoryNameTxt.Text;
            category.Parent_ID = Convert.ToInt32(CategoryGropsLkUpEdt.EditValue);
            //if CategoryGropsLkUpEdt returns null it will be 0 that what ?? implies
            base.Set_Data();
        }
        protected override bool IsDataValid()
        {
            int numError = 0;
            numError += CategoryNameTxt.Is_The_Text_Valid() ? 0 : 1;
            int count = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Category>(
                @"Select ID From [FinalSalesDB].[dbo].[Category] Where ID != @id And Name = @name",
                new
                {
                    id = category.ID,
                    name = CategoryNameTxt.Text.Trim()
                }).Count;
            if (count > 0)
            {
                CategoryNameTxt.ErrorText = Messages.Name_Exist;
                return false;
            }
            return (numError == 0);
            /*
             هذا الدالة تقوم بالتحقق من الشروط الموضحة أعلاه
             الشرط الكبير يقوم باسترجاع البيانات بحيث الإسم لايساوي أسم موجود مسبقا
             وآخر شرط عملناه عشان التعديل بحيث ما يجيش خطأ عندما يعدل على البيانات الموجودة مسبقا 
             */
        }
        protected override void Save()
        {
            if (category.ID == 0)
            {
                category.ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>(
                    @"INSERT INTO [FinalSalesDB].[dbo].[Category]
                           ([Name]
                           ,[Parent_ID])
                     VALUES
                           (@name
                           ,@par_ID)", new
                    {
                        name = category.Name,
                        par_ID = category.Parent_ID,
                    }).FirstOrDefault().ID;
            }
            else
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce(
                    @"UPDATE [FinalSalesDB].[dbo].[Category]
                       SET [Name] = @name
                          ,[Parent_ID] = @par_ID
                     WHERE [ID] = @id", new
                    {
                        name = category.Name,
                        par_ID = category.Parent_ID,
                        id = category.ID
                    });
            }
            the_Changed_Source_ID = category.ID;
            the_Changed_Source_Name = category.Name;
            base.Save();
            Refresh_Data();
        }
        void CategoriesTreLst_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var cate = CategoriesTreLst.GetRow(e.Node.Id) as DBModels.Category;
            if (cate == null) return;
            category.ID = cate.ID;
            category.Name = cate.Name;
            category.Parent_ID = cate.Parent_ID;
            Get_Data();
        }
    }
}
