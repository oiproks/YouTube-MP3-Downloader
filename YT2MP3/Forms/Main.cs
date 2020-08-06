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
using System.Web;
using System.Windows.Forms;
using YT2MP3.Properties;
using YT2MP3.Statics;
using YT2MP3.Various;
using static YT2MP3.Statics.Commands;

namespace YT2MP3
{
    public partial class MainPanel : Form
    {
        #region Variables
        string destinationPath = string.Empty, executablePath;
        List<VideoInfos> urlList;
        List<VideoInfos> workingList;
        List<VideoInfos> removeList;
        CommandList commandsList;
        int listCount = 0;
        int count = 1;
        bool converting = false;
        ToolTip tip = new ToolTip();
        VideoHistory history = new VideoHistory();
        History hisPanel;
        #endregion

        #region Init
        public MainPanel()
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

            urlList = new List<VideoInfos>();
            workingList = new List<VideoInfos>();
            removeList = new List<VideoInfos>();
            commandsList = new CommandList();

            executablePath = Utils.AddQuotesIfRequired(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Embedded", "youtube-dl.exe"));

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


            if (hisPanel != null && !hisPanel.positionSet)
            {
                if (hisPanel.isPositionRight)
                {
                    if (Location.X + Width + 10 >= hisPanel.Location.X || Location.X + Width - 10 <= hisPanel.Location.X)
                        hisPanel.Location = new Point(Location.X + Width + 10, hisPanel.Location.Y);
                } else
                {
                    if (hisPanel.Location.X + hisPanel.Width + 10 >= Location.X || hisPanel.Location.X + hisPanel.Width - 10 <= Location.X)
                        hisPanel.Location = new Point(Location.X - 10 - hisPanel.Width, hisPanel.Location.Y);
                }
            }
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
            //progBar.Value = e.ProgressPercentage;
            UpdateProgressBar(e.ProgressPercentage);
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

                if (urlList.Count > 0 && (chkAudio.Checked || chkVideo.Checked))
                        EnableConvert(true);
            }
        }

        private void EnableConvert(bool enable)
        {
            btnConvert.Enabled = enable;
            btnConvert.BackgroundImage = enable ? Resources.play : Resources.play_disabled;
        }

        private void Convert_Click(object sender, EventArgs e)
        {
            lblUpdate.Text = string.Format("Converting and downloading: 0/{0}", listCount);
            EnableConvert(false);
            flpDestination.Enabled = false;

            foreach (VideoInfos url in urlList)
                workingList.Add(url);

            urlList.Clear();
            flpSettings.Enabled = false;

            Parameter param;

            if (chkAudio.Checked)
            {
                commandsList.AddCommand(Com.ExtractAudio);
                commandsList.AddCommand(Com.AudioQuality, new Parameter(0));

                param = new Parameter(AudioFormats.GetAudioFormat(cmbOptions.SelectedItem.ToString()));
            }
            else
                param = new Parameter(VideoFormats.GetVideoFormat(cmbOptions.SelectedItem.ToString()));

            commandsList.AddCommand(chkVideo.Checked ? Com.VideoFormat : Com.AudioFormat, param);

            // Output
            commandsList.AddCommand(Com.Output, new Parameter(destinationPath));

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
                {
                    hisPanel.Location = new Point(this.Location.X + this.Width + 10, this.Location.Y);
                    hisPanel.isPositionRight = true;
                }
                else
                {
                    hisPanel.Location = new Point(this.Location.X - 10 - hisPanel.Width, this.Location.Y);
                    hisPanel.isPositionRight = false;
                }

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

        private void AudioCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked)
            {
                cmbOptions.Items.Clear();
                chkVideo.Checked = false;
                foreach (string format in AudioFormats.GetAllFormats())
                    cmbOptions.Items.Add(format.ToUpper());

                cmbOptions.SelectedIndex = 0;
                cmbOptions.Enabled = true;

                if (!string.IsNullOrEmpty(destinationPath) && urlList.Count > 0)
                    EnableConvert(true);
            }
            else if (!chkAudio.Checked)
            {
                cmbOptions.Items.Clear();
                EnableConvert(false);
            }
        }

        private void VideoCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Checked)
            {
                cmbOptions.Items.Clear();
                chkAudio.Checked = false;
                foreach (string format in VideoFormats.GetAllFormats())
                    cmbOptions.Items.Add(format.ToUpper());

                cmbOptions.SelectedIndex = 0;
                cmbOptions.Enabled = true;

                if (!string.IsNullOrEmpty(destinationPath) && urlList.Count > 0)
                    EnableConvert(true);
            }
            else if (!chkVideo.Checked)
            {
                cmbOptions.Items.Clear();
                EnableConvert(false);
            }
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
                    url = url.Contains("&list") ? url.Substring(0, url.IndexOf("&list")) : url;
                    url = url.Contains("&feature") ? url.Substring(0, url.IndexOf("&feature")) : url;

                    string title = GetTitle(url);

                    if (string.IsNullOrEmpty(title))
                    {
                        throw new Exception("Can not reach the URL. Probably geografically blocked");
                    }

                    lstBox.Items.Add(HttpUtility.HtmlDecode(title));

                    urlList.Add(new VideoInfos(HttpUtility.HtmlDecode(title), url));

                    listCount++;
                    if (converting)
                        UpdateLabel(string.Format("Converting and downloading: {0}/{1}", count, listCount));

                    txtURL.Text = string.Empty;

                    if (!btnConvert.Enabled && !string.IsNullOrEmpty(destinationPath) && (chkAudio.Checked || chkVideo.Checked) && !converting)
                    {
                        EnableConvert(true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Impossible to get URL from YouTube.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteError(ex.Message);
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
            ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + $"{executablePath} --get-title {url}");

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

        bool error = false;
        string errorMsg = string.Empty;
        private void Download()
        {
            Thread thread;

            while (true)
            {
                converting = true;
                foreach (VideoInfos vi in workingList)
                {
                    if (removeList.FindAll(x => x.Title == vi.Title).Count > 0)
                    {
                        removeList.Remove(removeList.Find(x => x.Title == vi.Title));
                        continue;
                    }

                    thread = new Thread(new ParameterizedThreadStart(UpdateLabel));
                    thread.Start(string.Format("Converting and downloading: {0}/{1}", count, listCount));

                    string command = BuildCommandLine(executablePath, commandsList, vi.URL);

                    ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", $"{command}");

                    procStartInfo.RedirectStandardOutput = true;
                    procStartInfo.UseShellExecute = false;
                    procStartInfo.CreateNoWindow = true;
                    procStartInfo.RedirectStandardError = true;

                    using (Process process = new Process())
                    {
                        process.StartInfo = procStartInfo;
                        process.OutputDataReceived += new DataReceivedEventHandler(OutputDataHandler);
                        process.Start();
                        process.BeginOutputReadLine();
                        process.WaitForExit();
                        process.OutputDataReceived -= OutputDataHandler;
                    }

                    count++;
                    this.Invoke(new Action(() =>
                    {
                        lstBox.Items.RemoveAt(0);
                    }));

                    history.AddToHistory(vi);

                    if (hisPanel != null)
                        this.Invoke(new Action(() =>
                        {
                            hisPanel.PopulateList(history.HistoryList);
                        }));
                }

                workingList.Clear();

                if (urlList.Count == 0)
                {
                    break;
                }
                else
                {
                    foreach (VideoInfos url in urlList)
                        workingList.Add(url);

                    urlList.Clear();
                }
            }
            converting = false;
            commandsList = new CommandList();

            thread = new Thread(new ParameterizedThreadStart(UpdateLabel));
            string labelResult = string.Empty;
            if (!error)
                labelResult = "Conversion completed.";
            else
            {
                if (executablePath.Contains(" "))
                    labelResult = "ERROR! Program path contains white spaces.";
                else
                    labelResult = $"Conversion completed with errors: {errorMsg}";
            }

            thread.Start(labelResult);

            this.Invoke(new Action(() =>
            {
                EnableConvert(false);
                flpSettings.Enabled = true;
                flpDestination.Enabled = true;
            }));

            listCount = 0;
            count = 1;
            error = false;
        }

        void OutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (outLine.Data != null)
            {
                if (outLine.Data.Contains("%"))
                {
                    //string test = outLine.Data.Substring(10, outLine.Data.IndexOf("%") + 1).Trim();
                    string test = outLine.Data.Substring(10, outLine.Data.IndexOf("%") - 10).Trim();
                    int progress = (int)float.Parse(test);
                    if (test.Contains("100"))
                    {
                        this.Invoke(new Action(() =>
                        {
                            progBar.Visible = false;
                            lblUpdate.Visible = true;
                            lblClipboard.Visible = false;
                            lblUpdate.Text = $"Download completed. Converting file: {count}/{listCount}";
                        }));
                    } else
                        UpdateProgressBar(progress, outLine.Data);
                }
                if (outLine.Data.ToLower().Contains("exception"))
                {
                    error = true;
                    errorMsg = outLine.Data;
                }
            }
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
                removeList.Add(new VideoInfos(lstBox.Items[selectedIndex].ToString(), ""));
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
            chkAudio.ForeColor = foreColor;
            chkVideo.ForeColor = foreColor;
            flpSettings.ForeColor = foreColor;

            txtURL.BackColor = backcolor;
            lstBox.BackColor = backcolor;
            flpSettings.BackColor = backcolor;

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

        void UpdateProgressBar(int value, string text = null)
        {
            this.Invoke(new Action(() =>
            {
                if (!progBar.Visible)
                {
                    lblUpdate.Visible = false;
                    progBar.Visible = true;
                    if (!string.IsNullOrEmpty(text))
                        lblClipboard.Visible = true;
                }
                progBar.Value = value;
                if (!string.IsNullOrEmpty(text))
                    lblClipboard.Text = text.Substring(10, text.IndexOf("ETA") - 10).Trim();
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