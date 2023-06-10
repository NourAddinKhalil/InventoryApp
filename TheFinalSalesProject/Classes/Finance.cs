using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace TheFinalSalesProject.Classes
{
    public static class Finance
    {
        public static Account_Balance Get_Account_Balance(int account_ID)
        {
            if (account_ID == 0) return null;
            DynamicParameters p = new DynamicParameters();
            p.Add("@account_ID", account_ID, System.Data.DbType.Int32);
            var qurey = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Coupes_Of_Account>(
                @"Select Debit, Credit From Coupes_Of_Account Where Account_ID = @account_ID ", p);
            var totCredit = qurey.Sum(x => (double?)x.Credit) ?? 0;
            var totDebit = qurey.Sum(x => (double?)x.Debit) ?? 0;
            var account = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Accounts>(
                @"Select * From Accounts Where ID = @account_ID", p).FirstOrDefault();
            return new Account_Balance(account_ID, account.Name, Math.Abs(totCredit - totDebit),
                (totCredit > totDebit) ? Account_Balance.Balance_Types.Credit : Account_Balance.Balance_Types.Debit);
        }
        public static Account_Balance Get_Account_Balance(this DBModels.Accounts account, int account_ID)
        {
            return Get_Account_Balance(account_ID);
        }
        public class Account_Balance
        {
            public Account_Balance(int id, string accNmae, double balaAmount, Balance_Types balance_Type)
            {
                Account_ID = id;
                Account_Name = accNmae;
                Balance_Amount = balaAmount;
                Balance_Type = balance_Type;
            }
            public int Account_ID { get; }
            public string Account_Name { get; }
            public double Balance_Amount { get; }
            public Balance_Types Balance_Type { get; }
            public string Balance_Note
            {
                get
                {
                    return Balance_Amount.ToString() + " " + ((Balance_Type == Balance_Types.Credit) ? "دائن" : "مدين");
                }
            }

            public enum Balance_Types
            {
                Credit = 1,
                Debit,
            }
        }
    }
}
