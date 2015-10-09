namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ReportTabBar : UserControl, IDockableControl
    {
        private IContainer components;
        private DockableControl dockableControl;
        public TabControl reportTabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;

        public ReportTabBar()
        {
            this.dockableControl = new DockableControl(this);
            this.InitializeComponent();
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
            this.dockableControl.display(asPopup, parent, x, y);
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
            this.reportTabControl = new TabControl();
            this.tabPage1 = new TabPage();
            this.tabPage2 = new TabPage();
            this.reportTabControl.SuspendLayout();
            base.SuspendLayout();
            this.reportTabControl.Controls.Add(this.tabPage1);
            this.reportTabControl.Controls.Add(this.tabPage2);
            this.reportTabControl.Location = new Point(0, 13);
            this.reportTabControl.Name = "reportTabControl";
            this.reportTabControl.SelectedIndex = 0;
            this.reportTabControl.Size = new Size(0x193, 0x15);
            this.reportTabControl.TabIndex = 0;
            this.tabPage1.Location = new Point(4, 0x16);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x18b, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Reports";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Enter += new EventHandler(this.tabPage1_Enter);
            this.tabPage2.Location = new Point(4, 0x16);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(0x18b, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "History";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Enter += new EventHandler(this.tabPage2_Enter);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = ARGBColors.Transparent;
            base.Controls.Add(this.reportTabControl);
            base.Name = "ReportTabBar";
            base.Size = new Size(0x3e0, 0x20);
            this.reportTabControl.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public void initProperties(bool dockable, string title, ContainerControl parent)
        {
            this.dockableControl.initProperties(dockable, title, parent);
        }

        public bool isPopup()
        {
            return this.dockableControl.isPopup();
        }

        public bool isVisible()
        {
            return this.dockableControl.isVisible();
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            InterfaceMgr.Instance.switchReportTabs(0);
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            InterfaceMgr.Instance.switchReportTabs(1);
        }
    }
}

