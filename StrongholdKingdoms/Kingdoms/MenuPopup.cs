namespace Kingdoms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class MenuPopup : Form
    {
        private MenuBackground background = new MenuBackground();
        private bool closeOnClick = true;
        private bool closeOnDoubleClick = true;
        private int columnWidth;
        private IContainer components;
        private List<CustomSelfDrawPanel.CSDControl> currentControls = new List<CustomSelfDrawPanel.CSDControl>();
        private int curXPos = 4;
        private int curYPos = 4;
        private MenuCallback doubleClickCallback;
        private bool entered;
        private int fixedWidth = -1;
        private int lastClickedData = -1000;
        private int lineHeight = 0x17;
        private int maxWidth;
        private int maxYPos = 4;
        private MenuCallback menuCallback;
        private DateTime mouseClickedLastTime = DateTime.MinValue;
        private MenuItemRolloverDelegate mouseLeaveDelegate;
        private MenuItemRolloverDelegate mouseOverDelegate;

        public MenuPopup()
        {
            InterfaceMgr.Instance.closeMenuPopup();
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.BackColor = Color.FromArgb(0xff, 0xe8, 230, 0xe4);
            this.background.MouseEnter += new EventHandler(this.MenuPopup_MouseEnter);
            this.background.MouseLeave += new EventHandler(this.MenuPopup_MouseLeave);
            base.Controls.Add(this.background);
            this.currentControls.Clear();
        }

        public void addBar()
        {
            CustomSelfDrawPanel.CSDFill control = new CustomSelfDrawPanel.CSDFill {
                Position = new Point(this.curXPos + 3, this.curYPos + 3),
                FillColor = Color.FromArgb(0x60, 0, 0, 0),
                Size = new Size(6, 2)
            };
            this.background.addControl(control);
            this.currentControls.Add(control);
            this.curYPos += 8;
        }

        public CustomSelfDrawPanel.CSDButton addMenuItem(string ident, int id)
        {
            return this.addMenuItem(ident, id, false);
        }

        public CustomSelfDrawPanel.CSDButton addMenuItem(string ident, int id, bool bold)
        {
            FontStyle regular = FontStyle.Regular;
            if (bold)
            {
                regular = FontStyle.Bold;
            }
            CustomSelfDrawPanel.CSDButton control = new CustomSelfDrawPanel.CSDButton {
                Position = new Point(this.curXPos, this.curYPos)
            };
            Graphics graphics = base.CreateGraphics();
            Size size = graphics.MeasureString(ident, FontManager.GetFont("Microsoft Sans Serif", 8.25f, regular), 0x3e8).ToSize();
            graphics.Dispose();
            control.Size = new Size((size.Width + 4) + 8, this.lineHeight);
            control.FillRectOverColor = Color.FromArgb(0x20, 0, 0, 0);
            control.Text.Text = ident;
            control.Text.Position = new Point(4, 0);
            control.Text.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f, regular);
            control.Text.Color = ARGBColors.Black;
            control.TextYOffset = 0;
            control.Text.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.CENTER_LEFT;
            if (this.mouseOverDelegate != null)
            {
                control.setButtonMouseOverDelegate(new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseOverItem), new CustomSelfDrawPanel.CSDControl.CSD_MouseOverDelegate(this.mouseLeaveItem));
            }
            control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.Button_Click));
            control.Data = id;
            this.background.addControl(control);
            this.currentControls.Add(control);
            int width = control.Width;
            if (width > this.columnWidth)
            {
                this.columnWidth = width;
            }
            this.curYPos += 1 + control.Height;
            int num2 = 0;
            if (this.curYPos > (this.maxYPos + num2))
            {
                this.maxYPos = this.curYPos - num2;
            }
            return control;
        }

        private void Button_Click()
        {
            if (this.doubleClickCallback != null)
            {
                if ((this.background.ClickedControl.Data == this.lastClickedData) && (this.lastClickedData != -1000))
                {
                    this.doubleClickCallback(this.lastClickedData);
                    if (this.closeOnDoubleClick)
                    {
                        InterfaceMgr.Instance.closeMenuPopup();
                    }
                }
                else
                {
                    this.mouseClickedLastTime = DateTime.Now;
                    this.lastClickedData = this.background.ClickedControl.Data;
                    if (this.menuCallback != null)
                    {
                        this.menuCallback(this.background.ClickedControl.Data);
                    }
                }
            }
            else
            {
                if (this.closeOnClick)
                {
                    InterfaceMgr.Instance.closeMenuPopup();
                }
                if (this.menuCallback != null)
                {
                    this.menuCallback(this.background.ClickedControl.Data);
                }
            }
        }

        public void clearHighlights()
        {
            foreach (CustomSelfDrawPanel.CSDControl control in this.background.baseControl.Controls)
            {
                if (control.GetType() == typeof(CustomSelfDrawPanel.CSDButton))
                {
                    CustomSelfDrawPanel.CSDButton button = (CustomSelfDrawPanel.CSDButton) control;
                    if (button.FillRectVariant)
                    {
                        button.FillRectVariant = false;
                        button.invalidate();
                    }
                }
            }
        }

        public void closeOnClickOnly()
        {
            this.background.MouseEnter -= new EventHandler(this.MenuPopup_MouseEnter);
            this.background.MouseLeave -= new EventHandler(this.MenuPopup_MouseLeave);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void highlightByID(int id, Color col)
        {
            foreach (CustomSelfDrawPanel.CSDControl control in this.background.baseControl.Controls)
            {
                if (control.GetType() == typeof(CustomSelfDrawPanel.CSDButton))
                {
                    CustomSelfDrawPanel.CSDButton button = (CustomSelfDrawPanel.CSDButton) control;
                    if (button.Data == id)
                    {
                        button.FillRectColor = col;
                        button.invalidate();
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x3e8, 0x18);
            base.ControlBox = false;
            base.FormBorderStyle = FormBorderStyle.None;
            this.MinimumSize = new Size(10, 10);
            base.Name = "MenuPopup";
            base.Opacity = 0.95;
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "MenuPopup";
            base.MouseEnter += new EventHandler(this.MenuPopup_MouseEnter);
            base.MouseLeave += new EventHandler(this.MenuPopup_MouseLeave);
            base.ResumeLayout(false);
        }

        public static bool isAMenuVisible()
        {
            MenuPopup popup = InterfaceMgr.Instance.getMenuPopup();
            return ((popup != null) && popup.Visible);
        }

        private void MenuPopup_MouseEnter(object sender, EventArgs e)
        {
            this.entered = true;
        }

        private void MenuPopup_MouseLeave(object sender, EventArgs e)
        {
            if (this.entered && (InterfaceMgr.Instance.ParentForm != null))
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                InterfaceMgr.Instance.ParentForm.PointToClient(p);
                Rectangle rectangle = new Rectangle(base.Location, base.Size);
                if (!rectangle.Contains(p))
                {
                    InterfaceMgr.Instance.closeMenuPopup();
                }
            }
        }

        public void mouseLeaveItem()
        {
            if (this.mouseLeaveDelegate != null)
            {
                this.mouseLeaveDelegate(0);
            }
        }

        public void mouseOverDelegates(MenuItemRolloverDelegate overDel, MenuItemRolloverDelegate leaveDel)
        {
            this.mouseOverDelegate = overDel;
            this.mouseLeaveDelegate = leaveDel;
        }

        public void mouseOverItem()
        {
            if ((this.mouseOverDelegate != null) && (this.background.OverControl != null))
            {
                this.mouseOverDelegate(this.background.OverControl.Data);
            }
        }

        public void newColumn()
        {
            this.updateCurrentControls(this.columnWidth);
            this.curXPos += this.columnWidth + 4;
            this.curYPos = 4;
            this.columnWidth = 0;
        }

        public void setBackColour(Color col)
        {
            this.BackColor = col;
        }

        public void setCallBack(MenuCallback callback)
        {
            this.menuCallback = callback;
        }

        public void setDoubleClickCallBack(MenuCallback callback)
        {
            this.doubleClickCallback = callback;
        }

        public void setFixedWidth(int width)
        {
            this.fixedWidth = width;
        }

        public void setLineHeight(int height)
        {
            this.lineHeight = height;
        }

        public void setPosition(int x, int y)
        {
            base.Location = new Point(x, y);
        }

        public void showMenu()
        {
            if (this.fixedWidth >= 0)
            {
                this.columnWidth = this.fixedWidth - 8;
            }
            this.updateCurrentControls(this.columnWidth);
            base.Width = this.maxWidth;
            base.Height = this.maxYPos;
            this.background.Size = new Size(base.Width, base.Height);
            Rectangle rectangle = InterfaceMgr.Instance.getWindowRect();
            if ((base.Location.X + base.Width) > rectangle.Right)
            {
                Point location = base.Location;
                location.X = (rectangle.Right - base.Width) - 5;
                base.Location = location;
            }
            InterfaceMgr.Instance.setCurrentMenuPopup(this);
            base.Show(InterfaceMgr.Instance.ParentForm);
        }

        public void update()
        {
            if (this.lastClickedData != -1000)
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.mouseClickedLastTime);
                if (span.TotalMilliseconds > 500.0)
                {
                    this.lastClickedData = -1000;
                    if (this.closeOnClick)
                    {
                        InterfaceMgr.Instance.closeMenuPopup();
                    }
                }
            }
        }

        private void updateCurrentControls(int width)
        {
            foreach (CustomSelfDrawPanel.CSDControl control in this.currentControls)
            {
                if (control.GetType() == typeof(CustomSelfDrawPanel.CSDButton))
                {
                    CustomSelfDrawPanel.CSDButton button = (CustomSelfDrawPanel.CSDButton) control;
                    button.Size = new Size(width, button.Height);
                }
                else if (control.GetType() == typeof(CustomSelfDrawPanel.CSDFill))
                {
                    CustomSelfDrawPanel.CSDFill fill = (CustomSelfDrawPanel.CSDFill) control;
                    fill.Size = new Size((width + 4) - 10, fill.Height);
                }
            }
            this.currentControls.Clear();
            this.maxWidth += width + 8;
        }

        public bool CloseOnClick
        {
            set
            {
                this.closeOnClick = value;
            }
        }

        public bool CloseOnDoubleClick
        {
            set
            {
                this.closeOnDoubleClick = value;
            }
        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        public delegate void MenuCallback(int id);

        public delegate void MenuItemRolloverDelegate(int id);
    }
}

