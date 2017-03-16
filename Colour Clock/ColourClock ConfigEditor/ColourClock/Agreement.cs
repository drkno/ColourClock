using System;
using System.Windows.Forms;

namespace ColourClock
{
    public partial class Agreement : Form
    {
        public Agreement()
        {
            InitializeComponent();
        }

        private void ButtonDisagreeClick(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void ButtonAgreeClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}