namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class AboutPopup : MyFormBase
    {
        private IContainer components;
        private Label label1;
        private Label label2;
        private Label label4;
        private LinkLabel lblCredits;
        private Label lblGeckFX;
        private Label lblSlimDX;
        private Label lblVersionNumber;
        private Label lblXMLRPC;
        private Label lblZLIB;
        private LinkLabel linkLabel1;

        public AboutPopup()
        {
            this.InitializeComponent();
            this.label1.Font = FontManager.GetFont("Microsoft Sans Serif", 14f, FontStyle.Regular);
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            base.ShowClose = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void init()
        {
            this.label1.Text = SK.Text("About_Stronghold_Kingdoms", "Stronghold Kingdoms");
            this.label2.Text = SK.Text("About_Firefly", "(c)2010 Firefly Studios Ltd");
            this.lblCredits.Text = SK.Text("About_Credits", "Credits");
            this.Text = base.Title = SK.Text("About_About_Stronghold_Kingdoms", "About Stronghold Kingdoms");
            this.lblSlimDX.Text = SK.Text("About_Copyright_SlimDX", "SlimDX Copyright (c) 2007-2010 SlimDX Group");
            this.lblGeckFX.Text = SK.Text("About_Copyright_GeckoFX", "GeckoFX Copyright (c) 2008 Skybound Software");
            this.lblXMLRPC.Text = SK.Text("About_Copyright_XMLRPC", "XML-RPC.NET Copyright (c) 2006 Charles Cook");
            this.lblZLIB.Text = SK.Text("About_Copyright_XLIB", "zlib Copyright (C) 1995-2004 Jean-loup Gailly and Mark Adler");
            string[] strArray = Regex.Split(Application.StartupPath, @"\\");
            if (strArray.Length > 0)
            {
                this.lblVersionNumber.Visible = true;
                this.lblVersionNumber.Text = SK.Text("About_Version", "Version") + " : " + strArray[strArray.Length - 1];
            }
            if (Program.mySettings.LanguageIdent == "de")
            {
                this.linkLabel1.Text = "de.strongholdkingdoms.com";
            }
            else if (Program.mySettings.LanguageIdent == "fr")
            {
                this.linkLabel1.Text = "fr.strongholdkingdoms.com";
            }
            else if (Program.mySettings.LanguageIdent == "ru")
            {
                this.linkLabel1.Text = "ru.strongholdkingdoms.com";
            }
            else if (Program.mySettings.LanguageIdent == "es")
            {
                this.linkLabel1.Text = "es.strongholdkingdoms.com";
            }
            else if (Program.mySettings.LanguageIdent == "pl")
            {
                this.linkLabel1.Text = "pl.strongholdkingdoms.com";
            }
            else if (Program.mySettings.LanguageIdent == "tr")
            {
                this.linkLabel1.Text = "tr.strongholdkingdoms.com";
            }
            else if (Program.mySettings.LanguageIdent == "it")
            {
                this.linkLabel1.Text = "it.strongholdkingdoms.com";
            }
            else if (Program.mySettings.LanguageIdent == "pt")
            {
                this.linkLabel1.Text = "pt.strongholdkingdoms.com";
            }
            else
            {
                this.linkLabel1.Text = "www.strongholdkingdoms.com";
            }
        }

        private void InitializeComponent()
        {
            int num = 0;
            int num2 = 0;
            this.label1 = new Label();
            this.label2 = new Label();
            this.linkLabel1 = new LinkLabel();
            this.lblVersionNumber = new Label();
            this.lblCredits = new LinkLabel();
            this.lblSlimDX = new Label();
            this.lblGeckFX = new Label();
            this.lblXMLRPC = new Label();
            this.lblZLIB = new Label();
            this.label4 = new Label();
            base.SuspendLayout();
            this.label1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label1.BackColor = ARGBColors.Transparent;
            this.label1.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(14, 50);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x151, 0x17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Stronghold Kingdoms";
            this.label1.TextAlign = ContentAlignment.TopCenter;
            this.label2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.label2.BackColor = ARGBColors.Transparent;
            this.label2.Location = new Point(14, 0x53);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x151, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "(c)2010 Firefly Studios Ltd";
            this.label2.TextAlign = ContentAlignment.TopCenter;
            this.linkLabel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.linkLabel1.BackColor = ARGBColors.Transparent;
            this.linkLabel1.Location = new Point(12, 0x11a);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new Size(0x151, 13);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "www.strongholdkingdoms.com";
            this.linkLabel1.TextAlign = ContentAlignment.TopCenter;
            this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            this.lblVersionNumber.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblVersionNumber.BackColor = ARGBColors.Transparent;
            this.lblVersionNumber.Location = new Point(13, 0x139);
            this.lblVersionNumber.Name = "lblVersionNumber";
            this.lblVersionNumber.Size = new Size(0x151, 13);
            this.lblVersionNumber.TabIndex = 6;
            this.lblVersionNumber.Text = "1.1.1.1";
            this.lblVersionNumber.TextAlign = ContentAlignment.TopRight;
            this.lblCredits.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblCredits.BackColor = ARGBColors.Transparent;
            this.lblCredits.Location = new Point(12, 0xfb);
            this.lblCredits.Name = "lblCredits";
            this.lblCredits.Size = new Size(0x151, 13);
            this.lblCredits.TabIndex = 12;
            this.lblCredits.TabStop = true;
            this.lblCredits.Text = "Credits";
            this.lblCredits.TextAlign = ContentAlignment.TopCenter;
            this.lblCredits.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lblCredits_LinkClicked);
            this.lblSlimDX.AutoSize = true;
            this.lblSlimDX.BackColor = ARGBColors.Transparent;
            this.lblSlimDX.Location = new Point(0x43, 0x76);
            this.lblSlimDX.Name = "lblSlimDX";
            this.lblSlimDX.Size = new Size(0xe2, 13);
            this.lblSlimDX.TabIndex = 13;
            this.lblSlimDX.Text = "SlimDX Copyright (c) 2007-2010 SlimDX Group";
            this.lblGeckFX.AutoSize = true;
            this.lblGeckFX.BackColor = ARGBColors.Transparent;
            this.lblGeckFX.Location = new Point(0x3f, 0x90);
            this.lblGeckFX.Name = "lblGeckFX";
            this.lblGeckFX.Size = new Size(0xea, 13);
            this.lblGeckFX.TabIndex = 14;
            this.lblGeckFX.Text = "GeckoFX Copyright \x00a9 2008 Skybound Software";
            this.lblXMLRPC.AutoSize = true;
            this.lblXMLRPC.BackColor = ARGBColors.Transparent;
            this.lblXMLRPC.Location = new Point(0x3f, 170);
            this.lblXMLRPC.Name = "lblXMLRPC";
            this.lblXMLRPC.Size = new Size(0xea, 13);
            this.lblXMLRPC.TabIndex = 15;
            this.lblXMLRPC.Text = "XML-RPC.NET Copyright (c) 2006 Charles Cook";
            this.lblZLIB.AutoSize = true;
            this.lblZLIB.BackColor = ARGBColors.Transparent;
            this.lblZLIB.Location = new Point(0x23, 0xc4);
            this.lblZLIB.Name = "lblZLIB";
            this.lblZLIB.Size = new Size(0x123, 13);
            this.lblZLIB.TabIndex = 0x10;
            this.lblZLIB.Text = "zlib Copyright (C) 1995-2004 Jean-loup Gailly and Mark Adler";
            this.label4.AutoSize = true;
            this.label4.BackColor = ARGBColors.Transparent;
            this.label4.Location = new Point(0x4a, 0xde);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0xd4, 13);
            this.label4.TabIndex = 0x11;
            this.label4.Text = "NAudio Copyright (C) 2007 Ray Molenkamp";
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x169, 0x14f);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.lblZLIB);
            base.Controls.Add(this.lblXMLRPC);
            base.Controls.Add(this.lblGeckFX);
            base.Controls.Add(this.lblSlimDX);
            base.Controls.Add(this.lblCredits);
            base.Controls.Add(this.lblVersionNumber);
            base.Controls.Add(this.linkLabel1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Icon = Resources.shk_icon;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "AboutPopup";
            base.ShowClose = true;
            base.ShowIcon = false;
            this.Text = "About Stronghold Kingdoms";
            base.Controls.SetChildIndex(this.label1, 0);
            base.Controls.SetChildIndex(this.label2, 0);
            base.Controls.SetChildIndex(this.linkLabel1, 0);
            base.Controls.SetChildIndex(this.lblVersionNumber, 0);
            base.Controls.SetChildIndex(this.lblCredits, 0);
            base.Controls.SetChildIndex(this.lblSlimDX, 0);
            base.Controls.SetChildIndex(this.lblGeckFX, 0);
            base.Controls.SetChildIndex(this.lblXMLRPC, 0);
            base.Controls.SetChildIndex(this.lblZLIB, 0);
            base.Controls.SetChildIndex(this.label4, 0);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void lblCredits_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string str;
            if (Program.mySettings.LanguageIdent == "de")
            {
                str = "http://hilfe.strongholdkingdoms.de/index.php/\x00dcber_Stronghold_Kingdoms#Mitwirkende";
            }
            else if (Program.mySettings.LanguageIdent == "fr")
            {
                str = "http://aide.strongholdkingdoms.com/index.php/%C3%80_propos_de_Stronghold_Kingdoms#Credits";
            }
            else if (Program.mySettings.LanguageIdent == "ru")
            {
                str = "http://help.ru.strongholdkingdoms.com/index.php/%D0%9E%D0%B1_%D0%B8%D0%B3%D1%80%D0%B5_Stronghold_Kingdoms#Credits";
            }
            else if (Program.mySettings.LanguageIdent == "es")
            {
                str = "http://ayuda.strongholdkingdoms.com/index.php/Acerca_de_Stronghold_Kingdoms#credits";
            }
            else if (Program.mySettings.LanguageIdent == "pl")
            {
                str = "http://help.strongholdkingdoms.com/index.php/About_Stronghold_Kingdoms#Credits";
            }
            else if (Program.mySettings.LanguageIdent == "tr")
            {
                str = "http://help.strongholdkingdoms.com/index.php/About_Stronghold_Kingdoms#Credits";
            }
            else if (Program.mySettings.LanguageIdent == "it")
            {
                str = "http://help.strongholdkingdoms.com/index.php/About_Stronghold_Kingdoms#Credits";
            }
            else if (Program.mySettings.LanguageIdent == "pt")
            {
                str = "http://help.strongholdkingdoms.com/index.php/About_Stronghold_Kingdoms#Credits";
            }
            else
            {
                str = "http://help.strongholdkingdoms.com/index.php/About_Stronghold_Kingdoms#Credits";
            }
            new Process { StartInfo = { FileName = str } }.Start();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string str;
            if (Program.mySettings.LanguageIdent == "de")
            {
                str = "http://de.strongholdkingdoms.com";
            }
            else if (Program.mySettings.LanguageIdent == "fr")
            {
                str = "http://fr.strongholdkingdoms.com";
            }
            else if (Program.mySettings.LanguageIdent == "ru")
            {
                str = "http://ru.strongholdkingdoms.com";
            }
            else if (Program.mySettings.LanguageIdent == "es")
            {
                str = "http://es.strongholdkingdoms.com";
            }
            else if (Program.mySettings.LanguageIdent == "pl")
            {
                str = "http://pl.strongholdkingdoms.com";
            }
            else if (Program.mySettings.LanguageIdent == "tr")
            {
                str = "http://tr.strongholdkingdoms.com";
            }
            else if (Program.mySettings.LanguageIdent == "it")
            {
                str = "http://it.strongholdkingdoms.com";
            }
            else if (Program.mySettings.LanguageIdent == "pt")
            {
                str = "http://pt.strongholdkingdoms.com";
            }
            else
            {
                str = "http://www.strongholdkingdoms.com";
            }
            new Process { StartInfo = { FileName = str } }.Start();
        }
    }
}

