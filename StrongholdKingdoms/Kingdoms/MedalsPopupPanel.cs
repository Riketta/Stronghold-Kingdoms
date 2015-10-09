namespace Kingdoms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MedalsPopupPanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDButton closeButton = new CustomSelfDrawPanel.CSDButton();
        private IContainer components;
        private Form m_parent;
        private CustomSelfDrawPanel.CSDImage mainBackgroundImage = new CustomSelfDrawPanel.CSDImage();
        private CustomSelfDrawPanel.MedalWindow medalWindow = new CustomSelfDrawPanel.MedalWindow();

        public MedalsPopupPanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void closeClick()
        {
            this.m_parent.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(List<int> achievements, Form parent)
        {
            this.m_parent = parent;
            if (achievements == null)
            {
                achievements = new List<int>();
            }
            base.clearControls();
            this.mainBackgroundImage.Image = (Image) GFXLibrary.body_background_canvas;
            this.mainBackgroundImage.ClipRect = new Rectangle(new Point(), base.Size);
            this.mainBackgroundImage.Position = new Point(0, 0);
            this.mainBackgroundImage.Size = base.Size;
            this.mainBackgroundImage.Tile = true;
            base.addControl(this.mainBackgroundImage);
            this.closeButton.ImageNorm = (Image) GFXLibrary.int_button_close_normal;
            this.closeButton.ImageOver = (Image) GFXLibrary.int_button_close_over;
            this.closeButton.ImageClick = (Image) GFXLibrary.int_button_close_in;
            this.closeButton.Position = new Point(base.Width - 40, 0);
            this.closeButton.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.closeClick), "MedalsPopupPanel_close");
            base.addControl(this.closeButton);
            CustomSelfDrawPanel.WikiLinkControl.init(this, 0x2c, new Point(base.Width - 90, 0));
            this.medalWindow.Position = new Point(0, 0);
            this.medalWindow.init(achievements, false, true, 0);
            this.mainBackgroundImage.addControl(this.medalWindow);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.None;
        }
    }
}

