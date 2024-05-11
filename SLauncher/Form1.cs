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
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Contexts;

namespace SLauncher
{
    
    public partial class Form1 : Form
    {

        public double ver = 0.44;
        public int defx = 500, defy = 450;




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
            Environment.SetEnvironmentVariable("WEBVIEW2_ADDITIONAL_BROWSER_ARGUMENTS", "--autoplay-policy=no-user-gesture-required");
            InitializeComponent();

            //sets 
            if (Properties.Settings.Default.gamedirectory == "")
            {
                DialogResult dial = MessageBox.Show("This seems to be the first time you are running the launcher. Do you have an existing install of Starlight?","Notification",MessageBoxButtons.YesNo);
                if(dial == DialogResult.Yes)
                {
                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    MessageBox.Show("Please select you \"pso2_bin\" folder");
                    dialog.Description = "Select your pso2_bin folder";
                    bool check = false;
                    while(check != true)
                    {
                        dialog.ShowDialog();
                        if (dialog.SelectedPath != "" && File.Exists(dialog.SelectedPath + "\\pso2.exe"))
                        {
                            
                            Properties.Settings.Default.gamedirectory = dialog.SelectedPath;
                            Properties.Settings.Default.Save();
                            check = true;
                            return;
                        }
                        MessageBox.Show("Error cannot find pso2.exe in selected folder. Please try again.");
                    }
                    
                }
                else if (dial == DialogResult.No)
                {
                    DialogResult downres = MessageBox.Show("Would you like to download the game files?", "Confirmation", MessageBoxButtons.YesNo);
                    if(downres == DialogResult.Yes)
                    {
                        Download dl = new Download();
                        dl.ShowDialog();
                    }
                    else if(downres == DialogResult.No)
                    {
                        MessageBox.Show("Launcher cannot run without game data. You can download the game using the download button");
                    }
                    
                }

                
            }

            //File.Create("version.txt");
            File.WriteAllText("version.txt",ver.ToString());
            

        }

        public static string GenerateFileContents(string pso2BinDirectory)
        {
            var key = "kW7eheKa7RMFXkbW7V5U";
            var hour = DateTime.Now.Hour.ToString(CultureInfo.InvariantCulture);
            var sanitizedDirectoryPath = Properties.Settings.Default.gamedirectory;


            var directoryPathLength = sanitizedDirectoryPath.Length.ToString(CultureInfo.InvariantCulture);

            var combinedSeed = key + hour + directoryPathLength;
            using (var md5 = MD5.Create())
            {
                var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(combinedSeed));
                var hexedStrings = hashBytes.Select(b => b.ToString("x2", CultureInfo.InvariantCulture));
                return string.Concat(hexedStrings);
            }
        }

        static void WriteStringToFile(string text, string filePath)
        {
            try
            {
                // Write the string to the specified file
                File.WriteAllText(filePath, text);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static void LaunchExecutable(string exePath, string arguments)
        {
            try
            {
                // Create a process start info
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    WorkingDirectory = Properties.Settings.Default.gamedirectory,
                    FileName = "pso2.exe",
                    Verb = "runas",
                    Arguments = arguments,

                };

                // Start the process
                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.Start();

                    // Optionally, you can read the standard output if needed
                    // string output = process.StandardOutput.ReadToEnd();
                    // Console.WriteLine(output);

                    //process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

       

        

       

        private void Form1_Load(object sender, EventArgs e)
        {
            // gameD.Text = Properties.Settings.Default.gamedirectory;
            //this.AutoSize = false;
            //this.Size = new Size(500,450)
            


        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }



       


        //crc code

        public static void CalculateFolderCrc(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder not found: {folderPath}");
                return;
            }

            var files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                uint crc = CalculateFileCrc(file);
                Console.WriteLine($"CRC32 for {file}: {crc}");
            }
        }

        private static uint CalculateFileCrc(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var crc32 = new Crc32();

                return crc32.ComputeHash(fileStream);
            }
        }
        public class Crc32 : HashAlgorithm
        {
            public const uint DefaultPolynomial = 0xedb88320;
            public const uint DefaultSeed = 0xffffffff;

            private uint _hash;
            private readonly uint _seed;
            public readonly uint[] _table;
            private static readonly uint[] _defaultTable = InitializeTable(DefaultPolynomial);

            public Crc32()
            {
                _table = InitializeTable(DefaultPolynomial);
                _seed = DefaultSeed;
                Initialize();
            }

            public override void Initialize()
            {
                _hash = _seed;
            }

            protected override void HashCore(byte[] array, int ibStart, int cbSize)
            {
                _hash = CalculateHash(_table, _hash, array, ibStart, cbSize);
            }

            protected override byte[] HashFinal()
            {
                var hashBuffer = UInt32ToBigEndianBytes(~_hash);
                HashValue = hashBuffer;
                return hashBuffer;
            }

            public override int HashSize => 32;

            public uint ComputeHash(Stream stream)
            {
                Initialize();
                var buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    HashCore(buffer, 0, bytesRead);
                }
                return _hash;
            }

            public uint ComputeHash(byte[] buffer)
            {
                Initialize();
                HashCore(buffer, 0, buffer.Length);
                return _hash;
            }

            public uint ComputeHash(byte[] buffer, int offset, int count)
            {
                Initialize();
                HashCore(buffer, offset, count);
                return _hash;
            }

            private static uint[] InitializeTable(uint polynomial)
            {


                if (polynomial == DefaultPolynomial && _defaultTable != null)
                {
                    return _defaultTable;
                }

                var table = new uint[256];
                for (var i = 0; i < 256; i++)
                {
                    var entry = (uint)i;
                    for (var j = 0; j < 8; j++)
                    {
                        if ((entry & 1) == 1)
                        {
                            entry = (entry >> 1) ^ polynomial;
                        }
                        else
                        {
                            entry = entry >> 1;
                        }
                    }
                    table[i] = entry;
                }
                return table;
            }

            private static uint CalculateHash(uint[] table, uint seed, IList<byte> buffer, int start, int size)
            {
                if (table == null || buffer == null)
                {
                    throw new ArgumentNullException("table or buffer is null");
                }

                if (start < 0 || start >= buffer.Count || size <= 0 || start + size > buffer.Count)
                {
                    throw new ArgumentOutOfRangeException("Invalid start or size parameters");
                }

                var hash = seed;
                for (var i = start; i < start + size; i++)
                {
                    hash = (hash >> 8) ^ table[buffer[i] ^ hash & 0xff];
                }
                return hash;
            }

            private byte[] UInt32ToBigEndianBytes(uint x)
            {
                return new byte[]
                {
            (byte)((x >> 24) & 0xff),
            (byte)((x >> 16) & 0xff),
            (byte)((x >> 8) & 0xff),
            (byte)(x & 0xff)
                };
            }


        }




       

        private void button6_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Currently Disabled. Still WIP. currently being used as 7zip extraction test");
            DialogResult dr = MessageBox.Show("Would you like to check updates?","Confirmation",MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                
            }

            


            






            return;

            //string gamedata = gameD.Text + "\\data\\win32";
            //CalculateFolderCrc(gamedata);
            MessageBox.Show("Hash code complete", "Notification");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string strExeFilePath = Properties.Settings.Default.gamedirectory;
            string strWorkPath = Properties.Settings.Default.gamedirectory;
            var magic = GenerateFileContents(strWorkPath);
            WriteStringToFile(magic, strWorkPath + "//tweaker.bin");
            LaunchExecutable(strWorkPath + "//pso2.exe", "-pson2 -pson2");
            int dbg = 1;
        }

      

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.play_btn;
        }

        private void highlight_VisibleChanged(object sender, EventArgs e)
        {
            
        }

        private void Logo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SSettings sSettings = new SSettings();
            sSettings.TopLevel = false;


            this.AutoSize = true;
            this.Controls.Add(sSettings);
            sSettings.Show();
            this.Controls.SetChildIndex(sSettings, 0);
        }

        //Code for downloading game.
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Download dl = new Download();
            dl.ShowDialog();
            
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.play_btn_mo;
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.Image = Resources.Download_click;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Resources.download;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Image = Resources.Settings_click;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Resources.settings;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Resources.play_btn_dn;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            webView21.Source = new Uri("https://zmbkilla.github.io/SLWeb/update");
        }

        bool IsShown = false;

        
    }
}
