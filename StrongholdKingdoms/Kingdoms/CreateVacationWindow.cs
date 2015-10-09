namespace Kingdoms
{
    using CommonTypes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CreateVacationWindow : MyFormBase
    {
        private BitmapButton btnCancel;
        private BitmapButton btnStartVacation;
        private IContainer components;
        private Label label1;
        private Label label2;
        private Label lblDays;
        private Label lblDuration;
        private Label lblDurationValue;
        private Label lblExplanation;
        private Label lblNumAvailable;
        private Label lblNumberVacationLabel;
        private static CreateVacationWindow popup;
        private TrackBar trackNumDays;

        public CreateVacationWindow()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblExplanation.Font = FontManager.GetFont("Microsoft Sans Serif", 9f, FontStyle.Bold);
            this.Text = base.Title = SK.Text("VM_Heading", "Start Vacation");
            this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
            this.btnStartVacation.Text = SK.Text("VM_Heading", "Start Vacation");
            this.lblNumAvailable.Text = GameEngine.Instance.World.NumVacationsAvailable.ToString();
            this.lblNumberVacationLabel.Text = SK.Text("VM_Num_Available", "Number of Vacations Available");
            this.lblDuration.Text = SK.Text("VM_Duration", "Duration");
            this.lblDurationValue.Text = this.trackNumDays.Value.ToString();
            this.lblDays.Text = SK.Text("Vacation_Days", "Days");
            this.lblExplanation.Text = SK.Text("Vacation_Explanation", "Going away on holiday? Set vacation mode to protect your villages from attack for up to 15 days.");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnStartVacation_Click(object sender, EventArgs e)
        {
            DialogResult result;
            int numDays = this.trackNumDays.Value;
            MyMessageBox.setForcedForm(this);
            if (!ProfileLoginWindow.inSpecialWorld)
            {
                result = MyMessageBox.Show(SK.Text("VM_start_vacation_warning1", "You are about to enter Vacation Mode.") + Environment.NewLine + Environment.NewLine + SK.Text("VM_start_vacation_warning2", "During this time all your villages will be protected from new attacks across all worlds, but you will be unable to cancel this for 3 days and you will have no access to your account during this period.") + Environment.NewLine + Environment.NewLine + SK.Text("VM_start_vacation_warning3", "Are you sure you wish to start Vacation Mode? ") + Environment.NewLine + ".", SK.Text("VM_Heading", "Start Vacation"), MessageBoxButtons.YesNo);
            }
            else
            {
                result = MyMessageBox.Show(SK.Text("VM_start_vacation_warning1", "You are about to enter Vacation Mode.") + Environment.NewLine + Environment.NewLine + SK.Text("VM_start_vacation_warning2", "During this time all your villages will be protected from new attacks across all worlds, but you will be unable to cancel this for 3 days and you will have no access to your account during this period.") + Environment.NewLine + Environment.NewLine + Environment.NewLine + SK.Text("VM_start_vacation_warning10", "IMPORTANT: Special World Warning.") + Environment.NewLine + Environment.NewLine + SK.Text("VM_start_vacation_warning11", "You are currently playing in a special world") + " : " + ProfileLoginWindow.specialWorldName + Environment.NewLine + Environment.NewLine + SK.Text("VM_start_vacation_warning12", "SPECIAL WORLDS CANNOT BE PROTECTED BY VACATION MODE. If you continue with applying Vacation Mode to your Stronghold Kingdoms account, your villages within the special world will not be protected by Vacation Mode leaving them vulnerable and you will not be able to login to the special world.") + Environment.NewLine + Environment.NewLine + SK.Text("VM_start_vacation_warning3", "Are you sure you wish to start Vacation Mode? ") + Environment.NewLine + ".", SK.Text("VM_Heading", "Start Vacation"), MessageBoxButtons.YesNo);
            }
            if (result == DialogResult.Yes)
            {
                RemoteServices.Instance.set_SetVacationMode_UserCallBack(new RemoteServices.SetVacationMode_UserCallBack(this.SetVacationMode_callback));
                RemoteServices.Instance.SetVacationMode(numDays);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void init()
        {
        }

        private void InitializeComponent()
        {
            this.trackNumDays = new TrackBar();
            this.label1 = new Label();
            this.label2 = new Label();
            this.lblDuration = new Label();
            this.lblNumberVacationLabel = new Label();
            this.lblNumAvailable = new Label();
            this.btnStartVacation = new BitmapButton();
            this.btnCancel = new BitmapButton();
            this.lblDurationValue = new Label();
            this.lblDays = new Label();
            this.lblExplanation = new Label();
            this.trackNumDays.BeginInit();
            base.SuspendLayout();
            this.trackNumDays.AutoSize = false;
            this.trackNumDays.BackColor = Color.FromArgb(0x89, 0x9b, 0xa7);
            this.trackNumDays.LargeChange = 1;
            this.trackNumDays.Location = new Point(0x66, 0xbb);
            this.trackNumDays.Maximum = 15;
            this.trackNumDays.Minimum = 3;
            this.trackNumDays.Name = "trackNumDays";
            this.trackNumDays.Size = new Size(0xdf, 0x2d);
            this.trackNumDays.TabIndex = 13;
            this.trackNumDays.Value = 3;
            this.trackNumDays.ValueChanged += new EventHandler(this.trackNumDays_ValueChanged);
            this.label1.AutoSize = true;
            this.label1.BackColor = ARGBColors.Transparent;
            this.label1.ForeColor = ARGBColors.Black;
            this.label1.Location = new Point(0x6d, 0xeb);
            this.label1.Name = "label1";
            this.label1.Size = new Size(13, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "3";
            this.label2.AutoSize = true;
            this.label2.BackColor = ARGBColors.Transparent;
            this.label2.ForeColor = ARGBColors.Black;
            this.label2.Location = new Point(0x12f, 0xed);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x13, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "15";
            this.lblDuration.BackColor = ARGBColors.Transparent;
            this.lblDuration.ForeColor = ARGBColors.Black;
            this.lblDuration.Location = new Point(0x66, 0xa2);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new Size(0xdf, 13);
            this.lblDuration.TabIndex = 0x10;
            this.lblDuration.Text = "Vacation Duration";
            this.lblDuration.TextAlign = ContentAlignment.MiddleCenter;
            this.lblNumberVacationLabel.BackColor = ARGBColors.Transparent;
            this.lblNumberVacationLabel.ForeColor = ARGBColors.Black;
            this.lblNumberVacationLabel.Location = new Point(1, 0x61);
            this.lblNumberVacationLabel.Name = "lblNumberVacationLabel";
            this.lblNumberVacationLabel.Size = new Size(0x1a6, 0x15);
            this.lblNumberVacationLabel.TabIndex = 0x11;
            this.lblNumberVacationLabel.Text = "Number of Vacations Available";
            this.lblNumberVacationLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.lblNumAvailable.BackColor = ARGBColors.Transparent;
            this.lblNumAvailable.ForeColor = ARGBColors.Black;
            this.lblNumAvailable.Location = new Point(1, 0x79);
            this.lblNumAvailable.Name = "lblNumAvailable";
            this.lblNumAvailable.Size = new Size(0x1a6, 0x15);
            this.lblNumAvailable.TabIndex = 0x12;
            this.lblNumAvailable.Text = "0";
            this.lblNumAvailable.TextAlign = ContentAlignment.MiddleCenter;
            this.btnStartVacation.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnStartVacation.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnStartVacation.BorderColor = ARGBColors.DarkBlue;
            this.btnStartVacation.BorderDrawing = true;
            this.btnStartVacation.FocusRectangleEnabled = false;
            this.btnStartVacation.Image = null;
            this.btnStartVacation.ImageBorderColor = ARGBColors.Chocolate;
            this.btnStartVacation.ImageBorderEnabled = true;
            this.btnStartVacation.ImageDropShadow = true;
            this.btnStartVacation.ImageFocused = null;
            this.btnStartVacation.ImageInactive = null;
            this.btnStartVacation.ImageMouseOver = null;
            this.btnStartVacation.ImageNormal = null;
            this.btnStartVacation.ImagePressed = null;
            this.btnStartVacation.InnerBorderColor = ARGBColors.LightGray;
            this.btnStartVacation.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnStartVacation.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnStartVacation.Location = new Point(0x11b, 0x108);
            this.btnStartVacation.Name = "btnStartVacation";
            this.btnStartVacation.OffsetPressedContent = true;
            this.btnStartVacation.Padding2 = 5;
            this.btnStartVacation.Size = new Size(0x81, 0x1a);
            this.btnStartVacation.StretchImage = false;
            this.btnStartVacation.TabIndex = 20;
            this.btnStartVacation.Text = "Start Vacation";
            this.btnStartVacation.TextDropShadow = false;
            this.btnStartVacation.UseVisualStyleBackColor = false;
            this.btnStartVacation.Click += new EventHandler(this.btnStartVacation_Click);
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnCancel.BorderColor = ARGBColors.DarkBlue;
            this.btnCancel.BorderDrawing = true;
            this.btnCancel.FocusRectangleEnabled = false;
            this.btnCancel.Image = null;
            this.btnCancel.ImageBorderColor = ARGBColors.Chocolate;
            this.btnCancel.ImageBorderEnabled = true;
            this.btnCancel.ImageDropShadow = true;
            this.btnCancel.ImageFocused = null;
            this.btnCancel.ImageInactive = null;
            this.btnCancel.ImageMouseOver = null;
            this.btnCancel.ImageNormal = null;
            this.btnCancel.ImagePressed = null;
            this.btnCancel.InnerBorderColor = ARGBColors.LightGray;
            this.btnCancel.InnerBorderColor_Focus = ARGBColors.LightBlue;
            this.btnCancel.InnerBorderColor_MouseOver = ARGBColors.Gold;
            this.btnCancel.Location = new Point(14, 0x108);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OffsetPressedContent = true;
            this.btnCancel.Padding2 = 5;
            this.btnCancel.Size = new Size(0x4f, 0x1a);
            this.btnCancel.StretchImage = false;
            this.btnCancel.TabIndex = 0x13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextDropShadow = false;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.lblDurationValue.AutoSize = true;
            this.lblDurationValue.BackColor = ARGBColors.Transparent;
            this.lblDurationValue.ForeColor = ARGBColors.Black;
            this.lblDurationValue.Location = new Point(0x155, 0xc3);
            this.lblDurationValue.Name = "lblDurationValue";
            this.lblDurationValue.Size = new Size(0x13, 13);
            this.lblDurationValue.TabIndex = 0x15;
            this.lblDurationValue.Text = "15";
            this.lblDays.BackColor = ARGBColors.Transparent;
            this.lblDays.ForeColor = ARGBColors.Black;
            this.lblDays.Location = new Point(0x80, 0xed);
            this.lblDays.Name = "lblDays";
            this.lblDays.Size = new Size(0xad, 13);
            this.lblDays.TabIndex = 0x16;
            this.lblDays.Text = "Days";
            this.lblDays.TextAlign = ContentAlignment.MiddleCenter;
            this.lblExplanation.BackColor = ARGBColors.Transparent;
            this.lblExplanation.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold);
            this.lblExplanation.ForeColor = ARGBColors.Black;
            this.lblExplanation.Location = new Point(0x23, 0x22);
            this.lblExplanation.Name = "lblExplanation";
            this.lblExplanation.Size = new Size(0x166, 0x3f);
            this.lblExplanation.TabIndex = 0x17;
            this.lblExplanation.Text = "Explanation";
            this.lblExplanation.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(0x80, 0x91, 0x9c);
            base.ClientSize = new Size(0x1a8, 0x12e);
            base.Controls.Add(this.lblExplanation);
            base.Controls.Add(this.lblDays);
            base.Controls.Add(this.lblDurationValue);
            base.Controls.Add(this.btnStartVacation);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.lblNumAvailable);
            base.Controls.Add(this.lblNumberVacationLabel);
            base.Controls.Add(this.lblDuration);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.trackNumDays);
            base.Controls.Add(this.label1);
            base.Name = "CreateVacationWindow";
            base.ShowClose = true;
            this.Text = "CreateVacationWindow";
            base.Controls.SetChildIndex(this.label1, 0);
            base.Controls.SetChildIndex(this.trackNumDays, 0);
            base.Controls.SetChildIndex(this.label2, 0);
            base.Controls.SetChildIndex(this.lblDuration, 0);
            base.Controls.SetChildIndex(this.lblNumberVacationLabel, 0);
            base.Controls.SetChildIndex(this.lblNumAvailable, 0);
            base.Controls.SetChildIndex(this.btnCancel, 0);
            base.Controls.SetChildIndex(this.btnStartVacation, 0);
            base.Controls.SetChildIndex(this.lblDurationValue, 0);
            base.Controls.SetChildIndex(this.lblDays, 0);
            base.Controls.SetChildIndex(this.lblExplanation, 0);
            this.trackNumDays.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void SetVacationMode_callback(SetVacationMode_ReturnType returnData)
        {
            if (returnData.Success)
            {
                base.Close();
                InterfaceMgr.Instance.openLogoutWindow(false);
            }
            else
            {
                MyMessageBox.Show(ErrorCodes.getErrorString(returnData.m_errorCode, returnData.m_errorID), SK.Text("VM_Error", "Vacation Error"));
            }
        }

        public static void showVacationMode()
        {
            if (GameEngine.Instance.World.NumVacationsAvailable > 0)
            {
                if (GameEngine.Instance.World.isAccount730Premium())
                {
                    if ((popup == null) || !popup.Created)
                    {
                        popup = new CreateVacationWindow();
                    }
                    popup.init();
                    Form parentForm = InterfaceMgr.Instance.ParentForm;
                    popup.Location = new Point((parentForm.Location.X + (parentForm.Width / 2)) - (popup.Width / 2), (parentForm.Location.Y + (parentForm.Height / 2)) - (popup.Height / 2));
                    popup.Show(InterfaceMgr.Instance.ParentForm);
                }
                else
                {
                    MyMessageBox.setForcedForm(InterfaceMgr.Instance.ParentForm);
                    MyMessageBox.Show(SK.Text("VM_Not_Premium", "Vacation Mode requires you to have a 7 day or 30 day Premium Token active."), SK.Text("VM_Error", "Vacation Error"));
                }
            }
            else if (GameEngine.Instance.World.VacationNot30Days)
            {
                MyMessageBox.setForcedForm(InterfaceMgr.Instance.ParentForm);
                MyMessageBox.Show(SK.Text("VM_None_Available_30Days", "Vacation Mode is not available to you at this time. Your account must be at least 30 days old to be able to access Vacation Mode."), SK.Text("VM_Error", "Vacation Error"));
            }
            else
            {
                MyMessageBox.setForcedForm(InterfaceMgr.Instance.ParentForm);
                MyMessageBox.Show(SK.Text("VM_None_Available", "You have no Vacations Available at the current time."), SK.Text("VM_Error", "Vacation Error"));
            }
        }

        private void trackNumDays_ValueChanged(object sender, EventArgs e)
        {
            this.lblDurationValue.Text = this.trackNumDays.Value.ToString();
        }
    }
}

