namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Drawing;

    internal class CardExpiryReportPanelDerived : GenericReportPanelBasic
    {
        private CustomSelfDrawPanel.CSDButton btnReplay = new CustomSelfDrawPanel.CSDButton();
        private string cardText = "";

        public override void init(IDockableControl parent, Size size, object back)
        {
            base.init(parent, size, back);
            this.btnReplay.ImageNorm = (Image) GFXLibrary.button_132_normal_gold;
            this.btnReplay.ImageOver = (Image) GFXLibrary.button_132_over_gold;
            this.btnReplay.ImageClick = (Image) GFXLibrary.button_132_in_gold;
            this.btnReplay.setSizeToImage();
            this.btnReplay.Position = new Point((base.Width / 2) - (this.btnReplay.Width / 2), base.btnClose.Y);
            this.btnReplay.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.btnReplay.TextYOffset = -2;
            this.btnReplay.Text.Color = ARGBColors.Black;
            this.btnReplay.Enabled = true;
            this.btnReplay.Visible = false;
            this.btnReplay.Text.Text = SK.Text("Reports_Replay_Card", "Replay Card");
            this.btnReplay.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.showCardClick), "Reports_Replay_Card");
            if (base.imgBackground.Image != null)
            {
                base.imgBackground.addControl(this.btnReplay);
            }
            else
            {
                base.addControl(this.btnReplay);
            }
        }

        public override void setData(GetReport_ReturnType returnData)
        {
            CardTypes.CardDefinition definition;
            base.setData(returnData);
            this.cardText = CardTypes.getDescriptionFromCard(returnData.genericData1);
            switch (returnData.reportType)
            {
                case 0x4c:
                    base.lblSecondaryText.Text = this.cardText;
                    base.lblSubTitle.Text = SK.Text("Reports_Card_Expires", "Card Expires");
                    break;

                case 0x4d:
                    base.lblSubTitle.Text = SK.Text("Reports_Instant_Card_Played", "Instant Card Played");
                    base.lblSecondaryText.Text = this.cardText;
                    switch (CardTypes.getCardType(returnData.genericData1))
                    {
                        case 0xc05:
                        case 0xc06:
                        case 0xc07:
                            base.lblFurther.Text = returnData.genericData2.ToString();
                            base.imgFurther.Image = (Image) GFXLibrary.com_32_honour;
                            this.setResources(-1, -1);
                            goto Label_0838;

                        case 0xc08:
                        case 0xc09:
                        case 0xc0a:
                        case 0xc0b:
                        case 0xc0c:
                            base.lblFurther.Text = returnData.genericData2.ToString();
                            base.imgFurther.Image = (Image) GFXLibrary.com_32_money;
                            this.setResources(-1, -1);
                            goto Label_0838;

                        case 0xc0d:
                        case 0xc0e:
                        case 0xc0f:
                        case 0xc10:
                            this.setResources(13, returnData.genericData2);
                            goto Label_0838;

                        case 0xc11:
                        case 0xc12:
                        case 0xc13:
                        case 0xc14:
                            this.setResources(0x11, returnData.genericData2);
                            goto Label_0838;

                        case 0xc15:
                        case 0xc16:
                        case 0xc17:
                        case 0xc18:
                            this.setResources(0x10, returnData.genericData2);
                            goto Label_0838;

                        case 0xc19:
                        case 0xc1a:
                        case 0xc1b:
                        case 0xc1c:
                            this.setResources(14, returnData.genericData2);
                            goto Label_0838;

                        case 0xc1d:
                        case 0xc1e:
                        case 0xc1f:
                        case 0xc20:
                            this.setResources(15, returnData.genericData2);
                            goto Label_0838;

                        case 0xc21:
                        case 0xc22:
                        case 0xc23:
                        case 0xc24:
                            this.setResources(0x12, returnData.genericData2);
                            goto Label_0838;

                        case 0xc25:
                        case 0xc26:
                        case 0xc27:
                        case 0xc28:
                            this.setResources(12, returnData.genericData2);
                            goto Label_0838;

                        case 0xc29:
                        case 0xc2a:
                        case 0xc2b:
                        case 0xc2c:
                            this.setResources(6, returnData.genericData2);
                            goto Label_0838;

                        case 0xc2d:
                        case 0xc2e:
                        case 0xc2f:
                        case 0xc30:
                            this.setResources(7, returnData.genericData2);
                            goto Label_0838;

                        case 0xc31:
                        case 0xc32:
                        case 0xc33:
                        case 0xc34:
                            this.setResources(8, returnData.genericData2);
                            goto Label_0838;

                        case 0xc35:
                        case 0xc36:
                        case 0xc37:
                        case 0xc38:
                            this.setResources(9, returnData.genericData2);
                            goto Label_0838;

                        case 0xc39:
                        case 0xc3a:
                        case 0xc3b:
                        case 0xc3c:
                            this.setResources(0x16, returnData.genericData2);
                            goto Label_0838;

                        case 0xc3d:
                        case 0xc3e:
                        case 0xc3f:
                        case 0xc40:
                            this.setResources(0x15, returnData.genericData2);
                            goto Label_0838;

                        case 0xc41:
                        case 0xc42:
                        case 0xc43:
                        case 0xc44:
                            this.setResources(0x1a, returnData.genericData2);
                            goto Label_0838;

                        case 0xc45:
                        case 0xc46:
                        case 0xc47:
                        case 0xc48:
                            this.setResources(0x13, returnData.genericData2);
                            goto Label_0838;

                        case 0xc49:
                        case 0xc4a:
                        case 0xc4b:
                        case 0xc4c:
                            this.setResources(0x21, returnData.genericData2);
                            goto Label_0838;

                        case 0xc4d:
                        case 0xc4e:
                        case 0xc4f:
                        case 0xc50:
                            this.setResources(0x17, returnData.genericData2);
                            goto Label_0838;

                        case 0xc51:
                        case 0xc52:
                        case 0xc53:
                        case 0xc54:
                            this.setResources(0x18, returnData.genericData2);
                            goto Label_0838;

                        case 0xc55:
                        case 0xc56:
                        case 0xc57:
                        case 0xc58:
                            this.setResources(0x19, returnData.genericData2);
                            goto Label_0838;

                        case 0xc59:
                        case 0xc5a:
                        case 0xc5b:
                        case 0xc5c:
                            this.setResources(0x1d, returnData.genericData2);
                            goto Label_0838;

                        case 0xc5d:
                        case 0xc5e:
                        case 0xc5f:
                        case 0xc60:
                            this.setResources(0x1c, returnData.genericData2);
                            goto Label_0838;

                        case 0xc61:
                        case 0xc62:
                        case 0xc63:
                        case 0xc64:
                            this.setResources(0x1f, returnData.genericData2);
                            goto Label_0838;

                        case 0xc65:
                        case 0xc66:
                        case 0xc67:
                        case 0xc68:
                            this.setResources(30, returnData.genericData2);
                            goto Label_0838;

                        case 0xc69:
                        case 0xc6a:
                        case 0xc6b:
                        case 0xc6c:
                            this.setResources(0x20, returnData.genericData2);
                            goto Label_0838;

                        case 0xcc0:
                        case 0xcc1:
                        case 0xcc2:
                        case 0xcc3:
                            base.lblFurther.Text = returnData.genericData2.ToString();
                            base.imgFurther.Image = (Image) GFXLibrary.r_building_miltary_peasent;
                            this.setResources(-1, -1);
                            goto Label_0838;

                        case 0xcc4:
                        case 0xcc5:
                        case 0xcc6:
                        case 0xcc7:
                            base.lblFurther.Text = returnData.genericData2.ToString();
                            base.imgFurther.Image = (Image) GFXLibrary.r_building_miltary_archer;
                            this.setResources(-1, -1);
                            goto Label_0838;

                        case 0xcc8:
                        case 0xcc9:
                        case 0xcca:
                        case 0xccb:
                            base.lblFurther.Text = returnData.genericData2.ToString();
                            base.imgFurther.Image = (Image) GFXLibrary.r_building_miltary_pikemen;
                            this.setResources(-1, -1);
                            goto Label_0838;

                        case 0xccc:
                        case 0xccd:
                        case 0xcce:
                        case 0xccf:
                            base.lblFurther.Text = returnData.genericData2.ToString();
                            base.imgFurther.Image = (Image) GFXLibrary.r_building_miltary_swordsman;
                            this.setResources(-1, -1);
                            goto Label_0838;

                        case 0xcd0:
                        case 0xcd1:
                        case 0xcd2:
                        case 0xcd3:
                            base.lblFurther.Text = returnData.genericData2.ToString();
                            base.imgFurther.Image = (Image) GFXLibrary.r_building_miltary_catapult;
                            this.setResources(-1, -1);
                            goto Label_0838;

                        case 0xcd7:
                        case 0xcd8:
                        case 0xcd9:
                            base.lblFurther.Text = returnData.genericData2.ToString();
                            base.imgFurther.Image = (Image) GFXLibrary.r_building_miltary_scout;
                            this.setResources(-1, -1);
                            goto Label_0838;

                        case 0xcda:
                        case 0xcdb:
                        case 0xcdc:
                            base.lblFurther.Text = returnData.genericData2.ToString();
                            base.imgFurther.Image = (Image) GFXLibrary.monk_icon;
                            this.setResources(-1, -1);
                            goto Label_0838;

                        case 0xcdd:
                        case 0xcde:
                        case 0xcdf:
                            base.lblFurther.Text = returnData.genericData2.ToString();
                            base.imgFurther.Image = (Image) GFXLibrary.merchant_icon;
                            this.setResources(-1, -1);
                            goto Label_0838;
                    }
                    break;

                case 0x63:
                    base.lblSecondaryText.Text = this.cardText;
                    base.lblSubTitle.Text = SK.Text("ReportsPanel_Card_Used", "Card Used and Expired");
                    break;
            }
        Label_0838:
            definition = new CardTypes.CardDefinition();
            definition.cardCategory = CardTypes.getCardCategory(returnData.genericData1);
            GameEngine.Instance.World.searchProfileCards(definition, "meta", this.cardText);
            foreach (int num in GameEngine.Instance.World.ProfileCardsSearch)
            {
                if (GameEngine.Instance.World.ProfileCards[num].id == CardTypes.getCardType(returnData.genericData1))
                {
                    this.btnReplay.Visible = true;
                    break;
                }
            }
            base.btnUtility.Text.Text = SK.Text("GENERIC_Cards", "Cards");
            base.btnUtility.Visible = true;
        }

        public void setResources(int resourceType, int amount)
        {
            if (resourceType != -1)
            {
                base.lblFurther.Text = amount.ToString();
                base.imgFurther.Image = (Image) GFXLibrary.getCommodity32Image(resourceType);
            }
            base.lblDate.Y = base.lblDate.Position.Y - 20;
            base.imgFurther.setSizeToImage();
            base.imgFurther.Position = new Point((base.Width / 2) - base.imgFurther.Width, base.btnDelete.Rectangle.Bottom - 80);
            base.lblFurther.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            base.lblFurther.Size = new Size(base.Width, Math.Max(base.imgFurther.Height, 30));
            base.lblFurther.Position = new Point(base.imgFurther.Rectangle.Right + 10, base.imgFurther.Position.Y);
            base.showFurtherInfo();
        }

        private void showCardClick()
        {
            GameEngine.Instance.playInterfaceSound("CardExpiryReportPanel_cards");
            InterfaceMgr.Instance.openPlayCardsWindowSearch(0, this.cardText);
            base.m_parent.closeControl(true);
            InterfaceMgr.Instance.reactiveMainWindow();
        }

        protected override void utilityClick()
        {
            GameEngine.Instance.playInterfaceSound("CardExpiryReportPanel_cards");
            InterfaceMgr.Instance.openPlayCardsWindow(0);
            base.m_parent.closeControl(true);
            InterfaceMgr.Instance.reactiveMainWindow();
        }
    }
}

