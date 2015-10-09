namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    public class SharedIPErrorPopup : MyFormBase
    {
        private BitmapButton btnOK;
        private IContainer components;
        private Label lblExplanation;
        private LinkLabel linkLabelMoreInfo;

        public SharedIPErrorPopup()
        {
            this.InitializeComponent();
            this.lblExplanation.Font = FontManager.GetFont("Microsoft Sans Serif", 9f, FontStyle.Regular);
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("SharedIPErrorPopup_close");
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

        public void init(string explanation)
        {
            this.linkLabelMoreInfo.Text = SK.Text("SharedIPErrorPopup_More_Info", "Click Here for More Information");
            this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
            this.Text = base.Title = SK.Text("SharedIPErrorPopup_Shared_Connwectin", "Shared Connection Detected");
            this.lblExplanation.Text = explanation;
        }

        private void InitializeComponent()
        {
            this.lblExplanation = new Label();
            this.linkLabelMoreInfo = new LinkLabel();
            this.btnOK = new BitmapButton();
            base.SuspendLayout();
            this.lblExplanation.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.lblExplanation.BackColor = ARGBColors.Transparent;
            this.lblExplanation.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblExplanation.Location = new Point(8, 0x27);
            this.lblExplanation.Name = "lblExplanation";
            this.lblExplanation.Size = new Size(0x159, 0x4c);
            this.lblExplanation.TabIndex = 0;
            this.lblExplanation.Text = "label1";
            this.linkLabelMoreInfo.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.linkLabelMoreInfo.BackColor = ARGBColors.Transparent;
            this.linkLabelMoreInfo.Location = new Point(12, 0x86);
            this.linkLabelMoreInfo.Name = "linkLabelMoreInfo";
            this.linkLabelMoreInfo.Size = new Size(0xbd, 13);
            this.linkLabelMoreInfo.TabIndex = 3;
            this.linkLabelMoreInfo.TabStop = true;
            this.linkLabelMoreInfo.Text = "Click Here for More Information";
            this.linkLabelMoreInfo.TextAlign = ContentAlignment.MiddleLeft;
            this.linkLabelMoreInfo.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabelMoreInfo_LinkClicked);
            this.btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnOK.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnOK.Location = new Point(0x110, 0x7e);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x169, 0x99);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.linkLabelMoreInfo);
            base.Controls.Add(this.lblExplanation);
            base.Name = "SharedIPErrorPopup";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "Shared Connection Detected";
            base.Controls.SetChildIndex(this.lblExplanation, 0);
            base.Controls.SetChildIndex(this.linkLabelMoreInfo, 0);
            base.Controls.SetChildIndex(this.btnOK, 0);
            base.ResumeLayout(false);
        }

        private void linkLabelMoreInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process process = new Process();
            switch (Program.mySettings.LanguageIdent)
            {
                case "de":
                    process.StartInfo.FileName = "http://hilfe.strongholdkingdoms.de/index.php/Gemeinsame_IP-Adressen";
                    break;

                case "fr":
                    process.StartInfo.FileName = "http://aide.strongholdkingdoms.com/index.php/Adresse_IP_Partag%C3%A9e";
                    break;

                case "ru":
                    process.StartInfo.FileName = "http://help.ru.strongholdkingdoms.com/index.php/IP_Sharing";
                    break;

                case "es":
                    process.StartInfo.FileName = "http://ayuda.strongholdkingdoms.com/index.php/Compartir_IP";
                    break;

                case "pl":
                    process.StartInfo.FileName = "http://pomoc.strongholdkingdoms.com/index.php/Wsp%C3%B3%C5%82u%C5%BCytkowanie_adresu_IP";
                    break;

                case "it":
                    process.StartInfo.FileName = "http://help.strongholdkingdoms.com/index.php/IP_Sharing";
                    break;

                case "tr":
                    process.StartInfo.FileName = "http://help.strongholdkingdoms.com/index.php/IP_Sharing";
                    break;

                case "pt":
                    process.StartInfo.FileName = "http://help.strongholdkingdoms.com/index.php/IP_Sharing";
                    break;

                default:
                    process.StartInfo.FileName = "http://help.strongholdkingdoms.com/index.php/IP_Sharing";
                    break;
            }
            process.Start();
        }

        public static void showSharedIPPopup(string explanation)
        {
            SharedIPErrorPopup popup = new SharedIPErrorPopup();
            popup.init(explanation);
            popup.ShowDialog();
            popup.Dispose();
        }
    }
}

