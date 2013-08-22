using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.PowerPoint;
using ArtNet;

namespace arttrig
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            artNet = new ArtNet.ArtNet();      
            updateTimer.Start();

            lblStatus.Text = ("You must enable this plugin before it will look for ArtNet");

            artNet.DMX_Recieved += new ArtNet.ArtNet.DMX_RecievedEventHandler(artRec);

        }

        //Private Functions
        private void btnNext_Click(object sender, EventArgs e)
        {
            NextSlide();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            PrevSlide();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            running = false;


        }

        private void chbxEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (chbxEnabled.Checked == true)
            {
                running = true;
                btnNext.Enabled = true;
                btnPrev.Enabled = true;

                PrevVal = Convert.ToByte(tbxPrevVal.Text);
                HoldVal = Convert.ToByte(tbxHoldVal.Text);
                NextVal = Convert.ToByte(tbxNextVal.Text);


            }
            else
            {
                running = false;
                btnNext.Enabled = false;
                btnPrev.Enabled = false;

            }
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {


            if (running == true)
            {
                if (artNet.Detected == true)
                {
                    lblStatus.Text = "ArtNet Detected";
                    lblStatus.ForeColor = Color.Green;
                }
                else
                {
                    lblStatus.Text = "No ArtNet Found";
                    lblStatus.ForeColor = Color.Red;
                }
            }
        }

        //Here is where we deal with ArtNet data. 
        void artRec(int Universe, byte[] DMX)
        {
            if (running == true)
            {

                if (Universe == 1)
                {
                    LastRecievedVal = DMX[1];

                    if (LastRecievedVal != LastChangedVal)
                    {
                        if (LastRecievedVal <= PrevVal)
                        {
                            PrevSlide();
                            LastChangedVal = LastRecievedVal;
                        }

                        if (LastRecievedVal >= 121 && LastRecievedVal <= 129)
                        {
                            LastChangedVal = LastRecievedVal;
                        }

                        if (LastRecievedVal >= NextVal)
                        {
                            NextSlide();
                            LastChangedVal = LastRecievedVal;
                        }
                        
                    }
                }


            }
        }

        //Public Functions
        public void PrevSlide()
        {
            if (running == true)
            {
                var presentation = Globals.ThisAddIn.Application.ActivePresentation;

                presentation.SlideShowWindow.View.Previous();
            }
        }

        public void NextSlide()
        {
            if (running == true)
            {
                var presentation = Globals.ThisAddIn.Application.ActivePresentation;

                presentation.SlideShowWindow.View.Next();
            }
        }

        //Properties
        private bool running;
        private ArtNet.ArtNet artNet;
        private byte LastRecievedVal;
        private byte LastChangedVal = 200;

        private byte PrevVal;
        private byte HoldVal;
        private byte NextVal;

        private void tbxNextVal_TextChanged(object sender, EventArgs e)
        {
            if (tbxNextVal.Text != "")
            {
                NextVal = Convert.ToByte(tbxNextVal);
            }
        }

        private void tbxHoldVal_TextChanged(object sender, EventArgs e)
        {
            if (tbxHoldVal.Text != "")
            {
                HoldVal = Convert.ToByte(HoldVal);
            }
        }

        private void tbxPrevVal_TextChanged(object sender, EventArgs e)
        {
            if (tbxPrevVal.Text != "")
            {
                PrevVal = Convert.ToByte(PrevVal);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        } 
    }
}
