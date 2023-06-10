using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using TheFinalSalesProject.Classes;

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Master_List : Frm_Master
    {
        protected string idListName = "";
        public Frm_Master_List()
        {
            InitializeComponent();
        }
        public void Frm_Master_List_Load(object sender, EventArgs e)
        {
            SaveBtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            PrintBtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            RefreshBtn.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            ListGrdViw.OptionsBehavior.Editable = false;
            ListGrdViw.DoubleClick += ListGrdViw_DoubleClick;
            ListGrdViw.BestFitColumns();
        }
        private void ListGrdViw_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs eventArgs = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(eventArgs.Location);
            if (hitInfo.InRow || hitInfo.InRowCell)
            {
                //this is one way to do it
                //var frm = new Frm_Drawer(Convert.ToInt32(view.GetRowCellValue(hitInfo.VisibleIndex, "Drawer_ID")));
                int id = Convert.ToInt32(view.GetFocusedRowCellValue("ID"));
                if (id <= 0) return;
                Open_Form(id);
                Refresh_Data();
            }
            /*
             هنا حولنا التغير e 
             الى النوع اعلاه لانو يرجع قيمة من ذيك النوع ودرينا بهذا الخبر عندما عملنا 
             وقت التنفيذ كويك ووتش على المتغير فرجع لنا قيمته
             عملنا هذا عشان المستخدم اذا ضغط اي جزء من الداتا قريد غير الاسطر او الخلايا
             مايظهرش الفورم
             */
        }
        protected virtual void Open_Form(int id)
        {

        }
        protected override void Save()
        {

        }
        protected override void Delete()
        {

        }
        protected virtual void ListGrdViw_Click_Helper(int index)
        {

        }
        private void ListGrdViw_Click(object sender, EventArgs e)
        {
            DXMouseEventArgs eventArgs = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(eventArgs.Location);
            if (hitInfo.InRow || hitInfo.InRowCell)
            {
                //this is one way to do it
                //var frm = new Frm_Drawer(Convert.ToInt32(view.GetRowCellValue(hitInfo.VisibleIndex, "Drawer_ID")));
                int id = Convert.ToInt32(view.GetFocusedDataSourceRowIndex());
                if (id < 0) return;
                ListGrdViw_Click_Helper(id);
            }
        }
        protected override void New()
        {
            base.New();
            Enable_Delete_Print_Btns();
        }
    }
}
