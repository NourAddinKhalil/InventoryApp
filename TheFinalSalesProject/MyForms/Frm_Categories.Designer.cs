namespace TheFinalSalesProject.MyForms
{
    partial class Frm_Categories
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
            this.CategoriesTreLst = new DevExpress.XtraTreeList.TreeList();
            this.CategoryGropsLkUpEdt = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.CategoryNameTxt = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CategoriesTreLst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryGropsLkUpEdt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryNameTxt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.layoutControl1.Controls.Add(this.CategoriesTreLst);
            this.layoutControl1.Controls.Add(this.CategoryGropsLkUpEdt);
            this.layoutControl1.Controls.Add(this.CategoryNameTxt);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 51);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.EnableTransparentBackColor = false;
            this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(632, 448);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // CategoriesTreLst
            // 
            this.CategoriesTreLst.Location = new System.Drawing.Point(12, 68);
            this.CategoriesTreLst.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CategoriesTreLst.MinWidth = 23;
            this.CategoriesTreLst.Name = "CategoriesTreLst";
            this.CategoriesTreLst.Size = new System.Drawing.Size(608, 368);
            this.CategoriesTreLst.TabIndex = 6;
            this.CategoriesTreLst.TreeLevelWidth = 21;
            // 
            // CategoryGropsLkUpEdt
            // 
            this.CategoryGropsLkUpEdt.Location = new System.Drawing.Point(12, 40);
            this.CategoryGropsLkUpEdt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CategoryGropsLkUpEdt.Name = "CategoryGropsLkUpEdt";
            this.CategoryGropsLkUpEdt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CategoryGropsLkUpEdt.Properties.NullText = "";
            this.CategoryGropsLkUpEdt.Properties.PopupView = this.gridLookUpEdit1View;
            this.CategoryGropsLkUpEdt.Size = new System.Drawing.Size(537, 24);
            this.CategoryGropsLkUpEdt.StyleController = this.layoutControl1;
            this.CategoryGropsLkUpEdt.TabIndex = 5;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // CategoryNameTxt
            // 
            this.CategoryNameTxt.Location = new System.Drawing.Point(12, 12);
            this.CategoryNameTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CategoryNameTxt.Name = "CategoryNameTxt";
            this.CategoryNameTxt.Size = new System.Drawing.Size(537, 24);
            this.CategoryNameTxt.StyleController = this.layoutControl1;
            this.CategoryNameTxt.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(632, 448);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.CategoryGropsLkUpEdt;
            this.layoutControlItem2.CustomizationFormText = "تابع لمجموعة";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(612, 28);
            this.layoutControlItem2.Text = "تابع لمجموعة ";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(68, 18);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.CategoryNameTxt;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(612, 28);
            this.layoutControlItem1.Text = "الإسم";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(68, 18);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.CategoriesTreLst;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 56);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(612, 372);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // Frm_Categories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 526);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.Name = "Frm_Categories";
            this.Text = "فئات الأصناف";
            this.Load += new System.EventHandler(this.Frm_Categories_Load);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CategoriesTreLst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryGropsLkUpEdt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryNameTxt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraTreeList.TreeList CategoriesTreLst;
        private DevExpress.XtraEditors.GridLookUpEdit CategoryGropsLkUpEdt;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraEditors.TextEdit CategoryNameTxt;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}