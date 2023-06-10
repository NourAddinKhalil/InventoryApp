using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheFinalSalesProject.Classes;
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Main_Window : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        private static Frm_Main_Window window;
        public static Frm_Main_Window Main_Window_Ins
        {
            get
            {
                if (window == null)
                {
                    window = new Frm_Main_Window();
                }
                return window;
            }
        }
        public Frm_Main_Window()
        {
            InitializeComponent();
            accordionControl1.ElementClick += new DevExpress.XtraBars.Navigation.ElementClickEventHandler(AccordionControl1_ElementClick);
        }
        void AccordionControl1_ElementClick(object sender, DevExpress.XtraBars.Navigation.ElementClickEventArgs e)
        {
            string frm_Name = Convert.ToString(e.Element.Tag);
            if (frm_Name != null)
            {
                OpenForms(frm_Name);
            }
        }
        private void Frm_Main_Window_Load(object sender, EventArgs e)
        {
            UserLookAndFeel.Default.SkinName = Properties.Settings.Default.Skin_Name;
            UserLookAndFeel.Default.SetSkinStyle(Properties.Settings.Default.Skin_Name, Properties.Settings.Default.Skin_Plate);
            this.Font = new Font(Properties.Settings.Default.Font_Name, Properties.Settings.Default.Font_Size, GraphicsUnit.Pixel);
            accordionControl1.Elements.Clear();
            var screens = Session.Screen_Access_Role.Where(x => x.Can_Show == true || Session.user.Type == (byte)User_Type.Admin);
            screens.Where(x => x.Parent_Screen_ID == 0).ToList().ForEach(x =>
            {
                AccordionControlElement element = new AccordionControlElement()
                {
                    Text = x.Screen_Caption,
                    Tag = x.Screen_Name,
                    Name = x.Screen_Name,
                    Style = ElementStyle.Group
                };
                if (element.Name == "elm_Main_Page")
                    element.Style = ElementStyle.Item;
                accordionControl1.Elements.Add(element);
                Add_Accordion_Elements(element, x.Screen_ID);
            });
            UserNameLbl.Text = "المستخدم الحالي : " + $"{Session.user.Real_Name} ";
            timer1.Start();
        }
        private void Add_Accordion_Elements(AccordionControlElement parent, int parent_ID)
        {
            var screens = Session.Screen_Access_Role.Where(x => x.Can_Show == true || Session.user.Type == (byte)User_Type.Admin);
            screens.Where(x => x.Parent_Screen_ID == parent_ID).ToList().ForEach(x =>
            {
                AccordionControlElement element = new AccordionControlElement()
                {
                    Text = x.Screen_Caption,
                    Tag = x.Screen_Name,
                    Name = x.Screen_Name,
                    Style = ElementStyle.Item
                };
                if (element.Name.Contains("elm_"))
                {
                    element.Style = ElementStyle.Group;
                    Add_Accordion_Elements(element, x.Screen_ID);
                }
                parent.Elements.Add(element);
            });
        }
        private void Frm_Main_Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Skin_Name = UserLookAndFeel.Default.SkinName;
            Properties.Settings.Default.Skin_Plate = UserLookAndFeel.Default.ActiveSvgPaletteName;
            Properties.Settings.Default.Save();
            DAL.Impelement_Stored_Procedure.Excute_Proce
                        (
                        "Update [FinalSalesDB].[dbo].[User] Set Is_Online = 0 Where User_Name = @UserName And ID = @uid",
                        new
                        {
                            UserName = Session.user.User_Name,
                            uid = Session.user.ID
                        });
            Application.Exit();
            timer1.Stop();
        }
        #region FormHandler
        public static void OpenForms(string form_Name, bool just_One_Window = true)
        {
            Form frm = null;
            var instance = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == form_Name);
            if (instance != null)
            {
                frm = Activator.CreateInstance(instance) as Form;
                if (Application.OpenForms[frm.Name] != null)
                {
                    frm = Application.OpenForms[frm.Name];
                }
                else
                {
                    //frm.ShowDialog();
                }
                frm.BringToFront();
                // frm = null;
                if (frm != null)
                {
                    frm.Name = form_Name;
                    OpenForm(frm, just_One_Window);
                    return;
                }
            }
        }
        public static void OpenForm(Form frm, bool just_One_Window = true)
        {
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            if (Session.user.Type == (byte)User_Type.Admin)
            {
                Main_Window_Ins.Add_Form_To_Page(frm, just_One_Window);
                return;
            }
            var screen = Session.Screen_Access_Role.FirstOrDefault(x => x.Screen_Name == frm.Name);
            if (screen != null)
            {
                if (screen.Can_Open == true)
                {
                    Main_Window_Ins.Add_Form_To_Page(frm, just_One_Window);
                    return;
                }
                else
                {
                    XtraMessageBox.Show("غير مصرح لك", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        private void Add_Form_To_Page(Form frm , bool just_One_Window = true)
        {
            foreach (XtraTabPage item in FormsXtraTabCtrl.TabPages)
            {
                if (item.Name == frm.Name && just_One_Window)
                {
                    item.Select();
                    return;
                }
            }
            FormsXtraTabCtrl.TabPages.Add(new XtraTabPage()
            {
                Text = frm.Text,
                Name = frm.Name,
            });
            foreach (XtraTabPage item in FormsXtraTabCtrl.TabPages)
            {
                if (item.Name == frm.Name)
                {
                    item.Controls.Add(frm);
                    frm.Dock = DockStyle.Fill;
                    frm.Show();
                }
            }
        }
        #endregion

        private void FormsXtraTabCtrl_CloseButtonClick(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs arg = (ClosePageButtonEventArgs)e;
            ((XtraTabPage)arg.Page).Dispose();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            DateAndTimeLbl.Text = DateTime.Now.ToString();
        }
        private void FontBrn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                if(fontDialog.ShowDialog()== DialogResult.OK)
                {
                    Properties.Settings.Default.Font_Name = fontDialog.Font.Name;
                    Properties.Settings.Default.Font_Size = fontDialog.Font.Size;
                    Properties.Settings.Default.Is_Bold = fontDialog.Font.Bold;
                    Properties.Settings.Default.Is_Italic = fontDialog.Font.Italic;
                    this.Font = fontDialog.Font;
                }
            }
        }
    }
}
