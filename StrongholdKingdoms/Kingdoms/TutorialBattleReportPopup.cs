namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TutorialBattleReportPopup : MyFormBase
    {
        private BitmapButton btnViewReport;
        private IContainer components;
        private Label label3;
        private Label lblMessage;

        public TutorialBattleReportPopup()
        {
            this.InitializeComponent();
            this.lblMessage.Font = FontManager.GetFont("Arial", 9.75f, FontStyle.Regular);
            base.Title = this.Text = SK.Text("TutorialBattleReport_Victorious", "We are victorious Sire");
            this.btnViewReport.Text = SK.Text("TutorialBattleReport_View_Battle", "View Battle");
            this.btnViewReport.Font = FontManager.GetFont("Arial", 11f, FontStyle.Bold);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnViewReport.Enabled = false;
            RemoteServices.Instance.set_ViewBattle_UserCallBack(new RemoteServices.ViewBattle_UserCallBack(this.viewBattleCallback));
            RemoteServices.Instance.ViewBattle(-5555L);
        }

        private void closeCallbackClicked()
        {
            PostTutorialWindow.CreatePostTutorialWindow(true);
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
            this.lblMessage.Text = SK.Text("TutorialBattleReport_Message", "Your castle has been attacked!");
            base.closeCallback = new MyFormBase.MFBClose(this.closeCallbackClicked);
        }

        private void InitializeComponent()
        {
            this.btnViewReport = new BitmapButton();
            this.label3 = new Label();
            this.lblMessage = new Label();
            base.SuspendLayout();
            this.btnViewReport.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnViewReport.BorderColor = ARGBColors.DarkBlue;
            this.btnViewReport.BorderDrawing = true;
            this.btnViewReport.FocusRectangleEnabled = false;
            this.btnViewReport.Image = null;
            this.btnViewReport.ImageBorderColor = ARGBColors.Chocolate;
            this.btnViewReport.ImageBorderEnabled = true;
            this.btnViewReport.ImageDropShadow = true;
            this.btnViewReport.ImageFocused = null;
            this.btnViewReport.ImageInactive = null;
            this.btnViewReport.ImageMouseOver = null;
            this.btnViewReport.ImageNormal = null;
            this.btnViewReport.ImagePressed = null;
            this.btnViewReport.InnerBorderColor = ARGBColors.LightGray;
            this.btnViewReport.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnViewReport.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnViewReport.Location = new Point(0x73, 130);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.OffsetPressedContent = true;
            this.btnViewReport.Padding2 = 5;
            this.btnViewReport.Size = new Size(0xc9, 0x27);
            this.btnViewReport.StretchImage = false;
            this.btnViewReport.TabIndex = 2;
            this.btnViewReport.Text = "View Report";
            this.btnViewReport.TextDropShadow = false;
            this.btnViewReport.UseVisualStyleBackColor = false;
            this.btnViewReport.Click += new EventHandler(this.btnOK_Click);
            this.label3.AutoSize = true;
            this.label3.BackColor = ARGBColors.Transparent;
            this.label3.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label3.ForeColor = ARGBColors.White;
            this.label3.Location = new Point(0xb3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0, 0x10);
            this.label3.TabIndex = 9;
            this.lblMessage.BackColor = ARGBColors.Transparent;
            this.lblMessage.Location = new Point(12, 0x2c);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new Size(0x196, 0x4d);
            this.lblMessage.TabIndex = 13;
            this.lblMessage.Text = "label1";
            this.lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            base.ClientSize = new Size(430, 0xda);
            base.Controls.Add(this.lblMessage);
            base.Controls.Add(this.btnViewReport);
            base.Icon = Resources.shk_icon;
            base.Name = "TutorialBattleReportPopup";
            base.ShowClose = true;
            base.Controls.SetChildIndex(this.btnViewReport, 0);
            base.Controls.SetChildIndex(this.lblMessage, 0);
            base.ResumeLayout(false);
        }

        private void viewBattleCallback(ViewBattle_ReturnType returnData)
        {
            if (returnData.Success)
            {
                InterfaceMgr.Instance.reactiveMainWindow();
                InterfaceMgr.Instance.getMainTabBar().selectDummyTab(6);
                int campMode = 0;
                int villageID = -1;
                List<int> list = GameEngine.Instance.World.getUserVillageIDList();
                if (list.Count > 0)
                {
                    villageID = list[0];
                    Sound.playBattleMusic();
                    GameEngine.Instance.InitBattle(returnData.castleMapSnapshot, returnData.damageMapSnapshot, returnData.castleTroopsSnapshot, returnData.attackMapSnapshot, returnData.keepLevel, returnData.defenderResearchData, returnData.attackerResearchData, campMode, -1, -1, -1, 13, villageID, null, returnData.landType);
                    GameEngine.Instance.CastleBattle.tutorialFastForward();
                }
            }
            base.Close();
        }
    }
}

