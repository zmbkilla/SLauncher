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
        //j = resolution, bs = basic
        public int rheight, rwidth,j,bs;
        public string setadd,ph,pw,pf,pvf,gheight,gwidth,gres,movbl, vl1,vl2,vl3,vl4;

        private void button1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog gdir = new FolderBrowserDialog();
            gdir.Description = "select the folder containing the pso2.exe";
            gdir.ShowDialog();
            //gameD.Text = gdir.SelectedPath;
            //Properties.Settings.Default.gamedirectory = gameD.Text;
            Properties.Settings.Default.Save();
        }

        private void settingbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (settingbox.SelectedIndex == 0)
            {
                panel2.SendToBack();
                panel2.Enabled = false;
                panel1.Enabled = true;
                
            }
            else if (settingbox.SelectedIndex == 1)
            {
                panel1.SendToBack();
                panel1.Enabled = false;
                panel2.Enabled = true;
            }
        }

        private void SSettings_Leave(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.AutoSize = false;
            fm.Width = 800;
            fm.Height = 500;
        }

        private void trackBar4_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown4.Value = trackBar4.Value;
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown3.Value = trackBar1.Value;
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2.Value = trackBar2.Value;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Value = trackBar1.Value;
        }

        public string[] varl = new string[100];
        public int[] pos = new int[100];


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

            this.AutoSize = false;
            form1.AutoSize = false;
            this.Width = 800;
            this.Height = 500;
            
            //form1.Size = new Size(800, 450);
            
            
            
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //save resolution
                if (comboBox1.Text != gres)
                {
                    //gheight = gheight;
                    //gwidth = gwidth;

                    string[] arrLine = File.ReadAllLines(setadd);
                    arrLine[pos[99]] = arrLine[pos[99]].Replace(ph,comboBox1.Text.Substring(comboBox1.Text.Length - 4, 4));
                    File.WriteAllLines(setadd, arrLine);
                    arrLine[pos[98]] = arrLine[pos[98]].Replace(pw,comboBox1.Text.Substring(0, 4));
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
                    arrLine[pos[97]] = arrLine[pos[97]].Replace("FullScreen = " + pf, "FullScreen = true");
                    arrLine[pos[96]] = arrLine[pos[96]].Replace("VirtualFullScreen = " + pvf, "VirtualFullScreen = false");
                    File.WriteAllLines(setadd, arrLine);
                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    string[] arrLine = File.ReadAllLines(setadd);
                    arrLine[pos[97]] = arrLine[pos[97]].Replace("FullScreen = " + pf, "FullScreen = false");
                    arrLine[pos[96]] = arrLine[pos[96]].Replace("VirtualFullScreen = " + pvf, "VirtualFullScreen = true");
                    File.WriteAllLines(setadd, arrLine);
                }
                else if (comboBox2.SelectedIndex == 2)
                {
                    string[] arrLine = File.ReadAllLines(setadd);
                    arrLine[pos[97]] = arrLine[pos[97]].Replace("FullScreen = " + pf, "FullScreen = false");
                    arrLine[pos[96]] = arrLine[pos[96]].Replace("VirtualFullScreen = " + pvf, "VirtualFullScreen = false");
                    File.WriteAllLines(setadd, arrLine);
                }
                else
                {
                    MessageBox.Show("Error saving Windows style. Please check the user settings file", "Error");
                }

                //save basic

                if(checkBox1.Checked == true)
                {
                    string[] arrLine = File.ReadAllLines(setadd);
                    arrLine[pos[0]] = arrLine[pos[0]].Replace("MoviePlay = " + movbl, "MoviePlay = true");
                    File.WriteAllLines(setadd, arrLine);
                }
                else
                {
                    string[] arrLine = File.ReadAllLines(setadd);
                    arrLine[pos[0]] = arrLine[pos[0]].Replace("MoviePlay = " + movbl, "MoviePlay = false");
                    File.WriteAllLines(setadd, arrLine);
                }

                //save sound
                string[] sLine = File.ReadAllLines(setadd);
                sLine[pos[1]] = sLine[pos[1]].Replace("Bgm = " + vl1, "Bgm = " + numericUpDown1.Value.ToString());
                sLine[pos[2]] = sLine[pos[2]].Replace("Voice = " + vl2, "Voice = " + numericUpDown2.Value.ToString());
                sLine[pos[3]] = sLine[pos[3]].Replace("Movie = " + vl3, "Movie = " + numericUpDown3.Value.ToString());
                sLine[pos[4]] = sLine[pos[4]].Replace("Se = " + vl4, "Se = " + numericUpDown4.Value.ToString());
                File.WriteAllLines(setadd, sLine);



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
                    DialogResult dr = MessageBox.Show("Would you like to select an existing user.pso2 file?", "Donfirmation", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        OpenFileDialog ofd = new OpenFileDialog();
                        ofd.Title = "please select your pso2 settings file (\"user.pso2\")";
                        ofd.ShowDialog();
                        if (ofd.FileName != "")
                        {
                            Properties.Settings.Default.settingdirectory = ofd.FileName;
                            setadd = ofd.FileName;
                            Properties.Settings.Default.Save();
                        }
                        
                    }
                    else if (dr == DialogResult.No)
                    {
                        MessageBox.Show("Creating settings file in User's folder");
                        string runningpath = System.AppDomain.CurrentDomain.BaseDirectory;
                        string createdir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SEGA\\PHANTASYSTARONLINE2\\";
                        string createfil = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SEGA\\PHANTASYSTARONLINE2\\user.pso2";
                        Directory.CreateDirectory(createdir);
                        //File.Copy(string.Format("{0}Resources\\user.pso2", Path.GetFullPath(Path.Combine(runningpath, @"..\..\"))), createfil, true);
                        File.Copy(runningpath+"\\Resources\\default",createfil,true);
                        Properties.Settings.Default.settingdirectory = createfil;
                        setadd = createfil;
                        Properties.Settings.Default.Save();
                    }
                    
                    
                    
                    Set_val = true;
                }
                string val_check="";
                string rl;
                int i = 0;
                
                if (Set_val == true && setadd != "")
                {

                    StreamReader sr = new StreamReader(setadd);
                    setadd = Properties.Settings.Default.settingdirectory;
                    while (i < 999)
                    {
                        rl = sr.ReadLine();
                        //checks if valid setting file by reading first line
                        if (i == 0)
                        {

                            val_check = rl;
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
                        //check basic settings
                        if (rl.Trim() == "MoviePlay = true,")
                        {

                            pos[0] = i;
                            
                            
                        }

                        //check for sound settings
                        if ( rl.Trim() == "Sound = {")
                        {
                            pos[1] = i+6;
                            pos[2] = i + 7;
                            pos[3] = i+8;
                            pos[4] = i+ 9;
                        }


                        //checks for resolution related settings
                        if (rl.Trim() == "Windows = {")
                        {
                            //height
                            pos[99] = i+2;
                            
                            //width
                            pos[98] = i+5;
                            //fscreen
                            pos[97] = i + 1;
                            //vscreen
                            pos[96] = i + 3;
                            
                        }
                        if (rl.Trim() == "System = {")
                        {
                            
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
            varl[0] = File.ReadLines(setadd).Skip(pos[0]).Take(1).Last();
            varl[0].Trim();
            varl[99] = File.ReadLines(setadd).Skip(pos[99]).Take(1).Last();
            varl[99] = Regex.Match(varl[99], @"[0-9]+").Value;
            varl[98] = File.ReadLines(setadd).Skip(pos[98]).Take(1).Last();
            varl[98] = Regex.Match(varl[98], @"[0-9]+").Value;
            gres = varl[98] + " x " + varl[99];
            ph = varl[99];
            pw = varl[98];

            //code for basic settings checkbox

            if (varl[0].Contains("true"))
            {
                checkBox1.Checked = true;
                movbl = "true";
            }
            else
            {
                movbl = "false";
            }
            //code for sound
            varl[1] = File.ReadLines(setadd).Skip(pos[1]).Take(1).Last();
            varl[1] = Regex.Match(varl[1], @"[0-9]+").Value;
            varl[2] = File.ReadLines(setadd).Skip(pos[2]).Take(1).Last();
            varl[2] = Regex.Match(varl[2], @"[0-9]+").Value;
            varl[3] = File.ReadLines(setadd).Skip(pos[3]).Take(1).Last();
            varl[3] = Regex.Match(varl[3], @"[0-9]+").Value;
            varl[4] = File.ReadLines(setadd).Skip(pos[4]).Take(1).Last();
            varl[4] = Regex.Match(varl[4], @"[0-9]+").Value;

            trackBar1.Value = Convert.ToInt16(varl[1]);
            numericUpDown1.Value = trackBar1.Value;
            vl1 = numericUpDown1.Value.ToString();
            trackBar2.Value = Convert.ToInt16(varl[2]);
            numericUpDown2.Value = trackBar2.Value;
            vl2 = numericUpDown2.Value.ToString();
            trackBar3.Value = Convert.ToInt16(varl[3]);
            numericUpDown3.Value = trackBar3.Value;
            vl3 = numericUpDown3.Value.ToString();
            trackBar4.Value = Convert.ToInt16(varl[4]);
            numericUpDown4.Value = trackBar4.Value;
            vl4 = numericUpDown4.Value.ToString();
            //code to set combobox value for resolution
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

            string Fscreen = File.ReadLines(setadd).Skip(pos[97]).Take(1).Last();
            string vscreen = File.ReadLines(setadd).Skip(pos[96]).Take(1).Last();
            

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

            

            //load initial setting panel
            panel2.SendToBack();
            panel2.Enabled = false;
            gameD.Text = Properties.Settings.Default.gamedirectory;
            settingbox.SelectedIndex = 0;
            
        }

    }
}
