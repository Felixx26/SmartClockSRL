using SmartClockSRL.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartClockSRL
{
    public partial class frmMainMenu : Form
    {
        public frmMainMenu()
        {
            InitializeComponent();
        }

        private Form activeForm = null;

        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void frmMainMenu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openChildForm(new frmDynamicTable("employees", this));
            label1.Text = "Reg Employees";

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            openChildForm(new frmAddNewEmployee());
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            openChildForm(new frmDynamicTable("checkin", this));
            label1.Text = "Reg Check-In";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new frmDynamicTable("checkout", this));
            label1.Text = "Reg Check-Out";

        }

        private void frmMainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
