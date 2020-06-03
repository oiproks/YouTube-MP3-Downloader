namespace YT2MP3
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtURL = new System.Windows.Forms.TextBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.lblDestination = new System.Windows.Forms.Label();
            this.flpDestination = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.lblUpdate = new System.Windows.Forms.Label();
            this.lstBox = new System.Windows.Forms.ListBox();
            this.flpDestination.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(47, 6);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(191, 20);
            this.txtURL.TabIndex = 2;
            this.txtURL.TextChanged += new System.EventHandler(this.txtURL_TextChanged);
            this.txtURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtURL_KeyDown);
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrl.Location = new System.Drawing.Point(12, 9);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(29, 13);
            this.lblUrl.TabIndex = 3;
            this.lblUrl.Text = "URL";
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestination.Location = new System.Drawing.Point(41, 15);
            this.lblDestination.Margin = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(120, 13);
            this.lblDestination.TabIndex = 5;
            this.lblDestination.Text = "Select destination folder";
            this.lblDestination.MouseClick += new System.Windows.Forms.MouseEventHandler(this.flpDestination_MouseClick);
            // 
            // flpDestination
            // 
            this.flpDestination.Controls.Add(this.btnSelectFolder);
            this.flpDestination.Controls.Add(this.lblDestination);
            this.flpDestination.Location = new System.Drawing.Point(12, 224);
            this.flpDestination.Name = "flpDestination";
            this.flpDestination.Size = new System.Drawing.Size(186, 40);
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
            // btnConvert
            // 
            this.btnConvert.BackColor = System.Drawing.Color.Transparent;
            this.btnConvert.BackgroundImage = global::YT2MP3.Properties.Resources.play_disabled;
            this.btnConvert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnConvert.Enabled = false;
            this.btnConvert.FlatAppearance.BorderSize = 0;
            this.btnConvert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConvert.Location = new System.Drawing.Point(203, 228);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(35, 35);
            this.btnConvert.TabIndex = 0;
            this.btnConvert.TabStop = false;
            this.btnConvert.UseVisualStyleBackColor = false;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // progBar
            // 
            this.progBar.Location = new System.Drawing.Point(9, 195);
            this.progBar.Margin = new System.Windows.Forms.Padding(0);
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(232, 23);
            this.progBar.TabIndex = 8;
            this.progBar.Visible = false;
            // 
            // lblUpdate
            // 
            this.lblUpdate.BackColor = System.Drawing.Color.Transparent;
            this.lblUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdate.Location = new System.Drawing.Point(9, 195);
            this.lblUpdate.Margin = new System.Windows.Forms.Padding(0);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(229, 22);
            this.lblUpdate.TabIndex = 9;
            this.lblUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstBox
            // 
            this.lstBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstBox.FormattingEnabled = true;
            this.lstBox.HorizontalScrollbar = true;
            this.lstBox.Location = new System.Drawing.Point(12, 32);
            this.lstBox.Name = "lstBox";
            this.lstBox.Size = new System.Drawing.Size(226, 160);
            this.lstBox.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 271);
            this.Controls.Add(this.lstBox);
            this.Controls.Add(this.lblUpdate);
            this.Controls.Add(this.progBar);
            this.Controls.Add(this.flpDestination);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.btnConvert);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YT 2 MP3";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Form1_Load);
            this.flpDestination.ResumeLayout(false);
            this.flpDestination.PerformLayout();
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
    }
}

