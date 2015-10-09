namespace Kingdoms
{
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AdminIngameMessage : MyFormBase
    {
        private BitmapButton btnCancel;
        private BitmapButton btnSend;
        private IContainer components;
        private Label label1;
        private TextBox tbDuration;
        private TextBox tbMaintenanceMessage;

        public AdminIngameMessage()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.Text = base.Title = "Admin Ingame Message";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("AdminIngameMessage_close");
            base.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            int num = this.getInt32FromString(this.tbDuration.Text);
            if (num < 1)
            {
                num = 1;
            }
            RemoteServices.Instance.SetAdminMessage(this.tbMaintenanceMessage.Text, 0x3e8 + num);
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public int getInt32FromString(string text)
        {
            if (text.Length == 0)
            {
                return 0;
            }
            return Convert.ToInt32(text);
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(AdminIngameMessage));
            this.tbMaintenanceMessage = new TextBox();
            this.btnSend = new BitmapButton();
            this.btnCancel = new BitmapButton();
            this.tbDuration = new TextBox();
            this.label1 = new Label();
            base.SuspendLayout();
            this.tbMaintenanceMessage.Location = new Point(12, 0x17);
            this.tbMaintenanceMessage.Multiline = true;
            this.tbMaintenanceMessage.Name = "tbMaintenanceMessage";
            this.tbMaintenanceMessage.ScrollBars = ScrollBars.Vertical;
            this.tbMaintenanceMessage.Size = new Size(450, 0x141);
            this.tbMaintenanceMessage.TabIndex = 0;
            this.btnSend.Location = new Point(0x183, 360);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new Size(0x4b, 0x17);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new EventHandler(this.btnSend_Click);
            this.btnCancel.Location = new Point(0x132, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.tbDuration.Location = new Point(12, 360);
            this.tbDuration.Name = "tbDuration";
            this.tbDuration.Size = new Size(100, 20);
            this.tbDuration.TabIndex = 3;
            this.tbDuration.Text = "5";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x76, 0x16d);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x90, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Message Duration in Minutes";
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x1da, 0x18b);
            base.ControlBox = false;
            base.Controls.Add(this.label1);
            base.Controls.Add(this.tbDuration);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnSend);
            base.Controls.Add(this.tbMaintenanceMessage);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Icon = Resources.shk_icon;
            base.Name = "AdminIngameMessage";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Admin Ingame Message";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

