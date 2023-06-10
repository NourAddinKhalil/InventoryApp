using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TheFinalSalesProject.Classes.Enum_Choices;
using TheFinalSalesProject.Classes;
using Dapper;
using DevExpress.XtraEditors;

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Exchange_Balances : Frm_Master
    {
        private DBModels.Transfer_Money transfer_Money;
        private byte type;
        private Finance.Account_Balance balance;
        public Frm_Exchange_Balances()
        {
            InitializeComponent();
            Refresh_Data();
            New();
        }
        private void Frm_Exchange_Balances_Load(object sender, EventArgs e)
        {

        }
        protected override void New()
        {
            transfer_Money = new DBModels.Transfer_Money()
            {
                Date = DateTime.Now
            };
            base.New();
        }
        protected override void Refresh_Data()
        {
            FromAccLkp.Initilaze_Data(Session.Accounts, clear: true);
            ToAccLkp.Initilaze_Data(Session.Accounts, clear: true);
            MoveTypeLkp.Initilaze_Data(Master_Class.Drawer_Balance_Type_List, clear: true);
            base.Refresh_Data();
        }
        protected override void Get_Data()
        {
            FromAccLkp.EditValue = transfer_Money.From_Acc_ID;
            ToAccLkp.EditValue = transfer_Money.To_Acc_ID;
            AmountSpn.EditValue = transfer_Money.Amount;
            BillDateDateEdit.DateTime = transfer_Money.Date;
            NotesMemEdt.Text = transfer_Money.Notes;
            BillIDTxt.Text = transfer_Money.ID.ToString();
            MoveTypeLkp.EditValue = transfer_Money.Type;
            type = transfer_Money.Type;
            base.Get_Data();
        }
        protected override void Set_Data()
        {
            transfer_Money.Date = BillDateDateEdit.DateTime;
            transfer_Money.From_Acc_ID = Convert.ToInt32(FromAccLkp.EditValue);
            transfer_Money.To_Acc_ID = Convert.ToInt32(ToAccLkp.EditValue);
            transfer_Money.Notes = NotesMemEdt.Text.Trim();
            transfer_Money.Amount = Convert.ToDouble(AmountSpn.EditValue);
            transfer_Money.Type = Convert.ToByte(MoveTypeLkp.EditValue);
            transfer_Money.User_ID = Session.user.ID;
            base.Set_Data();
        }
        protected override bool IsDataValid()
        {
            int numError = 0;
            numError += ToAccLkp.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += FromAccLkp.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += MoveTypeLkp.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += AmountSpn.Is_The_Edit_Value_More_Than_Zero() ? 0 : 1;
            numError += BillDateDateEdit.Is_The_Date_Valid() ? 0 : 1;
            if (type == Convert.ToByte(Enum_Choices.Drawer_Balance_Type.Exchange_Money) || 
                type == Convert.ToByte(Enum_Choices.Drawer_Balance_Type.Pull_Money)) 
            switch (Session.Current_User_Settings_Prope.General_Settings.When_Transfer_Money_More_Than_Exsist_Between_Accounts)
            {
                case Warining_Handel.Do_Not_Interrupt:
                    break;
                case Warining_Handel.Show_Warning:
                        if (XtraMessageBox.Show("المبلغ المراد تحويلة / سحبه أكبر من الرصيد الموجود هل تريد المتابعة ؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            numError++;
                            AmountSpn.ErrorText = "المبلغ المراد تحويلة / سحبه أكبر من الرصيد الموجود";
                        }
                        break;
                case Warining_Handel.Prevent:
                        XtraMessageBox.Show("لا يمكن المتابعة لأن المبلغ المراد تحويلة / سحبه أكبر من الرصيد الموجود", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        numError++;
                        AmountSpn.ErrorText = "المبلغ المراد تحويلة / سحبه أكبر من الرصيد الموجود";
                    break;
                default:
                    break;
            }
            if(FromAccLkp.EditValue == ToAccLkp.EditValue)
            {
                ToAccLkp.ErrorText = Messages.Same_Vlaue;
                numError++;
            }
            return (numError == 0);
        }
        protected override void Save()
        {
            if (transfer_Money.ID == 0)
            {
                transfer_Money.ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>(
                    @"INSERT INTO [FinalSalesDB].[dbo].[Transfer_Money]
                                 ([From_Acc_ID]
                                 ,[To_Acc_ID]
                                 ,[Date]
                                 ,[Type]
                                 ,[Amount]
                                 ,[Notes]
                                 ,[User_ID])
                        VALUES
                         (@from,@to,@date,@type,@amount,@note,@uID)",
                    new
                    {
                        from = transfer_Money.From_Acc_ID,
                        to = transfer_Money.To_Acc_ID,
                        date = transfer_Money.Date,
                        type = transfer_Money.Type,
                        amount = transfer_Money.Amount,
                        note = transfer_Money.Notes,
                        uID = transfer_Money.User_ID
                    }).FirstOrDefault().ID;
            }
            else
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce(
                    @"UPDATE [FinalSalesDB].[dbo].[Transfer_Money]
                       SET [From_Acc_ID] = @from
                          ,[To_Acc_ID] = @to
                          ,[Date] = @date
                          ,[Type] = @type
                          ,[Amount] = @amount
                          ,[Notes] = @note
                          ,[User_ID] = @uID
                       WHERE [ID] = @id",
                    new
                    {
                        from = transfer_Money.From_Acc_ID,
                        to = transfer_Money.To_Acc_ID,
                        date = transfer_Money.Date,
                        type = transfer_Money.Type,
                        amount = transfer_Money.Amount,
                        note = transfer_Money.Notes,
                        uID = transfer_Money.User_ID,
                        id = transfer_Money.ID
                    });
            }
            byte opening = Convert.ToByte(Drawer_Balance_Type.Opening_Account);
            byte exch = Convert.ToByte(Drawer_Balance_Type.Exchange_Money);
            byte move_Name = (transfer_Money.Type == opening) ?
                Convert.ToByte(Account_Move_Name.Opening_Balance) :
                (transfer_Money.Type == exch) ?
                Convert.ToByte(Account_Move_Name.Exchange) : Convert.ToByte(Account_Move_Name.Pull_Money);
            Delete_Data.Delete_Coupe_Of_Accounts(transfer_Money.ID, type);
            List<DBModels.Coupes_Of_Account> coupAccount = new List<DBModels.Coupes_Of_Account>();
            string msg = MoveTypeLkp.Text + $" من حساب {FromAccLkp.Text} إلى حساب {ToAccLkp.Text}";
            coupAccount.Add(new DBModels.Coupes_Of_Account()
            {
                Account_ID = transfer_Money.From_Acc_ID,
                Code = "50",
                Credit = transfer_Money.Amount,
                Debit = 0,
                Insert_Date = DateTime.Now,
                Source_ID = transfer_Money.ID,
                Source_Type = transfer_Money.Type,
                Notes = msg,
                Move_Name = move_Name,
                User_ID = transfer_Money.User_ID
            });
            coupAccount.Add(new DBModels.Coupes_Of_Account()
            {
                Account_ID = transfer_Money.To_Acc_ID,
                Code = "50",
                Credit = 0,
                Debit = transfer_Money.Amount,
                Insert_Date = DateTime.Now,
                Source_ID = transfer_Money.ID,
                Source_Type = transfer_Money.Type,
                Notes = msg,
                Move_Name = move_Name,
                User_ID = transfer_Money.User_ID
            });
            var coupes = new
            {
                coupes = Convert_List_To_DataTable.Convert_Coupes_List_To_Table(coupAccount).AsTableValuedParameter("Account_Coupes_List_Type")
            };
            DAL.Impelement_Stored_Procedure.Excute_Proce("Add_List_Of_Coupes", coupes, isCommandText: false);
            base.Save();
            type = transfer_Money.Type;
            BillIDTxt.Text = transfer_Money.ID.ToString();
        }
        protected override void Delete()
        {
            Delete_Data.Delete_Coupe_Of_Accounts(transfer_Money.ID, type);
            DAL.Impelement_Stored_Procedure.Excute_Proce(
                @"Delete From [FinalSalesDB].[dbo].[Transfer_Money] Where [ID] = @id", 
                new { id = transfer_Money.ID });
            base.Delete();
        }
        private string Get_Balance(int id)
        {
            balance = Finance.Get_Account_Balance(id);
            if (balance != null)
            {
                return balance.Balance_Note;
            }
            return "";
        }
        private void FromAccLkp_EditValueChanged(object sender, EventArgs e)
        {
            FromBalanceTxt.Text = Get_Balance(Convert.ToInt32(FromAccLkp.EditValue));
        }
        private void ToAccLkp_EditValueChanged(object sender, EventArgs e)
        {
            ToBalanceTxt.Text = Get_Balance(Convert.ToInt32(ToAccLkp.EditValue));
        }
    }
}
