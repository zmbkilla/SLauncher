using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLauncher
{
    public partial class Download : Form
    {
        public Download()
        {
            InitializeComponent();
        }

        private void Download_Enter(object sender, EventArgs e)
        {

        }

        private void Download_Load(object sender, EventArgs e)
        {
            FolderBrowserDialog Gamedown = new FolderBrowserDialog();
            Gamedown.Description = "Select the folder you want to download the game";
            Gamedown.ShowDialog();
            Console1.Show();

            string filepath = Gamedown.SelectedPath;


            WebClient webClient = new WebClient();

            if (File.Exists("" + filepath + "\\game.7z"))
            {
                DialogResult dr = new DialogResult();
                dr = MessageBox.Show("Gamedata already exist. Would you like to extract it instead?", "Notification", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    string runningpath = System.AppDomain.CurrentDomain.BaseDirectory;

                    ProcessStartInfo pz = new ProcessStartInfo

                    {

                        //FileName = string.Format("{0}Resources\\7zg.exe", Path.GetFullPath(Path.Combine(runningpath, @"..\..\"))),
                        FileName = runningpath+ "\\Resources\\7zg.exe",

                        Arguments = "x \"" + filepath + "\\game.7z\" -r -o\"" + Gamedown.SelectedPath + "\"",

                        WindowStyle = ProcessWindowStyle.Normal

                    };

                    Process x = Process.Start(pz);

                    x.WaitForExit();
                    try
                    {

                        webClient.DownloadFile(new Uri("https://github.com/zmbkilla/SLauncher/releases/download/resources/stats"), "" + filepath + "\\PHANTASYSTARONLINE2_JP_5thFeb-2021~\\PHANTASYSTARONLINE2\\pso2_bin\\data\\win32\\595f683e58a4986214efde6922f5430f");
                    }
                    catch
                    {
                        MessageBox.Show("Error getting additional files. Please contact devteam");
                    }
                    DialogResult dres = MessageBox.Show("Extract Complete","Confirmation",MessageBoxButtons.OK);
                    if (dres == DialogResult.OK)
                    {
                        
                        
                        this.Close();
                    }

                    return;
                }

            }

            webClient.DownloadProgressChanged += (s, p) =>
            {
                progressBar1.Visible = true;
                progressBar1.Value = p.ProgressPercentage;
                Console1.Text = (Convert.ToString(p.UserState) + "    downloaded " + p.BytesReceived + " of " + p.TotalBytesToReceive + " bytes. " + p.ProgressPercentage + " % complete...");
                

            };
            webClient.DownloadFileCompleted += (s, p) =>
            {
                progressBar1.Visible = false;
                
                // any other code to process the file


                //below is old code for extracting game files
                //archiveFile.Extract("Output"); // extract all

                string runningpath = System.AppDomain.CurrentDomain.BaseDirectory;

                ProcessStartInfo pz = new ProcessStartInfo

                {

                    //FileName = string.Format("{0}Resources\\7zg.exe", Path.GetFullPath(Path.Combine(runningpath, @"..\..\"))),
                    FileName = runningpath + "\\Resources\\7zg.exe",
                    Arguments = "x \"" + filepath + "\\game.7z\" -r -o\"" + Gamedown.SelectedPath + "\"",

                    WindowStyle = ProcessWindowStyle.Normal

                };

                Process x = Process.Start(pz);

                x.WaitForExit();

                MessageBox.Show("Download Complete!", "Notification");
                var delres = MessageBox.Show("Do you want to delete zipped download?", "Confirmation", MessageBoxButtons.YesNo);
                if (delres == DialogResult.Yes)
                {
                    File.Delete(@filepath + "\\game.7z");
                    try
                    {
                        webClient.DownloadFile(new Uri("https://github.com/zmbkilla/SLauncher/releases/download/resources/stats"), "" + filepath + "\\PHANTASYSTARONLINE2_JP_5thFeb-2021~\\PHANTASYSTARONLINE2\\pso2_bin\\data\\win32\\595f683e58a4986214efde6922f5430f");
                    }
                    catch
                    {
                        MessageBox.Show("Error getting Additional required files. Use the download button to restart additional file download");
                    }

                }
                else if (delres == DialogResult.No)
                {
                    try
                    {
                        webClient.DownloadFile(new Uri("https://github.com/zmbkilla/SLauncher/releases/download/resources/stats"), "" + filepath + "\\PHANTASYSTARONLINE2_JP_5thFeb-2021~\\PHANTASYSTARONLINE2\\pso2_bin\\data\\win32\\595f683e58a4986214efde6922f5430f");
                    }
                    catch
                    {
                        MessageBox.Show("Error getting Additional required files. Use the download button to restart additional file download");
                    }
                }
                Properties.Settings.Default.gamedirectory = "\"" + filepath + "\\PHANTASYSTARONLINE2_JP_5thFeb-2021~\\PHANTASYSTARONLINE2\\pso2_bin\"";
                //webClient.DownloadFileAsync(new Uri("https://cdn.discordapp.com/attachments/1181777274744352808/1182607074446802975/595f683e58a4986214efde6922f5430f?ex=66354fea&is=6633fe6a&hm=a95eeff13e5804be1ff3d899e41309a88cfc1a63172e0bd4c4fa23ef5382280e&"), "\""+gameD.Text+"\\data\\win32\"");

            };

            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback4);
            if (filepath != "" || filepath == null)
            {
                //old link https://onedrive.live.com/download?resid=ADE1D97E92AEC8BE%21403144&authkey=!AN7Xv7If2YMHH88
                webClient.DownloadFileAsync(new Uri("insertlinkhere"), @filepath + "\\game.7z");
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
    
    }
}
