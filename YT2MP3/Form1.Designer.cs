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
            this.btnDayNight = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.backPanel = new System.Windows.Forms.PictureBox();
            this.lblClipboard = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flpDestination.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backPanel)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtURL
            // 
            this.txtURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtURL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtURL.ForeColor = System.Drawing.Color.White;
            this.txtURL.Location = new System.Drawing.Point(47, 17);
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
            this.lblUrl.Location = new System.Drawing.Point(12, 20);
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
            this.lblDestination.Text = "Select destination folder";
            this.lblDestination.MouseClick += new System.Windows.Forms.MouseEventHandler(this.flpDestination_MouseClick);
            // 
            // flpDestination
            // 
            this.flpDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flpDestination.BackColor = System.Drawing.Color.Transparent;
            this.flpDestination.Controls.Add(this.btnSelectFolder);
            this.flpDestination.Controls.Add(this.lblDestination);
            this.flpDestination.Location = new System.Drawing.Point(12, 224);
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
            this.btnSelectFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectFolder.Location = new System.Drawing.Point(3, 3);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(35, 35);
            this.btnSelectFolder.TabIndex = 4;
            this.btnSelectFolder.TabStop = false;
            this.btnSelectFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelectFolder.UseVisualStyleBackColor = false;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // progBar
            // 
            this.progBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progBar.BackColor = System.Drawing.Color.White;
            this.progBar.ForeColor = System.Drawing.Color.LimeGreen;
            this.progBar.Location = new System.Drawing.Point(12, 195);
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
            this.lblUpdate.Location = new System.Drawing.Point(12, 195);
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
            this.lstBox.Location = new System.Drawing.Point(12, 46);
            this.lstBox.Name = "lstBox";
            this.lstBox.Size = new System.Drawing.Size(268, 144);
            this.lstBox.TabIndex = 11;
            this.lstBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstBox_MouseDown);
            // 
            // btnDayNight
            // 
            this.btnDayNight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDayNight.BackColor = System.Drawing.Color.Transparent;
            this.btnDayNight.BackgroundImage = global::YT2MP3.Properties.Resources.day;
            this.btnDayNight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDayNight.FlatAppearance.BorderSize = 0;
            this.btnDayNight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDayNight.Location = new System.Drawing.Point(255, 14);
            this.btnDayNight.Name = "btnDayNight";
            this.btnDayNight.Size = new System.Drawing.Size(25, 25);
            this.btnDayNight.TabIndex = 12;
            this.btnDayNight.TabStop = false;
            this.btnDayNight.UseVisualStyleBackColor = false;
            this.btnDayNight.Click += new System.EventHandler(this.btnDayNight_Click);
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
            this.btnConvert.Location = new System.Drawing.Point(245, 228);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(35, 35);
            this.btnConvert.TabIndex = 0;
            this.btnConvert.TabStop = false;
            this.btnConvert.UseVisualStyleBackColor = false;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // backPanel
            // 
            this.backPanel.Enabled = false;
            this.backPanel.Location = new System.Drawing.Point(0, 0);
            this.backPanel.Name = "backPanel";
            this.backPanel.Size = new System.Drawing.Size(293, 272);
            this.backPanel.TabIndex = 13;
            this.backPanel.TabStop = false;
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.Controls.Add(this.lblClipboard, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 211);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(268, 20);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // mainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(292, 271);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnDayNight);
            this.Controls.Add(this.lstBox);
            this.Controls.Add(this.lblUpdate);
            this.Controls.Add(this.progBar);
            this.Controls.Add(this.flpDestination);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.backPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(266, 310);
            this.Name = "mainPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YT 2 MP3";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Form1_Load);
            this.flpDestination.ResumeLayout(false);
            this.flpDestination.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backPanel)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.PictureBox backPanel;
        private System.Windows.Forms.Label lblClipboard;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

