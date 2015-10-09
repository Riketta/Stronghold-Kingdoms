namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class LoggingOutPopup : MyFormBase
    {
        private IContainer components;
        private Label label1;
        public static bool loggingOut;

        public LoggingOutPopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
        }

        public static void clearLoggingOut()
        {
            loggingOut = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void doOpen(bool manual, bool autoScout, bool autoTrade, bool autoAttack, bool autoAttackWolf, bool autoAttackBandit, bool autoAttackAI, int resourceType, int percent, bool autoRecruit, bool autoRecruitPeasant, bool autoRecruitArchers, bool autoRecruitPikemen, bool autoRecruitSwordsmen, bool autoRecruitCatapults, int autoRecruitPeasant_Cap, int autoRecruitArchers_Cap, int autoRecruitPikemen_Cap, int autoRecruitSwordsmen_Cap, int autoRecruitCatapults_Cap)
        {
            this.label1.Text = SK.Text("LoggingOutPopup_Please_Wait", "Please wait....");
            this.Text = base.Title = SK.Text("LoggingOutPopup_Logging_Out", "Logging Out");
            RemoteServices.Instance.set_Chat_Logout_UserCallBack(null);
            RemoteServices.Instance.Chat_Logout();
            RemoteServices.Instance.set_LogOut_UserCallBack(new RemoteServices.LogOut_UserCallBack(this.LogOutCallback));
            RemoteServices.Instance.LogOut(manual, autoScout, autoTrade, autoAttack, autoAttackWolf, autoAttackBandit, autoAttackAI, resourceType, percent, autoRecruit, autoRecruitPeasant, autoRecruitArchers, autoRecruitPikemen, autoRecruitSwordsmen, autoRecruitCatapults, autoRecruitPeasant_Cap, autoRecruitArchers_Cap, autoRecruitPikemen_Cap, autoRecruitSwordsmen_Cap, autoRecruitCatapults_Cap);
            RemoteServices.Instance.SessionID = 0;
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            base.SuspendLayout();
            this.label1.BackColor = ARGBColors.Transparent;
            this.label1.Location = new Point(0x37, 0x25);
            this.label1.Name = "label1";
            this.label1.Size = new Size(250, 0x23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please wait....";
            this.label1.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x169, 80);
            base.Controls.Add(this.label1);
            base.Name = "LoggingOutPopup";
            this.Text = "Logging Out";
            base.TopMost = true;
            base.Controls.SetChildIndex(this.label1, 0);
            base.ResumeLayout(false);
        }

        public void LogOutCallback(LogOut_ReturnType returnData)
        {
            Thread.Sleep(0x1388);
            loggingOut = false;
            GameEngine.Instance.sessionExpired(-1);
            base.Close();
        }

        public static void open(bool manual, bool autoScout, bool autoTrade, bool autoAttack, bool autoAttackWolf, bool autoAttackBandit, bool autoAttackAI, int resourceType, int percent, bool autoRecruit, bool autoRecruitPeasant, bool autoRecruitArchers, bool autoRecruitPikemen, bool autoRecruitSwordsmen, bool autoRecruitCatapults, int autoRecruitPeasant_Cap, int autoRecruitArchers_Cap, int autoRecruitPikemen_Cap, int autoRecruitSwordsmen_Cap, int autoRecruitCatapults_Cap)
        {
            loggingOut = true;
            LoggingOutPopup popup = new LoggingOutPopup();
            popup.doOpen(manual, autoScout, autoTrade, autoAttack, autoAttackWolf, autoAttackBandit, autoAttackAI, resourceType, percent, autoRecruit, autoRecruitPeasant, autoRecruitArchers, autoRecruitPikemen, autoRecruitSwordsmen, autoRecruitCatapults, autoRecruitPeasant_Cap, autoRecruitArchers_Cap, autoRecruitPikemen_Cap, autoRecruitSwordsmen_Cap, autoRecruitCatapults_Cap);
            popup.Show();
        }
    }
}

