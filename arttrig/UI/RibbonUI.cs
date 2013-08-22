using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using arttrig.UI;

namespace arttrig
{
    public partial class RibbonUI
    {
        private void RibbonUI_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void btnSetup_Click(object sender, RibbonControlEventArgs e)
        {
            if (open == false) //Just to make sure we can only open 1 instance of the ArtNet crap.
            {
                Main main = new Main();
                main.Show();
                open = true;
            }
        }

        private void btnAbout_Click(object sender, RibbonControlEventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private bool open = false;

    }
}
