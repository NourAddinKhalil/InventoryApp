using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheFinalSalesProject.Classes;
using Dapper;
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject.MyForms
{
    public partial class Frm_Add_Product : Frm_Master
    {
        private DBModels.Product product;
        private RepositoryItemLookUpEdit lookUpEdit = new RepositoryItemLookUpEdit();
        List<int> store_IDs = new List<int>();
        private readonly DBModels.Product_Unit products_Uint;
        private readonly DBModels.Category category;
        public Frm_Add_Product()
        {
            InitializeComponent();
            Prepare_Event();
            New();
            Refresh_Data();
        }
        public Frm_Add_Product(int pro_ID)
        {
            InitializeComponent();
            Prepare_Event();
            Load_Product(pro_ID);
            Refresh_Data();
        }
        private void Prepare_Event()
        {
            //ProductUnitGrdViw.ValidateRow += new ValidateRowEventHandler(ProductUnitGrdViw_ValidateRow);
            //ProductUnitGrdViw.InvalidRowException += new InvalidRowExceptionEventHandler(ProductUnitGrdViw_InvalidRowException);
            //ProductUnitGrdViw.FocusedRowChanged += new FocusedRowChangedEventHandler(ProductUnitGrdViw_FocusedRowChanged);
            //ProductUnitGrdViw.CustomRowCellEditForEditing += new CustomRowCellEditEventHandler(ProductUnitGrdViw_CustomRowCellEditForEditing);
            //lookUpEdit.ProcessNewValue += new ProcessNewValueEventHandler(LookUpEdit_ProcessNewValue);
            ProCategoryLkUpEdt.ProcessNewValue += new ProcessNewValueEventHandler(ProCategoryLkUpEdt_ProcessNewValue);
        }
        private void Load_Product(int pro_ID)
        {
            product = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Product>("Select * From Product Where ID = @pr_id", new { pr_id = pro_ID }).SingleOrDefault();
            this.Text = $"{0}بيانات الصنف " + product.Name;
            Get_Data();
        }
        public void Frm_Add_Products_Load(object sender, EventArgs e)
        {
            SetUp_lookUpEdit();
            SetUp_ProCategoryLkUpEdt_Prop();
            SetUp_TypeLokUpEdt_Prop();
            SetUp_ProductUintGrdCtrl_Prop();
            Cost_Calculate_Method_LkUpEdt.Properties.PopulateColumns();
            Cost_Calculate_Method_LkUpEdt.Properties.Columns["ID"].Visible = false;
            Cost_Calculate_Method_LkUpEdt.Properties.Columns["Name"].Caption = "إسم الطريقة";
            UnitNameLkpEdt.Properties.PopulateColumns();
            UnitNameLkpEdt.Look_Up_Edit_Translate_Column("Unit");
            FactorSpnEdt.EditValue = "1";
            ProductUnitGrdViw.OptionsBehavior.Editable = false;
            BarCodeTxtEdt.Text = Get_New_Product_BarCode();
            OpeningGridView.Columns["ID"].Visible =
            OpeningGridView.Columns["Source_Type"].Visible =
            OpeningGridView.Columns["Source_ID"].Visible =
            OpeningGridView.Columns["Notes"].Visible =
            OpeningGridView.Columns["User_ID"].Visible =
            OpeningGridView.Columns["Export_Qty"].Visible =
            OpeningGridView.Columns["Insert_Date"].Visible =
            OpeningGridView.Columns["Product_ID"].Visible = false;
            OpeningGridView.Columns["Import_Qty"].Caption = "الكمية";
            OpeningGridView.Columns["Cost_Value"].Caption = "القيمة";
            OpeningGridView.Columns["Store_ID"].Caption = "المخزن";
        }
        private void SetUp_ProCategoryLkUpEdt_Prop()
        {
            ProCategoryLkUpEdt.Properties.PopulateColumns();
            ProCategoryLkUpEdt.Properties.Columns[nameof(category.Parent_ID)].Visible = false;
            ProCategoryLkUpEdt.Properties.Columns[nameof(category.Name)].Caption = "إسم الفئة";
            //حددنا الفاليو ممبر والديسبلاي ممبر
            ProCategoryLkUpEdt.Properties.TextEditStyle = TextEditStyles.Standard;
            //allow the writing in ProCategoryLkUpEdt ماشي ذي في الداتا قريد
        }
        private void SetUp_TypeLokUpEdt_Prop()
        {
            ProTypeLokUpEdt.Initilaze_Data(Master_Class.product_Type);
            //we filled ptoduct type lookUpEdit with this tow values 
            ProTypeLokUpEdt.Properties.PopulateColumns();
            ProTypeLokUpEdt.Properties.Columns["ID"].Visible = false;
            ProTypeLokUpEdt.Properties.Columns["Name"].Caption = "النوع";
            //حددنا ايش القيمة المعروضة ةالقيمة الاصلية لل نوع المنتج
        }
        private void SetUp_ProductUintGrdCtrl_Prop()
        {
            ProductUnitGrdViw.OptionsView.ShowGroupPanel = false;
            //remove the search panel in the top of ControlGrid
            //ProductUnitGrdViw.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
            //حددنا حقل الكتابة مايكون يجي الا في اول خانة
            ProductUnitGrdViw.Columns[nameof(products_Uint.ID)].Visible = false;
            ProductUnitGrdViw.Columns[nameof(products_Uint.Pro_ID)].Visible = false;
            ProductUnitGrdViw.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            //RepositoryItemCalcEdit calcEdit = new RepositoryItemCalcEdit();
            //we add calculator in folowing fields
           // ProductUintGrdCtrl.RepositoryItems.Add(calcEdit);
            //ProductUintGrdCtrl.RepositoryItems.Add(lookUpEdit);
            //first we add them into controlGrid
            //ProductUnitGrdViw.Columns[nameof(products_Uint.Sell_Price)].ColumnEdit = calcEdit;
            //ProductUnitGrdViw.Columns[nameof(products_Uint.Buy_Price)].ColumnEdit = calcEdit;
            //ProductUnitGrdViw.Columns[nameof(products_Uint.Sell_Discount)].ColumnEdit = calcEdit;
            //ProductUnitGrdViw.Columns[nameof(products_Uint.Unit_ID)].ColumnEdit = lookUpEdit;
            //ProductUnitGrdViw.Columns[nameof(products_Uint.Factor)].ColumnEdit = calcEdit;
            ProductUnitGrdViw.Columns[nameof(products_Uint.BarCode)].Caption = "باركود";
            ProductUnitGrdViw.Columns[nameof(products_Uint.Buy_Price)].Caption = "سعر الشراء";
            ProductUnitGrdViw.Columns[nameof(products_Uint.Sell_Price)].Caption = "سعر البيع";
            ProductUnitGrdViw.Columns[nameof(products_Uint.Sell_Discount)].Caption = "الخصم";
            ProductUnitGrdViw.Columns[nameof(products_Uint.Unit_ID)].Caption = "الوحدة";
            ProductUnitGrdViw.Columns[nameof(products_Uint.Factor)].Caption = "معامل التحويل";
            //now we Add them Into the wanted Fields
        }
        private void SetUp_lookUpEdit()
        {
            lookUpEdit.PopulateColumns();
            lookUpEdit.Columns["ID"].Visible = false;
            lookUpEdit.Columns["Name"].Caption = "إسم الوحدة";
            lookUpEdit.TextEditStyle = TextEditStyles.Standard;
        }
        protected override void Refresh_Data()
        {
            var proCate = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Category>
                ("SELECT TOP (1) * FROM[Category] WHERE((SELECT COUNT(*) FROM[Category] WHERE [Parent_ID] = [ID] )) = 0");
            if (proCate != null)
            {
                product.Cate_ID = proCate.FirstOrDefault().ID;
                ProCategoryLkUpEdt.Initilaze_Data(proCate);
                /*
                 هنا طرحنا قيمة ابتدائية للفئات اذا كان موجود فئات
                */
            }
            /*
              هذا الاستعلام يقوم بإرجاع جميع الأنواع التي ليس لديها أبناء
              بمعنى إذا كان عدد السجلات أكبر من صفر لن يرجع شئ لأن احنا مانشتي إلا 
              آخر عقدة فقط
            */
            //var unit = Session.Units;
            if (Session.Units == null || Session.Units.Count <= 0)
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce("Insert Into Unit Values (@name)", new { name = "قطعة" });
                //Refresh_Data();
                /*
               هنا طرحنا قيمة ابتدائية للوحدة اذا كان مابش  موجود وحدة
                */
            }
            lookUpEdit.Initilaze_Data(Session.Units, ProductUintGrdCtrl,ProductUnitGrdViw.Columns[nameof(products_Uint.Unit_ID)]);
            UnitNameLkpEdt.Initilaze_Data(Session.Units);
            //this is the item we added in datagridView
            Cost_Calculate_Method_LkUpEdt.Initilaze_Data(Classes.Master_Class.Cost_Calc_Method_List);
            base.Refresh_Data();
        }
        private string Get_New_Product_Code()
        {
            string maxCode;
            var s = DAL.Impelement_Stored_Procedure.SelectData<DBModels.ID_Model>(
                @"Select ISNULL(MAX(Code),'Prd0') As StrID From Product").FirstOrDefault().StrID;
            maxCode = string.IsNullOrEmpty(s) ? "" : s;
            return Master_Class.Get_Next_Number_InTheString(maxCode);
        }
        private string Get_New_Product_BarCode()
        {
            string maxCode;
            maxCode = DAL.Impelement_Stored_Procedure.SelectData<DBModels.ID_Model>(
                @"Select ISNULL(MAX(BarCode),'Cate0') As StrID From Product_Unit").FirstOrDefault().StrID;
            return Master_Class.Get_Next_Number_InTheString((string.IsNullOrEmpty(maxCode) ? "" : maxCode));
        }
        protected override void New()
        {
            product = new DBModels.Product()
            {
                Code = Get_New_Product_Code(),
                Is_Active = true,
                Order_Limit = 1
            };

            //DBModels.Category cate = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Category>
            //    ("SELECT TOP (1) * FROM [Category] WHERE((SELECT COUNT(*) FROM[Category] WHERE [Parent_ID] = [ID] )) = 0").FirstOrDefault();
            ///*
            //  هذا الاستعلام يقوم بإرجاع جميع الأنواع التي ليس لديها أبناء
            //  بمعنى إذا كان عدد السجلات أكبر من صفر لن يرجع شئ لأن احنا مانشتي إلا 
            //  آخر عقدة فقط
            //*/
            //if (cate != null)
            //{
            //    product.Cate_ID = cate.ID;
            //    /*
            //     هنا طرحنا قيمة ابتدائية للفئات اذا كان موجود فئات
            //    */
            //}
            base.New();//don't remove it from here
            //var prodataGrid = ProductUnitGrdViw.DataSource as BindingList<DBModels.Product_Unit>;
           
            //int id = 0;
            //var obj = DAL.Impelement_Stored_Procedure.SelectData<DBModels.ID_Model>("Select ID From Unit").FirstOrDefault();
            //if (obj != null)
            //    id = obj.ID;
            //if (prodataGrid is null)
            //    prodataGrid = new BindingList<DBModels.Product_Unit>();
            //prodataGrid.Add(new DBModels.Product_Unit
            //{
            //    Factor = 1,
            //    Unit_ID = id,
            //    BarCode = Get_New_Product_BarCode()
            //});
            /*
             هنا عملنا اول ما يفتح النافذة يدي له اول وحده  واذا كان مابش وحدات
             فعنضيف وحدة افتراضية عشان ما يطلعش خطا
             */
        }
        protected override void Get_Data()
        {
            ProCodeTxt.Text = product.Code;
            ProNameTxt.Text = product.Name;
            ProCategoryLkUpEdt.EditValue = product.Cate_ID;
            ProTypeLokUpEdt.EditValue = (byte)product.Type;
            Cost_Calculate_Method_LkUpEdt.EditValue = product.Cost_Calc_Method;
            IsActiveChkEdt.Checked = product.Is_Active;
            ProDiscribtionMemoEdt.Text = product.Discribtion;
            if (product.Image != null)
                ProImagePictrEdt.Image = Classes.Master_Class.Convert_Byte_To_Image(product.Image.ToArray());
            else
                ProImagePictrEdt.Image = null;
            OrderLimitSpn.EditValue = product.Order_Limit;
            ProductUintGrdCtrl.DataSource = Session.Product_Units.Where(x => x.Pro_ID == product.ID).ToList();
            /*
             هذا الاستعلام من اجل نعبي الداتا حق وحدات القياس
             وعملنا داتا كونتكست لحلها عنحتاجها نااازل
             */
            OpeningGridCtrl.DataSource = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Pro_Store_Movement>(
                @"Select * From Pro_Store_Movement Where Product_ID = @proID 
                               And  Source_Type = @srcType",
                 new
                 {
                     proID = product.ID,
                     srcType = Convert.ToByte(Enum_Choices.Source_Type.OpeningAccount)
                 });
            store_IDs = (OpeningGridView.DataSource as List<DBModels.Pro_Store_Movement>).Select(x => x.Store_ID).ToList();
            UnitNameLkpEdt.EditValue = 0;
            FactorSpnEdt.EditValue = "1";
            BuyPriceSpnEdt.EditValue =
            SellPriceSpnEdt.EditValue =
            SellDiscSpnEdt.EditValue = "0";
            BarCodeTxtEdt.Text = Get_New_Product_BarCode();
            the_Changed_Source_ID = product.ID;
            the_Changed_Source_Name = product.Name;
            base.Get_Data();
        }
        protected override void Set_Data()
        {
            product.Code = ProCodeTxt.Text;
            product.Name = ProNameTxt.Text;
            product.Cate_ID = (int)ProCategoryLkUpEdt.EditValue;
            product.Type = (byte)ProTypeLokUpEdt.EditValue;//bit
            product.Cost_Calc_Method = (byte)Cost_Calculate_Method_LkUpEdt.EditValue;
            product.Is_Active = IsActiveChkEdt.Checked;
            product.Discribtion = ProDiscribtionMemoEdt.Text;
            product.Image = Classes.Master_Class.Convert_Image_To_Byte(ProImagePictrEdt.Image);
            product.Order_Limit = Convert.ToDouble(OrderLimitSpn.EditValue);
            product.Has_Opening_Balance = (OpeningGridView.RowCount > 0 ? true : false);
            base.Set_Data();
        }
        protected override void Open_New_Window()
        {
            Frm_Main_Window.OpenForm(new Frm_Add_Product(), false);
            base.Open_New_Window();
        }
        protected override bool IsDataValid()
        {
            int numError = 0;
            if (ProductUnitGrdViw.RowCount == 0)
            {
                numError++;
                XtraMessageBox.Show("الرجاء إضافة الوحدات أولاً ", "فشل حفظ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            numError += ProCategoryLkUpEdt.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += ProTypeLokUpEdt.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += Cost_Calculate_Method_LkUpEdt.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += ProNameTxt.Is_The_Text_Valid() ? 0 : 1;
            numError += ProCodeTxt.Is_The_Text_Valid() ? 0 : 1;
            numError += OrderLimitSpn.Is_The_Edit_Value_More_Than_Zero() ? 0 : 1;
            var check = Session.products.Where(x => x.ID != product.ID);
            if(check.Where(x=>x.Code == ProCodeTxt.Text.Trim()).Count() > 0)
            {
                numError++;
                ProCodeTxt.ErrorText = Messages.Code_Exist;
            }
            if (check.Where(x => x.Name == ProNameTxt.Text.Trim()).Count() > 0)
            {
                numError++;
                ProNameTxt.ErrorText = Messages.Name_Exist;
            }
            //for (int i = 0; i < ProductUnitGrdViw.RowCount; i++)
            //{
            //    ProductUnitGrdViw.FocusedRowHandle = i;
            //    //ProductUnitGrdViw_ValidateRow(ProductUnitGrdViw, new ValidateRowEventArgs(i, ProductUnitGrdViw.GetRow(i)));
            //    if (ProductUnitGrdViw.HasColumnErrors)
            //    {
            //        numError++;
            //        XtraMessageBox.Show("الرجاء التأكد من صحة البيانات ", "فشل حفظ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        //ProductUnitGrdViw_ValidateRow(ProductUnitGrdViw, new ValidateRowEventArgs(i, ProductUnitGrdViw.GetRow(i)));
            //        return false;
            //    }
            //}
            return (numError <= 0);
        }
        protected override void Save()
        {
            ProductUnitGrdViw.UpdateCurrentRow();
            OpeningGridView.UpdateCurrentRow();
            bool openingBalance = OpeningGridView.RowCount > 0;
            if (product.ID == 0)
            {
                product.ID = DAL.Impelement_Stored_Procedure.Excute_Proce_And_Retrieve_ID<DBModels.ID_Model>(
                    @"Insert Into Product Values 
                (@code,@name,@type,@isActive,@order_Lim,@image,@cateid,@cost,@disc,@openBal)",
                new
                {
                    code = product.Code,
                    name = product.Name,
                    type = product.Type,
                    isActive = product.Is_Active,
                    order_Lim = product.Order_Limit,
                    image = product.Image,
                    cateid = product.Cate_ID,
                    cost = product.Cost_Calc_Method,
                    disc = product.Discribtion,
                    openBal = openingBalance,
                }).SingleOrDefault().ID;
            }
            else
            {
                DAL.Impelement_Stored_Procedure.Excute_Proce(@"Update Product Set 
                Code = @code, Name = @name,Type = @type, Is_Active = @isActive, 
                Order_Limit = @order_Lim ,Image = @image, Cate_ID = @cateid, 
                Cost_Calc_Method = @cost, Discribtion = @disc ,Has_Opening_Balance = @openBal Where ID = @id",
               new
               {
                   code = product.Code,
                   name = product.Name,
                   type = product.Type,
                   isActive = product.Is_Active,
                   order_Lim = product.Order_Limit,
                   image = product.Image,
                   cateid = product.Cate_ID,
                   cost = product.Cost_Calc_Method,
                   disc = product.Discribtion,
                   openBal = openingBalance,
                   id = product.ID
               });
            }
            DAL.Impelement_Stored_Procedure.Excute_Proce(
                @"Delete From Product_Unit Where Pro_ID = @id", new { id = product.ID });
            var productUnit = new
            {
                productUnits = Table().AsTableValuedParameter("Add_Product_Unit_Type")
            };
            DAL.Impelement_Stored_Procedure.Excute_Proce("Add_Product_Units", productUnit, isCommandText: false);
            if (store_IDs.Count > 0) 
            {
                foreach (var store_ID in store_IDs)
                {
                    Delete_Data.Delete_Pro_Store_Move_Details_By_ProID(product.ID, Source_Type.OpeningAccount, store_ID);
                }
            }
            if (openingBalance)
            {
                var proMove = new
                {
                    proMove = Convert_ProMove_List_To_Table().
                       AsTableValuedParameter("Pro_Movement_List_Type")
                };
                DAL.Impelement_Stored_Procedure.Excute_Proce(
                    "FinalSalesDB.dbo.Add_List_Of_Pro_Movement", proMove, isCommandText: false);
            }
            /*
             هنا عملنا متغير عشان نعبي رقم المنتج في جدول وحدات المنتجات 
             وحولنا مصدر الداتا قريد حق وحدات النتجات الى BindingList<DAL.Products_Uint>
             لان الداتا قريد تعيد بيانات من القوائم المترابطة ذي نوع بياناتها prpductUint
             */
            store_IDs = (OpeningGridView.DataSource as List<DBModels.Pro_Store_Movement>).Select(x => x.Store_ID).ToList();
            the_Changed_Source_ID = product.ID;
            the_Changed_Source_Name = product.Name;
            base.Save();
        }
        protected override void Delete()
        {
            DAL.Impelement_Stored_Procedure.Excute_Proce("Delete From Product Where ID = @id", new { id = product.ID });
            base.Delete();
        }
        private DataTable Table()
        {
            using (DataTable table =new DataTable())
            {
                var data = ProductUnitGrdViw.DataSource as List<DBModels.Product_Unit>;
                table.Columns.Add("ID", typeof(int));
                table.Columns.Add("Pro_ID", typeof(int));
                table.Columns.Add("Unit_ID", typeof(int));
                table.Columns.Add("Factor", typeof(double));
                table.Columns.Add("Buy_Price", typeof(double));
                table.Columns.Add("Sell_Price", typeof(double));
                table.Columns.Add("Sell_Discount", typeof(double));
                table.Columns.Add("BarCode", typeof(string));
                Parallel.ForEach<DBModels.Product_Unit>(data, (item) =>
                {
                    table.Rows.Add(item.ID, product.ID, item.Unit_ID, item.Factor, item.Buy_Price, item.Sell_Price, item.Sell_Discount, item.BarCode);
                });
                //foreach (var item in data)
                //{
                //    table.Rows.Add(item.ID, pro_ID, item.Unit_ID, item.Factor, item.Buy_Price, item.Sell_Price, item.Sell_Discount, item.BarCode);
                //};
                return table;
            }
        }
        private DataTable Convert_ProMove_List_To_Table()
        {
            using (DataTable dataTable = new DataTable())
            {
                List<DBModels.Pro_Store_Movement> data = OpeningGridView.DataSource as List<DBModels.Pro_Store_Movement>;
                dataTable.Columns.Add("Source_Type", typeof(byte));
                dataTable.Columns.Add("Source_ID", typeof(int));
                dataTable.Columns.Add("Product_ID", typeof(int));
                dataTable.Columns.Add("Store_ID", typeof(int));
                dataTable.Columns.Add("Export_Qty", typeof(double));
                dataTable.Columns.Add("Import_Qty", typeof(double));
                dataTable.Columns.Add("Cost_Value", typeof(double));
                dataTable.Columns.Add("Insert_Date", typeof(DateTime));
                dataTable.Columns.Add("Notes", typeof(string));
                dataTable.Columns.Add("User_ID", typeof(int));
                foreach (var item in data)
                {//todo comehere
                    dataTable.Rows.Add(item.Source_Type, item.Source_ID
                        , item.Product_ID, item.Store_ID, item.Export_Qty, item.Import_Qty, item.Cost_Value
                        , item.Insert_Date, item.Notes, item.User_ID);
                };
                return dataTable;
            }
        }
        #region Event Not Used
        void ProductUnitGrdViw_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == nameof(products_Uint.Unit_ID))
            {
                //we collect all units ids in ProductUnitGrdViw because its return (Collection<DAL.Products_Uint>) or (BindingList<DBModels.Product_Unit>) 
                var unit_Ids = ((List<DBModels.Product_Unit>)ProductUnitGrdViw.DataSource).Select(x => x.Unit_ID).ToList();
                // we creat this so we can control the elements in lookUpEdit tool
                RepositoryItemLookUpEdit EnnerlookUpEdit = new RepositoryItemLookUpEdit();
                var currentId = (Int32?)e.CellValue;
                unit_Ids.Remove(currentId ?? 0);//if null then it'll not remove any column 
                var unit = DAL.Impelement_Stored_Procedure.SelectData<DBModels.Unit>("Select * From Unit").Where(x => unit_Ids.Contains(x.ID) == false).ToList();
                EnnerlookUpEdit.DataSource = unit;
                EnnerlookUpEdit.ValueMember = "ID";
                EnnerlookUpEdit.DisplayMember = "Name";
                EnnerlookUpEdit.PopulateColumns();
                EnnerlookUpEdit.Columns["ID"].Visible = false;
                EnnerlookUpEdit.Columns["Name"].Caption = "إسم الوحدة";
                EnnerlookUpEdit.TextEditStyle = TextEditStyles.Standard;
                EnnerlookUpEdit.NullText = "";
                EnnerlookUpEdit.BestFitMode = BestFitMode.BestFitResizePopup;
                //EnnerlookUpEdit.ProcessNewValue += LookUpEdit_ProcessNewValue;
                e.RepositoryItem = EnnerlookUpEdit;
                /*
                 عملنا ذيه الحدث واللوك الداخلية عشان عندما المستخدم يجي يختار وحدة 
                 معد تجي لهش بعدا الا اذا حذف السطر ومعا تيه مشاكل اي اذا جينا نضيف وحدة 
                 مش موجودة يطلع خطا بس يضيفها
                 ملاخظة لازم نعمل قبل PopulateColumns إذا نشتي نعدل على اسامي ال لوك اب
                 بما اننا ربطنا الوك اب الداخلية بالخارجية هذي الداخلية عتعمل خطا
                 اذا اضفنا قيمة مش موجودة في الوك اب 
                 فلهذا عنعالج الموضوع في الحدث حق معالجة قيمة جديدة لل لوك اب
                */
            }
        }
        void ProductUnitGrdViw_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            ProductUnitGrdViw.Columns[nameof(products_Uint.Factor)].OptionsColumn.AllowEdit = (e.FocusedRowHandle != 0);
            /*
             هذا السطر معناه ما شاخلي المستخدم يعدل الا اذا السطر لا يساوي صفر
             */
        }
        void ProductUnitGrdViw_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            if (e.Row == null || (e.Row as DBModels.Product_Unit).Pro_ID == 0)
            {
                ProductUnitGrdViw.DeleteRow(e.RowHandle);
                e.ExceptionMode = ExceptionMode.Ignore;
                return;
            }
            e.ExceptionMode = ExceptionMode.NoAction;
            //هذا السطر عشان يبعد الرسالة ذي سع ال messagBox ويخلي الرسالة ذي في الحقل فقط
        }
        void ProductUnitGrdViw_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            var row = e.Row as DBModels.Product_Unit;
            var view = sender as GridView;//be the one who send the event is the datagrid it self
            if (row == null)
                return;
            if (row.Factor <= 1 && e.RowHandle != 0)
            {
                e.Valid = false;
                view.SetColumnError(view.Columns[nameof(row.Factor)], "The Field Must Be Bigger Than '1' ");
            }
            if (row.Unit_ID <= 0)
            {
                e.Valid = false;
                view.SetColumnError(view.Columns[nameof(row.Unit_ID)], Messages.Necessary_Field);
            }

            if (Check_If_BaraCode_Exist(row.BarCode, product.ID, row.Unit_ID))
            {
                e.Valid = false;
                view.SetColumnError(view.Columns[nameof(row.BarCode)], Messages.BaraCode_Exist);
            }
            /*
             هذا الحدث عشان نتأكد من صحة القيم المدخلة في الداتا قريد 
             بحيث أول شرط لايمكن إدخال قيمة للحقل فاكتور اقل او تساوي واحد
             والصف لايساوي الصف صفر بحيث الصف صفر يكون اصغر وحدة (مش متأكد سوا
             والشرط الثاني لا يسمح بادخال رقم وحدة يساوي صفر لانو مابش
             */
        }
        /// <summary>
        ///هذا الحدث اذا كتب المستخدم قيمة مش موجودة في القايمة يضيفها في القاعدة والقايمة
        ///في الوحدات
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LookUpEdit_ProcessNewValue(object sender, ProcessNewValueEventArgs e)
        {
            if (e.DisplayValue is string u && u.Trim() != string.Empty)
            {
                DBModels.Unit newUnit = new DBModels.Unit() { Name = u.Trim() };
                DAL.Impelement_Stored_Procedure.Excute_Proce("Insert Into Unit Values (@name)", new { name =newUnit.Name });

                ((List<DBModels.Unit>)lookUpEdit.DataSource).Add(newUnit);
                //we deal with ProCategoryLkUpEdt as a list because lately we converted it 
                //datasource into list
                ((List<DBModels.Unit>)(((LookUpEdit)sender).Properties.DataSource)).Add(newUnit);
                /*
                هنا عملنا كاست للسندر لانو من نوع لوك اب وعملنا ذيه الكاست والاضافة لانو 
                ذلحين المتغير ذيه lookUpEdit
                ماشو ذي رسل الحدث ذيه هو الوك الثاني ذي عملناه في الحدث ProductUnitGrdViw_CustomRowCellEditForEditing
                فلهذا مابيضتافش في السندر حق المتغير الاول فنضطر الى اضافته للمتغير ذي 
                رسل الحدث وهو EnnerlookUpEdit
                وبما ان التغير ماشو عام لذا نعمل كاست لنوعه وعو من نوع LookUpEdit
                */
                //Debug.Print(sender.ToString());work like consol.write
                e.Handled = true;
            }

            /*
              هذا الحدث إذا أدخل المستخدم قيمة ماشي موجودة في 
              lookUpEdit فتقوم الدالة بإدخاله تلقائي الخاصة بالداتا قريد
           */
        }
        /// <summary>
        ///هذا الحدث اذا كتب المستخدم قيمة مش موجودة في القايمة يضيفها في القاعدة والقايمة
        ///في انواع المنتجات
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        #endregion
        private void ProCategoryLkUpEdt_ProcessNewValue(object sender, ProcessNewValueEventArgs e)
        {
            if (e.DisplayValue is string s && s.Trim() != string.Empty)
            {
                var newCate = new DBModels.Category()
                {
                    Name = s,
                    Parent_ID = 0
                };
                DAL.Impelement_Stored_Procedure.Excute_Proce(
                    @"Insert Into [FinalSalesDB].[dbo].[Category] Values (@name , @parent)",
                    new
                    {
                        name = newCate.Name,
                        parent = newCate.Parent_ID,
                    });
                ((List<DBModels.Category>)ProCategoryLkUpEdt.Properties.DataSource).Add(newCate);
                //we deal with ProCategoryLkUpEdt as a list because lately we converted it 
                //datasource into list
                e.Handled = true;
            }
            /*
               هذا الحدث إذا أدخل المستخدم قيمة ماشي موجودة في 
               ProCategoryLkUpEdt فتقوم الدالة بإدخاله تلقائي
            */
        }
        private bool Check_If_BaraCode_Exist(string baraCode, int pro_ID, int unit_ID)
        {
            {
                if (string.IsNullOrEmpty(baraCode)) return true;
                int count = DAL.Impelement_Stored_Procedure.SelectData<DBModels.ID_Model>("Select Count(*) As ID From Product_Unit Where BarCode = @barCode And Pro_ID != @pr_id", new { barCode = baraCode, pr_id = pro_ID }).SingleOrDefault().ID;
                var row = ProductUnitGrdViw.DataSource as BindingList<DBModels.Product_Unit>;
                if (row != null)
                {//هذا الشرط زيادة قد يمكن الكود يتكرر في نفس الصفوف وما قد اضفناه في القاعدة نفسها
                    count += row.Where(x => x.BarCode.Equals(baraCode) && x.Unit_ID != unit_ID).Count();
                }
                return count > 0 ? true : false;
            }
        }
        private bool Unit_Validate()
        {
            var obj = (ProductUintGrdCtrl.DataSource as List<DBModels.Product_Unit>).ToList();
            int numError = 0;
            numError += UnitNameLkpEdt.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += FactorSpnEdt.Is_The_Edit_Value_More_Than_Zero() ? 0 : 1;
            numError += BuyPriceSpnEdt.Is_The_Edit_Value_Less_Than_Zero() ? 0 : 1;
            numError += SellPriceSpnEdt.Is_The_Edit_Value_Less_Than_Zero() ? 0 : 1;
            numError += SellDiscSpnEdt.Is_The_Edit_Value_Less_Than_Zero() ? 0 : 1;
            numError += BarCodeTxtEdt.Is_The_Text_Valid() ? 0 : 1;
            if (obj.Select(x=>x.Factor).Contains(Convert.ToDouble(FactorSpnEdt.EditValue)))
            {
                numError++;
                FactorSpnEdt.ErrorText = Messages.Factor_Exist;
            }
            if (obj.Select(x => x.Unit_ID).Contains(Convert.ToInt32(UnitNameLkpEdt.EditValue)))
            {
                numError++;
                UnitNameLkpEdt.ErrorText = Messages.Unit_Exist;
            }
            if (obj.Select(x => x.BarCode).Contains(BarCodeTxtEdt.Text))
            {
                numError++;
                BarCodeTxtEdt.ErrorText = Messages.BaraCode_Exist;
            }
            return (numError <= 0);
        }
        private void AddToListBtn_Click(object sender, EventArgs e)
        {
            if (!Unit_Validate()) return;
            var data = new DBModels.Product_Unit()
            {
                Unit_ID = Convert.ToInt32(UnitNameLkpEdt.EditValue),
                Factor = Convert.ToDouble(FactorSpnEdt.EditValue),
                Buy_Price = Convert.ToDouble(BuyPriceSpnEdt.EditValue),
                Sell_Price = Convert.ToDouble(SellPriceSpnEdt.EditValue),
                Sell_Discount = Convert.ToDouble(SellDiscSpnEdt.EditValue),
                BarCode = BarCodeTxtEdt.Text
            }; 
            ProductUnitGrdViw.AddNewRow();
            ((List<DBModels.Product_Unit>)(ProductUintGrdCtrl.DataSource)).Add(data);
            ProductUnitGrdViw.RefreshData();
            BarCodeTxtEdt.Text = Master_Class.Get_Next_Number_InTheString(BarCodeTxtEdt.Text);
            UnitNameLkpEdt.EditValue =
            FactorSpnEdt.EditValue =
            BuyPriceSpnEdt.EditValue =
            SellPriceSpnEdt.EditValue =
            SellDiscSpnEdt.EditValue = null;
        }
        private void ProductUnitGrdViw_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs eventArgs = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(eventArgs.Location);
            if (hitInfo.InRow || hitInfo.InRowCell)
            {
                //this is one way to do it
                if (UnitNameLkpEdt.Is_The_Lkp_Text_Valid(false))
                    if (XtraMessageBox.Show("يوجد بيانات في الحقول هل أنت متأكد أنك تريد الإستبدال ؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                var data = ProductUnitGrdViw.GetFocusedRow() as DBModels.Product_Unit;
                UnitNameLkpEdt.EditValue = data.Unit_ID;
                FactorSpnEdt.EditValue = data.Factor;
                BuyPriceSpnEdt.EditValue = data.Buy_Price;
                SellPriceSpnEdt.EditValue = data.Sell_Price;
                SellDiscSpnEdt.EditValue = data.Sell_Discount;
                BarCodeTxtEdt.Text = data.BarCode;
                ProductUnitGrdViw.DeleteRow(ProductUnitGrdViw.FocusedRowHandle);
                ProductUnitGrdViw.RefreshData();
            }
        }
        private void UnitNameLkpEdt_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Plus)
                Frm_Main_Window.OpenForm(new MyForms.Frm_Units(), true);
        }
        private void ProUnXtraTabCtrl_Selected(object sender, DevExpress.XtraTab.TabPageEventArgs e)
        {
            List<int> ids = (ProductUnitGrdViw.DataSource as List<DBModels.Product_Unit>)?.Select(x => x.Unit_ID).ToList();
            if (ids == null || ids.Count <= 0) return;
            OpeningUnitLkp.Initilaze_Data(Session.Units.Where(x => ids.Contains(x.ID)).ToList(), clear: true);
            OpeningUnitLkp.Properties.PopulateColumns();
            //OpeningUnitLkp.Look_Up_Edit_Translate_Column("Unit");
            //OpeningUnitLkp.Properties.Columns["ID"].Caption = "الرقم";
            //OpeningUnitLkp.Properties.Columns["Name"].Caption = "الإسم";
            OpeningStoreLkp.Initilaze_Data(Session.Stores, clear: true);
            //OpeningStoreLkp.Look_Up_Edit_Translate_Column("Store");
        }
        private bool Is_Opening_Fields_Valid()
        {
            int numError = 0;
            numError += OpeningUnitLkp.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += OpeningStoreLkp.Is_The_Lkp_Text_Valid() ? 0 : 1;
            numError += OpeningPriceSpn.Is_The_Edit_Value_More_Than_Zero() ? 0 : 1;
            numError += OpeningQtySpn.Is_The_Edit_Value_More_Than_Zero() ? 0 : 1;
            //numError += OpeningTotalSpn.Is_The_Edit_Value_More_Than_Zero() ? 0 : 1;
            return (numError == 0);
        }
        private void OpeningAddToListBtn_Click(object sender, EventArgs e)
        {
            if (!Is_Opening_Fields_Valid()) return;
            int unitID = Convert.ToInt32(OpeningUnitLkp.EditValue);
            var unitv = (ProductUnitGrdViw.DataSource as List<DBModels.Product_Unit>).FirstOrDefault(x => x.Unit_ID == unitID);
            if(unitv == null)
            {
                XtraMessageBox.Show("أدخل وحدات الأصناف أولاً", "فشل الإضافة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            (OpeningGridView.DataSource as List<DBModels.Pro_Store_Movement>).Add(
                new DBModels.Pro_Store_Movement()
                {
                    Product_ID = product.ID,
                    Insert_Date = DateTime.Now,
                    Source_ID = 0,
                    Source_Type = Convert.ToByte(Source_Type.OpeningAccount),
                    Notes = " رصيد إفتتاحي للصنف ",
                    Import_Qty = (Convert.ToDouble(OpeningQtySpn.EditValue) * unitv.Factor),
                    Export_Qty = 0,
                    Store_ID = Convert.ToInt32(OpeningStoreLkp.EditValue),
                    Cost_Value = Convert.ToDouble(OpeningPriceSpn.EditValue) / unitv.Factor,
                    User_ID = Session.user.ID,
                });
            OpeningGridView.RefreshData();
        }
    }
}
