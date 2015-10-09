namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class FormationPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDButton castlePlaceArcherButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlaceArcherInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlaceArcherLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castlePlaceCaptainButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlaceCaptainInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlaceCaptainLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castlePlaceCatapultButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlaceCatapultInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlaceCatapultLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castlePlacePeasantButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlacePeasantInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlacePeasantLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castlePlacePikemanButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlacePikemanInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlacePikemanLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton castlePlaceSwordsmanButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDImage castlePlaceSwordsmanInset = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel castlePlaceSwordsmanLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton clearButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton createButton = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDLabel createLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel createNameLabel = new CustomSelfDrawPanel.CSDLabel();
        private MyMessageBoxPopUp createPopUp;
        private CustomSelfDrawPanel.CSDButton deleteButton = new CustomSelfDrawPanel.CSDButton();
        private MyMessageBoxPopUp deletePopUp;
        private CustomSelfDrawPanel.CSDImage formationImage = new CustomSelfDrawPanel.CSDImage();
        private List<CustomSelfDrawPanel.CSDListItem> formationNames = new List<CustomSelfDrawPanel.CSDListItem>();
        private CustomSelfDrawPanel.CSDButton loadButton = new CustomSelfDrawPanel.CSDButton();
        private FormationPopup m_parent;
        private CustomSelfDrawPanel.CSDLabel manageLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDButton renameButton = new CustomSelfDrawPanel.CSDButton();
        private string saveName = "";
        private CustomSelfDrawPanel.CSDLabel selectedLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel selectedTitleLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel selectedTroopCountLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel storedLabel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDListBox storedList = new CustomSelfDrawPanel.CSDListBox();
        private CustomSelfDrawPanel.CSDButton totalsButton = new CustomSelfDrawPanel.CSDButton();

        public FormationPanel()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void clearClick()
        {
            GameEngine.Instance.CastleAttackerSetup.deleteAllAttackers();
        }

        private void closeClick(CustomSelfDrawPanel.CSDListItem item)
        {
        }

        private void CloseCreatePopUp()
        {
            if (this.createPopUp != null)
            {
                if (this.createPopUp.Created)
                {
                    this.createPopUp.Close();
                }
                this.createPopUp = null;
            }
        }

        private void CloseDeletePopUp()
        {
            if (this.deletePopUp != null)
            {
                if (this.deletePopUp.Created)
                {
                    this.deletePopUp.Close();
                }
                this.deletePopUp = null;
            }
        }

        private void createClick()
        {
            this.saveName = this.m_parent.getCreateText();
            if (!this.storedList.contains(this.saveName) || (MyMessageBox.Show(SK.Text("Formations_Overwrite", "That name is already in use. Do you want to replace the existing formation?"), SK.Text("Formations_Overwrite_Title", "Name Already in Use"), MessageBoxButtons.YesNo) != DialogResult.No))
            {
                this.SharedCreateCode();
            }
        }

        private void CreatePopUpOkClicked()
        {
            this.SharedCreateCode();
            this.createPopUp.Close();
        }

        private void deleteClick()
        {
            if ((this.storedList.getSelectedItem() != null) && (MyMessageBox.Show(SK.Text("Formations_Confirm_Delete", "Are you sure you want to delete this formation?"), SK.Text("Formations_Delete_Confirmation_Title", "Confirm Deletion"), MessageBoxButtons.YesNo) != DialogResult.No))
            {
                this.SharedDeleteCode();
            }
        }

        private void DeletePopUpOkClicked()
        {
            this.SharedDeleteCode();
            this.deletePopUp.Close();
        }

        public void init(FormationPopup parent)
        {
            this.m_parent = parent;
            base.Size = this.m_parent.Size;
            this.BackColor = ARGBColors.Transparent;
            CustomSelfDrawPanel.CSDImage control = new CustomSelfDrawPanel.CSDImage {
                Alpha = 0.1f,
                Image = (Image) GFXLibrary.formations_img,
                Scale = 5.0,
                Position = new Point(0, 0),
                Size = base.Size
            };
            base.addControl(control);
            this.manageLabel.Text = SK.Text("Formations_Manage_Box", "Manage Formations");
            this.manageLabel.Color = ARGBColors.White;
            this.manageLabel.DropShadowColor = ARGBColors.Black;
            this.manageLabel.Position = new Point(base.Width / 3, 5);
            this.manageLabel.Size = new Size(base.Width / 3, 0x18);
            this.manageLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.manageLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            base.addControl(this.manageLabel);
            this.storedLabel.Text = SK.Text("Formations_Stored", "Stored Formations");
            this.storedLabel.Color = ARGBColors.White;
            this.storedLabel.DropShadowColor = ARGBColors.Black;
            this.storedLabel.Position = new Point(base.Width / 3, 0x19);
            this.storedLabel.Size = new Size(base.Width / 3, 0x18);
            this.storedLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.storedLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            base.addControl(this.storedLabel);
            this.storedList.Size = new Size(190, 0xd8);
            this.storedList.Position = new Point((base.Width / 2) - (this.storedList.Width / 2), 0x2d);
            base.addControl(this.storedList);
            this.storedList.Create(12, 0x12);
            this.storedList.setLineClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.listClick));
            this.storedList.setDoubleClickedDelegate(new CustomSelfDrawPanel.CSDListBox.CSD_LineClickedDelegate(this.listDoubleClick));
            this.loadButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.loadButton.ImageOver = (Image) GFXLibrary.button_132_over;
            this.loadButton.ImageClick = (Image) GFXLibrary.button_132_in;
            this.loadButton.setSizeToImage();
            this.loadButton.Position = new Point((base.Width / 2) - (this.loadButton.Width / 2), this.storedList.Rectangle.Bottom + 5);
            this.loadButton.Text.Text = SK.Text("Formations_Load", "Load Formation");
            this.loadButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.loadButton.TextYOffset = -2;
            this.loadButton.Text.Color = ARGBColors.Black;
            this.loadButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.loadClick), "Formation_Load");
            this.loadButton.Enabled = true;
            base.addControl(this.loadButton);
            this.deleteButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.deleteButton.ImageOver = (Image) GFXLibrary.button_132_over;
            this.deleteButton.ImageClick = (Image) GFXLibrary.button_132_in;
            this.deleteButton.setSizeToImage();
            this.deleteButton.Position = new Point(this.loadButton.Position.X, this.loadButton.Rectangle.Bottom + 5);
            this.deleteButton.Text.Text = SK.Text("Formations_Delete", "Delete Formation");
            this.deleteButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.deleteButton.TextYOffset = -2;
            this.deleteButton.Text.Color = ARGBColors.Black;
            this.deleteButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.deleteClick), "Formation_Delete");
            this.deleteButton.Enabled = true;
            base.addControl(this.deleteButton);
            this.renameButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.renameButton.ImageOver = (Image) GFXLibrary.button_132_over;
            this.renameButton.ImageClick = (Image) GFXLibrary.button_132_in;
            this.renameButton.setSizeToImage();
            this.renameButton.Position = new Point(this.loadButton.Position.X, this.deleteButton.Rectangle.Bottom + 30);
            this.renameButton.Text.Text = SK.Text("Formations_Rename", "Rename Formation");
            this.renameButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.renameButton.TextYOffset = -2;
            this.renameButton.Text.Color = ARGBColors.Black;
            this.renameButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.renameClick), "Formation_Rename");
            this.renameButton.Enabled = true;
            base.addControl(this.renameButton);
            this.clearButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.clearButton.ImageOver = (Image) GFXLibrary.button_132_over;
            this.clearButton.ImageClick = (Image) GFXLibrary.button_132_in;
            this.clearButton.setSizeToImage();
            this.clearButton.Position = new Point(((5 * base.Width) / 6) - (this.clearButton.Width / 2), this.renameButton.Y);
            this.clearButton.Text.Text = SK.Text("Formations_Clear", "Clear Deployment");
            this.clearButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.clearButton.TextYOffset = -2;
            this.clearButton.Text.Color = ARGBColors.Black;
            this.clearButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.clearClick), "Formation_Clear");
            this.clearButton.Enabled = true;
            base.addControl(this.clearButton);
            this.totalsButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.totalsButton.ImageOver = (Image) GFXLibrary.button_132_over;
            this.totalsButton.ImageClick = (Image) GFXLibrary.button_132_in;
            this.totalsButton.setSizeToImage();
            this.totalsButton.Position = new Point(((5 * base.Width) / 6) - (this.totalsButton.Width / 2), this.deleteButton.Y);
            this.totalsButton.Text.Text = SK.Text("Formations_CurrentTotals", "Current Totals");
            this.totalsButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.totalsButton.TextYOffset = -2;
            this.totalsButton.Text.Color = ARGBColors.Black;
            this.totalsButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.totalsClick), "Formation_CurrentTotals");
            this.totalsButton.Enabled = true;
            base.addControl(this.totalsButton);
            this.formationImage.Image = (Image) GFXLibrary.formations_img;
            this.formationImage.setSizeToImage();
            this.formationImage.Position = new Point((base.Width / 6) - (this.formationImage.Width / 2), (this.storedList.Y + (this.storedList.Height / 2)) - (this.formationImage.Height / 2));
            base.addControl(this.formationImage);
            this.createLabel.Text = SK.Text("Formations_New_Box", "Create New Formation");
            this.createLabel.Color = ARGBColors.White;
            this.createLabel.DropShadowColor = ARGBColors.Black;
            this.createLabel.Position = new Point(0, this.deleteButton.Y);
            this.createLabel.Size = new Size(base.Width / 3, this.deleteButton.Height);
            this.createLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
            this.createLabel.Font = FontManager.GetFont("Arial", 12f, FontStyle.Bold);
            base.addControl(this.createLabel);
            this.createNameLabel.Text = SK.Text("Formations_Name", "Formation Name");
            this.createNameLabel.Color = ARGBColors.White;
            this.createNameLabel.DropShadowColor = ARGBColors.Black;
            this.createNameLabel.Position = new Point(0, this.deleteButton.Y);
            this.createNameLabel.Size = new Size(base.Width / 3, this.deleteButton.Height);
            this.createNameLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.BOTTOM_CENTER;
            this.createNameLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            this.createButton.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.createButton.ImageOver = (Image) GFXLibrary.button_132_over;
            this.createButton.ImageClick = (Image) GFXLibrary.button_132_in;
            this.createButton.setSizeToImage();
            this.createButton.Position = new Point((base.Width / 6) - (this.createButton.Width / 2), this.renameButton.Y);
            this.createButton.Text.Text = SK.Text("Formations_Save", "Save Formation");
            this.createButton.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            this.createButton.TextYOffset = -2;
            this.createButton.Text.Color = ARGBColors.Black;
            this.createButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.createClick), "Formation_Create");
            this.createButton.Enabled = true;
            base.addControl(this.createButton);
            this.castlePlacePeasantButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_peasent;
            this.castlePlacePeasantButton.ImageOver = (Image) GFXLibrary.r_building_miltary_peasent;
            this.castlePlacePeasantButton.setSizeToImage();
            this.castlePlacePeasantButton.Position = new Point((((5 * base.Width) / 6) - this.castlePlacePeasantButton.Width) - 10, this.storedLabel.Y);
            this.castlePlacePeasantButton.Data = 90;
            this.castlePlacePeasantButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlacePeasantInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castlePlacePeasantInset.Position = new Point(0x37, 0x37);
            this.castlePlacePeasantLabel.Text = "0";
            this.castlePlacePeasantLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castlePlacePeasantLabel.Position = new Point(0, -1);
            this.castlePlacePeasantLabel.Size = this.castlePlacePeasantInset.Size;
            this.castlePlacePeasantLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            base.addControl(this.castlePlacePeasantButton);
            this.castlePlacePeasantInset.addControl(this.castlePlacePeasantLabel);
            this.castlePlacePeasantButton.addControl(this.castlePlacePeasantInset);
            this.castlePlacePeasantButton.Active = false;
            this.castlePlaceArcherButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_archer;
            this.castlePlaceArcherButton.ImageOver = (Image) GFXLibrary.r_building_miltary_archer;
            this.castlePlaceArcherButton.setSizeToImage();
            this.castlePlaceArcherButton.Position = new Point(this.castlePlacePeasantButton.Rectangle.Right - 10, this.castlePlacePeasantButton.Position.Y);
            this.castlePlaceArcherButton.Data = 0x5c;
            this.castlePlaceArcherButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlaceArcherInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castlePlaceArcherInset.Position = new Point(0x37, 0x37);
            this.castlePlaceArcherLabel.Text = "0";
            this.castlePlaceArcherLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castlePlaceArcherLabel.Position = new Point(0, -1);
            this.castlePlaceArcherLabel.Size = this.castlePlaceArcherInset.Size;
            this.castlePlaceArcherLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            base.addControl(this.castlePlaceArcherButton);
            this.castlePlaceArcherInset.addControl(this.castlePlaceArcherLabel);
            this.castlePlaceArcherButton.addControl(this.castlePlaceArcherInset);
            this.castlePlaceArcherButton.Active = false;
            this.castlePlacePikemanButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_pikemen;
            this.castlePlacePikemanButton.ImageOver = (Image) GFXLibrary.r_building_miltary_pikemen;
            this.castlePlacePikemanButton.setSizeToImage();
            this.castlePlacePikemanButton.Position = new Point(this.castlePlacePeasantButton.Position.X, this.castlePlacePeasantButton.Rectangle.Bottom - 0x22);
            this.castlePlacePikemanButton.Data = 0x5d;
            this.castlePlacePikemanButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlacePikemanInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castlePlacePikemanInset.Position = new Point(0x37, 0x37);
            this.castlePlacePikemanLabel.Text = "0";
            this.castlePlacePikemanLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castlePlacePikemanLabel.Position = new Point(0, -1);
            this.castlePlacePikemanLabel.Size = this.castlePlacePikemanInset.Size;
            this.castlePlacePikemanLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            base.addControl(this.castlePlacePikemanButton);
            this.castlePlacePikemanInset.addControl(this.castlePlacePikemanLabel);
            this.castlePlacePikemanButton.addControl(this.castlePlacePikemanInset);
            this.castlePlacePikemanButton.Active = false;
            this.castlePlaceSwordsmanButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_swordsman;
            this.castlePlaceSwordsmanButton.ImageOver = (Image) GFXLibrary.r_building_miltary_swordsman;
            this.castlePlaceSwordsmanButton.setSizeToImage();
            this.castlePlaceSwordsmanButton.Position = new Point(this.castlePlaceArcherButton.Position.X, this.castlePlacePikemanButton.Y);
            this.castlePlaceSwordsmanButton.Data = 0x5b;
            this.castlePlaceSwordsmanButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlaceSwordsmanInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castlePlaceSwordsmanInset.Position = new Point(0x37, 0x37);
            this.castlePlaceSwordsmanLabel.Text = "0";
            this.castlePlaceSwordsmanLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castlePlaceSwordsmanLabel.Position = new Point(0, -1);
            this.castlePlaceSwordsmanLabel.Size = this.castlePlaceSwordsmanInset.Size;
            this.castlePlaceSwordsmanLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            base.addControl(this.castlePlaceSwordsmanButton);
            this.castlePlaceSwordsmanInset.addControl(this.castlePlaceSwordsmanLabel);
            this.castlePlaceSwordsmanButton.addControl(this.castlePlaceSwordsmanInset);
            this.castlePlaceSwordsmanButton.Active = false;
            this.castlePlaceCatapultButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_catapult;
            this.castlePlaceCatapultButton.ImageOver = (Image) GFXLibrary.r_building_miltary_catapult;
            this.castlePlaceCatapultButton.setSizeToImage();
            this.castlePlaceCatapultButton.Position = new Point(this.castlePlacePeasantButton.Position.X, this.castlePlacePikemanButton.Rectangle.Bottom - 0x22);
            this.castlePlaceCatapultButton.Data = 0x5e;
            this.castlePlaceCatapultButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlaceCatapultInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castlePlaceCatapultInset.Position = new Point(0x37, 0x41);
            this.castlePlaceCatapultLabel.Text = "0";
            this.castlePlaceCatapultLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castlePlaceCatapultLabel.Position = new Point(0, -1);
            this.castlePlaceCatapultLabel.Size = this.castlePlaceCatapultInset.Size;
            this.castlePlaceCatapultLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            base.addControl(this.castlePlaceCatapultButton);
            this.castlePlaceCatapultInset.addControl(this.castlePlaceCatapultLabel);
            this.castlePlaceCatapultButton.addControl(this.castlePlaceCatapultInset);
            this.castlePlaceCatapultButton.Active = false;
            this.castlePlaceCaptainButton.ImageNorm = (Image) GFXLibrary.r_building_miltary_captain_normal;
            this.castlePlaceCaptainButton.ImageOver = (Image) GFXLibrary.r_building_miltary_captain_normal;
            this.castlePlaceCaptainButton.setSizeToImage();
            this.castlePlaceCaptainButton.Position = new Point(this.castlePlaceArcherButton.Position.X, this.castlePlaceCatapultButton.Position.Y);
            this.castlePlaceCaptainButton.Data = 0x5e;
            this.castlePlaceCaptainButton.ClickArea = new Rectangle(10, 10, 0x55, 0x55);
            this.castlePlaceCaptainInset.Image = (Image) GFXLibrary.castlescreen_unit_capsule;
            this.castlePlaceCaptainInset.Position = new Point(0x37, 0x41);
            this.castlePlaceCaptainLabel.Text = "0";
            this.castlePlaceCaptainLabel.Color = Color.FromArgb(0xfe, 0xf8, 0xe5);
            this.castlePlaceCaptainLabel.Position = new Point(0, -1);
            this.castlePlaceCaptainLabel.Size = this.castlePlaceCaptainInset.Size;
            this.castlePlaceCaptainLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_CENTER;
            base.addControl(this.castlePlaceCaptainButton);
            this.castlePlaceCaptainInset.addControl(this.castlePlaceCaptainLabel);
            this.castlePlaceCaptainButton.addControl(this.castlePlaceCaptainInset);
            this.castlePlaceCaptainButton.Active = false;
            this.selectedTitleLabel.Text = "";
            this.selectedTitleLabel.Color = ARGBColors.White;
            this.selectedTitleLabel.DropShadowColor = ARGBColors.Black;
            this.selectedTitleLabel.Position = new Point((2 * base.Width) / 3, this.manageLabel.Y);
            this.selectedTitleLabel.Size = new Size(base.Width / 3, 0x18);
            this.selectedTitleLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.selectedTitleLabel.Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular);
            base.addControl(this.selectedTitleLabel);
            this.selectedTroopCountLabel.Text = "";
            this.selectedTroopCountLabel.Color = ARGBColors.White;
            this.selectedTroopCountLabel.DropShadowColor = ARGBColors.Black;
            this.selectedTroopCountLabel.Position = new Point((2 * base.Width) / 3, this.storedLabel.Y);
            this.selectedTroopCountLabel.Size = new Size(base.Width / 3, 0x18);
            this.selectedTroopCountLabel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.selectedTroopCountLabel.Font = FontManager.GetFont("Arial", 8f, FontStyle.Regular);
            base.addControl(this.selectedTroopCountLabel);
            if (!Program.mySettings.AttackSetupsUpdated)
            {
                GameEngine.Instance.CastleAttackerSetup.cleanUpAttackSaveNames();
                Program.mySettings.AttackSetupsUpdated = true;
            }
            this.loadNames();
            this.initTotals();
        }

        private void initTotals()
        {
            if (GameEngine.Instance.CastleAttackerSetup != null)
            {
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                int num6 = 0;
                CampCastleElement[] elementArray = GameEngine.Instance.CastleAttackerSetup.getCurrentAttackSetup();
                int length = elementArray.Length;
                for (int i = 0; i < length; i++)
                {
                    switch (elementArray[i].elementType)
                    {
                        case 90:
                            num++;
                            break;

                        case 0x5b:
                            num4++;
                            break;

                        case 0x5c:
                            num2++;
                            break;

                        case 0x5d:
                            num3++;
                            break;

                        case 0x5e:
                            num5++;
                            break;

                        case 100:
                        case 0x65:
                        case 0x66:
                        case 0x67:
                        case 0x68:
                            num6++;
                            break;
                    }
                }
                this.selectedTitleLabel.Text = SK.Text("Formations_Current_Totals", "Current Troop Totals");
                this.selectedTroopCountLabel.Text = SK.Text("Formations_Troop_Count", "Total Troops");
                this.selectedTroopCountLabel.Text = this.selectedTroopCountLabel.Text + " - " + length.ToString();
                this.castlePlacePeasantLabel.Text = num.ToString();
                this.castlePlaceArcherLabel.Text = num2.ToString();
                this.castlePlacePikemanLabel.Text = num3.ToString();
                this.castlePlaceSwordsmanLabel.Text = num4.ToString();
                this.castlePlaceCatapultLabel.Text = num5.ToString();
                this.castlePlaceCaptainLabel.Text = num6.ToString();
            }
        }

        private void listClick(CustomSelfDrawPanel.CSDListItem item)
        {
            this.updateTotals(item);
            this.m_parent.setSelectedText(item.Text);
            this.renameButton.Enabled = true;
            this.deleteButton.Enabled = true;
            this.loadButton.Enabled = true;
        }

        private void listDoubleClick(CustomSelfDrawPanel.CSDListItem item)
        {
            if (GameEngine.Instance.CastleAttackerSetup != null)
            {
                GameEngine.Instance.CastleAttackerSetup.restoreAttackSetup(item.Text);
            }
        }

        private void loadClick()
        {
            if ((this.storedList.getSelectedItem() != null) && (GameEngine.Instance.CastleAttackerSetup != null))
            {
                GameEngine.Instance.CastleAttackerSetup.restoreAttackSetup(this.storedList.getSelectedItem().Text);
            }
        }

        private void loadNames()
        {
            char[] separator = new char[] { '_' };
            this.formationNames.Clear();
            foreach (string str in Directory.GetFiles(GameEngine.getSettingsPath(true), "*.cas"))
            {
                string fileName = Path.GetFileName(str.Remove(str.LastIndexOf('.')));
                string[] strArray2 = fileName.Split(separator);
                if ((strArray2.Length >= 2) && !(strArray2[0].ToLowerInvariant() != "attacksetup"))
                {
                    fileName = fileName.Replace("AttackSetup_", "");
                    CustomSelfDrawPanel.CSDListItem item = new CustomSelfDrawPanel.CSDListItem {
                        Text = fileName
                    };
                    this.formationNames.Add(item);
                }
            }
            this.storedList.populate(this.formationNames);
        }

        private void renameClick()
        {
            string testText = this.m_parent.getSelectedText();
            if ((this.storedList.getSelectedItem() != null) && (testText != ""))
            {
                if (this.storedList.contains(testText))
                {
                    MyMessageBox.Show(SK.Text("Formations_Name_Exists", "That name is already in use"), SK.Text("Formations_Overwrite_Title", "Name Already in Use"));
                }
                else
                {
                    string text = this.storedList.getSelectedItem().Text;
                    GameEngine.Instance.CastleAttackerSetup.renameAttackSetup(text, testText);
                    this.formationNames.Remove(this.storedList.getSelectedItem());
                    CustomSelfDrawPanel.CSDListItem item = new CustomSelfDrawPanel.CSDListItem {
                        Text = testText
                    };
                    this.formationNames.Add(item);
                    this.storedList.populate(this.formationNames);
                    this.selectedTitleLabel.Text = testText;
                    this.loadNames();
                }
            }
        }

        private void saveFormation()
        {
            if (GameEngine.Instance.CastleAttackerSetup != null)
            {
                GameEngine.Instance.CastleAttackerSetup.memoriseAttackSetup(this.saveName);
            }
        }

        private void SharedCreateCode()
        {
            this.saveFormation();
            if (!this.storedList.contains(this.saveName))
            {
                CustomSelfDrawPanel.CSDListItem item = new CustomSelfDrawPanel.CSDListItem {
                    Text = this.saveName
                };
                this.formationNames.Add(item);
                this.storedList.populate(this.formationNames);
            }
            this.loadNames();
            this.updateTotals(null);
            this.m_parent.setCreateText("");
        }

        private void SharedDeleteCode()
        {
            GameEngine.Instance.CastleAttackerSetup.deleteAttackSetup(this.storedList.getSelectedItem().Text);
            this.formationNames.Remove(this.storedList.getSelectedItem());
            this.storedList.populate(this.formationNames);
            this.m_parent.setSelectedText("");
            this.renameButton.Enabled = false;
            this.deleteButton.Enabled = false;
            this.loadButton.Enabled = false;
            this.updateTotals(null);
            this.loadNames();
        }

        private void totalsClick()
        {
            this.initTotals();
        }

        private void updateTotals(CustomSelfDrawPanel.CSDListItem current)
        {
            if (current == null)
            {
                this.castlePlacePeasantButton.Visible = false;
                this.castlePlaceArcherButton.Visible = false;
                this.castlePlacePikemanButton.Visible = false;
                this.castlePlaceSwordsmanButton.Visible = false;
                this.castlePlaceCatapultButton.Visible = false;
                this.castlePlaceCaptainButton.Visible = false;
                this.selectedTitleLabel.Text = "";
                this.selectedTroopCountLabel.Text = "";
            }
            else
            {
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                int num6 = 0;
                List<CastleMap.RestoreCastleElement> list = GameEngine.Instance.CastleAttackerSetup.getAttackSetup(current.Text);
                int count = list.Count;
                foreach (CastleMap.RestoreCastleElement element in list)
                {
                    switch (element.elementType)
                    {
                        case 90:
                            num++;
                            break;

                        case 0x5b:
                            num4++;
                            break;

                        case 0x5c:
                            num2++;
                            break;

                        case 0x5d:
                            num3++;
                            break;

                        case 0x5e:
                            num5++;
                            break;

                        case 100:
                        case 0x65:
                        case 0x66:
                        case 0x67:
                        case 0x68:
                            num6++;
                            break;
                    }
                }
                this.selectedTitleLabel.Text = current.Text;
                this.selectedTroopCountLabel.Text = SK.Text("Formations_Troop_Count", "Total Troops");
                this.selectedTroopCountLabel.Text = this.selectedTroopCountLabel.Text + " - " + count.ToString();
                this.castlePlacePeasantLabel.Text = num.ToString();
                this.castlePlaceArcherLabel.Text = num2.ToString();
                this.castlePlacePikemanLabel.Text = num3.ToString();
                this.castlePlaceSwordsmanLabel.Text = num4.ToString();
                this.castlePlaceCatapultLabel.Text = num5.ToString();
                this.castlePlaceCaptainLabel.Text = num6.ToString();
                this.castlePlacePeasantButton.Enabled = num != 0;
                this.castlePlaceArcherButton.Enabled = num2 != 0;
                this.castlePlacePikemanButton.Enabled = num3 != 0;
                this.castlePlaceSwordsmanButton.Enabled = num4 != 0;
                this.castlePlaceCatapultButton.Enabled = num5 != 0;
                this.castlePlaceCaptainButton.Enabled = num6 != 0;
                this.castlePlacePeasantButton.Visible = true;
                this.castlePlaceArcherButton.Visible = true;
                this.castlePlacePikemanButton.Visible = true;
                this.castlePlaceSwordsmanButton.Visible = true;
                this.castlePlaceCatapultButton.Visible = true;
                this.castlePlaceCaptainButton.Visible = true;
            }
        }
    }
}

