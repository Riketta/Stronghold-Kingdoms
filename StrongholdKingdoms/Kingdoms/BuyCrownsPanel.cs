namespace Kingdoms
{
    using CommonTypes;
    using Stronghold.AuthClient;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class BuyCrownsPanel : CustomSelfDrawPanel, CustomSelfDrawPanel.ICardsPanel
    {
        private CustomSelfDrawPanel.CSDImage APImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDExtendingPanel AvailablePanel;
        private CustomSelfDrawPanel.CSDImage AvailablePanelContent = new CustomSelfDrawPanel.CSDImage();
        private int AvailablePanelWidth;
        private static int BorderPadding = 0x10;
        private CustomSelfDrawPanel.CSDButton buyAPButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage buybutton = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.UICardsButtons cardButtons;
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage closeImage = new CustomSelfDrawPanel.CSDImage();
        private IContainer components;
        private int ContentWidth;
        private CustomSelfDrawPanel.CSDImage crownsbutton = new CustomSelfDrawPanel.CSDImage();
        private int currentCardSection = -1;
        private CustomSelfDrawPanel.CSDFill greyout = new CustomSelfDrawPanel.CSDFill();
        private CustomSelfDrawPanel.CSDLabel labelBottom = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel labelFeedback = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel labelPoints = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel labelTitle = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage managebutton = new CustomSelfDrawPanel.CSDImage();
        private Dictionary<string, CustomSelfDrawPanel.UICardPack> packControls = new Dictionary<string, CustomSelfDrawPanel.UICardPack>();
        private CustomSelfDrawPanel.CSDImage playbutton = new CustomSelfDrawPanel.CSDImage();
        private string PlayerCountry;
        private string PlayerCurrency;
        private string PlayerLanguage;
        private CustomSelfDrawPanel.CSDImage premiumbutton = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDVertScrollBar scrollbarAvailable = new CustomSelfDrawPanel.CSDVertScrollBar();
        private int storedAeriaPoints;
        private string strCrowns = SK.Text("BuyCrownsPanel_Crowns", "Crowns");
        private string strOrderNow = SK.Text("BuyCrownsPanel_Order_Now", "Order Now");

        public BuyCrownsPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public void closeClick()
        {
            InterfaceMgr.Instance.closePlayCardsWindow();
            InterfaceMgr.Instance.ParentForm.TopMost = true;
            InterfaceMgr.Instance.ParentForm.TopMost = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(int cardSection)
        {
            CustomSelfDrawPanel.CSDImage image2;
            this.currentCardSection = cardSection;
            base.clearControls();
            this.mainBackgroundImage.Image = GFXLibrary.dummy;
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = base.Size;
            this.mainBackgroundImage.Tile = true;
            base.addControl(this.mainBackgroundImage);
            this.ContentWidth = base.Width - (2 * BorderPadding);
            this.AvailablePanelWidth = 800;
            CustomSelfDrawPanel.CSDExtendingPanel control = new CustomSelfDrawPanel.CSDExtendingPanel {
                Size = base.Size,
                Position = new Point(0, 0)
            };
            this.mainBackgroundImage.addControl(control);
            control.Create((Image) GFXLibrary.cardpanel_panel_back_top_left, (Image) GFXLibrary.cardpanel_panel_back_top_mid, (Image) GFXLibrary.cardpanel_panel_back_top_right, (Image) GFXLibrary.cardpanel_panel_back_mid_left, (Image) GFXLibrary.cardpanel_panel_back_mid_mid, (Image) GFXLibrary.cardpanel_panel_back_mid_right, (Image) GFXLibrary.cardpanel_panel_back_bottom_left, (Image) GFXLibrary.cardpanel_panel_back_bottom_mid, (Image) GFXLibrary.cardpanel_panel_back_bottom_right);
            CustomSelfDrawPanel.CSDImage image = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.cardpanel_panel_gradient_top_left,
                Size = GFXLibrary.cardpanel_panel_gradient_top_left.Size,
                Position = new Point(0, 0)
            };
            control.addControl(image);
            image2 = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.cardpanel_panel_gradient_bottom_right,
                Size = GFXLibrary.cardpanel_panel_gradient_bottom_right.Size,
                Position = new Point((control.Width - ((Image) GFXLibrary.cardpanel_panel_gradient_bottom_right).Width) - 6, (control.Height - ((Image) GFXLibrary.cardpanel_panel_gradient_bottom_right).Height) - 6)
            };
            control.addControl(image2);
            this.AvailablePanel = new CustomSelfDrawPanel.CSDExtendingPanel();
            this.AvailablePanel.Size = new Size(this.AvailablePanelWidth, 550);
            this.AvailablePanel.Position = new Point(8, (base.Height - 8) - 550);
            this.AvailablePanel.Alpha = 0.8f;
            int width = base.Width;
            int borderPadding = BorderPadding;
            int num9 = this.AvailablePanel.Width;
            this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal;
            this.closeImage.Size = this.closeImage.Image.Size;
            this.closeImage.setMouseOverDelegate(() => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_over, () => this.closeImage.Image = (Image) GFXLibrary.cardpanel_button_close_normal);
            this.closeImage.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick));
            this.closeImage.Position = new Point((base.Width - 14) - 0x11, 10);
            this.mainBackgroundImage.addControl(this.closeImage);
            CustomSelfDrawPanel.WikiLinkControl.init(this.mainBackgroundImage, 0x26, new Point((((base.Width - 1) - 0x11) - 50) + 3, 5), true);
            CustomSelfDrawPanel.CSDFill fill = new CustomSelfDrawPanel.CSDFill {
                FillColor = Color.FromArgb(0xff, 130, 0x81, 0x7e),
                Size = new Size(base.Width - 10, 1),
                Position = new Point(5, 0x22)
            };
            this.mainBackgroundImage.addControl(fill);
            this.greyout.FillColor = Color.FromArgb(0xd7, 0x19, 0x19, 0x19);
            this.greyout.Size = new Size(this.mainBackgroundImage.Width, this.AvailablePanel.Y + this.AvailablePanel.Height);
            this.greyout.Position = new Point(0, 0);
            this.greyout.setClickDelegate(delegate {
            });
            CustomSelfDrawPanel.CSDImage closeGrey = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.cardpanel_button_close_normal,
                Size = this.closeImage.Image.Size
            };
            closeGrey.setMouseOverDelegate(() => closeGrey.Image = (Image) GFXLibrary.cardpanel_button_close_over, () => closeGrey.Image = (Image) GFXLibrary.cardpanel_button_close_normal);
            closeGrey.Position = new Point((base.Width - 14) - 0x11, 10);
            this.greyout.addControl(closeGrey);
            this.labelTitle.Position = new Point(0x1b, 8);
            this.labelTitle.Size = new Size(0x3a7, 0x40);
            this.labelTitle.Text = SK.Text("BuyCrownsPanel_Buy_Crowns", "Buy Crowns");
            this.labelTitle.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.labelTitle.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
            this.labelTitle.Color = ARGBColors.Black;
            this.mainBackgroundImage.addControl(this.labelTitle);
            CustomSelfDrawPanel.UICardsButtons buttons = new CustomSelfDrawPanel.UICardsButtons((PlayCardsWindow) base.ParentForm) {
                Position = new Point(0x328, 0x25)
            };
            this.mainBackgroundImage.addControl(buttons);
            this.cardButtons = buttons;
            List<ProductInfo> productList = new List<ProductInfo>();
            if (Program.steamActive)
            {
                this.PlayerCountry = "UK";
                this.PlayerCurrency = "GBP";
                this.PlayerLanguage = MySettings.load().LanguageIdent;
                XmlRpcAuthProvider provider = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
                XmlRpcAuthRequest req = new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", null, null, null, null) {
                    SteamID = Program.steamID,
                    SessionID = RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""),
                    Culture = this.PlayerLanguage,
                    Currency = this.PlayerCurrency,
                    Country = this.PlayerCountry
                };
                productList = provider.SteamGetProductList(req, null, this, 0x3a98).ProductList;
            }
            else if (Program.aeriaInstall)
            {
                XmlRpcAuthProvider provider2 = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressLogin, URLs.ProfileServerPort, URLs.ProfilePath);
                XmlRpcAuthRequest request2 = new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", null, null, null, null);
                XmlRpcAuthResponse response = null;
                this.storedAeriaPoints = provider2.AeriaGetBalance(request2, null, this, 0x3a98, ref response);
                productList = response.ProductList;
                this.buyAPButton.ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal;
                this.buyAPButton.ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over;
                this.buyAPButton.ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed;
                this.buyAPButton.Position = new Point(0x13d, 0x49);
                this.buyAPButton.Text.Text = this.storedAeriaPoints.ToString();
                this.buyAPButton.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT;
                this.buyAPButton.Text.Size = new Size((this.buyAPButton.Width / 2) - 3, this.buyAPButton.Height);
                this.buyAPButton.TextYOffset = -2;
                this.buyAPButton.Text.Color = ARGBColors.Black;
                this.buyAPButton.ImageIcon = (Image) GFXLibrary.aeriaPoints;
                this.buyAPButton.ImageIconPosition = new Point((this.buyAPButton.Width / 2) + 3, 1);
                this.buyAPButton.Text.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
                this.buyAPButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.purchaseAP));
                this.buyAPButton.CustomTooltipID = 0x286e;
                this.mainBackgroundImage.addControl(this.buyAPButton);
            }
            int y = 0x42;
            int num2 = 0x5e;
            int num3 = -1;
            if (Program.aeriaInstall)
            {
                y = 0x84;
            }
            NumberFormatInfo nFI = GameEngine.NFI;
            NumberFormatInfo info2 = GameEngine.NFI_D2;
            foreach (ProductInfo info3 in productList)
            {
                num3++;
                int x = num2;
                if (Program.steamActive)
                {
                    if (num3 > 3)
                    {
                        if (num3 == 4)
                        {
                            y = 0x84;
                        }
                        x += 350;
                    }
                }
                else if (Program.aeriaInstall && (num3 > 2))
                {
                    if (num3 == 3)
                    {
                        y = 0x84;
                    }
                    x += 350;
                }
                CustomSelfDrawPanel.CSDLabel label = new CustomSelfDrawPanel.CSDLabel();
                CustomSelfDrawPanel.CSDLabel label2 = new CustomSelfDrawPanel.CSDLabel();
                CustomSelfDrawPanel.CSDLabel label3 = new CustomSelfDrawPanel.CSDLabel();
                CustomSelfDrawPanel.CSDLabel label4 = new CustomSelfDrawPanel.CSDLabel();
                CustomSelfDrawPanel.CSDLabel label5 = new CustomSelfDrawPanel.CSDLabel();
                CustomSelfDrawPanel.CSDImage image3 = new CustomSelfDrawPanel.CSDImage();
                CustomSelfDrawPanel.CSDImage crownsbutton = new CustomSelfDrawPanel.CSDImage();
                CustomSelfDrawPanel.CSDImage orderbutton = new CustomSelfDrawPanel.CSDImage();
                crownsbutton.Image = (Image) GFXLibrary.cardpanel_payment_button_crowns_normal;
                crownsbutton.Position = new Point(x, y);
                crownsbutton.Height = crownsbutton.Image.Height;
                crownsbutton.Width = crownsbutton.Image.Width;
                crownsbutton.setMouseOverDelegate(() => crownsbutton.Image = (Image) GFXLibrary.cardpanel_payment_button_crowns_over, () => crownsbutton.Image = (Image) GFXLibrary.cardpanel_payment_button_crowns_normal);
                crownsbutton.Tag = info3;
                crownsbutton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.productclick));
                this.mainBackgroundImage.addControl(crownsbutton);
                x += crownsbutton.Width + 0x20;
                orderbutton.Image = (Image) GFXLibrary.cardpanel_payment_button_greywhite_normal;
                orderbutton.Position = new Point(x, (y + 0x12) + 3);
                orderbutton.Height = orderbutton.Image.Height;
                orderbutton.Width = orderbutton.Image.Width;
                orderbutton.setMouseOverDelegate(() => orderbutton.Image = (Image) GFXLibrary.cardpanel_payment_button_greywhite_over, () => orderbutton.Image = (Image) GFXLibrary.cardpanel_payment_button_greywhite_normal);
                orderbutton.Tag = info3;
                orderbutton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.productclick));
                label5.Text = this.strOrderNow;
                label5.Position = new Point(0, 0);
                label5.Width = orderbutton.Width;
                label5.Height = orderbutton.Height;
                label5.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
                label5.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
                orderbutton.addControl(label5);
                label5.Tag = info3;
                label5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.productclick));
                int num5 = 14;
                int num6 = 0;
                if (Program.mySettings.LanguageIdent == "fr")
                {
                    num5 = 13;
                    num6 = -5;
                }
                label.Text = info3.Strikethrough.ToString();
                label.Position = new Point(0x74 + num6, 0x15);
                label.Width = 300;
                label.Height = 0x18;
                label.Font = FontManager.GetFont("Arial", (float) num5, FontStyle.Strikeout);
                label.Color = ARGBColors.Black;
                crownsbutton.addControl(label);
                label.Tag = info3;
                label.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.productclick));
                label.Size = label.TextSizeX;
                label2.Text = info3.Crowns.ToString();
                label2.Position = new Point(label.X + label.Width, label.Y);
                label2.Font = FontManager.GetFont("Arial", (float) num5, FontStyle.Bold);
                label2.Color = ARGBColors.Purple;
                label2.Width = 300;
                label2.Height = 0x18;
                crownsbutton.addControl(label2);
                label2.Tag = info3;
                label2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.productclick));
                label2.Size = label2.TextSizeX;
                label3.Text = this.strCrowns;
                label3.Position = new Point((label2.X + label2.Width) + num6, label2.Y);
                label3.Font = FontManager.GetFont("Arial", (float) num5, FontStyle.Bold);
                label3.Color = ARGBColors.Black;
                label3.Size = new Size(300, 0x18);
                crownsbutton.addControl(label3);
                label3.Tag = info3;
                label3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.productclick));
                label3.Size = label3.TextSizeX;
                if (Program.aeriaInstall)
                {
                    label4.Text = " " + ((int) info3.Cost).ToString("F", nFI);
                }
                else
                {
                    label4.Text = info3.Currency + " " + info3.Cost.ToString("F", info2);
                }
                label4.Position = new Point(label.X, (label.Y + label.Height) + 4);
                label4.Font = FontManager.GetFont("Arial", 14f, FontStyle.Bold);
                label4.Color = ARGBColors.Black;
                label4.Size = new Size(300, 0x18);
                crownsbutton.addControl(label4);
                label4.Tag = info3;
                label4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.productclick));
                label4.Size = label4.TextSizeX;
                if (Program.aeriaInstall)
                {
                    image3.Image = (Image) GFXLibrary.aeriaPoints;
                    label4.Position = new Point(label.X + 20, label.Y + label.Height);
                    image3.Position = new Point(label.X, (((label.Y + label.Height) + 4) - 2) - 3);
                    image3.Tag = info3;
                    crownsbutton.addControl(image3);
                }
                y += crownsbutton.Height + 40;
            }
            this.mainBackgroundImage.invalidate();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }

        private void productclick()
        {
            if (Program.steamActive)
            {
                ProductInfo tag = (ProductInfo) base.ClickedControl.Tag;
                XmlRpcAuthProvider provider = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfilePath);
                XmlRpcAuthRequest req = new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), null, null, null) {
                    Culture = this.PlayerLanguage,
                    Currency = this.PlayerCurrency,
                    Country = this.PlayerCountry,
                    SteamID = Program.steamID,
                    ItemID = tag.ProductID.ToString()
                };
                InterfaceMgr.Instance.closeAllPopups();
                XmlRpcAuthResponse response = provider.SteamPaymentInit(req, null, this, 0x3a98);
                if (response.SuccessCode == 0)
                {
                    MessageBox.Show(response.Message);
                }
                else
                {
                    Program.forceSteamDXOverlay();
                }
            }
            else if (Program.aeriaInstall)
            {
                ProductInfo info2 = (ProductInfo) base.ClickedControl.Tag;
                if (MyMessageBox.Show(SK.Text("EmptyVillagePanel_Buy_Village", "Purchase") + Environment.NewLine + Environment.NewLine + info2.Crowns.ToString() + " " + SK.Text("BuyCrownsPanel_Crowns", "Crowns") + Environment.NewLine + info2.Cost.ToString() + " Aeria Points", SK.Text("ManageCandsPanel_Confirm_Purchase_Crowns", "Confirm Crowns Purchase"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int crowns = info2.Crowns;
                    XmlRpcAuthProvider provider2 = XmlRpcAuthProvider.CreateForEndpoint(URLs.ProfileProtocol, URLs.ProfileServerAddressCards, URLs.ProfileServerPort, URLs.ProfilePath);
                    XmlRpcAuthRequest request2 = new XmlRpcAuthRequest(RemoteServices.Instance.UserGuid.ToString().Replace("-", ""), "", "", "", RemoteServices.Instance.SessionGuid.ToString().Replace("-", ""), null, null, null) {
                        ItemID = crowns.ToString(),
                        OrderID = info2.Cost.ToString()
                    };
                    XmlRpcAuthResponse response2 = provider2.AeriaMakePayment(request2, null, this, 0x3a98);
                    if (response2.SuccessCode == 0)
                    {
                        if (((response2.Message[0] != '2') || (response2.Message[1] != '0')) || (response2.Message[2] != '5'))
                        {
                            MessageBox.Show(response2.Message);
                        }
                        else if (MyMessageBox.Show(SK.Text("ManageCandsPanel_Purchase_Failed_Buy_Points", "You don't have enough Aeria Points for this purchase. Do you wish to purchase Aeria Points now?"), SK.Text("ManageCandsPanel_Purchase_Failed", "Purchase Failed"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            this.purchaseAP();
                        }
                    }
                    else
                    {
                        WorldMap world = GameEngine.Instance.World;
                        world.ProfileCrowns += info2.Crowns;
                        MyMessageBox.Show(SK.Text("ManageCandsPanel_Successful_Purchase", "Your purchase has been successfully completed"), SK.Text("ManageCandsPanel_Crowns_Purchased", "Crowns Purchased"));
                        this.closeClick();
                    }
                }
            }
        }

        public void purchaseAP()
        {
            new Process { StartInfo = { FileName = "https://billing.aeriagames.com/" } }.Start();
        }

        public void update()
        {
        }
    }
}

