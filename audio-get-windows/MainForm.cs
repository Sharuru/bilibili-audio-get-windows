using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Threading;

namespace AudioGet
{
    public partial class MainForm : Form
    {

        private readonly AppService appService = new AppService();

        public MainForm()
        {
            InitializeComponent();
            appService.Update += new AppService.UpdateStatus(UpdateUI);
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
                logTextBox.Text += ex.Message + Environment.NewLine;
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

        public void UpdateUI()
        {
            logTextBox.Text += appService.DownloadStatus + Environment.NewLine;
            logTextBox.SelectionStart = logTextBox.Text.Length;
            logTextBox.ScrollToCaret();
            progressBar.Value = appService.DownloadProgress;
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
                logTextBox.Clear();
            }
        }

    }

}

