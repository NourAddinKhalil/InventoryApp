using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.Utils.Svg;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.Classes
{
    public static class Draw_In_GridView
    {
        public static void Add_Button_To_Group_Header(this GridView view, SvgImage image, EventHandler handler, bool righToLeft)
        {
            Custom_Button_Header button_Header = new Custom_Button_Header(view, image, handler, righToLeft);
        }
        public class Custom_Button_Header
        {
            GridView view;
            SvgImage image;
            EventHandler handler;
            bool righToLeft;
            public Custom_Button_Header(GridView gridView, SvgImage svgImage, EventHandler eventHandler, bool righToLeft)
            {
                view = gridView;
                image = svgImage;
                handler = eventHandler;
                this.righToLeft = righToLeft;
                view.CustomDrawGroupPanel += View_CustomDrawGroupPanel;
                view.MouseMove += View_MouseMove;
                view.Click += View_Click;
            }

            private void View_Click(object sender, EventArgs e)
            {
                DXMouseEventArgs ea = e as DXMouseEventArgs;
                if (rectangle.Contains(ea.Location))
                {
                    handler(sender, e);
                }
            }
            private void View_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
            {
                isInRectangel = rectangle.Contains(e.Location);
                view.Invalidate();
            }
            private bool isInRectangel;
            Rectangle rectangle;
            private void View_CustomDrawGroupPanel(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
            {
                if (image == null) return;
                int imagsize = 16;
                SvgBitmap imageBitmap = SvgBitmap.Create(image);
                Brush brush = e.Cache.GetGradientBrush(e.Bounds, Color.Transparent, Color.Transparent,
                    System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
                if (righToLeft)
                {
                    rectangle = new Rectangle(
                        /*e.Bounds.X +e.Bounds.Width -*/  (imagsize * 3)
                        , e.Bounds.Y + ((e.Bounds.Height - imagsize) / 2)
                        , imagsize, imagsize);
                }
                else
                {
                    rectangle = new Rectangle(
                    e.Bounds.X + e.Bounds.Width - (imagsize * 3)
                    , e.Bounds.Y + ((e.Bounds.Height - imagsize) / 2)
                    , imagsize, imagsize);
                }
                var palette = SvgPaletteHelper.GetSvgPalette(UserLookAndFeel.Default,
                    DevExpress.Utils.Drawing.ObjectState.Normal);
                e.Cache.DrawImage(imageBitmap.Render(palette), rectangle);
                int thickness = (isInRectangel) ? 2 : 1;
                int offset = thickness + 1;
                e.Cache.DrawRectangle((rectangle.X - offset), (rectangle.Y - offset), (rectangle.Width + offset * offset), (rectangle.Height + offset * offset), Color.Black, thickness);

                e.Handled = true;
            }
        }
    }
}
