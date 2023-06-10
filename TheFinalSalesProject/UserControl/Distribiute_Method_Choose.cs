using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.UserControl
{
    class Distribiute_Method_Choose : XtraUserControl
    {
        RadioGroup radioGroup = new RadioGroup();
        public Cost_Distribute Selected_Buttun
        {
            get
            {
                if (radioGroup.EditValue != null)
                {
                    return (Cost_Distribute)radioGroup.EditValue;
                }
                else
                {
                    return Cost_Distribute.Price;
                }
            }
        }
        public Distribiute_Method_Choose()
        {
            LayoutControl lc = new LayoutControl();
            lc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            radioGroup.Properties.Items.AddRange(new RadioGroupItem[]
            {
                new RadioGroupItem(Cost_Distribute.Price,"حسب التكلفة"),
                new RadioGroupItem(Cost_Distribute.Quantity,"حسب الكمية")
            });
            lc.AddItem("طريقة توزيع المصاريف على الأصناف", radioGroup).TextLocation = DevExpress.Utils.Locations.Top;
            radioGroup.SelectedIndex = 0;
            this.RightToLeft = System.Windows.Forms.RightToLeft.Inherit;
            this.Controls.Add(lc);
        }
    }
}
