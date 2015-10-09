namespace Kingdoms
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Net.Cache;

    public class InstallerUpdater
    {
        public static string downloadSelfUpdater(Uri uri)
        {
            WebClient client = new WebClient();
            RequestCachePolicy policy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            client.CachePolicy = policy;
            string fileName = Path.GetTempPath() + Path.GetFileName(uri.AbsolutePath);
            try
            {
                client.DownloadFile(uri, fileName);
                return fileName;
            }
            catch (Exception)
            {
            }
            return "";
        }

        public static bool runInstaller(string path)
        {
            string fileName = Path.GetFileName(path);
            path = Path.GetDirectoryName(path);
            Process process = new Process {
                StartInfo = new ProcessStartInfo(fileName)
            };
            process.StartInfo.WorkingDirectory = path;
            process.Start();
            return true;
        }
    }
}

