using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheFinalSalesProject.Classes;

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Units : Frm_Master
    {
        private bool myNew;
        DBModels.Unit unit;
        public Frm_Units()
        {
            InitializeComponent();
            New();
            Refresh_Data();
        }
        protected override void New()
        {
            unit = new DBModels.Unit();
            myNew = true;
            base.New();
        }
        protected override void Get_Data()
        {
            UnitNameTxt.Text = unit.Name;
            base.Get_Data();
        }
        protected override void Set_Data()
        {
            unit.Name = UnitNameTxt.Text.Trim();
            base.Set_Data();
        }
        protected override bool IsDataValid()
        {
            if (!UnitNameTxt.Is_The_Text_Valid())
                return false;
            return true;
        }
        protected override void Refresh_Data()
        {
            UnitsGrdCtrl.DataSource = Session.Units;
            base.Refresh_Data();
        }
        protected override void Save()
        {
            if (myNew)
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce("Insert Into Unit Values (@name)", new { name = unit.Name });
            }
            else
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce("Update Unit Set Name = @name Where ID = @id)", new { name = unit.Name, id = unit.ID });
            }
            base.Save();
        }
        protected override void Delete()
        {
            if (IsDataValid())
            {
                if (XtraMessageBox.Show(" هل أنت متأكد أنك تريد الحذف ؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                DAL.Impelement_Stored_Procedure.Excute_Proce("Delete From Unit Where ID = @id", new { id = unit.ID });
            }
            else
            {
                XtraMessageBox.Show("لا يوجد بيانات لحذفها", "فشل الحذف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            base.Delete();
        }
        private void UnitsGrdViw_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs eventArgs = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(eventArgs.Location);
            if (hitInfo.InRow || hitInfo.InRowCell)
            {
                //this is one way to do it
                if (UnitNameTxt.Is_The_Text_Valid(false))
                    if (XtraMessageBox.Show("يوجد بيانات في الحقل هل أنت متأكد أنك تريد الإستبدال ؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                var data = (DBModels.Unit)UnitsGrdViw.GetFocusedRow();
                UnitNameTxt.Text = data.Name;
                unit.ID = data.ID;
                myNew = false;
                DeleteBtn.Enabled = true;
            }
        }
        private void Frm_Units_Load(object sender, EventArgs e)
        {
            UnitsGrdViw.Columns["ID"].Visible = false;
            UnitsGrdViw.Columns["Name"].Caption = "الإسم";
        }
    }
}
