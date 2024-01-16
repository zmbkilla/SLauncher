using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLauncher
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

           FolderBrowserDialog gdir = new FolderBrowserDialog();
            gdir.Description = "select the pso2_bin folder";
            gdir.ShowDialog();
            gameD.Text = gdir.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process startP = new Process();
            //startP.StartInfo.Verb = "runas";
            startP.StartInfo.WorkingDirectory = gameD.Text;
            startP.StartInfo.FileName = "pso2.exe";
            startP.StartInfo.Arguments = "-pson2 -pson2";
            
            startP.Start();
        }
    }
}
