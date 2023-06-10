using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.Classes
{
    public static class Inventory_Calculations
    {
        /// <summary>
        /// هذي الدالة ترجع لنا قيمة التكلفة حسب النوع 
        /// </summary>
        /// <param name="pro_ID"></param>
        /// <param name="store_ID"></param>
        /// <param name="qty"></param>
        /// <returns></returns>
        public static double Get_Cost_Of_Next_Product(int pro_ID, int store_ID, double qty)
        {
            //هذا الاستدعاء يعيد لنا جميع حركة الصنف ضمن المخزن المحدد
            var qurey = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Pro_Store_Movement>(
                @"Select * From Pro_Store_Movement 
                           Where Store_ID = @store_ID And Product_ID = @pro_ID 
                           Order By Insert_Date",
                new
                {
                    store_ID = store_ID,
                    pro_ID = pro_ID
                });
            //todo come here
            //if (qurey.Count() <= 0)
            //{
            //    return Session.Pro_Cate_Views.Single(x => x.Product_ID == pro_ID).pro_Unit.OrderByDescending(x => x.Factor).First().Buy_Price;
            //}
            //هذا فيه مجموع كمية الصنف الصادرة
            double sumOut = qurey.Sum(x => (double?)x.Export_Qty) ?? 0;
            //هذا فيه الكمية المتاحة حاليا
            double balance = Get_Product_Balance_In_Store(pro_ID, store_ID);
            if (balance <= 0)
                return 0;
            //هذا فيه يعيد مجموع كمية الاصناف الواردة حيث يجب ان يكون التاريخ اقل من تاريخ الصنف ذي نشتي نبيعه
            //بحيث يكون مجموعهن اكبر من مجموع الكمية الصادرة
            var subQuery = qurey.Where(q => qurey.Where(x => ((double?)x.Import_Qty ?? 0) > 0 && x.Insert_Date <= q.Insert_Date)
             .Sum(x => ((double?)x.Import_Qty ?? 0)) > sumOut && q.Import_Qty > 0).ToList();
            //في هذا المتغير بيرجع لنا الفرق بين مجموع الكمية الصادرة ومجموع الكمية الواردة  الباقية وبعدا يخرج لنا منها ذي عنبيع
            double subQuerBalance = subQuery.Sum(x => x.Import_Qty) -
                subQuery.Sum(x => x.Export_Qty);
            //في هذا بنعالج المتوسط المرجح
            if (subQuerBalance > balance)
            {
                double diffrent = subQuerBalance - balance;
                subQuery[0].Import_Qty -= diffrent;
            }
            double fifo;
            double lifo;
            double wac;
            if (subQuery.Count <= 0)
                return 0;
            //fifo
            if (subQuery[0].Import_Qty < qty)
            {
                int i = 0;
                double qtyx = qty;
                double sumPrice = 0;
                while (qtyx > 0 && subQuery.Count() >= i)
                {
                    var row = subQuery[i];
                    double qty2 = (qtyx > row.Import_Qty) ? row.Import_Qty : qtyx;
                    sumPrice += qty2 * row.Cost_Value;
                    qtyx -= qty2;
                    i++;
                }
                //هنا نعطي التكلفة للوحدة من الكمية المعطاة من المستخدم
                //مثلا اذا يشتي يبيع اكثر من المتاح مثلا اذا طلب 20 وما معانا الا 10
                //فعيبقى في ال qtyx 10
                //وعنقصها من المطلوبة فعيقسم على على 10 وهذا المطلوب
                fifo = sumPrice / (qtyx - qty);
            }
            else
            {
                fifo = subQuery.First().Cost_Value;
            }
            //lifo
            subQuery = subQuery.OrderByDescending(x => x.Insert_Date).ToList();
            if (subQuery[0].Import_Qty < qty)
            {
                int i = 0;
                double qtyx = qty;
                double sumPrice = 0;
                while (qtyx > 0 && subQuery.Count() >= i)
                {
                    var row = subQuery[i];
                    double qty2 = (qtyx > row.Import_Qty) ? row.Import_Qty : qtyx;
                    sumPrice += qty2 * row.Cost_Value;
                    qtyx -= qty2;
                    i++;
                }
                //هنا نعطي التكلفة للوحدة من الكمية المعطاة من المستخدم
                //مثلا اذا يشتي يبيع اكثر من المتاح مثلا اذا طلب 20 وما معانا الا 10
                //فعيبقى في ال qtyx 10
                //وعنقصها من المطلوبة فعيقسم على على 10 وهذا المطلوب
                lifo = sumPrice / (qtyx - qty);
            }
            else
            {
                lifo = subQuery.First().Cost_Value;
            }
            wac = subQuery.Select(x => x.Import_Qty * x.Cost_Value).Sum() / balance;
            Cost_Calc_Method method = (Cost_Calc_Method)Session.products.Single(x => x.ID == pro_ID).Cost_Calc_Method;
            switch (method)
            {
                case Cost_Calc_Method.Fifo:
                    return fifo;
                case Cost_Calc_Method.Lifo:
                    return lifo;
                case Cost_Calc_Method.WAC:
                    return wac;
                default:
                    return -12;
            }
        }
        public static double Get_Product_Balance_In_Store(int pro_ID, int store_ID)
        {
            //هذا الاستدعاء يعيد لنا جميع حركة الصنف ضمن المخزن المحدد
            var qurey = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Pro_Store_Movement>(
                @"Select * From Pro_Store_Movement 
                           Where Store_ID = @store_ID And Product_ID = @pro_ID",
                new
                {
                    store_ID = store_ID,
                    pro_ID = pro_ID
                });
            //هذا فيه مجموع كمية الصنف الصادرة
            double sumOut = qurey.Sum(x => (double?)x.Export_Qty) ?? 0;
            //هذا فيه مجموع كمية الصنف الواردة
            double sumIN = qurey.Sum(x => (double?)x.Import_Qty) ?? 0;
            //هذا فيه الكمية المتاحة حاليا
            double balance = sumIN - sumOut;
            //todo if balance less than or equal zero
            if (balance <= 0)
                balance = 0;
            return balance;
        }
    }
}
