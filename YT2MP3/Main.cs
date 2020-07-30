﻿using HtmlAgilityPack;
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
using System.Web;
using System.Windows.Forms;
using YT2MP3.Properties;

namespace YT2MP3
{
    public partial class mainPanel : Form
    {
        #region Variables
        string destinationPath;
        List<VideoList> urlList;
        List<VideoList> workingList;
        List<VideoList> removeList;
        int listCount = 0;
        int count = 1;
        bool converting = false;
        ToolTip tip = new ToolTip();
        DownloadHistory history = new DownloadHistory();
        History hisPanel;
        #endregion

        #region Init
        public mainPanel()
        {
            InitializeComponent();
        
            // Testing
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetColors(ConfigurationManager.AppSettings[Settings.Interface].Equals("day") ? ColourMode.Day : ColourMode.Night);

            TopMost = !ConfigurationManager.AppSettings[Settings.OnTop].Equals(OnTopMode.False);

            lblDestination.Text = Utils.SetDestinationPath(ConfigurationManager.AppSettings[Settings.DownloadFolder], out destinationPath, false);

            CheckUpdate();

            txtURL.Focus();

            urlList = new List<VideoList>();
            workingList = new List<VideoList>();
            removeList = new List<VideoList>();

            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add(new MenuItem("Copy URL", CopyUrl));
            cm.MenuItems.Add(new MenuItem("Remove", RemoveItem));
            cm.MenuItems.Add(new MenuItem("Open in browser", OpenInBrowser));
            lstBox.ContextMenu = cm;
        }
        #endregion

        #region Resizing and Dragging
        bool mouseDown = false;
        Point lastLocation;
        private void Interface_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
            if (hisPanel != null && !hisPanel.positionSet)
            {
                hisPanel.lastLocation = e.Location;
            }
        }

        private void Interface_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                Location = new Point((Location.X - lastLocation.X) + e.X, (Location.Y - lastLocation.Y) + e.Y);

                if (hisPanel != null && !hisPanel.positionSet)
                {
                    hisPanel.Location = new Point((hisPanel.Location.X - hisPanel.lastLocation.X) + e.X, (hisPanel.Location.Y - hisPanel.lastLocation.Y) + e.Y);
                }

                Update();
            }
        }

        private void Interface_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private const int cGrip = 16;      // Grip size
        private const int cBorder = 5;      // Border size

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                
                if (pos.X <= cBorder)
                {
                    m.Result = (IntPtr)10; // Left border
                    return;
                } else if (pos.X >= this.ClientSize.Width - cBorder && pos.Y <= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)11; // Right border
                    return;
                } else if (pos.X <= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cBorder)
                {
                    m.Result = (IntPtr)15; // Bottom border
                    return;
                } else if (pos.Y <= cBorder)
                {
                    m.Result = (IntPtr)12; // Bottom border
                    return;
                } else if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // Bottom Right Corner
                    return;
                }
            }
            base.WndProc(ref m);
        }
        #endregion

        #region Search for Update
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

                            if (!string.IsNullOrEmpty(exeUrl))
                            {
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

        #region Buttons Interactions
        private void Destination_MouseClick(object sender, MouseEventArgs e)
        {
            lblUpdate.Text = string.Empty;
            SelectFolder_Click(sender, e);
        }

        private void SelectFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            dialog.Title = "Select destination folder";
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                lblDestination.Text = Utils.SetDestinationPath(dialog.FileName, out destinationPath);

                if (urlList.Count > 0)
                {
                    btnConvert.Enabled = true;
                    btnConvert.BackgroundImage = Resources.play;
                }
            }
        }
        
        private void Convert_Click(object sender, EventArgs e)
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

        private void DayNight_Click(object sender, EventArgs e)
        {
            string setting = ConfigurationManager.AppSettings["interface"];

            if (setting.Equals("day"))
            {
                SetColors(ColourMode.Night);
                setting = "night";
            }
            else
            {
                SetColors(ColourMode.Day);
                setting = "day";
            }

            Utils.SaveConfig(Settings.Interface, setting);
        }

        private void History_Click(object sender, EventArgs e)
        {
            if (hisPanel == null)
            {
                hisPanel = new History(history);
                hisPanel.FormClosed += new FormClosedEventHandler(HistoryClosed);

                hisPanel.StartPosition = FormStartPosition.Manual;

                if (this.Location.X + this.Width <= Screen.PrimaryScreen.Bounds.Width - hisPanel.Size.Width)
                    hisPanel.Location = new Point(this.Location.X + this.Width + 10, this.Location.Y);
                else
                    hisPanel.Location = new Point(this.Location.X - 10 - hisPanel.Width, this.Location.Y);

                hisPanel.Size = new Size(hisPanel.Width, this.Size.Height);
                hisPanel.TopMost = TopMost;
                hisPanel.Owner = this;
                hisPanel.Show();
            } else
            {
                hisPanel.Close();
                hisPanel = null;
            }
        }

        private void HistoryClosed(object sender, EventArgs e)
        {
            hisPanel = null;
        }

        private void Min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OnTop_click(object sender, EventArgs e)
        {
            string text, setting;
            int textWidth;

            if (TopMost)
            {
                TopMost = false;
                text = "Window won't stay on top anymore";
                textWidth = 100;
                setting = OnTopMode.False;
            }
            else
            {
                TopMost = true;
                text = "Window will stay on top";
                textWidth = 60;
                setting = OnTopMode.True;
            }

            if (hisPanel != null)
                hisPanel.TopMost = TopMost;

            tip.Dispose();
            tip = new ToolTip();
            int yLoc = txtURL.Location.Y - (txtURL.Height);
            tip.Show(text, this, (this.Width / 2) - textWidth, yLoc, 2000);

            Utils.SaveConfig(Settings.OnTop, setting);
        }
        #endregion

        #region Text Interactions
        private void URL_TextChanged(object sender, EventArgs e)
        {
            string URL = txtURL.Text.ToString();
            if (!string.IsNullOrEmpty(URL) && (URL.Contains("youtu.be") || URL.Contains("youtube.com")))
            {
                URL_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
        }

        private void URL_KeyDown(object sender, KeyEventArgs e)
        {
            string url = txtURL.Text.ToString();
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(url))
            {
                txtURL.Enabled = false;
                try
                {
                    string videoID = url.Substring(url.IndexOf("?v=") + 3);
                    videoID = videoID.Contains("&list") ? videoID.Substring(0, videoID.IndexOf("&list")) : videoID;

                    string title = GetTitle(url);

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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Impossible to get URL from YouTube.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            txtURL.Enabled = true;
            txtURL.Focus();
        }

        private void MouseOver(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string text = string.Empty;
            int x = button.Location.X;
            int y = button.Location.Y;
            int time = 1000;
            switch (button.Tag)
            {
                case "ontop":
                    if (!TopMost)
                        text = "Stay On Top";
                    else
                        text = "Remove From Top";
                    y += -button.Height - button.Height;
                    break;
                case "minimize":
                    text = "Minimize on taskbar";
                    y += -button.Height - button.Height;
                    break;
                case "close":
                    text = "Quit";
                    y += -button.Height - button.Height;
                    break;
                case "convert":
                    text = "Start converting";
                    x += button.Width;
                    break;
                case "day_night":
                    string setting = ConfigurationManager.AppSettings["interface"];
                    if (setting.Equals("day"))
                        text = "Switch to night mode";
                    else
                        text = "Switch to day mode";
                    x += button.Width;
                    break;
                case "history":
                    if (hisPanel == null)
                        text = history.HistoryList.Count > 0 ? string.Format("See history ({0} videos)", history.HistoryList.Count) : "History is empty";
                    else
                        text = "Close history panel.";
                    x += button.Width;
                    break;
                case "path":
                    text = ConfigurationManager.AppSettings[Settings.DownloadFolder];
                    y += button.Parent.Location.Y + button.Height / 4;
                    x += button.Parent.Location.X + button.Location.X + button.Width;
                    time = 2000;
                    break;
                default:
                    break;
            }
            tip.Dispose();
            tip = new ToolTip();
            tip.Show(text,
                this,
                x,
                y,
                time);
        }
        #endregion

        #region Download Management
        private string GetTitle(string url)
        {
            string executablePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Embedded", "youtube-dl.exe");

            ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + string.Format("{0} --get-title {1}", executablePath, url));

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;

            using (Process process = new Process())
            {
                process.StartInfo = procStartInfo;
                process.Start();

                process.WaitForExit();

                string result = process.StandardOutput.ReadToEnd();

                return result;
            }
        }

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

                    ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + string.Format("{0} --extract-audio --audio-format mp3 --audio-quality 0 -o \"{1}\" {2}", executablePath, destinationPath, vl.URL)); // --metadata-from-title \"%(artist)s - %(title)s\" --embed-thumbnail 

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

                    history.AddToHistory(vl);
                }

                workingList.Clear();

                if (urlList.Count == 0)
                {
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
        #endregion

        #region Song List Context Menu
        private void lstBox_MouseDown(object sender, MouseEventArgs e)
        {
            lstBox.SelectedIndex = lstBox.IndexFromPoint(e.X, e.Y);
        }

        private void RemoveItem(Object sender, System.EventArgs e)
        {
            if (converting && lstBox.SelectedIndex == 0)
                return;
            else 
            {
                int selectedIndex = lstBox.SelectedIndex;
                removeList.Add(new VideoList(lstBox.Items[selectedIndex].ToString(), ""));
                lstBox.Items.Remove(lstBox.Items[selectedIndex]);
                listCount--;
                if (converting)
                    UpdateLabel(string.Format("Converting and downloading: {0}/{1}", count, listCount));
            }
        }

        private void CopyUrl(object sender, EventArgs e)
        {
            string popUpText = string.Empty;
            if (lstBox.SelectedIndex >= 0)
            {
                int selectedIndex = lstBox.SelectedIndex;
                string URL;
                if (converting)
                {
                    URL = workingList.Find(x => x.Title == lstBox.Items[selectedIndex].ToString()).URL;
                    if (string.IsNullOrEmpty(URL))
                        URL = urlList.Find(x => x.Title == lstBox.Items[selectedIndex].ToString()).URL;
                }
                else
                    URL = urlList.Find(x => x.Title == lstBox.Items[selectedIndex].ToString()).URL;

                Clipboard.SetText(URL);

                popUpText = "URL copied to clipboard";
            }
            else
                popUpText = "Nothing selected";

            Thread thread = new Thread(new ParameterizedThreadStart(PopUp));
            thread.Start(popUpText);
        }
        
        private void OpenInBrowser(object sender, EventArgs e)
        {
            if (lstBox.SelectedIndex >= 0)
            {
                int selectedIndex = lstBox.SelectedIndex;
                string URL;
                if (converting)
                {
                    URL = workingList.Find(x => x.Title == Utils.CleanTitle(lstBox.Items[selectedIndex].ToString())).URL;
                    if (string.IsNullOrEmpty(URL))
                        URL = urlList.Find(x => x.Title == Utils.CleanTitle(lstBox.Items[selectedIndex].ToString())).URL;
                }
                else
                {
                    URL = urlList.Find(x => x.Title == Utils.CleanTitle(lstBox.Items[selectedIndex].ToString())).URL;
                }

                Process.Start(URL);
            }
        }

        private void PopUp(object text)
        {
            this.Invoke(new Action(() =>
            {
                lblClipboard.Text = text.ToString();
                lblClipboard.Visible = true;
            }));

            Thread.Sleep(1500);

            this.Invoke(new Action(() =>
            {
                lblClipboard.Visible = false;
            }));
        }
        #endregion

        #region Utils
        private void SetColors(ColourMode style)
        {
            Color foreColor, backcolor, mouseOverBack;
            Bitmap dayNight;

            if (style == ColourMode.Night)
            {
                foreColor = Color.White;
                backcolor = Color.FromArgb(64, 64, 64);
                dayNight = Resources.day;
                mouseOverBack = Color.Gray;
                this.BackColor = backcolor;
            }
            else
            {
                foreColor = Color.Black;
                backcolor = Color.White;
                dayNight = Resources.night;
                mouseOverBack = Color.Gainsboro;
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
            btnDayNight.FlatAppearance.MouseOverBackColor = mouseOverBack;
            btnHistory.FlatAppearance.MouseOverBackColor = mouseOverBack;
            btnSelectFolder.FlatAppearance.MouseOverBackColor = mouseOverBack;
            btnConvert.FlatAppearance.MouseOverBackColor = mouseOverBack;

            if (hisPanel != null)
                hisPanel.SetColors(style);
        }

        private void UpdateLabel(object text)
        {
            this.Invoke(new Action(() =>
            {
                lblUpdate.Text = text.ToString();
            }));
        }
        #endregion

        #region DropUrl
        private void mainPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void mainPanel_DragDrop(object sender, DragEventArgs e)
        {
            txtURL.Text = e.Data.GetData(DataFormats.Text).ToString();
        }
        #endregion

        private void mainPanel_Activated(object sender, EventArgs e)
        {
            if (hisPanel != null && !hisPanel.showing)
            {
                hisPanel.Activate();
                this.Focus();
            }
        }
    }
}