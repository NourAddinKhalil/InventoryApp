using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheFinalSalesProject.Classes;
using static TheFinalSalesProject.Classes.Enum_Choices;

namespace TheFinalSalesProject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            UserLookAndFeel.Default.SkinName = Properties.Settings.Default.Skin_Name;
            UserLookAndFeel.Default.SetSkinStyle(Properties.Settings.Default.Skin_Name, Properties.Settings.Default.Skin_Plate);
            var frm = new MyForms.Frm_Log_In_Window();
            frm.Show();
            Application.Run();
            //Application.Run(new MyForms.Test());
        }

    }
}
