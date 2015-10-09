namespace Kingdoms.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0")]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = ((Settings) SettingsBase.Synchronized(new Settings()));

        [DefaultSettingValue("True"), UserScopedSetting, DebuggerNonUserCode]
        public bool CastleWalls
        {
            get
            {
                return (bool) this["CastleWalls"];
            }
            set
            {
                this["CastleWalls"] = value;
            }
        }

        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        [DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("False")]
        public bool LicenseAlpha3Viewed
        {
            get
            {
                return (bool) this["LicenseAlpha3Viewed"];
            }
            set
            {
                this["LicenseAlpha3Viewed"] = value;
            }
        }

        [UserScopedSetting, DefaultSettingValue("False"), DebuggerNonUserCode]
        public bool LicenseViewed
        {
            get
            {
                return (bool) this["LicenseViewed"];
            }
            set
            {
                this["LicenseViewed"] = value;
            }
        }

        [DefaultSettingValue(""), DebuggerNonUserCode, UserScopedSetting]
        public string Password
        {
            get
            {
                return (string) this["Password"];
            }
            set
            {
                this["Password"] = value;
            }
        }

        [DefaultSettingValue("-1"), UserScopedSetting, DebuggerNonUserCode]
        public int ScreenHeight
        {
            get
            {
                return (int) this["ScreenHeight"];
            }
            set
            {
                this["ScreenHeight"] = value;
            }
        }

        [DefaultSettingValue("-1"), UserScopedSetting, DebuggerNonUserCode]
        public int ScreenWidth
        {
            get
            {
                return (int) this["ScreenWidth"];
            }
            set
            {
                this["ScreenWidth"] = value;
            }
        }

        [DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("")]
        public string Username
        {
            get
            {
                return (string) this["Username"];
            }
            set
            {
                this["Username"] = value;
            }
        }
    }
}

