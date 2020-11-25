using iTextSharp.text;
using iTextSharp.text.pdf;
using SmartClockSRL.Models;
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

namespace SmartClockSRL.Presentation
{
    public partial class frmDynamicTable : Form
    {
        private string option;
        private frmMainMenu frmMain;
        public frmDynamicTable(string option, frmMainMenu frmMain)
        {
            InitializeComponent();
            this.option = option;
            this.frmMain = frmMain;
        }

        private void frmDynamicTable_Load(object sender, EventArgs e)
        {
            this.GetData(this.option);
        }

        #region Methods
        private void GetData(string option)
        {
            using (smartclocksrldbEntities db = new smartclocksrldbEntities())
            {
                if (option.Equals("employees"))
                {
                    var lst = from d in db.employees
                              select new 
                              { 
                                  id = d.id,
                                  name = d.name,
                                  lastname = d.lastname,
                                  address = d.address,
                                  birthdate = d.birthdate,
                                  phone = d.phone,
                                  email = d.email,
                                  admissiondate = d.admissionDate,
                                  position = d.position.name
                              };
                    dataGridView1.DataSource = lst.ToList();
                }
                if(option.Equals("checkin"))
                {
                    var lst = from d in db.checkin
                              select new
                              {
                                  id = d.id,
                                  employee = d.employees.name + " " + d.employees.lastname,
                                  Date = d.checkDate,
                                  Hour = d.checkHour,
                                  Observations = d.observations != null ? d.observations : "No hay observaciones"
                              };
                    dataGridView1.DataSource = lst.ToList();
                }
                if (option.Equals("checkout"))
                {
                    var lst = from d in db.checkout 
                              select new 
                             {
                                  id = d.id,
                                  employee = d.employees.name + " " + d.employees.lastname,
                                  Date = d.checkDate,
                                  Hour = d.checkHour,
                                  Observations = d.observations != null ? d.observations : "No hay observaciones"
                              };
                    dataGridView1.DataSource = lst.ToList();
                }
            }
        }
        #endregion

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (this.option.Equals("employees"))
                {
                    frmEmployeesInfo frmEmployeesInfo = new frmEmployeesInfo((int)dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    this.frmMain.openChildForm(frmEmployeesInfo);
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmExport frm = new frmExport();
            var result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                ExportarDataGridViewExcel(dataGridView1);
            }else if (result == DialogResult.Cancel)
            {
                toPdf(dataGridView1);
            }
        }

        private void ExportarDataGridViewExcel(DataGridView grd)
        {
            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "Excel (*.xls)|*.xls";
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application aplicacion;
                Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
                aplicacion = new Microsoft.Office.Interop.Excel.Application();
                libros_trabajo = aplicacion.Workbooks.Add();
                hoja_trabajo =
                    (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);
                //Recorremos el DataGridView rellenando la hoja de trabajo
                for (int i = 0; i < grd.ColumnCount; i++)
                {
                    hoja_trabajo.Cells[1, i + 1] = grd.Columns[i].HeaderText;
                }
                for (int i = 0; i < grd.Rows.Count; i++)
                {
                    for (int j = 0; j < grd.Columns.Count; j++)
                    {
                        hoja_trabajo.Cells[i + 2, j + 1] = grd.Rows[i].Cells[j].Value.ToString();
                    }
                }
                libros_trabajo.SaveAs(fichero.FileName,
                    Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                libros_trabajo.Close(true);
                aplicacion.Quit();
                MessageBox.Show("Data Exported Successfully !!!", "Info");
            }
        }

        private void toPdf(DataGridView dataGrid)
        {
            if (dataGrid.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Output.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(dataGrid.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dataGrid.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dataGrid.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }
                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }
        }
    }

}
