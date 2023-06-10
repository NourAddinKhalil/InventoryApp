using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraSplashScreen;

namespace TheFinalSalesProject.MyForms
{
    public partial class Loading_Screen : SplashScreen
    {
        public Loading_Screen()
        {
            InitializeComponent();
            this.LblCopyright.Text = "Copyright  For Devloper \n Nour Addin Khalil " + DateTime.Now.Year.ToString();
            UserLookAndFeel.Default.SkinName = TheFinalSalesProject.Properties.Settings.Default.Skin_Name;
            UserLookAndFeel.Default.SetSkinStyle(TheFinalSalesProject.Properties.Settings.Default.Skin_Name, TheFinalSalesProject.Properties.Settings.Default.Skin_Plate);
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }
    }
}