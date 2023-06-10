namespace TheFinalSalesProject.MyForms
{
    partial class Frm_Manually_Rigister
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
            this.ManuallyGridCtrl = new DevExpress.XtraGrid.GridControl();
            this.ManuallyGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ManuallyIDTxt = new DevExpress.XtraEditors.TextEdit();
            this.ManuallyDate = new DevExpress.XtraEditors.DateEdit();
            this.ManuallyMemo = new DevExpress.XtraEditors.MemoEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ManuallyGridCtrl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ManuallyGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ManuallyIDTxt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ManuallyDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ManuallyDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ManuallyMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ManuallyGridCtrl);
            this.layoutControl1.Controls.Add(this.ManuallyIDTxt);
            this.layoutControl1.Controls.Add(this.ManuallyDate);
            this.layoutControl1.Controls.Add(this.ManuallyMemo);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 45);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(847, 441);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ManuallyGridCtrl
            // 
            this.ManuallyGridCtrl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ManuallyGridCtrl.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.ManuallyGridCtrl.EmbeddedNavigator.Appearance.BorderColor = System.Drawing.Color.Blue;
            this.ManuallyGridCtrl.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
            this.ManuallyGridCtrl.EmbeddedNavigator.Appearance.Options.UseBorderColor = true;
            this.ManuallyGridCtrl.EmbeddedNavigator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ManuallyGridCtrl.Location = new System.Drawing.Point(24, 235);
            this.ManuallyGridCtrl.MainView = this.ManuallyGridView;
            this.ManuallyGridCtrl.Name = "ManuallyGridCtrl";
            this.ManuallyGridCtrl.Size = new System.Drawing.Size(799, 182);
            this.ManuallyGridCtrl.TabIndex = 19;
            this.ManuallyGridCtrl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ManuallyGridView});
            // 
            // ManuallyGridView
            // 
            this.ManuallyGridView.Appearance.EvenRow.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ManuallyGridView.Appearance.EvenRow.Options.UseBackColor = true;
            this.ManuallyGridView.Appearance.FocusedCell.Options.UseTextOptions = true;
            this.ManuallyGridView.Appearance.FocusedCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.ManuallyGridView.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ManuallyGridView.Appearance.OddRow.Options.UseBackColor = true;
            this.ManuallyGridView.GridControl = this.ManuallyGridCtrl;
            this.ManuallyGridView.Name = "ManuallyGridView";
            this.ManuallyGridView.NewItemRowText = "إضغط هنا لإضافة قيد جديد";
            this.ManuallyGridView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.ManuallyGridView.OptionsPrint.EnableAppearanceOddRow = true;
            this.ManuallyGridView.OptionsView.EnableAppearanceEvenRow = true;
            this.ManuallyGridView.OptionsView.EnableAppearanceOddRow = true;
            this.ManuallyGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            // 
            // ManuallyIDTxt
            // 
            this.ManuallyIDTxt.Location = new System.Drawing.Point(393, 45);
            this.ManuallyIDTxt.Name = "ManuallyIDTxt";
            this.ManuallyIDTxt.Properties.Appearance.Options.UseTextOptions = true;
            this.ManuallyIDTxt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ManuallyIDTxt.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ManuallyIDTxt.Properties.ReadOnly = true;
            this.ManuallyIDTxt.Size = new System.Drawing.Size(381, 20);
            this.ManuallyIDTxt.StyleController = this.layoutControl1;
            this.ManuallyIDTxt.TabIndex = 4;
            // 
            // ManuallyDate
            // 
            this.ManuallyDate.EditValue = null;
            this.ManuallyDate.Location = new System.Drawing.Point(393, 69);
            this.ManuallyDate.Name = "ManuallyDate";
            this.ManuallyDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ManuallyDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ManuallyDate.Size = new System.Drawing.Size(381, 20);
            this.ManuallyDate.StyleController = this.layoutControl1;
            this.ManuallyDate.TabIndex = 5;
            // 
            // ManuallyMemo
            // 
            this.ManuallyMemo.Location = new System.Drawing.Point(393, 93);
            this.ManuallyMemo.Name = "ManuallyMemo";
            this.ManuallyMemo.Size = new System.Drawing.Size(381, 93);
            this.ManuallyMemo.StyleController = this.layoutControl1;
            this.ManuallyMemo.TabIndex = 6;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.emptySpaceItem1,
            this.layoutControlGroup2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(847, 441);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 190);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(827, 231);
            this.layoutControlGroup1.Text = "قائمة القيود";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ManuallyGridCtrl;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(803, 186);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(369, 190);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup2.Location = new System.Drawing.Point(369, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(458, 190);
            this.layoutControlGroup2.Text = "معلومات القيد";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ManuallyIDTxt;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(434, 24);
            this.layoutControlItem1.Text = "رقم القيد";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(46, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ManuallyDate;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(434, 24);
            this.layoutControlItem2.Text = "تاريخ القيد";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(46, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ManuallyMemo;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(434, 97);
            this.layoutControlItem3.Text = "ملاحظات";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(46, 13);
            // 
            // Frm_Manually_Rigister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 508);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Frm_Manually_Rigister";
            this.Text = "Frm_Manually_Rigister";
            this.Load += new System.EventHandler(this.Frm_Manually_Rigister_Load);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ManuallyGridCtrl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ManuallyGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ManuallyIDTxt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ManuallyDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ManuallyDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ManuallyMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit ManuallyIDTxt;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.DateEdit ManuallyDate;
        private DevExpress.XtraEditors.MemoEdit ManuallyMemo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.GridControl ManuallyGridCtrl;
        private DevExpress.XtraGrid.Views.Grid.GridView ManuallyGridView;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
    }
}