﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace AudioGet
{
    public partial class MainForm : Form
    {
        private readonly AppService appService = new AppService();
        private bool isMouseDown = false;
        private Point FormLocation;
        private Point mouseOffset;

        public MainForm()
        {
            InitializeComponent();
            IgnoreDPI();
            appService.Update += new AppService.UpdateStatus(UpdateLabel);
        }

        private async void Add_Click(object sender, EventArgs e)
        {
            try
            {
                string url = InputBox.Text;
                if (url.Contains("audio/au"))
                {
                    Regex re = new Regex(@"(audio/au)\d+", RegexOptions.Compiled);
                    string id = re.Match(url).ToString();
                    id = id.Replace("audio/au", "");


                    string name = "";
                    name = await appService.GetSingleAudioInfo(id);
                    if (name != "")
                    {
                        DownloadList.Items.Add(name);
                    }
                }

                else if (url.Contains("audio/am"))
                {
                    Regex re = new Regex(@"(audio/am)\d+", RegexOptions.Compiled);
                    string id = re.Match(url).ToString();
                    id = id.Replace("audio/am", "");

                    List<string> nameList = new List<string>();
                    nameList = await appService.GetPlayListInfo(id);
                    if (nameList.Count() != 0)
                    {
                        foreach (string name in nameList)
                        {
                            DownloadList.Items.Add(name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                StatusLabel.Text = ex.Message;
            }
            finally
            {
                InputBox.Clear();
            }
        }

        private async void Download_Click(object sender, EventArgs e)
        {
            if (DownloadList.Items.Count != 0)
            {
                Download.Enabled = false;
                string appBasePath = Application.StartupPath;
                await appService.DownloadAll(appBasePath);
                Download.Enabled = true;
            }

        }

        public void UpdateLabel()
        {
            StatusLabel.Text = appService.DownloadStatus;
        }

        private void RemoveSelected_Click(object sender, EventArgs e)
        {
            int index = DownloadList.SelectedIndex;
            if (DownloadList.Items.Count != 0 && index != -1)
            {

                DownloadList.Items.Remove(DownloadList.SelectedItem);
                appService.Remove(index);
            }
        }

        private void RemoveAll_Click(object sender, EventArgs e)
        {
            if (DownloadList.Items.Count != 0)
            {
                DownloadList.Items.Clear();
                appService.RemoveAll();
            }
        }

        public static int IgnoreDPI()
        {
            SetProcessDPIAware();
            IntPtr screenDC = GetDC(IntPtr.Zero);
            int dpi_x = GetDeviceCaps(screenDC, /*DeviceCap.*/LOGPIXELSX);
            int dpi_y = GetDeviceCaps(screenDC, /*DeviceCap.*/LOGPIXELSY);
            ReleaseDC(IntPtr.Zero, screenDC);

            return dpi_x;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr ptr);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(
        IntPtr hdc, // handle to DC
        int nIndex // index of capability
        );

        [DllImport("user32.dll")]
        internal static extern bool SetProcessDPIAware();

        const int LOGPIXELSX = 88;
        const int LOGPIXELSY = 90;

        private void Minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                FormLocation = this.Location;
                mouseOffset = Control.MousePosition;
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {

            int _x = 0;
            int _y = 0;
            if (isMouseDown)
            {
                Point pt = Control.MousePosition;
                _x = mouseOffset.X - pt.X;
                _y = mouseOffset.Y - pt.Y;

                this.Location = new Point(FormLocation.X - _x, FormLocation.Y - _y);
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void GithubBox_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/LXG-Shadow/BilibiliTools");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN")
            {
                Add.Text = "添加歌曲";
                Download.Text = "下载全部歌曲";
                RemoveSelected.Text = "移除选择歌曲";
                RemoveAll.Text = "移除全部歌曲";
                StatusLabel.Text = "开源协议: Apache 2.0";
            }
        }
    }
}

