namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using StatTracking;
    using Stronghold.AuthClient;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    public class PlayCardsWindow : Form
    {
        private static string bigpointURL = string.Empty;
        private BuyCardsPanel cardPanelBuy = new BuyCardsPanel();
        public ManageCardsPanel cardPanelManage = new ManageCardsPanel();
        private PlayCardsPanel cardPanelPlay = new PlayCardsPanel();
        private PremiumCardsPanel cardPanelPremium = new PremiumCardsPanel();
        private ViewAllCardsPanel cardPanelViewAll = new ViewAllCardsPanel();
        private bool closing;
        private IContainer components;
        private BuyCrownsPanel crownsBuyPanel = new BuyCrownsPanel();
        public static bool CrownsOpened = false;
        private int currentCardSection;
        private CustomSelfDrawPanel currentPanel;
        private int currentPanelID;
        private static DateTime lastCrownsOpened = DateTime.MinValue;
        private static DateTime lastUpdatedBPURL = DateTime.MinValue;
        private bool m_fromOpen;
        private static DateTime m_lastRewardCardsCall = DateTime.MinValue;
        private bool processTextChanged = true;
        public TextBox tbSearchBox;

        public PlayCardsWindow()
        {
            this.currentPanel = this.cardPanelPlay;
            this.currentPanelID = 1;
            this.InitializeComponent();
            this.tbSearchBox.Font = FontManager.GetFont("Arial", 9.75f, FontStyle.Regular);
            this.tbSearchBox.Parent.Controls.Remove(this.tbSearchBox);
            this.currentPanel.Controls.Add(this.tbSearchBox);
            this.processTextChanged = false;
            this.tbSearchBox.Text = "";
            this.processTextChanged = true;
            this.tbSearchBox.Visible = false;
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            base.TransparencyKey = Color.FromArgb(0xff, 0xff, 0, 0xff);
            this.BackColor = base.TransparencyKey;
            if (!GameEngine.Instance.World.TutorialIsAdvancing())
            {
                if (GameEngine.Instance.World.getTutorialStage() == 8)
                {
                    GameEngine.Instance.World.checkQuestObjectiveComplete(7);
                }
                if (GameEngine.Instance.World.getTutorialStage() == 12)
                {
                    GameEngine.Instance.World.checkQuestObjectiveComplete(11);
                }
                if (GameEngine.Instance.World.getTutorialStage() == 0x66)
                {
                    GameEngine.Instance.World.checkQuestObjectiveComplete(13);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void fireURLError()
        {
            MyMessageBox.Show("Stronghold Kingdoms encountered an error when trying to " + Environment.NewLine + "open your system's Default Web Browser. Please check that " + Environment.NewLine + "your web browser is working correctly and there are no unresponsive " + Environment.NewLine + "copies showing in task manager->Processes and then try again." + Environment.NewLine + "If this problem persists, please contact support.", "Error opening Web Browser");
        }

        public void GetBigpointURL()
        {
            XmlRpcBPProvider provider = XmlRpcBPProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressBigpoint, URLs.ProfileServerPort, URLs.ProfileBPPath);
            XmlRpcBPRequest req = new XmlRpcBPRequest {
                SessionID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""),
                UserGUID = RemoteServices.Instance.UserGuid.ToString().Replace("-", "")
            };
            provider.GetPaymentURL(req, new BPEndResponseDelegate(this.GetbigpointURLCallback), this);
        }

        public void GetbigpointURLCallback(IBPProvider provider, IBPResponse response)
        {
            if (response.SuccessCode != 1)
            {
                MyMessageBox.Show("");
            }
            else
            {
                lastUpdatedBPURL = DateTime.Now;
                bigpointURL = response.URL;
                try
                {
                    if (bigpointURL.Length > 0)
                    {
                        Process.Start(bigpointURL);
                    }
                }
                catch (Exception)
                {
                    this.fireURLError();
                }
            }
        }

        public void GetCrowns()
        {
            CrownsOpened = true;
            if (Program.steamActive || Program.aeriaInstall)
            {
                this.SwitchPanel(7);
            }
            else
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - lastCrownsOpened);
                if (span.TotalSeconds >= 5.0)
                {
                    lastCrownsOpened = DateTime.Now;
                    if (GameEngine.Instance.World.isBigpointAccount || Program.bigpointPartnerInstall)
                    {
                        TimeSpan span2 = (TimeSpan) (DateTime.Now - lastUpdatedBPURL);
                        if ((bigpointURL == string.Empty) || (span2.TotalMinutes >= 2.0))
                        {
                            this.GetBigpointURL();
                        }
                        else
                        {
                            try
                            {
                                Process.Start(bigpointURL);
                            }
                            catch (Exception)
                            {
                                this.fireURLError();
                            }
                        }
                    }
                    else
                    {
                        string fileName = (URLs.ProfilePaymentURL + "?u=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&s=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "")) + "&lang=" + Program.mySettings.LanguageIdent.ToLower();
                        if (Program.arcInstall)
                        {
                            Program.arc_openURL("https://billing.arcgames.com/" + Program.mySettings.languageIdent);
                            InterfaceMgr.Instance.closePlayCardsWindow();
                            TutorialPanel.minimizeTutorial();
                        }
                        else
                        {
                            if (Program.gamersFirstInstall)
                            {
                                fileName = (URLs.ProfileGamersFirstPaymentURL + "?shk_userguid=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&shk_sessionguid=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "")) + "&lang=" + Program.mySettings.LanguageIdent.ToLower();
                            }
                            try
                            {
                                Process.Start(fileName);
                            }
                            catch (Exception)
                            {
                                this.fireURLError();
                            }
                        }
                    }
                }
            }
        }

        public void GetCrownsCallback(ICardsProvider provider, ICardsResponse response)
        {
        }

        public string getNameSearchText()
        {
            if (this.tbSearchBox.Visible)
            {
                return this.tbSearchBox.Text;
            }
            return "";
        }

        private void getRewardcardsCallback(ICardsProvider sender, ICardsResponse response)
        {
            m_lastRewardCardsCall = DateTime.Now;
            foreach (int num in response.Cards.Keys)
            {
                if (!GameEngine.Instance.World.ProfileCards.ContainsKey(num))
                {
                    GameEngine.Instance.World.addProfileCard(num, response.Cards[num]);
                    GameEngine.Instance.World.ProfileCards[num].rewardcard = true;
                    GameEngine.Instance.World.ProfileCards[num].worldID = RemoteServices.Instance.ProfileWorldID;
                }
            }
            if (response.Cardpoints.HasValue)
            {
                GameEngine.Instance.World.FakeCardPoints = response.Cardpoints.Value;
            }
            ((CustomSelfDrawPanel.ICardsPanel) this.currentPanel).init(this.currentCardSection);
        }

        public void init(int cardSection, bool fromOpen)
        {
            this.m_fromOpen = fromOpen;
            this.currentCardSection = cardSection;
            int num = 180;
            if (CrownsOpened)
            {
                num = 30;
            }
            if (DateTime.Now.Subtract(GameEngine.Instance.World.LastUpdatedCrowns).TotalSeconds > num)
            {
                this.UpdateCrowns();
            }
            if ((this.m_fromOpen && GameEngine.Instance.World.isTutorialActive()) && (DateTime.Now.Subtract(m_lastRewardCardsCall).TotalSeconds > 600.0))
            {
                this.UpdateRewardCards();
            }
            ((CustomSelfDrawPanel.ICardsPanel) this.currentPanel).init(cardSection);
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(PlayCardsWindow));
            this.currentPanel = new PlayCardsPanel();
            this.tbSearchBox = new TextBox();
            base.SuspendLayout();
            this.currentPanel.Location = new Point(0, 0);
            this.currentPanel.Name = "cardPanel";
            this.currentPanel.Size = new Size(0x3e8, 600);
            this.currentPanel.StoredGraphics = null;
            this.currentPanel.TabIndex = 0;
            this.tbSearchBox.Location = new Point(770, 7);
            this.tbSearchBox.Name = "tbSearchBox";
            this.tbSearchBox.Size = new Size(160, 20);
            this.tbSearchBox.TabIndex = 1;
            this.tbSearchBox.TextChanged += new EventHandler(this.tbSearchBox_TextChanged);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x3e8, 600);
            base.ControlBox = false;
            base.Controls.Add(this.currentPanel);
            base.Controls.Add(this.tbSearchBox);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = Resources.shk_icon;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "PlayCardsWindow";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "PlayCardsWindow";
            base.FormClosing += new FormClosingEventHandler(this.PlayCardsWindow_FormClosing);
            base.ResumeLayout(false);
        }

        public void InviteAFriend()
        {
            string fileName = (URLs.InviteAFriendURL + "?u=" + RemoteServices.Instance.UserGuid.ToString().Replace("-", "") + "&s=" + RemoteServices.Instance.SessionGuid.ToString().Replace("-", "")) + "&lang=" + Program.mySettings.LanguageIdent.ToLower();
            try
            {
                Process.Start(fileName);
            }
            catch (Exception)
            {
                MyMessageBox.Show(SK.Text("ERROR_Browser1", "Stronghold Kingdoms encountered an error when trying to open your system's Default Web Browser. Please check that your web browser is working correctly and there are no unresponsive copies showing in task manager->Processes and then try again.") + Environment.NewLine + Environment.NewLine + SK.Text("ERROR_Browser2", "If this problem persists, please contact support."), SK.Text("ERROR_Browser3", "Error opening Web Browser"));
            }
        }

        public bool isCardWindowOnManage()
        {
            return (this.currentPanel == this.cardPanelManage);
        }

        public bool isCardWindowOnPremium()
        {
            return (this.currentPanel == this.cardPanelPremium);
        }

        public static void logout()
        {
            CrownsOpened = false;
            resetRewardCardTimer();
        }

        public void performSearch()
        {
            try
            {
                if (this.currentPanelID == 1)
                {
                    ((PlayCardsPanel) this.currentPanel).forceSearch();
                }
            }
            catch (Exception)
            {
            }
        }

        private void PlayCardsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && !this.closing)
            {
                this.closing = true;
                StatTrackingClient.Instance().ActivateTrigger(0x15, null);
                InterfaceMgr.Instance.closePlayCardsWindow();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Program.arcInstall && (keyData == (Keys.Shift | Keys.Tab)))
            {
                Program.arc_forceoverlay();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void reactivatePanel()
        {
            this.currentPanel.PanelActive = true;
        }

        public static void resetRewardCardTimer()
        {
            m_lastRewardCardsCall = DateTime.MinValue;
        }

        public void SetCardSection(int cardSection)
        {
            this.currentCardSection = cardSection;
        }

        public void SwitchPanel(int panel)
        {
            this.cardPanelPlay.clearControls();
            this.cardPanelBuy.clearControls();
            this.cardPanelManage.clearControls();
            this.cardPanelPremium.clearControls();
            this.cardPanelViewAll.clearControls();
            this.crownsBuyPanel.clearControls();
            if (panel == this.currentPanelID)
            {
                panel = 1;
            }
            switch (panel)
            {
                case 1:
                    this.currentPanel = this.cardPanelPlay;
                    break;

                case 2:
                    this.currentPanel = this.cardPanelBuy;
                    break;

                case 4:
                    this.currentPanel = this.cardPanelPremium;
                    break;

                case 6:
                    this.currentPanel = this.cardPanelManage;
                    break;

                case 7:
                    this.currentPanel = this.crownsBuyPanel;
                    break;

                case 8:
                    this.currentPanel = this.cardPanelViewAll;
                    break;

                default:
                    this.currentPanel = this.cardPanelPlay;
                    break;
            }
            this.tbSearchBox.Parent.Controls.Remove(this.tbSearchBox);
            this.currentPanel.Controls.Add(this.tbSearchBox);
            this.processTextChanged = false;
            this.tbSearchBox.Text = "";
            this.processTextChanged = true;
            this.tbSearchBox.Visible = false;
            this.currentPanelID = panel;
            new ComponentResourceManager(this.currentPanel.GetType());
            base.SuspendLayout();
            this.currentPanel.Location = new Point(0, 0);
            this.currentPanel.Name = "cardPanel";
            this.currentPanel.Size = new Size(0x3e8, 600);
            this.currentPanel.StoredGraphics = null;
            this.currentPanel.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x3e8, 600);
            base.ControlBox = false;
            base.Controls.Clear();
            base.Controls.Add(this.currentPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "PlayCardsWindow";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "PlayCardsWindow";
            base.ResumeLayout(false);
            this.init(this.currentCardSection, false);
        }

        public void SwitchToManageAndFilter(int filter, int cardType)
        {
            this.SwitchPanel(6);
            this.cardPanelManage.setFilter(filter);
            this.cardPanelManage.SwitchToBuy();
            this.cardPanelManage.addCardToCard(cardType, false);
        }

        private void tbSearchBox_TextChanged(object sender, EventArgs e)
        {
            if (this.processTextChanged && this.tbSearchBox.Visible)
            {
                try
                {
                    if (this.currentPanelID == 1)
                    {
                        ((PlayCardsPanel) this.currentPanel).handleSearchTextChanged();
                    }
                    if (this.currentPanelID == 6)
                    {
                        ((ManageCardsPanel) this.currentPanel).handleSearchTextChanged();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public static string translateCardError(string message, int cardType)
        {
            return translateCardError(message, cardType, -1);
        }

        public static string translateCardError(string message, int cardType, int altMethod)
        {
            SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play");
            if (message.Contains("More than one of this card (or this type of card) may not be played at the same time."))
            {
                return (SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_17", "More than one of this card (or this type of card) may not be played at the same time."));
            }
            if (message.Contains("Troop type not researched.") || (altMethod == 5))
            {
                return (SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_18", "Troop type not researched."));
            }
            if (message.Contains("Not enough space in the barracks for those troops.") || (altMethod == 1))
            {
                return (SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_15", "Not enough space in the barracks for those troops."));
            }
            if (message.Contains("No Room for Merchants.") || (altMethod == 4))
            {
                return (SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_19", "No Room for Merchants."));
            }
            if (message.Contains("No walls under construction."))
            {
                return (SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_20", "No walls under construction."));
            }
            if (message.Contains("No moat under construction"))
            {
                return (SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_21", "No moat under construction"));
            }
            if (message.Contains("No pits under construction"))
            {
                return (SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_22", "No pits under construction"));
            }
            if (message.Contains("No room for Monks") || (altMethod == 3))
            {
                return (SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_23", "No room for Monks"));
            }
            if (message.Contains("No room for Scouts") || (altMethod == 2))
            {
                return (SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_24", "No room for Scouts"));
            }
            if (message.Contains("Nothing under construction"))
            {
                return (SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_25", "Nothing under construction"));
            }
            if (message.Contains("No current building queue"))
            {
                return (SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_12", "No current building queue"));
            }
            if (message.Contains("No current Research"))
            {
                return (SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_11", "No current Research"));
            }
            if (message.Contains("Premium card already in play"))
            {
                return SK.Text("RETURNED_CARD_ERROR_6", "Premium token already in play");
            }
            if (message.Contains("Player Rank too low"))
            {
                return (SK.Text("RETURNED_CARD_ERROR_BASE", "Could not play") + Environment.NewLine + Environment.NewLine + CardTypes.getDescriptionFromCard(cardType) + Environment.NewLine + Environment.NewLine + SK.Text("RETURNED_CARD_ERROR_8", "Player Rank too low"));
            }
            return message;
        }

        public void update()
        {
            ((CustomSelfDrawPanel.ICardsPanel) this.currentPanel).update();
        }

        public void UpdateCrowns()
        {
            XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath).getCrowns(new XmlRpcCardsRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", "")), new CardsEndResponseDelegate(this.UpdateCrownsCallback), this);
        }

        public void UpdateCrownsCallback(ICardsProvider provider, ICardsResponse response)
        {
            if (response.SuccessCode.Value == 1)
            {
                GameEngine.Instance.World.ProfileCrowns = response.Crowns.Value;
                GameEngine.Instance.World.LastUpdatedCrowns = DateTime.Now;
                GameEngine.Instance.World.ProfileUserCardPacks = response.UserCardPacks;
            }
        }

        public void UpdateRewardCards()
        {
            ICardsProvider provider = XmlRpcCardsProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfileCardPath);
            ICardsRequest req = new XmlRpcCardsRequest {
                UserGUID = RemoteServices.Instance.UserGuid.ToString("N"),
                WorldID = RemoteServices.Instance.ProfileWorldID.ToString()
            };
            ((XmlRpcCardsProvider) provider).getRewardCards(req, new CardsEndResponseDelegate(this.getRewardcardsCallback), this);
        }

        public ManageCardsPanel CardPanelManage
        {
            get
            {
                return this.cardPanelManage;
            }
        }

        public int CurrentPanelID
        {
            get
            {
                return this.currentPanelID;
            }
        }
    }
}

