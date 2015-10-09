namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Drawing;

    internal class ScoutReportPanelDerived : GenericReportPanelBasic
    {
        private CustomSelfDrawPanel.CSDButton btnViewCastle = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel lblHonour = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblResult = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblScouts = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblWolves = new CustomSelfDrawPanel.CSDLabel();
        private Point mapTarget = new Point(-1, -1);
        private double targetZoomLevel;

        private void attackerDoubleClick()
        {
            if (base.m_returnData != null)
            {
                Point point = GameEngine.Instance.World.getVillageLocation(base.m_returnData.attackingVillage);
                double targetZoom = 10000.0;
                if (point.X != -1)
                {
                    GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
                    InterfaceMgr.Instance.changeTab(0);
                    GameEngine.Instance.World.startMultiStageZoom(targetZoom, (double) point.X, (double) point.Y);
                    base.m_parent.closeControl(true);
                    InterfaceMgr.Instance.reactiveMainWindow();
                    InterfaceMgr.Instance.displaySelectedVillagePanel(base.m_returnData.attackingVillage, false, true, false, false);
                }
            }
        }

        private void defenderDoubleClick()
        {
            if (base.m_returnData != null)
            {
                Point point = GameEngine.Instance.World.getVillageLocation(base.m_returnData.defendingVillage);
                double targetZoom = 10000.0;
                if (point.X != -1)
                {
                    GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
                    InterfaceMgr.Instance.changeTab(0);
                    GameEngine.Instance.World.startMultiStageZoom(targetZoom, (double) point.X, (double) point.Y);
                    base.m_parent.closeControl(true);
                    InterfaceMgr.Instance.reactiveMainWindow();
                    InterfaceMgr.Instance.displaySelectedVillagePanel(base.m_returnData.defendingVillage, false, true, false, false);
                }
            }
        }

        public override void init(IDockableControl parent, Size size, object back)
        {
            base.init(parent, size, back);
            this.btnViewCastle.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.btnViewCastle.ImageOver = (Image) GFXLibrary.button_132_over;
            this.btnViewCastle.ImageClick = (Image) GFXLibrary.button_132_in;
            this.btnViewCastle.setSizeToImage();
            this.btnViewCastle.Position = new Point(base.btnForward.Position.X, (base.btnUtility.Position.Y - this.btnViewCastle.Height) - 2);
            this.btnViewCastle.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.btnViewCastle.TextYOffset = -2;
            this.btnViewCastle.Text.Color = ARGBColors.Black;
            this.btnViewCastle.Enabled = true;
            this.btnViewCastle.Visible = false;
            this.btnViewCastle.Text.Text = SK.Text("Reports_View_Castle", "View Castle");
            this.btnViewCastle.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.viewCastleClick), "Reports_View_Castle");
            base.btnUtility.Text.Text = SK.Text("Reports_Show_On_Map", "Show On Map");
            this.lblResult.Color = ARGBColors.Black;
            this.lblResult.Font = FontManager.GetFont("Arial", 14f, FontStyle.Regular);
            this.lblResult.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.lblResult.Position = new Point(20, base.lblSecondaryText.Rectangle.Bottom);
            this.lblResult.Size = new Size(base.Width - 40, 0x1a);
            base.lblDate.Y = this.lblResult.Rectangle.Bottom;
            this.lblScouts.Text = SK.Text("GENERIC_Scouts", "Scouts");
            this.lblScouts.Color = ARGBColors.Black;
            this.lblScouts.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.lblScouts.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.lblScouts.Position = new Point(base.borderOffset, base.lblDate.Rectangle.Bottom + 5);
            this.lblScouts.Size = new Size(base.Width - (base.borderOffset * 2), 0x1a);
            this.lblScouts.Visible = false;
            this.lblWolves.Color = ARGBColors.Black;
            this.lblWolves.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.lblWolves.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.lblWolves.Position = new Point(base.borderOffset, this.lblScouts.Y + 30);
            this.lblWolves.Size = new Size(base.Width - (base.borderOffset * 2), 0x1a);
            this.lblWolves.Visible = false;
            this.lblHonour.Color = ARGBColors.Black;
            this.lblHonour.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.lblHonour.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.lblHonour.Position = new Point(base.borderOffset, base.btnDelete.Y);
            this.lblHonour.Size = new Size(base.Width - (base.borderOffset * 2), 0x1a);
            this.lblHonour.Visible = false;
            if (base.imgBackground.Image != null)
            {
                base.imgBackground.addControl(this.lblScouts);
                base.imgBackground.addControl(this.lblResult);
                base.imgBackground.addControl(this.btnViewCastle);
                base.imgBackground.addControl(this.lblHonour);
                base.imgBackground.addControl(this.lblWolves);
            }
            else
            {
                base.addControl(this.lblScouts);
                base.addControl(this.lblResult);
                base.addControl(this.btnViewCastle);
                base.addControl(this.lblHonour);
                base.addControl(this.lblWolves);
            }
        }

        public override void setData(GetReport_ReturnType returnData)
        {
            base.setData(returnData);
            bool flag = true;
            bool flag2 = false;
            this.lblResult.Text = SK.Text("GENERIC_The_Attacker_Wins", "The Attacker Wins");
            switch (returnData.reportType)
            {
                case 0x79:
                case 0x7a:
                case 0x7e:
                case 0x15:
                case 0x1a:
                case 0x1b:
                case 0x36:
                case 0x37:
                case 0x38:
                case 0x39:
                    base.lblMainText.Text = base.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Scouts_Out", "Scouts");
                    if (returnData.otherUser.Length != 0)
                    {
                        base.lblSecondaryText.Text = returnData.otherUser + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                        break;
                    }
                    if (returnData.reportType == 0x15)
                    {
                        if (returnData.defendingVillage < 0)
                        {
                            base.lblSecondaryText.Text = SK.Text("GENERIC_An_Empty_Village", "An empty village");
                        }
                        else
                        {
                            base.lblSecondaryText.Text = GameEngine.Instance.World.getVillageName(returnData.defendingVillage);
                        }
                    }
                    else if (returnData.reportType == 0x1a)
                    {
                        base.lblSecondaryText.Text = SK.Text("GENERIC_A_Bandit_Camp", "A Bandit Camp");
                    }
                    else if (returnData.reportType == 0x1b)
                    {
                        base.lblSecondaryText.Text = SK.Text("GENERIC_A_Wolf_Lair", "A Wolf Lair");
                    }
                    else if (returnData.reportType == 0x36)
                    {
                        base.lblSecondaryText.Text = SK.Text("GENERIC_Rats_Castle", "Rat's Castle");
                    }
                    else if (returnData.reportType == 0x37)
                    {
                        base.lblSecondaryText.Text = SK.Text("GENERIC_Snakes_Castle", "Snake's Castle");
                    }
                    else if (returnData.reportType == 0x38)
                    {
                        base.lblSecondaryText.Text = SK.Text("GENERIC_Pigs_Castle", "Pig's Castle");
                    }
                    else if (returnData.reportType == 0x39)
                    {
                        base.lblSecondaryText.Text = SK.Text("GENERIC_Wolfs_Castle", "Wolf's Castle");
                    }
                    else if (returnData.reportType == 0x79)
                    {
                        base.lblSecondaryText.Text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
                    }
                    else if (returnData.reportType == 0x7a)
                    {
                        base.lblSecondaryText.Text = SK.Text("GENERIC_Paladin_Castle", "Paladin's Castle");
                    }
                    else if (returnData.reportType == 0x7e)
                    {
                        string[] strArray = new string[] { SK.Text("GENERIC_Treasure_Castle", "Treasure Castle"), " ", SK.Text("GENERIC_TREASURE_CASTLE_LEVEL", "Level"), " : ", (returnData.genericData31 + 1).ToString() };
                        base.lblSecondaryText.Text = string.Concat(strArray);
                        this.lblScouts.Position = new Point(0, base.lblDate.Rectangle.Bottom + 5);
                        this.lblScouts.Size = new Size(base.Width, 0x1a);
                        this.lblScouts.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
                        this.lblScouts.Text = SK.Text("GENERIC_Treasure_Chests", "Treasure Chests") + " : " + returnData.genericData32.ToString();
                        this.lblScouts.Visible = true;
                        flag = false;
                    }
                    break;

                case 0x16:
                    this.btnViewCastle.Visible = false;
                    if (returnData.otherUser.Length != 0)
                    {
                        base.lblMainText.Text = returnData.otherUser;
                    }
                    else
                    {
                        base.lblMainText.Text = SK.Text("GENERIC_An_Unknown_Player", "An Unknown Player");
                    }
                    base.lblMainText.Text = base.lblMainText.Text + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Scouts_Out", "Scouts");
                    if (returnData.otherUser.Length == 0)
                    {
                        base.lblSecondaryText.Text = SK.Text("GENERIC_An_Empty_Village", "An empty village");
                    }
                    else
                    {
                        base.lblSecondaryText.Text = base.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.defendingVillage) + ")";
                    }
                    if (!returnData.successStatus)
                    {
                        this.lblResult.Text = SK.Text("GENERIC_The_Defender_Wins", "The Defender Wins");
                    }
                    goto Label_06CD;

                case 0x17:
                    base.lblMainText.Text = base.reportOwner + " (" + GameEngine.Instance.World.getVillageName(returnData.attackingVillage) + ")";
                    base.lblSubTitle.Text = SK.Text("Reports_Forages", "Forages");
                    this.lblResult.Visible = false;
                    if (returnData.genericData6 <= 0)
                    {
                        goto Label_06CD;
                    }
                    this.lblResult.Text = SK.Text("SeasonalBonus", "Seasonal Bonus");
                    this.lblResult.Visible = true;
                    base.lblDate.Y -= 50;
                    this.lblHonour.Y -= 50;
                    this.lblResult.Y += 0x23;
                    this.lblResult.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
                    switch (returnData.genericData6)
                    {
                        case 2:
                            this.lblScouts.Text = SK.Text("REPORTS_SeasonalWheelSpins2", "Tier 2 Wheel Spin");
                            goto Label_053C;

                        case 3:
                            this.lblScouts.Text = SK.Text("REPORTS_SeasonalWheelSpins3", "Tier 3 Wheel Spin");
                            goto Label_053C;
                    }
                    this.lblScouts.Text = SK.Text("REPORTS_SeasonalWheelSpins1", "Tier 1 Wheel Spin");
                    goto Label_053C;

                default:
                    goto Label_06CD;
            }
            if (!returnData.successStatus)
            {
                this.lblResult.Text = SK.Text("GENERIC_The_Defender_Wins", "The Defender Wins");
                this.btnViewCastle.Visible = false;
            }
            goto Label_06CD;
        Label_053C:
            this.lblScouts.Position = this.lblResult.Position;
            this.lblScouts.Y += 0x16;
            this.lblScouts.Size = this.lblResult.Size;
            this.lblScouts.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.lblScouts.Font = FontManager.GetFont("Arial", 12f, FontStyle.Regular);
            this.lblScouts.Visible = true;
            flag = false;
            flag2 = true;
        Label_06CD:
            if ((returnData.reportType == 0x1b) && (returnData.genericData6 > 0))
            {
                this.lblWolves.Text = SK.Text("GENERIC_Wolves", "Wolves") + " " + returnData.genericData6.ToString();
                this.lblWolves.Visible = true;
            }
            if (returnData.defendingVillage >= 0)
            {
                this.mapTarget = GameEngine.Instance.World.getVillageLocation(returnData.defendingVillage);
                this.targetZoomLevel = 10000.0;
                base.btnUtility.Visible = true;
            }
            else
            {
                base.btnUtility.Visible = false;
            }
            if ((returnData.genericData3 < 100) || (returnData.genericData3 > 0xc7))
            {
                base.lblFurther.Visible = false;
                if (flag)
                {
                    this.lblScouts.Text = SK.Text("GENERIC_Scouts", "Scouts") + " " + returnData.genericData2.ToString("N", base.nfi) + "/" + returnData.genericData1.ToString("N", base.nfi);
                    this.lblScouts.Visible = true;
                }
                if (returnData.reportType != 0x27)
                {
                    this.btnViewCastle.Visible = true;
                }
                base.imgFurther.Visible = false;
            }
            else
            {
                this.btnViewCastle.Visible = false;
                if (!flag2)
                {
                    this.lblScouts.Visible = false;
                }
                base.lblSecondaryText.Text = SpecialVillageTypes.getName((returnData.genericData3 - 100) + 100, Program.mySettings.LanguageIdent);
                switch (returnData.genericData3)
                {
                    case 0x6a:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_wood;
                        break;

                    case 0x6b:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_stone;
                        break;

                    case 0x6c:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_iron;
                        break;

                    case 0x6d:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_pitch;
                        break;

                    case 0x70:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_ale;
                        break;

                    case 0x71:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_apples;
                        break;

                    case 0x72:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_bread;
                        break;

                    case 0x73:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_veg;
                        break;

                    case 0x74:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_meat;
                        break;

                    case 0x75:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_cheese;
                        break;

                    case 0x76:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_fish;
                        break;

                    case 0x77:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_clothing;
                        break;

                    case 0x79:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_furniture;
                        break;

                    case 0x7a:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_venison;
                        break;

                    case 0x7b:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_salt;
                        break;

                    case 0x7c:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_spice;
                        break;

                    case 0x7d:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_silk;
                        break;

                    case 0x7e:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_metalwork;
                        break;

                    case 0x80:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_pikes;
                        break;

                    case 0x81:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_bows;
                        break;

                    case 130:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_swords;
                        break;

                    case 0x83:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_armour;
                        break;

                    case 0x84:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_catapults;
                        break;

                    case 0x85:
                        base.imgFurther.Image = (Image) GFXLibrary.com_32_wine;
                        break;
                }
                base.imgFurther.setSizeToImage();
                base.imgFurther.Position = new Point((base.Width / 2) - base.imgFurther.Width, base.btnForward.Position.Y);
                base.lblFurther.Text = returnData.genericData4.ToString("N", base.nfi);
                base.lblFurther.Position = new Point(base.Width / 2, base.btnForward.Position.Y);
                base.lblFurther.Size = new Size(base.Width / 2, 0x1a);
                base.lblFurther.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
                base.showFurtherInfo();
                if (returnData.genericData5 > 0)
                {
                    this.lblHonour.Text = SK.Text("GENERIC_Honour", "Honour") + " : " + returnData.genericData5.ToString();
                    this.lblHonour.Visible = true;
                }
            }
            base.lblMainText.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.attackerDoubleClick), "Reports_Attacker_DClick");
            base.lblSecondaryText.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.defenderDoubleClick), "Reports_Defender_DClick");
        }

        protected override void utilityClick()
        {
            if (this.mapTarget.X != -1)
            {
                GameEngine.Instance.playInterfaceSound("ReportsGeneric_goto_map");
                InterfaceMgr.Instance.changeTab(0);
                GameEngine.Instance.World.startMultiStageZoom(this.targetZoomLevel, (double) this.mapTarget.X, (double) this.mapTarget.Y);
                base.m_parent.closeControl(true);
                InterfaceMgr.Instance.reactiveMainWindow();
            }
        }

        private void viewCastleCallback(ViewCastle_ReturnType returnData)
        {
            if (returnData.Success && ((returnData.castleMapSnapshot != null) || (returnData.castleTroopsSnapshot != null)))
            {
                base.m_parent.closeControl(true);
                InterfaceMgr.Instance.getMainTabBar().selectDummyTab(6);
                InterfaceMgr.Instance.reactiveMainWindow();
                int villageID = -1;
                int campMode = 0;
                if (base.m_returnData != null)
                {
                    if (base.m_returnData.reportType == 0x1a)
                    {
                        campMode = 1;
                        villageID = -2;
                    }
                    else if (base.m_returnData.reportType == 0x1b)
                    {
                        campMode = 2;
                        villageID = -3;
                    }
                    else if ((base.m_returnData.reportType == 0x15) && (base.m_returnData.otherUser.Length == 0))
                    {
                        villageID = -4;
                    }
                    else if (base.m_returnData.reportType == 0x36)
                    {
                        campMode = 0;
                        villageID = -5;
                    }
                    else if (base.m_returnData.reportType == 0x37)
                    {
                        campMode = 0;
                        villageID = -6;
                    }
                    else if (base.m_returnData.reportType == 0x38)
                    {
                        campMode = 0;
                        villageID = -7;
                    }
                    else if (base.m_returnData.reportType == 0x39)
                    {
                        campMode = 0;
                        villageID = -8;
                    }
                    else if (base.m_returnData.reportType == 0x79)
                    {
                        campMode = 0;
                        villageID = -9;
                    }
                    else if (base.m_returnData.reportType == 0x7a)
                    {
                        campMode = 0;
                        villageID = -10;
                    }
                    else if (base.m_returnData.reportType == 0x7e)
                    {
                        campMode = 0;
                        villageID = -11;
                    }
                    else
                    {
                        villageID = returnData.villageID;
                    }
                }
                GameEngine.Instance.InitCastleView(returnData.castleMapSnapshot, returnData.castleTroopsSnapshot, returnData.keepLevel, campMode, returnData.defencesLevel, villageID, returnData.landType);
                InterfaceMgr.Instance.castleBattleTimes(returnData.lastCastleTime, returnData.lastTroopTime);
            }
            else
            {
                MyMessageBox.Show(SK.Text("ReportsPanel_No_Longer_Valid", "The target for this scout report is no longer valid."), SK.Text("ReportsPanel_Scout_Report", "Scout Report"));
            }
        }

        private void viewCastleClick()
        {
            GameEngine.Instance.playInterfaceSound("ScoutReportPanel_view_castle");
            RemoteServices.Instance.set_ViewCastle_UserCallBack(new RemoteServices.ViewCastle_UserCallBack(this.viewCastleCallback));
            RemoteServices.Instance.ViewCastle_Report(base.reportID);
        }
    }
}

