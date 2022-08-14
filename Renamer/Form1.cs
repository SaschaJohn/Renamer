using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Renamer
{
    public partial class Form1 : Form
    {

        private List<string> patterns;
        private ListBox listBox1;
        private Button button1;
        private Button button2;
        private Label label1;
        private ListBox listBox2;
        private Label label2;
        private Button button3;
        private Button button4;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.listBox1.AllowDrop = true;
            this.listBox1.DragEnter += new DragEventHandler(this.listBoxFiles_DragEnter);
            this.refreshPatterns();
        }

        private void listBoxFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;
            e.Effect = DragDropEffects.Copy;
        }

        private void dd(object sender, DragEventArgs e)
        {
            List<string> stringList = new List<string>();
            foreach (string str in (string[])e.Data.GetData(DataFormats.FileDrop, false))
            {
                if (Directory.Exists(str))
                {
                    stringList.Add(str);
                    this.addTargetListbox(str);
                }
            }
            this.listBox1.Items.AddRange((object[])stringList.ToArray());
        }

        private void addTargetListbox(string s) => this.listBox2.Items.Add((object)this.getTargetName(new DirectoryInfo(s).Name));

        private string getTargetName(string sourceName)
        {
            string input = sourceName;
            foreach (string pattern in this.patterns)
            {
                if (pattern != null)
                    input = input.Replace(pattern, "", StringComparison.CurrentCultureIgnoreCase);
            }
            Regex regex = new Regex("(.*)\\.([12][0-9][0-9][0-9])(.*)");
            if (regex.IsMatch(input))
            {
                Match match = regex.Match(input);
                input = match.Groups[1]?.ToString() + " (" + match.Groups[2]?.ToString() + ")" + match.Groups[3]?.ToString();
            }
            return input;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            this.listBox2.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e) => this.refreshPatterns();

        private void refreshPatterns()
        {
            this.patterns = new List<string>();
            string path = ".\\patterns.txt";
            if (!File.Exists(path))
            {
                int num = (int)MessageBox.Show("File not found!");
            }
            else
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    string str = ((TextReader)streamReader).ReadLine();
                    while (str != null)
                    {
                        str = ((TextReader)streamReader).ReadLine();
                        this.patterns.Add(str);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (string str in this.listBox1.Items)
                Directory.Move(str, this.getTargetName(str));
            this.listBox1.Items.Clear();
            this.listBox2.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e) => Process.Start("notepad.exe", ".\\patterns.txt");


        public Form1()
        {
            InitializeComponent();
            this.listBox1 = new ListBox();
            this.button1 = new Button();
            this.button2 = new Button();
            this.label1 = new Label();
            this.listBox2 = new ListBox();
            this.label2 = new Label();
            this.button3 = new Button();
            this.button4 = new Button();
            this.SuspendLayout();
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new Point(12, 42);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(238, 334);
            this.listBox1.TabIndex = 0;
            this.listBox1.DragDrop += new DragEventHandler(this.dd);
            this.button1.Location = new Point(13, 383);
            this.button1.Name = "button1";
            this.button1.Size = new Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.Location = new Point(256, 382);
            this.button2.Name = "button2";
            this.button2.Size = new Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Rename";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(43, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Source";
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 15;
            this.listBox2.Location = new Point(256, 42);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new Size(485, 334);
            this.listBox2.TabIndex = 4;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(273, 9);
            this.label2.Name = "label2";
            this.label2.Size = new Size(48, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Preview";
            this.button3.Location = new Point(175, 382);
            this.button3.Name = "button3";
            this.button3.Size = new Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Refresh";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new EventHandler(this.button3_Click);
            this.button4.Location = new Point(94, 383);
            this.button4.Name = "button4";
            this.button4.Size = new Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "Edit";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new EventHandler(this.button4_Click);
            this.AutoScaleDimensions = new SizeF(7f, 15f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(753, 450);
            this.Controls.Add((Control)this.button4);
            this.Controls.Add((Control)this.button3);
            this.Controls.Add((Control)this.label2);
            this.Controls.Add((Control)this.listBox2);
            this.Controls.Add((Control)this.label1);
            this.Controls.Add((Control)this.button2);
            this.Controls.Add((Control)this.button1);
            this.Controls.Add((Control)this.listBox1);
            this.Name = nameof(Form1);
            this.Text = "Renamer";
            this.Load += new EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}