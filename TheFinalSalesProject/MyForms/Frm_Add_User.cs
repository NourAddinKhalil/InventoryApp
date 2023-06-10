using DevExpress.XtraEditors;
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
    public partial class Frm_Add_User : Frm_Master
    {
        DBModels.User user;
        public Frm_Add_User()
        {
            InitializeComponent();
            Refresh_Data();
            New();
            this.Text = "إضافة مستخدم";
        }
        public Frm_Add_User(int uid)
        {
            InitializeComponent();
            user = DAL.Impelement_Stored_Procedure.SelectData<DBModels.User>(
                @"Select * From FinalSalesDB.dbo.[User] Where ID = @id",
                new { id = uid }).FirstOrDefault();
            this.Text = "تعديل مستخدم";
            Refresh_Data();
            Get_Data();
        }
        protected override void New()
        {
            user = new DBModels.User();
            user.Is_Active = true;
            base.New();
        }
        protected override void Get_Data()
        {
            NameTxt.Text = user.Real_Name;
            UserNameTxt.Text = user.User_Name;
            PassWordTxt.Text = user.Password;
            IsActiveToglSwch.IsOn = user.Is_Active;
            UserTypeLokUp.EditValue = user.Type;
            SettingProfileIDLokUp.EditValue = user.Setting_Profile_ID;
            SettingScreenIDLokUp.EditValue = user.Setting_Screen_ID;

            the_Changed_Source_ID = user.ID;
            the_Changed_Source_Name = user.Real_Name;
            base.Get_Data();
        }
        protected override void Set_Data()
        {
            //if (user.Password != PassWordTxt.Text)
            //{
            //    var hasher = new PasswordHasher();
            //    string myTxtHash = hasher.Hash(PassWordTxt.Text);
            //    PassWordTxt.Text = myTxtHash;
            //}
            user.Real_Name = NameTxt.Text.Trim();
            user.User_Name = UserNameTxt.Text.Trim();
            user.Password = PassWordTxt.Text.Trim();
            user.Is_Active = IsActiveToglSwch.IsOn;
            user.Type = (byte)UserTypeLokUp.EditValue;
            user.Setting_Profile_ID = Convert.ToInt32(SettingProfileIDLokUp.EditValue);
            user.Setting_Screen_ID = Convert.ToInt32(SettingScreenIDLokUp.EditValue);
            if (Session.user.ID == user.ID)
                user.Is_Online = true;
            else
                user.Is_Online = false;
            base.Set_Data();
        }
        protected override void Open_New_Window()
        {
            Frm_Main_Window.OpenForm(new Frm_Add_User(), false);
        }
        protected override void Refresh_Data()
        {
            SettingProfileIDLokUp.Initilaze_Data(DAL.Impelement_Stored_Procedure.SelectData<DBModels.Profile_Prope_Name>(
                @"Select * From Profile_Prope_Name"));
            SettingScreenIDLokUp.Initilaze_Data(DAL.Impelement_Stored_Procedure.SelectData<DBModels.Screen_Roles_Name>(
                @"Select * From Screen_Roles_Name"));
            UserTypeLokUp.Initilaze_Data(Master_Class.User_Type_List);
            base.Refresh_Data();
        }
        protected override bool IsDataValid()
        {
            int numError = 0;
            if (DAL.Impelement_Stored_Procedure.SelectData<DBModels.User>(
                    @"Select * From FinalSalesDB.dbo.[User] Where 
                               [User_Name] = @uName And ID != @id",
            new { uName = UserNameTxt.Text.Trim(), id = user.ID }).Count() > 0)
            {
                numError++;
                UserNameTxt.ErrorText = Messages.Name_Exist;
            }
            if (DAL.Impelement_Stored_Procedure.SelectData<DBModels.User>(
                    @"Select * From FinalSalesDB.dbo.[User] Where 
                               [Real_Name] = @rName And ID != @id",
            new { rName = NameTxt.Text.Trim(), id = user.ID }).Count() > 0)
            {
                numError++;
                UserNameTxt.ErrorText = Messages.Name_Exist;
            }
            numError += NameTxt.Is_The_Text_Valid() ? 0 : 1;
            numError += UserNameTxt.Is_The_Text_Valid() ? 0 : 1;
            numError += PassWordTxt.Is_The_Text_Valid() ? 0 : 1;
            numError += SettingProfileIDLokUp.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += SettingScreenIDLokUp.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += UserTypeLokUp.Is_The_Lkp_Text_Valid() ? 0 : 1;

            return (numError == 0);
        }
        protected override void Save()
        {
            if (user.ID <= 0)
            {
                user.ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>(
                    @"Insert Into FinalSalesDB.dbo.[User] Values (
                        @rname,@uName,@pwd,@type,@profID,@scrnID,@active,@online ) ",
                    new
                    {
                        rname = user.Real_Name,
                        uName = user.User_Name,
                        pwd = user.Password,
                        type = user.Type,
                        profID = user.Setting_Profile_ID,
                        scrnID = user.Setting_Screen_ID,
                        active = user.Is_Active,
                        online = user.Is_Online
                    }).FirstOrDefault().ID;
            }
            else
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce(
                    @"Update FinalSalesDB.dbo.[User] Set 
                             [Real_Name] = @rname
                            ,[User_Name] = @uName
                            ,[Password] = @pwd
                            ,[Type] = @type
                            ,[Setting_Profile_ID] = @profID
                            ,[Setting_Screen_ID] = @scrnID
                            ,[Is_Active] = @active
                            ,[Is_Online] = @online
                             Where ID = @id",
                    new
                    {
                        rname = user.Real_Name,
                        uName = user.User_Name,
                        pwd = user.Password,
                        type = user.Type,
                        profID = user.Setting_Profile_ID,
                        scrnID = user.Setting_Screen_ID,
                        active = user.Is_Active,
                        online = user.Is_Online,
                        id = user.ID
                    });
            }
            the_Changed_Source_ID = user.ID;
            the_Changed_Source_Name = user.Real_Name;
            base.Save();
        }
        protected override void Delete()
        {
            int movenum = 0;
            if (DAL.Impelement_Stored_Procedure.SelectData<DBModels.User_Actions_Per_Screen>(
                   @"Select ID From User_Actions_Per_Screen Where [User_ID] = @uID",
                new { uID = user.ID }).Count > 0)
            {
                movenum++;
            }
            if (DAL.Impelement_Stored_Procedure.SelectData<DBModels.Coupes_Of_Account>(
                   @"Select ID From Coupes_Of_Account Where [User_ID] = @uID",
                new { uID = user.ID }).Count > 0)
            {
                movenum++;
            }
            if (DAL.Impelement_Stored_Procedure.SelectData<DBModels.Pro_Store_Movement>(
                   @"Select ID From Pro_Store_Movement Where [User_ID] = @uID",
                new { uID = user.ID }).Count > 0)
            {
                movenum++;
            }
            if (movenum != 0)
            {
                XtraMessageBox.Show("لا يمكن حذف المستخدم لأنه مربوط بحسابات أخرى", "فشل الحذف", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DAL.Impelement_Stored_Procedure.Excute_Proce(
                @"Delete From FinalSalesDB.dbo.[User] Where ID = @id", new { id = user.ID });
            base.Delete();
        }
    }
}
