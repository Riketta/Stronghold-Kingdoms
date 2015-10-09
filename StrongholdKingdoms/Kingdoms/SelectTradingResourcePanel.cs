namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SelectTradingResourcePanel : CustomSelfDrawPanel
    {
        private CustomSelfDrawPanel.CSDExtendingPanel backgroundImage = new CustomSelfDrawPanel.CSDExtendingPanel();
        private IContainer components;
        private LogoutPanel m_logoutParent;
        private SelectTradingResourcePopup m_parentWindow;

        public SelectTradingResourcePanel()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init(int currentResource, SelectTradingResourcePopup parentWindow, LogoutPanel logoutParent)
        {
            this.m_parentWindow = parentWindow;
            this.m_logoutParent = logoutParent;
            base.clearControls();
            int width = 12;
            this.backgroundImage.Size = new Size(0x270, 0x144);
            CustomSelfDrawPanel.CSDFill control = new CustomSelfDrawPanel.CSDFill {
                Position = new Point(0, 0),
                Size = new Size(width, this.backgroundImage.Height),
                FillColor = Color.FromArgb(0xff, 0, 0xff)
            };
            base.addControl(control);
            CustomSelfDrawPanel.CSDFill fill2 = new CustomSelfDrawPanel.CSDFill {
                Position = new Point(this.backgroundImage.Width - width, 0),
                Size = new Size(width, this.backgroundImage.Height),
                FillColor = Color.FromArgb(0xff, 0, 0xff)
            };
            base.addControl(fill2);
            CustomSelfDrawPanel.CSDFill fill3 = new CustomSelfDrawPanel.CSDFill {
                Position = new Point(0, 0),
                Size = new Size(this.backgroundImage.Width, width),
                FillColor = Color.FromArgb(0xff, 0, 0xff)
            };
            base.addControl(fill3);
            CustomSelfDrawPanel.CSDFill fill4 = new CustomSelfDrawPanel.CSDFill {
                Position = new Point(0, this.backgroundImage.Height - width),
                Size = new Size(this.backgroundImage.Width, width),
                FillColor = Color.FromArgb(0xff, 0, 0xff)
            };
            base.addControl(fill4);
            this.backgroundImage.Position = new Point(0, 0);
            base.addControl(this.backgroundImage);
            this.backgroundImage.Create((Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_upper_left, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_upper_middle, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_upper_right, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_middle_left, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_middle_middle, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_middle_right, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_bottom_left, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_bottom_middle, (Image) GFXLibrary.parishwall_solid_rounded_rectangle_tan_bottom_right);
            int num2 = 0x4b;
            CustomSelfDrawPanel.ResourceButton button = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width, width)
            };
            button.init(6, logoutParent);
            this.backgroundImage.addControl(button);
            CustomSelfDrawPanel.ResourceButton button2 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + num2, width)
            };
            button2.init(7, logoutParent);
            this.backgroundImage.addControl(button2);
            CustomSelfDrawPanel.ResourceButton button3 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + (num2 * 2), width)
            };
            button3.init(8, logoutParent);
            this.backgroundImage.addControl(button3);
            CustomSelfDrawPanel.ResourceButton button4 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + (num2 * 3), width)
            };
            button4.init(9, logoutParent);
            this.backgroundImage.addControl(button4);
            CustomSelfDrawPanel.ResourceButton button5 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width, width + num2)
            };
            button5.init(13, logoutParent);
            this.backgroundImage.addControl(button5);
            CustomSelfDrawPanel.ResourceButton button6 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + num2, width + num2)
            };
            button6.init(0x11, logoutParent);
            this.backgroundImage.addControl(button6);
            CustomSelfDrawPanel.ResourceButton button7 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + (num2 * 2), width + num2)
            };
            button7.init(0x10, logoutParent);
            this.backgroundImage.addControl(button7);
            CustomSelfDrawPanel.ResourceButton button8 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + (num2 * 3), width + num2)
            };
            button8.init(14, logoutParent);
            this.backgroundImage.addControl(button8);
            CustomSelfDrawPanel.ResourceButton button9 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + (num2 * 4), width + num2)
            };
            button9.init(15, logoutParent);
            this.backgroundImage.addControl(button9);
            CustomSelfDrawPanel.ResourceButton button10 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + (num2 * 5), width + num2)
            };
            button10.init(0x12, logoutParent);
            this.backgroundImage.addControl(button10);
            CustomSelfDrawPanel.ResourceButton button11 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + (num2 * 7), width + num2)
            };
            button11.init(12, logoutParent);
            this.backgroundImage.addControl(button11);
            CustomSelfDrawPanel.ResourceButton button12 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width, width + (num2 * 2))
            };
            button12.init(0x16, logoutParent);
            this.backgroundImage.addControl(button12);
            CustomSelfDrawPanel.ResourceButton button13 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + num2, width + (num2 * 2))
            };
            button13.init(0x15, logoutParent);
            this.backgroundImage.addControl(button13);
            CustomSelfDrawPanel.ResourceButton button14 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + (num2 * 2), width + (num2 * 2))
            };
            button14.init(0x1a, logoutParent);
            this.backgroundImage.addControl(button14);
            CustomSelfDrawPanel.ResourceButton button15 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + (num2 * 3), width + (num2 * 2))
            };
            button15.init(0x13, logoutParent);
            this.backgroundImage.addControl(button15);
            CustomSelfDrawPanel.ResourceButton button16 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + (num2 * 4), width + (num2 * 2))
            };
            button16.init(0x21, logoutParent);
            this.backgroundImage.addControl(button16);
            CustomSelfDrawPanel.ResourceButton button17 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + (num2 * 5), width + (num2 * 2))
            };
            button17.init(0x17, logoutParent);
            this.backgroundImage.addControl(button17);
            CustomSelfDrawPanel.ResourceButton button18 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + (num2 * 6), width + (num2 * 2))
            };
            button18.init(0x18, logoutParent);
            this.backgroundImage.addControl(button18);
            CustomSelfDrawPanel.ResourceButton button19 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + (num2 * 7), width + (num2 * 2))
            };
            button19.init(0x19, logoutParent);
            this.backgroundImage.addControl(button19);
            CustomSelfDrawPanel.ResourceButton button20 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width, width + (num2 * 3))
            };
            button20.init(0x1d, logoutParent);
            this.backgroundImage.addControl(button20);
            CustomSelfDrawPanel.ResourceButton button21 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + num2, width + (num2 * 3))
            };
            button21.init(0x1c, logoutParent);
            this.backgroundImage.addControl(button21);
            CustomSelfDrawPanel.ResourceButton button22 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + (num2 * 2), width + (num2 * 3))
            };
            button22.init(0x1f, logoutParent);
            this.backgroundImage.addControl(button22);
            CustomSelfDrawPanel.ResourceButton button23 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + (num2 * 3), width + (num2 * 3))
            };
            button23.init(30, logoutParent);
            this.backgroundImage.addControl(button23);
            CustomSelfDrawPanel.ResourceButton button24 = new CustomSelfDrawPanel.ResourceButton {
                Position = new Point(width + (num2 * 4), width + (num2 * 3))
            };
            button24.init(0x20, logoutParent);
            this.backgroundImage.addControl(button24);
            parentWindow.Size = this.backgroundImage.Size;
            base.Invalidate();
            parentWindow.Invalidate();
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.White;
            base.Name = "SelectTradingResourcePanel";
            base.Size = new Size(600, 0x37);
            base.ResumeLayout(false);
        }

        public void update()
        {
        }
    }
}

