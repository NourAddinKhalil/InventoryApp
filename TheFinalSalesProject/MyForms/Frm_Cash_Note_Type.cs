using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.MyForms
{
    class Frm_Cash_Note_In : Frm_Cashes_Note_Guaranteed
    {
        public Frm_Cash_Note_In() :base (true)
        {

        }
    }
    class Frm_Cash_Note_Out : Frm_Cashes_Note_Guaranteed
    {
        public Frm_Cash_Note_Out() : base(false)
        {

        }
    }
    class Frm_Cash_Note_Out_List : Frm_Guaranteed_Notes_List
    {
        public Frm_Cash_Note_Out_List() : base(false)
        {

        }
    }
    class Frm_Cash_Note_In_List : Frm_Guaranteed_Notes_List
    {
        public Frm_Cash_Note_In_List() : base(true)
        {

        }
    }
}
