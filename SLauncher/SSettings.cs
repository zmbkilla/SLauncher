using Newtonsoft.Json;
using SLauncher.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
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
        public string[] scalex = new string[9], scaley = new string[9];
        public bool initializing = true;
        public dynamic settingfil = JsonConvert.DeserializeObject(File.ReadAllText(Directory.GetCurrentDirectory()+"\\json\\settings.json"));


        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x84)
        //    {
        //        m.Result = new IntPtr(-1);
        //        return;
        //    }
        //    base.WndProc(ref m);
        //}


        private void runcommandmouse()
        {
            
        }

        private void SSettings_MouseDown(object sender, MouseEventArgs e)
        {


            Form1 fm1 = (Form1)Application.OpenForms[0];
            
            fm1.Form1_MouseDown(sender,e);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true && initializing == false)
            {
                ReplaceOps();
                Properties.Settings.Default.SetOPCRID = true;
                
                Properties.Settings.Default.Save();
                Properties.Settings.Default.SetOPCRIDID = CRIDcmbbox.SelectedIndex;
                Properties.Settings.Default.Save();
            }
            else if (checkBox3.Checked == false && initializing == false)
            {
                DefaultOps();
                Properties.Settings.Default.SetOPCRID = false;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.SetOPCRIDID = 6;
                Properties.Settings.Default.Save();
            }
        }

        private void InitializeOps()
        {
            string datalocation = Properties.Settings.Default.gamedirectory + "\\data\\win32";

            PSO2Downloader downloader = new PSO2Downloader();
            PSO2Files psf = new PSO2Files();
            string[] files = psf.OpeningCRID;
            File.WriteAllLines(Directory.GetCurrentDirectory() + "\\OPS.txt", files);
            string[] args = new string[1];
            args[0] = Directory.GetCurrentDirectory() + "\\OPS.txt";
            bool checkdone = false;
            for (int i = 0; i < files.Length; i++)
            {
                if (!File.Exists(Directory.GetCurrentDirectory() + "\\patchData\\" + files[i]))
                {
                    args[0] = files[i];
                    downloader.Main(args);
                }

                if (i == 6)
                {
                    checkdone = true;
                }
            }

            if (!checkdone)
            {
                downloader.Main(args);
            }
            while(File.Exists(Directory.GetCurrentDirectory() + "\\OPS.txt"))
            {
                File.Delete(Directory.GetCurrentDirectory() + "\\OPS.txt");
            }
            
            
            
            
            
        }

        private void ReplaceOps()
        {
            string datalocation = Properties.Settings.Default.gamedirectory+"\\";

            PSO2Downloader downloader = new PSO2Downloader();
            PSO2Files psf = new PSO2Files();
            string[] files = psf.OpeningCRID;
            string temp = "";

            switch (CRIDcmbbox.SelectedIndex)
            {
                case 0: temp = datalocation + files[0] + "_temp"; File.Move(datalocation + files[6], temp); File.Move(datalocation + files[0], datalocation + files[6]); File.Move(temp, datalocation + files[0]);
                    break;
                case 1:
                     temp = datalocation + files[1] + "_temp"; File.Move(datalocation + files[6], temp); File.Move(datalocation + files[1], datalocation + files[6]); File.Move(temp, datalocation + files[1]);
                    break;
                case 2:
                    temp = datalocation + files[2] + "_temp"; File.Move(datalocation + files[6], temp); File.Move(datalocation + files[2], datalocation + files[6]); File.Move(temp, datalocation + files[2]);
                    break;
                case 3:
                    temp = datalocation + files[3] + "_temp"; File.Move(datalocation + files[6], temp); File.Move(datalocation + files[3], datalocation + files[6]); File.Move(temp, datalocation + files[3]);
                    break;
                case 4:
                    temp = datalocation + files[4] + "_temp"; File.Move(datalocation + files[6], temp); File.Move(datalocation + files[4], datalocation + files[6]); File.Move(temp, datalocation + files[4]);
                    break;
                case 5:
                    temp = datalocation + files[5] + "_temp"; File.Move(datalocation + files[6], temp); File.Move(datalocation + files[5], datalocation + files[6]); File.Move(temp, datalocation + files[5]);
                    break;
                case 6:
                    temp = datalocation + files[Properties.Settings.Default.SetOPCRIDID] + "_temp"; File.Move(datalocation + files[Properties.Settings.Default.SetOPCRIDID], temp); File.Move(datalocation + files[6], datalocation + files[Properties.Settings.Default.SetOPCRIDID]); File.Move(temp, datalocation + files[6]);
                    break;
                
            }


            //for (int i = 0;i<files.Length;i++)
            //{
            //    ////File.Copy(Directory.GetCurrentDirectory() + "\\patchData\\" + files[CRIDcmbbox.SelectedIndex], datalocation +"\\"+ files[i],true);
            //    //if (CRIDcmbbox.SelectedIndex != i)
            //    //{
            //    //    File.Move(datalocation + "//" + files[CRIDcmbbox.SelectedIndex], datalocation + "//" + files[i]);
            //    //}
            //
            //    
            //    
            //}
        }

        private void DefaultOps()
        {
            string datalocation = Properties.Settings.Default.gamedirectory;

            PSO2Downloader downloader = new PSO2Downloader();
            PSO2Files psf = new PSO2Files();
            string[] files = psf.OpeningCRID;
            for (int i = 0; i < files.Length; i++)
            {
                File.Copy(Directory.GetCurrentDirectory() + "\\patchData\\" + files[i], datalocation +"\\"+ files[i], true);
            }






        }

        private void CRIDcmbbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initializing == false && checkBox3.Checked == true)
            {
                ReplaceOps();
                Properties.Settings.Default.SetOPCRIDID = CRIDcmbbox.SelectedIndex;
                Properties.Settings.Default.Save();
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            Form1 fm1 = (Form1)Application.OpenForms[0];
            
            fm1.Form1_MouseDown(sender, e);
        }

        private void ctheme_Click(object sender, EventArgs e)
        {
            Themes ct = new Themes();
            ct.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog gdir = new FolderBrowserDialog();
            gdir.Description = "select the folder containing the pso2.exe";
            gdir.ShowDialog();
            gameD.Text = gdir.SelectedPath;
            Properties.Settings.Default.gamedirectory = gdir.SelectedPath;
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
            //Form1 form1 = (Form1)Application.OpenForms[0];

            Form1 form1 = new Form1();
            this.AutoSize = false;
            form1.AutoSize = false;
            this.Width = 800;
            this.Height = 500;
            //form1.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((Left)));

            //form1.Size = new Size(800, 450);


            initializing = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            this.Close();
            initializing = true;
            return;

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

                if (checkBox2.Checked == true)
                {
                    string[] arrLine = File.ReadAllLines(setadd);
                    arrLine[pos[5]] = arrLine[pos[5]].Replace("PadBackgroundUse = " + varl[5], "PadBackgroundUse = true");
                    File.WriteAllLines(setadd, arrLine);
                }
                else
                {
                    string[] arrLine = File.ReadAllLines(setadd);
                    arrLine[pos[0]] = arrLine[pos[0]].Replace("PadBackgroundUse = " + varl[5], "PadBackgroundUse = false");
                    File.WriteAllLines(setadd, arrLine);
                }



                //save sound
                string[] sLine = File.ReadAllLines(setadd);
                sLine[pos[1]] = sLine[pos[1]].Replace("Bgm = " + vl1, "Bgm = " + numericUpDown1.Value.ToString());
                sLine[pos[2]] = sLine[pos[2]].Replace("Voice = " + vl2, "Voice = " + numericUpDown2.Value.ToString());
                sLine[pos[3]] = sLine[pos[3]].Replace("Movie = " + vl3, "Movie = " + numericUpDown3.Value.ToString());
                sLine[pos[4]] = sLine[pos[4]].Replace("Se = " + vl4, "Se = " + numericUpDown4.Value.ToString());
                
                //save graphics
                sLine[pos[95]] = sLine[pos[95]].Replace("ShaderLevel = " + varl[95].ToString(), "ShaderLevel = " + shaderlvl.Value.ToString());
                sLine[pos[94]] = sLine[pos[94]].Replace("TextureResolution = " + varl[94].ToString(), "TextureResolution = " + textres.Value.ToString());
                sLine[pos[93]] = sLine[pos[93]].Replace("DrawFilter = " + varl[93].ToString(), "DrawFilter = " + Dfilter.Value.ToString());
                sLine[pos[92]] = sLine[pos[92]].Replace("ShaderQuality = " + varl[92].ToString(), "ShaderQuality = " + comboBox3.Text);
                sLine[pos[91]] = sLine[pos[91]].Replace("AnisotropicFiltering = " + varl[91].ToString(), "AnisotropicFiltering = " + comboBox4.SelectedIndex.ToString());

                //save Scaling
                if(comboBox6.Text != "Default")
                {
                    sLine[pos[90]] = sLine[pos[90]].Replace("PreferredScale = " + varl[90].ToString(), "PreferredScale = " + comboBox6.Text);
                    if(comboBox6.Text == "0.4")
                    {
                        sLine[pos[89]] = sLine[pos[89]].Replace("Y = " + varl[89].ToString(), "Y = " + scaley[1]);
                        sLine[pos[88]] = sLine[pos[88]].Replace("X = " + varl[88].ToString(), "X = " + scalex[1]);
                    }
                    if (comboBox6.Text == "0.6")
                    {
                        sLine[pos[89]] = sLine[pos[89]].Replace("Y = " + varl[89].ToString(), "Y = " + scaley[2]);
                        sLine[pos[88]] = sLine[pos[88]].Replace("X = " + varl[88].ToString(), "X = " + scalex[2]);

                    }
                    if (comboBox6.Text == "0.8")
                    {
                        sLine[pos[89]] = sLine[pos[89]].Replace("Y = " + varl[89].ToString(), "Y = " + scaley[3]);
                        sLine[pos[88]] = sLine[pos[88]].Replace("X = " + varl[88].ToString(), "X = " + scalex[3]);
                    }
                    if (comboBox6.Text == "1.4")
                    {
                        sLine[pos[89]] = sLine[pos[89]].Replace("Y = " + varl[89].ToString(), "Y = " + scaley[4]);
                        sLine[pos[88]] = sLine[pos[88]].Replace("X = " + varl[88].ToString(), "X = " + scalex[4]);
                    }
                    if (comboBox6.Text == "1.8")
                    {
                        sLine[pos[89]] = sLine[pos[89]].Replace("Y = " + varl[89].ToString(), "Y = " + scaley[5]);
                        sLine[pos[88]] = sLine[pos[88]].Replace("X = " + varl[88].ToString(), "X = " + scalex[5]);
                    }
                    if (comboBox6.Text == "2.2")
                    {
                        sLine[pos[89]] = sLine[pos[89]].Replace("Y = " + varl[89].ToString(), "Y = " + scaley[6]);
                        sLine[pos[88]] = sLine[pos[88]].Replace("X = " + varl[88].ToString(), "X = " + scalex[6]);
                    }
                }else if (comboBox6.Text == "Default")
                {
                    sLine[pos[90]] = sLine[pos[90]].Replace("PreferredScale = " + varl[90].ToString(), "PreferredScale = " + "1.0");
                    sLine[pos[89]] = sLine[pos[89]].Replace("Y = " + varl[89].ToString(), "Y = " + scaley[0]);
                    sLine[pos[88]] = sLine[pos[88]].Replace("X = " + varl[88].ToString(), "X = " + scalex[0]);
                }


                //save simple
                sLine[pos[86]] = sLine[pos[86]].Replace("DrawLevel = " + varl[86].ToString(), "DrawLevel = " + trackBar5.Value.ToString());









                File.WriteAllLines(setadd, sLine);

                MessageBox.Show("Settings Saved", "Complete");
                this.Close();

            }
            catch (Exception l)
            {
                MessageBox.Show("Error: " + l, "error");
            }
        
        }

        protected override System.Drawing.Point ScrollToControl(System.Windows.Forms.Control activeControl)
        {
            // Returning the current location prevents the panel from
            // scrolling to the active control when the panel loses and regains focus
            return this.DisplayRectangle.Location;
        }
        private void Settings_Load(object sender, EventArgs e)
        {
            dynamic jset = JsonConvert.DeserializeObject(File.ReadAllText(Directory.GetCurrentDirectory() + "\\json\\settings.json"));
            
            setadd = jset["settingdirectory"];
            bool load_fin = false;
            bool Set_val = true;

            bool cridbool;
            int cridID;
            cridbool = Properties.Settings.Default.SetOPCRID;
            cridID = Properties.Settings.Default.SetOPCRIDID;

            InitializeOps();
            
            if (Properties.Settings.Default.SetOPCRID == true)
            {
                checkBox3.Checked = true;
                CRIDcmbbox.SelectedIndex = cridID;
            }
            else
            {
                CRIDcmbbox.SelectedIndex = 0;
            }

            //load initial setting panel
            //panel2.SendToBack();
            //panel2.Enabled = false;
            string[] iniContent = File.ReadAllLines(setadd+"\\user.pso2");
            Dictionary<string,object> settingparameters = new Dictionary<string,object>();
            Dictionary<string, object> NoticeP = new Dictionary<string, object>();
            foreach (var lines in iniContent)
            {
                var trimmed = lines.ToString().Trim();
                if (trimmed.StartsWith("Ini")|| trimmed.StartsWith("Config"))
                {
                    continue;
                }
                Match match = Regex.Match(trimmed, @"(\w+) = (\S+),");
                if (trimmed.StartsWith("Motice"))
                {

                    if ((match.Success))
                    {
                        string parameter = match.Groups[1].Value;
                        string Pvalue = match.Groups[2].Value;
                        try
                        {
                            if (int.TryParse(Pvalue, out int Ivalue))
                            {
                                NoticeP.Add(parameter, Ivalue);
                            }
                            else if (bool.TryParse(Pvalue, out bool Bvalue))
                            {
                                NoticeP.Add(parameter, Bvalue);
                            }
                            else
                            {
                                NoticeP.Add(parameter, Pvalue);
                            }
                        }
                        catch
                        {

                        }
                    }

                }
                
                if ((match.Success))
                {
                    string parameter =  match.Groups[1].Value;
                    string Pvalue = match.Groups[2].Value;
                    try
                    {
                        if(int.TryParse(Pvalue,out int Ivalue))
                        {
                            settingparameters.Add(parameter, Ivalue);
                        }
                        else if(bool.TryParse(Pvalue,out bool Bvalue))
                        {
                            settingparameters.Add(parameter,Bvalue);
                        }
                        else
                        {
                            settingparameters.Add(parameter,Pvalue);
                        }
                    }
                    catch
                    {

                    }
                }
                
            }
            string dbg = "0";

            gameD.Text = settingfil["gamedirectory"];
            settingbox.SelectedIndex = 0;

            initializing = false;
            return;
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
                    int flength = File.ReadAllLines(setadd).Length;
                    while (i < flength)
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
                        if (rl.Trim().Contains("MoviePlay ="))
                        {

                            pos[0] = i;
                            
                            
                        }
                        if (rl.Trim().Contains ("PadBackgroundUse ="))
                        {

                            pos[5] = i;


                        }
                        //check for sound settings
                        if ( rl.Trim() == "Sound = {")
                        {
                            pos[1] = i+3;
                            pos[2] = i + 5;
                            pos[3] = i+6;
                            pos[4] = i+ 4;
                        }


                        //checks for resolution related settings
                        if (rl.Trim() == "Windows = {")
                        {
                            //height
                            pos[99] = i+3;
                            
                            //width
                            pos[98] = i+6;
                            //fscreen
                            pos[97] = i + 2;
                            //vscreen
                            pos[96] = i + 4;
                            
                        }
                        //check for graphics settings
                        if (rl.Trim().Contains("ShaderLevel ="))
                        {
                            //Shader level
                            pos[95] = i ;


                        }
                        if (rl.Trim().Contains("TextureResolution ="))
                        {
                            //Texture Quality
                            pos[94] = i;


                        }
                        if (rl.Trim().Contains("DrawFilter ="))
                        {
                            //Draw Filter
                            pos[93] = i;


                        }

                        if (rl.Trim().Contains("ShaderQuality ="))
                        {
                            //Shader Quality
                            pos[92] = i;


                        }
                        
                        if (rl.Trim().Contains("AnisotropicFiltering ="))
                        {
                            
                            pos[91] = i;


                        }

                        if (rl.Trim().Contains("PreferredScale ="))
                        {
                            //screen setting has bug that somehow moves paramters around. have to read them separately
                            pos[90] = i;


                        }
                        //ReferenceResolutionRE =
                        if (rl.Trim().Contains("ReferenceResolutionRE ="))
                        {
                            //related to pos[90]
                            //SizeY
                            pos[89] = i+1;
                            pos[88] = i + 2;


                        }
                        //UI size
                        if (rl.Trim().Contains("InterfaceSizeRE ="))
                        {
                            //related to pos[90]
                            
                            pos[87] = i;
                            


                        }
                        //simple
                        if (rl.Trim().Contains("Simple = {"))
                        {
                            //related to pos[90]
                            //Sizex
                            pos[86] = i+1;



                        }
                        //end
                        //if (rl.Trim() == "System = {")
                        //{
                        //    
                        //    break;
                        //}

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
            varl[5] = File.ReadLines(setadd).Skip(pos[5]).Take(1).Last();
            varl[5].Trim();
            varl[99] = File.ReadLines(setadd).Skip(pos[99]).Take(1).Last();
            varl[99] = Regex.Match(varl[99], @"[0-9]+").Value;
            varl[98] = File.ReadLines(setadd).Skip(pos[98]).Take(1).Last();
            varl[98] = Regex.Match(varl[98], @"[0-9]+").Value;
            gres = varl[98] + " x " + varl[99];
            ph = varl[99];
            pw = varl[98];

            scalex[0] = "1280";
            scaley[0] = "720";
            scalex[1] = "640";
            scaley[1] = "480";
            scalex[2] = "854";
            scaley[2] = "480";
            scalex[3] = "1024";
            scaley[3] = "768";
            scalex[4] = "1920";
            scaley[4] = "1080";
            scalex[5] = "2560";
            scaley[5] = "1440";
            scalex[6] = "3840";
            scaley[6] = "2160";

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
            //gamepad
            if (varl[5].Contains("true"))
            {
                checkBox2.Checked = true;
                
            }
            else
            {
                
            }


            //code for graphics
            //varl[95] = File.ReadLines(setadd).Skip(pos[95]).Take(1).Last();
            //varl[95] = Regex.Match(varl[95], @"[0-9]+").Value;
            //varl[94] = File.ReadLines(setadd).Skip(pos[94]).Take(1).Last();
            //varl[94] = Regex.Match(varl[94], @"[0-9]+").Value;
            //varl[93] = File.ReadLines(setadd).Skip(pos[93]).Take(1).Last();
            //varl[93] = Regex.Match(varl[93], @"[0-9]+").Value;
            //get true/false
            //varl[92] = File.ReadLines(setadd).Skip(pos[92]).Take(1).Last();
            //varl[92].Trim();
            //set control values
            //shaderlvl.Value = Convert.ToInt16(varl[95]);
            //textres.Value = Convert.ToInt16(varl[94]);
            //Dfilter.Value = Convert.ToInt16(varl[93]);
            //if (varl[92].Contains("true"))
            //{
            //    comboBox3.SelectedIndex = 0;
            //} else if (varl[92].Contains("false"))
            //{
            //    comboBox3.SelectedIndex = 1;
            //}
            //anistropic
            varl[91] = File.ReadLines(setadd).Skip(pos[91]).Take(1).Last();
            varl[91] = Regex.Match(varl[91], @"[0-9]+").Value;
            comboBox4.SelectedIndex = Convert.ToInt16(varl[91]);
            //ui size
            varl[87] = File.ReadLines(setadd).Skip(pos[87]).Take(1).Last();
            varl[87] = Regex.Match(varl[87], @"[0-9]+").Value;
            comboBox5.SelectedIndex = Convert.ToInt16(varl[87]);
            //window/text size
            varl[90] = File.ReadLines(setadd).Skip(pos[90]).Take(1).Last();
            varl[90] = Regex.Match(varl[90], @"([0-9])+.[0-9]").Value;
            varl[89] = File.ReadLines(setadd).Skip(pos[89]).Take(1).Last();
            varl[89] = Regex.Match(varl[89], @"[0-9]+").Value;
            varl[88] = File.ReadLines(setadd).Skip(pos[88]).Take(1).Last();
            varl[88] = Regex.Match(varl[88], @"[0-9]+").Value;
            bool found = false;
            int loop = 0;
            while (found != true)
            {
                comboBox6.SelectedIndex = loop;
                string Default = "1.0";
                
                if (comboBox6.Text == varl[90].ToString()|| loop == 3 && Default == varl[90].ToString())
                {
                    found = true;
                    break;
                }
                loop++;
            }
           
            // simple
            varl[86] = File.ReadLines(setadd).Skip(pos[86]).Take(1).Last();
            varl[86] = Regex.Match(varl[86], @"[0-9]+").Value;
            trackBar5.Value = Convert.ToInt16(varl[86]);
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

            

            
            
        }

    }
}
