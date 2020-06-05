using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using HtmlAgilityPack;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using YT2MP3.Properties;

namespace YT2MP3
{
    public partial class mainPanel : Form
    {
        string destinationPath;
        List<VideoList> urlList;
        List<VideoList> workingList;
        List<VideoList> removeList;
        int listCount = 0;
        int count = 1;
        bool converting = false;

        private enum Mode
        {
            Night,
            Day
        }

        public mainPanel()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetColors(ConfigurationManager.AppSettings["interface"].Equals("day") ? Mode.Day : Mode.Night);

            CheckUpdate();

            txtURL.Focus();

            urlList = new List<VideoList>();
            workingList = new List<VideoList>();
            removeList = new List<VideoList>();

            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add(new MenuItem("Remove", RemoveItem));
            lstBox.ContextMenu = cm;
        }

        #region Update
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
        #endregion

        #region Interface Interactions
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

        private void flpDestination_MouseClick(object sender, MouseEventArgs e)
        {
            lblUpdate.Text = string.Empty;
            btnSelectFolder_Click(sender, e);
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            lblUpdate.Text = string.Format("Converting and downloading: 0/{0}", listCount);
            btnConvert.Enabled = false;
            btnConvert.BackgroundImage = Resources.play_disabled;
            flpDestination.Enabled = false;

            foreach (VideoList url in urlList)
                workingList.Add(url);

            urlList.Clear();
            
            Thread thread = new Thread(Download);
            thread.Start();
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
                txtURL.Enabled = false;
                try
                {
                    string videoID = url.Substring(url.IndexOf("?v=") + 3);
                    videoID = videoID.Contains("&list") ? videoID.Substring(0, videoID.IndexOf("&list")) : videoID;
                    string title = await GetTitle(videoID);

                    lstBox.Items.Add(HttpUtility.HtmlDecode(title));

                    urlList.Add(new VideoList(HttpUtility.HtmlDecode(title), url));

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
            txtURL.Enabled = true;
            txtURL.Focus();
        }

        private void btnDayNight_Click(object sender, EventArgs e)
        {
            string setting = ConfigurationManager.AppSettings["interface"];

            if (setting.Equals("day"))
            {
                SetColors(Mode.Night);
                setting = "night";
            }
            else
            {
                SetColors(Mode.Day);
                setting = "day";
            }

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["interface"].Value = setting;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void SetColors (Mode style)
        {
            Color foreColor, backcolor;
            Bitmap dayNight;

            if (style == Mode.Night)
            {
                foreColor = Color.White;
                backcolor = Color.FromArgb(64, 64, 64);
                dayNight = Resources.day;
                this.BackColor = backcolor;
            } else
            {
                foreColor = Color.Black;
                backcolor = Color.White;
                dayNight = Resources.night;
                this.BackColor = Color.FromKnownColor(KnownColor.Control);
            }

            lblUrl.ForeColor = foreColor;
            lblUpdate.ForeColor = foreColor;
            lblDestination.ForeColor = foreColor;
            txtURL.ForeColor = foreColor;
            lstBox.ForeColor = foreColor;

            txtURL.BackColor = backcolor;
            lstBox.BackColor = backcolor;

            btnDayNight.BackgroundImage = dayNight;
        }
        #endregion

        #region Download Management
        private void Download()
        {
            Thread thread;
            while (true)
            {
                converting = true;
                foreach (VideoList vl in workingList)
                {
                    if (removeList.FindAll(x => x.Title == vl.Title).Count > 0)
                    {
                        removeList.Remove(removeList.Find(x => x.Title == vl.Title));
                        continue;
                    }

                    thread = new Thread(new ParameterizedThreadStart(UpdateLabel));
                    thread.Start(string.Format("Converting and downloading: {0}/{1}", count, listCount));

                    string executablePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Embedded", "youtube-dl.exe");

                    ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + string.Format("{0} --extract-audio --audio-format mp3 --audio-quality 0 -o \"{1}\" {2}", executablePath, destinationPath, vl.URL));

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
                    foreach (VideoList url in urlList)
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

            return searchListResponse.Items[0].Snippet.Title;
        }
        #endregion

        private void UpdateLabel(object text)
        {
            this.Invoke(new Action(() =>
            {
                lblUpdate.Text = text.ToString();
            }));
        }

        #region Remove From List
        private void lstBox_MouseDown(object sender, MouseEventArgs e)
        {
            lstBox.SelectedIndex = lstBox.IndexFromPoint(e.X, e.Y);
        }

        private void RemoveItem(Object sender, System.EventArgs e)
        {
            if (lstBox.SelectedIndex > 0)
            {
                int selectedIndex = lstBox.SelectedIndex;
                removeList.Add(new VideoList(lstBox.Items[selectedIndex].ToString(), ""));
                lstBox.Items.Remove(lstBox.Items[selectedIndex]);
                listCount--;
                if (converting)
                    UpdateLabel(string.Format("Converting and downloading: {0}/{1}", count, listCount));
            }
        }
        #endregion
    }
}
