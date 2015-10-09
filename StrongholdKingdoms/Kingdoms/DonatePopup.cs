namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class DonatePopup : Form
    {
        private IContainer components;
        private DonatePanel donatePanel;

        public DonatePopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        public static void CreateDonatePopup(Point location, ParishWallDetailInfo_ReturnType returnData)
        {
            bool doShow = false;
            DonatePopup popup = InterfaceMgr.Instance.getDonatePopup();
            if (popup == null)
            {
                popup = new DonatePopup();
                doShow = true;
            }
            else if (!popup.Created || !popup.Visible)
            {
                doShow = true;
            }
            popup.setText(returnData);
            popup.updateLocation(location);
            popup.showWindow(doShow);
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
            this.donatePanel = new DonatePanel();
            base.SuspendLayout();
            this.donatePanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.donatePanel.BackColor = ARGBColors.Fuchsia;
            this.donatePanel.Location = new Point(0, 0);
            this.donatePanel.Name = "donatePanel";
            this.donatePanel.Size = new Size(0x124, 0x10a);
            this.donatePanel.StoredGraphics = null;
            this.donatePanel.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.White;
            base.ClientSize = new Size(0x124, 0x10a);
            base.ControlBox = false;
            base.Controls.Add(this.donatePanel);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "DonatePopup";
            base.Opacity = 0.95;
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Donation";
            base.TransparencyKey = ARGBColors.Fuchsia;
            base.ResumeLayout(false);
        }

        public void setText(ParishWallDetailInfo_ReturnType returnData)
        {
            this.donatePanel.setText(returnData, this);
        }

        public void showWindow(bool doShow)
        {
            this.Text = SK.Text("DonatePopup_Donation", "Donation");
            InterfaceMgr.Instance.setCurrentDonatePopup(this);
            if (doShow)
            {
                Form parentForm = InterfaceMgr.Instance.ParentForm;
                base.Show(parentForm);
            }
        }

        public void updateLocation(Point location)
        {
            int x = location.X;
            int y = location.Y - 20;
            Screen screen = Screen.FromPoint(location);
            int num3 = x + base.Width;
            if (num3 > (screen.WorkingArea.Width + screen.WorkingArea.X))
            {
                x = (screen.WorkingArea.Width + screen.WorkingArea.X) - base.Width;
            }
            int num4 = y + base.Height;
            if (num4 > (screen.WorkingArea.Height + screen.WorkingArea.Y))
            {
                y = (screen.WorkingArea.Height + screen.WorkingArea.Y) - base.Height;
            }
            base.Location = new Point(x, y);
        }
    }
}

