using Dapper;
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
    public partial class Frm_Company_Info : Frm_Master
    {
        bool mynew;
        DBModels.CompanyInfo company_Info;
        public Frm_Company_Info()
        {
            InitializeComponent();
            New();
        }
        protected override void New()
        {
            company_Info = new DBModels.CompanyInfo();
            base.New();
        }
        public void Frm_Company_Info_Load(object sender, EventArgs e)
        {
            DeleteBtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            NewBtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }
        protected override void Get_Data()
        {
            company_Info = DAL.Impelement_Stored_Procedure.SelectData<DBModels.CompanyInfo>("Select * From Company_Info", null).FirstOrDefault();
            if (company_Info == null)
            {
                company_Info = new DBModels.CompanyInfo();
                mynew = true;
                return;
            }
            CompanyNametxt.Text = company_Info.Name;
            Phonetxt.Text = company_Info.Phone;
            Mobiletxt.Text = company_Info.Mobile;
            Addresstxt.Text = company_Info.Address;
            if (company_Info.Logo != null)
                CompanyPic.Image = Master_Class.Convert_Byte_To_Image(company_Info.Logo.ToArray());
            base.Get_Data();
        }
        protected override bool IsDataValid()
        {
            if (CompanyNametxt.Text.Trim() == string.Empty)
            {
                CompanyNametxt.ErrorText = Messages.Necessary_Field;
                return false;
            }
            return true;
        }
        protected override void Set_Data()
        {
            company_Info.Name = CompanyNametxt.Text.Trim();
            company_Info.Phone = Phonetxt.Text.Trim();
            company_Info.Mobile = Mobiletxt.Text.Trim();
            company_Info.Address = Addresstxt.Text.Trim();
            if (CompanyPic.Image != null)
                company_Info.Logo = Classes.Master_Class.Convert_Image_To_Byte(CompanyPic.Image);
            base.Set_Data();
        }
        protected override void Save()
        {//todo edit here
            var p = new DynamicParameters();
            if (!mynew)
                p.Add("@id", company_Info.ID, DbType.Int32);
            p.Add("@name", company_Info.Name, DbType.String);
            p.Add("@phone", company_Info.Phone, DbType.String);
            p.Add("@mobile", company_Info.Mobile, DbType.String);
            p.Add("@address", company_Info.Address, DbType.String);
            p.Add("@logo", company_Info.Logo , DbType.Binary);
            if(mynew)
            DAL.Impelement_Stored_Procedure.Excute_Proce("[dbo].[Add_Company_Info]", p);
            else
                DAL.Impelement_Stored_Procedure.Excute_Proce("[dbo].[Update_Company_Info]", p);
            base.Save();
        }
        public void Frm_Company_Info_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
}
