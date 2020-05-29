using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using Microsoft.WindowsAPICodePack.Dialogs;
using YT2MP3.Properties;

namespace YT2MP3
{
    public partial class Form1 : Form
    {
        string destinationPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckUpdate();

            txtURL.Focus();
        }

        private async void CheckUpdate()
        {
            try
            {
                string exeUrl = string.Empty;
                string installedVersion = ConfigurationManager.AppSettings["version"];

                HtmlWeb hw = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc = hw.Load("https://ytdl-org.github.io/youtube-dl/download.html");
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    // Get the exe Url
                    if (link.InnerText.Equals("Windows exe"))
                    {
                        exeUrl = link.GetAttributeValue("href", string.Empty);

                        continue;
                    }

                    // Get the latest version date
                    if (link.InnerText.Contains("2020.") || link.InnerText.Contains("2021."))
                    {
                        if (string.IsNullOrEmpty(installedVersion) || !installedVersion.Equals(link.InnerText))
                        {
                            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                            config.AppSettings.Settings["version"].Value = link.InnerText;
                            config.Save(ConfigurationSaveMode.Modified);
                            ConfigurationManager.RefreshSection("appSettings");

                            if (!string.IsNullOrEmpty(exeUrl)) {
                                using (WebClient wc = new WebClient())
                                {
                                    lblUpdate.Visible = false;
                                    progBar.Visible = true;
                                    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                                    wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadDataCompleted);
                                    await wc.DownloadFileTaskAsync(
                                        new Uri(exeUrl),
                                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Embedded", "youtube-dl.exe"));
                                    
                                }
                            }
                        }
                        break;
                    }

                    
                }
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show(
                    this,
                    ex.Message,
                    "ERROR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                }));
            }
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progBar.Value = e.ProgressPercentage;
        }

        void wc_DownloadDataCompleted(object sender, AsyncCompletedEventArgs e)
        {
            progBar.Visible = false;
            lblUpdate.Visible = true;
            lblUpdate.Text = "The program has been updated.";
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            dialog.Title = "Select destination folder";
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                destinationPath = dialog.FileName;
                string folder = destinationPath.Substring(destinationPath.LastIndexOf("\\") + 1);
                lblDestination.Text = folder;

                destinationPath = Path.Combine(destinationPath, "%(title)s.%(ext)s");

                if (!string.IsNullOrEmpty(txtURL.Text.ToString()))
                {
                    btnConvert.Enabled = true;
                    btnConvert.BackgroundImage = Resources.play;
                }
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            lblUpdate.Text = "Converting and downloading...";
            string url = txtURL.Text.ToString();

            txtURL.Text = string.Empty;
            btnConvert.Enabled = false;
            btnConvert.BackgroundImage = Resources.play_disabled;
            flpDestination.Enabled = false;
            
            string executablePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Embedded", "youtube-dl.exe");

            ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + string.Format("{0} --extract-audio --audio-format mp3 --audio-quality 0 -o \"{1}\" {2}", executablePath, destinationPath, url));

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;

            using (Process process = new Process())
            {
                process.StartInfo = procStartInfo;
                process.Start();

                process.WaitForExit();

                string result = process.StandardOutput.ReadToEnd();
                Console.WriteLine(result);
            }

            lblUpdate.Text = "Conversion complete. File downloaded.";
            flpDestination.Enabled = true;
        }

        private void flpDestination_MouseClick(object sender, MouseEventArgs e)
        {
            lblUpdate.Text = string.Empty;
            btnSelectFolder_Click(sender, e);
        }

        private void txtURL_TextChanged(object sender, EventArgs e)
        {
            lblUpdate.Text = string.Empty;

            if (!string.IsNullOrEmpty(txtURL.Text.ToString()) && !string.IsNullOrEmpty(destinationPath))
            {
                btnConvert.Enabled = true;
                btnConvert.BackgroundImage = Resources.play;
            }
        }
    }
}
