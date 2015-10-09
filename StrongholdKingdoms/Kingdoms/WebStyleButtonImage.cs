namespace Kingdoms
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;

    public class WebStyleButtonImage
    {
        public static Image Generate(int width, int height, string text, Font font, Color forecolour, Color backcolour, int radius)
        {
            int num = radius * 2;
            Image image = new Bitmap(width + 1, height + 1);
            SolidBrush brush = new SolidBrush(forecolour);
            SolidBrush brush2 = new SolidBrush(backcolour);
            Pen pen = new Pen(forecolour);
            Pen pen2 = new Pen(backcolour);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.Clear(ARGBColors.Transparent);
                graphics.FillRectangle(brush2, new Rectangle(0, radius, width, height - num));
                graphics.FillRectangle(brush2, new Rectangle(radius, 0, width - num, height));
                graphics.FillEllipse(brush2, new Rectangle(0, 0, num, num));
                graphics.FillEllipse(brush2, new Rectangle(0, height - num, num, num));
                graphics.FillEllipse(brush2, new Rectangle(width - num, 0, num, num));
                graphics.FillEllipse(brush2, new Rectangle(width - num, height - num, num, num));
                StringFormat format = new StringFormat {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center
                };
                font = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-BoldCond.ttf", 12f, FontStyle.Bold);
                graphics.DrawString(text, font, brush, new RectangleF(0f, 0f, (float) (width + 1), (float) (height + 1)), format);
            }
            pen.Dispose();
            pen2.Dispose();
            brush.Dispose();
            brush2.Dispose();
            return image;
        }

        public static Image GenerateLabel(int width, int height, string text, Color forecolour, Color backcolour)
        {
            Image image = new Bitmap(width + 1, height + 1);
            SolidBrush brush = new SolidBrush(forecolour);
            SolidBrush brush2 = new SolidBrush(backcolour);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.Clear(backcolour);
                StringFormat format = new StringFormat {
                    LineAlignment = StringAlignment.Near,
                    Alignment = StringAlignment.Near
                };
                Font font = FontManager.GetPrivateFont("AssetIcons/Cards/panel/Brokenscript-BoldCond.ttf", 14f, FontStyle.Bold);
                graphics.DrawString(text, font, brush, new RectangleF(0f, 0f, (float) (width + 1), (float) (height + 1)), format);
            }
            brush.Dispose();
            brush2.Dispose();
            return image;
        }
    }
}

