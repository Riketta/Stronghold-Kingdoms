namespace Kingdoms
{
    using Kingdoms.Properties;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class DockableControl
    {
        private Color bottomGradientColor = Color.FromArgb(0x9f, 180, 0xc1);
        private bool controlActive;
        private bool controlAsPopup;
        private bool controlDockable = true;
        private int controlDockedX;
        private int controlDockedY;
        private ContainerControl controlParentControl;
        private int controlPopupX = -10000;
        private int controlPopupY = -10000;
        private Form m_popup;
        private UserControl m_self;
        private string popupTitle = "";
        private bool showTitleBar = true;
        private bool sizeableWindow;
        private Color topGradientColor = Color.FromArgb(0x56, 0x62, 0x6a);

        public DockableControl(UserControl self)
        {
            this.m_self = self;
        }

        public void closeControl(bool closeAllIncludingPopups)
        {
            if (this.controlActive && (closeAllIncludingPopups || !this.controlAsPopup))
            {
                if (!this.controlAsPopup)
                {
                    ((IDockWindow) this.controlParentControl).RemoveControl(this.m_self);
                }
                else if (this.m_popup != null)
                {
                    if (this.m_popup.Created)
                    {
                        this.controlPopupX = this.m_popup.Location.X;
                        this.controlPopupY = this.m_popup.Location.Y;
                        this.m_popup.Controls.Remove(this.m_self);
                        this.controlActive = false;
                        this.m_popup.Close();
                    }
                    this.m_popup = null;
                }
                this.controlActive = false;
            }
        }

        public void controlDockToggle()
        {
            if (this.controlAsPopup)
            {
                this.display(false, this.controlParentControl, this.controlDockedX, this.controlDockedY);
            }
            else
            {
                this.display(true, this.controlParentControl, this.controlPopupX, this.controlPopupY);
            }
        }

        public void display(ContainerControl parent, int x, int y)
        {
            this.display(this.controlAsPopup, parent, x, y, false);
        }

        public void display(bool asPopup, ContainerControl parent, int x, int y)
        {
            this.display(asPopup, parent, x, y, false);
        }

        public void display(bool asPopup, ContainerControl parent, int x, int y, bool asCustomPanel)
        {
            this.display(asPopup, parent, x, y, asCustomPanel, false);
        }

        public void display(bool asPopup, ContainerControl parent, int x, int y, bool asCustomPanel, bool inTaskBar)
        {
            this.closeControl(true);
            if (parent != null)
            {
                this.controlParentControl = parent;
            }
            this.controlAsPopup = asPopup;
            int num = this.showTitleBar ? 30 : 0;
            if (!asPopup)
            {
                if (x != -10000)
                {
                    this.controlDockedX = x;
                    this.controlDockedY = y;
                }
                IDockWindow controlParentControl = (IDockWindow) this.controlParentControl;
                if (controlParentControl != null)
                {
                    controlParentControl.AddControl(this.m_self, this.controlDockedX, this.controlDockedY);
                    this.controlActive = true;
                }
            }
            else
            {
                if (!asCustomPanel)
                {
                    this.m_popup = new SHKForm();
                }
                else
                {
                    MyFormBase base2 = new MyFormBase {
                        ShowBar = this.showTitleBar
                    };
                    base2.setGradient(this.topGradientColor, this.bottomGradientColor);
                    this.m_popup = base2;
                }
                if (inTaskBar)
                {
                    this.m_popup.ShowInTaskbar = true;
                }
                this.m_popup.SuspendLayout();
                this.m_popup.Icon = Resources.shk_icon;
                this.m_popup.ClientSize = new Size(this.m_self.Size.Width, this.m_self.Size.Height);
                bool flag = false;
                if (this.m_self.Name == "ChatScreen")
                {
                    flag = true;
                    this.m_popup.MaximizeBox = true;
                    this.m_popup.MinimizeBox = true;
                    this.m_popup.ControlBox = true;
                    this.m_popup.FormClosing += new FormClosingEventHandler(((ChatScreen) this.m_self).closeClickForm);
                    ((MyFormBase) this.m_popup).Resizable = true;
                }
                else
                {
                    this.m_popup.MaximizeBox = false;
                    this.m_popup.MinimizeBox = false;
                    this.m_popup.ControlBox = false;
                }
                this.m_popup.Name = this.m_self.Name + "Popup";
                if (this.controlPopupX == -10000)
                {
                    this.m_popup.StartPosition = FormStartPosition.WindowsDefaultLocation;
                }
                else
                {
                    this.m_popup.StartPosition = FormStartPosition.Manual;
                    this.m_popup.Location = new Point(this.controlPopupX, this.controlPopupY);
                }
                if (!asCustomPanel)
                {
                    this.m_self.Location = new Point(0, 0);
                }
                else
                {
                    this.m_self.Location = new Point(0, num);
                    this.m_popup.StartPosition = FormStartPosition.CenterScreen;
                }
                this.m_popup.Text = this.popupTitle;
                if (asCustomPanel)
                {
                    ((MyFormBase) this.m_popup).Title = this.popupTitle;
                    this.m_popup.Text = this.popupTitle;
                    this.m_self.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                    this.m_popup.MinimumSize = new Size(this.m_self.Width, this.m_self.Height + num);
                }
                else if (this.sizeableWindow)
                {
                    this.m_self.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
                    this.m_popup.MinimumSize = new Size(this.m_self.MinimumSize.Width, this.m_self.MinimumSize.Height + num);
                }
                else
                {
                    this.m_popup.MinimumSize = new Size(this.m_self.Size.Width, this.m_self.Size.Height);
                }
                this.m_popup.Controls.Add(this.m_self);
                if (asCustomPanel)
                {
                    this.m_popup.FormBorderStyle = FormBorderStyle.None;
                    if (flag)
                    {
                        ((MyFormBase) this.m_popup).ShowClose = true;
                        ((MyFormBase) this.m_popup).showMinMax();
                    }
                }
                else if (!this.sizeableWindow)
                {
                    this.m_popup.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                }
                else if (!flag)
                {
                    this.m_popup.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                }
                else
                {
                    this.m_popup.FormBorderStyle = FormBorderStyle.Sizable;
                }
                this.m_popup.FormClosing += new FormClosingEventHandler(this.formClosingCallback);
                this.m_popup.ResumeLayout(false);
                this.m_popup.PerformLayout();
                this.m_popup.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
                this.m_popup.Show();
                this.controlPopupX = this.m_popup.Location.X;
                this.controlPopupY = this.m_popup.Location.Y;
                this.controlActive = true;
            }
        }

        private void formClosingCallback(object sender, FormClosingEventArgs e)
        {
            if (this.controlActive)
            {
                if (this.controlDockable)
                {
                    this.display(false, this.controlParentControl, this.controlDockedX, this.controlDockedY);
                }
                else
                {
                    this.closeControl(true);
                }
            }
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.controlDockable = dockable;
            if (title != null)
            {
                this.popupTitle = title;
            }
            if (parent != null)
            {
                this.controlParentControl = parent;
            }
        }

        public void initProperties(bool dockable, string title, ContainerControl parent, bool showBar)
        {
            this.showTitleBar = showBar;
            this.initProperties(dockable, title, parent);
        }

        public void initProperties(bool dockable, string title, ContainerControl parent, bool showBar, Color topColor, Color bottomColor)
        {
            this.topGradientColor = topColor;
            this.bottomGradientColor = bottomColor;
            this.initProperties(dockable, title, parent, showBar);
        }

        public bool isPopup()
        {
            return (this.controlActive && this.controlAsPopup);
        }

        public bool isVisible()
        {
            return this.controlActive;
        }

        public void setPosition(int x, int y)
        {
            if (this.m_self != null)
            {
                this.m_self.Location = new Point(x, y);
            }
        }

        public void setSizeableWindow()
        {
            this.sizeableWindow = true;
        }
    }
}

