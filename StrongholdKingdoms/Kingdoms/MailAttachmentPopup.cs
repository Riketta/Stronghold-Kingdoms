namespace Kingdoms
{
    using CommonTypes;
    using DXGraphics;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MailAttachmentPopup : UserControl, IDockableControl
    {
        private IContainer components;
        private MailAttachmentPanel customPanel;
        private DockableControl dockableControl;
        private double lastUpdateTime;
        private TextBox tbPlayerInput;
        private TextBox tbRegionInput;

        public MailAttachmentPopup(MailScreen mailParent)
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
            this.customPanel.init(base.Size, this, mailParent);
        }

        public void clearContents(bool includeLinks)
        {
            if (this.customPanel != null)
            {
                this.customPanel.clearContents(includeLinks);
            }
            this.tbPlayerInput.Text = "";
            this.tbRegionInput.Text = "";
        }

        public void closeControl(bool includePopups)
        {
            this.dockableControl.closeControl(includePopups);
        }

        public void controlDockToggle()
        {
            this.dockableControl.controlDockToggle();
        }

        public void display(ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(parent, x, y);
        }

        public void display(bool asPopup, ContainerControl parent, int x, int y)
        {
            this.dockableControl.display(asPopup, parent, x, y, true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public List<MailLink> getLinks()
        {
            if (this.customPanel == null)
            {
                return new List<MailLink>();
            }
            return this.customPanel.linkList;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.customPanel = new MailAttachmentPanel();
            this.tbPlayerInput = new TextBox();
            this.tbRegionInput = new TextBox();
            base.AutoScaleMode = AutoScaleMode.Font;
            base.SuspendLayout();
            base.Size = new Size(0xd9, 430);
            this.tbPlayerInput.BackColor = Color.FromArgb(0xf7, 0xfc, 0xfe);
            this.tbPlayerInput.ForeColor = ARGBColors.Black;
            this.tbPlayerInput.Location = new Point(0x1b, 0x55);
            this.tbPlayerInput.MaxLength = 50;
            this.tbPlayerInput.Name = "tbPlayerInput";
            this.tbPlayerInput.Size = new Size(160, 20);
            this.tbPlayerInput.TabIndex = 11;
            this.tbPlayerInput.TextChanged += new EventHandler(this.tbFindInput_TextChanged);
            this.tbPlayerInput.KeyUp += new KeyEventHandler(this.tbFindInput_KeyUp);
            this.tbPlayerInput.KeyPress += new KeyPressEventHandler(this.tbFindInput_KeyPress);
            this.tbPlayerInput.Visible = false;
            this.tbRegionInput.BackColor = Color.FromArgb(0xf7, 0xfc, 0xfe);
            this.tbRegionInput.ForeColor = ARGBColors.Black;
            this.tbRegionInput.Location = new Point(0x1b, 0x55);
            this.tbRegionInput.MaxLength = 50;
            this.tbRegionInput.Name = "tbRegionInput";
            this.tbRegionInput.Size = new Size(160, 20);
            this.tbRegionInput.TabIndex = 12;
            this.tbRegionInput.TextChanged += new EventHandler(this.tbFindInput_TextChanged);
            this.tbRegionInput.KeyUp += new KeyEventHandler(this.tbFindInput_KeyUp);
            this.tbRegionInput.KeyPress += new KeyPressEventHandler(this.tbFindInput_KeyPress);
            this.tbRegionInput.Visible = false;
            this.customPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.customPanel.ClickThru = false;
            this.customPanel.Location = new Point(0, 0);
            this.customPanel.Name = "customPanel";
            this.customPanel.PanelActive = true;
            this.customPanel.StoredGraphics = null;
            this.customPanel.TabIndex = 0x63;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.BorderStyle = BorderStyle.None;
            base.Name = "ReligiousReportPanel";
            base.Controls.Add(this.tbPlayerInput);
            base.Controls.Add(this.tbRegionInput);
            base.Controls.Add(this.customPanel);
            base.ResumeLayout(false);
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent, true);
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        public void SetLinks(List<MailLink> inputList, bool readOnly)
        {
            if (this.customPanel != null)
            {
                this.customPanel.linkList = inputList;
                this.customPanel.setReadOnly(readOnly);
                this.customPanel.initCurrentAttachments();
            }
        }

        public void setReadOnly(bool value)
        {
            if (this.customPanel != null)
            {
                this.customPanel.setReadOnly(value);
            }
        }

        public void setTextBoxVisible(int type)
        {
            switch (type)
            {
                case 1:
                    this.tbPlayerInput.Visible = true;
                    this.tbRegionInput.Visible = false;
                    return;

                case 3:
                    this.tbPlayerInput.Visible = false;
                    this.tbRegionInput.Visible = true;
                    return;
            }
            this.tbPlayerInput.Visible = false;
            this.tbRegionInput.Visible = false;
        }

        private void tbFindInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private void tbFindInput_KeyUp(object sender, KeyEventArgs e)
        {
            this.lastUpdateTime = DXTimer.GetCurrentMilliseconds();
        }

        private void tbFindInput_TextChanged(object sender, EventArgs e)
        {
            this.lastUpdateTime = DXTimer.GetCurrentMilliseconds();
        }

        public void update()
        {
            this.updateSearch();
        }

        private void updateSearch()
        {
            string textInput = "";
            bool flag = false;
            if (this.tbPlayerInput.Visible)
            {
                textInput = this.tbPlayerInput.Text;
            }
            else if (this.tbRegionInput.Visible)
            {
                flag = true;
                textInput = this.tbRegionInput.Text;
            }
            if (this.lastUpdateTime != 0.0)
            {
                double currentMilliseconds = DXTimer.GetCurrentMilliseconds();
                if ((currentMilliseconds - this.lastUpdateTime) > 1000.0)
                {
                    if (textInput.Length == 0)
                    {
                        this.lastUpdateTime = 0.0;
                    }
                    else if ((((textInput.Length == 1) || (textInput.Length == 2)) && ((currentMilliseconds - this.lastUpdateTime) > 2000.0)) || (textInput.Length > 2))
                    {
                        this.lastUpdateTime = 0.0;
                        if (this.customPanel != null)
                        {
                            if (flag)
                            {
                                this.customPanel.searchRegionUpdateCallback(textInput);
                            }
                            else
                            {
                                this.customPanel.searchPlayerUpdateCallback(textInput);
                            }
                        }
                    }
                }
            }
        }
    }
}

