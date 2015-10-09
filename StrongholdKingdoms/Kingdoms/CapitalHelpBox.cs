namespace Kingdoms
{
    using CommonTypes;
    using Kingdoms.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CapitalHelpBox : MyFormBase
    {
        private Button btnClose;
        private IContainer components;
        public static CapitalHelpBox helpBox;
        private Label lblBuildingType;
        private Label lblHelpText;

        public CapitalHelpBox()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
            this.lblBuildingType.Font = FontManager.GetFont("Microsoft Sans Serif", 9f, FontStyle.Bold);
            this.Text = base.Title = SK.Text("MENU_Help", "Help");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            GameEngine.Instance.playInterfaceSound("CapitalHelpBox_Close");
            closeHelpBox();
        }

        public static void closeHelpBox()
        {
            if (helpBox != null)
            {
                helpBox.Close();
                helpBox = null;
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

        public void init(int buildingType)
        {
            this.lblBuildingType.Text = VillageBuildingsData.getBuildingName(buildingType);
            this.lblHelpText.Text = VillageBuildingsData.getCapitalBuildingHelpText(buildingType);
        }

        private void InitializeComponent()
        {
            new ComponentResourceManager(typeof(CapitalHelpBox));
            this.btnClose = new Button();
            this.lblBuildingType = new Label();
            this.lblHelpText = new Label();
            base.SuspendLayout();
            this.btnClose.BackColor = Color.FromArgb(0xcb, 0xd7, 0xdf);
            this.btnClose.Location = new Point(0x112, 0xef);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(0x4b, 0x17);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.lblBuildingType.BackColor = ARGBColors.Transparent;
            this.lblBuildingType.Location = new Point(13, 0x2e);
            this.lblBuildingType.Name = "lblBuildingType";
            this.lblBuildingType.Size = new Size(0x14f, 0x16);
            this.lblBuildingType.TabIndex = 1;
            this.lblBuildingType.Text = "label1";
            this.lblHelpText.BackColor = ARGBColors.Transparent;
            this.lblHelpText.Location = new Point(13, 0x52);
            this.lblHelpText.Name = "lblHelpText";
            this.lblHelpText.Size = new Size(0x14f, 0x90);
            this.lblHelpText.TabIndex = 2;
            this.lblHelpText.Text = "label2";
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x169, 0x112);
            base.Controls.Add(this.lblHelpText);
            base.Controls.Add(this.lblBuildingType);
            base.Controls.Add(this.btnClose);
            base.Icon = Resources.shk_icon;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "CapitalHelpBox";
            base.ShowClose = false;
            base.ShowIcon = false;
            this.Text = "Help";
            base.Controls.SetChildIndex(this.btnClose, 0);
            base.Controls.SetChildIndex(this.lblBuildingType, 0);
            base.Controls.SetChildIndex(this.lblHelpText, 0);
            base.ResumeLayout(false);
        }

        public static void openHelpBox(int buildingType)
        {
            if ((helpBox == null) || !helpBox.Visible)
            {
                helpBox = new CapitalHelpBox();
            }
            helpBox.init(buildingType);
            helpBox.Show();
            helpBox.TopMost = true;
            helpBox.TopMost = false;
        }
    }
}

