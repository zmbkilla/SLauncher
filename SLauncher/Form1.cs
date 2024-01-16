using SLauncher.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SevenZipExtractor;

namespace SLauncher
{
    public partial class Form1 : Form
    {

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }



        public Form1()
        {
            InitializeComponent();
            if (gameD.Text == "")
            {
                gameD.Text = Properties.Settings.Default.gamedirectory;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

           FolderBrowserDialog gdir = new FolderBrowserDialog();
            gdir.Description = "select the folder containing the pso2.exe";
            gdir.ShowDialog();
            gameD.Text = gdir.SelectedPath;
            Properties.Settings.Default.gamedirectory = gameD.Text;
            Properties.Settings.Default.Save();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (File.Exists(gameD.Text + "\\pso2.exe"))
                {
                Process startP = new Process();
                //startP.StartInfo.Verb = "runas";
                startP.StartInfo.WorkingDirectory = gameD.Text;
                startP.StartInfo.FileName = "pso2.exe";
                startP.StartInfo.Arguments = "-pson2 -pson2";

                startP.Start();
            }
            else
            {
                MessageBox.Show("The directory does not contain the pso2 executable","Error");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            
            SSettings sSettings = new SSettings();
            sSettings.TopLevel = false;
           


            this.Controls.Add(sSettings);
            sSettings.Show();
            this.Controls.SetChildIndex(sSettings, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gameD.Text = Properties.Settings.Default.gamedirectory;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button5_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog Gamedown = new FolderBrowserDialog();
            Gamedown.Description = "Select the folder you want to download the game";
            Gamedown.ShowDialog();

            string filepath = Gamedown.SelectedPath;
            WebClient webClient = new WebClient();


            webClient.DownloadProgressChanged += (s, p) =>
            {
                progressBar1.Visible = true;
                progressBar1.Value = p.ProgressPercentage;
                Console1.Text = (Convert.ToString(p.UserState) + "    downloaded "+p.BytesReceived+ " of "+p.TotalBytesToReceive+" bytes. "+p.ProgressPercentage+" % complete...");
                button4.Enabled = false;

            };
            webClient.DownloadFileCompleted += (s, p) =>
            {
                progressBar1.Visible = false;
                button4.Enabled = true;
                // any other code to process the file

                using (ArchiveFile archiveFile = new ArchiveFile(filepath+"\\game.7z"))
                {
                    archiveFile.Extract("Output"); // extract all
                }
                MessageBox.Show("Download Complete!", "Notification");
                var delres = MessageBox.Show("Do you want to delete zipped download?", "Confirmation", MessageBoxButtons.YesNo);
                if(delres == DialogResult.Yes)
                {
                    File.Delete(@filepath + "\\game.7z");
                }
            };

            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback4);
            webClient.DownloadFileAsync(new Uri("https://onedrive.live.com/download?resid=ADE1D97E92AEC8BE%21403144&authkey=!AN7Xv7If2YMHH88"), @filepath + "\\game.7z");

            

        }

        public static void DownloadProgressCallback4(object sender, DownloadProgressChangedEventArgs e)
        {
            
           
            

            //MessageBox.Show("Downloading " + e.BytesReceived + " of " + e.TotalBytesToReceive + ". " + e.ProgressPercentage)

            // Displays the operation identifier, and the transfer progress.
            Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...",
                (string)e.UserState,
                e.BytesReceived,
                e.TotalBytesToReceive,
               e.ProgressPercentage);
        }


    }
}
