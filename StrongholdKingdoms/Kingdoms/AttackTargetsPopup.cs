namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AttackTargetsPopup : MyFormBase
    {
        private bool closing;
        private IContainer components;
        private AttackTargetsPanel customPanel;

        public AttackTargetsPopup()
        {
            this.InitializeComponent();
            base.Title = SK.Text("Attack_Targets", "Attack Targets");
            this.customPanel.init(this);
        }

        private void AttackTargetsPoup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing) && !this.closing)
            {
                this.closing = true;
                InterfaceMgr.Instance.closeAttackTargetsPopup();
            }
        }

        private void closeFunction()
        {
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
            this.customPanel = new AttackTargetsPanel();
            base.SuspendLayout();
            this.customPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.customPanel.ClickThru = false;
            this.customPanel.Location = new Point(0, 0x22);
            this.customPanel.Name = "customPanel";
            this.customPanel.PanelActive = true;
            this.customPanel.Size = base.Size;
            this.customPanel.StoredGraphics = null;
            this.customPanel.TabIndex = 0x63;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(700, 450);
            base.Controls.Add(this.customPanel);
            this.DoubleBuffered = true;
            base.FormClosing += new FormClosingEventHandler(this.AttackTargetsPoup_FormClosing);
            base.Name = "AttackTargetsPopup";
            base.ShowClose = true;
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Manage Formations";
            base.TransparencyKey = ARGBColors.Fuchsia;
            base.Controls.SetChildIndex(this.customPanel, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

