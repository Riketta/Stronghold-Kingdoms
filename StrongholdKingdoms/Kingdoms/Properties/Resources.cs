namespace Kingdoms.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
    internal class Resources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resources()
        {
        }

        internal static Bitmap connectinglogo
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("connectinglogo", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Kingdoms.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static Bitmap right_side_panel_large
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("right_side_panel_large", resourceCulture);
            }
        }

        internal static Bitmap right_side_panel_large_stone_tan
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("right_side_panel_large_stone_tan", resourceCulture);
            }
        }

        internal static Icon shk_icon
        {
            get
            {
                return (Icon) ResourceManager.GetObject("shk_icon", resourceCulture);
            }
        }

        internal static Icon shk_icon1
        {
            get
            {
                return (Icon) ResourceManager.GetObject("shk_icon1", resourceCulture);
            }
        }

        internal static Bitmap splash_screen
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("splash_screen", resourceCulture);
            }
        }
    }
}

