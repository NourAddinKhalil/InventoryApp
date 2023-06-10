using System.Linq;
using TheFinalSalesProject.Classes;

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Add_Drawer : Frm_Master
    {
        private DBModels.Drawer drawer;
        public Frm_Add_Drawer()
        {
            InitializeComponent();
            New();
            this.Text = "إضافة خزينة";
        }
        public Frm_Add_Drawer(int draw_Id)
        {
            InitializeComponent();
            Load_Draw(draw_Id);
            this.Text = "تعديل خزينة";
        }
        private void Load_Draw(int draw_Id)
        {
            drawer = Session.Drawers.FirstOrDefault(d => d.ID == draw_Id);
            Get_Data();
        }
        protected override void New()
        {
            drawer = new DBModels.Drawer();
            base.New();
        }
        protected override void Get_Data()
        {
            DrawerNametxt.Text = drawer.Name;
            the_Changed_Source_ID = drawer.ID;
            the_Changed_Source_Name = drawer.Name;
            base.Get_Data();
        }
        protected override void Set_Data()
        {
            drawer.Name = DrawerNametxt.Text.Trim();
            base.Set_Data();
        }
        protected override void Open_New_Window()
        {
            Frm_Main_Window.OpenForm(new Frm_Add_Drawer(), false);
        }
        protected override bool IsDataValid()
        {
            int numError = 0;
            numError += DrawerNametxt.Is_The_Text_Valid() ? 0 : 1;
            return (numError == 0);
        }
        protected override void Save()
        {
            if (drawer.ID == 0)
            {
                drawer.Account_ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>(
                    @"Insert Into Accounts Values(
                                @name , @code , @parent)",
                    new
                    {
                        name = drawer.Name,
                        code = "9",
                        parent = 0//todo خلي المستخدم يعمل حساب افتراضي للصندوق عشان نكن نضيف الخزنات تحت الحساب
                    }).FirstOrDefault().ID;
                drawer.ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>(
                    @"Insert Into Drawer Values(
                             @name , @accID)",
                    new
                    {
                        name = drawer.Name,
                        accID = drawer.Account_ID
                    }).FirstOrDefault().ID;
            }
            else
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce(@"Update Accounts Set
                 Name = @name Where ID = @id", new { name = drawer.Name, id = drawer.Account_ID });
                DAL.Impelement_Stored_Procedure.Excute_Proce(@"Update Drawer Set
                Name = @name Where ID = @id ", new { name = drawer.Name, id = drawer.ID });
            }
            the_Changed_Source_ID = drawer.ID;
            the_Changed_Source_Name = drawer.Name;
            base.Save();
        }
        protected override void Delete()
        {
            if(DAL.Impelement_Stored_Procedure.SelectData<DBModels.Invoice>(
                @"Select ID From [FinalSalesDB].[dbo].[Invoice] 
                  Where Drawer_ID = @id",
                new {id = drawer.ID }).Count > 0)
            {
                Can_Not_Delete();
                return;
            }
            if (DAL.Impelement_Stored_Procedure.SelectData<DBModels.Coupes_Of_Account>(
                @"Select ID From [FinalSalesDB].[dbo].[Coupes_Of_Account] 
                  Where Account_ID = @id",
                new { id = drawer.Account_ID }).Count > 0)
            {
                Can_Not_Delete();
                return;
            }
            if (DAL.Impelement_Stored_Procedure.SelectData<DBModels.Guaranteed_Notes>(
                @"Select ID From [FinalSalesDB].[dbo].[Guaranteed_Notes] 
                  Where Drawer_ID = @id",
                new { id = drawer.ID }).Count > 0)
            {
                Can_Not_Delete();
                return;
            }
            DAL.Impelement_Stored_Procedure.Excute_Proce(
                @"Delete From [FinalSalesDB].[dbo].[Accounts] Where ID = @id",
                new
                {
                    id = drawer.Account_ID
                });
            DAL.Impelement_Stored_Procedure.Excute_Proce(
                @"Delete From [FinalSalesDB].[dbo].[Drawer] Where ID = @id",
                new
                {
                    id = drawer.ID
                });
            base.Delete();
        }
    }
}
