using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.Classes
{
    public static class Layout_Saving
    {
        public static string Setting_Path
        {
            get
            {
                var document = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var appName = "King Sale";
                var path = Path.Combine(document, appName, "Layout Settings");
                Directory.CreateDirectory(path);
                return path;
            }
        }
        public static void Save_Lyout(this GridView view, string parentName)
        {
            try
            {
                string filePath = $"{Setting_Path} \\ {parentName}_{view.Name}";
                view.SaveLayoutToXml(filePath);
            }
            catch
            {

            }
        }
        public static void Load_Lyout(this GridView view, string parentName)
        {
            try
            {
                string filePath = $"{Setting_Path} \\ {parentName}_{view.Name}";
                if (File.Exists(filePath))
                    view.RestoreLayoutFromXml(filePath);
            }
            catch
            {

            }
        }
        public static void Save_Group_Layout(this LayoutControl control, string parentName)
        {
            try
            {
                string filePath = $"{Setting_Path} \\ {parentName}_{control.Name}";
                control.SaveLayoutToXml(filePath);
            }
            catch
            {

                throw;
            }
        }
        public static void Load_Group_Layout(this LayoutControl control, string parentName)
        {
            try
            {
                string filePath = $"{Setting_Path} \\ {parentName}_{control.Name}";
                if (File.Exists(filePath))
                    control.RestoreLayoutFromXml(filePath);
            }
            catch
            {

                throw;
            }
        }
    }
}
