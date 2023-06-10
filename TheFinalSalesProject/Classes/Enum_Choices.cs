using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.Classes
{
    public static class Enum_Choices
    {
        public enum ProductTypesEnum : byte
        {
            Invetory = 1,
            Service
        }
        public enum Part_Type :byte
        {
            Supplier = 1,
            Customer ,
            Cus_Supp,
            Account,
            Employee
        }
        public enum bill_Type :byte
        {
            Buy = Source_Type.Buy,
            Sale = Source_Type.Sale,
            BuyReturn = Source_Type.BuyReturn,
            SaleReturn = Source_Type.SaleReturn
        }
        public enum Guaranteed_Types :byte
        {
            CashNoteIn = Source_Type.CashNoteIn,
            CashNoteOut = Source_Type.CashNoteOut
        }
        public enum Source_Type :byte
        {
            Buy = 1,
            Sale,
            BuyReturn,
            SaleReturn,
            CashNoteIn,
            CashNoteOut,
            OpeningAccount,
            Destructor,
            Transfer_Balance,
            Manually_Rigister,
            Exchange_Money,
            Pull_Money,
        }
        public enum Store_Balance_Type : byte
        {
            Opening_Account = Source_Type.OpeningAccount,
            Destructor = Source_Type.Destructor,
            Transfer_Balance = Source_Type.Transfer_Balance
        }
        public enum Drawer_Balance_Type : byte
        {
            Opening_Account = Source_Type.OpeningAccount,
            Exchange_Money = Source_Type.Exchange_Money,
            Pull_Money = Source_Type.Pull_Money,
        }
        public enum Cost_Distribute :byte
        {
            Price = 1,
            Quantity
        }
        public enum Print_Mode :byte
        {
            Directly = 1,
            ShowPreview,
            ShowDialog
        }
        public enum Pay_Mode : byte
        {
            Cash = 1,
            Credit,
        }
        public enum Warining_Handel :byte
        {
            Do_Not_Interrupt = 1,
            Show_Warning,
            Prevent,
        }
        public enum Screen_Actions :byte
        {
            Show = 1,
            Open,
            Add,
            Edit,
            Delete,
            Print,
        }
        public enum User_Type :byte
        {
            Admin = 1,
            User,
        }
        public enum Cost_Calc_Method :byte
        {
            Fifo = 1,
            Lifo,
            WAC
        }
        public enum User_Actions :byte
        {
            Add = 1,
            Edit,
            Delete,
            Print,
        }
        public enum Account_Move_Name :byte
        {
            Opening_Balance = 1,
            Sales_Bill,
            Purchase_Bill,
            Sales_Return_Bill,
            Purchase_Return_Bill,
            CashNoteIn,
            CashNoteOut,
            Repay,
            Payment,
            Exchange,
            Pull_Money,
        }
        /// <summary>
        /// هذي على شان ما افعلش عدة شاشات قدي واحدة ةالبيانات كله على حسب النوع
        /// </summary>
        public enum Accounts_Balance_Type :byte
        {
            Supplier = 1,
            Customer ,
            Employee ,
            Drawer
        }
    }
}
