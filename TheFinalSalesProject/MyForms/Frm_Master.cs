using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using DevExpress.XtraEditors;
using static TheFinalSalesProject.Classes.Enum_Choices;
using TheFinalSalesProject.Classes;
using DevExpress.XtraBars;

namespace TheFinalSalesProject
{
    public partial class Frm_Master : XtraForm
    {
        protected bool isNew;
        public int the_Changed_Source_ID;
        public string the_Changed_Source_Name;

        public Frm_Master()
        {
            InitializeComponent();
            this.KeyDown += Frm_Master_KeyDown;
        }
        public static void  Can_Not_Delete()
        {
            XtraMessageBox.Show("لا يمكن الحذف لأنها متصلة بسندات أخرى", "فشل الحذف", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        protected void Enable_Delete_Print_Btns()
        {
            DeleteBtn.Enabled = true;
            PrintBtn.Enabled = true;
        }
        public static bool Ask_For_Delete()
        {
            if (XtraMessageBox.Show("هل فعلاَ تريد الحذف", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                return true;
            return false;
        }
        private void SaveBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveBtn.Enabled && SaveBtn.Visibility == BarItemVisibility.Always)
            {
                if (!isNew)
                    if (XtraMessageBox.Show("هل حقاً تريد التعديل والحفظ ؟", " تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                if (Is_Authorized(this.Name, (isNew) ? Screen_Actions.Add : Screen_Actions.Edit))
                    if (IsDataValid())
                    {
                        SaveBtn.Enabled = false;
                        Set_Data();
                        Save();
                        SaveBtn.Enabled = true;
                    }
            }
        }
        private void NewBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (NewBtn.Enabled && NewBtn.Visibility == BarItemVisibility.Always)
            {
                NewBtn.Enabled = false;
                New();
                NewBtn.Enabled = true;
                DeleteBtn.Enabled = false;
            }
        }
        private void DeleteBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DeleteBtn.Enabled && DeleteBtn.Visibility == BarItemVisibility.Always)
            {
                if (Is_Authorized(this.Name, Screen_Actions.Delete))
                {
                    if (Ask_For_Delete())
                    {
                        DeleteBtn.Enabled = false;
                        Delete();
                    }
                }
            }
        }
        private void RefreshBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (RefreshBtn.Enabled && RefreshBtn.Visibility == BarItemVisibility.Always)
                Refresh_Data();
        }
        private void PrintBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (PrintBtn.Enabled && PrintBtn.Visibility == BarItemVisibility.Always)
                if (Is_Authorized(this.Name, Screen_Actions.Print))
                {
                    PrintBtn.Enabled = false;
                    Print();
                    PrintBtn.Enabled = true;
                }
        }
        protected static bool Is_Authorized(string form_Name, Classes.Enum_Choices.Screen_Actions action, DBModels.User user = null)
        {
            if (user == null)//return current user
                user = Classes.Session.user;
            if (user.Type == (byte)Classes.Enum_Choices.User_Type.Admin)
                return true;
            else
            {
                var screen = Classes.Session.Screen_Access_Role.SingleOrDefault(x => x.Screen_Name == form_Name);
                bool check = false;
                switch (action)
                {
                    case Classes.Enum_Choices.Screen_Actions.Open:
                        check = screen.Can_Open;
                        break;
                    case Classes.Enum_Choices.Screen_Actions.Add:
                        check = screen.Can_Add;
                        break;
                    case Classes.Enum_Choices.Screen_Actions.Edit:
                        check = screen.Can_Edit;
                        break;
                    case Classes.Enum_Choices.Screen_Actions.Delete:
                        check = screen.Can_Delete;
                        break;
                    case Classes.Enum_Choices.Screen_Actions.Print:
                        check = screen.Can_Print;
                        break;
                    default:
                        break;
                }
                if (check == false)
                {
                    XtraMessageBox.Show("غير مصرح لك", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return check;
            }
        }
        public void Insert_User_Action(User_Actions actions)
        {
            Insert_User_Action(actions, this.the_Changed_Source_ID, this.the_Changed_Source_Name, this.Name);
        }
        public static void Insert_User_Action(User_Actions actions, int the_Changed_Source_ID, string the_Changed_Source_Name, string screenName)
        {
            int screenId = 0;
            var screens = Screens.Get_Screens.FirstOrDefault(x => x.Screen_Name == screenName);
            if (screens != null) screenId = screens.Screen_ID;
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce(
                    @"Insert Into User_Actions_Per_Screen Values (
                    @uID,@date,@crnID,@type,@chSoceID,@chSoceNAme)",
                    new
                    {
                        uID = Session.user.ID,
                        date = DateTime.Now.Date,
                        crnID = screenId,
                        type = Convert.ToByte(actions),
                        chSoceID = the_Changed_Source_ID,
                        chSoceNAme = the_Changed_Source_Name ?? ""//todo null value,
                    });
            }
        }
        protected virtual void Get_Screen_History()
        {

        }
        protected virtual void Save()
        {
            XtraMessageBox.Show("Saved Successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Refresh_Data();
            Insert_User_Action((isNew) ? User_Actions.Add : User_Actions.Edit);
            isNew = false;
            DeleteBtn.Enabled = true;
            PrintBtn.Enabled = true;
            Get_Screen_History();
        }
        protected virtual void New()//todo in evry form when we call new we'll make performclick
        {
            isNew = true;
            Get_Data();
            isNew = true;
            DeleteBtn.Enabled = false;
            PrintBtn.Enabled = false;
        }
        protected virtual void Delete()
        {
            XtraMessageBox.Show(" Deleted Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Insert_User_Action(User_Actions.Delete);
            Get_Screen_History();
            NewBtn.PerformClick();
        }
        protected virtual void Get_Data()
        {
            Get_Screen_History();
        }
        protected virtual void Set_Data()
        {

        }
        public void Impelment_RefreshData()
        {
            Refresh_Data();
        }
        protected virtual void Refresh_Data()
        {

        }
        protected virtual bool IsDataValid()
        {
            return true;
        }
        private void OpenNewWindowBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Open_New_Window();
        }
        public virtual void Print()
        {
            Get_Screen_History();
        }
        protected virtual void Open_New_Window()
        {

        }
        private void Frm_Master_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                SaveBtn.PerformClick();
            }
            else if (e.KeyCode == Keys.F2)
            {
                NewBtn.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                DeleteBtn.PerformClick();
            }
            else if (e.KeyCode == Keys.F4)
            {
                PrintBtn.PerformClick();
            }
            else if (e.KeyCode == Keys.F5)
            {
                RefreshBtn.PerformClick();
            }
            else if (e.KeyCode == Keys.F6)
            {
                OpenNewWindowBtn.PerformClick();
            }
        }
    }
}