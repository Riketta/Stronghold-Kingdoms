namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class MyMessageBox : Form
    {
        private BitmapButton btnCancel;
        private BitmapButton btnOK;
        private static MessageBoxButtons buttons = MessageBoxButtons.OK;
        private IContainer components;
        private static string customCancelSound = "";
        private static string customOKSound = "";
        private static MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1;
        private static Form forcedForm = null;
        public const int HT_CAPTION = 2;
        private Label lblMessage;
        private Label lblTimer;
        private Label lblTitle;
        public Timer msgTimer;
        private static MyMessageBox newMessageBox;
        private Panel panel1;
        private Panel panel2;
        private static DialogResult result = DialogResult.OK;
        public const int WM_NCLBUTTONDOWN = 0xa1;

        public MyMessageBox()
        {
            this.InitializeComponent();
            this.panel2.BackgroundImage = (Image) GFXLibrary.messageboxtop;
            this.lblTimer.Font = FontManager.GetFont("Arial", 9.75f, FontStyle.Bold);
            this.lblMessage.Font = FontManager.GetFont("Arial", 9.75f, FontStyle.Regular);
            this.lblTitle.Font = FontManager.GetFont("Arial", 9.75f, FontStyle.Bold);
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            Form parentForm = InterfaceMgr.Instance.ParentForm;
            if ((parentForm != null) && (parentForm.WindowState != FormWindowState.Minimized))
            {
                Point location = parentForm.Location;
                Size size = parentForm.Size;
                Size size2 = base.Size;
                Point point2 = new Point(((size.Width - size2.Width) / 2) + location.X, ((size.Height - size2.Height) / 2) + location.Y);
                base.Location = point2;
            }
            else
            {
                base.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (customCancelSound.Length == 0)
            {
                GameEngine.Instance.playInterfaceSound("MyMessageBox_cancel");
            }
            else
            {
                GameEngine.Instance.playInterfaceSound(customCancelSound);
            }
            if (buttons == MessageBoxButtons.OK)
            {
                result = DialogResult.OK;
            }
            if (buttons == MessageBoxButtons.OKCancel)
            {
                result = DialogResult.Cancel;
            }
            if (buttons == MessageBoxButtons.YesNo)
            {
                result = DialogResult.No;
            }
            newMessageBox.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (customOKSound.Length == 0)
            {
                GameEngine.Instance.playInterfaceSound("MyMessageBox_ok");
            }
            else
            {
                GameEngine.Instance.playInterfaceSound(customOKSound);
            }
            if (buttons == MessageBoxButtons.OKCancel)
            {
                result = DialogResult.OK;
            }
            if (buttons == MessageBoxButtons.YesNo)
            {
                result = DialogResult.Yes;
            }
            newMessageBox.Dispose();
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
            new ComponentResourceManager(typeof(MyMessageBox));
            this.btnCancel = new BitmapButton();
            this.lblTimer = new Label();
            this.btnOK = new BitmapButton();
            this.panel1 = new Panel();
            this.lblMessage = new Label();
            this.lblTitle = new Label();
            this.panel2 = new Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCancel.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnCancel.Location = new Point(0xb8, 0x70);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4a, 0x19);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.lblTimer.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.lblTimer.AutoSize = true;
            this.lblTimer.BackColor = ARGBColors.Transparent;
            this.lblTimer.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblTimer.Location = new Point(9, 120);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new Size(0, 0x10);
            this.lblTimer.TabIndex = 4;
            this.btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnOK.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnOK.Location = new Point(0x68, 0x70);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4a, 0x19);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = ARGBColors.Transparent;
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Location = new Point(13, 0x29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x14f, 0x3a);
            this.panel1.TabIndex = 6;
            this.lblMessage.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblMessage.Location = new Point(0, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new Size(0x14f, 0x3a);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Testing text";
            this.lblMessage.TextAlign = ContentAlignment.TopCenter;
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = ARGBColors.Transparent;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblTitle.ForeColor = ARGBColors.White;
            this.lblTitle.Location = new Point(10, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(0, 0x10);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.MouseDown += new MouseEventHandler(this.lblTitle_MouseDown);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Location = new Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x167, 30);
            this.panel2.TabIndex = 9;
            this.panel2.MouseDown += new MouseEventHandler(this.panel2_MouseDown);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(0x9f, 180, 0xc1);
            base.ClientSize = new Size(0x169, 0x90);
            base.ControlBox = false;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.lblTimer);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = Resources.shk_icon;
            base.Name = "MyMessageBox";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "MyMessageBox";
            base.Load += new EventHandler(this.MyMessageBox_Load);
            base.Paint += new PaintEventHandler(this.MyMessageBox_Paint);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(base.Handle, 0xa1, 2, 0);
            }
        }

        private void MyMessageBox_Load(object sender, EventArgs e)
        {
            if (buttons == MessageBoxButtons.OK)
            {
                this.btnCancel.Location = new Point(0x8f, this.btnCancel.Location.Y);
                this.btnCancel.Text = SK.Text("GENERIC_OK", "OK");
                this.btnCancel.Visible = true;
                this.btnOK.Visible = false;
                this.btnCancel.TabIndex = 1;
                this.btnOK.TabIndex = 2;
                this.btnCancel.Focus();
            }
            if (buttons == MessageBoxButtons.YesNo)
            {
                this.btnCancel.Location = new Point(0xb8, this.btnCancel.Location.Y);
                this.btnCancel.Text = SK.Text("GENERIC_No", "No");
                this.btnCancel.Visible = true;
                this.btnOK.Text = SK.Text("GENERIC_Yes", "Yes");
                this.btnOK.Visible = true;
                if (defaultButton == MessageBoxDefaultButton.Button1)
                {
                    this.btnOK.TabIndex = 1;
                    this.btnCancel.TabIndex = 2;
                    this.btnOK.Focus();
                }
                else
                {
                    this.btnOK.TabIndex = 2;
                    this.btnCancel.TabIndex = 1;
                    this.btnCancel.Focus();
                }
            }
            if (buttons == MessageBoxButtons.OKCancel)
            {
                this.btnCancel.Location = new Point(0xb8, this.btnCancel.Location.Y);
                this.btnCancel.Text = SK.Text("GENERIC_Cancel", "Cancel");
                this.btnCancel.Visible = true;
                this.btnOK.Text = SK.Text("GENERIC_OK", "OK");
                this.btnOK.Visible = true;
                if (defaultButton == MessageBoxDefaultButton.Button1)
                {
                    this.btnOK.TabIndex = 1;
                    this.btnCancel.TabIndex = 2;
                    this.btnOK.Focus();
                }
                else
                {
                    this.btnOK.TabIndex = 2;
                    this.btnCancel.TabIndex = 1;
                    this.btnCancel.Focus();
                }
            }
        }

        private void MyMessageBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.FromArgb(0x56, 0x62, 0x6a), 1f);
            Rectangle rect = new Rectangle(1, 1, (base.Width - 1) - 2, (base.Height - 1) - 2);
            LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(0x56, 0x62, 0x6a), Color.FromArgb(0x9f, 180, 0xc1), LinearGradientMode.Vertical);
            graphics.FillRectangle(brush, rect);
            graphics.DrawRectangle(pen, rect);
            brush.Dispose();
            pen.Dispose();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(base.Handle, 0xa1, 2, 0);
            }
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public static void resetCustomSounds()
        {
            customCancelSound = "";
            customOKSound = "";
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        public static void setCustomSounds(string newOK, string newCancel)
        {
            customOKSound = newOK;
            customCancelSound = newCancel;
        }

        public static void setForcedForm(Form form)
        {
            forcedForm = form;
        }

        public static DialogResult Show(string txtMessage)
        {
            newMessageBox = new MyMessageBox();
            buttons = MessageBoxButtons.OK;
            defaultButton = MessageBoxDefaultButton.Button1;
            newMessageBox.lblMessage.Text = txtMessage;
            Graphics graphics = newMessageBox.lblMessage.CreateGraphics();
            Size size = graphics.MeasureString(txtMessage, newMessageBox.lblMessage.Font, 0x14f).ToSize();
            graphics.Dispose();
            int height = size.Height;
            if (height < 50)
            {
                height = 50;
            }
            newMessageBox.lblMessage.Size = new Size(0x14f, height);
            newMessageBox.panel1.Size = newMessageBox.lblMessage.Size;
            newMessageBox.Size = new Size(newMessageBox.Size.Width, (height + 0x8e) - 0x3a);
            bool flag = false;
            Form activeForm = Form.ActiveForm;
            if (forcedForm != null)
            {
                activeForm = forcedForm;
                flag = true;
                newMessageBox.StartPosition = FormStartPosition.CenterParent;
            }
            else if (((activeForm != null) && (activeForm.ProductName == newMessageBox.ProductName)) && (activeForm.WindowState == FormWindowState.Normal))
            {
                flag = true;
            }
            if (flag)
            {
                newMessageBox.ShowDialog(activeForm);
            }
            else
            {
                newMessageBox.ShowDialog();
            }
            newMessageBox.Dispose();
            forcedForm = null;
            if ((activeForm != null) && flag)
            {
                bool topMost = activeForm.TopMost;
                activeForm.TopMost = false;
                activeForm.TopMost = true;
                activeForm.Focus();
                activeForm.BringToFront();
                activeForm.Focus();
                activeForm.TopMost = topMost;
            }
            return result;
        }

        public static DialogResult Show(string txtMessage, string txtTitle)
        {
            newMessageBox = new MyMessageBox();
            buttons = MessageBoxButtons.OK;
            defaultButton = MessageBoxDefaultButton.Button1;
            newMessageBox.lblTitle.Text = txtTitle;
            newMessageBox.lblMessage.Text = txtMessage;
            Graphics graphics = newMessageBox.lblMessage.CreateGraphics();
            Size size = graphics.MeasureString(txtMessage, newMessageBox.lblMessage.Font, 0x14f).ToSize();
            graphics.Dispose();
            int num = size.Height + 3;
            if (num < 50)
            {
                num = 50;
            }
            newMessageBox.lblMessage.Size = new Size(0x14f, num + 20);
            newMessageBox.panel1.Size = newMessageBox.lblMessage.Size;
            newMessageBox.Size = new Size(newMessageBox.Size.Width, (num + 0x8e) - 0x3a);
            bool flag = false;
            Form activeForm = Form.ActiveForm;
            if (forcedForm != null)
            {
                activeForm = forcedForm;
                flag = true;
                newMessageBox.StartPosition = FormStartPosition.CenterParent;
            }
            else if (((activeForm != null) && (activeForm.ProductName == newMessageBox.ProductName)) && (activeForm.WindowState == FormWindowState.Normal))
            {
                flag = true;
            }
            if (flag)
            {
                newMessageBox.StartPosition = FormStartPosition.CenterParent;
                newMessageBox.ShowDialog(activeForm);
            }
            else
            {
                newMessageBox.ShowDialog();
            }
            newMessageBox.Dispose();
            forcedForm = null;
            if ((activeForm != null) && flag)
            {
                bool topMost = activeForm.TopMost;
                activeForm.TopMost = false;
                activeForm.TopMost = true;
                activeForm.Focus();
                activeForm.BringToFront();
                activeForm.Focus();
                activeForm.TopMost = topMost;
            }
            return result;
        }

        public static DialogResult Show(string txtMessage, string txtTitle, MessageBoxButtons buts)
        {
            newMessageBox = new MyMessageBox();
            buttons = buts;
            defaultButton = MessageBoxDefaultButton.Button1;
            newMessageBox.lblTitle.Text = txtTitle;
            newMessageBox.lblMessage.Text = txtMessage;
            Graphics graphics = newMessageBox.lblMessage.CreateGraphics();
            Size size = graphics.MeasureString(txtMessage, newMessageBox.lblMessage.Font, 0x14f).ToSize();
            graphics.Dispose();
            int height = size.Height;
            if (height < 50)
            {
                height = 50;
            }
            newMessageBox.lblMessage.Size = new Size(0x14f, height + 20);
            newMessageBox.panel1.Size = newMessageBox.lblMessage.Size;
            newMessageBox.Size = new Size(newMessageBox.Size.Width, (height + 0x8e) - 0x3a);
            bool flag = false;
            Form activeForm = Form.ActiveForm;
            if (forcedForm != null)
            {
                activeForm = forcedForm;
                flag = true;
                newMessageBox.StartPosition = FormStartPosition.CenterParent;
            }
            else if (((activeForm != null) && (activeForm.ProductName == newMessageBox.ProductName)) && (activeForm.WindowState == FormWindowState.Normal))
            {
                flag = true;
            }
            if (flag)
            {
                newMessageBox.ShowDialog(activeForm);
            }
            else
            {
                newMessageBox.ShowDialog();
            }
            newMessageBox.Dispose();
            forcedForm = null;
            if ((activeForm != null) && flag)
            {
                bool topMost = activeForm.TopMost;
                activeForm.TopMost = false;
                activeForm.TopMost = true;
                activeForm.Focus();
                activeForm.BringToFront();
                activeForm.Focus();
                activeForm.TopMost = topMost;
            }
            return result;
        }

        public static DialogResult Show(string txtMessage, string txtTitle, MessageBoxButtons buts, MessageBoxIcon x1, MessageBoxDefaultButton defaultBut, int x2)
        {
            newMessageBox = new MyMessageBox();
            buttons = buts;
            defaultButton = defaultBut;
            newMessageBox.lblTitle.Text = txtTitle;
            newMessageBox.lblMessage.Text = txtMessage;
            Graphics graphics = newMessageBox.lblMessage.CreateGraphics();
            Size size = graphics.MeasureString(txtMessage, newMessageBox.lblMessage.Font, 0x14f).ToSize();
            graphics.Dispose();
            int height = size.Height;
            if (height < 50)
            {
                height = 50;
            }
            newMessageBox.lblMessage.Size = new Size(0x14f, height + 20);
            newMessageBox.panel1.Size = newMessageBox.lblMessage.Size;
            newMessageBox.Size = new Size(newMessageBox.Size.Width, (height + 0x8e) - 0x3a);
            bool flag = false;
            Form activeForm = Form.ActiveForm;
            if (((activeForm != null) && (activeForm.ProductName == newMessageBox.ProductName)) && (activeForm.WindowState == FormWindowState.Normal))
            {
                flag = true;
            }
            if (flag)
            {
                newMessageBox.ShowDialog(activeForm);
            }
            else
            {
                newMessageBox.ShowDialog();
            }
            newMessageBox.Dispose();
            if ((activeForm != null) && flag)
            {
                bool topMost = activeForm.TopMost;
                activeForm.TopMost = false;
                activeForm.TopMost = true;
                activeForm.Focus();
                activeForm.BringToFront();
                activeForm.Focus();
                activeForm.TopMost = topMost;
            }
            return result;
        }
    }
}

