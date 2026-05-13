using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace MediaHomework
{
    public partial class frmBeepPlayer : Form
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);

        int[] freq = { 523, 587, 659, 698, 784, 880, 988, 1046 };

        int initWidth = 0;
        int initHeight = 0;
        Dictionary<string, Rectangle> initControl = new Dictionary<string, Rectangle>();

        public frmBeepPlayer()
        {
            InitializeComponent();
            InitializeButton();
        }

        private void InitializeButton()
        {
            btn1.Click += btn1_Click;
            btn2.Click += btn1_Click;
            btn3.Click += btn1_Click;
            btn4.Click += btn1_Click;
            btn5.Click += btn1_Click;
            btn6.Click += btn1_Click;
            btn7.Click += btn1_Click;
            btn8.Click += btn1_Click;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == null) return;

            btn.Enabled = false;
            Beep(freq[btn.TabIndex], 300);
            btn.Enabled = true;
        }

        private void frmBeepPlayer_Load(object sender, EventArgs e)
        {
            initWidth = palMain.Width;
            initHeight = palMain.Height;

            initControl.Clear();

            foreach (Control ctl in palMain.Controls)
            {
                initControl[ctl.Name] = new Rectangle(
                    ctl.Left,
                    ctl.Top,
                    ctl.Width,
                    ctl.Height
                );
            }
        }

        private void frmBeepPlayer_SizeChanged(object sender, EventArgs e)
        {
            if (initWidth == 0 || initHeight == 0) return;

            double width = palMain.Width;
            double height = palMain.Height;

            double iRatioWidth = width / initWidth;
            double iRatioHeight = height / initHeight;

            foreach (Control ctl in palMain.Controls)
            {
                if (!initControl.ContainsKey(ctl.Name)) continue;

                ctl.Left = (int)(initControl[ctl.Name].Left * iRatioWidth);
                ctl.Top = (int)(initControl[ctl.Name].Top * iRatioHeight);
                ctl.Width = (int)(initControl[ctl.Name].Width * iRatioWidth);
                ctl.Height = (int)(initControl[ctl.Name].Height * iRatioHeight);
            }
        }
    }
}