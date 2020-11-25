using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartClockSRL.Models;

namespace SmartClockSRL.Presentation
{
    public partial class frmAddNewEmployee : Form
    {
        public frmAddNewEmployee()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                using (smartclocksrldbEntities db = new smartclocksrldbEntities())
                {
                    employees oEmployees = new employees();
                    position oPosition = db.position.Find(cmbPosition.SelectedValue);
                    oEmployees.name = txtName.Text;
                    oEmployees.lastname = txtLastName.Text;
                    oEmployees.address = txtAddress.Text;
                    oEmployees.birthdate = dtpBirthDate.Value;
                    oEmployees.phone = txtPhone.Text;
                    oEmployees.email = txtEmail.Text;
                    oEmployees.admissionDate = dtpAdmissionDate.Value;
                    oEmployees.huella = null;
                    oEmployees.gender = cmbGender.Text;
                    oEmployees.position = oPosition;
                    MemoryStream memorystream = new MemoryStream();
                    pictureBox1.Image.Save(memorystream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    oEmployees.images = memorystream.ToArray();

                    db.employees.Add(oEmployees);
                    db.SaveChanges();
                    MessageBox.Show("Added Successfully");
                }
            }
        }

        private bool validation()
        {
            resetValidation();
            List<bool> val = new List<bool>();
            val.Add(true);
            if (txtName.Text == null || txtName.Text == "")
            {
                lblValidationName.Visible = true;
                val.Add(false);
            }
            if (txtLastName.Text == null || txtLastName.Text =="") 
            {
                lblValidationLastName.Visible = true;
                val.Add(false);
            }
            if (txtAddress.Text == null || txtAddress.Text == "")
            {
                lblValidationAddress.Visible = true;
                val.Add(false);
            }
            if (dtpBirthDate.Value == null)
            {
                lblValidationBirthdate.Visible = true;
                val.Add(false);
            }
            if (txtPhone.Text == null || txtPhone.Text == "")
            {
                lblValidationPhone.Visible = true;
                val.Add(false);
            }
            if (txtEmail.Text == null || txtEmail.Text == "" || !txtEmail.Text.Contains("@"))
            {
                lblValidationEmail.Visible = true;
                val.Add(false);
            }
            if (cmbPosition.SelectedValue == null || cmbPosition.SelectedValue == "")
            {
                lblValidationPosition.Visible = true;
                val.Add(false);
            }
            if (dtpAdmissionDate.Value == null)
            {
                lblValidationAdmissionDate.Visible = true;
                val.Add(false);
            }
            if (cmbGender.Text == null)
            {
                lblValidationGender.Visible = true;
                val.Add(false);
            }
            return val.Contains(false)?false:true;
        }

        private void resetValidation()
        {
            lblValidationName.Visible = false;
            lblValidationLastName.Visible = false;
            lblValidationAddress.Visible = false;
            lblValidationBirthdate.Visible = false;
            lblValidationPhone.Visible = false;
            lblValidationEmail.Visible = false;
            lblValidationPosition.Visible = false;
            lblValidationAdmissionDate.Visible = false;
            lblValidationGender.Visible = false;

        }

        private void resetInputs()
        {
            txtName.Clear();
            txtLastName.Clear();
            txtAddress.Clear();
            dtpBirthDate.Value = DateTime.Now;
            txtEmail.Clear();
            txtPhone.Clear();
            cmbPosition.Text = "";
            cmbGender.Text = "";
            dtpAdmissionDate.Value = DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resetInputs();
        }

        private void frmAddNewEmployee_Load(object sender, EventArgs e)
        {
            using (smartclocksrldbEntities db = new smartclocksrldbEntities())
            {
                var lstPosition = from d in db.position
                                  select d;
                cmbPosition.DataSource = lstPosition.ToList();
                cmbPosition.DisplayMember = "name";
                cmbPosition.ValueMember = "id";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string image = openFileDialog1.FileName;
                    pictureBox1.Image = Image.FromFile(image);
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.BackColor = pictureBox2.BackColor == Color.FromArgb(0, 64, 0) ? Color.FromArgb(50, 50, 50) : Color.FromArgb(0, 64, 0);
        }
    }
}
