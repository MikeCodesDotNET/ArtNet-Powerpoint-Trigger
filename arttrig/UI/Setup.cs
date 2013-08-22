using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace arttrig.UI
{
    public partial class Setup : Form
    {
        public Setup()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int uni = Convert.ToInt16(cmbUniverse.SelectedItem);
            int chan = Convert.ToInt16(tbxChannel.Text);
            
            Main main = new Main();
            main.Show();
            this.Close();
        }
    }
}
