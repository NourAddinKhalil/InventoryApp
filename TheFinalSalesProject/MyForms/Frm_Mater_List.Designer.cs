namespace TheFinalSalesProject.MyForms
{
    partial class Frm_Master_List
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
            this.ListGrdCtrl = new DevExpress.XtraGrid.GridControl();
            this.ListGrdViw = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlMaster = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)(this.ListGrdCtrl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListGrdViw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            this.SuspendLayout();
            // 
            // ListGrdCtrl
            // 
            this.ListGrdCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListGrdCtrl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ListGrdCtrl.Font = new System.Drawing.Font("Sylfaen", 8F);
            this.ListGrdCtrl.Location = new System.Drawing.Point(0, 48);
            this.ListGrdCtrl.MainView = this.ListGrdViw;
            this.ListGrdCtrl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ListGrdCtrl.Name = "ListGrdCtrl";
            this.ListGrdCtrl.Size = new System.Drawing.Size(789, 395);
            this.ListGrdCtrl.TabIndex = 4;
            this.ListGrdCtrl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ListGrdViw});
            // 
            // ListGrdViw
            // 
            this.ListGrdViw.Appearance.EvenRow.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ListGrdViw.Appearance.EvenRow.Options.UseBackColor = true;
            this.ListGrdViw.Appearance.FooterPanel.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Warning;
            this.ListGrdViw.Appearance.FooterPanel.Options.UseForeColor = true;
            this.ListGrdViw.Appearance.GroupFooter.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Warning;
            this.ListGrdViw.Appearance.GroupFooter.Options.UseForeColor = true;
            this.ListGrdViw.Appearance.OddRow.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ListGrdViw.Appearance.OddRow.Options.UseBackColor = true;
            this.ListGrdViw.Appearance.Row.Options.UseTextOptions = true;
            this.ListGrdViw.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.ListGrdViw.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.ListGrdViw.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.ListGrdViw.AppearancePrint.EvenRow.Options.UseTextOptions = true;
            this.ListGrdViw.AppearancePrint.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ListGrdViw.AppearancePrint.EvenRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ListGrdViw.AppearancePrint.EvenRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.ListGrdViw.AppearancePrint.GroupRow.Options.UseTextOptions = true;
            this.ListGrdViw.AppearancePrint.GroupRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.ListGrdViw.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.ListGrdViw.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.ListGrdViw.AppearancePrint.Lines.Options.UseTextOptions = true;
            this.ListGrdViw.AppearancePrint.Lines.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.ListGrdViw.AppearancePrint.OddRow.Options.UseTextOptions = true;
            this.ListGrdViw.AppearancePrint.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ListGrdViw.AppearancePrint.OddRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ListGrdViw.AppearancePrint.OddRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.ListGrdViw.AppearancePrint.Row.Options.UseTextOptions = true;
            this.ListGrdViw.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ListGrdViw.AppearancePrint.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ListGrdViw.AppearancePrint.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.ListGrdViw.ColumnPanelRowHeight = 51;
            this.ListGrdViw.DetailHeight = 253;
            this.ListGrdViw.DetailTabHeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Right;
            this.ListGrdViw.FixedLineWidth = 1;
            this.ListGrdViw.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.ListGrdViw.GridControl = this.ListGrdCtrl;
            this.ListGrdViw.HorzScrollStep = 1;
            this.ListGrdViw.Name = "ListGrdViw";
            this.ListGrdViw.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.ListGrdViw.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.ListGrdViw.OptionsBehavior.ReadOnly = true;
            this.ListGrdViw.OptionsPrint.PrintFilterInfo = true;
            this.ListGrdViw.OptionsView.ColumnAutoWidth = false;
            this.ListGrdViw.OptionsView.EnableAppearanceEvenRow = true;
            this.ListGrdViw.OptionsView.EnableAppearanceOddRow = true;
            this.ListGrdViw.OptionsView.RowAutoHeight = true;
            this.ListGrdViw.OptionsView.ShowFooter = true;
            this.ListGrdViw.Click += new System.EventHandler(this.ListGrdViw_Click);
            // 
            // layoutControlMaster
            // 
            this.layoutControlMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlMaster.Location = new System.Drawing.Point(0, 48);
            this.layoutControlMaster.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControlMaster.Name = "layoutControlMaster";
            this.layoutControlMaster.OptionsView.RightToLeftMirroringApplied = true;
            this.layoutControlMaster.Root = this.Root;
            this.layoutControlMaster.Size = new System.Drawing.Size(789, 395);
            this.layoutControlMaster.TabIndex = 5;
            this.layoutControlMaster.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(789, 395);
            this.Root.TextVisible = false;
            // 
            // Frm_Master_List
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 468);
            this.Controls.Add(this.ListGrdCtrl);
            this.Controls.Add(this.layoutControlMaster);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm_Master_List";
            this.Text = "Frm_Master_List";
            this.Load += new System.EventHandler(this.Frm_Master_List_Load);
            this.Controls.SetChildIndex(this.layoutControlMaster, 0);
            this.Controls.SetChildIndex(this.ListGrdCtrl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ListGrdCtrl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListGrdViw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected DevExpress.XtraGrid.GridControl ListGrdCtrl;
        protected DevExpress.XtraGrid.Views.Grid.GridView ListGrdViw;
        private DevExpress.XtraLayout.LayoutControl layoutControlMaster;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
    }
}