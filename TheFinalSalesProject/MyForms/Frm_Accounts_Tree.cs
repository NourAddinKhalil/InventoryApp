using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
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
    public partial class Frm_Accounts_Tree : Frm_Master
    {
        DBModels.Accounts account;
        public Frm_Accounts_Tree()
        {
            InitializeComponent();
            New();
            Refresh_Data();
        }
        private void Frm_Accounts_Tree_Load(object sender, EventArgs e)
        {
            AccountTreeList.ParentFieldName = "Parent_ID";
            AccountTreeList.ChildListFieldName = "ID";
            ParentIDLkp.Properties.PopulateColumns();
            ParentIDLkp.Look_Up_Edit_Translate_Column("Accounts");
        }
        protected override void New()
        {
            account = new DBModels.Accounts();
            base.New();
        }
        protected override void Refresh_Data()
        {
            AccountTreeList.DataSource = Session.Accounts;
            ParentIDLkp.Initilaze_Data(Session.Accounts);
            base.Refresh_Data();
        }
        protected override void Get_Data()
        {
            AccountIDTxt.Text = account.ID.ToString();
            ParentIDLkp.EditValue = account.Parent_ID;
            AccountCodeTxt.Text = account.Code;
            AccountNameTxt.Text = account.Name;
            base.Get_Data();
        }
        protected override void Set_Data()
        {
            account.ID = Convert.ToInt32(AccountIDTxt.Text);
            account.Parent_ID = Convert.ToInt32(ParentIDLkp.EditValue);
            account.Name = AccountNameTxt.Text.Trim();
            account.Code = AccountCodeTxt.Text.Trim();
            base.Set_Data();
        }
        protected override bool IsDataValid()
        {
            int numError = 0;
            //if (AccountTreeList.AllNodesCount <= 0)
            //{
            //    numError++;
            //    XtraMessageBox.Show("أضف حسابات ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //numError += ParentIDLkp.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += AccountCodeTxt.Is_The_Text_Valid() ? 0 : 1;
            numError += AccountCodeTxt.Is_The_Text_Valid() ? 0 : 1;
            numError += AccountIDTxt.Is_The_Text_Valid() ? 0 : 1;
            return (numError == 0);
        }
        protected override void Save()
        {
            if (account.ID == 0)
            {
                account.ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>(
                    @"INSERT INTO [FinalSalesDB].[dbo].[Accounts]
                         ([Name]
                         ,[Code]
                         ,[Parent_ID])
                     VALUES
                          (@name
                          ,@code
                          ,@ParID)", new
                    {
                        name = account.Name,
                        code = account.Code,
                        ParID = account.Parent_ID
                    }).FirstOrDefault().ID;
            }
            else
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce(
                    @"UPDATE [FinalSalesDB].[dbo].[Accounts]
                       SET [Name] = @name
                          ,[Code] = @code
                          ,[Parent_ID] = @ParID
                     WHERE [ID] = @id", new
                    {
                        name = account.Name,
                        code = account.Code,
                        ParID = account.Parent_ID,
                        id = account.ID
                    });
            }
            base.Save();
        }
        private void ParentIDLkp_EditValueChanged(object sender, EventArgs e)
        {
            // if id =0 and code =0 that's mean lkp is empty
            //we'll add the first account
            int id = Convert.ToInt32(ParentIDLkp.EditValue);
            //how many children does he has
            int count = Session.Accounts.Where(x => x.Parent_ID == id).Count();
            string code = "0";
            //what the parent code
            var acc = Session.Accounts.FirstOrDefault(x => x.ID == id);
            if (acc != null)
                code = acc.Code;
            AccountCodeTxt.Text = Generate_Code(count, code ,id);
        }
        private string Generate_Code(int count , string parCode , int parID)
        {
            if(count == 0)
            {
                if(parCode == "0")
                {
                    //if does not have any children that's mean it's primary account
                    int count2 = Session.Accounts.Where(x => x.Parent_ID != parID).Count();
                    return (count2 + 1).ToString();
                }
                return parCode + (count + 1).ToString();
            }
            return parCode + (count + 1).ToString();
        }
        private void AccountTreeList_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            var curAcc = AccountTreeList.GetRow(e.Node.Id) as DBModels.Accounts;
            if (curAcc == null) return;
            ParentIDLkp.EditValue = null;
            ParentIDLkp.EditValue = curAcc.Parent_ID;
            AccountCodeTxt.Text = curAcc.Code;
            AccountIDTxt.Text = curAcc.ID.ToString();
            AccountNameTxt.Text = curAcc.Name;
            List<int> ids = new List<int>() { curAcc.ID };
            var accountID = new
            {
                accountID = Convert_List_To_DataTable.Convert_ID_List_To_Table(ids).
               AsTableValuedParameter("[dbo].[IDs_List_Type]")
            };
            StocksGridCtrl.DataSource = DAL.Impelement_Stored_Procedure.SelectData<Table_View.Full_Coupes_Account>(
                "[FinalSalesDB].[dbo].[Get_Full_Coupes_Of_Account]", accountID, false).Select(x => new
                {
                    x.Name,
                    x.Insert_Date,
                    x.Debit,
                    x.Credit,
                    x.Notes
                });//todo the summarry must be one decalered
            StocksGridViw.BestFitColumns();
            DeleteBtn.Enabled = true;
            isNew = false;
            StocksGridViw.Add_Sum_Summary_To_Grid_Footer("Credit");
            StocksGridViw.Add_Sum_Summary_To_Grid_Footer("Debit");
        }
    }
}
