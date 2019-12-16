using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Musical_Chairs
{
    public partial class Form1 : Form
    {
        public WaveOutEvent outputDevice;
        public AudioFileReader audioFile;
        Color color;
        public Form1()
        {
            InitializeComponent();
            if (Properties.Settings.Default.hasExistedBefore == true)
            {
                color = Properties.Settings.Default.color;
            }
        }
        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            outputDevice.Dispose();
            outputDevice = null;
            audioFile.Dispose();
            audioFile = null;
        }
        bool change = true;

        private void label2_Click(object sender, EventArgs e)
        {
            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
                outputDevice.PlaybackStopped += OnPlaybackStopped;
            }
            if (audioFile == null)
            {
                try {
                    audioFile = new AudioFileReader(@"song.mp3");
                    outputDevice.Init(audioFile);
                }
                catch { MessageBox.Show("You need to put the file song.mp3 in the software folder"); Application.Exit(); }
            }
            if (change==true)
            {
                label2.BackColor = color;
                change = false;
                button1.Visible = false;
                button2.Visible = false;
                outputDevice.Play();
            }
            else
            {
                label2.BackColor = Color.FromArgb(0, 0, 0);;
                change = true;
                outputDevice.Pause();
                button1.Visible = true;
                button2.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            audioFile = null;
            outputDevice = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog()==DialogResult.OK)
            {
                //display(colorDialog1.Color.Name);
                color = colorDialog1.Color;
                Properties.Settings.Default.color = color;
                Properties.Settings.Default.Save();
            }

        }
        private void display(string text)
        {
            MessageBox.Show(text);
        }
    }
}
