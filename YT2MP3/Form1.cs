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
using System.Web;
using System.Windows.Forms;
using HtmlAgilityPack;
using Microsoft.WindowsAPICodePack.Dialogs;
using YT2MP3.Properties;

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace YT2MP3
{
    public partial class Form1 : Form
    {
        string destinationPath;
        List<string> urlList;
        List<string> workingList;
        int listCount = 0;
        int count = 1;
        bool converting = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckUpdate();

            txtURL.Focus();

            urlList = new List<string>();
            workingList = new List<string>();
        }

        private async Task<string> GetTitle(string url)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyCQTeygLLYWKCj4zjpqQBQSc1I_6jN_ipE",
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = url; 
            searchListRequest.MaxResults = 1;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            //foreach (var searchResult in searchListResponse.Items)
            //{
            //    switch (searchResult.Id.Kind)
            //    {
            //        case "youtube#video":
            //            return searchResult.Snippet.Title;
            //    }
            //}

            return searchListResponse.Items[0].Snippet.Title;
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

                if (urlList.Count > 0)
                {
                    btnConvert.Enabled = true;
                    btnConvert.BackgroundImage = Resources.play;
                }
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            lblUpdate.Text = string.Format("Converting and downloading: 0/{0}", listCount);
            btnConvert.Enabled = false;
            btnConvert.BackgroundImage = Resources.play_disabled;
            flpDestination.Enabled = false;

            foreach (string url in urlList)
                workingList.Add(url);

            urlList.Clear();
            
            Thread thread = new Thread(Download);
            thread.Start();
        }

        private void Download()
        {
            Thread thread;
            while (true)
            {
                converting = true;
                foreach (string url in workingList)
                {
                    thread = new Thread(new ParameterizedThreadStart(UpdateLabel));
                    thread.Start(string.Format("Converting and downloading: {0}/{1}", count, listCount));

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

                    count++;
                    this.Invoke(new Action(() =>
                    {
                        lstBox.Items.RemoveAt(0);
                    }));
                }

                workingList.Clear();

                if (urlList.Count == 0) {
                    break;
                }
                else
                {
                    foreach (string url in urlList)
                        workingList.Add(url);
                    urlList.Clear();
                }
            }
            converting = false;

            thread = new Thread(new ParameterizedThreadStart(UpdateLabel));
            thread.Start("Conversion complete. File downloaded.");

            this.Invoke(new Action(() =>
            {
                btnConvert.Enabled = false;
                btnConvert.BackgroundImage = Resources.play_disabled;
                flpDestination.Enabled = true;
            }));

            listCount = 0;
            count = 1;
        }

        private void UpdateLabel(object text)
        {
            this.Invoke(new Action(() =>
            {
                lblUpdate.Text = text.ToString();
            }));
        }

        private void flpDestination_MouseClick(object sender, MouseEventArgs e)
        {
            lblUpdate.Text = string.Empty;
            btnSelectFolder_Click(sender, e);
        }

        private void txtURL_TextChanged(object sender, EventArgs e)
        {
            string URL = txtURL.Text.ToString();
            if (!string.IsNullOrEmpty(URL) && (URL.Contains("youtu.be") || URL.Contains("youtube.com")))
            {
                txtURL_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
        }

        private async void txtURL_KeyDown(object sender, KeyEventArgs e)
        {
            string url = txtURL.Text.ToString();
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(url))
            {
                try
                {
                    string videoID = url.Substring(url.IndexOf("?v=") + 3);
                    videoID = videoID.Contains("&list") ? videoID.Substring(0, videoID.IndexOf("&list")) : videoID;
                    string title = await GetTitle(videoID);

                    lstBox.Items.Add(HttpUtility.HtmlDecode(title));

                    urlList.Add(url);

                    listCount++;
                    if (converting)
                        UpdateLabel(string.Format("Converting and downloading: {0}/{1}", count, listCount));

                    txtURL.Text = string.Empty;

                    if (!btnConvert.Enabled && !string.IsNullOrEmpty(destinationPath) && !converting)
                    {
                        btnConvert.Enabled = true;
                        btnConvert.BackgroundImage = Resources.play;
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show(this, "Impossible to get URL from YouTube.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
