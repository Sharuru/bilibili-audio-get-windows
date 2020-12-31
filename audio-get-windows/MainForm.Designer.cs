namespace AudioGet
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Add = new System.Windows.Forms.Button();
            this.Download = new System.Windows.Forms.Button();
            this.InputBox = new System.Windows.Forms.TextBox();
            this.DownloadList = new System.Windows.Forms.ListBox();
            this.RemoveSelected = new System.Windows.Forms.Button();
            this.RemoveAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // Add
            // 
            this.Add.BackColor = System.Drawing.Color.Gold;
            this.Add.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add.ForeColor = System.Drawing.Color.Black;
            this.Add.Location = new System.Drawing.Point(12, 35);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(340, 28);
            this.Add.TabIndex = 2;
            this.Add.Text = "Add Song / Playlist";
            this.Add.UseVisualStyleBackColor = false;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Download
            // 
            this.Download.BackColor = System.Drawing.Color.GreenYellow;
            this.Download.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Download.ForeColor = System.Drawing.Color.Black;
            this.Download.Location = new System.Drawing.Point(358, 35);
            this.Download.Name = "Download";
            this.Download.Size = new System.Drawing.Size(346, 28);
            this.Download.TabIndex = 3;
            this.Download.Text = "Download All";
            this.Download.UseVisualStyleBackColor = false;
            this.Download.Click += new System.EventHandler(this.Download_Click);
            // 
            // InputBox
            // 
            this.InputBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.InputBox.Location = new System.Drawing.Point(49, 6);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(923, 23);
            this.InputBox.TabIndex = 0;
            // 
            // DownloadList
            // 
            this.DownloadList.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadList.ForeColor = System.Drawing.Color.Gray;
            this.DownloadList.FormattingEnabled = true;
            this.DownloadList.ItemHeight = 17;
            this.DownloadList.Location = new System.Drawing.Point(12, 69);
            this.DownloadList.Name = "DownloadList";
            this.DownloadList.Size = new System.Drawing.Size(960, 174);
            this.DownloadList.TabIndex = 1;
            // 
            // RemoveSelected
            // 
            this.RemoveSelected.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.RemoveSelected.BackColor = System.Drawing.Color.Orange;
            this.RemoveSelected.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemoveSelected.ForeColor = System.Drawing.Color.Black;
            this.RemoveSelected.Location = new System.Drawing.Point(710, 35);
            this.RemoveSelected.Name = "RemoveSelected";
            this.RemoveSelected.Size = new System.Drawing.Size(128, 28);
            this.RemoveSelected.TabIndex = 4;
            this.RemoveSelected.Text = "Remove Selected";
            this.RemoveSelected.UseVisualStyleBackColor = false;
            this.RemoveSelected.Click += new System.EventHandler(this.RemoveSelected_Click);
            // 
            // RemoveAll
            // 
            this.RemoveAll.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.RemoveAll.BackColor = System.Drawing.Color.Red;
            this.RemoveAll.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemoveAll.ForeColor = System.Drawing.Color.Black;
            this.RemoveAll.Location = new System.Drawing.Point(844, 35);
            this.RemoveAll.Name = "RemoveAll";
            this.RemoveAll.Size = new System.Drawing.Size(128, 28);
            this.RemoveAll.TabIndex = 5;
            this.RemoveAll.Text = "Remove All";
            this.RemoveAll.UseVisualStyleBackColor = false;
            this.RemoveAll.Click += new System.EventHandler(this.RemoveAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Link";
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.logTextBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.logTextBox.ForeColor = System.Drawing.Color.Gray;
            this.logTextBox.Location = new System.Drawing.Point(12, 249);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTextBox.Size = new System.Drawing.Size(960, 270);
            this.logTextBox.TabIndex = 7;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 525);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(960, 10);
            this.progressBar.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(984, 543);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RemoveAll);
            this.Controls.Add(this.RemoveSelected);
            this.Controls.Add(this.DownloadList);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.Download);
            this.Controls.Add(this.Add);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AudioGet";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Download;
        private System.Windows.Forms.TextBox InputBox;
        private System.Windows.Forms.ListBox DownloadList;
        private System.Windows.Forms.Button RemoveSelected;
        private System.Windows.Forms.Button RemoveAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

