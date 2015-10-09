namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DonatePanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private CustomSelfDrawPanel.CSDLabel bodyLabel = new CustomSelfDrawPanel.CSDLabel();
        private IContainer components;

        public DonatePanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private CustomSelfDrawPanel.CSDImage addRow(int index, int buildingType, int amount, int resource)
        {
            int num = GFXLibrary.parishwall_tan_bar_01_short.Height + 3;
            CustomSelfDrawPanel.CSDImage image = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.parishwall_tan_bar_01_short,
                Position = new Point(10, 10 + (num * index))
            };
            CustomSelfDrawPanel.CSDLabel control = new CustomSelfDrawPanel.CSDLabel {
                Text = VillageBuildingsData.getBuildingName(buildingType),
                Color = ARGBColors.Black,
                Position = new Point(10, 0),
                Size = new Size(image.Width - 20, image.Height),
                Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT
            };
            image.addControl(control);
            CustomSelfDrawPanel.CSDLabel label2 = new CustomSelfDrawPanel.CSDLabel {
                Text = amount.ToString(),
                Color = ARGBColors.Black,
                Position = new Point(10, 0),
                Size = new Size(image.Width - 60, image.Height),
                Font = FontManager.GetFont("Arial", 10f, FontStyle.Regular),
                Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_RIGHT
            };
            image.addControl(label2);
            CustomSelfDrawPanel.CSDImage image2 = new CustomSelfDrawPanel.CSDImage {
                Image = (Image) GFXLibrary.getCommodity32Image(resource),
                Position = new Point(image.Width - 0x2d, 2)
            };
            image.addControl(image2);
            return image;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.White;
            base.Name = "DonatePanel";
            base.Size = new Size(600, 0x37);
            base.ResumeLayout(false);
        }

        public void setText(ParishWallDetailInfo_ReturnType returnData, DonatePopup parent)
        {
            base.clearControls();
            int num = 0;
            foreach (WallInfo info in returnData.detailedInfo)
            {
                if (info.detailedInfo.detail1 > 0)
                {
                    num++;
                }
                if (info.detailedInfo.detail2 > 0)
                {
                    num++;
                }
                if (info.detailedInfo.detail3 > 0)
                {
                    num++;
                }
                if (info.detailedInfo.detail4 > 0)
                {
                    num++;
                }
                if (info.detailedInfo.detail5 > 0)
                {
                    num++;
                }
                if (info.detailedInfo.detail6 > 0)
                {
                    num++;
                }
                if (info.detailedInfo.detail7 > 0)
                {
                    num++;
                }
                if (info.detailedInfo.detail8 > 0)
                {
                    num++;
                }
            }
            int num2 = GFXLibrary.parishwall_tan_bar_01_short.Height + 3;
            this.backgroundImage.Size = new Size(GFXLibrary.parishwall_tan_bar_01_short.Width + 20, ((num2 * num) + 20) - 3);
            this.backgroundImage.Position = new Point(0, 0);
            base.addControl(this.backgroundImage);
            this.backgroundImage.Create((Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_upper_left, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_upper_middle, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_upper_right, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_middle_left, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_middle_middle, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_middle_right, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_bottom_left, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_bottom_middle, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_bottom_right);
            int index = 0;
            foreach (WallInfo info2 in returnData.detailedInfo)
            {
                if (info2.detailedInfo.detail1 > 0)
                {
                    int resource = VillageBuildingsData.getRequiredResourceType(info2.data1, 0);
                    this.backgroundImage.addControl(this.addRow(index, info2.data1, info2.detailedInfo.detail1, resource));
                    index++;
                }
                if (info2.detailedInfo.detail2 > 0)
                {
                    int num5 = VillageBuildingsData.getRequiredResourceType(info2.data1, 1);
                    this.backgroundImage.addControl(this.addRow(index, info2.data1, info2.detailedInfo.detail2, num5));
                    index++;
                }
                if (info2.detailedInfo.detail3 > 0)
                {
                    int num6 = VillageBuildingsData.getRequiredResourceType(info2.data1, 2);
                    this.backgroundImage.addControl(this.addRow(index, info2.data1, info2.detailedInfo.detail3, num6));
                    index++;
                }
                if (info2.detailedInfo.detail4 > 0)
                {
                    int num7 = VillageBuildingsData.getRequiredResourceType(info2.data1, 3);
                    this.backgroundImage.addControl(this.addRow(index, info2.data1, info2.detailedInfo.detail4, num7));
                    index++;
                }
                if (info2.detailedInfo.detail5 > 0)
                {
                    int num8 = VillageBuildingsData.getRequiredResourceType(info2.data1, 4);
                    this.backgroundImage.addControl(this.addRow(index, info2.data1, info2.detailedInfo.detail5, num8));
                    index++;
                }
                if (info2.detailedInfo.detail6 > 0)
                {
                    int num9 = VillageBuildingsData.getRequiredResourceType(info2.data1, 5);
                    this.backgroundImage.addControl(this.addRow(index, info2.data1, info2.detailedInfo.detail6, num9));
                    index++;
                }
                if (info2.detailedInfo.detail7 > 0)
                {
                    int num10 = VillageBuildingsData.getRequiredResourceType(info2.data1, 6);
                    this.backgroundImage.addControl(this.addRow(index, info2.data1, info2.detailedInfo.detail7, num10));
                    index++;
                }
                if (info2.detailedInfo.detail8 > 0)
                {
                    int num11 = VillageBuildingsData.getRequiredResourceType(info2.data1, 7);
                    this.backgroundImage.addControl(this.addRow(index, info2.data1, info2.detailedInfo.detail8, num11));
                    index++;
                }
            }
            parent.Size = this.backgroundImage.Size;
            base.Invalidate();
            parent.Invalidate();
        }

        public void update()
        {
        }
    }
}

