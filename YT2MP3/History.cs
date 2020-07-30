using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YT2MP3.Properties;

namespace YT2MP3
{
    public partial class History : Form
    {
        private DownloadHistory history;
        public bool positionSet = false;
        public bool showing = false;

        #region Init
        public History(DownloadHistory history)
        {
            InitializeComponent();

            // Testing
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            this.history = history;
        }
        private void History_Load(object sender, EventArgs e)
        {
            SetColors(ConfigurationManager.AppSettings[Settings.Interface].Equals("day") ? ColourMode.Day : ColourMode.Night);

            foreach (VideoList vl in history.HistoryList)
                lstBox.Items.Add(vl.Title);

            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add(new MenuItem("Copy URL", CopyUrl));
            cm.MenuItems.Add(new MenuItem("Open in browser", OpenInBrowser));
            lstBox.ContextMenu = cm;
        }
        #endregion

        #region Resizing and Dragging
        bool mouseDown = false;
        public Point lastLocation;
        private void Interface_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Interface_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                positionSet = true;

                Location = new Point(
                    (Location.X - lastLocation.X) + e.X, (Location.Y - lastLocation.Y) + e.Y);

                Update();
            }
        }

        private void Interface_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private const int cGrip = 16;   // Grip size
        private const int cBorder = 5;  // Border size

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
                }
                else if (pos.X >= this.ClientSize.Width - cBorder && pos.Y <= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)11; // Right border
                    return;
                }
                else if (pos.X <= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cBorder)
                {
                    m.Result = (IntPtr)15; // Bottom border
                    return;
                }
                else if (pos.Y <= cBorder)
                {
                    m.Result = (IntPtr)12; // Bottom border
                    return;
                }
                else if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // Bottom Right Corner
                    return;
                }
            }
            base.WndProc(ref m);
        }
        #endregion

        #region Button Interaction
        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Song List Context Menu
        private void lstBox_MouseDown(object sender, MouseEventArgs e)
        {
            lstBox.SelectedIndex = lstBox.IndexFromPoint(e.X, e.Y);
        }

        private void CopyUrl(object sender, EventArgs e)
        {
            string popUpText = string.Empty;
            if (lstBox.SelectedIndex >= 0)
            {
                int selectedIndex = lstBox.SelectedIndex;
                string URL;
                URL = history.HistoryList.Find(x => x.Title == lstBox.Items[selectedIndex].ToString()).URL;

                Clipboard.SetText(URL);

                popUpText = "URL copied to clipboard";
            } else
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
                URL = history.HistoryList.Find(x => x.Title == Utils.CleanTitle(lstBox.Items[selectedIndex].ToString())).URL;

                Process.Start(URL);
            }
        }

        private void PopUp(object text)
        {
            this.Invoke(new Action(() =>
            {
                lblClipboard.Text = text.ToString(); ;
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
        public void SetColors(ColourMode style)
        {
            if (style == ColourMode.Night)
            {
                lstBox.ForeColor = Color.White;
                lstBox.BackColor = Color.FromArgb(64, 64, 64);
                this.BackColor = Color.FromArgb(64, 64, 64);
            }
            else
            {
                lstBox.ForeColor = Color.Black;
                lstBox.BackColor = Color.White;
                this.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
        }
        #endregion

        private void History_Activated(object sender, EventArgs e)
        {
            showing = true;
        }
    }
}
