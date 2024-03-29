﻿using SLauncher.Properties;
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
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

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

                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
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

            string strExeFilePath = gameD.Text;
            string strWorkPath = gameD.Text;
            var magic = GenerateFileContents(strWorkPath);
            WriteStringToFile(magic, strWorkPath + "//tweaker.bin");
            LaunchExecutable(strWorkPath + "//pso2.exe", "-pson2 -pson2");
            int dbg = 1;

            //if (File.Exists(gameD.Text + "\\pso2.exe"))
            //    {
            //    Process startP = new Process();
            //    //startP.StartInfo.Verb = "runas";
            //    startP.StartInfo.WorkingDirectory = gameD.Text;
            //    startP.StartInfo.FileName = "pso2.exe";
            //    startP.StartInfo.Arguments = "-pson2 -pson2";
            //
            //    startP.Start();
            //}
            //else
            //{
            //    MessageBox.Show("The directory does not contain the pso2 executable","Error");
            //}

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
                button3.Enabled = false;
                button2.Enabled = false;
                button5.Enabled = false;

            };
            webClient.DownloadFileCompleted += (s, p) =>
            {
                progressBar1.Visible = false;
                button3.Enabled = true;
                button2.Enabled = true;
                button5.Enabled = true;
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
                gameD.Text = filepath;
            };

            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback4);
            if (filepath != ""|| filepath == null)
            {
                webClient.DownloadFileAsync(new Uri("https://onedrive.live.com/download?resid=ADE1D97E92AEC8BE%21403144&authkey=!AN7Xv7If2YMHH88"), @filepath + "\\game.7z");
            }
            else
            {
                MessageBox.Show("Error. No download address specified", "Error");
            }
            

            

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
            string gamedata = gameD.Text + "\\data\\win32";
            CalculateFolderCrc(gamedata);
            MessageBox.Show("Hash code complete", "Notification");
        }
    }
}
