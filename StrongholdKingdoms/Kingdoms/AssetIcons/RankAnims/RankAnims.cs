namespace Kingdoms.AssetIcons.RankAnims
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), DebuggerNonUserCode]
    internal class RankAnims
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal RankAnims()
        {
        }

        internal static Bitmap crown_prince
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("crown_prince", resourceCulture);
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

        internal static Bitmap lords
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("lords", resourceCulture);
            }
        }

        internal static byte[] lords_uv
        {
            get
            {
                return (byte[]) ResourceManager.GetObject("lords_uv", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Kingdoms.AssetIcons.RankAnims.RankAnims", typeof(Kingdoms.AssetIcons.RankAnims.RankAnims).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

