﻿namespace SLauncher
{
    partial class SSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sbut = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1920 x 1080",
            "1760 x 990",
            "1680 x 1050",
            "1600 x 900",
            "1440 x 900",
            "1366 x 768",
            "1280 x 1024",
            "1280 x 800",
            "1080 x 720",
            "1152 x 870",
            "1152 x 864",
            "1128 x 634",
            "1024 x 768",
            "832 x 624",
            "800 x 600",
            "720 x 576",
            "720 x 480",
            "720 x 400",
            "640 x 480"});
            this.comboBox1.Location = new System.Drawing.Point(452, 293);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(449, 265);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Resolution";
            // 
            // sbut
            // 
            this.sbut.Location = new System.Drawing.Point(31, 379);
            this.sbut.Name = "sbut";
            this.sbut.Size = new System.Drawing.Size(103, 31);
            this.sbut.TabIndex = 3;
            this.sbut.Text = "Save";
            this.sbut.UseVisualStyleBackColor = true;
            this.sbut.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(165, 379);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 31);
            this.button2.TabIndex = 4;
            this.button2.Text = "Back";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Fullscreen",
            "Virtual Fullscreen",
            "Windowed"});
            this.comboBox2.Location = new System.Drawing.Point(452, 210);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(449, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Window style";
            // 
            // SSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.sbut);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "SSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button sbut;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label3;
    }
}