namespace DDay.Update.Bootstrap
{
    using DDay.Update;
    using DDay.Update.Configuration;
    using DDay.Update.Utilities;
    using Localization;
    using System;
    using System.Configuration;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    internal class Program
    {
        private static ILog log = new Log4NetLogger();

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            log.Error("An unhandled exception occurred during the update process!", e.ExceptionObject as Exception);
            Exception exceptionObject = e.ExceptionObject as Exception;
            MessageBox.Show(tm(), DDText.getText(0x12) + Environment.NewLine + exceptionObject.Message + "\n\n" + exceptionObject.ToString(), DDText.getText(13));
            Application.Exit();
        }

        private static void Main(string[] args)
        {
            bool flag;
            string name = @"Global\StrongholdKingdomsAlphaUpdater";
            using (new Mutex(true, name, out flag))
            {
                DDText.loadText(Application.StartupPath + @"\");
                if (!flag)
                {
                    MessageBox.Show(tm(), DDText.getText(6), DDText.getText(7));
                    return;
                }
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);
                DDayUpdateConfigurationSection section = ConfigurationManager.GetSection("DDay.Update") as DDayUpdateConfigurationSection;
                UpdateManager.SetCommandLineParameters(args);
                bool flag2 = true;
                bool flag3 = false;
                bool flag4 = false;
                bool flag5 = false;
                try
                {
                    if (((args != null) && (args.Length > 0)) && (args[0] == "-uninstall"))
                    {
                        flag2 = false;
                        UpdateManager.Uninstall();
                    }
                    log.Debug("Starting Updater V" + SelfUpdater.CurrentBuildMajorVersion.ToString() + "." + SelfUpdater.CurrentBuildMinorVersion.ToString());
                    Form owner = null;
                    if (!section.Automatic || !flag2)
                    {
                        goto Label_03D2;
                    }
                    Uri uri = SelfUpdater.isSelfUpdaterAvailable(section.Uri, DDText.getText(0x17) == "XX");
                    if (uri != null)
                    {
                        MessageBoxButtons oKCancel = MessageBoxButtons.OKCancel;
                        if (MessageBox.Show(tm(), DDText.getText(8) + Environment.NewLine + DDText.getText(9) + Environment.NewLine + Environment.NewLine + DDText.getText(10), DDText.getText(11), oKCancel) == DialogResult.OK)
                        {
                            string path = SelfUpdater.downloadSelfUpdater(uri);
                            if ((path != null) && (path.Length > 0))
                            {
                                SelfUpdater.runInstaller(path);
                            }
                        }
                        flag2 = false;
                        return;
                    }
                    if (!UpdateManager.IsUpdateAvailable(section.Uri, section.Username, section.Password, section.Domain))
                    {
                        goto Label_03C1;
                    }
                    log.Debug("Update is available, beginning update process...");
                    int num = 30;
                Label_01D3:
                    if (num < 0)
                    {
                        flag2 = false;
                        log.Debug("Run App Cancelled 1.");
                        flag3 = true;
                    }
                    else
                    {
                        if (num != 30)
                        {
                            UpdateManager.SysDelay(0x3e8);
                            if (flag5)
                            {
                                UpdateManager.IsUpdateAvailable(section.Uri, section.Username, section.Password, section.Domain);
                            }
                        }
                        if (UpdateManager.ManifestIssue)
                        {
                            MessageBox.Show(tm(), DDText.getText(12) + Environment.NewLine + Environment.NewLine + UpdateManager.ManifestString, DDText.getText(13));
                            flag2 = false;
                        }
                        else
                        {
                            bool flag6 = true;
                            while (UpdateManager.ConnectionIssue)
                            {
                                if (num == 30)
                                {
                                    UpdateManager.SysDelay(500);
                                }
                                MessageBoxButtons retryCancel = MessageBoxButtons.RetryCancel;
                                DialogResult cancel = DialogResult.Cancel;
                                if (UpdateManager._UpdateNotifier != null)
                                {
                                    owner = UpdateManager._UpdateNotifier.GetForm();
                                }
                                else
                                {
                                    owner = null;
                                }
                                if (owner != null)
                                {
                                    cancel = MessageBox.Show(owner, DDText.getText(14) + Environment.NewLine + Environment.NewLine + UpdateManager.WebErrorString, DDText.getText(15), retryCancel);
                                }
                                else
                                {
                                    cancel = MessageBox.Show(tm(), DDText.getText(14) + Environment.NewLine + Environment.NewLine + UpdateManager.WebErrorString, DDText.getText(15), retryCancel);
                                }
                                if (cancel == DialogResult.Retry)
                                {
                                    if (UpdateManager.IsUpdateAvailable(section.Uri, section.Username, section.Password, section.Domain))
                                    {
                                        continue;
                                    }
                                    flag6 = false;
                                }
                                else
                                {
                                    flag2 = false;
                                    flag6 = false;
                                    flag3 = false;
                                    flag5 = false;
                                    log.Debug("Run App Cancelled 2.");
                                }
                                break;
                            }
                            if (flag6)
                            {
                                flag5 = false;
                                log.Debug("Calling Update Manager...");
                                UpdateManager.Update();
                                num--;
                                if (UpdateManager.UserCancelled)
                                {
                                    flag2 = false;
                                    log.Debug("Run App Cancelled 3.");
                                    flag4 = true;
                                }
                                else
                                {
                                    if (UpdateManager.UserError)
                                    {
                                        flag5 = true;
                                    }
                                    if (flag5 || UpdateManager.IsUpdateAvailable(section.Uri, section.Username, section.Password, section.Domain))
                                    {
                                        goto Label_01D3;
                                    }
                                }
                            }
                        }
                    }
                    goto Label_04FF;
                Label_03C1:
                    log.Debug("Application is up-to-date.");
                    goto Label_04FF;
                Label_03D2:
                    log.Debug("Automatic updates are disabled.");
                }
                finally
                {
                    if (UpdateManager._UpdateNotifier != null)
                    {
                        Form form = UpdateManager._UpdateNotifier.GetForm();
                        if (form != null)
                        {
                            form.Close();
                        }
                    }
                    log.Debug("Main - Finally.");
                    if (flag2)
                    {
                        log.Debug("Main - Start App.");
                        string[] parameters = null;
                        if (DDText.getText(0x17) == "XX")
                        {
                            parameters = new string[] { "-InstallerVersion", "", "", "st" };
                        }
                        else
                        {
                            parameters = new string[] { "-InstallerVersion", "", "" };
                        }
                        parameters[1] = SelfUpdater.CurrentBuildVersion.ToString();
                        parameters[2] = DDText.getText(0);
                        UpdateManager.SetCommandLineParameters(parameters);
                        UpdateManager.StartApplication();
                    }
                    if (flag3 || flag5)
                    {
                        MessageBox.Show(tm(), DDText.getText(0x10), DDText.getText(13));
                    }
                    if (flag4)
                    {
                        MessageBox.Show(tm(), DDText.getText(0x11), DDText.getText(13));
                    }
                }
            Label_04FF:
                log.Debug("Exiting bootstrap application...");
                Application.Exit();
            }
        }

        private static Form tm()
        {
            Form form = new Form {
                TopMost = true,
                Size = new Size(1, 1),
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.CenterScreen
            };
            form.Show();
            form.TopMost = false;
            form.TopMost = true;
            form.BringToFront();
            form.Focus();
            return form;
        }
    }
}

