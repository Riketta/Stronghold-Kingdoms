namespace Kingdoms
{
    using CompressedSink;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    public class DebugPopup : Form
    {
        private IContainer components;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label7;
        private Label label9;
        public int lastNumLines;
        private Label lblAverageLongRTT;
        private Label lblAverageRTT;
        private Label lblAverageShortRTT;
        private Label lblLongCount;
        private Label lblNetworkDataReceived;
        private Label lblNetworkDataSent;
        private Label lblShortCount;
        private Label lblTimeouts;
        private TextBox tbDetailedLogging;

        public DebugPopup()
        {
            this.InitializeComponent();
            this.Font = FontManager.GetFont("Microsoft Sans Serif", 8.25f);
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
            this.label1 = new Label();
            this.label2 = new Label();
            this.lblNetworkDataSent = new Label();
            this.lblNetworkDataReceived = new Label();
            this.label3 = new Label();
            this.lblAverageRTT = new Label();
            this.lblAverageLongRTT = new Label();
            this.label5 = new Label();
            this.lblTimeouts = new Label();
            this.label7 = new Label();
            this.lblAverageShortRTT = new Label();
            this.label9 = new Label();
            this.lblShortCount = new Label();
            this.lblLongCount = new Label();
            this.tbDetailedLogging = new TextBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0x1a);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Network Data Sent";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 0x33);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x7a, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Network Data Received";
            this.lblNetworkDataSent.AutoSize = true;
            this.lblNetworkDataSent.Location = new Point(0x8b, 0x1a);
            this.lblNetworkDataSent.Name = "lblNetworkDataSent";
            this.lblNetworkDataSent.Size = new Size(13, 13);
            this.lblNetworkDataSent.TabIndex = 2;
            this.lblNetworkDataSent.Text = "0";
            this.lblNetworkDataReceived.AutoSize = true;
            this.lblNetworkDataReceived.Location = new Point(0x8b, 0x33);
            this.lblNetworkDataReceived.Name = "lblNetworkDataReceived";
            this.lblNetworkDataReceived.Size = new Size(13, 13);
            this.lblNetworkDataReceived.TabIndex = 3;
            this.lblNetworkDataReceived.Text = "0";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 0x4c);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x48, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Average RTT";
            this.lblAverageRTT.AutoSize = true;
            this.lblAverageRTT.Location = new Point(0x8b, 0x4c);
            this.lblAverageRTT.Name = "lblAverageRTT";
            this.lblAverageRTT.Size = new Size(13, 13);
            this.lblAverageRTT.TabIndex = 5;
            this.lblAverageRTT.Text = "0";
            this.lblAverageLongRTT.AutoSize = true;
            this.lblAverageLongRTT.Location = new Point(0x8b, 0x7f);
            this.lblAverageLongRTT.Name = "lblAverageLongRTT";
            this.lblAverageLongRTT.Size = new Size(13, 13);
            this.lblAverageLongRTT.TabIndex = 7;
            this.lblAverageLongRTT.Text = "0";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(12, 0x7f);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x63, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Average Long RTT";
            this.lblTimeouts.AutoSize = true;
            this.lblTimeouts.Location = new Point(0x8b, 0x9b);
            this.lblTimeouts.Name = "lblTimeouts";
            this.lblTimeouts.Size = new Size(13, 13);
            this.lblTimeouts.TabIndex = 9;
            this.lblTimeouts.Text = "0";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(12, 0x9b);
            this.label7.Name = "label7";
            this.label7.Size = new Size(50, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Timeouts";
            this.lblAverageShortRTT.AutoSize = true;
            this.lblAverageShortRTT.Location = new Point(0x8b, 0x66);
            this.lblAverageShortRTT.Name = "lblAverageShortRTT";
            this.lblAverageShortRTT.Size = new Size(13, 13);
            this.lblAverageShortRTT.TabIndex = 11;
            this.lblAverageShortRTT.Text = "0";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(12, 0x66);
            this.label9.Name = "label9";
            this.label9.Size = new Size(100, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Average Short RTT";
            this.lblShortCount.AutoSize = true;
            this.lblShortCount.Location = new Point(0xf2, 0x66);
            this.lblShortCount.Name = "lblShortCount";
            this.lblShortCount.Size = new Size(13, 13);
            this.lblShortCount.TabIndex = 13;
            this.lblShortCount.Text = "0";
            this.lblLongCount.AutoSize = true;
            this.lblLongCount.Location = new Point(0xf2, 0x7f);
            this.lblLongCount.Name = "lblLongCount";
            this.lblLongCount.Size = new Size(13, 13);
            this.lblLongCount.TabIndex = 12;
            this.lblLongCount.Text = "0";
            this.tbDetailedLogging.Location = new Point(12, 0xb8);
            this.tbDetailedLogging.Multiline = true;
            this.tbDetailedLogging.Name = "tbDetailedLogging";
            this.tbDetailedLogging.ReadOnly = true;
            this.tbDetailedLogging.ScrollBars = ScrollBars.Vertical;
            this.tbDetailedLogging.Size = new Size(0x127, 0x88);
            this.tbDetailedLogging.TabIndex = 14;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x13f, 0x14c);
            base.Controls.Add(this.tbDetailedLogging);
            base.Controls.Add(this.lblShortCount);
            base.Controls.Add(this.lblLongCount);
            base.Controls.Add(this.lblAverageShortRTT);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.lblTimeouts);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.lblAverageLongRTT);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.lblAverageRTT);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.lblNetworkDataReceived);
            base.Controls.Add(this.lblNetworkDataSent);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "DebugPopup";
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Debug";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void run()
        {
            string str = CompressedClientSink.realDataSent.ToString() + " (" + CompressedClientSink.rawDataSent.ToString() + ") Pkts: " + CompressedClientSink.packetsSent.ToString();
            string[] strArray2 = new string[] { CompressedClientSink.rawDataReceived.ToString(), " (", (CompressedClientSink.realDataReceived - CompressedClientSink.midDataReceived).ToString(), "/", CompressedClientSink.realDataReceived.ToString(), ") Pkts: ", CompressedClientSink.packetsReceived.ToString() };
            string str2 = string.Concat(strArray2);
            this.lblNetworkDataSent.Text = str;
            this.lblNetworkDataReceived.Text = str2;
            this.lblAverageRTT.Text = ((int) RemoteServices.Instance.RTTAverageTime).ToString();
            this.lblAverageShortRTT.Text = ((int) RemoteServices.Instance.RTTAverageShortTime).ToString();
            this.lblAverageLongRTT.Text = ((int) RemoteServices.Instance.RTTAverageLongTime).ToString();
            this.lblTimeouts.Text = RemoteServices.Instance.RTTTimeOuts.ToString();
            this.lblShortCount.Text = RemoteServices.Instance.RTTAverageShortCount.ToString();
            this.lblLongCount.Text = RemoteServices.Instance.RTTAverageLongCount.ToString();
            List<RemoteServices.RTT_Log_data> list = RemoteServices.Instance.getDetailedLogging();
            if (this.lastNumLines != list.Count)
            {
                bool flag = true;
                StringBuilder builder = new StringBuilder();
                try
                {
                    foreach (RemoteServices.RTT_Log_data _data in list)
                    {
                        string str3 = _data.packetType.Name + "               ";
                        if (_data.time < 0)
                        {
                            str3 = str3 + "TimeOut";
                        }
                        else
                        {
                            str3 = str3 + _data.time.ToString();
                        }
                        builder.AppendLine(str3);
                    }
                }
                catch (Exception)
                {
                    flag = false;
                }
                if (flag)
                {
                    this.tbDetailedLogging.Text = builder.ToString();
                    this.lastNumLines = list.Count;
                }
            }
        }
    }
}

