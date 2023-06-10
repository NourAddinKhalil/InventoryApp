namespace TheFinalSalesProject.MyForms
{
    partial class Frm_Accounts_Tree
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ParentIDLkp = new DevExpress.XtraEditors.LookUpEdit();
            this.AccountCodeTxt = new DevExpress.XtraEditors.TextEdit();
            this.StocksGridCtrl = new DevExpress.XtraGrid.GridControl();
            this.StocksGridViw = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.AccountTreeList = new DevExpress.XtraTreeList.TreeList();
            this.AccountNameTxt = new DevExpress.XtraEditors.TextEdit();
            this.AccountIDTxt = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ParentIDLkp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountCodeTxt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StocksGridCtrl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StocksGridViw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountTreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountNameTxt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountIDTxt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ParentIDLkp);
            this.layoutControl1.Controls.Add(this.AccountCodeTxt);
            this.layoutControl1.Controls.Add(this.StocksGridCtrl);
            this.layoutControl1.Controls.Add(this.AccountTreeList);
            this.layoutControl1.Controls.Add(this.AccountNameTxt);
            this.layoutControl1.Controls.Add(this.AccountIDTxt);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 45);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(930, 441);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ParentIDLkp
            // 
            this.ParentIDLkp.Location = new System.Drawing.Point(494, 55);
            this.ParentIDLkp.Name = "ParentIDLkp";
            this.ParentIDLkp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ParentIDLkp.Properties.NullText = "";
            this.ParentIDLkp.Size = new System.Drawing.Size(347, 20);
            this.ParentIDLkp.StyleController = this.layoutControl1;
            this.ParentIDLkp.TabIndex = 8;
            this.ParentIDLkp.EditValueChanged += new System.EventHandler(this.ParentIDLkp_EditValueChanged);
            // 
            // AccountCodeTxt
            // 
            this.AccountCodeTxt.Location = new System.Drawing.Point(494, 123);
            this.AccountCodeTxt.Name = "AccountCodeTxt";
            this.AccountCodeTxt.Properties.ReadOnly = true;
            this.AccountCodeTxt.Size = new System.Drawing.Size(347, 20);
            this.AccountCodeTxt.StyleController = this.layoutControl1;
            this.AccountCodeTxt.TabIndex = 6;
            // 
            // StocksGridCtrl
            // 
            this.StocksGridCtrl.Location = new System.Drawing.Point(494, 236);
            this.StocksGridCtrl.MainView = this.StocksGridViw;
            this.StocksGridCtrl.Name = "StocksGridCtrl";
            this.StocksGridCtrl.Size = new System.Drawing.Size(412, 181);
            this.StocksGridCtrl.TabIndex = 5;
            this.StocksGridCtrl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.StocksGridViw});
            // 
            // StocksGridViw
            // 
            this.StocksGridViw.Appearance.FooterPanel.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Warning;
            this.StocksGridViw.Appearance.FooterPanel.Options.UseForeColor = true;
            this.StocksGridViw.GridControl = this.StocksGridCtrl;
            this.StocksGridViw.Name = "StocksGridViw";
            this.StocksGridViw.OptionsView.ColumnAutoWidth = false;
            this.StocksGridViw.OptionsView.ShowFooter = true;
            this.StocksGridViw.OptionsView.ShowGroupPanel = false;
            // 
            // AccountTreeList
            // 
            this.AccountTreeList.Location = new System.Drawing.Point(24, 45);
            this.AccountTreeList.Name = "AccountTreeList";
            this.AccountTreeList.OptionsBehavior.Editable = false;
            this.AccountTreeList.Size = new System.Drawing.Size(442, 372);
            this.AccountTreeList.TabIndex = 4;
            this.AccountTreeList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.AccountTreeList_FocusedNodeChanged);
            // 
            // AccountNameTxt
            // 
            this.AccountNameTxt.Location = new System.Drawing.Point(494, 157);
            this.AccountNameTxt.Name = "AccountNameTxt";
            this.AccountNameTxt.Size = new System.Drawing.Size(347, 20);
            this.AccountNameTxt.StyleController = this.layoutControl1;
            this.AccountNameTxt.TabIndex = 7;
            // 
            // AccountIDTxt
            // 
            this.AccountIDTxt.Location = new System.Drawing.Point(494, 89);
            this.AccountIDTxt.Name = "AccountIDTxt";
            this.AccountIDTxt.Properties.ReadOnly = true;
            this.AccountIDTxt.Size = new System.Drawing.Size(347, 20);
            this.AccountIDTxt.StyleController = this.layoutControl1;
            this.AccountIDTxt.TabIndex = 9;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlGroup2,
            this.layoutControlGroup3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(930, 441);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(470, 421);
            this.layoutControlGroup1.Text = "شجرة الحسابات";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.AccountTreeList;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(446, 376);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(470, 191);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(440, 230);
            this.layoutControlGroup2.Text = "أرصدة الحسابات";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.StocksGridCtrl;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(416, 185);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.emptySpaceItem4,
            this.layoutControlItem6,
            this.emptySpaceItem5});
            this.layoutControlGroup3.Location = new System.Drawing.Point(470, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(440, 191);
            this.layoutControlGroup3.Text = "بيانات الحساب";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.AccountCodeTxt;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(416, 24);
            this.layoutControlItem3.Text = "كود الحساب";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(62, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.AccountNameTxt;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 112);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(416, 24);
            this.layoutControlItem4.Text = "إسم الحساب";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(62, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ParentIDLkp;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 10);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(416, 24);
            this.layoutControlItem5.Text = "ينتمي إلى";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(62, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(416, 10);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 34);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(416, 10);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 102);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(416, 10);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 136);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(416, 10);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.AccountIDTxt;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 44);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(416, 24);
            this.layoutControlItem6.Text = "رقم الحساب";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(62, 13);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.Location = new System.Drawing.Point(0, 68);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(416, 10);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // Frm_Accounts_Tree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 508);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Frm_Accounts_Tree";
            this.Text = "Frm_Accounts_Tree";
            this.Load += new System.EventHandler(this.Frm_Accounts_Tree_Load);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ParentIDLkp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountCodeTxt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StocksGridCtrl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StocksGridViw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountTreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountNameTxt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountIDTxt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraTreeList.TreeList AccountTreeList;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.LookUpEdit ParentIDLkp;
        private DevExpress.XtraEditors.TextEdit AccountCodeTxt;
        private DevExpress.XtraGrid.GridControl StocksGridCtrl;
        private DevExpress.XtraGrid.Views.Grid.GridView StocksGridViw;
        private DevExpress.XtraEditors.TextEdit AccountNameTxt;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.TextEdit AccountIDTxt;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
    }
}