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
    }
}
