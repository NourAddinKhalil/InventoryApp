using Dapper;
using DevExpress.Utils.Animation;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraTab;
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
    public partial class Frm_Add_User_Setting_Prope : Frm_Master
    {
        private DBModels.Profile_Prope_Name prope_Name;
        private List<BaseEdit> listEdits;
        public Frm_Add_User_Setting_Prope()
        {
            InitializeComponent();
            accordionControl1.ElementClick += AccordionControl1_ElementClick;
            New();
            Get_Data();
        }
        public Frm_Add_User_Setting_Prope(int profile_Id)
        {
            InitializeComponent();
            accordionControl1.ElementClick += AccordionControl1_ElementClick;

            prope_Name = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Profile_Prope_Name>(
                @"Select * From Profile_Prope_Name Where ID = @id",
                new
                {
                    id = profile_Id
                }).FirstOrDefault();
            Get_Data();
        }
        public void Frm_User_Setting_Prope_Load(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedPageChanging += XtraTabControl1_SelectedPageChanging;
            accordionControl1.AnimationType = AnimationType.Simple;
            accordionControl1.OptionsMinimizing.AllowMinimizeMode = DevExpress.Utils.DefaultBoolean.False;
            accordionControl1.Appearance.Item.Hovered.Font = new Font(
                accordionControl1.Appearance.Item.Hovered.Font, FontStyle.Bold);
            accordionControl1.Appearance.Item.Hovered.Options.UseFont = true;
            accordionControl1.Appearance.Item.Pressed.Font = new Font(
                accordionControl1.Appearance.Item.Pressed.Font, FontStyle.Bold);
            accordionControl1.Appearance.Item.Pressed.Options.UseFont = true;
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            xtraTabControl1.Transition.AllowTransition = DevExpress.Utils.DefaultBoolean.True;
            SlideFadeTransition transition = new SlideFadeTransition();
            transition.Parameters.EffectOptions = PushEffectOptions.FromBottom;
            xtraTabControl1.Transition.TransitionType = transition;
        }
        private void AccordionControl1_ElementClick(object sender, ElementClickEventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = accordionControl1.Elements.IndexOf(e.Element);
        }
        private void XtraTabControl1_SelectedPageChanging(object sender, TabPageChangingEventArgs e)
        {
            SlideFadeTransition transition = new SlideFadeTransition();
            int current = xtraTabControl1.TabPages.IndexOf(e.Page);
            int prePage = xtraTabControl1.TabPages.IndexOf(e.PrevPage);
            if (current > prePage)
                transition.Parameters.EffectOptions = PushEffectOptions.FromBottom;
            else
                transition.Parameters.EffectOptions = PushEffectOptions.FromTop;
            xtraTabControl1.Transition.TransitionType = transition;
        }
        protected override void New()
        {
            prope_Name = new DBModels.Profile_Prope_Name();
            isNew = true;//todo temp
            DeleteBtn.Enabled = false;
            PrintBtn.Enabled = false;
        }
        protected override void Get_Data()
        {
            RoleNameTxt.Text = prope_Name.Name;
            listEdits = new List<BaseEdit>();
            User_Settings_Propes user_Propes = new User_Settings_Propes(prope_Name.ID);
            //هذا بيرجع لنا الخواص العامة ذي في الكلاس الرئيسي
            var menu = user_Propes.GetType().GetProperties();
            accordionControl1.Elements.Clear();
            accordionControl1.AllowItemSelection = true;
            xtraTabControl1.TabPages.Clear();
            foreach (var item in menu)
            {//بتضيف اسامي الخواص في القايمة
                accordionControl1.Elements.Add(
                    new AccordionControlElement()
                    {
                        Name = nameof(item),
                        Text = Classes.User_Settings_Propes.Get_Property_Name(item.Name),
                        Style = ElementStyle.Item,
                    });
                var page = new XtraTabPage()
                {
                    Name = item.Name,
                    Text = Classes.User_Settings_Propes.Get_Property_Name(item.Name),
                };
                xtraTabControl1.TabPages.Add(page);
                LayoutControl lc = new LayoutControl();
                //EmptySpaceItem space1 = new EmptySpaceItem();
                //EmptySpaceItem space2 = new EmptySpaceItem();
                //lc.AddItem(space1);
                //lc.AddItem(space1, InsertType.Left);
                //يعني عيرجع لي الخواص حق المتغيرات ذي داخل User_Settings_Roles
                //وبعدا يدي لي الخواص ذي داخلهن
                //هذي الدوارة بترجع لنا الخواص ذي داخل الخواص ذي طالع
                var props = item.GetValue(user_Propes).GetType().GetProperties();
                foreach (var prop in props)
                {
                    //ذيه السطر معناه يسير لاعند الكلاس الرئسي وبعدها يدخل الخاصية العامة ذي احنا 
                    //فيها وبعدها يدي لي الخاصية الداخلية ذي احنا فيها ويدي قيمتها
                    BaseEdit edit = Classes.User_Settings_Propes.Get_Property_Control(prop.Name, prop.GetValue(item.GetValue(user_Propes)));
                    if (edit != null)
                    {
                        var layoutItem = lc.AddItem(" ", edit);
                        layoutItem.TextVisible = true;
                        layoutItem.Text = Classes.User_Settings_Propes.Get_Property_Name(prop.Name);
                        listEdits.Add(edit);
                        layoutItem.SizeConstraintsType = SizeConstraintsType.Custom;
                        layoutItem.MaxSize = new Size(500, 26);
                        layoutItem.MinSize = new Size(250, 25);
                    }
                }
                lc.Dock = DockStyle.Fill;
                page.Controls.Add(lc);
            }
            the_Changed_Source_ID = prope_Name.ID;
            the_Changed_Source_Name = prope_Name.Name;
        }
        protected override void Open_New_Window()
        {
            Frm_Main_Window.OpenForm(new Frm_Add_User_Setting_Prope(), false);
        }
        protected override bool IsDataValid()
        {
            int numError = 0;
            if (RoleNameTxt.Text.Trim() == string.Empty)
            {
                RoleNameTxt.ErrorText = Messages.Necessary_Field;
                numError++;
            }
            listEdits.ForEach(x =>
            {
                if (x.GetType() == typeof(LookUpEdit) && ((LookUpEdit)x).Properties.DataSource.GetType() != typeof(List<Master_Class.Types>))
                {
                    numError += ((LookUpEdit)x).Is_The_Lkp_Text_Valid() ? 0 : 1;
                }
            });
            return (numError == 0);
        }
        protected override void Save()
        {
            prope_Name.Name = RoleNameTxt.Text;
            if (prope_Name.ID == 0)
            {
                prope_Name.ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>(
                    @"Insert Into Profile_Prope_Name Values (@name)", 
                    new 
                    {
                        name = prope_Name.Name 
                    }).FirstOrDefault().ID;
            }
            else
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce(
                    @"Update Profile_Prope_Name Set Name = @name Where ID = @id", 
                    new 
                    { 
                        name = prope_Name.Name ,
                        id = prope_Name.ID 
                    }
                    , false);
            }

            Delete_Profile_Prope_Sett(prope_Name.ID);
            var Props = new
            {
                prop = Table().AsTableValuedParameter("Add_User_Profile_Property_Type")
            };
            DAL.Impelement_Stored_Procedure.Excute_Proce("[dbo].[Add_User_Profile_Prope]", Props, isCommandText: false);
            the_Changed_Source_ID = prope_Name.ID;
            the_Changed_Source_Name = prope_Name.Name;
            base.Save();
        }
        private DataTable Table()
        {
            using (DataTable dataTable = new DataTable())
            {
                var data = listEdits;
                dataTable.Columns.Add("Profile_ID", typeof(int));
                dataTable.Columns.Add("Prope_Name", typeof(string));
                dataTable.Columns.Add("Prope_Value", typeof(byte[]));
                foreach (var item in data)
                {
                    dataTable.Rows.Add(prope_Name.ID, item.Name, Master_Class.From_AnyType_To_Byte_Array<object>(item.EditValue));
                };
                return dataTable;
            }
        }
        protected override void Delete()
        {
            Delete_Profile_Prope_Sett(prope_Name.ID);
            DAL.Impelement_Stored_Procedure.Excute_Proce(
                @"Delete From Profile_Prope_Name Where ID = @id", new { id = prope_Name.ID });
            base.Delete();
        }
        private void Delete_Profile_Prope_Sett(int id)
        {
            DAL.Impelement_Stored_Procedure.Excute_Proce(
              @"Delete From User_Profile_Property Where Profile_ID = @id",
              new
              {
                  id = id
              });
        }
    }
}
