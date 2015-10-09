namespace Kingdoms
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class Sound
    {
        private static PlayList battleEndDefeatMusicPlayList = new PlayList();
        private static PlayList battleEndVictoryMusicPlayList = new PlayList();
        private static PlayList battleMusicPlayList = new PlayList();
        private static bool BattleSFXActive = true;
        private static PlayList currentPlayingPlayList = null;
        private static PlayList defaultMusicPlayList = new PlayList();
        private static List<DelayedSound> delayedSounds = new List<DelayedSound>();
        private static bool envActive = true;
        private static string[] environmentalSounds = new string[] { 
            "environment_lowland.mp3", "environment_highland.mp3", "environment_river1.mp3", "environment_river2.mp3", "environment_mountainpeak.mp3", "environment_saltflat.mp3", "environment_marsh.mp3", "environment_plains.mp3", "environment_valleyside.mp3", "environment_forest.mp3", "environment_parish.mp3", "environment_county.mp3", "environment_province.mp3", "environment_country.mp3", "environment_capital_quiet.mp3", "environment_capital_normal.mp3", 
            "environment_capital_busy.mp3", "environment_castle.mp3", "environment_castle_construction.mp3", "environment_world.mp3", "environment_rank_2.mp3", "environment_rank_3.mp3", "environment_rank_4.mp3", "environment_rank_5.mp3", "environment_rank_6.mp3", "environment_rank_7.mp3", "environment_rank_8.mp3", "environment_rank_9.mp3", "environment_rank_10.mp3", "environment_rank_11.mp3", "environment_rank_12.mp3", "environment_rank_13.mp3", 
            "environment_rank_14.mp3", "environment_rank_15.mp3", "environment_rank_16.mp3", "environment_rank_17.mp3", "environment_rank_18.mp3", "environment_rank_19.mp3", "environment_rank_20.mp3", "environment_rank_21.mp3", "environment_rank_22.mp3", "environment_rank_23.mp3", "environment_battle_1.mp3", "environment_battle_2.mp3", "environment_battle_3.mp3", "environment_battle_4.mp3", "environment_rank_18_short_4.mp3"
         };
        public static bool envPaused = false;
        private static float FADE_DURATION = 1f;
        private static bool musicActive = true;
        public static bool musicPaused = false;
        private static bool playingBattleMusic = false;
        public const int RANK_SOUND_10 = 0x1c;
        public const int RANK_SOUND_11 = 0x1d;
        public const int RANK_SOUND_12 = 30;
        public const int RANK_SOUND_13 = 0x1f;
        public const int RANK_SOUND_14 = 0x20;
        public const int RANK_SOUND_15 = 0x21;
        public const int RANK_SOUND_16 = 0x22;
        public const int RANK_SOUND_17 = 0x23;
        public const int RANK_SOUND_18 = 0x24;
        public const int RANK_SOUND_19 = 0x25;
        public const int RANK_SOUND_2 = 20;
        public const int RANK_SOUND_20 = 0x26;
        public const int RANK_SOUND_21 = 0x27;
        public const int RANK_SOUND_22 = 40;
        public const int RANK_SOUND_23 = 0x29;
        public const int RANK_SOUND_3 = 0x15;
        public const int RANK_SOUND_4 = 0x16;
        public const int RANK_SOUND_5 = 0x17;
        public const int RANK_SOUND_6 = 0x18;
        public const int RANK_SOUND_7 = 0x19;
        public const int RANK_SOUND_8 = 0x1a;
        public const int RANK_SOUND_9 = 0x1b;
        public const int REWARD_JINGLE = 0x2e;
        private static bool s_blockEnvWhilePlaying = false;
        private static float s_currentFadeVolume = 1f;
        private static int s_currentVillageEnvironmental = -1;
        private static bool s_fading = false;
        private static bool s_loop = true;
        private static bool s_silencedMusic = true;
        private static DateTime s_startFadeDT = DateTime.MinValue;
        private static float s_startFadeVolume = 1f;
        private static float s_storedMusicVolume = 1f;
        private static float s_targetFadeVolume = 1f;
        public const int SFX_PLACE_BUILDING = 0x2711;
        private static bool sfxActive = true;
        public const int WORLD_AREA_TYPE_BATTLE_1 = 0x2a;
        public const int WORLD_AREA_TYPE_BATTLE_2 = 0x2b;
        public const int WORLD_AREA_TYPE_BATTLE_3 = 0x2c;
        public const int WORLD_AREA_TYPE_BATTLE_4 = 0x2d;
        public const int WORLD_AREA_TYPE_CAPITAL_BUSY = 0x10;
        public const int WORLD_AREA_TYPE_CAPITAL_NORMAL = 15;
        public const int WORLD_AREA_TYPE_CAPITAL_QUIET = 14;
        public const int WORLD_AREA_TYPE_CASTLE = 0x11;
        public const int WORLD_AREA_TYPE_CASTLE_CONSTRUCTION = 0x12;
        public const int WORLD_AREA_TYPE_COUNTRY_CAPITAL = 13;
        public const int WORLD_AREA_TYPE_COUNTY_CAPITAL = 11;
        public const int WORLD_AREA_TYPE_FOREST = 9;
        public const int WORLD_AREA_TYPE_LOWLAND = 0;
        public const int WORLD_AREA_TYPE_MARSH = 6;
        public const int WORLD_AREA_TYPE_MOUNTAIN_PEAK = 4;
        public const int WORLD_AREA_TYPE_PARISH_CAPITAL = 10;
        public const int WORLD_AREA_TYPE_PLAINS = 7;
        public const int WORLD_AREA_TYPE_PROVINCE_CAPITAL = 12;
        public const int WORLD_AREA_TYPE_RIVER1 = 2;
        public const int WORLD_AREA_TYPE_RIVER2 = 3;
        public const int WORLD_AREA_TYPE_SALTFLAT = 5;
        public const int WORLD_AREA_TYPE_UPLAND = 1;
        public const int WORLD_AREA_TYPE_VALLEYSIDE = 8;
        public const int WORLD_AREA_TYPE_WORLD = 0x13;

        public static void createPlayLists()
        {
            defaultMusicPlayList.addEntry(new PlayListEntry(Application.StartupPath + @"\assets\music\monks1.mp3", 0.5f, 30, 4));
            defaultMusicPlayList.addEntry(new PlayListEntry(Application.StartupPath + @"\assets\music\mandloop1.mp3", 0.5f, 30));
            defaultMusicPlayList.addEntry(new PlayListEntry(Application.StartupPath + @"\assets\music\sadtimesb.mp3", 0.5f, 30));
            defaultMusicPlayList.addEntry(new PlayListEntry(Application.StartupPath + @"\assets\music\stainedglass-All.mp3", 0.5f, 30));
            defaultMusicPlayList.addEntry(new PlayListEntry(Application.StartupPath + @"\assets\music\the maidenA.mp3", 0.5f, 30));
            defaultMusicPlayList.addEntry(new PlayListEntry(Application.StartupPath + @"\assets\music\underanoldtree.mp3", 0.5f, 30));
            defaultMusicPlayList.addEntry(new PlayListEntry(Application.StartupPath + @"\assets\music\journeys.mp3", 0.5f, 30));
            defaultMusicPlayList.random = true;
            battleMusicPlayList.addEntry(new PlayListEntry(Application.StartupPath + @"\assets\music\Battle.mp3", 0.5f, 15));
            battleMusicPlayList.addEntry(new PlayListEntry(Application.StartupPath + @"\assets\music\glory_03.mp3", 0.5f, 15));
            battleMusicPlayList.addEntry(new PlayListEntry(Application.StartupPath + @"\assets\music\honor_04.mp3", 0.5f, 15));
            battleMusicPlayList.random = true;
            battleEndDefeatMusicPlayList.addEntry(new PlayListEntry(Application.StartupPath + @"\assets\music\battle end defeat.mp3", 0.5f, 0x15180));
            battleEndVictoryMusicPlayList.addEntry(new PlayListEntry(Application.StartupPath + @"\assets\music\battle end.mp3", 0.5f, 0x15180));
            loadInterfaceSounds();
        }

        public static void fadeInVillageEnvironmental(int villageType)
        {
            playVillageEnvironmental(villageType, true, false, true);
        }

        public static void fadeOutCurrentPlaying()
        {
            if (!s_fading)
            {
                s_fading = true;
                s_targetFadeVolume = 0f;
                s_startFadeDT = DateTime.Now;
                FADE_DURATION = GameEngine.Instance.AudioEngine.getVolumeFromTag("BattleFadeLength") * 100f;
                if (isEnvAnSFX(s_currentVillageEnvironmental) || ((s_currentVillageEnvironmental >= 0x2a) && (s_currentVillageEnvironmental <= 0x2d)))
                {
                    s_currentFadeVolume = s_startFadeVolume = GameEngine.Instance.AudioEngine.getCurrentMP3Volume(2);
                }
                else
                {
                    s_currentFadeVolume = s_startFadeVolume = GameEngine.Instance.AudioEngine.getCurrentMP3Volume(1);
                }
            }
        }

        public static void forceFullPlayOfNextEnvironmental()
        {
            s_blockEnvWhilePlaying = true;
        }

        public static int getCurrentEnvironmental()
        {
            return s_currentVillageEnvironmental;
        }

        public static bool isEnvAnSFX(int env)
        {
            if (((env < 20) || (env > 0x29)) && (env != 0x2e))
            {
                return false;
            }
            return true;
        }

        public static bool isFading()
        {
            return s_fading;
        }

        public static bool isPlayingEnvironmental(int soundID)
        {
            return (soundID == s_currentVillageEnvironmental);
        }

        public static void loadInterfaceSounds()
        {
        }

        public static void monitorEnvironmentals()
        {
            if (s_currentVillageEnvironmental >= 0)
            {
                int channel = 1;
                if (isEnvAnSFX(s_currentVillageEnvironmental))
                {
                    channel = 2;
                }
                if ((s_currentVillageEnvironmental >= 0x2a) && (s_currentVillageEnvironmental <= 0x2d))
                {
                    channel = 2;
                    if ((GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_CASTLE) || (GameEngine.Instance.GameDisplayModeSubMode != GameEngine.GameDisplaySubModes.SUBMODE_BATTLE))
                    {
                        stopVillageEnvironmental();
                        return;
                    }
                }
                if (!GameEngine.Instance.AudioEngine.isMP3Playing(channel) && !GameEngine.Instance.AudioEngine.isMP3Paused(channel))
                {
                    if (s_loop)
                    {
                        string str = Application.StartupPath + @"\assets\SFX\";
                        float volume = 1f;
                        if (isEnvAnSFX(s_currentVillageEnvironmental))
                        {
                            volume = GameEngine.Instance.AudioEngine.getVolumeFromTag("VolumeOnly_rank");
                            GameEngine.Instance.AudioEngine.playMp3(str + environmentalSounds[s_currentVillageEnvironmental], volume, 2);
                        }
                        else if ((s_currentVillageEnvironmental >= 0x2a) && (s_currentVillageEnvironmental <= 0x2d))
                        {
                            volume = GameEngine.Instance.AudioEngine.getVolumeFromTag("VolumeOnly_battle");
                            GameEngine.Instance.AudioEngine.playMp3(str + environmentalSounds[s_currentVillageEnvironmental], volume, 2);
                        }
                        else
                        {
                            volume = GameEngine.Instance.AudioEngine.getVolumeFromTag("VolumeOnly_environment");
                            GameEngine.Instance.AudioEngine.playMp3(str + environmentalSounds[s_currentVillageEnvironmental], volume, 1);
                        }
                    }
                    else
                    {
                        stopVillageEnvironmental();
                        s_fading = false;
                        restoreSilencedMusic();
                    }
                }
                if (s_fading)
                {
                    TimeSpan span = (TimeSpan) (DateTime.Now - s_startFadeDT);
                    if (span.TotalSeconds > FADE_DURATION)
                    {
                        if (s_targetFadeVolume == 0f)
                        {
                            stopVillageEnvironmental();
                        }
                        else
                        {
                            s_fading = false;
                            s_currentFadeVolume = s_targetFadeVolume;
                            if (isEnvAnSFX(s_currentVillageEnvironmental) || ((s_currentVillageEnvironmental >= 0x2a) && (s_currentVillageEnvironmental <= 0x2d)))
                            {
                                GameEngine.Instance.AudioEngine.setCurrentMP3Volume(s_currentFadeVolume, 2);
                            }
                            else
                            {
                                GameEngine.Instance.AudioEngine.setCurrentMP3Volume(s_currentFadeVolume, 1);
                            }
                        }
                    }
                    else
                    {
                        float num3 = ((float) span.TotalSeconds) / FADE_DURATION;
                        s_currentFadeVolume = ((s_targetFadeVolume - s_startFadeVolume) * num3) + s_startFadeVolume;
                        if (isEnvAnSFX(s_currentVillageEnvironmental) || ((s_currentVillageEnvironmental >= 0x2a) && (s_currentVillageEnvironmental <= 0x2d)))
                        {
                            GameEngine.Instance.AudioEngine.setCurrentMP3Volume(s_currentFadeVolume, 2);
                        }
                        else
                        {
                            GameEngine.Instance.AudioEngine.setCurrentMP3Volume(s_currentFadeVolume, 1);
                        }
                    }
                }
            }
        }

        public static void monitorMusic()
        {
            if (musicActive)
            {
                if (currentPlayingPlayList != null)
                {
                    currentPlayingPlayList.update();
                }
                if (InterfaceMgr.Instance.ParentForm != null)
                {
                    if (!InterfaceMgr.Instance.ParentForm.Visible || (InterfaceMgr.Instance.ParentForm.WindowState == FormWindowState.Minimized))
                    {
                        pauseMusic();
                    }
                    else
                    {
                        resumeMusic();
                    }
                }
                if (playingBattleMusic && ((GameEngine.Instance.GameDisplayMode != GameEngine.GameDisplays.DISPLAY_CASTLE) || (GameEngine.Instance.GameDisplayModeSubMode != GameEngine.GameDisplaySubModes.SUBMODE_BATTLE)))
                {
                    playMusic();
                }
            }
            if (EnvironmentActive && (InterfaceMgr.Instance.ParentForm != null))
            {
                if (!InterfaceMgr.Instance.ParentForm.Visible || (InterfaceMgr.Instance.ParentForm.WindowState == FormWindowState.Minimized))
                {
                    pauseEnv();
                }
                else
                {
                    resumeEnv();
                }
            }
            monitorEnvironmentals();
            processDelayedSounds();
        }

        public static void pauseEnv()
        {
            if (EnvironmentActive && !envPaused)
            {
                envPaused = true;
                GameEngine.Instance.AudioEngine.pauseMp3(2);
                GameEngine.Instance.AudioEngine.pauseMp3(1);
            }
        }

        public static void pauseEnvironmental(bool pause)
        {
            if (s_currentVillageEnvironmental >= 0)
            {
                if (isEnvAnSFX(s_currentVillageEnvironmental) || ((s_currentVillageEnvironmental >= 0x2a) && (s_currentVillageEnvironmental <= 0x2d)))
                {
                    if (pause)
                    {
                        GameEngine.Instance.AudioEngine.pauseMp3(2);
                    }
                    else
                    {
                        GameEngine.Instance.AudioEngine.resumeMp3(2);
                    }
                }
                else if (pause)
                {
                    GameEngine.Instance.AudioEngine.pauseMp3(1);
                }
                else
                {
                    GameEngine.Instance.AudioEngine.resumeMp3(1);
                }
            }
        }

        public static void pauseMusic()
        {
            if (musicActive && !musicPaused)
            {
                musicPaused = true;
                GameEngine.Instance.AudioEngine.pauseMp3(0);
            }
        }

        public static void playBattleEndDefeatMusic()
        {
            playPlayList(battleEndDefeatMusicPlayList);
            playingBattleMusic = true;
        }

        public static void playBattleEndVictoryMusic()
        {
            playPlayList(battleEndVictoryMusicPlayList);
            playingBattleMusic = true;
        }

        public static void playBattleMusic()
        {
            playPlayList(battleMusicPlayList);
            playingBattleMusic = true;
        }

        public static void playDelayedInterfaceSound(string tag, int delayMS)
        {
            DelayedSound item = new DelayedSound {
                tag = tag,
                playTime = DateTime.Now.AddMilliseconds((double) delayMS)
            };
            delayedSounds.Add(item);
        }

        public static void playInterfaceSound(int sfx)
        {
        }

        public static void playMusic()
        {
            playPlayList(defaultMusicPlayList);
            playingBattleMusic = false;
        }

        private static void playPlayList(PlayList pl)
        {
            stopMusic();
            if (musicActive)
            {
                currentPlayingPlayList = pl;
                currentPlayingPlayList.play();
                musicPaused = false;
            }
        }

        public static void playVillageEnvironmental(int villageType)
        {
            playVillageEnvironmental(villageType, true, false, false);
        }

        public static void playVillageEnvironmental(int villageType, bool loop, bool silenceMusic)
        {
            playVillageEnvironmental(villageType, loop, silenceMusic, false);
        }

        public static void playVillageEnvironmental(int villageType, bool loop, bool silenceMusic, bool fadeIn)
        {
            if (villageType != s_currentVillageEnvironmental)
            {
                if (s_blockEnvWhilePlaying)
                {
                    int channel = 1;
                    if (isEnvAnSFX(s_currentVillageEnvironmental))
                    {
                        channel = 2;
                    }
                    if (GameEngine.Instance.AudioEngine.isMP3Playing(channel))
                    {
                        return;
                    }
                    s_blockEnvWhilePlaying = false;
                }
                stopVillageEnvironmental();
                if (isEnvAnSFX(villageType) || ((villageType >= 0x2a) && (villageType <= 0x2d)))
                {
                    if (!SFXActive)
                    {
                        return;
                    }
                }
                else if (!envActive)
                {
                    return;
                }
                s_currentVillageEnvironmental = villageType;
                s_loop = loop;
                s_silencedMusic = silenceMusic;
                if (silenceMusic)
                {
                    s_storedMusicVolume = GameEngine.Instance.AudioEngine.getMP3Volume(0);
                    GameEngine.Instance.AudioEngine.setMP3MasterVolume(0.001f, 0);
                }
                string str = Application.StartupPath + @"\assets\SFX\";
                if (fadeIn)
                {
                    s_fading = true;
                    s_startFadeDT = DateTime.Now;
                    s_currentFadeVolume = s_startFadeVolume = 0f;
                    FADE_DURATION = GameEngine.Instance.AudioEngine.getVolumeFromTag("BattleFadeLength") * 100f;
                }
                float volume = 1f;
                if (isEnvAnSFX(s_currentVillageEnvironmental))
                {
                    volume = GameEngine.Instance.AudioEngine.getVolumeFromTag("VolumeOnly_rank");
                    if (fadeIn)
                    {
                        s_targetFadeVolume = volume * GameEngine.Instance.AudioEngine.getMP3Volume(2);
                        volume = 0f;
                    }
                    GameEngine.Instance.AudioEngine.playMp3(str + environmentalSounds[s_currentVillageEnvironmental], volume, 2);
                }
                else if ((s_currentVillageEnvironmental >= 0x2a) && (s_currentVillageEnvironmental <= 0x2d))
                {
                    volume = GameEngine.Instance.AudioEngine.getVolumeFromTag("VolumeOnly_battle");
                    if (fadeIn)
                    {
                        s_targetFadeVolume = volume * GameEngine.Instance.AudioEngine.getMP3Volume(2);
                        volume = 0f;
                    }
                    GameEngine.Instance.AudioEngine.playMp3(str + environmentalSounds[s_currentVillageEnvironmental], volume, 2);
                }
                else
                {
                    volume = GameEngine.Instance.AudioEngine.getVolumeFromTag("VolumeOnly_environment");
                    if (fadeIn)
                    {
                        s_targetFadeVolume = volume * GameEngine.Instance.AudioEngine.getMP3Volume(1);
                        volume = 0f;
                    }
                    GameEngine.Instance.AudioEngine.playMp3(str + environmentalSounds[s_currentVillageEnvironmental], volume, 1);
                }
            }
        }

        public static void processDelayedSounds()
        {
            if (delayedSounds.Count > 0)
            {
                List<DelayedSound> list = new List<DelayedSound>();
                foreach (DelayedSound sound in delayedSounds)
                {
                    if (sound.playTime < DateTime.Now)
                    {
                        GameEngine.Instance.playInterfaceSound(sound.tag);
                        list.Add(sound);
                    }
                }
                if (list.Count > 0)
                {
                    foreach (DelayedSound sound2 in list)
                    {
                        delayedSounds.Remove(sound2);
                    }
                }
            }
        }

        public static void restoreSilencedMusic()
        {
            if (s_silencedMusic)
            {
                s_silencedMusic = false;
                GameEngine.Instance.AudioEngine.setMP3MasterVolume(s_storedMusicVolume, 0);
            }
        }

        public static void resumeEnv()
        {
            if (EnvironmentActive && envPaused)
            {
                envPaused = false;
                GameEngine.Instance.AudioEngine.resumeMp3(2);
                GameEngine.Instance.AudioEngine.resumeMp3(1);
            }
        }

        public static void resumeMusic()
        {
            if (musicActive && musicPaused)
            {
                musicPaused = false;
                GameEngine.Instance.AudioEngine.resumeMp3(0);
            }
        }

        public static void setBattleSFXState(bool active)
        {
            if (active != BattleSFXActive)
            {
                BattleSFXActive = active;
                if (!active)
                {
                    stopVillageEnvironmentalSFXOnly();
                }
            }
        }

        public static void setEnvironmentalState(bool active)
        {
            if (active != envActive)
            {
                envActive = active;
                if (!active)
                {
                    stopVillageEnvironmentalOnly();
                }
            }
        }

        public static void setMusicState(bool active)
        {
            if (active != musicActive)
            {
                musicActive = active;
                if (active)
                {
                    if ((GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE) && (GameEngine.Instance.GameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_BATTLE))
                    {
                        playBattleMusic();
                    }
                    else
                    {
                        playMusic();
                    }
                }
                else
                {
                    stopMusic();
                }
            }
        }

        public static void setSFXState(bool active)
        {
            if (active != sfxActive)
            {
                sfxActive = active;
                if (!active)
                {
                    stopVillageEnvironmentalSFXOnly();
                }
            }
        }

        public static void stopMusic()
        {
            playingBattleMusic = false;
            if (currentPlayingPlayList != null)
            {
                currentPlayingPlayList.stop();
                currentPlayingPlayList = null;
            }
        }

        public static void stopVillageEnvironmental()
        {
            if (s_currentVillageEnvironmental >= 0)
            {
                s_blockEnvWhilePlaying = false;
                s_currentVillageEnvironmental = -1;
                s_fading = false;
                restoreSilencedMusic();
                GameEngine.Instance.AudioEngine.stopMp3(1);
                GameEngine.Instance.AudioEngine.stopMp3(2);
            }
        }

        public static void stopVillageEnvironmentalExceptWorld()
        {
            if ((s_currentVillageEnvironmental >= 0) && (s_currentVillageEnvironmental != 0x13))
            {
                s_blockEnvWhilePlaying = false;
                s_currentVillageEnvironmental = -1;
                s_fading = false;
                restoreSilencedMusic();
                GameEngine.Instance.AudioEngine.stopMp3(1);
                GameEngine.Instance.AudioEngine.stopMp3(2);
            }
        }

        public static void stopVillageEnvironmentalOnly()
        {
            if (s_currentVillageEnvironmental >= 0)
            {
                s_blockEnvWhilePlaying = false;
                s_currentVillageEnvironmental = -1;
                s_fading = false;
                restoreSilencedMusic();
                GameEngine.Instance.AudioEngine.stopMp3(1);
            }
        }

        public static void stopVillageEnvironmentalSFXOnly()
        {
            if (s_currentVillageEnvironmental >= 0)
            {
                s_blockEnvWhilePlaying = false;
                s_currentVillageEnvironmental = -1;
                s_fading = false;
                restoreSilencedMusic();
                GameEngine.Instance.AudioEngine.stopMp3(2);
            }
        }

        public static bool EnvironmentActive
        {
            get
            {
                return envActive;
            }
        }

        public static bool SFXActive
        {
            get
            {
                if ((GameEngine.Instance.GameDisplayMode == GameEngine.GameDisplays.DISPLAY_CASTLE) && (GameEngine.Instance.GameDisplayModeSubMode == GameEngine.GameDisplaySubModes.SUBMODE_BATTLE))
                {
                    return BattleSFXActive;
                }
                return sfxActive;
            }
        }

        private class DelayedSound
        {
            public DateTime playTime = DateTime.MaxValue;
            public string tag;
        }

        public class PlayList
        {
            public int currentLoop;
            public int currentStep = -1;
            public List<Sound.PlayListEntry> entries = new List<Sound.PlayListEntry>();
            public bool inSilence;
            public int nextStep;
            public bool playing;
            public bool random;
            public DateTime silenceEnd = DateTime.MinValue;

            public void addEntry(Sound.PlayListEntry entry)
            {
                if (entry != null)
                {
                    this.entries.Add(entry);
                }
            }

            public void advance()
            {
                if (!this.random)
                {
                    GameEngine.Instance.AudioEngine.playMp3(this.entries[this.nextStep].filename, this.entries[this.nextStep].volume, 0);
                    this.currentStep = this.nextStep;
                    this.nextStep++;
                    if (this.nextStep >= this.entries.Count)
                    {
                        this.nextStep = 0;
                    }
                }
                else
                {
                    this.nextStep = this.currentStep = new Random().Next(this.entries.Count);
                    GameEngine.Instance.AudioEngine.playMp3(this.entries[this.nextStep].filename, this.entries[this.nextStep].volume, 0);
                }
                this.currentLoop = 0;
            }

            public void play()
            {
                this.restart();
                this.advance();
            }

            public void restart()
            {
                this.currentStep = -1;
                this.nextStep = 0;
                this.inSilence = false;
                this.silenceEnd = DateTime.MinValue;
                this.currentLoop = 0;
            }

            public void stop()
            {
                GameEngine.Instance.AudioEngine.stopMp3(0);
            }

            public void update()
            {
                if (this.inSilence)
                {
                    if ((DateTime.Now > this.silenceEnd) && !Sound.musicPaused)
                    {
                        this.inSilence = false;
                        this.advance();
                    }
                }
                else if (!GameEngine.Instance.AudioEngine.isMP3Playing(0))
                {
                    if (this.entries[this.currentStep].numLoops <= this.currentLoop)
                    {
                        if (this.entries[this.currentStep].trailingSilenceSeconds > 0)
                        {
                            this.inSilence = true;
                            this.silenceEnd = DateTime.Now.AddSeconds((double) this.entries[this.currentStep].trailingSilenceSeconds);
                        }
                        else
                        {
                            this.advance();
                        }
                    }
                    else
                    {
                        this.currentLoop++;
                        GameEngine.Instance.AudioEngine.playMp3(this.entries[this.currentStep].filename, this.entries[this.currentStep].volume, 0);
                    }
                }
            }
        }

        public class PlayListEntry
        {
            public string filename;
            public int numLoops;
            public int trailingSilenceSeconds;
            public float volume;

            public PlayListEntry(string fn, float v, int silence)
            {
                this.filename = "";
                this.volume = 1f;
                this.filename = fn;
                this.volume = v;
                this.trailingSilenceSeconds = silence;
                this.numLoops = 0;
            }

            public PlayListEntry(string fn, float v, int silence, int loops)
            {
                this.filename = "";
                this.volume = 1f;
                this.filename = fn;
                this.volume = v;
                this.trailingSilenceSeconds = silence;
                this.numLoops = loops;
            }
        }
    }
}

