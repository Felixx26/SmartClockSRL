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
using SmartClockSRL.Properties;

namespace SmartClockSRL.Presentation
{
    public partial class frmEmployeesInfo : Form
    {
        private int? employeeID;
        public frmEmployeesInfo(int? employeeID)
        {
            InitializeComponent();
            this.employeeID = employeeID;
        }

        private void frmEmployeesInfo_Load(object sender, EventArgs e)
        {
            using (smartclocksrldbEntities db = new smartclocksrldbEntities())
            {
                employees employee = db.employees.Find(this.employeeID);
                if(employee != null)
                    populateData(employee);

                var lstemployees = from d in db.employees select d;
                lblPersonalCount.Text = lstemployees.Where(p => p.idPosition == employee.idPosition).Count().ToString();
            }
        }

        #region

        private void populateData(employees e)
        {
            lblName.Text = e.name;
            lblLastName.Text = e.lastname;
            lblRegNumber.Text = e.id.ToString();
            lblGender.Text = e.gender;
            lblBirthDate.Text = e.birthdate.Value.ToString("dd/MM/yyyy");
            lblPosition.Text = e.position.name;
            lblAddress.Text = e.address;
            lblPositionName.Text = e.position.name;
            lblPhone.Text = e.phone;
            lblDescription.Text = e.position.description;
            if(e.images != null)
            {
                MemoryStream ms = new MemoryStream(e.images);
                Image returnImage = Image.FromStream(ms);
                pictureBox1.Image = returnImage;
            }
        }
        #endregion
    }
}
