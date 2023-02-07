using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoItX3Lib;
using System.Threading;
using System.Runtime.InteropServices;

namespace TotalnieNieFishingBot
{
    public partial class Form1 : Form
    {
        Random rd = new Random();

        int col;

        private int _ticks;
        IntPtr handle;

        AutoItX3 au3 = new AutoItX3();

        public RECT rect;
        public struct RECT
        {
            public int left, top, right, bottom;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void durabilityCheck()
        {
            col = au3.PixelGetColor(1278, 114);
            if (col == 0xB74F44)
            {
                timer1.Stop();
                checkBox1.Checked = false;
            }

        }


        private void energyCheck()
        {
            col = au3.PixelGetColor(793, 859);
            if (col == 0x000000)
            {
                timer1.Stop();
                checkBox1.Checked = false;
            }

        }


        private void fishing()
        {
                    Object pix = au3.PixelSearch(400,0,1680,830, 0xFBD090,1);
                    if(pix.ToString()!="0")
                    {
                     object[] pixelCoord = (object[])pix;
                    au3.MouseMove((int)pixelCoord[0], (int)pixelCoord[1], rd.Next(1000,2000));
                    au3.Send("E"); //getting fish out of water
                    Thread.Sleep(rd.Next(6000,8000));
                    au3.MouseMove((int)pixelCoord[0]+400, (int)pixelCoord[1], rd.Next(1000, 2000)); //throwing rod xD
                    au3.Send("E");
                     Thread.Sleep(rd.Next(2000, 3000));
                au3.MouseMove((int)pixelCoord[0], (int)pixelCoord[1], rd.Next(1000, 2000));//currsor back to float
                   Thread.Sleep(rd.Next(1500,3000));
                    }

                    Thread.Sleep(20);
        }




        private void timer1_Tick(object sender, EventArgs e)
        {
            _ticks++;
            this.Text = _ticks.ToString();

            if (_ticks>1000)
            {
                this.Text = "Done";
                timer1.Stop();
                checkBox1.Checked = false;
                MessageBox.Show("We done");
            }
            else
            {
                energyCheck();
                durabilityCheck();
                fishing();
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                timer1.Start();
            }    
            else
            {
                timer1.Stop();
                _ticks = 0;
            }
        }
    }
}
