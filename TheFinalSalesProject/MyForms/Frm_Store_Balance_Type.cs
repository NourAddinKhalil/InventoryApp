using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.MyForms
{
    public class Frm_Store_Opening_Balance : Frm_Add_Opening_Pro_Balance
    {
        public Frm_Store_Opening_Balance() : base (Store_Balance_Type.Opening_Account)
        {
            this.Text = "رصيد إفتتاحي للمخزن";
        }
    }
    public class Frm_Store_Destructor_Balance : Frm_Add_Opening_Pro_Balance
    {
        public Frm_Store_Destructor_Balance() : base(Store_Balance_Type.Destructor)
        {
            this.Text = "إهلاك رصيد من المخزن";
        }
    }
}
