namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CastleMapAttackerSetupPanel : CustomSelfDrawPanel, IDockableControl
    {
        private CustomSelfDrawPanel.CSDButton advancedButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton aiExportButton = new CustomSelfDrawPanel.CSDButton();
        private bool armyLaunched;
        private CustomSelfDrawPanel.CSDButton cancelButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDArea captain_castleSelectionBackgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDLabel captain_castleSelectionCaptainCommandLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton captain_castleSelectionCaptainDeleteButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage captain_castleSelectionCaptainImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage captain_castleSelectionCaptainInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel captain_castleSelectionCaptainLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel captain_castleSelectionCaptainSecondsCountLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel captain_castleSelectionCaptainSecondsLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton captain_castleSelectionCommand1 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton captain_castleSelectionCommand2 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton captain_castleSelectionCommand3 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton captain_castleSelectionCommand4 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton captain_castleSelectionCommand5 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton captain_castleSelectionCommand6 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton captain_castleSelectionCommand7 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton captain_castleSelectionCommand8 = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage captain_castleSelectionInset6Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage captain_castleSelectionPanelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton captain_closeButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDTrackBar captain_commandValueTrack = new CustomSelfDrawPanel.CSDTrackBar();
        private CustomSelfDrawPanel.CSDButton castlePlaceArcherButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlaceArcherCastle = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castlePlaceArcherInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlaceArcherLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea castlePlaceBackgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton castlePlaceCaptainButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlaceCaptainInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlaceCaptainLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castlePlaceCatapultButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlaceCatapultInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlaceCatapultLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage castlePlacePanelFaderImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castlePlacePanelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton castlePlacePeasantButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlacePeasantCastle = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castlePlacePeasantInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlacePeasantLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castlePlacePikemanButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlacePikemanCastle = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castlePlacePikemanInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlacePikemanLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castlePlaceSize15Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlaceSize1Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlaceSize3Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlaceSize5Button = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton castlePlaceSwordsmanButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlaceSwordsmanCastle = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castlePlaceSwordsmanInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlaceSwordsmanLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castleSelectionArcherDeleteButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castleSelectionArcherImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionArcherInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castleSelectionArcherLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDArea castleSelectionBackgroundArea = new CustomSelfDrawPanel.CSDArea();
        private CustomSelfDrawPanel.CSDButton castleSelectionCaptainDeleteButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castleSelectionCaptainImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionCaptainInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castleSelectionCaptainLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castleSelectionCatapultDeleteButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castleSelectionCatapultImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionCatapultInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castleSelectionCatapultLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDImage castleSelectionInset1Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionInset2Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionInset3Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionInset4Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionInset5Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionInset6Image = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionPanelImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDButton castleSelectionPeasantDeleteButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castleSelectionPeasantImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionPeasantInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castleSelectionPeasantLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castleSelectionPikemanDeleteButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castleSelectionPikemanImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionPikemanInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castleSelectionPikemanLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castleSelectionSwordsmanDeleteButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castleSelectionSwordsmanImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage castleSelectionSwordsmanInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castleSelectionSwordsmanLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private DockableControl dockableControl;
        private CustomSelfDrawPanel.CSDButton launchHeaderButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton loadButton = new CustomSelfDrawPanel.CSDButton();
        private OpenFileDialog LoadSetupFileDialog;
        private int placementSize = 1;
        private CustomSelfDrawPanel.CSDButton saveButton = new CustomSelfDrawPanel.CSDButton();
        private SaveFileDialog SaveSetupFileDialog;

        public CastleMapAttackerSetupPanel()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.SelfDrawBackground = true;
        }

        private int calcLaunchBarYPos()
        {
            return 0x16e;
        }

        private void cancelAttack()
        {
            InterfaceMgr.Instance.toggleDXCardBarActive(true);
            InterfaceMgr.Instance.getMainTabBar().changeTab(9);
            InterfaceMgr.Instance.getMainTabBar().changeTab(0);
        }

        private void captainCommandClick()
        {
            if (base.OverControl != null)
            {
                int data = base.OverControl.Data;
                this.updateCaptainCommands(data);
                GameEngine.Instance.CastleAttackerSetup.updateAttackingCaptainCommand(data);
            }
        }

        private void castlePlaceClick()
        {
            CustomSelfDrawPanel.CSDControl overControl = base.OverControl;
            if (overControl != null)
            {
                int data = overControl.Data;
                this.setPlaceSize(this.placementSize);
                if (GameEngine.Instance.CastleAttackerSetup.checkNormalTroopsAvailable(data))
                {
                    GameEngine.Instance.CastleAttackerSetup.setUsingCastleTroops(false);
                }
                else
                {
                    GameEngine.Instance.CastleAttackerSetup.setUsingCastleTroops(true);
                }
                GameEngine.Instance.CastleAttackerSetup.startPlacingAttackerTroops(data);
            }
        }

        private void castlePlaceSizeClick()
        {
            CustomSelfDrawPanel.CSDControl overControl = base.OverControl;
            if (overControl != null)
            {
                int size = 0;
                int data = overControl.Data;
                size = overControl.Data;
                this.setPlaceSize(size);
            }
        }

        public void clearSelectedTroop()
        {
            this.castleSelectionBackgroundArea.Visible = false;
            this.captain_castleSelectionBackgroundArea.Visible = false;
            this.castlePlaceBackgroundArea.Visible = true;
        }

        private void closeClick()
        {
            GameEngine.Instance.CastleAttackerSetup.clearLasso();
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        public void create()
        {
            this.initCastlePlacePanel();
            this.initLaunchBar();
            this.initSelectionPanel();
            this.initSelectionPanel_Captain();
        }

        private void DEBUG_export()
        {
        }

        private void DEBUG_load()
        {
        }

        private void DEBUG_save()
        {
        }

        public void display(ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(parent, x, y);
        }

        public void display(bool asPopup, ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(asPopup, parent, x, y);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init()
        {
            this.setPlaceSize(3);
        }

        public void initCastlePlacePanel()
        {
            this.castlePlaceBackgroundArea.Position = new Point(3, 0);
            this.castlePlaceBackgroundArea.Size = base.Size;
            base.addControl(this.castlePlaceBackgroundArea);
            this.castlePlacePanelImage.Image = (Image) GFXLibrary.castlescreen_panelback_A;
            this.castlePlacePanelImage.Position = new Point(0, 0);
            this.castlePlaceBackgroundArea.addControl(this.castlePlacePanelImage);
            this.castlePlacePeasantButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_peasent;
            this.castlePlacePeasantButton.ImageOver = (Image) GFXLibrary.r_building_miltary_peasent_over;
            this.castlePlacePeasantButton.Position = new Point(-9, 5);
            this.castlePlacePeasantButton.Data = 90;
            this.castlePlacePeasantButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlacePeasantButton.CustomTooltipID = 200;
            this.castlePlacePeasantButton.CustomTooltipData = 90;
            this.castlePlacePeasantButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapAttackerSetupPanel_peasants");
            this.castlePlacePanelImage.addControl(this.castlePlacePeasantButton);
            this.castlePlacePeasantInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castlePlacePeasantInset.Position = new Point(0x37, 0x41);
            this.castlePlacePeasantButton.addControl(this.castlePlacePeasantInset);
            this.castlePlacePeasantLabel.Text = "0";
            this.castlePlacePeasantLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castlePlacePeasantLabel.Position = new Point(0, -1);
            this.castlePlacePeasantLabel.Size = this.castlePlacePeasantInset.Size;
            this.castlePlacePeasantLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castlePlacePeasantInset.addControl(this.castlePlacePeasantLabel);
            this.castlePlacePeasantCastle.Image = (Image) GFXLibrary.castlescreen_take_from_castle;
            this.castlePlacePeasantCastle.Position = new Point(15, -20);
            this.castlePlacePeasantCastle.Visible = false;
            this.castlePlacePeasantLabel.addControl(this.castlePlacePeasantCastle);
            this.castlePlaceArcherButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_archer;
            this.castlePlaceArcherButton.ImageOver = (Image) GFXLibrary.r_building_miltary_archer_over;
            this.castlePlaceArcherButton.Position = new Point(0x49, 5);
            this.castlePlaceArcherButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlaceArcherButton.Data = 0x5c;
            this.castlePlaceArcherButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapAttackerSetupPanel_archers");
            this.castlePlaceArcherButton.CustomTooltipID = 200;
            this.castlePlaceArcherButton.CustomTooltipData = 0x5c;
            this.castlePlacePanelImage.addControl(this.castlePlaceArcherButton);
            this.castlePlaceArcherInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castlePlaceArcherInset.Position = new Point(0x37, 0x41);
            this.castlePlaceArcherButton.addControl(this.castlePlaceArcherInset);
            this.castlePlaceArcherLabel.Text = "0";
            this.castlePlaceArcherLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castlePlaceArcherLabel.Position = new Point(0, -1);
            this.castlePlaceArcherLabel.Size = this.castlePlaceArcherInset.Size;
            this.castlePlaceArcherLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castlePlaceArcherInset.addControl(this.castlePlaceArcherLabel);
            this.castlePlaceArcherCastle.Image = (Image) GFXLibrary.castlescreen_take_from_castle;
            this.castlePlaceArcherCastle.Position = new Point(15, -20);
            this.castlePlaceArcherCastle.Visible = false;
            this.castlePlaceArcherLabel.addControl(this.castlePlaceArcherCastle);
            this.castlePlacePikemanButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_pikemen;
            this.castlePlacePikemanButton.ImageOver = (Image) GFXLibrary.r_building_miltary_pikemen_over;
            this.castlePlacePikemanButton.Position = new Point(-9, 80);
            this.castlePlacePikemanButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlacePikemanButton.Data = 0x5d;
            this.castlePlacePikemanButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapAttackerSetupPanel_pikemen");
            this.castlePlacePikemanButton.CustomTooltipID = 200;
            this.castlePlacePikemanButton.CustomTooltipData = 0x5d;
            this.castlePlacePanelImage.addControl(this.castlePlacePikemanButton);
            this.castlePlacePikemanInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castlePlacePikemanInset.Position = new Point(0x37, 0x41);
            this.castlePlacePikemanButton.addControl(this.castlePlacePikemanInset);
            this.castlePlacePikemanLabel.Text = "0";
            this.castlePlacePikemanLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castlePlacePikemanLabel.Position = new Point(0, -1);
            this.castlePlacePikemanLabel.Size = this.castlePlacePikemanInset.Size;
            this.castlePlacePikemanLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castlePlacePikemanInset.addControl(this.castlePlacePikemanLabel);
            this.castlePlacePikemanCastle.Image = (Image) GFXLibrary.castlescreen_take_from_castle;
            this.castlePlacePikemanCastle.Position = new Point(15, -20);
            this.castlePlacePikemanCastle.Visible = false;
            this.castlePlacePikemanLabel.addControl(this.castlePlacePikemanCastle);
            this.castlePlaceSwordsmanButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_swordsman;
            this.castlePlaceSwordsmanButton.ImageOver = (Image) GFXLibrary.r_building_miltary_swordsman_over;
            this.castlePlaceSwordsmanButton.Position = new Point(0x49, 80);
            this.castlePlaceSwordsmanButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlaceSwordsmanButton.Data = 0x5b;
            this.castlePlaceSwordsmanButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapAttackerSetupPanel_swordsmen");
            this.castlePlaceSwordsmanButton.CustomTooltipID = 200;
            this.castlePlaceSwordsmanButton.CustomTooltipData = 0x5b;
            this.castlePlacePanelImage.addControl(this.castlePlaceSwordsmanButton);
            this.castlePlaceSwordsmanInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castlePlaceSwordsmanInset.Position = new Point(0x37, 0x41);
            this.castlePlaceSwordsmanButton.addControl(this.castlePlaceSwordsmanInset);
            this.castlePlaceSwordsmanLabel.Text = "0";
            this.castlePlaceSwordsmanLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castlePlaceSwordsmanLabel.Position = new Point(0, -1);
            this.castlePlaceSwordsmanLabel.Size = this.castlePlaceSwordsmanInset.Size;
            this.castlePlaceSwordsmanLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castlePlaceSwordsmanInset.addControl(this.castlePlaceSwordsmanLabel);
            this.castlePlaceSwordsmanCastle.Image = (Image) GFXLibrary.castlescreen_take_from_castle;
            this.castlePlaceSwordsmanCastle.Position = new Point(15, -20);
            this.castlePlaceSwordsmanCastle.Visible = false;
            this.castlePlaceSwordsmanLabel.addControl(this.castlePlaceSwordsmanCastle);
            this.castlePlaceCatapultButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_catapult;
            this.castlePlaceCatapultButton.ImageOver = (Image) GFXLibrary.r_building_miltary_catapult_over;
            this.castlePlaceCatapultButton.Position = new Point(-9, 0x9b);
            this.castlePlaceCatapultButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlaceCatapultButton.Data = 0x5e;
            this.castlePlaceCatapultButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapAttackerSetupPanel_catapults");
            this.castlePlaceCatapultButton.CustomTooltipID = 200;
            this.castlePlaceCatapultButton.CustomTooltipData = 0x5e;
            this.castlePlacePanelImage.addControl(this.castlePlaceCatapultButton);
            this.castlePlaceCatapultInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castlePlaceCatapultInset.Position = new Point(0x37, 0x41);
            this.castlePlaceCatapultButton.addControl(this.castlePlaceCatapultInset);
            this.castlePlaceCatapultLabel.Text = "0";
            this.castlePlaceCatapultLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castlePlaceCatapultLabel.Position = new Point(0, 0);
            this.castlePlaceCatapultLabel.Size = this.castlePlaceCatapultInset.Size;
            this.castlePlaceCatapultLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castlePlaceCatapultInset.addControl(this.castlePlaceCatapultLabel);
            this.castlePlaceCaptainButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_captain_normal;
            this.castlePlaceCaptainButton.ImageOver = (Image) GFXLibrary.r_building_miltary_captain_over;
            this.castlePlaceCaptainButton.Position = new Point(0x49, 0x9b);
            this.castlePlaceCaptainButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlaceCaptainButton.Data = 100;
            this.castlePlaceCaptainButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceClick), "CastleMapAttackerSetupPanel_captain");
            this.castlePlaceCaptainButton.CustomTooltipID = 200;
            this.castlePlaceCaptainButton.CustomTooltipData = 100;
            this.castlePlacePanelImage.addControl(this.castlePlaceCaptainButton);
            this.castlePlaceCaptainInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castlePlaceCaptainInset.Position = new Point(0x37, 0x41);
            this.castlePlaceCaptainButton.addControl(this.castlePlaceCaptainInset);
            this.castlePlaceCaptainLabel.Text = "0";
            this.castlePlaceCaptainLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castlePlaceCaptainLabel.Position = new Point(0, 0);
            this.castlePlaceCaptainLabel.Size = this.castlePlaceCaptainInset.Size;
            this.castlePlaceCaptainLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castlePlaceCaptainInset.addControl(this.castlePlaceCaptainLabel);
            this.castlePlaceSize1Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x1_normal;
            this.castlePlaceSize1Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x1_over;
            this.castlePlaceSize1Button.Position = new Point(0x1a, 0x11d);
            this.castlePlaceSize1Button.Data = 1;
            this.castlePlaceSize1Button.CustomTooltipID = 0xcf;
            this.castlePlaceSize1Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceSizeClick), "CastleMapAttackerSetupPanel_1x1");
            this.castlePlacePanelImage.addControl(this.castlePlaceSize1Button);
            this.castlePlaceSize3Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_3x3_normal;
            this.castlePlaceSize3Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_3x3_over;
            this.castlePlaceSize3Button.Position = new Point(0x40, 0x11d);
            this.castlePlaceSize3Button.Data = 3;
            this.castlePlaceSize3Button.CustomTooltipID = 0xd0;
            this.castlePlaceSize3Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceSizeClick), "CastleMapAttackerSetupPanel_3x3");
            this.castlePlacePanelImage.addControl(this.castlePlaceSize3Button);
            this.castlePlaceSize5Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_5x5_normal;
            this.castlePlaceSize5Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_5x5_over;
            this.castlePlaceSize5Button.Position = new Point(0x66, 0x11d);
            this.castlePlaceSize5Button.Data = 5;
            this.castlePlaceSize5Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceSizeClick), "CastleMapAttackerSetupPanel_5x5");
            this.castlePlaceSize5Button.CustomTooltipID = 0xd1;
            this.castlePlacePanelImage.addControl(this.castlePlaceSize5Button);
            this.castlePlaceSize15Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x5_normal;
            this.castlePlaceSize15Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x5_over;
            this.castlePlaceSize15Button.Position = new Point(140, 0x11d);
            this.castlePlaceSize15Button.Data = 15;
            this.castlePlaceSize15Button.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.castlePlaceSizeClick), "CastleMapAttackerSetupPanel_1x5");
            this.castlePlaceSize15Button.CustomTooltipID = 210;
            this.castlePlacePanelImage.addControl(this.castlePlaceSize15Button);
            this.castlePlacePanelFaderImage.Image = (Image) GFXLibrary.castlescreen_panelback_A;
            this.castlePlacePanelFaderImage.Position = new Point(0, 0);
            this.castlePlacePanelFaderImage.Alpha = 0f;
            this.castlePlacePanelImage.addControl(this.castlePlacePanelFaderImage);
        }

        private void InitializeComponent()
        {
            this.LoadSetupFileDialog = new OpenFileDialog();
            this.SaveSetupFileDialog = new SaveFileDialog();
            base.SuspendLayout();
            this.LoadSetupFileDialog.DefaultExt = "cmap";
            this.LoadSetupFileDialog.Filter = "Castle Maps (*.cmap)|*.cmap";
            this.LoadSetupFileDialog.Title = "Load Debug Castle Map";
            this.SaveSetupFileDialog.DefaultExt = "cmap";
            this.SaveSetupFileDialog.Filter = "Castle Maps (*.cmap)|*.cmap";
            this.SaveSetupFileDialog.Title = "Save Debug Castle Map";
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Name = "CastleMapAttackerSetupPanel2";
            base.Size = new Size(0xc4, 0x236);
            base.ResumeLayout(false);
        }

        public void initLaunchBar()
        {
            int y = this.calcLaunchBarYPos();
            this.launchHeaderButton.ImageNorm = (Image) GFXLibrary.infobar_03;
            this.launchHeaderButton.ImageHighlight = (Image) GFXLibrary.infobar_03_over;
            this.launchHeaderButton.Position = new Point(0, y);
            this.launchHeaderButton.Text.Text = SK.Text("GENERIC_Launch_Attack", "Launch Attack");
            this.launchHeaderButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.launchHeaderButton.TextYOffset = -5;
            this.launchHeaderButton.Enabled = false;
            this.launchHeaderButton.Text.Color = ARGBColors.Black;
            this.launchHeaderButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.launchAttack), "CastleMapAttackerSetupPanel_launch");
            this.castlePlaceBackgroundArea.addControl(this.launchHeaderButton);
            this.cancelButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.cancelButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.cancelButton.Position = new Point(0, (y + 0x37) + 10);
            this.cancelButton.Size = new Size(0xc4, this.cancelButton.ImageNorm.Height);
            this.cancelButton.Text.Text = SK.Text("CastleMapAttackerSetup_Cancel_Attack", "Cancel Attack");
            this.cancelButton.Text.Size = this.cancelButton.Size;
            this.cancelButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.cancelButton.TextYOffset = 0;
            this.cancelButton.Text.Color = ARGBColors.Black;
            this.cancelButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.cancelAttack), "CastleMapAttackerSetupPanel_cancel");
            this.castlePlaceBackgroundArea.addControl(this.cancelButton);
            this.advancedButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.advancedButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.advancedButton.Position = new Point(0, ((y + 0x37) + 50) + 10);
            this.advancedButton.Size = new Size(0xc4, this.advancedButton.ImageNorm.Height);
            this.advancedButton.Text.Text = SK.Text("CastleMapPanel_Manage_Formations", "Manage Formations");
            this.advancedButton.Text.Size = this.advancedButton.Size;
            this.advancedButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.advancedButton.TextYOffset = 0;
            this.advancedButton.Text.Color = ARGBColors.Black;
            this.advancedButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.utilAdvancedClick), "CastleMapPanel_advanced_options");
            this.castlePlaceBackgroundArea.addControl(this.advancedButton);
            this.loadButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.loadButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.loadButton.Position = new Point(0x2a, (y + 0x37) + 30);
            this.loadButton.Text.Text = "Load";
            this.loadButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.loadButton.TextYOffset = 1;
            this.loadButton.Visible = false;
            this.loadButton.Text.Color = ARGBColors.Black;
            this.loadButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.DEBUG_load));
            this.castlePlaceBackgroundArea.addControl(this.loadButton);
            this.saveButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.saveButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.saveButton.Position = new Point(0x2a, (y + 0x37) + 60);
            this.saveButton.Text.Text = "Save";
            this.saveButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.saveButton.TextYOffset = 1;
            this.saveButton.Visible = false;
            this.saveButton.Text.Color = ARGBColors.Black;
            this.saveButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.DEBUG_save));
            this.castlePlaceBackgroundArea.addControl(this.saveButton);
            this.aiExportButton.ImageNorm = (Image) GFXLibrary.int_but_delete_norm;
            this.aiExportButton.ImageOver = (Image) GFXLibrary.int_but_delete_over;
            this.aiExportButton.Position = new Point(0x2a, (y + 0x37) + 90);
            this.aiExportButton.Text.Text = "AI Export";
            this.aiExportButton.Text.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.aiExportButton.TextYOffset = 1;
            this.aiExportButton.Visible = false;
            this.aiExportButton.Text.Color = ARGBColors.Black;
            this.aiExportButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.DEBUG_export));
            this.castlePlaceBackgroundArea.addControl(this.aiExportButton);
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent);
        }

        public void initSelectionPanel()
        {
            this.castleSelectionBackgroundArea.Position = new Point(0, 0);
            this.castleSelectionBackgroundArea.Size = base.Size;
            this.castleSelectionBackgroundArea.Visible = false;
            base.addControl(this.castleSelectionBackgroundArea);
            this.castleSelectionPanelImage.Image = (Image) GFXLibrary.castlescreen_panelback_B;
            this.castleSelectionPanelImage.Position = new Point(0, 0);
            this.castleSelectionBackgroundArea.addControl(this.castleSelectionPanelImage);
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(0x99, 6);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "CastleMapAttackerSetupPanel_close");
            this.castleSelectionBackgroundArea.addControl(this.closeButton);
            this.castleSelectionInset1Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_off_select;
            this.castleSelectionInset1Image.Position = new Point(3, 0x1c);
            this.castleSelectionPanelImage.addControl(this.castleSelectionInset1Image);
            this.castleSelectionPeasantImage.Image = (Image) GFXLibrary.r_building_miltary_peasent;
            this.castleSelectionPeasantImage.Position = new Point(-10, -20);
            this.castleSelectionInset1Image.addControl(this.castleSelectionPeasantImage);
            this.castleSelectionPeasantInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castleSelectionPeasantInset.Position = new Point(70, 60);
            this.castleSelectionPeasantImage.addControl(this.castleSelectionPeasantInset);
            this.castleSelectionPeasantLabel.Text = "0";
            this.castleSelectionPeasantLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castleSelectionPeasantLabel.Position = new Point(0, 0);
            this.castleSelectionPeasantLabel.Size = this.castleSelectionPeasantInset.Size;
            this.castleSelectionPeasantLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castleSelectionPeasantInset.addControl(this.castleSelectionPeasantLabel);
            this.castleSelectionPeasantDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
            this.castleSelectionPeasantDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
            this.castleSelectionPeasantDeleteButton.Position = new Point(0x7d, 13);
            this.castleSelectionPeasantDeleteButton.Data = 90;
            this.castleSelectionPeasantDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapAttackerSetupPanel_delete_peasants");
            this.castleSelectionInset1Image.addControl(this.castleSelectionPeasantDeleteButton);
            this.castleSelectionInset2Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_off_select;
            this.castleSelectionInset2Image.Position = new Point(3, 0x6c);
            this.castleSelectionPanelImage.addControl(this.castleSelectionInset2Image);
            this.castleSelectionArcherImage.Image = (Image) GFXLibrary.r_building_miltary_archer;
            this.castleSelectionArcherImage.Position = new Point(-10, -20);
            this.castleSelectionInset2Image.addControl(this.castleSelectionArcherImage);
            this.castleSelectionArcherInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castleSelectionArcherInset.Position = new Point(70, 60);
            this.castleSelectionArcherImage.addControl(this.castleSelectionArcherInset);
            this.castleSelectionArcherLabel.Text = "0";
            this.castleSelectionArcherLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castleSelectionArcherLabel.Position = new Point(0, 0);
            this.castleSelectionArcherLabel.Size = this.castleSelectionArcherInset.Size;
            this.castleSelectionArcherLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castleSelectionArcherInset.addControl(this.castleSelectionArcherLabel);
            this.castleSelectionArcherDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
            this.castleSelectionArcherDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
            this.castleSelectionArcherDeleteButton.Position = new Point(0x7d, 13);
            this.castleSelectionArcherDeleteButton.Data = 0x5c;
            this.castleSelectionArcherDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapAttackerSetupPanel_delete_archers");
            this.castleSelectionInset2Image.addControl(this.castleSelectionArcherDeleteButton);
            this.castleSelectionInset3Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_off_select;
            this.castleSelectionInset3Image.Position = new Point(3, 0xbc);
            this.castleSelectionPanelImage.addControl(this.castleSelectionInset3Image);
            this.castleSelectionPikemanImage.Image = (Image) GFXLibrary.r_building_miltary_pikemen;
            this.castleSelectionPikemanImage.Position = new Point(-10, -20);
            this.castleSelectionInset3Image.addControl(this.castleSelectionPikemanImage);
            this.castleSelectionPikemanInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castleSelectionPikemanInset.Position = new Point(70, 60);
            this.castleSelectionPikemanImage.addControl(this.castleSelectionPikemanInset);
            this.castleSelectionPikemanLabel.Text = "0";
            this.castleSelectionPikemanLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castleSelectionPikemanLabel.Position = new Point(0, 0);
            this.castleSelectionPikemanLabel.Size = this.castleSelectionPikemanInset.Size;
            this.castleSelectionPikemanLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castleSelectionPikemanInset.addControl(this.castleSelectionPikemanLabel);
            this.castleSelectionPikemanDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
            this.castleSelectionPikemanDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
            this.castleSelectionPikemanDeleteButton.Position = new Point(0x7d, 13);
            this.castleSelectionPikemanDeleteButton.Data = 0x5d;
            this.castleSelectionPikemanDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapAttackerSetupPanel_delete_pikemen");
            this.castleSelectionInset3Image.addControl(this.castleSelectionPikemanDeleteButton);
            this.castleSelectionInset4Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_off_select;
            this.castleSelectionInset4Image.Position = new Point(3, 0x10c);
            this.castleSelectionPanelImage.addControl(this.castleSelectionInset4Image);
            this.castleSelectionSwordsmanImage.Image = (Image) GFXLibrary.r_building_miltary_swordsman;
            this.castleSelectionSwordsmanImage.Position = new Point(-10, -20);
            this.castleSelectionInset4Image.addControl(this.castleSelectionSwordsmanImage);
            this.castleSelectionSwordsmanInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castleSelectionSwordsmanInset.Position = new Point(70, 60);
            this.castleSelectionSwordsmanImage.addControl(this.castleSelectionSwordsmanInset);
            this.castleSelectionSwordsmanLabel.Text = "0";
            this.castleSelectionSwordsmanLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castleSelectionSwordsmanLabel.Position = new Point(0, 0);
            this.castleSelectionSwordsmanLabel.Size = this.castleSelectionSwordsmanInset.Size;
            this.castleSelectionSwordsmanLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castleSelectionSwordsmanInset.addControl(this.castleSelectionSwordsmanLabel);
            this.castleSelectionSwordsmanDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
            this.castleSelectionSwordsmanDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
            this.castleSelectionSwordsmanDeleteButton.Position = new Point(0x7d, 13);
            this.castleSelectionSwordsmanDeleteButton.Data = 0x5b;
            this.castleSelectionSwordsmanDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapAttackerSetupPanel_delete_swordsmen");
            this.castleSelectionInset4Image.addControl(this.castleSelectionSwordsmanDeleteButton);
            this.castleSelectionInset5Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_off_select;
            this.castleSelectionInset5Image.Position = new Point(3, 0x15c);
            this.castleSelectionPanelImage.addControl(this.castleSelectionInset5Image);
            this.castleSelectionCatapultImage.Image = (Image) GFXLibrary.r_building_miltary_catapult;
            this.castleSelectionCatapultImage.Position = new Point(-10, -20);
            this.castleSelectionInset5Image.addControl(this.castleSelectionCatapultImage);
            this.castleSelectionCatapultInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castleSelectionCatapultInset.Position = new Point(70, 60);
            this.castleSelectionCatapultImage.addControl(this.castleSelectionCatapultInset);
            this.castleSelectionCatapultLabel.Text = "0";
            this.castleSelectionCatapultLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castleSelectionCatapultLabel.Position = new Point(0, 0);
            this.castleSelectionCatapultLabel.Size = this.castleSelectionCatapultInset.Size;
            this.castleSelectionCatapultLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castleSelectionCatapultInset.addControl(this.castleSelectionCatapultLabel);
            this.castleSelectionCatapultDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
            this.castleSelectionCatapultDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
            this.castleSelectionCatapultDeleteButton.Position = new Point(0x7d, 13);
            this.castleSelectionCatapultDeleteButton.Data = 0x5e;
            this.castleSelectionCatapultDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapAttackerSetupPanel_delete_catapults");
            this.castleSelectionInset5Image.addControl(this.castleSelectionCatapultDeleteButton);
            this.castleSelectionInset6Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_off_select;
            this.castleSelectionInset6Image.Position = new Point(3, 0x1ac);
            this.castleSelectionPanelImage.addControl(this.castleSelectionInset6Image);
            this.castleSelectionCaptainImage.Image = (Image) GFXLibrary.r_building_miltary_captain_normal;
            this.castleSelectionCaptainImage.Position = new Point(-10, -20);
            this.castleSelectionInset6Image.addControl(this.castleSelectionCaptainImage);
            this.castleSelectionCaptainInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castleSelectionCaptainInset.Position = new Point(70, 60);
            this.castleSelectionCaptainImage.addControl(this.castleSelectionCaptainInset);
            this.castleSelectionCaptainLabel.Text = "0";
            this.castleSelectionCaptainLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castleSelectionCaptainLabel.Position = new Point(0, 0);
            this.castleSelectionCaptainLabel.Size = this.castleSelectionCaptainInset.Size;
            this.castleSelectionCaptainLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.castleSelectionCaptainInset.addControl(this.castleSelectionCaptainLabel);
            this.castleSelectionCaptainDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
            this.castleSelectionCaptainDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
            this.castleSelectionCaptainDeleteButton.Position = new Point(0x7d, 13);
            this.castleSelectionCaptainDeleteButton.Data = 100;
            this.castleSelectionCaptainDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapAttackerSetupPanel_delete_captains");
            this.castleSelectionInset6Image.addControl(this.castleSelectionCaptainDeleteButton);
        }

        public void initSelectionPanel_Captain()
        {
            this.captain_castleSelectionBackgroundArea.Position = new Point(0, 0);
            this.captain_castleSelectionBackgroundArea.Size = base.Size;
            this.captain_castleSelectionBackgroundArea.Visible = false;
            base.addControl(this.captain_castleSelectionBackgroundArea);
            this.captain_castleSelectionPanelImage.Image = (Image) GFXLibrary.castlescreen_panelback_B;
            this.captain_castleSelectionPanelImage.Position = new Point(0, 0);
            this.captain_castleSelectionBackgroundArea.addControl(this.captain_castleSelectionPanelImage);
            this.captain_closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.captain_closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.captain_closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.captain_closeButton.Position = new Point(0x99, 6);
            this.captain_closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "CastleMapAttackerSetupPanel_close");
            this.captain_castleSelectionBackgroundArea.addControl(this.captain_closeButton);
            this.captain_castleSelectionInset6Image.Image = (Image) GFXLibrary.castlescreen_panel_halfinset_off_select;
            this.captain_castleSelectionInset6Image.Position = new Point(3, 0x1c);
            this.captain_castleSelectionPanelImage.addControl(this.captain_castleSelectionInset6Image);
            this.captain_castleSelectionCaptainImage.Image = (Image) GFXLibrary.r_building_miltary_captain_normal;
            this.captain_castleSelectionCaptainImage.Position = new Point(-10, -20);
            this.captain_castleSelectionInset6Image.addControl(this.captain_castleSelectionCaptainImage);
            this.captain_castleSelectionCaptainInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.captain_castleSelectionCaptainInset.Position = new Point(70, 60);
            this.captain_castleSelectionCaptainImage.addControl(this.captain_castleSelectionCaptainInset);
            this.captain_castleSelectionCaptainLabel.Text = "0";
            this.captain_castleSelectionCaptainLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.captain_castleSelectionCaptainLabel.Position = new Point(0, 0);
            this.captain_castleSelectionCaptainLabel.Size = this.castleSelectionCaptainInset.Size;
            this.captain_castleSelectionCaptainLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            this.captain_castleSelectionCaptainInset.addControl(this.captain_castleSelectionCaptainLabel);
            this.captain_castleSelectionCaptainDeleteButton.ImageNorm = (Image) GFXLibrary.castlescreen_sendback_normal;
            this.captain_castleSelectionCaptainDeleteButton.ImageOver = (Image) GFXLibrary.castlescreen_sendback_over;
            this.captain_castleSelectionCaptainDeleteButton.Position = new Point(0x7d, 13);
            this.captain_castleSelectionCaptainDeleteButton.Data = 100;
            this.captain_castleSelectionCaptainDeleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.troopDeleteClick), "CastleMapAttackerSetupPanel_delete_captains");
            this.captain_castleSelectionInset6Image.addControl(this.captain_castleSelectionCaptainDeleteButton);
            this.captain_castleSelectionCommand1.ImageNorm = (Image) GFXLibrary.captains_commands_icons[0x10];
            this.captain_castleSelectionCommand1.ImageOver = (Image) GFXLibrary.captains_commands_icons[8];
            this.captain_castleSelectionCommand1.Position = new Point(0x15, 100);
            this.captain_castleSelectionCommand1.Data = 100;
            this.captain_castleSelectionCommand1.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.captainCommandClick), "CastleMapAttackerSetupPanel_captain_command_1");
            this.captain_castleSelectionPanelImage.addControl(this.captain_castleSelectionCommand1);
            this.captain_castleSelectionCommand2.ImageNorm = (Image) GFXLibrary.captains_commands_icons[0x11];
            this.captain_castleSelectionCommand2.ImageOver = (Image) GFXLibrary.captains_commands_icons[9];
            this.captain_castleSelectionCommand2.Position = new Point(0x65, 100);
            this.captain_castleSelectionCommand2.Data = 0x65;
            this.captain_castleSelectionCommand2.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.captainCommandClick), "CastleMapAttackerSetupPanel_captain_command_2");
            this.captain_castleSelectionPanelImage.addControl(this.captain_castleSelectionCommand2);
            this.captain_castleSelectionCommand3.ImageNorm = (Image) GFXLibrary.captains_commands_icons[0x12];
            this.captain_castleSelectionCommand3.ImageOver = (Image) GFXLibrary.captains_commands_icons[10];
            this.captain_castleSelectionCommand3.Position = new Point(0x15, 180);
            this.captain_castleSelectionCommand3.Data = 0x66;
            this.captain_castleSelectionCommand3.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.captainCommandClick), "CastleMapAttackerSetupPanel_captain_command_3");
            this.captain_castleSelectionPanelImage.addControl(this.captain_castleSelectionCommand3);
            this.captain_castleSelectionCommand4.ImageNorm = (Image) GFXLibrary.captains_commands_icons[0x13];
            this.captain_castleSelectionCommand4.ImageOver = (Image) GFXLibrary.captains_commands_icons[11];
            this.captain_castleSelectionCommand4.Position = new Point(0x15, 260);
            this.captain_castleSelectionCommand4.Data = 0x67;
            this.captain_castleSelectionCommand4.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.captainCommandClick), "CastleMapAttackerSetupPanel_captain_command_4");
            this.captain_castleSelectionPanelImage.addControl(this.captain_castleSelectionCommand4);
            this.captain_castleSelectionCommand5.ImageNorm = (Image) GFXLibrary.captains_commands_icons[20];
            this.captain_castleSelectionCommand5.ImageOver = (Image) GFXLibrary.captains_commands_icons[12];
            this.captain_castleSelectionCommand5.Position = new Point(0x65, 180);
            this.captain_castleSelectionCommand5.Data = 0x68;
            this.captain_castleSelectionCommand5.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.captainCommandClick), "CastleMapAttackerSetupPanel_captain_command_5");
            this.captain_castleSelectionPanelImage.addControl(this.captain_castleSelectionCommand5);
            this.captain_castleSelectionCommand6.ImageNorm = (Image) GFXLibrary.captains_commands_icons[0x15];
            this.captain_castleSelectionCommand6.ImageOver = (Image) GFXLibrary.captains_commands_icons[13];
            this.captain_castleSelectionCommand6.Position = new Point(0x65, 260);
            this.captain_castleSelectionCommand6.Data = 0x69;
            this.captain_castleSelectionCommand6.Visible = false;
            this.captain_castleSelectionCommand6.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.captainCommandClick), "CastleMapAttackerSetupPanel_captain_command_6");
            this.captain_castleSelectionPanelImage.addControl(this.captain_castleSelectionCommand6);
            this.captain_castleSelectionCommand7.ImageNorm = (Image) GFXLibrary.captains_commands_icons[0x16];
            this.captain_castleSelectionCommand7.ImageOver = (Image) GFXLibrary.captains_commands_icons[14];
            this.captain_castleSelectionCommand7.Position = new Point(0x15, 340);
            this.captain_castleSelectionCommand7.Data = 0x6a;
            this.captain_castleSelectionCommand7.Visible = false;
            this.captain_castleSelectionCommand7.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.captainCommandClick), "CastleMapAttackerSetupPanel_captain_command_7");
            this.captain_castleSelectionPanelImage.addControl(this.captain_castleSelectionCommand7);
            this.captain_castleSelectionCommand8.ImageNorm = (Image) GFXLibrary.captains_commands_icons[0x17];
            this.captain_castleSelectionCommand8.ImageOver = (Image) GFXLibrary.captains_commands_icons[15];
            this.captain_castleSelectionCommand8.Position = new Point(0x65, 340);
            this.captain_castleSelectionCommand8.Data = 0x6b;
            this.captain_castleSelectionCommand8.Visible = false;
            this.captain_castleSelectionCommand8.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.captainCommandClick), "CastleMapAttackerSetupPanel_captain_command_8");
            this.captain_castleSelectionPanelImage.addControl(this.captain_castleSelectionCommand8);
            this.captain_castleSelectionCaptainCommandLabel.Text = SK.Text("CastleMapAttackerSetup_Command", "Command");
            this.captain_castleSelectionCaptainCommandLabel.Color = ARGBColors.Black;
            this.captain_castleSelectionCaptainCommandLabel.Position = new Point(0, 0x1a5);
            this.captain_castleSelectionCaptainCommandLabel.Size = new Size(this.captain_castleSelectionPanelImage.Size.Width, 30);
            this.captain_castleSelectionCaptainCommandLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.captain_castleSelectionCaptainCommandLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.captain_castleSelectionPanelImage.addControl(this.captain_castleSelectionCaptainCommandLabel);
            this.captain_commandValueTrack.Position = new Point(0x20, 0x1bc);
            this.captain_commandValueTrack.Margin = new Rectangle(3, -1, 1, 0);
            this.captain_commandValueTrack.Value = 0;
            this.captain_commandValueTrack.Max = 0x27;
            this.captain_commandValueTrack.setValueChangeDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ValueChangedDelegate(this.tracksMoved));
            this.captain_castleSelectionPanelImage.addControl(this.captain_commandValueTrack);
            this.captain_commandValueTrack.Create((Image) GFXLibrary.int_slidebar_ruler, (Image) GFXLibrary.int_slidebar_thumb_middle_normal, (Image) GFXLibrary.int_slidebar_thumb_left_normal, (Image) GFXLibrary.int_slidebar_thumb_right_normal, (Image) GFXLibrary.int_slidebar_thumb_middle_in, (Image) GFXLibrary.int_slidebar_thumb_middle_over);
            this.captain_castleSelectionCaptainSecondsCountLabel.Text = "0";
            this.captain_castleSelectionCaptainSecondsCountLabel.Color = ARGBColors.Black;
            this.captain_castleSelectionCaptainSecondsCountLabel.Position = new Point(0, 0x1df);
            this.captain_castleSelectionCaptainSecondsCountLabel.Size = new Size(100, 30);
            this.captain_castleSelectionCaptainSecondsCountLabel.Font = FontManager.GetFont("Arial", 16f, FontStyle.Bold);
            this.captain_castleSelectionCaptainSecondsCountLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_RIGHT;
            this.captain_castleSelectionPanelImage.addControl(this.captain_castleSelectionCaptainSecondsCountLabel);
            this.captain_castleSelectionCaptainSecondsLabel.Text = SK.Text("CastleMapAttackerSetup_Seconds", "Seconds");
            this.captain_castleSelectionCaptainSecondsLabel.Color = ARGBColors.Black;
            this.captain_castleSelectionCaptainSecondsLabel.Position = new Point(100, 0x1e4);
            this.captain_castleSelectionCaptainSecondsLabel.Size = new Size(this.captain_castleSelectionPanelImage.Size.Width - 100, 30);
            this.captain_castleSelectionCaptainSecondsLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Bold);
            this.captain_castleSelectionCaptainSecondsLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_LEFT;
            this.captain_castleSelectionPanelImage.addControl(this.captain_castleSelectionCaptainSecondsLabel);
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        private void launchAttack()
        {
            if (this.loadButton.Visible)
            {
                GameEngine.Instance.CastleAttackerSetup.launchArmy();
            }
            else
            {
                this.armyLaunched = false;
                SendArmyWindow window = InterfaceMgr.Instance.openLaunchAttackPopup();
                string villageName = "";
                int attackRealTargetVillage = GameEngine.Instance.CastleAttackerSetup.attackRealTargetVillage;
                int attackRealAttackingVillage = GameEngine.Instance.CastleAttackerSetup.attackRealAttackingVillage;
                int parentOfAttackingVillage = GameEngine.Instance.CastleAttackerSetup.ParentOfAttackingVillage;
                if (GameEngine.Instance.World.isSpecial(attackRealTargetVillage))
                {
                    villageName = SpecialVillageTypes.getName(GameEngine.Instance.World.getSpecial(attackRealTargetVillage), Program.mySettings.LanguageIdent);
                }
                else if ((GameEngine.Instance.CastleAttackerSetup.m_targetUserName != null) && (GameEngine.Instance.CastleAttackerSetup.m_targetUserName.Length > 0))
                {
                    villageName = GameEngine.Instance.World.getVillageName(attackRealTargetVillage) + " (" + GameEngine.Instance.CastleAttackerSetup.m_targetUserName + ")";
                }
                else
                {
                    villageName = GameEngine.Instance.World.getVillageName(attackRealTargetVillage);
                }
                WorldData localWorldData = GameEngine.Instance.LocalWorldData;
                Point point = GameEngine.Instance.World.getVillageLocation(attackRealAttackingVillage);
                Point point2 = GameEngine.Instance.World.getVillageLocation(attackRealTargetVillage);
                int x = point.X;
                int y = point.Y;
                int num7 = point2.X;
                int num8 = point2.Y;
                double d = ((x - num7) * (x - num7)) + ((y - num8) * (y - num8));
                d = Math.Sqrt(d);
                if (!GameEngine.Instance.World.isCapital(parentOfAttackingVillage) && !GameEngine.Instance.World.isCapital(attackRealAttackingVillage))
                {
                    if (GameEngine.Instance.CastleAttackerSetup.containsCaptain())
                    {
                        d *= (localWorldData.CaptainsMoveSpeed * localWorldData.gamePlaySpeed) * ResearchData.CaptainTimes[GameEngine.Instance.World.GetResearchDataForCurrentVillage().Research_Courtiers];
                    }
                    else
                    {
                        d *= (localWorldData.armyMoveSpeed * localWorldData.gamePlaySpeed) * ResearchData.ArmyTimes[GameEngine.Instance.World.GetResearchDataForCurrentVillage().Research_ForcedMarch];
                    }
                }
                else
                {
                    double capitalAttackRate = GameEngine.Instance.CastleAttackerSetup.CapitalAttackRate;
                    if (capitalAttackRate == 0.0)
                    {
                        capitalAttackRate = 1.0;
                    }
                    d *= (localWorldData.armyMoveSpeed * localWorldData.gamePlaySpeed) * capitalAttackRate;
                }
                bool gotCaptain = GameEngine.Instance.CastleAttackerSetup.captainPlaced();
                window.init(parentOfAttackingVillage, attackRealAttackingVillage, attackRealTargetVillage, villageName, d, GameEngine.Instance.CastleAttackerSetup.m_battleHonourData, gotCaptain, this);
                GameEngine.Instance.DisableMouseClicks();
                bool armyLaunched = this.armyLaunched;
            }
        }

        public void launched()
        {
            this.armyLaunched = true;
            this.launchHeaderButton.Enabled = false;
        }

        public void Run()
        {
        }

        private void setPlaceSize(int size)
        {
            this.placementSize = size;
            switch (size)
            {
                case 1:
                    if (GameEngine.Instance.CastleAttackerSetup != null)
                    {
                        GameEngine.Instance.CastleAttackerSetup.setPlacementSize(1);
                    }
                    this.castlePlaceSize1Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x1_selected;
                    this.castlePlaceSize1Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x1_selected;
                    this.castlePlaceSize3Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_3x3_normal;
                    this.castlePlaceSize3Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_3x3_over;
                    this.castlePlaceSize5Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_5x5_normal;
                    this.castlePlaceSize5Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_5x5_over;
                    this.castlePlaceSize15Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x5_normal;
                    this.castlePlaceSize15Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x5_over;
                    return;

                case 2:
                case 4:
                    break;

                case 3:
                    if (GameEngine.Instance.CastleAttackerSetup != null)
                    {
                        GameEngine.Instance.CastleAttackerSetup.setPlacementSize(2);
                    }
                    this.castlePlaceSize1Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x1_normal;
                    this.castlePlaceSize1Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x1_over;
                    this.castlePlaceSize3Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_3x3_selected;
                    this.castlePlaceSize3Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_3x3_selected;
                    this.castlePlaceSize5Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_5x5_normal;
                    this.castlePlaceSize5Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_5x5_over;
                    this.castlePlaceSize15Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x5_normal;
                    this.castlePlaceSize15Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x5_over;
                    return;

                case 5:
                    if (GameEngine.Instance.CastleAttackerSetup != null)
                    {
                        GameEngine.Instance.CastleAttackerSetup.setPlacementSize(3);
                    }
                    this.castlePlaceSize1Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x1_normal;
                    this.castlePlaceSize1Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x1_over;
                    this.castlePlaceSize3Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_3x3_normal;
                    this.castlePlaceSize3Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_3x3_over;
                    this.castlePlaceSize5Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_5x5_selected;
                    this.castlePlaceSize5Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_5x5_selected;
                    this.castlePlaceSize15Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x5_normal;
                    this.castlePlaceSize15Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x5_over;
                    return;

                case 15:
                    if (GameEngine.Instance.CastleAttackerSetup != null)
                    {
                        GameEngine.Instance.CastleAttackerSetup.setPlacementSize(4);
                    }
                    this.castlePlaceSize1Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x1_normal;
                    this.castlePlaceSize1Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x1_over;
                    this.castlePlaceSize3Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_3x3_normal;
                    this.castlePlaceSize3Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_3x3_over;
                    this.castlePlaceSize5Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_5x5_normal;
                    this.castlePlaceSize5Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_5x5_over;
                    this.castlePlaceSize15Button.ImageNorm = (Image) GFXLibrary.castlescreen_unitbrush_1x5_selected;
                    this.castlePlaceSize15Button.ImageOver = (Image) GFXLibrary.castlescreen_unitbrush_1x5_selected;
                    break;

                default:
                    return;
            }
        }

        public void setSelectedTroop(int numPeasants, int numArchers, int numPikemen, int numSwordsmen, int numCatapults, int numCaptains, int captainsCommand, int captainsData)
        {
            GameEngine.Instance.playInterfaceSound("CastleMapPanel_open_selected_troops_panel");
            if ((((numCaptains == 1) && (numPeasants == 0)) && ((numArchers == 0) && (numPikemen == 0))) && ((numSwordsmen == 0) && (numCatapults == 0)))
            {
                if (!this.captain_castleSelectionBackgroundArea.Visible)
                {
                    int num = 0;
                    if (captainsData > 0)
                    {
                        num = (captainsData / 5) - 1;
                        if (num < 0)
                        {
                            num = 0;
                        }
                    }
                    this.captain_commandValueTrack.Value = num;
                    this.tracksMoved();
                    this.updateCaptainCommands(captainsCommand);
                }
                this.castleSelectionBackgroundArea.Visible = false;
                this.captain_castleSelectionBackgroundArea.Visible = true;
                this.castlePlaceBackgroundArea.Visible = false;
                this.captain_castleSelectionCaptainLabel.Text = numCaptains.ToString();
                this.captain_castleSelectionCaptainDeleteButton.Enabled = numCaptains > 0;
            }
            else
            {
                this.castleSelectionBackgroundArea.Visible = true;
                this.captain_castleSelectionBackgroundArea.Visible = false;
                this.castlePlaceBackgroundArea.Visible = false;
                this.castleSelectionPeasantLabel.Text = numPeasants.ToString();
                this.castleSelectionArcherLabel.Text = numArchers.ToString();
                this.castleSelectionPikemanLabel.Text = numPikemen.ToString();
                this.castleSelectionSwordsmanLabel.Text = numSwordsmen.ToString();
                this.castleSelectionCatapultLabel.Text = numCatapults.ToString();
                this.castleSelectionCaptainLabel.Text = numCaptains.ToString();
                this.castleSelectionPeasantDeleteButton.Enabled = numPeasants > 0;
                this.castleSelectionArcherDeleteButton.Enabled = numArchers > 0;
                this.castleSelectionPikemanDeleteButton.Enabled = numPikemen > 0;
                this.castleSelectionSwordsmanDeleteButton.Enabled = numSwordsmen > 0;
                this.castleSelectionCatapultDeleteButton.Enabled = numCatapults > 0;
                this.castleSelectionCaptainDeleteButton.Enabled = numCaptains > 0;
            }
        }

        public void setStats(int numArchers, int numPikemen, int numSwordsmen, int numPeasants, int numCatapults, int maxPeasants, int maxArchers, int maxPikemen, int maxSwordsmen, int maxCatapults, int numCaptains, int maxCaptains, int captainsCommand, int numPeasantsInCastle, int numArchersInCastle, int numPikemenInCastle, int numSwordsmenInCastle)
        {
            int num = maxPeasants - numPeasants;
            int num2 = maxArchers - numArchers;
            int num3 = maxPikemen - numPikemen;
            int num4 = maxSwordsmen - numSwordsmen;
            int num5 = maxCatapults - numCatapults;
            int num6 = maxCaptains - numCaptains;
            bool flag = false;
            if (num <= 0)
            {
                num += numPeasantsInCastle;
                if (num > 0)
                {
                    flag = true;
                }
            }
            bool flag2 = false;
            if (num2 <= 0)
            {
                num2 += numArchersInCastle;
                if (num2 > 0)
                {
                    flag2 = true;
                }
            }
            bool flag3 = false;
            if (num3 <= 0)
            {
                num3 += numPikemenInCastle;
                if (num3 > 0)
                {
                    flag3 = true;
                }
            }
            bool flag4 = false;
            if (num4 <= 0)
            {
                num4 += numSwordsmenInCastle;
                if (num4 > 0)
                {
                    flag4 = true;
                }
            }
            if (num <= 0)
            {
                num = 0;
                this.castlePlacePeasantButton.Enabled = false;
            }
            else
            {
                this.castlePlacePeasantButton.Enabled = true;
            }
            if (num2 <= 0)
            {
                num2 = 0;
                this.castlePlaceArcherButton.Enabled = false;
            }
            else
            {
                this.castlePlaceArcherButton.Enabled = true;
            }
            if (num3 <= 0)
            {
                num3 = 0;
                this.castlePlacePikemanButton.Enabled = false;
            }
            else
            {
                this.castlePlacePikemanButton.Enabled = true;
            }
            if (num4 <= 0)
            {
                num4 = 0;
                this.castlePlaceSwordsmanButton.Enabled = false;
            }
            else
            {
                this.castlePlaceSwordsmanButton.Enabled = true;
            }
            if (num5 <= 0)
            {
                num5 = 0;
                this.castlePlaceCatapultButton.Enabled = false;
            }
            else
            {
                this.castlePlaceCatapultButton.Enabled = true;
            }
            if (num6 <= 0)
            {
                num6 = 0;
                this.castlePlaceCaptainButton.Enabled = false;
            }
            else
            {
                this.castlePlaceCaptainButton.Enabled = true;
            }
            this.castlePlacePeasantLabel.Text = num.ToString();
            this.castlePlaceArcherLabel.Text = num2.ToString();
            this.castlePlacePikemanLabel.Text = num3.ToString();
            this.castlePlaceSwordsmanLabel.Text = num4.ToString();
            this.castlePlaceCatapultLabel.Text = num5.ToString();
            this.castlePlaceCaptainLabel.Text = num6.ToString();
            this.castlePlacePeasantCastle.Visible = flag;
            this.castlePlaceArcherCastle.Visible = flag2;
            this.castlePlacePikemanCastle.Visible = flag3;
            this.castlePlaceSwordsmanCastle.Visible = flag4;
        }

        public void setTimes(DateTime castleViewTime, bool castleAvailable, DateTime troopViewTime, bool troopAvailable)
        {
        }

        public void showAttackReady(bool state)
        {
            if (!this.armyLaunched)
            {
                this.launchHeaderButton.Enabled = state;
            }
        }

        public void showRealAttack(bool state)
        {
            this.armyLaunched = false;
            if (state)
            {
                this.loadButton.Visible = false;
                this.saveButton.Visible = false;
                this.aiExportButton.Visible = false;
                this.launchHeaderButton.Enabled = false;
            }
            else
            {
                this.loadButton.Visible = true;
                this.saveButton.Visible = true;
                this.aiExportButton.Visible = true;
                this.launchHeaderButton.Enabled = true;
            }
        }

        public void tracksMoved()
        {
            int num = (this.captain_commandValueTrack.Value + 1) * 5;
            this.captain_castleSelectionCaptainSecondsCountLabel.Text = num.ToString();
            GameEngine.Instance.CastleAttackerSetup.updateCaptainsDetails(num);
        }

        private void troopDeleteClick()
        {
            if (base.OverControl != null)
            {
                int data = base.OverControl.Data;
                GameEngine.Instance.CastleAttackerSetup.startDeleteAttackingTroops(data);
                if (data == 100)
                {
                    GameEngine.Instance.CastleAttackerSetup.startDeleteAttackingTroops(0x66);
                    GameEngine.Instance.CastleAttackerSetup.startDeleteAttackingTroops(0x68);
                    GameEngine.Instance.CastleAttackerSetup.startDeleteAttackingTroops(0x67);
                    GameEngine.Instance.CastleAttackerSetup.startDeleteAttackingTroops(0x69);
                    GameEngine.Instance.CastleAttackerSetup.startDeleteAttackingTroops(0x6a);
                    GameEngine.Instance.CastleAttackerSetup.startDeleteAttackingTroops(0x6b);
                    GameEngine.Instance.CastleAttackerSetup.startDeleteAttackingTroops(0x65);
                }
            }
        }

        public void updateCaptainCommands(int activeCommand)
        {
            this.captain_castleSelectionCommand1.ImageNorm = (Image) GFXLibrary.captains_commands_icons[0x10];
            this.captain_castleSelectionCommand1.ImageOver = (Image) GFXLibrary.captains_commands_icons[0];
            this.captain_castleSelectionCommand2.ImageNorm = (Image) GFXLibrary.captains_commands_icons[0x11];
            this.captain_castleSelectionCommand2.ImageOver = (Image) GFXLibrary.captains_commands_icons[1];
            this.captain_castleSelectionCommand3.ImageNorm = (Image) GFXLibrary.captains_commands_icons[0x12];
            this.captain_castleSelectionCommand3.ImageOver = (Image) GFXLibrary.captains_commands_icons[2];
            this.captain_castleSelectionCommand4.ImageNorm = (Image) GFXLibrary.captains_commands_icons[0x13];
            this.captain_castleSelectionCommand4.ImageOver = (Image) GFXLibrary.captains_commands_icons[3];
            this.captain_castleSelectionCommand5.ImageNorm = (Image) GFXLibrary.captains_commands_icons[20];
            this.captain_castleSelectionCommand5.ImageOver = (Image) GFXLibrary.captains_commands_icons[4];
            this.captain_castleSelectionCommand6.ImageNorm = (Image) GFXLibrary.captains_commands_icons[0x15];
            this.captain_castleSelectionCommand6.ImageOver = (Image) GFXLibrary.captains_commands_icons[5];
            this.captain_castleSelectionCommand7.ImageNorm = (Image) GFXLibrary.captains_commands_icons[0x16];
            this.captain_castleSelectionCommand7.ImageOver = (Image) GFXLibrary.captains_commands_icons[6];
            this.captain_castleSelectionCommand8.ImageNorm = (Image) GFXLibrary.captains_commands_icons[0x17];
            this.captain_castleSelectionCommand8.ImageOver = (Image) GFXLibrary.captains_commands_icons[7];
            switch (activeCommand)
            {
                case 100:
                    this.captain_castleSelectionCommand1.ImageNorm = (Image) GFXLibrary.captains_commands_icons[8];
                    this.captain_castleSelectionCaptainCommandLabel.Text = SK.Text("CAPTAINS_COMMANDS_", "Delay");
                    break;

                case 0x65:
                    this.captain_castleSelectionCommand2.ImageNorm = (Image) GFXLibrary.captains_commands_icons[9];
                    this.captain_castleSelectionCaptainCommandLabel.Text = SK.Text("CAPTAINS_COMMANDS_Rallying_Cry", "Rallying Cry");
                    break;

                case 0x66:
                    this.captain_castleSelectionCommand3.ImageNorm = (Image) GFXLibrary.captains_commands_icons[10];
                    this.captain_castleSelectionCaptainCommandLabel.Text = SK.Text("CAPTAINS_COMMANDS_Arrow_Volley", "Arrow Volley");
                    break;

                case 0x67:
                    this.captain_castleSelectionCommand4.ImageNorm = (Image) GFXLibrary.captains_commands_icons[11];
                    this.captain_castleSelectionCaptainCommandLabel.Text = SK.Text("CAPTAINS_COMMANDS_Catapult_Volley", "Catapult Volley");
                    break;

                case 0x68:
                    this.captain_castleSelectionCommand5.ImageNorm = (Image) GFXLibrary.captains_commands_icons[12];
                    this.captain_castleSelectionCaptainCommandLabel.Text = SK.Text("CAPTAINS_COMMANDS_Battle_Cry", "Battle Cry");
                    break;

                case 0x69:
                    this.captain_castleSelectionCommand6.ImageNorm = (Image) GFXLibrary.captains_commands_icons[13];
                    this.captain_castleSelectionCaptainCommandLabel.Text = "Command 6";
                    break;

                case 0x6a:
                    this.captain_castleSelectionCommand7.ImageNorm = (Image) GFXLibrary.captains_commands_icons[14];
                    this.captain_castleSelectionCaptainCommandLabel.Text = "Command 7";
                    break;

                case 0x6b:
                    this.captain_castleSelectionCommand8.ImageNorm = (Image) GFXLibrary.captains_commands_icons[15];
                    this.captain_castleSelectionCaptainCommandLabel.Text = "Command 8";
                    break;
            }
            int num = GameEngine.Instance.World.UserResearchData.Research_Tactics;
            if (num < 1)
            {
                this.captain_castleSelectionCommand2.Alpha = 0.5f;
                this.captain_castleSelectionCommand2.Enabled = false;
            }
            else
            {
                this.captain_castleSelectionCommand2.Alpha = 1f;
                this.captain_castleSelectionCommand2.Enabled = true;
            }
            if (num < 2)
            {
                this.captain_castleSelectionCommand3.Alpha = 0.5f;
                this.captain_castleSelectionCommand3.Enabled = false;
            }
            else
            {
                this.captain_castleSelectionCommand3.Alpha = 1f;
                this.captain_castleSelectionCommand3.Enabled = true;
            }
            if (num < 4)
            {
                this.captain_castleSelectionCommand4.Alpha = 0.5f;
                this.captain_castleSelectionCommand4.Enabled = false;
            }
            else
            {
                this.captain_castleSelectionCommand4.Alpha = 1f;
                this.captain_castleSelectionCommand4.Enabled = true;
            }
            if (num < 3)
            {
                this.captain_castleSelectionCommand5.Alpha = 0.5f;
                this.captain_castleSelectionCommand5.Enabled = false;
            }
            else
            {
                this.captain_castleSelectionCommand5.Alpha = 1f;
                this.captain_castleSelectionCommand5.Enabled = true;
            }
            if (num < 6)
            {
                this.captain_castleSelectionCommand6.Alpha = 0.5f;
                this.captain_castleSelectionCommand6.Enabled = false;
            }
            else
            {
                this.captain_castleSelectionCommand6.Alpha = 1f;
                this.captain_castleSelectionCommand6.Enabled = true;
            }
            if (num < 7)
            {
                this.captain_castleSelectionCommand7.Alpha = 0.5f;
                this.captain_castleSelectionCommand7.Enabled = false;
            }
            else
            {
                this.captain_castleSelectionCommand7.Alpha = 1f;
                this.captain_castleSelectionCommand7.Enabled = true;
            }
            if (num < 8)
            {
                this.captain_castleSelectionCommand8.Alpha = 0.5f;
                this.captain_castleSelectionCommand8.Enabled = false;
            }
            else
            {
                this.captain_castleSelectionCommand8.Alpha = 1f;
                this.captain_castleSelectionCommand8.Enabled = true;
            }
        }

        private void utilAdvancedClick()
        {
            if (GameEngine.Instance.CastleAttackerSetup != null)
            {
                InterfaceMgr.Instance.openFormationPopup();
            }
        }
    }
}

