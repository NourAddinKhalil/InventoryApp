namespace TheFinalSalesProject.MyForms
{
    partial class Frm_Accounts_Balances_List
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
            this.SpnDebitSum = new DevExpress.XtraEditors.SpinEdit();
            this.SpnCreditSum = new DevExpress.XtraEditors.SpinEdit();
            this.SpnNet = new DevExpress.XtraEditors.SpinEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpnDebitSum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpnCreditSum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpnNet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.SpnDebitSum);
            this.layoutControl1.Controls.Add(this.SpnCreditSum);
            this.layoutControl1.Controls.Add(this.SpnNet);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.layoutControl1.Location = new System.Drawing.Point(0, 361);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(933, 164);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // SpnDebitSum
            // 
            this.SpnDebitSum.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SpnDebitSum.Location = new System.Drawing.Point(546, 11);
            this.SpnDebitSum.Name = "SpnDebitSum";
            this.SpnDebitSum.Properties.Appearance.Font = new System.Drawing.Font("Sylfaen", 10F);
            this.SpnDebitSum.Properties.Appearance.Options.UseFont = true;
            this.SpnDebitSum.Properties.Appearance.Options.UseTextOptions = true;
            this.SpnDebitSum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SpnDebitSum.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.SpnDebitSum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SpnDebitSum.Properties.Mask.EditMask = "n2";
            this.SpnDebitSum.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.SpnDebitSum.Size = new System.Drawing.Size(322, 24);
            this.SpnDebitSum.StyleController = this.layoutControl1;
            this.SpnDebitSum.TabIndex = 4;
            // 
            // SpnCreditSum
            // 
            this.SpnCreditSum.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SpnCreditSum.Location = new System.Drawing.Point(546, 39);
            this.SpnCreditSum.Name = "SpnCreditSum";
            this.SpnCreditSum.Properties.Appearance.Font = new System.Drawing.Font("Sylfaen", 10F);
            this.SpnCreditSum.Properties.Appearance.Options.UseFont = true;
            this.SpnCreditSum.Properties.Appearance.Options.UseTextOptions = true;
            this.SpnCreditSum.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SpnCreditSum.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.SpnCreditSum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SpnCreditSum.Properties.Mask.EditMask = "n2";
            this.SpnCreditSum.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.SpnCreditSum.Size = new System.Drawing.Size(322, 24);
            this.SpnCreditSum.StyleController = this.layoutControl1;
            this.SpnCreditSum.TabIndex = 5;
            // 
            // SpnNet
            // 
            this.SpnNet.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SpnNet.Location = new System.Drawing.Point(546, 67);
            this.SpnNet.Name = "SpnNet";
            this.SpnNet.Properties.Appearance.Font = new System.Drawing.Font("Sylfaen", 10F);
            this.SpnNet.Properties.Appearance.Options.UseFont = true;
            this.SpnNet.Properties.Appearance.Options.UseTextOptions = true;
            this.SpnNet.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SpnNet.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.SpnNet.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SpnNet.Properties.Mask.EditMask = "n2";
            this.SpnNet.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.SpnNet.Size = new System.Drawing.Size(322, 24);
            this.SpnNet.StyleController = this.layoutControl1;
            this.SpnNet.TabIndex = 6;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.emptySpaceItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(933, 164);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.SpnDebitSum;
            this.layoutControlItem1.Location = new System.Drawing.Point(534, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(379, 28);
            this.layoutControlItem1.Text = "مجموع مدين";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(50, 16);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.SpnCreditSum;
            this.layoutControlItem2.Location = new System.Drawing.Point(534, 28);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(379, 28);
            this.layoutControlItem2.Text = "مجموع دائن";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(50, 16);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.SpnNet;
            this.layoutControlItem3.Location = new System.Drawing.Point(534, 56);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(379, 28);
            this.layoutControlItem3.Text = "الصافي";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(50, 16);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 84);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(913, 62);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(534, 84);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // Frm_Accounts_Balances_List
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 550);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Frm_Accounts_Balances_List";
            this.Text = "Frm_Accounts_Balances_List";
            this.Load += new System.EventHandler(this.Frm_Accounts_Balances_List_Load);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SpnDebitSum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpnCreditSum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpnNet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SpinEdit SpnDebitSum;
        private DevExpress.XtraEditors.SpinEdit SpnCreditSum;
        private DevExpress.XtraEditors.SpinEdit SpnNet;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}