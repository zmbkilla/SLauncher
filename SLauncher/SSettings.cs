using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4)
            {
                e.Handled = true;
                Application.Exit();
            }
        }

        public SSettings()
        {
            InitializeComponent();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            
            
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //save resolution
                if (comboBox1.Text != gres)
                {
                    gheight = gheight;
                    gwidth = gwidth;

                    string[] arrLine = File.ReadAllLines(setadd);
                    arrLine[j + 3] = arrLine[j + 2].Replace(ph,comboBox1.Text.Substring(comboBox1.Text.Length - 4, 4));
                    File.WriteAllLines(setadd, arrLine);
                    arrLine[j + 6] = arrLine[j + 5].Replace(pw,comboBox1.Text.Substring(0, 4));
                    File.WriteAllLines(setadd, arrLine);
                }
                else if(comboBox1.Text == gres)
                {

                }
                else
                {
                    MessageBox.Show("Error saving resoultion. Please check the user settings file", "Error");
                    
                }

                //save windows style

                if (comboBox2.SelectedIndex == 0)
                {
                    string[] arrLine = File.ReadAllLines(setadd);
                    arrLine[j + 2] = arrLine[j + 2].Replace("FullScreen = " + pf, "FullScreen = true");
                    arrLine[j + 4] = arrLine[j + 4].Replace("VirtualFullScreen = " + pvf, "VirtualFullScreen = false");
                    File.WriteAllLines(setadd, arrLine);
                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    string[] arrLine = File.ReadAllLines(setadd);
                    arrLine[j + 2] = arrLine[j + 2].Replace("FullScreen = " + pf, "FullScreen = false");
                    arrLine[j + 4] = arrLine[j + 4].Replace("VirtualFullScreen = " + pvf, "VirtualFullScreen = true");
                    File.WriteAllLines(setadd, arrLine);
                }
                else if (comboBox2.SelectedIndex == 2)
                {
                    string[] arrLine = File.ReadAllLines(setadd);
                    arrLine[j + 2] = arrLine[j + 2].Replace("FullScreen = " + pf, "FullScreen = false");
                    arrLine[j + 4] = arrLine[j + 4].Replace("VirtualFullScreen = " + pvf, "VirtualFullScreen = false");
                    File.WriteAllLines(setadd, arrLine);
                }
                else
                {
                    MessageBox.Show("Error saving Windows style. Please check the user settings file", "Error");
                }

                MessageBox.Show("Settings Saved", "Complete");
                this.Close();

            }
            catch (Exception l)
            {
                MessageBox.Show("Error: " + l, "error");
            }
        
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            setadd = Properties.Settings.Default.settingdirectory;
            bool load_fin = false;
            bool Set_val = true;
            while (load_fin != true) 
            {
                
                if (setadd == "" || setadd == null)
                {
                    Set_val = false;

                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Title = "please select your pso2 settings file (\"user.pso2\")";
                    ofd.ShowDialog();
                    Properties.Settings.Default.settingdirectory = ofd.FileName;
                    setadd = ofd.FileName;
                    Properties.Settings.Default.Save();
                    Set_val = true;
                }
                string val_check="";
                int i = 0;
                if (Set_val == true && setadd != "")
                {

                    StreamReader sr = new StreamReader(setadd);
                    setadd = Properties.Settings.Default.settingdirectory;
                    while (i < 999)
                    {
                        if (i == 0)
                        {
                            val_check = sr.ReadLine();
                            if (val_check != "Ini = {")
                            {
                                MessageBox.Show("Error: Invalid file. Please check if you have selected the user.pso2 file");
                                Properties.Settings.Default.settingdirectory = "";
                                Properties.Settings.Default.Save();
                                setadd = "";
                                Set_val = false;
                                break;
                            }

                        }

                        if (sr.ReadLine().ToString().Contains("Windows = {"))
                        {
                            j = i;
                            break;
                        }
                        i += 1;
                    }

                    sr.Close();

                }
                else
                {
                    MessageBox.Show("No valid file selected. please select the correct file");
                    
                }
                

                if (val_check == "Ini = {")
                {
                    load_fin = true;
                }

            }
            
           

            gheight= File.ReadLines(setadd).Skip(j+3).Take(1).Last();
            gheight = Regex.Match(gheight, @"[0-9]+").Value;
            gwidth = File.ReadLines(setadd).Skip(j + 6).Take(1).Last();
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

            string Fscreen = File.ReadLines(setadd).Skip(j + 2).Take(1).Last();
            string vscreen = File.ReadLines(setadd).Skip(j + 4).Take(1).Last();
            

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
