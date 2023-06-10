using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.Classes
{
    public static class Table_View
    {
        public class Product_And_Category_And_Units_View
        {
            /// <summary>
            /// هذي الدالة ترجع فقط صف واحد حسب رقم المنتج عشان عنكون نستدخدمها في
            /// كلاس مراقب قاعدة البيانات 
            /// وهذي بس عشان نضيفها في الواجهه لاني في الاصل قد مضافة في القاعدة 
            /// باقي نشتي نضيفها في الواجهه فلهذا بنحدث القريد ايا كانت
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public static Product_And_Category_And_Units_View Get_Product_By_ID(int pid)
            {
                List<Product_And_Category_And_Units_View> data = DAL.Impelement_Stored_Procedure.Get_The_Full_Pro(
                    "[FinalSalesDB].[dbo].[Get_Single_Full_Product]",new { proID = pid });
                return data.FirstOrDefault();
            }
            public int ID { get; set; }                                   
            public string Code { get; set; }                              
            public string Name { get; set; }                              
            public byte Type { get; set; }                    
            public bool Is_Active { get; set; }
            public double Order_Limit { get; set; }                   
            public int Cate_ID { get; set; }
            public string Cate_Name { get; set; }
            public byte Cost_Calc_Method { get; set; }
            public string Discribtion { get; set; }
            public List<Product_Unit> pro_Unit { get; set; } = new List<Product_Unit>();
                                                            
            public class Product_Unit
            {
                public int pro_Un_ID { get; set; }
                public int Unit_ID { get; set; }
                public string Unit_Name { get; set; }
                public double Factor { get; set; }
                public double Buy_Price { get; set; }
                public double Sell_Price { get; set; }
                public double Sell_Discount { get; set; }
                public string BarCode { get; set; }
            }
            /*
             هذا الكلاس عملناه عشان نعمل سع الفيو ومعد نعيدش الكود حق الاستدعاء مرتين 
             لان احنا كنا محتاجين نفس الاستعلام في قائمة المنتجات ونحتاجه مرة ثانية في الفاتورة
             فعنتعامل مع هذا الكلاس سع الليست المترابطة  وعيتوضح الكود عند التنفيذ
             عنستخدمه في كلاس السيشن
             */
        }
        public class Full_Coupes_Account : DBModels.Coupes_Of_Account
        {
            public string Name { get; set; }
            public string Real_Name { get; set; }
        }

    }
}
