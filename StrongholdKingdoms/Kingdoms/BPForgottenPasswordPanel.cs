namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    public class BPForgottenPasswordPanel : CustomSelfDrawPanel
    {
        public BPForgottenPasswordPanel()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            base.clearControls();
            CustomSelfDrawPanel.CSDButton control = new CustomSelfDrawPanel.CSDButton {
                ImageNorm = (Image) GFXLibrary.misc_button_blue_210wide_normal,
                ImageOver = (Image) GFXLibrary.misc_button_blue_210wide_over,
                ImageClick = (Image) GFXLibrary.misc_button_blue_210wide_pushed,
                Position = new Point(300, 0)
            };
            control.Text.Text = SK.Text("LOGIN_ForgottenPassword", "Forgotten Password");
            control.Text.Font = FontManager.GetFont("Arial", 9f, FontStyle.Regular);
            control.Text.Color = ARGBColors.Black;
            control.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.forgottenClicked));
            base.addControl(control);
        }

        private void forgottenClicked()
        {
            try
            {
                new Process { StartInfo = { FileName = "http://login.strongholdkingdoms.com/bigpoint/changepass.php?lang=" + Program.mySettings.LanguageIdent } }.Start();
            }
            catch (Exception)
            {
            }
        }
    }
}

