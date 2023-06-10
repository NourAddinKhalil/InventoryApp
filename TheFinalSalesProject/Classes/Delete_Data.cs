using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.Classes
{
    public static class Delete_Data
    {
        public static void Delete_Pro_Store_Move_Details_For_Invoices(int bill_ID, bill_Type itype)
        {
            DAL.Impelement_Stored_Procedure.Excute_Proce(@"Delete From [FinalSalesDB].[dbo].[Pro_Store_Movement]
                     Where [Source_Type] = @type And [Source_ID] IN(
                                                                Select ID From [FinalSalesDB].[dbo].[Invoice_Details] 
                                                                Where [Invoice_ID] = @invID)",
                                                new
                                                {
                                                    type = Convert.ToByte(itype),
                                                    invID = bill_ID
                                                });
        }
        public static void Delete_Invoice_Details(int invoiceID)
        {
            DAL.Impelement_Stored_Procedure.Excute_Proce(
                    @"Delete From [FinalSalesDB].[dbo].[Invoice_Details] Where [Invoice_ID] = @invID", 
                    new { invID = invoiceID });
        }
        public static void Delete_Coupe_Of_Accounts(int Source_ID, byte Source_Type)
        {
            DAL.Impelement_Stored_Procedure.Excute_Proce(@"Delete From [FinalSalesDB].[dbo].[Coupes_Of_Account] 
                    Where [Source_ID] = @id And [Source_Type] = @type", 
                    new { id = Source_ID, type = Source_Type });
        }
        public static void Delete_Pro_Store_Move_Details_By_ProID(int pro_ID, Source_Type itype, int store_ID)
        {
            DAL.Impelement_Stored_Procedure.Excute_Proce(@"Delete From [FinalSalesDB].[dbo].[Pro_Store_Movement]
                     Where [Source_Type] = @type And [Product_ID] = @proID And [Store_ID] = @strID
                     And [Source_ID] = 0",
                                                new
                                                {
                                                    type = Convert.ToByte(itype),
                                                    proID = pro_ID,
                                                    strID = store_ID
                                                });
        }
        public static void Delete_Pro_Store_Move_Details_For_OpenDestruct(int openID, Store_Balance_Type type)
        {
            DAL.Impelement_Stored_Procedure.Excute_Proce(@"Delete From [FinalSalesDB].[dbo].[Pro_Store_Movement]
                     Where [Source_Type] = @type And [Source_ID] IN(
                                                                Select ID From [FinalSalesDB].[dbo].[Opening_Destructor_Details] 
                                                                Where [Open_Dest_ID] = @odID)",
                                                new
                                                {
                                                    type = Convert.ToByte(type),
                                                    odID = openID
                                                });
        }
        public static void Delete_OpenDestrct_Details(int OpenID)
        {
            DAL.Impelement_Stored_Procedure.Excute_Proce(
                @"Delete From [FinalSalesDB].[dbo].[Opening_Destructor_Details] Where [Open_Dest_ID] = @id",
                new { id = OpenID });
        }
        public static void Delete_Pro_Store_Move_Details_For_Transfer(int trnasferID, Store_Balance_Type type)
        {
            DAL.Impelement_Stored_Procedure.Excute_Proce(@"Delete From [FinalSalesDB].[dbo].[Pro_Store_Movement]
                     Where [Source_Type] = @type And [Source_ID] IN(
                                                                Select ID From [FinalSalesDB].[dbo].[Transfer_Bal_Details] 
                                                                Where [Transfer_ID] = @trID)",
                                                new
                                                {
                                                    type = Convert.ToByte(type),
                                                    trID = trnasferID
                                                });
        }
        public static void Delete_Transfer_Bal_Details(int OpenID)
        {
            DAL.Impelement_Stored_Procedure.Excute_Proce(
                @"Delete From [FinalSalesDB].[dbo].[Transfer_Bal_Details] Where [Transfer_ID] = @id",
                new { id = OpenID });
        }

    }
}
