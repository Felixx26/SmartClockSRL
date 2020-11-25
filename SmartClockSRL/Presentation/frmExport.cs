using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartClockSRL.Presentation
{
    public partial class frmExport : Form
    {
        public frmExport()
        {
            InitializeComponent();
        }

        private bool flag = false;
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            flag = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            flag = true;

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            flag = true;

            this.Close();
        }

        private void frmExport_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
