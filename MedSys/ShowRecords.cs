using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedSys
{
    public partial class ShowRecords : Form
    {
        Patient patient;
        Doctor doctor;
        ModelMedContainer db = ControlFunctions.dbContext;
        int function;

        public ShowRecords(Patient patient, int function = 0, Doctor doctor = null)
        {
            this.function = function;
            this.patient = patient;
            this.doctor = doctor;

            InitializeComponent();
        }

        private void ShowVisitRecords_Load(object sender, EventArgs e)
        {
            if (function == 1)
            {
                buttonRemove.Visible = true;
                buttonAdd.Visible = true;
            }
            else
            {
                buttonRemove.Visible = false;
                buttonAdd.Visible = false;
            }

            RefreshData();
        }

        private void RefreshData()
        {
            dataGridView1.Columns.Clear();

            patient = (Patient)db.PersonSet.Find(patient.Id);
            var thisPersonVisits = (from record in patient.MedCard.Record select new { Время_записи = record.Date, Имя_врача = record.Doctor.FullName, id = record.Id }).ToList();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Время записи", "Время записи");
            dataGridView1.Columns.Add("Имя врача", "Имя врача");
            dataGridView1.Columns.Add("Id", "Id");
            dataGridView1.Columns["Id"].Visible = false;

            foreach (var s in thisPersonVisits)
                dataGridView1.Rows.Add(s.Время_записи, s.Имя_врача, s.id);

            dataGridView1.Sort(dataGridView1.Columns["Время записи"], ListSortDirection.Descending);
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Refresh();
        }
       

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells != null && dataGridView1.SelectedCells.Count != 0)
            {
                patient = (Patient)db.PersonSet.Find(patient.Id);
                int id = (int)dataGridView1.SelectedRows[0].Cells[2].Value;
                Record temp = db.RecordSet.Find(id);

                Form showInfoAboutRecord = new ShowInfoAboutRecord(temp);
                showInfoAboutRecord.Show();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count != 0)
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells[2].Value;
                ControlFunctions.RemoveRecord(id);
                RefreshData();
            }
            else
                MessageBox.Show("Выберите запись для удаления");
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Form addRecord = new AddRecord(patient, doctor);
            addRecord.ShowDialog();
            RefreshData();
        }
    }
}
