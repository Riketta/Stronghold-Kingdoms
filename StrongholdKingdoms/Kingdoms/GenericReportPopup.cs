namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class GenericReportPopup : UserControl, IDockableControl
    {
        private IContainer components;
        private GenericReportPanelBasic customPanel;
        private DockableControl dockableControl;
        private Panel panel1;

        public GenericReportPopup()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent(null);
        }

        public GenericReportPopup(GenericReportPanelBasic contentPanel)
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent(contentPanel);
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

        private void InitializeComponent(GenericReportPanelBasic contentPanel)
        {
            if (contentPanel == null)
            {
                this.customPanel = new GenericReportPanelBasic();
            }
            else
            {
                this.customPanel = contentPanel;
            }
            base.Size = this.customPanel.Size;
            base.SuspendLayout();
            this.customPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.customPanel.ClickThru = false;
            this.customPanel.Location = new Point(0, 2);
            this.customPanel.Name = "customPanel";
            this.customPanel.PanelActive = true;
            this.customPanel.StoredGraphics = null;
            this.customPanel.TabIndex = 0x63;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.BorderStyle = BorderStyle.None;
            base.Name = "ReligiousReportPanel";
            this.customPanel.Size = base.Size;
            base.Controls.Add(this.customPanel);
            base.ResumeLayout(false);
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent, false, Color.FromArgb(0xe0, 0xea, 0xf5), Color.FromArgb(0xbf, 0xc9, 0xd3));
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        public void setData(GetReport_ReturnType returnData)
        {
            this.customPanel.init(this, base.Size, null);
            this.customPanel.setData(returnData);
        }
    }
}

