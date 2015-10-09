namespace Kingdoms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FloatingInputText : Form
    {
        private IContainer components;
        private bool inChange;
        private static FloatingInputText Instance;
        public string lastString = "";
        private TextBox textBox1;

        public FloatingInputText()
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

        private void FloatingInputText_Deactivate(object sender, EventArgs e)
        {
            InterfaceMgr.Instance.closeTextStringInput(this.lastString);
        }

        public void init()
        {
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
            this.textBox1.MaxLength = 140;
            this.textBox1.Size = new Size(500, 13);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyPress += new KeyPressEventHandler(this.textBox1_KeyPress);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(0x3d, 0x26, 0x16);
            base.ClientSize = new Size(500, 0x13);
            base.ControlBox = false;
            base.Controls.Add(this.textBox1);
            this.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.None;
            this.MaximumSize = new Size(500, 0x13);
            this.MinimumSize = new Size(500, 0x13);
            base.Name = "FloatingInputText";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "FloatingInputText";
            base.Deactivate += new EventHandler(this.FloatingInputText_Deactivate);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public static void open(int x, int y, string text, Form parent)
        {
            close();
            Instance = new FloatingInputText();
            Instance.Location = new Point(x, y);
            Instance.textBox1.Text = text;
            Instance.init();
            Instance.Show(parent);
        }

        public static void openDisband(int x, int y, string text, Form parent)
        {
            close();
            Instance = new FloatingInputText();
            Instance.Location = new Point(x, y);
            Instance.MinimumSize = new Size(500, 0x13);
            Instance.Size = new Size(500, 0x13);
            Instance.MaximumSize = new Size(500, 0x13);
            Instance.textBox1.Text = text;
            Instance.textBox1.BackColor = Color.FromArgb(0x4a, 0x56, 0x5c);
            Instance.textBox1.ForeColor = ARGBColors.White;
            Instance.BackColor = Color.FromArgb(0x4a, 0x56, 0x5c);
            Instance.init();
            Instance.Show(parent);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                InterfaceMgr.Instance.closeTextStringInput(this.lastString);
            }
            else if ((this.textBox1.Text.Length >= 90) && !char.IsControl(e.KeyChar))
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
                str2 = str2 + text;
                if (text != str2)
                {
                    this.textBox1.Text = str2;
                }
                this.lastString = this.textBox1.Text;
                this.inChange = false;
            }
        }
    }
}

