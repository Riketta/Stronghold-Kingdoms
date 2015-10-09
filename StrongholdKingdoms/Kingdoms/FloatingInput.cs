namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FloatingInput : Form
    {
        private IContainer components;
        private bool inChange;
        private static FloatingInput Instance;
        public string lastString = "";
        private int maxValue = 1;
        private TextBox textBox1;

        public FloatingInput()
        {
            this.InitializeComponent();
        }

        public static void close()
        {
            if (Instance != null)
            {
                Instance.Close();
                Instance = null;
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

        private void FloatingInput_Deactivate(object sender, EventArgs e)
        {
            InterfaceMgr.Instance.closeTextInput(this.getInt32FromString(this.lastString));
        }

        public int getInt32FromString(string text)
        {
            if (text.Length != 0)
            {
                try
                {
                    return Convert.ToInt32(text);
                }
                catch (Exception)
                {
                }
            }
            return 0;
        }

        public void init(int startingValue, int maxV)
        {
            if (startingValue > maxV)
            {
                startingValue = maxV;
            }
            if (startingValue < 0)
            {
                startingValue = 0;
            }
            this.maxValue = maxV;
            this.textBox1.Text = startingValue.ToString();
            this.lastString = this.textBox1.Text;
        }

        private void InitializeComponent()
        {
            this.textBox1 = new TextBox();
            base.SuspendLayout();
            this.textBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.textBox1.BackColor = Color.FromArgb(0x3d, 0x26, 0x16);
            this.textBox1.BorderStyle = BorderStyle.None;
            this.textBox1.ForeColor = Color.FromArgb(0xc4, 0xa1, 0x55);
            this.textBox1.Location = new Point(0, 3);
            this.textBox1.MaxLength = 10;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x69, 13);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyPress += new KeyPressEventHandler(this.textBox1_KeyPress);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(0x3d, 0x26, 0x16);
            base.ClientSize = new Size(0x69, 0x13);
            base.ControlBox = false;
            base.Controls.Add(this.textBox1);
            this.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.None;
            this.MaximumSize = new Size(0x69, 0x13);
            this.MinimumSize = new Size(0x69, 0x13);
            base.Name = "FloatingInput";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "FloatingInput";
            base.Deactivate += new EventHandler(this.FloatingInput_Deactivate);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public static void open(int x, int y, int startingValue, int maxV, Form parent)
        {
            close();
            Instance = new FloatingInput();
            Instance.Location = new Point(x, y);
            Instance.init(startingValue, maxV);
            Instance.Show(parent);
        }

        public static void openDisband(int x, int y, int startingValue, int maxV, Form parent)
        {
            close();
            Instance = new FloatingInput();
            Instance.Location = new Point(x, y);
            Instance.MinimumSize = new Size(60, 0x13);
            Instance.Size = new Size(60, 0x13);
            Instance.MaximumSize = new Size(60, 0x13);
            Instance.textBox1.BackColor = Color.FromArgb(0x4a, 0x56, 0x5c);
            Instance.textBox1.ForeColor = ARGBColors.White;
            Instance.BackColor = Color.FromArgb(0x4a, 0x56, 0x5c);
            Instance.init(startingValue, maxV);
            Instance.Show(parent);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                InterfaceMgr.Instance.closeTextInput(this.getInt32FromString(this.lastString));
            }
            else if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!this.inChange)
            {
                this.inChange = true;
                string text = this.textBox1.Text;
                string str2 = "";
                foreach (char ch in text)
                {
                    if (char.IsDigit(ch))
                    {
                        str2 = str2 + ch;
                    }
                }
                if (text != str2)
                {
                    this.textBox1.Text = str2;
                }
                int num = this.getInt32FromString(this.textBox1.Text);
                if ((num < 0) || (num > this.maxValue))
                {
                    int selectionStart = this.textBox1.SelectionStart;
                    this.textBox1.Text = this.lastString;
                    this.textBox1.SelectionStart = selectionStart;
                }
                else
                {
                    this.lastString = this.textBox1.Text;
                }
                this.inChange = false;
            }
        }
    }
}

