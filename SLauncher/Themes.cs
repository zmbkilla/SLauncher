using SLauncher.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SLauncher
{
    public partial class Themes : Form
    {
        public Image defaultimg;
        public int TCount;
        public int[] tID;
        public string[] tBG,tDesc,tSetting;
      
        public Themes()
        {
            InitializeComponent();
            
            //load default template
            defaultimg = Resources.fbg;
            imageList1.Images.Add(defaultimg);
            //get theme list
            string[] dirs = Directory.GetDirectories(System.AppDomain.CurrentDomain.BaseDirectory + "\\Themes", "*", SearchOption.TopDirectoryOnly);
            TCount = 1;
            tBG = new string[dirs.Length];
            tDesc = new string[dirs.Length];
            tSetting = new string[dirs.Length];
            try
            {
                foreach (string dir in dirs)
                {
                    //tID[TCount - 1] = TCount;
                    tBG[TCount - 1] = dirs[TCount - 1] + "\\BG.png";
                    tDesc[TCount - 1] = dirs[TCount - 1] + "\\Description.txt";
                    string tname = new DirectoryInfo(dirs[dirs.Length - 1]).Name;
                    tSetting[TCount - 1] = dirs[TCount - 1] + "\\settings.txt";
                    checkedListBox1.Items.Add(tname);
                }

            }catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Error: Invalid setting");
            }
            else if (checkedListBox1.SelectedIndex == 0)
            {

                bool set = false;
               
                Properties.Settings.Default.UsingTheme = false;
                Properties.Settings.Default.ShowLogo = false;
                Properties.Settings.Default.Save();


                var mainForm = Application.OpenForms.OfType<Form1>().Single();
                mainForm.SettingTheme(set);
                mainForm.Changetheme(Resources.fbg);
                
            }
            else
            {
                
                byte[] bufferimage = new byte[4096];
                bufferimage = File.ReadAllBytes(tBG[checkedListBox1.SelectedIndex - 1]);
                string encoded = System.Convert.ToBase64String(bufferimage);
                Properties.Settings.Default.CustomTheme = encoded;
               
                Properties.Settings.Default.UsingTheme = true;
                Properties.Settings.Default.Save();
                var mainForm = Application.OpenForms.OfType<Form1>().Single();
                mainForm.Changetheme(pictureBox1.Image);
                string setfil = File.ReadAllText(tSetting[checkedListBox1.SelectedIndex - 1]);
                //check settings file
                if (File.ReadAllText(tSetting[checkedListBox1.SelectedIndex - 1]) != "")
                {
                    //logo setting
                    if (setfil.Contains("logo=false"))
                    {
                        Properties.Settings.Default.ShowLogo = false;
                        Properties.Settings.Default.Save();
                        mainForm.SettingTheme(Properties.Settings.Default.ShowLogo);

                    }else if (setfil.Contains("logo=true"))
                    {
                        Properties.Settings.Default.ShowLogo = true;
                        Properties.Settings.Default.Save();
                        mainForm.SettingTheme(Properties.Settings.Default.ShowLogo);
                    }
                }
                else
                {
                    MessageBox.Show("Theme Updated");
                }
                //show logo

            }
        }

        private void Themes_Load(object sender, EventArgs e)
        {
            
            
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < checkedListBox1.Items.Count; ++ix)
                if (ix != e.Index) checkedListBox1.SetItemChecked(ix, false);
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == 0)
            {
                pictureBox1.Image = defaultimg;
                label1.Text = "The Default background image for the Starlight Launcher";
            }
            else if(checkedListBox1.SelectedIndex != 0) 
            {
                int sindex = checkedListBox1.SelectedIndex;
                Image loadt = Image.FromFile(tBG[sindex-1]);
                pictureBox1.Image = loadt;
                label1.Text = File.ReadAllText(tDesc[sindex-1]);
            }
        }
    }
}
