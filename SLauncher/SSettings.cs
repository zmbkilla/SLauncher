using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SLauncher
{
    public partial class SSettings : Form
    {
        public SSettings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            string line = File.ReadLines("C:\\Users\\zmbkilla\\Documents\\SEGA\\PHANTASYSTARONLINE2\\user.pso2").Skip(471).Take(1).First();
            line.Replace("  ", "");
            int i = 1; int j = 1;
            while (i <= j)
            {
                dataGridView1.Rows.Add(i);
                DataGridViewRow row = dataGridView1.Rows[i];
                
                row.Cells["Settings_name"].Value = line;
                i += 1;
            }
            
        }

    }
}
