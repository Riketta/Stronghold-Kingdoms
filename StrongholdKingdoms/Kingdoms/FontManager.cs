namespace Kingdoms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Text;

    public class FontManager
    {
        public const string DEFAULT_FONT = "Arial";
        public const string DEFAULT_FONT2 = "Microsoft Sans Serif";
        public static float dpi = 96f;
        private static bool dpiSet = false;
        public static Dictionary<string, Font> fontCollection = new Dictionary<string, Font>();
        private static PrivateFontCollection pfc = new PrivateFontCollection();
        private static Dictionary<string, int> privateFontNames = new Dictionary<string, int>();

        private static string createHashString(string fontFamilyName, float pointSize, FontStyle style)
        {
            return (fontFamilyName + pointSize.ToString() + style.ToString());
        }

        private static Font getFont(string fontFamilyName, float pointSize, FontStyle style)
        {
            try
            {
                Font font = new Font(fontFamilyName, (pointSize * 96f) / dpi, style);
                if (font != null)
                {
                    return font;
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static Font GetFont(string fontFamilyName, float pointSize)
        {
            return GetFont(fontFamilyName, pointSize, FontStyle.Regular);
        }

        public static Font GetFont(string fontFamilyName, float pointSize, FontStyle style)
        {
            string key = createHashString(fontFamilyName, pointSize, style);
            try
            {
                if (fontCollection.ContainsKey(key))
                {
                    Font font = fontCollection[key];
                    if (font != null)
                    {
                        return font;
                    }
                }
            }
            catch (Exception)
            {
            }
            Font font2 = getFont(fontFamilyName, pointSize, style);
            if (font2 == null)
            {
                font2 = GetFont("Microsoft Sans Serif", pointSize, style);
            }
            if ((font2 != null) && dpiSet)
            {
                fontCollection.Add(key, font2);
            }
            return font2;
        }

        public static Font GetPrivateFont(string fileName, float pointSize, FontStyle style)
        {
            string key = createHashString(fileName, pointSize, style);
            try
            {
                Font font = fontCollection[key];
                if (font != null)
                {
                    return font;
                }
            }
            catch (Exception)
            {
            }
            try
            {
                FontFamily family = null;
                int index = -1;
                try
                {
                    index = privateFontNames[fileName];
                    family = pfc.Families[index];
                }
                catch (Exception)
                {
                    pfc.AddFontFile(fileName);
                    index = pfc.Families.Length - 1;
                    family = pfc.Families[index];
                    privateFontNames.Add(fileName, index);
                }
                Font font2 = new Font(family, (pointSize * 96f) / dpi, style);
                if (font2 != null)
                {
                    if (dpiSet)
                    {
                        fontCollection.Add(key, font2);
                    }
                    return font2;
                }
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
            return null;
        }

        public static void setDPI(Graphics gfx)
        {
            dpi = gfx.DpiX;
            dpiSet = true;
        }
    }
}

