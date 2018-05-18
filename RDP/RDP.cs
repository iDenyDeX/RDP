using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDP
{
    public partial class RDP : Form
    {
        public RDP()
        {
            InitializeComponent();
        }

        private void RDP_Load(object sender, EventArgs e)
        {

        }

        private void RDP_Closing(object sender, FormClosingEventArgs e)
        {
            LoginForm LF = new LoginForm();
            LF.Visible = true;
        }
    }
}
