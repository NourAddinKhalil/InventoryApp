using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.Classes
{
    public static class Master_Class
    {
        public class Types
        {
            public byte ID { get; set; }
            public string Name { get; set; }
        }
        public static List<Types> product_Type = new List<Types>()
        {
                new Types() { ID = (byte)ProductTypesEnum.Invetory,Name = "مخزني"},
                new Types() { ID = (byte)ProductTypesEnum.Service,Name = "خدمي" }
        };
        public static List<Types> part_Type_List = new List<Types>()
        {
                new Types() { ID = (byte)Part_Type.Supplier ,Name = "مورد"},
                new Types() { ID = (byte)Part_Type.Customer ,Name = "عميل" }
        };
        public static List<Types> part_Type_List_For_Notes = new List<Types>()
        {
                new Types() { ID = Convert.ToByte(Part_Type.Supplier) ,Name = "مورد"},
                new Types() { ID = Convert.ToByte(Part_Type.Customer) ,Name = "عميل" },
                new Types() { ID = Convert.ToByte(Part_Type.Account) ,Name = "حساب" },
                new Types() { ID = Convert.ToByte(Part_Type.Employee) ,Name = "موظف" }
        };
        public static List<Types> invoices_Type_List = new List<Types>()
        {
                   new Types() { ID = Convert.ToByte(bill_Type.Buy) ,Name = "فاتورة شراء"},
                new Types() { ID = Convert.ToByte(bill_Type.Sale) ,Name = "فاتورة بيع" },
                new Types() { ID = Convert.ToByte(bill_Type.BuyReturn) ,Name = "فاتورة مرتجع شراء" },
                new Types() { ID = Convert.ToByte(bill_Type.SaleReturn) ,Name = "فاتورة مرتجع بيع" }
        };
        public static List<Types> Pay_Method_List = new List<Types>()
        {
                new Types() { ID = (byte)Pay_Mode.Cash, Name = "نقدي"},
                new Types() { ID = (byte)Pay_Mode.Credit, Name = "آجل" },
        };
        public static List<Types> Warning_Handel_List = new List<Types>()
        {
                new Types() { ID = (byte)Warining_Handel.Do_Not_Interrupt, Name = "عدم التدخل"},
                new Types() { ID = (byte)Warining_Handel.Show_Warning, Name = "عرض رسالة التحذير" },
                new Types() { ID = (byte)Warining_Handel.Prevent, Name = "منع العملية" },
        };
        public static List<Types> User_Type_List = new List<Types>()
        {
                new Types() { ID = (byte)User_Type.Admin, Name = "مدير النظام"},
                new Types() { ID = (byte)User_Type.User, Name = "دخول مخصص" },
        };
        public static List<Types> Cost_Calc_Method_List = new List<Types>()
        {
                new Types() { ID = (byte)Cost_Calc_Method.Fifo, Name = "الوارد أولاً الصادر أولاً"},
                new Types() { ID = (byte)Cost_Calc_Method.Lifo, Name = "الوارد أخيراً الصادر أولاً" },
                new Types() { ID = (byte)Cost_Calc_Method.WAC, Name = "المتوسط المرجح" },
        };
        public static List<Types> Source_Type_List = new List<Types>()
        {
                new Types() { ID = Convert.ToByte(Source_Type.Buy), Name = "مشتريات"},
                new Types() { ID = Convert.ToByte(Source_Type.Sale), Name = "مبيعات" },
                new Types() { ID = Convert.ToByte(Source_Type.SaleReturn), Name = "مرتجع بيع" },
                new Types() { ID = Convert.ToByte(Source_Type.BuyReturn), Name = "مرتجع شراء" },
                new Types() { ID = Convert.ToByte(Source_Type.CashNoteIn), Name = "سند قبض" },
                new Types() { ID = Convert.ToByte(Source_Type.CashNoteOut), Name = "سند صرف" },
                new Types() { ID = Convert.ToByte(Source_Type.OpeningAccount), Name = "رصيد إفتتاحي" },
                new Types() { ID = Convert.ToByte(Source_Type.Destructor), Name = "إهلاك رصيد" },
                new Types() { ID = Convert.ToByte(Source_Type.Transfer_Balance), Name = "نقل بضاعة" },
                new Types() { ID = Convert.ToByte(Source_Type.Manually_Rigister), Name = "قيد يدوي" },
                new Types() { ID = Convert.ToByte(Source_Type.Exchange_Money), Name = "تحويل رصيد" },
                new Types() { ID = Convert.ToByte(Source_Type.Pull_Money), Name = "سحب رصيد" },
        };
        public static List<Types> Moves_Name_List = new List<Types>()
        {
                new Types() { ID = Convert.ToByte(Account_Move_Name.Purchase_Bill), Name = "مشتريات"},
                new Types() { ID = Convert.ToByte(Account_Move_Name.Sales_Bill), Name = "مبيعات" },
                new Types() { ID = Convert.ToByte(Account_Move_Name.Sales_Return_Bill), Name = "مرتجع بيع" },
                new Types() { ID = Convert.ToByte(Account_Move_Name.Purchase_Return_Bill), Name = "مرتجع شراء" },
                new Types() { ID = Convert.ToByte(Account_Move_Name.CashNoteIn), Name = "سند قبض" },
                new Types() { ID = Convert.ToByte(Account_Move_Name.CashNoteOut), Name = "سند صرف" },
                new Types() { ID = Convert.ToByte(Account_Move_Name.Opening_Balance), Name = "رصيد إفتتاحي" },
                new Types() { ID = Convert.ToByte(Account_Move_Name.Payment), Name = "دفعة" },
                new Types() { ID = Convert.ToByte(Account_Move_Name.Repay), Name = "سداد" },
                new Types() { ID = Convert.ToByte(Account_Move_Name.Exchange), Name = "تحويل رصيد" },
                new Types() { ID = Convert.ToByte(Account_Move_Name.Pull_Money), Name = "سحب رصيد" },
        };
        public static List<Types> Drawer_Balance_Type_List = new List<Types>()
        {
                new Types() { ID = Convert.ToByte(Drawer_Balance_Type.Opening_Account), Name = "رصيد إفتتاحي" },
                new Types() { ID = Convert.ToByte(Drawer_Balance_Type.Exchange_Money), Name = "تحويل رصيد" },
                new Types() { ID = Convert.ToByte(Drawer_Balance_Type.Pull_Money), Name = "سحب رصيد" },
        };

        public static void Add_Button_In_Control(this PopupBaseEdit baseEdit)
        {
            baseEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[]
            {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear,"مسح")
            });
            baseEdit.ButtonPressed += (sender, e) =>
            {
                if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)
                {
                    //if(sender is LookUpEdit)
                    //    ((LookUpEdit)sender).EditValue = 0;
                    if (sender is LookUpEditBase)
                        ((LookUpEditBase)sender).EditValue = 0;
                    else
                    ((PopupBaseEdit)sender).EditValue = null;
                }
            };
        }
        public static Image Convert_Byte_To_Image(byte[] imageArray)
        {
            if (imageArray == null) return null;
            using (MemoryStream stream = new MemoryStream(imageArray, false))
            {
                Image img = null;
                try
                {
                    img = Image.FromStream(stream);
                    return img;
                }
                catch
                {
                    img = null;
                    return img;
                }
            }
        }
        public static byte[] Convert_Image_To_Byte(Image pic)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    if (pic == null) return null;
                    pic.Save(stream, ImageFormat.Jpeg);
                    return stream.ToArray();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return stream.ToArray();
                }
            }

        }
        public static string Get_Next_Number_InTheString(string maxCode)
        {
            if (string.IsNullOrEmpty(maxCode))
                return "1";
            string str1 = "";
            foreach (char c in maxCode)
            {
                str1 = char.IsDigit(c) ? str1 + c : "";
            }
            if (string.IsNullOrEmpty(str1))
                return "1";
            string str2 = str1.Insert(0, "1");
            str2 = (Convert.ToInt64(str2) + 1).ToString();
            string str3 = str2[0] == '1' ? str2.Remove(0, 1) : str2.Remove(0, 1).Insert(0, "1");
            int indx = maxCode.LastIndexOf(str1);
            maxCode = maxCode.Remove(indx);
            maxCode = maxCode.Insert(indx, str3);
            return maxCode;
        }
        public static int Find_Handel_Row_By_Object(this GridView view, object row)
        {
            if (row != null)
            {
                for (int i = 0; i < view.RowCount; i++)
                {
                    if (row.Equals(view.GetRow(i)))
                        return i;
                }
            }
            return GridControl.InvalidRowHandle;
        }
        /// <summary>
        /// هذي الدالة تعيد لنا قيمة الخاصية الحالية
        /// بحيث اذا كانت ترو او فلس او اي قيمةس
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T From_Byte_Array_To_AnyType<T>(byte[] data)
        {
            if (data == null)
                return default(T);
            using (MemoryStream ms = new MemoryStream(data))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                object obj = formatter.Deserialize(ms);
                return (T)obj;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] From_AnyType_To_Byte_Array<T>(T data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                if (data == null)
                    return null;
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, data);
                return ms.ToArray();
            }
        }
        /// <summary>
        /// هذي الدالة ترجع قيمة الخاصية على شكل مصفوفة ثنائية
        /// وبعدا نرسلها للدالة اعلاه لكي ترجع لنا قيمة المصفوفة
        /// </summary>
        /// <param name="propeName"></param>
        /// <param name="profileID"></param>
        /// <returns></returns>
        public static byte[] Get_Property_Value(string propeName, int profileID)
        {
            var propVal = DAL.Impelement_Stored_Procedure.SelectData<DBModels.User_Profile_Property>(
                          @"Select * From User_Profile_Property 
                                Where Profile_ID = @profID And Prope_Name = @propName",
                          new
                          {
                              profID = profileID,
                              propName = propeName
                          }).FirstOrDefault();
            if (propVal == null)
                return null;
            return propVal.Prope_Value.ToArray();
        }
        /// <summary>
        /// هذي الدالة ترجع لنا اسم الخاصية اة الدالة ذي استدعتها
        /// في وقت التنفيذ بحيث ما نحتاج نكتب اسم كل دالة
        /// </summary>
        /// <param name="callerName"></param>
        /// <returns></returns>
        public static string Get_Property_Name([CallerMemberName] string callerName = "")
        {
            return callerName;
        }
        public static void Add_DeleteColumns_To_GridView(this GridView view , GridControl control)
        {
            RepositoryItemButtonEdit buttonEdit = new RepositoryItemButtonEdit();
            buttonEdit.Buttons.Clear();
            buttonEdit.Buttons.Add(new EditorButton(ButtonPredefines.Delete));
            control.RepositoryItems.Add(buttonEdit);
            buttonEdit.TextEditStyle = TextEditStyles.HideTextEditor;
            GridColumn deleteClmn = new GridColumn()
            {
                Name = "colDelete",
                FieldName = "Delete",
                Caption = "حذف",
                ColumnEdit = buttonEdit,
                VisibleIndex = 100,
                Width = 10,
                MaxWidth = 15
            };
            view.Columns.Add(deleteClmn);
            buttonEdit.ButtonClick += ButtonEdit_ButtonClick;
        }
        public static void Add_NumberColumn_To_GridView(this GridView view, GridControl control)
        {
            view.Columns.Add(new GridColumn()
            {
                Name = "clmnNumber",
                FieldName = "Number",
                MaxWidth = 30,
                Caption = "م",
                UnboundType = UnboundColumnType.Integer,
                VisibleIndex = 0,
            });
            view.Columns["Number"].OptionsColumn.AllowFocus = false;
            view.CustomUnboundColumnData += View_CustomUnboundColumnData;
        }
        private static void View_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GridView view = sender as GridView;
            if (view != null)
            {
                if (e.Column.FieldName == "Number")
                    e.Value = view.GetVisibleRowHandle(e.ListSourceRowIndex) + 1;
            }
        }
        private static void ButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            GridView view = ((GridControl)((ButtonEdit)sender).Parent).MainView as GridView;
            if (view.FocusedRowHandle >= 0)
            {
                if(view.Name == "BillDetailsGridView")
                {
                    var row = view.GetRow(view.GetFocusedDataSourceRowIndex()) as DBModels.Invoice_Details;
                    if(!Session.Current_User_Settings_Prope.Invoice_Settings.CanChangeDeleteItemsInBills
                        && row.Invoice_ID != 0)
                    {
                        XtraMessageBox.Show(" غير مصرح لك حذف عناصر من الفاتورة ", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
                view.DeleteRow(view.FocusedRowHandle);
                view.RefreshData();
            }
        }
        public static void Add_CodeColumn_To_GridView(this GridView view, GridControl control)
        {
            view.Columns.Add(new GridColumn()
            {
                Name = "clmnName",
                FieldName = "Code",
                Caption = "الكود",
                UnboundType = UnboundColumnType.String
            });
        }
        public static void Add_BalanceColumn_To_GridView(this GridView view, GridControl control)
        {
            view.Columns.Add(new GridColumn()
            {
                Name = "clmnBalance",
                FieldName = "Balance",
                Caption = "الرصيد",
                UnboundType = UnboundColumnType.Decimal
            });
        }
        public static void Add_Spn_Repo_Value(this GridView view , GridControl control , string columnName)
        {
            RepositoryItemSpinEdit repoMSpnEdit = new RepositoryItemSpinEdit();
            repoMSpnEdit.Increment = 1m;
            repoMSpnEdit.Mask.EditMask = "n2";
            repoMSpnEdit.Mask.UseMaskAsDisplayFormat = true;
            repoMSpnEdit.MinValue = 0;
            control.RepositoryItems.Add(repoMSpnEdit);
            view.Columns[columnName].ColumnEdit = repoMSpnEdit;
        }
        public static void Add_Spn_Repo_Ratio(this GridView view, GridControl control, string columnName)
        {
            RepositoryItemSpinEdit repoRatioSpnEdit = new RepositoryItemSpinEdit();
            repoRatioSpnEdit.Increment = 0.01m;
            repoRatioSpnEdit.Mask.EditMask = "p";
            repoRatioSpnEdit.Mask.UseMaskAsDisplayFormat = true;
            repoRatioSpnEdit.MaxValue = 1;
            repoRatioSpnEdit.MaxValue = 0;
            control.RepositoryItems.Add(repoRatioSpnEdit);
            view.Columns[columnName].ColumnEdit = repoRatioSpnEdit;
        }
        public static void Add_Sum_Summary_To_Grid_Footer(this GridView view, string columnName)
        {
            GridColumnSummaryItem item = new GridColumnSummaryItem()
            {
                SummaryType = SummaryItemType.Sum,
                FieldName = columnName,
                DisplayFormat = "{0:n2}"
            };
            view.Columns[columnName].Summary.Add(item);
            view.Appearance.FooterPanel.Font = new Font("Sylfaen", 12, FontStyle.Bold);
        }
        public static void Add_Sum_Group_Summary_To_Grid_Footer(this GridView view, string columnName)
        {
            GridGroupSummaryItem item = new GridGroupSummaryItem()
            {
                SummaryType = SummaryItemType.Sum,
                FieldName = columnName,
                DisplayFormat = "{0:n2}",
                ShowInGroupColumnFooter = view.Columns[columnName],
            };
            view.Appearance.GroupFooter.Font = new Font("Sylfaen", 12, FontStyle.Bold);
            view.GroupSummary.Add(item);
        }
        public static void Add_Memo_Repo_Item(this GridView view, GridControl control, string columnName)
        {
            RepositoryItemMemoEdit repoMemoEdit = new RepositoryItemMemoEdit();
            control.RepositoryItems.Add(repoMemoEdit);
            view.Columns[columnName].ColumnEdit = repoMemoEdit;
        }
    }
}
