using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SLauncher
{
    public partial class SSettings : Form
    {
        public int rheight, rwidth,j;
        public string setadd,ph,pw,pf,pvf,gheight,gwidth,gres;
        public SSettings()
        {
            InitializeComponent();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int k;

            if (comboBox1.Text != gres)
            {
                string[] arrLine = File.ReadAllLines(setadd);
                arrLine[j + 2] = arrLine[j + 2].Replace("Height = " + ph, "Height = "+ comboBox1.Text.Substring(comboBox1.Text.Length - 4));
                arrLine[j + 5] = arrLine[j + 5].Replace("Width = " + pw, "Width = " + comboBox1.Text.Substring(0,4));
                File.WriteAllLines(setadd, arrLine);
            }

            if (comboBox2.SelectedIndex == 0)
            {
                string[] arrLine = File.ReadAllLines(setadd);
                arrLine[j + 1] = arrLine[j + 1].Replace("FullScreen = "+pf, "FullScreen = true");
                arrLine[j + 3] = arrLine[j + 3].Replace("VirtualFullScreen = " + pvf, "VirtualFullScreen = false");
                File.WriteAllLines(setadd, arrLine);
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                string[] arrLine = File.ReadAllLines(setadd);
                arrLine[j + 1] = arrLine[j + 1].Replace("FullScreen = " + pf, "FullScreen = false");
                arrLine[j + 3] = arrLine[j + 3].Replace("VirtualFullScreen = " + pvf, "VirtualFullScreen = true");
                File.WriteAllLines(setadd, arrLine);
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                string[] arrLine = File.ReadAllLines(setadd);
                arrLine[j + 1] = arrLine[j + 1].Replace("FullScreen = " + pf, "FullScreen = false");
                arrLine[j + 3] = arrLine[j + 3].Replace("VirtualFullScreen = " + pvf, "VirtualFullScreen = false");
                File.WriteAllLines(setadd, arrLine);
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            setadd = Properties.Settings.Default.settingdirectory;

            if (setadd == "" || setadd == null)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "please select your pso2 settings file (\"user.pso2\")";
                ofd.ShowDialog();
                Properties.Settings.Default.settingdirectory = ofd.FileName;
                setadd = ofd.FileName;
                Properties.Settings.Default.Save();
            }

            int i = 0;
            
            StreamReader sr = new StreamReader(setadd);
            setadd = Properties.Settings.Default.settingdirectory;
            while(i < 999)
            {
                
                if (sr.ReadLine().ToString().Contains("Windows = {"))
                {
                    j = i;
                    break;
                }
                i += 1;
            }

            sr.Close();

            gheight= File.ReadLines(setadd).Skip(j+2).Take(1).Last();
            gheight = Regex.Match(gheight, @"[0-9]+").Value;
            gwidth = File.ReadLines(setadd).Skip(j + 5).Take(1).Last();
            gwidth = Regex.Match(gwidth, @"[0-9]+").Value;
            gres = gwidth + " x " + gheight;
            ph = gheight;
            pw = gwidth;
            int resi = 0;
            while(resi < 19)
            {
                comboBox1.SelectedIndex = resi;
                if (comboBox1.Text == gres)
                {
                    break;
                }
                resi += 1;
            }

            string Fscreen = File.ReadLines(setadd).Skip(j + 1).Take(1).Last();
            string vscreen = File.ReadLines(setadd).Skip(j + 3).Take(1).Last();
            

            if (Fscreen.Contains("true") && vscreen.Contains("false"))
            {
                comboBox2.SelectedIndex = 0;
                pf = "true";
                pvf = "false";
            } else if (Fscreen.Contains("false") && vscreen.Contains("true"))
            {
                comboBox2.SelectedIndex = 1;
                pf = "false";
                pvf = "true";
            }
            else if (Fscreen.Contains("false") && vscreen.Contains("false"))
            {
                comboBox2.SelectedIndex = 2;
                pf = "false";
                pvf = "false";
            }
            else
            {
                MessageBox.Show("Error reading windows style", "error");
            }
            
            


        }

    }
}
