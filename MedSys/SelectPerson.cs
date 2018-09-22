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
    public partial class SelectPerson : Form
    {
        ModelMedContainer db = ControlFunctions.dbContext;
        int function, docId;
        public SelectPerson(int docId, int function = 0)
        {
            this.docId = docId;
            this.function = function;
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonFind_Click(object sender, EventArgs e)
        {

            string name = textBoxName.Text;
            DateTime date = dateTimePicker1.Value.Date;
            List<Person> searchResult = db.PersonSet.ToList();

            try
            {
                if (checkBoxName.Checked)
                    searchResult = (from d in searchResult where (d.FullName == name) select d).ToList();

                if (checkBoxBirth.Checked)
                {
                    switch (comboBoxBirth.SelectedIndex)
                    {
                        case 0: searchResult = (from d in searchResult where (d.BirthDate < date) select d).ToList(); break;
                        case 1: searchResult = (from d in searchResult where (d.BirthDate <= date) select d).ToList(); break;
                        case 2: searchResult = (from d in searchResult where (d.BirthDate == date) select d).ToList(); break;
                        case 3: searchResult = (from d in searchResult where (d.BirthDate > date) select d).ToList(); break;
                        case 4: searchResult = (from d in searchResult where (d.BirthDate >= date) select d).ToList(); break;
                    }
                }
                if (checkBoxDocs.Checked)
                    searchResult = (from d in searchResult where (d is Doctor) select d).ToList();
                if (checkBoxPat.Checked)
                    searchResult = (from d in searchResult where (d is Patient) select d).ToList();

                FillDataGridView(searchResult);
                dataGridView1.Refresh();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Искомые люди не обнаружены");
            }
        }


        private void buttonSelectPerson_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0 && dataGridView1.SelectedCells[0].Value != null)
            {
                int columnIndex = dataGridView1.Columns["Id"].Index;
                int id = (int)dataGridView1.SelectedCells[columnIndex].Value;
                Person person = db.PersonSet.Find(id);

                if (person is Patient)
                {

                    Doctor doctor = (Doctor)db.PersonSet.Find(docId);
                    Form patientDetails = new PatientDetails(person as Patient, doctor);
                    patientDetails.ShowDialog();
                }
                else
                {
                    Form doctorDetails = new DoctorDetails(person as Doctor);
                    doctorDetails.ShowDialog();
                }
            }
            else
                MessageBox.Show("Человек не выбран");
        }

        private void checkBoxName_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxDocs_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDocs.Checked)
                checkBoxPat.Checked = false;
        }

        private void checkBoxPat_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPat.Checked)
                checkBoxDocs.Checked = false;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0 && dataGridView1.SelectedCells[0] != null)
            {
                ControlFunctions.RemovePerson((int)dataGridView1.SelectedCells[4].Value);
                FillDataGridView(db.PersonSet.ToList());
                dataGridView1.Refresh();
            }
            else
                MessageBox.Show("Человек не выбран");
        }

        private void SelectPerson_Load(object sender, EventArgs e)
        {


            dataGridView1.Columns.Add("ФИО", "ФИО");
            dataGridView1.Columns.Add("ДР", "ДР");
            dataGridView1.Columns.Add("Пол", "Пол");
            dataGridView1.Columns.Add("Врач", "Врач");
            dataGridView1.Columns.Add("Id", "Id");
            dataGridView1.Columns["Id"].Visible = false;

            if (function == 0)
            {
                comboBoxBirth.SelectedIndex = 2;

                dataGridView1.ReadOnly = true;

                var doctors = (from pers in db.PersonSet
                               where pers is Doctor
                               select new
                               {
                                   ФИО = pers.FullName,
                                   ДР = pers.BirthDate,
                                   Пол = pers.Gender,
                                   Врач = "Врач",
                                   Id = pers.Id
                               }).ToList();

                

                
                foreach (var pers in doctors)
                    dataGridView1.Rows.Add(pers.ФИО, pers.ДР, pers.Пол, pers.Врач, pers.Id);
                

               
            }
            else
            {
                checkBoxDocs.Visible = false;
                checkBoxDocs.Checked = false;

                checkBoxPat.Visible = false;
                checkBoxPat.Checked = true;

            }

            var patients = (from pers in db.PersonSet
                            where pers is Patient
                            select new
                            {
                                ФИО = pers.FullName,
                                ДР = pers.BirthDate,
                                Пол = pers.Gender,
                                Врач = "Пациент",
                                Id = pers.Id
                            }).ToList();

            foreach (var pers in patients)
                dataGridView1.Rows.Add(pers.ФИО, pers.ДР.Date, pers.Пол, pers.Врач, pers.Id);
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Refresh();
        }


        private void FillDataGridView(List<Person> people)
        {

            dataGridView1.Rows.Clear();

            var doctors = (from pers in people
                           where pers is Doctor &&
                                 pers.Id != docId
                           select new
                           {
                               ФИО = pers.FullName,
                               ДР = pers.BirthDate,
                               Пол = pers.Gender,
                               Врач = "Врач",
                               Id = pers.Id
                           }).ToList();

            var patients = (from pers in people
                            where pers is Patient
                            select new
                            {
                                ФИО = pers.FullName,
                                ДР = pers.BirthDate,
                                Пол = pers.Gender,
                                Врач = "Пациент",
                                Id = pers.Id
                            }).ToList();
            if(function == 0)
             foreach (var pers in doctors)
                    dataGridView1.Rows.Add(pers.ФИО, pers.ДР.Date, pers.Пол, pers.Врач, pers.Id);

            foreach (var pers in patients)
                dataGridView1.Rows.Add(pers.ФИО, pers.ДР.Date, pers.Пол, pers.Врач, pers.Id);

            //dataGridView1.Rows.RemoveAt(patients.Count + doctors.Count);
        }
    }
}
