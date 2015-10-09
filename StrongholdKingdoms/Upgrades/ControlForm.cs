using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using Stronghold.AuthClient;
using CommonTypes;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

namespace Kingdoms
{
    public partial class ControlForm : Form
    {
        string[] researches;

        Thread tradeThread;
        bool trading = false;

        static bool autoLoot = true;

        Thread researchThread;
        Thread freecardThread;
        Thread updateVillagesThread;
        Thread repairThread;
        List<Thread> scoutThreads = new List<Thread>();

        Object Lock = new object();

        public void Log(string text)
        {
            text = "[" + DateTime.Now + "] " + text;
            Console.WriteLine(text);
            richTextBox_Log.Text = text + "\r\n" + richTextBox_Log.Text;

            try
            {
                if (!Directory.Exists("logs"))
                    Directory.CreateDirectory("logs");

                lock (Lock)
                    using (StreamWriter Writer = new StreamWriter(@".\logs\" + DateTime.Now.Ticks + ".log", true))
                        Writer.WriteLine(text);
            }
            catch (Exception ex)
            {
                WriteLog(ex);
            }
        }

        public ControlForm()
        {
            CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
            //this.Show();

            this.Text = "Tally-Ho " + DataExport.version;
            string[] path = Regex.Split(Application.StartupPath, @"\\");
            this.Text += " [" + path[path.Length - 1] + "]";
#if DEBUG
            Log("Form show");
#endif

            while (!GameEngine.Instance.World.isDownloadComplete())
            {
            }

            List<int> villageIDs = GameEngine.Instance.World.getListOfUserVillages(); // Получаем список наших деревень
            foreach (int villageID in villageIDs) // Перебираем их
            {
                string str = villageID + " - " + GameEngine.Instance.World.getVillageData(villageID).villageName;
                listBox_ActiveVillages.Items.Add(str);
                listBox_ActiveVillages.SetSelected(listBox_ActiveVillages.Items.Count - 1, true);
                listBox_scoutFrom.Items.Add(str);
                listBox_scoutFrom.SetSelected(listBox_scoutFrom.Items.Count - 1, true);
            }

            for (int i = 0; i < listBox_resForScouting.Items.Count; i++)
                listBox_resForScouting.SetSelected(i, true);

            //DownloadUserVillages();

                listBox_ResList.SelectedIndex = 0;
            //listBox_ResearchList.SelectedIndex = 0;

            updateVillagesThread = new Thread(DownloadUserVillages);
            updateVillagesThread.Start();

            ResearchInit();
            researchThread = new Thread(Research);
            researchThread.Start();

            freecardThread = new Thread(AutoLoot);
            freecardThread.Start();

            tradeThread = new Thread(Trade);
            tradeThread.Start();

            repairThread = new Thread(CastleRepair);
            repairThread.Start();

            foreach (int id in GameEngine.Instance.World.getListOfUserVillages())
            {
                Thread th = new Thread(() => Scout(id));
                th.Start();
                scoutThreads.Add(th);
                Console.WriteLine("Scout thread created for {0}", id);
            }

            // SpecialVillageTypes.getName(type, Program.mySettings.LanguageIdent)
            //GameEngine.Instance.getVillage;
            //GameEngine.Instance.World.getVillageData();
            //foreach (Kingdoms.WorldMap.VillageData data in GameEngine.Instance.World.villageList)
        }

        private void CastleRepair()
        {
            while (true)
            {
                try
                {
                    List<int> villageIDs = GameEngine.Instance.World.getListOfUserVillages();
                    foreach (int villageID in villageIDs)
                    {
                        // if (GameEngine.Instance.Castle != null)
                        //GameEngine this.castle = (CastleMap)this.castles[villageID];
                        //GameEngine.Instance.Castle.autoRepairCastle();
                        //RemoteServices.Instance.set_AutoRepairCastle_UserCallBack(new RemoteServices.AutoRepairCastle_UserCallBack(this.AutoRepairCastleCallback));
                        RemoteServices.Instance.AutoRepairCastle(villageID);
                    }
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Log(ex.ToString());
                }
            }
        }

        private void DownloadUserVillages()
        {
            bool isFirstDownload = true;

            while (true)
            {
                try
                {
                    int Secs = 0;
                    if (isFirstDownload)
                    {
                        Secs = 3;
                        isFirstDownload = false;
                    }
                    else Secs = int.Parse(textBox_VillageData_updateSleep.Text);

                    if (Secs == 0)
                        Secs = 1;
                    Thread.Sleep(Secs * 1000); // TODO: Fix dat madness or add timer

                    //GameEngine.Instance.GameDisplayMode = GameEngine.GameDisplays.DISPLAY_VILLAGE;
                    List<int> villageIDs = GameEngine.Instance.World.getListOfUserVillages();
                    foreach (int villageID in villageIDs)
                    {
                        //int villageID = GameEngine.Instance.World.getNextUserVillage(this.m_selectedMenuVillage, 1);
                        //if (villageID >= 0)
                        //    this.selectUserVillage(villageID, true);
                        //InterfaceMgr.Instance.selectUserVillage(villageID, true);
                        InterfaceMgr.Instance.setVillageNameBar(villageID);
                        GameEngine.Instance.forceDownloadCurrentVillage();

                        //GameEngine.Instance.getVillage(villageID);
                        //InterfaceMgr.Instance.selectUserVillage(villageID, false);
                        //GameEngine.Instance.downloadCurrentVillage();
                        //InterfaceMgr.Instance.runVillageInterface();
                        Thread.Sleep(3000);
                    }
                }
                catch (Exception ex)
                {
                    Log(ex.ToString());
                }
            }

            //InterfaceMgr.Instance.selectVillage(id)
            //int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
            //GameEngine.Instance.World.getNextUserVillage();
            //GameEngine.Instance.downloadCurrentVillage();
            //GameEngine.Instance.getVillage;
            //GameEngine.Instance.World.getVillageData();
        }

        private void button_Trade_Click(object sender, EventArgs e)
        {
            if (textBox_TradeTargetID.Text.Length > 0)
            {
                button_Trade.Text = (trading ? "Trade" : "Stop");
                trading = !trading;
            }
        }

        public void Research()
        {
            while (true)
            {
                try
                {
                    if (GameEngine.Instance.World.isDownloadComplete())
                    {
                        textBox_CurrentResearch.Text = CurrentResearch();
                        // If we have points
                        // Если есть очки исследований
                        if (listBox_Queue.Items.Count > 0)
                            if (GameEngine.Instance.World.userResearchData.research_points > 0)
                            {
                                // No researching in current time
                                // Если исследования в текущий момент не производятся
                                if (GameEngine.Instance.World.userResearchData.researchingType == -1)
                                {
                                    try
                                    {
                                        GameEngine.Instance.World.doResearch(GetID(listBox_Queue.Items[0].ToString()));

                                        TimeSpan span = GameEngine.Instance.World.userResearchData.calcResearchTime(GameEngine.Instance.World.userResearchData.research_pointCount, GameEngine.Instance.World.UserCardData, GameEngine.Instance.LocalWorldData);
                                        Log("Research " + listBox_Queue.Items[0].ToString() + " started!\nNext: " + span + " (" + DateTime.Now.Add(span) + ")");

                                        // Remove first item from queue cause it already in process
                                        // Удаляем первое исследование из очереди т.к. уже исследуем
                                        listBox_Queue.Items.RemoveAt(0);
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLog(ex);
                                    }
                                }
                            }
                            else
                            {
                                Log("No research points!");
                                listBox_Queue.Items.Clear(); // Чистим, дабы избежать спама в логи
                            }
                    }
                }
                catch (Exception ex)
                {
                    WriteLog(ex);
                }

                Thread.Sleep(5 * 1000); // 5 sec
            }
        }

        private string CurrentResearch()
        {
            if (GameEngine.Instance.World.userResearchData.researchingType != -1)
                return researches[GameEngine.Instance.World.userResearchData.researchingType];

            return "None";
        }

        private void WriteLog(Exception ex)
        {
            Console.WriteLine("\n======| EX INFO |======");
            Log(ex.ToString());
            Console.WriteLine("======| ======= |======\n");
        }

        private void Scout(int playerVillageID)
        {
            //int honourRange = CardTypes.adjustScoutingHonourRange(GameEngine.Instance.World.UserCardData, GameEngine.Instance.LocalWorldData.BaseScoutHonourRange);
            while (true)
            {
                if (!scout)
                {
                    Thread.Sleep(500);
                    continue;
                }

                try
                {
                    bool ok = false;
                    int index = 0;
                    foreach (string str in listBox_scoutFrom.Items) // Разведывать ли из этой деревни
                    {
                        int id = GetID(str);
                        if (id == playerVillageID)
                            if (listBox_scoutFrom.GetSelected(index))
                            {
                                ok = true;
                                break;
                            }
                        index++;
                    }
                    if (!ok)
                    {
                        Thread.Sleep(1000); // Нечего лаги пилить если вообще ничего не делаем
                        continue;
                    }

                    if (GameEngine.Instance.getVillage(playerVillageID) == null) // Если деревня игрока не прогружена - пропускаем
                    {
                        Thread.Sleep(3000); // Чтобы не лагать, деревня то вовсе не загружена
                        continue;
                    }

                    // TODO: возможно при отправке не всех сразу инфу надо будет обновлять
                    // чтобы оно не думало что скауты еще есть в наличии
                    VillageMap map = GameEngine.Instance.getVillage(playerVillageID);
                    if (map.calcTotalScoutsAtHome() == 0)
                    {
                        Thread.Sleep(500); // Чтобы не лагать, лучше переждать лишнего
                        continue;
                    }

                    double minRange = double.MaxValue;
                    bool isUnscouted = false;
                    int target = -1;

                    bool isContinue = false;
                    for (int i = 0; i < GameEngine.Instance.World.villageList.Length; i++) // Перебираем тайники
                    {
                        int id = GameEngine.Instance.World.villageList[i].id;
                        if (!GameEngine.Instance.World.isSpecial(id)) // Если это не тайник - пропускаем
                            continue;

                        int type = GameEngine.Instance.World.getSpecial(id); // 100 - unscouted
                        //if ((type >= 100) && (type <= 0xc7))
                        if (!DataExport.IsStash(type)) // Если не тайник с ресурсами - пропускаем
                            continue;

                        if (checkBox_Honourable.Checked && type == 100) // Если только не разведанные
                            if (!GameEngine.Instance.World.isScoutHonourOutOfRange(playerVillageID, id))
                                continue;

                        index = 0;
                        foreach (string str in listBox_resForScouting.Items) // Выбран ли тип ресурса для разведки
                        {
                            int typeID = GetID(str);
                            if (typeID == type)
                            {
                                if (!listBox_resForScouting.GetSelected(index))
                                    isContinue = true;
                                break;
                            }
                            index++;
                        }
                        if (isContinue) // Если этот тайник не выбран - пропускаем
                        {
                            isContinue = false;
                            continue;
                        }

                        // Считаем расстояние до тайника
                        Point playerVillage = GameEngine.Instance.World.getVillageLocation(playerVillageID);
                        Point targetStash = GameEngine.Instance.World.getVillageLocation(id);
                        int x = playerVillage.X;
                        int num7 = playerVillage.Y;
                        int num8 = targetStash.X;
                        int num9 = targetStash.Y;
                        double d = ((x - num8) * (x - num8)) + ((num7 - num9) * (num7 - num9)); // Квадрат расстояния
                        d = Math.Sqrt(d);
                        if (d > int.Parse(textBox_maxRadius.Text)) // Дальше чем положено нас не касается
                            continue;

                        if (type == 100) // не открытые тайники в приоритете
                            isUnscouted = true;
                        else if (isUnscouted) // Если это разведанный тайник, но уже был найден не разведанный
                            continue;
                        if (d < minRange) // && ((type == 100 && isUnscouted) || !isUnscouted))
                        {
                            minRange = d;
                            target = id;
                        }

                        //RemoteServices.Instance.set_SendScouts_UserCallBack(new RemoteServices.SendScouts_UserCallBack(this.sendScoutsCallback));
                        //d = Math.Sqrt(d) * ((GameEngine.Instance.LocalWorldData.ScoutsMoveSpeed * GameEngine.Instance.LocalWorldData.gamePlaySpeed) * ResearchData.ScoutTimes[GameEngine.Instance.World.UserResearchData.Research_Horsemanship]);
                        //d *= CardTypes.getScoutSpeed(GameEngine.Instance.World.UserCardData);
                        //string str = VillageMap.createBuildTimeString((int)d); // Time

                        //Console.WriteLine("WUT: " + id);
                        //Console.WriteLine("     | " + GameEngine.Instance.World.getVillageNameOrType(id));
                        //InterfaceMgr.Instance.SelectedVillage = id;
                        //GameEngine.Instance.World.setZooming(1, GameEngine.Instance.World.getVillageData(id).x, GameEngine.Instance.World.getVillageData(id).y);
                        //count++;
                    }

                    if (target == -1)
                    {
                        Thread.Sleep(1500); // Точек найдено не было
                        continue;
                    }

                    int numScouts = int.Parse(textBox_scoutsPerStash.Text);
                    if (numScouts == 0 || numScouts > map.calcTotalScoutsAtHome())
                        numScouts = map.calcTotalScoutsAtHome();

                    if (numScouts == 0) // вроде как непостижимый код
                    {
                        Thread.Sleep(500);
                        continue; // TODO: wait till != 0
                    }

                    RemoteServices.Instance.SendScouts(playerVillageID, target, numScouts);
                    AllVillagesPanel.travellersChanged();

                    Thread.Sleep(1500);
                }
                catch(Exception ex)
                {
                    Log(ex.ToString());
                }

                Thread.Sleep(1000);
            }

            //GameEngine.Instance.World.isScoutHonourOutOfRange(InterfaceMgr.Instance.OwnSelectedVillage, id);
            //InterfaceMgr.Instance.selectUserVillage(id)
            //InterfaceMgr.Instance.selectVillage(id)
            //int villageID = InterfaceMgr.Instance.getSelectedMenuVillage();
            //GameEngine.Instance.World.getNextUserVillage();
            //GameEngine.Instance.downloadCurrentVillage();
        }

        public void AutoLoot()
        {
            // TODO: fix crash
#if DEBUG
            Log("Trying to loot card");
#endif
            while (true)
            {
                if (autoLoot)
                {
                    //Log(GameEngine.Instance.World.FreeCardInfo.timeUntilNextFreeCard().TotalMilliseconds.ToString());
                    //if (GameEngine.Instance.World.FreeCardInfo.timeUntilNextFreeCard().TotalMilliseconds < 1000.0)
                    if (((GameEngine.Instance.World.FreeCardInfo.timeUntilNextFreeCard().TotalSeconds <= 0.0)
                        && GameEngine.Instance.World.FreeCardInfo.VeteranStages[0])
                        && (GameEngine.Instance.World.FreeCardInfo.CurrentVeteranLevel > 0))
                    {
                        // GameEngine.Instance.World.FreeCardInfo.
                        XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath).getFreeCard(new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", "")), new CardsEndResponseDelegate(FreeCardsPanel.revealCardCallback), this);
                        //InterfaceMgr.Instance.openFreeCardsPopup().freeCardsPanel.revealCard();
                        //InterfaceMgr.Instance.getFreeCardsPopup().freeCardsPanel.revealCard();
                        Log("Card looted at: " + DateTime.Now.ToString("HH:mm:ss"));
                    }

                }
                    //InterfaceMgr.Instance.getFreeCardsPopup().freeCardsPanel.Show();
                    //InterfaceMgr.Instance.getFreeCardsPopup().freeCardsPanel.revealCard();
                
                Thread.Sleep(500);
            }
        }

        public void Trade()
        {
#if DEBUG
            Log("Trade!");
#endif
            int sleep = 0;
            while (true)
            {
                try
                {
                    sleep = 8 + new Random().Next(-5, 5);

                    if (trading) // Если торгуем
                    {
                        Log("Step with \"" + listBox_ResList.SelectedItem.ToString() + "\"");
                        // Получаем ID товара из списка
                        int resID = GetID(listBox_ResList.SelectedItem.ToString());

                        int targetID = -1;
                        //List<int> villageIDs = GameEngine.Instance.World.getListOfUserVillages(); // Получаем список наших деревень

                        for (int i = 0; i < listBox_ActiveVillages.Items.Count; i++)
                        //foreach (int villageID in villageIDs) // Перебираем их
                        {
                            if (!listBox_ActiveVillages.GetSelected(i))
                                continue;
                            int villageID = GetID(listBox_ActiveVillages.Items[i].ToString());

                            // Если деревня прогружена (открывалась ее карта в текущей сессии хоть раз)
                            if (GameEngine.Instance.getVillage(villageID) != null)
                            {
                                // Получаем базовую информацию о нашей деревни
                                WorldMap.VillageData village = GameEngine.Instance.World.getVillageData(villageID);
                                VillageMap map = GameEngine.Instance.getVillage(villageID); // Получаем полную информацию
                                int merchantsCount = map.calcTotalTradersAtHome(); // Кол-во торговцев в ней
                                if (merchantsCount == 0)
                                    continue;
                                
                                int resAmount = (int)map.getResourceLevel(resID); // Кол-во ресурса на складе
                                Log("At village " + villageID + " (" + village.villageName + ") " + merchantsCount + " traders"); // Дебаг

                                int sendWithOne = int.Parse(textBox_ResCount.Text); // Кол-во ресурса на торговца
                                int maxAmount = merchantsCount * sendWithOne; // Кол-во ресурсов отправим
                                if (resAmount < maxAmount) // Если торговцы могут увезти больше чем есть
                                    merchantsCount = (int)(resAmount / sendWithOne); // Считаем сколько смогут увезти реально

                                if (merchantsCount > 0) // Если трейдеры дома есть
                                {
                                    if (radioButton1.Checked) // Parish
                                        targetID = GameEngine.Instance.World.getRegionCapitalVillage(village.regionID);
                                    else if (radioButton2.Checked) // Target
                                        targetID = int.Parse(textBox_TradeTargetID.Text);
                                    else if (radioButton3.Checked) // Resell
                                    {
                                        InterfaceMgr.Instance.selectStockExchange(-1);
                                        GameEngine.Instance.SkipVillageTab();
                                        InterfaceMgr.Instance.getMainTabBar().changeTab(1);
                                        InterfaceMgr.Instance.setVillageTabSubMode(3);
                                        InterfaceMgr.Instance.resetVillageReportPanelData();
                                        InterfaceMgr.Instance.selectStockExchange(int.Parse(listBox_ParishList.Items[0].ToString()));
                                    }


                                    // if target - player
                                    // GameEngine.Instance.getVillage(id).sendResources()
                                    // Вызываем высокоуровневую функцию торговли с рядом каллбеков
                                    GameEngine.Instance.getVillage(villageID).stockExchangeTrade(targetID, resID, merchantsCount * sendWithOne, false);
                                    AllVillagesPanel.travellersChanged(); // Подтверждаем изменения (ушли трейдеры) в GUI-клиента
                                }
                            }
                        }

                        Log("Again in " + sleep + " seconds - " + DateTime.Now.AddSeconds(sleep).ToString("HH:mm:ss"));
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    WriteLog(ex);
                }

                Thread.Sleep(sleep * 1000); // Спим, чтобы не спамить. Так меньше палева.
            }
        }

        public void ResearchInit()
        {
            researches = new string[listBox_ResearchList.Items.Count];
            foreach (string Item in listBox_ResearchList.Items)
                researches[GetID(Item)] = Item;
        }

        private void listBox_ResList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ResID = GetID(listBox_ResList.SelectedItem.ToString());

            switch (ResID)
            {
                // Wood and Stone
                case 6:
                case 7: textBox_ResCount.Text = "1000";
                    break;

                // Iron and Pitch
                case 8:
                case 9: textBox_ResCount.Text = "200";
                    break;

                // Ale
                case 12: textBox_ResCount.Text = "200";
                    break;

                // Food
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18: textBox_ResCount.Text = "500";
                    break;

                // Metalware, Clothes, Furniture, Venison
                case 19:
                case 21:
                case 22:
                case 26: textBox_ResCount.Text = "50";
                    break;

                // Salt, Salt (Not Silk?), Species, Wine
                case 23:
                case 24:
                case 25:
                case 33: textBox_ResCount.Text = "20";
                    break;

                // Weapons
                case 28:
                case 29:
                case 30:
                case 31:
                case 32: textBox_ResCount.Text = "5"; // 32 may be not valid (Catapults)
                    break;
            }
        }

        private int GetID(string Item)
        {
            return int.Parse(Item.Replace(" ", "").Split('-')[0]);
        }

        private void BotForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tradeThread.Abort();
            researchThread.Abort();
            freecardThread.Abort();
            updateVillagesThread.Abort();
            repairThread.Abort();

            foreach (var th in scoutThreads)
                th.Abort();
        }

        private void button_Exec_Click(object sender, EventArgs e)
        {
            if (richTextBox_In.Text.Length == 0 || !GameEngine.Instance.World.isDownloadComplete())
                return;

            richTextBox_Out.Text = "";

            // *** Example form input has code in a text box
            string lcCode = richTextBox_In.Text;

            ICodeCompiler loCompiler = new CSharpCodeProvider().CreateCompiler();
            CompilerParameters loParameters = new CompilerParameters();

            // *** Start by adding any referenced assemblies
            loParameters.ReferencedAssemblies.Add("System.dll");
            loParameters.ReferencedAssemblies.Add("System.Data.dll");
            loParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            loParameters.ReferencedAssemblies.Add("CommonTypes.dll");
            loParameters.ReferencedAssemblies.Add("Stronghold.WebService.dll");
            loParameters.ReferencedAssemblies.Add("StrongholdKingdoms.exe");

            // *** Must create a fully functional assembly as a string
            lcCode = @"using System;
            using System.IO;
            using System.Windows.Forms;
            using System.Collections.Generic;
            using System.Text;

            using Kingdoms;
            using CommonTypes;
            using Stronghold.AuthClient;

            namespace NSpace {
            public class NClass {
            public object DynamicCode(params object[] Parameters) 
            {
                " + lcCode +
            @" return null;
            }
            }
            }";

            // *** Load the resulting assembly into memory
            loParameters.GenerateInMemory = false;
            // *** Now compile the whole thing
            CompilerResults loCompiled =
                    loCompiler.CompileAssemblyFromSource(loParameters, lcCode);
            if (loCompiled.Errors.HasErrors)
            {
                string lcErrorMsg = "";
                lcErrorMsg = loCompiled.Errors.Count.ToString() + " Errors:";
                for (int x = 0; x < loCompiled.Errors.Count; x++)
                    lcErrorMsg = lcErrorMsg + "\r\nLine: " +
                                 loCompiled.Errors[x].Line.ToString() + " - " +
                                 loCompiled.Errors[x].ErrorText;

                richTextBox_Out.Text = lcErrorMsg + "\r\n\r\n" + lcCode;
                return;
            }

            Assembly loAssembly = loCompiled.CompiledAssembly;
            // *** Retrieve an obj ref – generic type only
            object loObject = loAssembly.CreateInstance("NSpace.NClass");

            if (loObject == null)
            {
                richTextBox_Out.Text = "Couldn't load class.";
                return;
            }

            object[] loCodeParms = new object[1];
            loCodeParms[0] = "SHKBot";
            try
            {
                object loResult = loObject.GetType().InvokeMember(
                                 "DynamicCode", BindingFlags.InvokeMethod,
                                 null, loObject, loCodeParms);

                //DateTime ltNow = (DateTime)loResult;
                if (loResult != null)
                    richTextBox_Out.Text = "Method Call Result:\r\n\r\n" + loResult.ToString();
            }
            catch (Exception ex)
            {
                WriteLog(ex);
            }
        }

        private void listBox_ResearchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox_Queue.Items.Add(listBox_ResearchList.SelectedItem);
        }

        private void button_FreeCardAutoLoot_Click(object sender, EventArgs e)
        {
            try
            {
                autoLoot = !autoLoot;
                button_FreeCardAutoLoot.Text = "AutoLoot " + (autoLoot ? "Enabled" : "Disabled");
            }
            catch (Exception ex)
            {
                WriteLog(ex);
            }
        }

        private void listBox_Queue_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Here is exception. May be recursive remove items. Dont know how to fix. 
                listBox_Queue.Items.RemoveAt(listBox_Queue.SelectedIndex);
            }
            catch { }
        }

        private void button_buildingDump_Click(object sender, EventArgs e)
        {
            DataExport.dumpBuildingMap(InterfaceMgr.Instance.getSelectedMenuVillage());
        }

        private void checkBox_premium_CheckedChanged(object sender, EventArgs e)
        {
            DataExport.premium = checkBox_premium.Checked;
        }

        private void listBox_ParishList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                listBox_ParishList.Items.RemoveAt(listBox_ParishList.SelectedIndex);
        }

        private void listBox_ParishList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_ParishList.SelectedIndex == -1)
                return;
            int ID = GetID(listBox_ParishList.Items[listBox_ParishList.SelectedIndex].ToString());
            textBox_TradeTargetID.Text = ID.ToString();

            //GameEngine.Instance.World.(ID);
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            listBox_ParishList.Items.Add(textBox_TradeTargetID.Text + " - "
                + GameEngine.Instance.World.getParishName(int.Parse(textBox_TradeTargetID.Text)));
        }

        private void button_StashDump_Click(object sender, EventArgs e)
        {
#if DEBUG
            int count = 0;
            for (int i = 0; i < GameEngine.Instance.World.villageList.Length; i++)
            {
                int id = GameEngine.Instance.World.villageList[i].id;
                if (!GameEngine.Instance.World.isSpecial(id))
                    continue;
                int type = GameEngine.Instance.World.getSpecial(id); // 100 - unscouted
                if (DataExport.IsStash(type))
                {
                    Console.WriteLine("WUT: " + id);
                    Console.WriteLine("     | " + GameEngine.Instance.World.getVillageNameOrType(id));
                    //InterfaceMgr.Instance.SelectedVillage = id;
                    //GameEngine.Instance.World.setZooming(1, GameEngine.Instance.World.getVillageData(id).x, GameEngine.Instance.World.getVillageData(id).y);
                    count++;
                }
            }
            Console.WriteLine(count);
#endif
        }

        private void button_removeTarget_Click(object sender, EventArgs e)
        {
            listBox_ParishList.Items.RemoveAt(listBox_ParishList.SelectedIndex);
        }

        bool scout = false;
        private void button_scout_Click(object sender, EventArgs e)
        {
            button_scout.Text = (scout ? "Scout" : "Stop");
            scout = !scout;
        }

        private void label_Site_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(label_Site.Text);
        }

        private void button_highest_Click(object sender, EventArgs e)
        {

        }

        //private void LookForHighest()
        //{
        //            this.closeCapitalsToTest.Clear();
        //            this.closeCapitalsToTest.Add(villageID);
        //            list.Sort((Comparison<ClosestCapitalSortItem>) ((a, b) => a.distance.CompareTo(b.distance)));
        //            if (list.Count > 20)
        //            {
        //                list.RemoveRange(20, list.Count - 20);
        //            }
        //            List<int> list3 = new List<int>();
        //            foreach (ClosestCapitalSortItem item2 in list)
        //            {
        //                this.closeCapitalsToTest.Add(item2.villageID);
        //                bool flag2 = true;
        //                if (this.stockExchanges[item2.villageID] != null)
        //}
    }
}
