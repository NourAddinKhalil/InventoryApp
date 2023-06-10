using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Abstracts;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
using System.Data.SqlClient;
using TheFinalSalesProject.MyForms;

namespace TheFinalSalesProject.Classes
{
    public class DataBaseWatchTower
    {
        public static SqlTableDependency<DBModels.Product> products;
        public static void Product_Changed(object sender, RecordChangedEventArgs<DBModels.Product> e)
        {
            Frm_Main_Window.Main_Window_Ins.Invoke(new Action(() =>
            {
                if (e.ChangeType == ChangeType.Insert)
                {
                    Session.products.Add(e.Entity);
                    Session.Full_products.Add(Table_View.Product_And_Category_And_Units_View.Get_Product_By_ID(e.Entity.ID));
                }
                else if (e.ChangeType == ChangeType.Update)
                {
                    var indx = Session.products.IndexOf(Session.products.FirstOrDefault(x => x.ID == e.Entity.ID));
                    Session.products.RemoveAt(indx);
                    Session.products.Insert(indx, e.Entity);

                    var viewindx = Session.Full_products.IndexOf(Session.Full_products.FirstOrDefault(x => x.ID == e.Entity.ID));
                    Session.Full_products.RemoveAt(viewindx);
                    Session.Full_products.Insert(viewindx, Table_View.Product_And_Category_And_Units_View.Get_Product_By_ID(e.Entity.ID));
                }
                else if (e.ChangeType == ChangeType.Delete)
                {
                    Session.products.Remove(Session.products.FirstOrDefault(x => x.ID == e.Entity.ID));
                    Session.Full_products.Remove(Session.Full_products.FirstOrDefault(x => x.ID == e.Entity.ID));
                }
            }
            ));
        }

        public static SqlTableDependency<DBModels.Pro_Store_Movement> proMovement;
        public static void ProMovement_Changed(object sender, RecordChangedEventArgs<DBModels.Pro_Store_Movement> e)
        {
            Frm_Main_Window.Main_Window_Ins.Invoke(new Action(() =>
            {
                var balance = Inventory_Calculations.Get_Product_Balance_In_Store(e.Entity.Product_ID, e.Entity.Store_ID);
                Session.Pro_Balance.Remove(Session.Pro_Balance.FirstOrDefault(x => x.Product_ID == e.Entity.Product_ID
                && x.Store_ID == e.Entity.Store_ID));
                Session.Pro_Balance.Add(new Product_Balance
                {
                    Product_ID = e.Entity.Product_ID,
                    Store_ID = e.Entity.Store_ID,
                    Balance = balance
                });
            }));
        }

        public static SqlTableDependency<DBModels.Customer_Supplier> supplier;
        public static void Supplier_Changed(object sender, RecordChangedEventArgs<DBModels.Customer_Supplier> e)
        {
            Application.OpenForms[0].Invoke(new Action(() =>
            {
                switch (e.ChangeType)
                {
                    case ChangeType.None:
                        break;
                    case ChangeType.Delete:
                        Session.Suppliers.Remove(Session.Suppliers.FirstOrDefault(x => x.ID == e.Entity.ID));
                        break;
                    case ChangeType.Insert:
                        Session.Suppliers.Add(e.Entity);
                        break;
                    case ChangeType.Update:
                        var indx = Session.Suppliers.IndexOf(Session.Suppliers.FirstOrDefault(x => x.ID == e.Entity.ID));
                        Session.Suppliers.Remove(Session.Suppliers.FirstOrDefault(x => x.ID == e.Entity.ID));
                        Session.Suppliers.Insert(indx, e.Entity);
                        break;
                    default:
                        break;
                }
            }));
        }
        public class Suppliers_Only : ITableDependencyFilter
        {
            public string Translate()
            {
                return "[Type] = 1 Or [Type] = 3";
            }
        }

        public static SqlTableDependency<DBModels.Customer_Supplier> customer;
        public static void Customer_Changed(object sender, RecordChangedEventArgs<DBModels.Customer_Supplier> e)
        {
            Frm_Main_Window.Main_Window_Ins.Invoke(new Action(() =>
            {
                switch (e.ChangeType)
                {
                    case ChangeType.None:
                        break;
                    case ChangeType.Delete:
                        Session.Customers.Remove(Session.Customers.FirstOrDefault(x => x.ID == e.Entity.ID));
                        break;
                    case ChangeType.Insert:
                        Session.Customers.Add(e.Entity);
                        break;
                    case ChangeType.Update:
                        var indx = Session.Customers.IndexOf(Session.Customers.FirstOrDefault(x => x.ID == e.Entity.ID));
                        Session.Customers.Remove(Session.Customers.FirstOrDefault(x => x.ID == e.Entity.ID));
                        Session.Customers.Insert(indx, e.Entity);
                        break;
                    default:
                        break;
                }
            }));
        }
        public class Customer_Only : ITableDependencyFilter
        {
            public string Translate()
            {//"[Is_Customer] = 'true'"
                return "[Type] = 2 Or [Type] = 3";
            }
        }

        public static SqlTableDependency<DBModels.Store> store;
        public static void Store_Changed(object sender, RecordChangedEventArgs<DBModels.Store> e)
        {
            Frm_Main_Window.Main_Window_Ins.Invoke(new Action(() =>
            {
                switch (e.ChangeType)
                {
                    case ChangeType.None:
                        break;
                    case ChangeType.Delete:
                        Session.Stores.Remove(Session.Stores.FirstOrDefault(x => x.ID == e.Entity.ID));
                        break;
                    case ChangeType.Insert:
                        Session.Stores.Add(e.Entity);
                        break;
                    case ChangeType.Update:
                        var indx = Session.Stores.IndexOf(Session.Stores.FirstOrDefault(x => x.ID == e.Entity.ID));
                        // Session.Stores[indx] = e.Entity; same
                        Session.Stores.Remove(Session.Stores.FirstOrDefault(x => x.ID == e.Entity.ID));
                        Session.Stores.Insert(indx, e.Entity);
                        break;
                    default:
                        break;
                }
            }));
        }

        public static SqlTableDependency<DBModels.Accounts> account;
        public static void Account_Changed(object sender, RecordChangedEventArgs<DBModels.Accounts> e)
        {
            Frm_Main_Window.Main_Window_Ins.Invoke(new Action(() =>
            {
                switch (e.ChangeType)
                {
                    case ChangeType.None:
                        break;
                    case ChangeType.Delete:
                        Session.Accounts.Remove(Session.Accounts.FirstOrDefault(x => x.ID == e.Entity.ID));
                        break;
                    case ChangeType.Insert:
                        Session.Accounts.Add(e.Entity);
                        break;
                    case ChangeType.Update:
                        var indx = Session.Accounts.IndexOf(Session.Accounts.FirstOrDefault(x => x.ID == e.Entity.ID));
                        // Session.Stores[indx] = e.Entity; same
                        Session.Accounts.Remove(Session.Accounts.FirstOrDefault(x => x.ID == e.Entity.ID));
                        Session.Accounts.Insert(indx, e.Entity);
                        break;
                    default:
                        break;
                }
            }));
        }

        public static SqlTableDependency<DBModels.Unit> unit;
        public static void Unit_Changed(object sender, RecordChangedEventArgs<DBModels.Unit> e)
        {
            Frm_Main_Window.Main_Window_Ins.Invoke(new Action(() =>
            {
                switch (e.ChangeType)
                {
                    case ChangeType.None:
                        break;
                    case ChangeType.Delete:
                        Session.Units.Remove(Session.Units.FirstOrDefault(x => x.ID == e.Entity.ID));
                        break;
                    case ChangeType.Insert:
                        Session.Units.Add(e.Entity);
                        break;
                    case ChangeType.Update:
                        var indx = Session.Units.IndexOf(Session.Units.FirstOrDefault(x => x.ID == e.Entity.ID));
                        // Session.Stores[indx] = e.Entity; same
                        Session.Units.Remove(Session.Units.FirstOrDefault(x => x.ID == e.Entity.ID));
                        Session.Units.Insert(indx, e.Entity);
                        break;
                    default:
                        break;
                }
            }));
        }

        public static SqlTableDependency<DBModels.Product_Unit> product_Units;
        public static void Product_Units_Changed(object sender, RecordChangedEventArgs<DBModels.Product_Unit> e)
        {
            Frm_Main_Window.Main_Window_Ins.Invoke(new Action(() =>
            {
                var viewindx = Session.Full_products.IndexOf(Session.Full_products.FirstOrDefault(x => x.ID == e.Entity.Pro_ID));
                switch (e.ChangeType)
                {
                    case ChangeType.None:
                        break;
                    case ChangeType.Delete://todo come here optmize the code
                        Session.Product_Units.Remove(Session.Product_Units.FirstOrDefault(x => x.ID == e.Entity.ID));
                        Session.Full_products.RemoveAt(viewindx);
                        Session.Full_products.Insert(viewindx, Table_View.Product_And_Category_And_Units_View.Get_Product_By_ID(e.Entity.Pro_ID));
                        break;
                    case ChangeType.Insert:
                        Session.Product_Units.Add(e.Entity);
                        Session.Full_products.RemoveAt(viewindx);
                        Session.Full_products.Insert(viewindx, Table_View.Product_And_Category_And_Units_View.Get_Product_By_ID(e.Entity.Pro_ID));
                        break;
                    case ChangeType.Update:
                        var indx = Session.Product_Units.IndexOf(Session.Product_Units.FirstOrDefault(x => x.ID == e.Entity.ID));
                        Session.Product_Units.RemoveAt(indx);
                        Session.Product_Units.Insert(indx, e.Entity);
                        Session.Full_products.RemoveAt(viewindx);
                        Session.Full_products.Insert(viewindx, Table_View.Product_And_Category_And_Units_View.Get_Product_By_ID(e.Entity.Pro_ID));
                        break;
                    default:
                        break;
                }
                //var viewindx = Session.Full_products.IndexOf(Session.Full_products.FirstOrDefault(x => x.ID == e.Entity.Pro_ID));
                //Session.Pro_Cate_Views.RemoveAt(viewindx);
                //Session.Full_products[viewindx] = Table_View.Product_And_Category_And_Units_View.Get_Product_By_ID(e.Entity.Pro_ID);
            }
             ));
        }
    }
}
