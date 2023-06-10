using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.Classes
{
    public class Convert_List_To_DataTable
    {
        public static DataTable Convert_Coupes_List_To_Table(List<DBModels.Coupes_Of_Account> data  = null, BindingList<DBModels.Coupes_Of_Account> bdata = null)
        {
            using (DataTable dataTable = new DataTable())
            {
                dataTable.Columns.Add("Code", typeof(string));
                dataTable.Columns.Add("Account_ID", typeof(int));
                dataTable.Columns.Add("Debit", typeof(double));
                dataTable.Columns.Add("Credit", typeof(double));
                dataTable.Columns.Add("Insert_Date", typeof(DateTime));
                dataTable.Columns.Add("Source_Type", typeof(byte));
                dataTable.Columns.Add("Source_ID", typeof(int));
                dataTable.Columns.Add("Notes", typeof(string));
                dataTable.Columns.Add("Move_Name", typeof(byte));
                dataTable.Columns.Add("User_ID", typeof(int));
                if(data!=null)
                    foreach (var item in data)
                    {//todo comehere
                        dataTable.Rows.Add(item.Code, item.Account_ID
                            , item.Debit, item.Credit, item.Insert_Date, item.Source_Type, item.Source_ID
                            , item.Notes, item.Move_Name, item.User_ID);
                    };
                if (bdata != null)
                    foreach (var item in bdata)
                    {//todo comehere
                        dataTable.Rows.Add(item.Code, item.Account_ID
                            , item.Debit, item.Credit, item.Insert_Date, item.Source_Type, item.Source_ID
                            , item.Notes, item.Move_Name, item.User_ID);
                    };
                return dataTable;
            }
        }
        public static DataTable Convert_ProMove_List_To_Table(List<DBModels.Pro_Store_Movement> data)
        {
            using (DataTable dataTable = new DataTable())
            {
                dataTable.Columns.Add("Source_Type", typeof(byte));
                dataTable.Columns.Add("Source_ID", typeof(int));
                dataTable.Columns.Add("Product_ID", typeof(int));
                dataTable.Columns.Add("Store_ID", typeof(int));
                dataTable.Columns.Add("Export_Qty", typeof(double));
                dataTable.Columns.Add("Import_Qty", typeof(double));
                dataTable.Columns.Add("Cost_Value", typeof(double));
                dataTable.Columns.Add("Insert_Date", typeof(DateTime));
                dataTable.Columns.Add("Notes", typeof(string));
                dataTable.Columns.Add("User_ID", typeof(int));
                foreach (var item in data)
                {//todo comehere
                    dataTable.Rows.Add(item.Source_Type, item.Source_ID
                        , item.Product_ID, item.Store_ID, item.Export_Qty, item.Import_Qty, item.Cost_Value
                        , item.Insert_Date, item.Notes, item.User_ID);
                };
                return dataTable;
            }
        }
        public static DataTable Convert_Opening_Destructor_Details_List_To_Table(List<DBModels.Opening_Destructor_Details> data)
        {
            using (DataTable dataTable = new DataTable())
            {
                dataTable.Columns.Add("Open_Dest_ID", typeof(int));
                dataTable.Columns.Add("Product_ID", typeof(int));
                dataTable.Columns.Add("Unit_ID", typeof(int));
                dataTable.Columns.Add("Qty", typeof(double));
                dataTable.Columns.Add("Price", typeof(double));
                dataTable.Columns.Add("Total_Price", typeof(double));
                foreach (var item in data)
                {//todo comehere
                    dataTable.Rows.Add(item.Open_Dest_ID, item.Product_ID
                        , item.Unit_ID, item.Qty, item.Price, item.Total_Price);
                };
                return dataTable;
            }
        }
        public static DataTable Convert_ID_List_To_Table(List<int> data)
        {
            using (DataTable dataTable = new DataTable())
            {
                dataTable.Columns.Add("ID", typeof(int));
                foreach (var item in data)
                {
                    dataTable.Rows.Add(item);
                };
                return dataTable;
            }
        }
    }
}
