﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        ImageController im = new ImageController();
        public Form1()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == false && string.IsNullOrEmpty(textBox2.Text) == false)
            {
                im.Scale(int.Parse(textBox1.Text.ToString()), int.Parse(textBox2.Text.ToString()), pictureBox1.CreateGraphics());

            }
            else
            {

                MessageBox.Show("please fill text box", "Error");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text) == false)
            {

                im.all(float.Parse(textBox1.Text), float.Parse(textBox2.Text), float.Parse(textBox3.Text), float.Parse(textBox4.Text), float.Parse(textBox5.Text), pictureBox1.CreateGraphics());
            }
            else
            {
                MessageBox.Show("please fill text box 3", "Error");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text) == false)
            {

                im.Rotate(int.Parse(textBox3.Text), pictureBox1.CreateGraphics());
            }
            else
            {
                MessageBox.Show("please fill text box 3", "Error");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text) == false && string.IsNullOrEmpty(textBox5.Text) == false)
            {
                im.Shearing(int.Parse(textBox4.Text), int.Parse(textBox4.Text), pictureBox1.CreateGraphics());

            }
            else
            {
                MessageBox.Show("please fill text box", "Error");
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(ofd.FileName))
            {
                pictureBox1.Image = im.Read(ofd.FileName);
                pictureBox1.Size = im.ImageBitmap.Size;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog ofd = new FolderBrowserDialog();
            if (ofd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(ofd.SelectedPath))
            {
                im.savingpicture(ofd.SelectedPath, im.ImageBitmap);
            }

        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = im.Grayscale();
        }

        private void nOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = im.NOT();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            numericUpDown1.Value = trackBar1.Value;
            pictureBox1.Image = im.Brightness(trackBar1.Value);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Value = decimal.ToInt32(numericUpDown1.Value);
            pictureBox1.Image = im.Brightness(trackBar1.Value);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Value = decimal.ToInt32(numericUpDown1.Value);
            pictureBox1.Image = im.Brightness(Convert.ToInt32(numericUpDown2.Value));
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            numericUpDown1.Value = trackBar2.Value;
            pictureBox1.Image = im.Brightness(trackBar2.Value);

        }
    }
}
