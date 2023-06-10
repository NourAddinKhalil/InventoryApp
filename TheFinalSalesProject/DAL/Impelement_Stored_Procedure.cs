using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using static TheFinalSalesProject.Classes.Table_View;

namespace TheFinalSalesProject.DAL
{
    public static class Impelement_Stored_Procedure
    {
        public static List<Model> SelectData<Model>(string proce_Name, object parameter = null, bool isCommandText = true)
        {
            using (SqlConnection connection = new SqlConnection(DAL.GetConnection.Connection))
            {
                CommandType commandType = CommandType.Text;
                if (isCommandText == false)
                    commandType = CommandType.StoredProcedure;
                List<Model> list;
                Open(connection);
                if (parameter != null)
                    list = connection.Query<Model>(proce_Name, parameter, commandType: commandType).ToList();
                else
                    list = connection.Query<Model>(proce_Name, commandType: commandType).ToList();
                Close(connection);
                return list;
            }
        }
        static void Open(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }
        static void Close(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        /// <summary>
        /// ضروري اذا الامر داخل روتين في القاعدة نعمل 
        /// iscommandtext =false
        /// </summary>
        /// <param name="proce_Name"></param>
        /// <param name="parameter"></param>
        /// <param name="commit"></param>
        /// <param name="isCommandText"></param>
        public static void Excute_Proce(string proce_Name , object parameter = null, bool commit = true ,bool isCommandText = true)
        {
            using (SqlConnection connection = new SqlConnection(DAL.GetConnection.Connection))
            {
                CommandType commandType = CommandType.Text;
                if (isCommandText == false)
                    commandType = CommandType.StoredProcedure;
                Open(connection);
                using (var trans = connection.BeginTransaction())
                {
                    try
                    {
                        if (parameter != null)
                            connection.Execute(proce_Name, parameter,transaction: trans ,commandType: commandType);
                        else
                            connection.Execute(proce_Name, transaction: trans , commandType: commandType);
                        if (commit)
                            trans.Commit();
                    }
                    catch (Exception ex)//todo record error
                    {
                        MessageBox.Show(ex.Message);
                        trans.Rollback();
                    }
                }
                Close(connection);
            }
        }
        public static List<Model> Excute_Proce_And_Retrieve_ID<Model>(string proce_Name, object parameter = null, bool commit = true, bool isCommandText = true)
        {
            //Excute_Proce(proce_Name, parameter, commit, isCommandText);
            //return SelectData<Model>("Select convert(int ,SCOPE_IDENTITY()) As ID", null);
            using (SqlConnection connection = new SqlConnection(DAL.GetConnection.Connection))
            {
                proce_Name += @"
                           Select CAST(SCOPE_IDENTITY() As int ) As ID";

                CommandType commandType = CommandType.Text;
                if (isCommandText == false)
                    commandType = CommandType.StoredProcedure;
                List<Model> list = new List<Model>();
                Open(connection);
                using (var trans = connection.BeginTransaction())
                {
                    try
                    {
                        if (parameter != null)
                            list = connection.Query<Model>(proce_Name, parameter, transaction: trans, commandType: commandType).ToList();
                        else
                            list = connection.Query<Model>(proce_Name, transaction: trans, commandType: commandType).ToList();
                        if (commit)
                            trans.Commit();
                    }
                    catch (Exception ex)//todo record error
                    {
                        MessageBox.Show(ex.Message);
                        trans.Rollback();
                    }
                }
                Close(connection);
                return list;
            }
        }
        public static List<Product_And_Category_And_Units_View> Get_The_Full_Pro(string proce_Name ="" , object parameter = null)
        {
            using (SqlConnection connection = new SqlConnection(DAL.GetConnection.Connection))
            {
                if (proce_Name == "")
                    proce_Name = @"[FinalSalesDB].[dbo].[Get_The_Full_Product]";
                Open(connection);
                List<Product_And_Category_And_Units_View> list = new List<Product_And_Category_And_Units_View>();
                if (parameter != null)
                {
                    list = connection.Query<Product_And_Category_And_Units_View, Product_And_Category_And_Units_View.Product_Unit, Product_And_Category_And_Units_View>(proce_Name,
                    (product, Pro_Unit) =>
                    {
                        product.pro_Unit.Add(Pro_Unit);
                        return product;
                    }, splitOn: "ID ,pro_Un_ID", commandType: CommandType.StoredProcedure,
                    param: parameter).ToList();
                }
                else
                {
                    list = connection.Query<Product_And_Category_And_Units_View, Product_And_Category_And_Units_View.Product_Unit, Product_And_Category_And_Units_View>(proce_Name,
                    (product, Pro_Unit) =>
                    {
                        product.pro_Unit.Add(Pro_Unit);
                        return product;
                    }, splitOn: "ID ,pro_Un_ID", commandType: CommandType.StoredProcedure).ToList();
                }
                list = list.GroupBy(x => x.ID).Select(x =>
                {
                    var groupedPro = x.First();
                    groupedPro.pro_Unit = x.Select(p => p.pro_Unit.Single()).ToList();
                    return groupedPro;
                }).ToList();

                Close(connection);
                return list;
            }
        }
        //public static List<DBModels.Full_Guarnteed_Note> Get_The_Full_Guarnteed_Note(string proce_Name = "", object parameter = null)
        //{
        //    using (SqlConnection connection = new SqlConnection(DAL.GetConnection.Connection))
        //    {
        //        Open(connection);
        //        List<DBModels.Full_Guarnteed_Note> list = new List<DBModels.Full_Guarnteed_Note>();
        //        if (parameter != null)
        //        {
        //            list = connection.Query<DBModels.Full_Guarnteed_Note,DBModels.Guara_Links, DBModels.Full_Guarnteed_Note>(proce_Name,
        //            (notes, links) =>
        //            {
        //                notes.Link.Add(links);
        //                return notes;
        //            }, splitOn: "ID ,Link_ID", commandType: CommandType.StoredProcedure,
        //            param: parameter).ToList();
        //        }
        //        else
        //        {
        //            list = connection.Query<DBModels.Full_Guarnteed_Note, DBModels.Guara_Links, DBModels.Full_Guarnteed_Note>(proce_Name,
        //            (notes, links) =>
        //            {
        //                notes.Link.Add(links);
        //                return notes;
        //            }, splitOn: "ID ,Link_ID", commandType: CommandType.StoredProcedure).ToList();
        //        }
        //        list = list.GroupBy(x => x.ID).Select(x =>
        //        {
        //            var groupedPro = x.First();
        //            groupedPro.Link = x.Select(p => p.Link.Single()).ToList();
        //            return groupedPro;
        //        }).ToList();

        //        Close(connection);
        //        return list;
        //    }
        //}
        public static List<DBModels.Max_Invoice> Get_The_Max_Invoice(string proce_Name = "", object parameter = null)
        {
            using (SqlConnection connection = new SqlConnection(DAL.GetConnection.Connection))
            {
                Open(connection);
                List<DBModels.Max_Invoice> list = new List<DBModels.Max_Invoice>();
                if (parameter != null)
                {
                    list = connection.Query<DBModels.Max_Invoice, DBModels.FullInvoiceDetail, DBModels.FullInvoiceBack, DBModels.Max_Invoice>(proce_Name,
                    (max, detail , back) =>
                    {
                        max.InvoiceDetail.Add(detail);
                        max.InvoiceBack.Add(back);
                        return max;
                    }, splitOn: "ID ,DetID ,BID", commandType: CommandType.StoredProcedure,
                    param: parameter).ToList();
                }
                else
                {
                    list = connection.Query<DBModels.Max_Invoice, DBModels.FullInvoiceDetail, DBModels.FullInvoiceBack, DBModels.Max_Invoice>(proce_Name,
                    (max, detail, back) =>
                    {
                        max.InvoiceDetail.Add(detail);
                        max.InvoiceBack.Add(back);
                        return max;
                    }, splitOn: "ID ,DetID ,BID", commandType: CommandType.StoredProcedure).ToList();
                }
                list = list.GroupBy(x => x.ID).Select(x =>
                {
                    var groupedPro = x.First();
                    groupedPro.InvoiceBack = x.Select(p => p.InvoiceBack.Single()).ToList();
                    groupedPro.InvoiceDetail = x.Select(p => p.InvoiceDetail.Single()).ToList();
                    return groupedPro;
                }).ToList();

                Close(connection);
                return list;
            }
        }
        public static List<DBModels.Full_Invoice> Get_The_Full_Invoice_For_Print(string proce_Name = "", object parameter = null)
        {
            using (SqlConnection connection = new SqlConnection(DAL.GetConnection.Connection))
            {
                Open(connection);
                List<DBModels.Full_Invoice> list = new List<DBModels.Full_Invoice>();
                if (parameter != null)
                {
                    list = connection.Query<DBModels.Full_Invoice, DBModels.Detail, DBModels.Full_Invoice>(proce_Name,
                    (inv, detail) =>
                    {
                        inv.Details.Add(detail);
                        return inv;
                    }, splitOn: "ID ,DetID", commandType: CommandType.StoredProcedure,
                    param: parameter).ToList();
                }
                else
                {
                    list = connection.Query<DBModels.Full_Invoice, DBModels.Detail, DBModels.Full_Invoice>(proce_Name,
                    (inv, detail) =>
                    {
                        inv.Details.Add(detail);
                        return inv;
                    }, splitOn: "ID ,DetID", commandType: CommandType.StoredProcedure).ToList();
                }
                list = list.GroupBy(x => x.ID).Select(x =>
                {
                    var groupedPro = x.First();
                    groupedPro.Details = x.Select(p => p.Details.Single()).ToList();
                    return groupedPro;
                }).ToList();

                Close(connection);
                return list;
            }
        }
    }
}
