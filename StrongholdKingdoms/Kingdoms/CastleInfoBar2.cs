namespace Kingdoms
{
    using System;
    using System.Drawing;
    using System.Globalization;

    public class CastleInfoBar2 : CustomSelfDrawPanel.CSDControl
    {
        private CustomSelfDrawPanel.CSDImage imgIron = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage imgPitch = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage imgStone = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDImage imgWood = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.CSDLabel lblIronLevel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblPitchLevel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblStoneLevel = new CustomSelfDrawPanel.CSDLabel();
        private CustomSelfDrawPanel.CSDLabel lblWoodLevel = new CustomSelfDrawPanel.CSDLabel();

        public void hide()
        {
            base.Visible = false;
            base.invalidate();
        }

        public void init()
        {
            this.clearControls();
            this.lblWoodLevel.Text = "";
            this.lblWoodLevel.Position = new Point(0x2c, 3);
            this.lblWoodLevel.Size = new Size(0x58, 0x1d);
            this.lblWoodLevel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblWoodLevel.Color = ARGBColors.Yellow;
            this.lblWoodLevel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.lblWoodLevel.CustomTooltipID = 0x8e;
            base.addControl(this.lblWoodLevel);
            this.lblStoneLevel.Text = "";
            this.lblStoneLevel.Position = new Point(0xaf, 3);
            this.lblStoneLevel.Size = new Size(0x58, 0x1d);
            this.lblStoneLevel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblStoneLevel.Color = ARGBColors.Yellow;
            this.lblStoneLevel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.lblStoneLevel.CustomTooltipID = 0x8f;
            base.addControl(this.lblStoneLevel);
            this.lblPitchLevel.Text = "";
            this.lblPitchLevel.Position = new Point(0x132, 3);
            this.lblPitchLevel.Size = new Size(0x58, 0x1d);
            this.lblPitchLevel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblPitchLevel.Color = ARGBColors.Yellow;
            this.lblPitchLevel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.lblPitchLevel.CustomTooltipID = 0x94;
            base.addControl(this.lblPitchLevel);
            this.lblIronLevel.Text = "";
            this.lblIronLevel.Position = new Point(0x1b5, 3);
            this.lblIronLevel.Size = new Size(0x58, 0x1d);
            this.lblIronLevel.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblIronLevel.Color = ARGBColors.Yellow;
            this.lblIronLevel.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            this.lblIronLevel.CustomTooltipID = 0x95;
            base.addControl(this.lblIronLevel);
            this.imgWood.Image = (Image) GFXLibrary.com_32_wood;
            this.imgWood.Position = new Point(6, 0);
            this.imgWood.CustomTooltipID = 0x8e;
            base.addControl(this.imgWood);
            this.imgStone.Image = (Image) GFXLibrary.com_32_stone;
            this.imgStone.Position = new Point(0x8a, 0);
            this.imgStone.CustomTooltipID = 0x8f;
            base.addControl(this.imgStone);
            this.imgPitch.Image = (Image) GFXLibrary.com_32_pitch;
            this.imgPitch.Position = new Point(0x10d, 0);
            this.imgPitch.CustomTooltipID = 0x94;
            base.addControl(this.imgPitch);
            this.imgIron.Image = (Image) GFXLibrary.com_32_iron;
            this.imgIron.Position = new Point(400, 0);
            this.imgIron.CustomTooltipID = 0x95;
            base.addControl(this.imgIron);
        }

        public bool isVisible()
        {
            return base.Visible;
        }

        public void setDisplayedLevels(int woodLevel, int stoneLevel, int pitchLevel, int ironLevel)
        {
            NumberFormatInfo nFI = GameEngine.NFI;
            this.lblWoodLevel.TextDiffOnly = woodLevel.ToString("N", nFI);
            this.lblStoneLevel.TextDiffOnly = stoneLevel.ToString("N", nFI);
            this.lblPitchLevel.TextDiffOnly = pitchLevel.ToString("N", nFI);
            this.lblIronLevel.TextDiffOnly = ironLevel.ToString("N", nFI);
        }

        public void show()
        {
            base.Visible = true;
            base.invalidate();
        }
    }
}

