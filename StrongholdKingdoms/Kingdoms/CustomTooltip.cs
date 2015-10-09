namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CustomTooltip : Form
    {
        private IContainer components;
        private CustomTooltipPanel customPanel;
        private static Form lastParent;
        private static bool screenEdgeTooltip;

        public CustomTooltip()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        public void closing()
        {
            CustomTooltipManager.MouseLeaveTooltipAreaStored();
        }

        public static void CreateToolTip(string text, int tooltipID, int data, Form parentWindow)
        {
            bool force = false;
            CustomTooltip tooltip = InterfaceMgr.Instance.getCustomTooltip();
            if (parentWindow == null)
            {
                parentWindow = InterfaceMgr.Instance.ParentForm;
            }
            if (tooltip == null)
            {
                tooltip = new CustomTooltip();
                force = true;
                tooltip.customPanel.MouseEnter += new EventHandler(CustomTooltip.customPanel_MouseEnter);
                tooltip.customPanel.MouseLeave += new EventHandler(CustomTooltip.customPanel_MouseLeave);
            }
            else
            {
                if (parentWindow != lastParent)
                {
                    tooltip.Close();
                    tooltip = new CustomTooltip();
                    force = true;
                    tooltip.customPanel.MouseEnter += new EventHandler(CustomTooltip.customPanel_MouseEnter);
                    tooltip.customPanel.MouseLeave += new EventHandler(CustomTooltip.customPanel_MouseLeave);
                }
                if (!tooltip.Created || !tooltip.Visible)
                {
                    force = true;
                }
            }
            lastParent = parentWindow;
            tooltip.updateLocation();
            tooltip.setText(text, tooltipID, data, force);
            tooltip.showTooltip(force, parentWindow);
        }

        public static void customPanel_MouseEnter(object sender, EventArgs e)
        {
            if (screenEdgeTooltip)
            {
                CustomTooltipManager.MouseEnterTooltipAreaStored();
            }
        }

        public static void customPanel_MouseLeave(object sender, EventArgs e)
        {
            if (screenEdgeTooltip)
            {
                CustomTooltipManager.MouseLeaveTooltipAreaStored();
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

        public void hidingTooltip()
        {
            this.customPanel.hidingTooltip();
        }

        private void InitializeComponent()
        {
            this.customPanel = new CustomTooltipPanel();
            base.SuspendLayout();
            this.customPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.customPanel.Location = new Point(0, 0);
            this.customPanel.Name = "customPanel";
            this.customPanel.Size = new Size(0x18, 0x18);
            this.customPanel.StoredGraphics = null;
            this.customPanel.TabIndex = 0;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x18, 0x18);
            base.ControlBox = false;
            base.Controls.Add(this.customPanel);
            base.FormBorderStyle = FormBorderStyle.None;
            this.MinimumSize = new Size(10, 10);
            base.Name = "CustomTooltip";
            base.Opacity = 0.95;
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "CustomTooltip";
            base.ResumeLayout(false);
        }

        public void setPosition(int x, int y)
        {
            base.Location = new Point(x, y);
        }

        public void setText(string text, int tooltipID, int data, bool force)
        {
            this.customPanel.setText(text, tooltipID, data, this, force);
        }

        public void showTooltip(bool doShow, Form parentWindow)
        {
            InterfaceMgr.Instance.setCurrentCustomTooltip(this);
            if (parentWindow == null)
            {
                parentWindow = InterfaceMgr.Instance.ParentForm;
            }
            if (doShow)
            {
                base.Show(parentWindow);
            }
        }

        public void updateLocation()
        {
            if (!base.IsDisposed)
            {
                Point point = new Point(Cursor.Position.X, Cursor.Position.Y);
                int x = point.X + 15;
                int y = point.Y + 15;
                Screen screen = Screen.FromPoint(point);
                int num3 = x + base.Width;
                int num4 = 0;
                screenEdgeTooltip = false;
                if (num3 > (screen.WorkingArea.Width + screen.WorkingArea.X))
                {
                    x = (screen.WorkingArea.Width + screen.WorkingArea.X) - base.Width;
                    num4++;
                }
                int num5 = y + base.Height;
                if (num5 > (screen.WorkingArea.Height + screen.WorkingArea.Y))
                {
                    y = (screen.WorkingArea.Height + screen.WorkingArea.Y) - base.Height;
                    num4++;
                }
                if (num4 == 2)
                {
                    screenEdgeTooltip = true;
                }
                this.setPosition(x, y);
            }
        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }
    }
}

