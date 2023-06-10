using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.Classes
{
    public static class Number_To_Text
    {
        public static string ConvertMoneyToArabicText(string value)
        {
            double result = 0.0;
            if (!double.TryParse(value, out result))
                return "";
            string accum = "";
            double rv1 = (double)(int)(result / 1000000.0);
            if (rv1 > 2.0)
                accum = NumToStr1(rv1, accum);
            if (rv1 >= 3.0 && rv1 < 10.0)
                accum += melion[3];
            else if (rv1 == 2.0)
                accum += melion[2];
            else if (rv1 == 1.0 || rv1 >= 10.0 && rv1 <= 999.0)
                accum += melion[1];
            double rv2 = (double)(int)((result - (double)((int)(result / 1000000.0) * 1000000)) / 1000.0);
            if (result != (double)((int)(result / 1000000.0) * 1000000) && result > 1000000.0)
                accum += " و";
            if (rv2 > 2.0)
                accum = NumToStr1(rv2, accum);
            if (rv2 >= 3.0 && rv2 < 10.0)
                accum += alf[3];
            else if (rv2 == 2.0)
                accum += alf[2];
            else if (rv2 == 1.0 || rv2 >= 10.0 && rv2 <= 999.0)
                accum += alf[1];
            double rv3 = (double)(int)(result - (double)((int)(result / 1000.0) * 1000) + 0.0001);
            if (result != (double)((int)(result / 1000.0) * 1000) && result > 1000.0 && rv3 != 0.0)
                accum += " و ";
            if (rv3 >= 2.0 && result != 2.0)
                accum = NumToStr1(rv3, accum);
            if (result > 0.999)
                accum = result >= 11.0 || rv3 <= 2.0 ? (result != 2.0 ? accum + "  ريال " : accum + "  ريال  ") : accum + "  ريال  ";
            double rv4 = (double)(int)((result - (double)(int)(result + 0.0001) + 0.0001) * 1000.0) / 10.0;
            if (rv4 >= 1.0 && result > 0.99)
                accum += " و";
            int num = 2;
            if (num == 2)
            {
                if (rv4 > 2.9)
                    accum = NumToStr1(rv4, accum);
                if (rv4 >= 1.0)
                    accum = rv4 < 2.0 || rv4 >= 2.99 ? (rv4 >= 11.0 || rv4 <= 2.9 ? accum + " فلس " : accum + " فلس ") : accum + " فلس ";
            }
            if (num == 3)
            {
                double rv5 = rv4 * 10.0;
                accum = NumToStr1(rv5, accum);
                if (rv5 >= 1.0)
                    accum = rv5 != 2.0 ? (rv5 >= 11.0 || rv5 <= 2.0 ? accum + " فلس " : accum + " فلس ") : accum + " فلس ";
            }
            return accum;
        }
        public static string NumToStr1(double rv, string accum)
        {
            if (rv >= 100.0)
            {
                int index = (int)(rv / 100.0);
                accum += meat[index];
            }
            int index1 = (int)(rv - (double)((int)(rv / 100.0) * 100));
            if (index1 != 0 && rv > 99.0)
                accum += " و";
            int index2 = index1 - index1 / 10 * 10;
            if (index1 < 13 && (uint)index1 > 0U)
                accum += ahad[index1];
            if (index1 > 12 && (uint)index2 > 0U)
                accum += ahad2[index2];
            if (index1 > 10 && index1 < 20)
                accum += ahad2[10];
            if (index1 > 19)
            {
                if ((uint)index2 > 0U)
                    accum += " و";
                accum += asharat[index1 / 10];
            }
            return accum;
        }
        private static string[] ahad = new string[13]
        {
      "",
      "واحد",
      "إثنين",
      "ثلاثة",
      "أربعة",
      "خمسة",
      "ستة",
      "سبعة",
      " ثمانية",
      " تسعة",
      " عشرة",
      " أحد",
      " اثنى"
        };
        private static string[] ahad2 = new string[13]
        {
      "",
      "واحد",
      "إثنين",
      "ثلاثة",
      "أربعة",
      "خمسة",
      "ستة",
      "سبعة",
      "ثمانية",
      "تسعة",
      " عشر",
      " أحد",
      " اثنى"
        };
        private static string[] asharat = new string[10]
        {
      "",
      "واحد",
      "عشرون",
      "ثلاثون",
      "أربعون",
      "خمسون",
      "ستون",
      "سبعون",
      "ثمانون",
      "تسعون"
        };
        private static string[] meat = new string[10]
        {
      "",
      "مائة",
      "مائتين",
      "ثلاثمائة",
      "أربعمائة",
      "خمسمائة",
      "ستمائة",
      "سبعمائة",
      "ثمانمائة",
      "تسعمائة"
        };
        private static string[] melion = new string[4]
        {
      "",
      " مليون",
      " مليونان",
      " ملايين"
        };
        private static string[] alf = new string[4]
        {
      "",
      " ألف",
      " ألفين",
      " آلاف"
        };
    }
}
