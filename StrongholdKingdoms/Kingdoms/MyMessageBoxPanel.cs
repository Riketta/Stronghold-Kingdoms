namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class MyMessageBoxPanel : CustomSelfDrawPanel
    {
        public const int ABORTRETRYIGNORE = 5;
        private CustomSelfDrawPanel.CSDButton buttonCenter = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton buttonLeft = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDButton buttonRight = new CustomSelfDrawPanel.CSDButton();
        private CustomSelfDrawPanel.CSDArea controlToAddTo = new CustomSelfDrawPanel.CSDArea();
        private bool leaveGreyoutOpen;
        public const int NOBUTTONS = 7;
        private CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate noSpecialDelegate;
        public const int OK = 2;
        public const int OKCANCEL = 3;
        public const int OKSPECIAL = 6;
        private MyMessageBoxPopUp parent;
        public CustomSelfDrawPanel.CSDLabel popupText = new CustomSelfDrawPanel.CSDLabel();
        public const int RETRYCANCEL = 4;
        private int typeOfPopUp;
        public const int YESNO = 0;
        public const int YESNOCANCEL = 1;

        public MyMessageBoxPanel()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void ButtonCenterClicked()
        {
        }

        private void ButtonLeftClicked()
        {
        }

        private void ClosePanel()
        {
            if (this.noSpecialDelegate != null)
            {
                this.noSpecialDelegate();
            }
            if (!this.leaveGreyoutOpen)
            {
                InterfaceMgr.Instance.closeGreyOut();
            }
            this.parent.closing = true;
            this.parent.Close();
        }

        public void init(MyMessageBoxPopUp myFormBaseParent, string message, int type, CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate leftClickDelegate, CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate noClickDelegateSpecial)
        {
            base.clearControls();
            this.BackColor = ARGBColors.Transparent;
            this.typeOfPopUp = type;
            this.parent = myFormBaseParent;
            base.addControl(this.controlToAddTo);
            Graphics graphics = base.CreateGraphics();
            Size size = graphics.MeasureString(message, this.popupText.Font, 0x3e8).ToSize();
            graphics.Dispose();
            this.popupText.Size = new Size(size.Width + 10, size.Height);
            this.popupText.Text = message;
            this.popupText.Size = new Size(base.Width - 0x10, base.Height - 0x10);
            this.popupText.Alignment = CustomSelfDrawPanel.CSD_Text_Alignment.TOP_CENTER;
            this.popupText.Position = new Point((base.Size.Width / 2) - (this.popupText.Width / 2), 15);
            this.controlToAddTo.addControl(this.popupText);
            this.buttonLeft.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.buttonLeft.ImageOver = (Image) GFXLibrary.button_132_over;
            this.buttonLeft.ImageClick = (Image) GFXLibrary.button_132_in;
            this.buttonLeft.Position = new Point(10, (base.Size.Height - this.buttonLeft.Height) - 5);
            this.buttonLeft.setClickDelegate(leftClickDelegate);
            this.buttonCenter.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.buttonCenter.ImageOver = (Image) GFXLibrary.button_132_over;
            this.buttonCenter.ImageClick = (Image) GFXLibrary.button_132_in;
            this.buttonCenter.Position = new Point((base.Size.Width / 2) - (this.buttonCenter.Width / 2), (base.Size.Height - this.buttonCenter.Height) - 5);
            this.buttonRight.ImageNorm = (Image) GFXLibrary.button_132_normal;
            this.buttonRight.ImageOver = (Image) GFXLibrary.button_132_over;
            this.buttonRight.ImageClick = (Image) GFXLibrary.button_132_in;
            this.buttonRight.Position = new Point((base.Size.Width - this.buttonRight.Width) - 10, (base.Size.Height - this.buttonRight.Size.Height) - 5);
            this.noSpecialDelegate = noClickDelegateSpecial;
            this.buttonRight.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClosePanel));
            switch (type)
            {
                case 0:
                    this.buttonLeft.Text.Text = SK.Text("GENERIC_Yes", "Yes");
                    this.buttonRight.Text.Text = SK.Text("GENERIC_No", "No");
                    this.controlToAddTo.addControl(this.buttonLeft);
                    this.controlToAddTo.addControl(this.buttonRight);
                    return;

                case 1:
                case 4:
                case 5:
                    break;

                case 2:
                    this.buttonCenter.Text.Text = SK.Text("GENERIC_OK", "OK");
                    this.buttonCenter.setClickDelegate(new CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate(this.ClosePanel));
                    this.controlToAddTo.addControl(this.buttonCenter);
                    return;

                case 3:
                    this.buttonLeft.Text.Text = SK.Text("GENERIC_OK", "OK");
                    this.buttonRight.Text.Text = SK.Text("GENERIC_Cancel", "Cancel");
                    this.controlToAddTo.addControl(this.buttonLeft);
                    this.controlToAddTo.addControl(this.buttonRight);
                    return;

                case 6:
                    this.buttonCenter.Text.Text = SK.Text("GENERIC_OK", "OK");
                    this.buttonCenter.setClickDelegate(leftClickDelegate);
                    this.controlToAddTo.addControl(this.buttonCenter);
                    break;

                default:
                    return;
            }
        }

        public void init(MyMessageBoxPopUp myFormBaseParent, string message, int type, CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate leftClickDelegate, bool leaveGreaoutOpenOnClose)
        {
            this.leaveGreyoutOpen = leaveGreaoutOpenOnClose;
            this.init(myFormBaseParent, message, type, leftClickDelegate, (CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate) null);
        }

        public void init(MyMessageBoxPopUp myFormBaseParent, string message, int type, CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate leftClickDelegate, CustomSelfDrawPanel.CSDControl.CSD_ClickDelegate noButtonClickDelegate, bool leaveGreaoutOpenOnClose)
        {
            this.leaveGreyoutOpen = leaveGreaoutOpenOnClose;
            this.init(myFormBaseParent, message, type, leftClickDelegate, noButtonClickDelegate);
        }

        public void UpdatePopupBodyText(string newText)
        {
            this.popupText.Text = newText;
        }
    }
}

