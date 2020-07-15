namespace YT2MP3
{
    partial class mainPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainPanel));
            this.txtURL = new System.Windows.Forms.TextBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.lblDestination = new System.Windows.Forms.Label();
            this.flpDestination = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.lblUpdate = new System.Windows.Forms.Label();
            this.lstBox = new System.Windows.Forms.ListBox();
            this.lblClipboard = new System.Windows.Forms.Label();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOnTop = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDayNight = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.topBar = new System.Windows.Forms.PictureBox();
            this.flpDestination.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topBar)).BeginInit();
            this.SuspendLayout();
            // 
            // txtURL
            // 
            this.txtURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtURL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtURL.ForeColor = System.Drawing.Color.White;
            this.txtURL.Location = new System.Drawing.Point(47, 49);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(202, 20);
            this.txtURL.TabIndex = 2;
            this.txtURL.TextChanged += new System.EventHandler(this.txtURL_TextChanged);
            this.txtURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtURL_KeyDown);
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.BackColor = System.Drawing.Color.Transparent;
            this.lblUrl.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrl.ForeColor = System.Drawing.Color.White;
            this.lblUrl.Location = new System.Drawing.Point(12, 52);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(28, 14);
            this.lblUrl.TabIndex = 3;
            this.lblUrl.Text = "URL";
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.BackColor = System.Drawing.Color.Transparent;
            this.lblDestination.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestination.ForeColor = System.Drawing.Color.White;
            this.lblDestination.Location = new System.Drawing.Point(41, 15);
            this.lblDestination.Margin = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(142, 14);
            this.lblDestination.TabIndex = 5;
            this.lblDestination.Tag = "";
            this.lblDestination.Text = "Select destination folder";
            this.lblDestination.MouseClick += new System.Windows.Forms.MouseEventHandler(this.flpDestination_MouseClick);
            // 
            // flpDestination
            // 
            this.flpDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flpDestination.BackColor = System.Drawing.Color.Transparent;
            this.flpDestination.Controls.Add(this.btnSelectFolder);
            this.flpDestination.Controls.Add(this.lblDestination);
            this.flpDestination.Location = new System.Drawing.Point(12, 263);
            this.flpDestination.Name = "flpDestination";
            this.flpDestination.Size = new System.Drawing.Size(196, 40);
            this.flpDestination.TabIndex = 6;
            this.flpDestination.MouseClick += new System.Windows.Forms.MouseEventHandler(this.flpDestination_MouseClick);
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectFolder.BackgroundImage = global::YT2MP3.Properties.Resources.folder;
            this.btnSelectFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectFolder.FlatAppearance.BorderSize = 0;
            this.btnSelectFolder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnSelectFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectFolder.Location = new System.Drawing.Point(3, 3);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(35, 35);
            this.btnSelectFolder.TabIndex = 4;
            this.btnSelectFolder.TabStop = false;
            this.btnSelectFolder.Tag = "path";
            this.btnSelectFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelectFolder.UseVisualStyleBackColor = false;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            this.btnSelectFolder.MouseHover += new System.EventHandler(this.btn_MouseOver);
            // 
            // progBar
            // 
            this.progBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progBar.BackColor = System.Drawing.Color.White;
            this.progBar.ForeColor = System.Drawing.Color.LimeGreen;
            this.progBar.Location = new System.Drawing.Point(12, 234);
            this.progBar.Margin = new System.Windows.Forms.Padding(0);
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(268, 22);
            this.progBar.TabIndex = 8;
            this.progBar.Visible = false;
            // 
            // lblUpdate
            // 
            this.lblUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUpdate.BackColor = System.Drawing.Color.Transparent;
            this.lblUpdate.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdate.ForeColor = System.Drawing.Color.White;
            this.lblUpdate.Location = new System.Drawing.Point(12, 234);
            this.lblUpdate.Margin = new System.Windows.Forms.Padding(0);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(268, 22);
            this.lblUpdate.TabIndex = 9;
            this.lblUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstBox
            // 
            this.lstBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lstBox.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstBox.ForeColor = System.Drawing.Color.White;
            this.lstBox.FormattingEnabled = true;
            this.lstBox.HorizontalScrollbar = true;
            this.lstBox.ItemHeight = 14;
            this.lstBox.Location = new System.Drawing.Point(12, 78);
            this.lstBox.Name = "lstBox";
            this.lstBox.Size = new System.Drawing.Size(268, 144);
            this.lstBox.TabIndex = 11;
            this.lstBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstBox_MouseDown);
            // 
            // lblClipboard
            // 
            this.lblClipboard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblClipboard.AutoSize = true;
            this.lblClipboard.BackColor = System.Drawing.Color.White;
            this.lblClipboard.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClipboard.ForeColor = System.Drawing.Color.Gray;
            this.lblClipboard.Location = new System.Drawing.Point(66, 3);
            this.lblClipboard.Name = "lblClipboard";
            this.lblClipboard.Size = new System.Drawing.Size(136, 14);
            this.lblClipboard.TabIndex = 14;
            this.lblClipboard.Text = "URL copied to clipboard";
            this.lblClipboard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblClipboard.Visible = false;
            // 
            // btnMin
            // 
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.BackColor = System.Drawing.Color.White;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnMin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.Location = new System.Drawing.Point(188, 14);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(25, 12);
            this.btnMin.TabIndex = 9;
            this.btnMin.TabStop = false;
            this.btnMin.Tag = "minimize";
            this.btnMin.UseVisualStyleBackColor = false;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            this.btnMin.MouseHover += new System.EventHandler(this.btn_MouseOver);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Firebrick;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkRed;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(262, 14);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(25, 12);
            this.btnClose.TabIndex = 10;
            this.btnClose.TabStop = false;
            this.btnClose.Tag = "close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseHover += new System.EventHandler(this.btn_MouseOver);
            // 
            // btnOnTop
            // 
            this.btnOnTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOnTop.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnOnTop.FlatAppearance.BorderSize = 0;
            this.btnOnTop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSeaGreen;
            this.btnOnTop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.btnOnTop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOnTop.Location = new System.Drawing.Point(225, 14);
            this.btnOnTop.Name = "btnOnTop";
            this.btnOnTop.Size = new System.Drawing.Size(25, 12);
            this.btnOnTop.TabIndex = 11;
            this.btnOnTop.TabStop = false;
            this.btnOnTop.Tag = "ontop";
            this.btnOnTop.UseVisualStyleBackColor = false;
            this.btnOnTop.Click += new System.EventHandler(this.btnOnTop_click);
            this.btnOnTop.MouseHover += new System.EventHandler(this.btn_MouseOver);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 268F));
            this.tableLayoutPanel1.Controls.Add(this.lblClipboard, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 250);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(268, 20);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // btnDayNight
            // 
            this.btnDayNight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDayNight.BackColor = System.Drawing.Color.Transparent;
            this.btnDayNight.BackgroundImage = global::YT2MP3.Properties.Resources.day;
            this.btnDayNight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDayNight.FlatAppearance.BorderSize = 0;
            this.btnDayNight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnDayNight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDayNight.Location = new System.Drawing.Point(255, 46);
            this.btnDayNight.Name = "btnDayNight";
            this.btnDayNight.Size = new System.Drawing.Size(25, 25);
            this.btnDayNight.TabIndex = 12;
            this.btnDayNight.TabStop = false;
            this.btnDayNight.Tag = "day_night";
            this.btnDayNight.UseVisualStyleBackColor = false;
            this.btnDayNight.Click += new System.EventHandler(this.btnDayNight_Click);
            this.btnDayNight.MouseHover += new System.EventHandler(this.btn_MouseOver);
            // 
            // btnConvert
            // 
            this.btnConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConvert.BackColor = System.Drawing.Color.Transparent;
            this.btnConvert.BackgroundImage = global::YT2MP3.Properties.Resources.play_disabled;
            this.btnConvert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnConvert.Enabled = false;
            this.btnConvert.FlatAppearance.BorderSize = 0;
            this.btnConvert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConvert.Location = new System.Drawing.Point(245, 267);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(35, 35);
            this.btnConvert.TabIndex = 0;
            this.btnConvert.TabStop = false;
            this.btnConvert.Tag = "convert";
            this.btnConvert.UseVisualStyleBackColor = false;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            this.btnConvert.MouseHover += new System.EventHandler(this.btn_MouseOver);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblTitle.Font = new System.Drawing.Font("Bradley Hand ITC", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(88, 24);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Tag = "";
            this.lblTitle.Text = "YT2MP3";
            // 
            // topBar
            // 
            this.topBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.topBar.Location = new System.Drawing.Point(0, 5);
            this.topBar.Margin = new System.Windows.Forms.Padding(0);
            this.topBar.Name = "topBar";
            this.topBar.Size = new System.Drawing.Size(292, 32);
            this.topBar.TabIndex = 17;
            this.topBar.TabStop = false;
            this.topBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Interface_MouseDown);
            this.topBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Interface_MouseMove);
            this.topBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Interface_MouseUp);
            // 
            // mainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(292, 310);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnDayNight);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lstBox);
            this.Controls.Add(this.lblUpdate);
            this.Controls.Add(this.progBar);
            this.Controls.Add(this.flpDestination);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.btnMin);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOnTop);
            this.Controls.Add(this.topBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(266, 310);
            this.Name = "mainPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YT 2 MP3";
            this.Shown += new System.EventHandler(this.Form1_Load);
            this.flpDestination.ResumeLayout(false);
            this.flpDestination.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.FlowLayoutPanel flpDestination;
        private System.Windows.Forms.ProgressBar progBar;
        private System.Windows.Forms.Label lblUpdate;
        private System.Windows.Forms.ListBox lstBox;
        private System.Windows.Forms.Button btnDayNight;
        private System.Windows.Forms.Label lblClipboard;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOnTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox topBar;
    }
}

