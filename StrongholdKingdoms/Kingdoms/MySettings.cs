namespace Kingdoms
{
    using DXGraphics;
    using System;
    using System.IO;
    using System.Xml.Serialization;

    [Serializable]
    public class MySettings
    {
        public int AAMode;
        public bool AdvancedTrading;
        public bool AdvertShown;
        public bool AttackSetupsUpdated;
        public bool AutoLogin;
        public bool BattleSFX = true;
        public bool BuyMultipleCardPacks = true;
        public bool CastleWalls = true;
        public bool ConfirmPlayCard = true;
        public bool Environmentals = true;
        public int EnvironmentalVolume = 0x22;
        public string facebookaccesstoken = "";
        public bool fastCashIn;
        public bool FlashingTaskbarAttack = true;
        public bool HasLoggedIn;
        public string InstalledLanguageIdent = "";
        public string languageIdent = "";
        public int LastWorldID = -1;
        public bool LicenseAlpha3Viewed;
        public bool LicenseViewed;
        public bool Maximize;
        public bool Music = true;
        public int MusicVolume = 13;
        public bool NotifyChatUpdate = true;
        public int NumWorldsCount = -1;
        public DateTime NumWorldsLastChanged = DateTime.MinValue;
        public bool OpenMultipleCardPacks = true;
        public bool OwnLanguageAvailableAndChecked;
        public string Password = "";
        public int ScreenHeight = -1;
        public int ScreenWidth = -1;
        public bool SeasonalSpecialFX = true;
        public bool SeasonalWinterLandscape = true;
        public bool SeenAnalyticsPrompt;
        public bool SendAnalytics = true;
        public bool SETTINGS_instantTooltips;
        public bool SETTINGS_showTooltips = true;
        public int SETTINGS_staticMouseTime = 400;
        public bool SFX = true;
        public int SFXVolume = 100;
        public bool showGameFeaturesScreenIcon = true;
        public bool ShowProductionInfo = true;
        public bool UseMapTextBorders = true;
        public string Username = "";
        public bool viewCapitalIDs;
        public bool viewVillageIDs;

        public bool hasLoggedIn()
        {
            return (this.HasLoggedIn || (this.Username.Length > 0));
        }

        public static MySettings load()
        {
            MySettings settings2;
            try
            {
                MySettings settings = null;
                string str = GameEngine.getSettingsPath(false);
                FileStream input = null;
                BinaryReader reader = null;
                try
                {
                    input = new FileStream(str + @"\config.dat", FileMode.Open, FileAccess.Read);
                    reader = new BinaryReader(input);
                    settings = new MySettings {
                        MusicVolume = 13,
                        SFXVolume = 100,
                        EnvironmentalVolume = 0x22,
                        Username = reader.ReadString(),
                        Password = reader.ReadString(),
                        ScreenWidth = reader.ReadInt32(),
                        ScreenHeight = reader.ReadInt32(),
                        LicenseViewed = reader.ReadBoolean(),
                        LicenseAlpha3Viewed = reader.ReadBoolean(),
                        CastleWalls = reader.ReadBoolean(),
                        NotifyChatUpdate = reader.ReadBoolean()
                    };
                    try
                    {
                        settings.ConfirmPlayCard = reader.ReadBoolean();
                        settings.SETTINGS_instantTooltips = reader.ReadBoolean();
                        settings.SETTINGS_staticMouseTime = reader.ReadInt32();
                        settings.SETTINGS_showTooltips = reader.ReadBoolean();
                        settings.LanguageIdent = reader.ReadString();
                        settings.OwnLanguageAvailableAndChecked = reader.ReadBoolean();
                        settings.BuyMultipleCardPacks = reader.ReadBoolean();
                        settings.OpenMultipleCardPacks = reader.ReadBoolean();
                        settings.Music = reader.ReadBoolean();
                        settings.MusicVolume = reader.ReadInt32();
                        settings.AAMode = reader.ReadInt32();
                        settings.LastWorldID = reader.ReadInt32();
                        settings.AutoLogin = reader.ReadBoolean();
                        settings.NumWorldsCount = reader.ReadInt32();
                        settings.NumWorldsLastChanged = new DateTime(reader.ReadInt64());
                        settings.HasLoggedIn = reader.ReadBoolean();
                        settings.fastCashIn = reader.ReadBoolean();
                        settings.SFX = reader.ReadBoolean();
                        settings.SFXVolume = reader.ReadInt32();
                        settings.Environmentals = reader.ReadBoolean();
                        settings.EnvironmentalVolume = reader.ReadInt32();
                        settings.Maximize = reader.ReadBoolean();
                        if (settings.MusicVolume < 0)
                        {
                            settings.MusicVolume = reader.ReadInt32();
                            settings.SFXVolume = reader.ReadInt32();
                            settings.EnvironmentalVolume = reader.ReadInt32();
                        }
                        else
                        {
                            if (settings.MusicVolume > 13)
                            {
                                settings.MusicVolume = 13;
                            }
                            settings.SFXVolume = 100;
                            settings.EnvironmentalVolume = 0x22;
                        }
                        settings.BattleSFX = reader.ReadBoolean();
                        settings.viewVillageIDs = reader.ReadBoolean();
                        settings.showGameFeaturesScreenIcon = reader.ReadBoolean();
                        settings.SeasonalSpecialFX = reader.ReadBoolean();
                        try
                        {
                            settings.InstalledLanguageIdent = reader.ReadString();
                        }
                        catch (Exception)
                        {
                            settings.InstalledLanguageIdent = Program.installedLangCode;
                        }
                        try
                        {
                            settings.FlashingTaskbarAttack = reader.ReadBoolean();
                        }
                        catch
                        {
                            settings.FlashingTaskbarAttack = true;
                        }
                        try
                        {
                            settings.ShowProductionInfo = reader.ReadBoolean();
                        }
                        catch
                        {
                            settings.ShowProductionInfo = true;
                        }
                        try
                        {
                            settings.AdvancedTrading = reader.ReadBoolean();
                        }
                        catch
                        {
                            settings.AdvancedTrading = false;
                        }
                        try
                        {
                            settings.AdvertShown = reader.ReadBoolean();
                        }
                        catch
                        {
                            settings.AdvertShown = false;
                        }
                        try
                        {
                            settings.viewCapitalIDs = reader.ReadBoolean();
                        }
                        catch
                        {
                            settings.viewCapitalIDs = false;
                        }
                        try
                        {
                            settings.AttackSetupsUpdated = reader.ReadBoolean();
                        }
                        catch
                        {
                            settings.AttackSetupsUpdated = false;
                        }
                        try
                        {
                            settings.SeasonalWinterLandscape = reader.ReadBoolean();
                        }
                        catch
                        {
                            settings.SeasonalWinterLandscape = true;
                        }
                        try
                        {
                            settings.UseMapTextBorders = reader.ReadBoolean();
                        }
                        catch
                        {
                            settings.UseMapTextBorders = true;
                        }
                        try
                        {
                            settings.facebookaccesstoken = reader.ReadString();
                        }
                        catch
                        {
                            settings.facebookaccesstoken = "";
                        }
                        try
                        {
                            settings.SendAnalytics = reader.ReadBoolean();
                        }
                        catch
                        {
                            settings.SendAnalytics = true;
                        }
                        try
                        {
                            settings.SeenAnalyticsPrompt = reader.ReadBoolean();
                        }
                        catch
                        {
                            settings.SeenAnalyticsPrompt = false;
                        }
                    }
                    catch (Exception)
                    {
                        settings.BattleSFX = settings.SFX;
                    }
                    reader.Close();
                    input.Close();
                    return settings;
                }
                catch (Exception)
                {
                    try
                    {
                        if (reader != null)
                        {
                            reader.Close();
                        }
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        if (input != null)
                        {
                            input.Close();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                FileStream stream = null;
                XmlSerializer serializer = new XmlSerializer(typeof(MySettings));
                try
                {
                    stream = new FileStream(str + @"\settings.dat", FileMode.Open, FileAccess.Read);
                    settings = (MySettings) serializer.Deserialize(stream);
                    stream.Close();
                    stream = null;
                    settings2 = settings;
                }
                catch (FileNotFoundException)
                {
                    settings2 = new MySettings();
                }
                catch (DirectoryNotFoundException)
                {
                    settings2 = new MySettings();
                }
                catch (Exception)
                {
                    try
                    {
                        if (stream != null)
                        {
                            stream.Close();
                        }
                    }
                    catch (Exception)
                    {
                    }
                    settings2 = new MySettings();
                }
            }
            catch (Exception)
            {
                settings2 = new MySettings();
            }
            return settings2;
        }

        public void Save()
        {
            string str = GameEngine.getSettingsPath(true);
            try
            {
                FileInfo info = new FileInfo(str + @"\config.dat") {
                    IsReadOnly = false
                };
            }
            catch (Exception)
            {
            }
            int num = -1;
            FileStream output = null;
            BinaryWriter writer = null;
            try
            {
                output = new FileStream(str + @"\config.dat", FileMode.Create);
                writer = new BinaryWriter(output);
                writer.Write(this.Username);
                writer.Write(this.Password);
                writer.Write(this.ScreenWidth);
                writer.Write(this.ScreenHeight);
                writer.Write(this.LicenseViewed);
                writer.Write(this.LicenseAlpha3Viewed);
                writer.Write(this.CastleWalls);
                writer.Write(this.NotifyChatUpdate);
                writer.Write(this.ConfirmPlayCard);
                writer.Write(this.SETTINGS_instantTooltips);
                writer.Write(this.SETTINGS_staticMouseTime);
                writer.Write(this.SETTINGS_showTooltips);
                writer.Write(this.LanguageIdent);
                writer.Write(this.OwnLanguageAvailableAndChecked);
                writer.Write(this.BuyMultipleCardPacks);
                writer.Write(this.OpenMultipleCardPacks);
                writer.Write(this.Music);
                writer.Write(num);
                writer.Write(this.AAMode);
                writer.Write(this.LastWorldID);
                writer.Write(this.AutoLogin);
                writer.Write(this.NumWorldsCount);
                writer.Write(this.NumWorldsLastChanged.Ticks);
                writer.Write(this.HasLoggedIn);
                writer.Write(this.fastCashIn);
                writer.Write(this.SFX);
                writer.Write(num);
                writer.Write(this.Environmentals);
                writer.Write(num);
                writer.Write(this.Maximize);
                writer.Write(this.MusicVolume);
                writer.Write(this.SFXVolume);
                writer.Write(this.EnvironmentalVolume);
                writer.Write(this.BattleSFX);
                writer.Write(this.viewVillageIDs);
                writer.Write(this.showGameFeaturesScreenIcon);
                writer.Write(this.SeasonalSpecialFX);
                writer.Write(this.InstalledLanguageIdent);
                writer.Write(this.FlashingTaskbarAttack);
                writer.Write(this.ShowProductionInfo);
                writer.Write(this.AdvancedTrading);
                writer.Write(this.AdvertShown);
                writer.Write(this.viewCapitalIDs);
                writer.Write(this.AttackSetupsUpdated);
                writer.Write(this.SeasonalWinterLandscape);
                writer.Write(this.UseMapTextBorders);
                writer.Write(this.facebookaccesstoken);
                writer.Write(this.SendAnalytics);
                writer.Write(this.SeenAnalyticsPrompt);
                writer.Close();
                output.Close();
            }
            catch (Exception)
            {
                try
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    if (output != null)
                    {
                        output.Close();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public string LanguageIdent
        {
            get
            {
                return this.languageIdent;
            }
            set
            {
                this.languageIdent = value;
                BaseImage.currentLangSetting = this.languageIdent;
            }
        }
    }
}

