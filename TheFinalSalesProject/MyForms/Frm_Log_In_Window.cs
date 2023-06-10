using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using System.Diagnostics;
using DevExpress.XtraSplashScreen;
using System.Reflection;

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Log_In_Window : DevExpress.XtraEditors.XtraForm
    {
        public Frm_Log_In_Window()
        {
            InitializeComponent();
        }
        public void Frm_Log_In_Window_Load(object sender, EventArgs e)
        {
            UserLookAndFeel.Default.SkinName = Properties.Settings.Default.Skin_Name;
            UserLookAndFeel.Default.SetSkinStyle(Properties.Settings.Default.Skin_Name, Properties.Settings.Default.Skin_Plate);
            EnterBtn.ActiveForecolor = Color.Transparent;
            EnterBtn.ActiveLineColor = Color.FromArgb(128, 128, 255);
            EnterBtn.ActiveFillColor = Color.FromArgb(128, 128, 255);
            EnterBtn.IdleFillColor = Color.Transparent;
            EnterBtn.IdleForecolor = Color.FromArgb(128, 128, 255);
            EnterBtn.IdleLineColor = Color.FromArgb(128, 128, 255);

            CancelBtn.ActiveForecolor = Color.Transparent;
            CancelBtn.ActiveLineColor = Color.FromArgb(192, 0, 0);
            CancelBtn.ActiveFillColor = Color.FromArgb(192, 0, 0);
            CancelBtn.IdleFillColor = Color.Transparent;
            CancelBtn.IdleForecolor = Color.FromArgb(192, 0, 0);
            CancelBtn.IdleLineColor = Color.FromArgb(192, 0, 0);
            if (Debugger.IsAttached)
            {
                UserNameTxt.Text = "Khalil";
                PasswordTxt.Text = "12345";
                //UserNameTxt.Text = "1234";
                //PasswordTxt.Text = "1234";
                EnterBtn_Click(null, null);
            }
        }
        public void ShowPassChk_OnChange(object sender, EventArgs e)
        {
            if (ShowPassChk.Checked == true)
            {
                OpenEyelayCtrl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                OpenEyeLbl.Visible = true;
                CloseEyelayCtrl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                PasswordTxt.Properties.UseSystemPasswordChar = false;
            }
            else
            {
                OpenEyelayCtrl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                OpenEyeLbl.Visible = false;
                CloseEyelayCtrl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                PasswordTxt.Properties.UseSystemPasswordChar = true;
            }
        }
        public void EnterBtn_Click(object sender, EventArgs e)
        {
            string userName = UserNameTxt.Text;
            string passWord = PasswordTxt.Text;
            DBModels.User user = DAL.Impelement_Stored_Procedure.SelectData<DBModels.User>("Select * From [FinalSalesDB].[dbo].[User] Where User_Name = @UserName", new { UserName = userName }).FirstOrDefault();
            if (user == null) goto MSGERROR;
            else
            {
                if (user.Is_Active == false)
                {
                    XtraMessageBox.Show("هذا المستخدم غير مفعل", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    goto EXIT;
                }//todo encrypt passsWord
                if(passWord.Equals(user.Password))
                {
                    this.Hide();
                    SplashScreenManager.ShowForm(Frm_Main_Window.Main_Window_Ins, typeof(Loading_Screen));
                    Classes.Session.Set_User(user);
                    DAL.Impelement_Stored_Procedure.Excute_Proce
                        (
                        "Update [FinalSalesDB].[dbo].[User] Set Is_Online = 1 Where User_Name = @UserName And ID = @uid", 
                        new 
                        { 
                            UserName = user.User_Name, uid = user.ID 
                        });
                    //هنا بنحمل جميع الخصائص عشان برج مراقبة قاعدة البيانات
                    //ولان الخصائص كلهن ستاتيك ماعنرسلهنش متغير في الدوارة لانهن ستاتيك وما نقدر 
                    //نفعل متغير من الكلاس
                    Type type = typeof(Classes.Session);
                    var props = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                    //Parallel.ForEach<PropertyInfo>((props), x =>
                    //{
                    //    var obj = x.GetValue(null);//becuase ites static
                    //});
                    foreach (var item in props)
                    {
                        var obj = item.GetValue(null);
                    }
                    Frm_Main_Window.Main_Window_Ins.Show();
                    this.Close();
                    SplashScreenManager.CloseForm();
                    goto EXIT;
                }
                else
                {
                    goto MSGERROR;
                }
            }
        
            MSGERROR:
            XtraMessageBox.Show("إسم المستخدم أو كلمة المرور غير صحيحة", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            EXIT:
            return;
        }
        public void CancelBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}