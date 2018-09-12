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
    public partial class ControlIllnesses : Form
    {
        ModelMedContainer db = ControlFunctions.dbContext;

        public ControlIllnesses()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void ReloadForm(bool RefreshDataGrid)
        {
            if (RefreshDataGrid)
            {

                var patientInfo = (from patient in db.PersonSet where patient is Patient select new { Имя_пациента = patient.FullName, Дата_рождения = patient.BirthDate, Пол = patient.Gender, id = patient.Id }).ToList();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("Имя пациента", "Имя пациента");
                dataGridView1.Columns.Add("Дата рождения", "Дата рождения");
                dataGridView1.Columns.Add("Пол", "Пол");
                dataGridView1.Columns.Add("Id", "Id");
                dataGridView1.Columns["Id"].Visible = false;

                foreach (var s in patientInfo)
                {
                    dataGridView1.Rows.Add(s.Имя_пациента, s.Дата_рождения.Date, s.Пол, s.id);
                }

                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Refresh();
            }

                comboBoxJobs.Items.Clear();
                var jobs = (from Illness j in db.IllnessSet select j.Name).ToList();
                foreach (string j in jobs)
                    comboBoxJobs.Items.Add(j);
                comboBoxJobs.Items.Add("Все болезни");
                comboBoxJobs.SelectedText = "Все болезни";
            
        }
        private void ReloadData()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Имя пациента", "Имя пациента");
            dataGridView1.Columns.Add("Дата рождения", "Дата рождения");
            dataGridView1.Columns.Add("Пол", "Пол");
            dataGridView1.Columns.Add("Id", "Id");
            dataGridView1.Columns["Id"].Visible = false;

            if (comboBoxJobs.Text != "Все болезни")
            {
                string illnessName = comboBoxJobs.Text;
                Illness illness = (from ill in db.IllnessSet where illnessName == ill.Name select ill).ToList()[0];
                var patientInfo = (from patient in illness.MedCard select new { Имя_пациента = patient.Patient.FullName, Дата_рождения = patient.Patient.BirthDate, Пол = patient.Patient.Gender, id = patient.Patient.Id }).ToList();
                
                foreach (var s in patientInfo)
                {
                    dataGridView1.Rows.Add(s.Имя_пациента, s.Дата_рождения.Date, s.Пол, s.id);
                }
            }
            else
            {
                var patientInfo = (from patient in db.PersonSet where patient is Patient select new { Имя_пациента = patient.FullName, Дата_рождения = patient.BirthDate, Пол = patient.Gender, id = patient.Id }).ToList();

                foreach (var s in patientInfo)
                {
                    dataGridView1.Rows.Add(s.Имя_пациента, s.Дата_рождения.Date, s.Пол, s.id);
                }
            }
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Refresh();



        }
        private void ChangeJobs_Load(object sender, EventArgs e)
        {
            ReloadForm(RefreshDataGrid: false);
        }

        private void buttonRemoveDocFromJob_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0 || dataGridView1.SelectedCells[0].Value == null)
                MessageBox.Show("Выберите пациента", "Ошибка");
            else
            {
                int id = (int)dataGridView1.SelectedCells[3].Value;
                Patient patient = (Patient)db.PersonSet.Find(id);

                Form showEditDeleteIllnesses = new ShowEditDeleteIllnesses(patient);
                showEditDeleteIllnesses.ShowDialog();
            }
            ReloadForm(true);
        }

        private void buttonDeleteJob_Click(object sender, EventArgs e)
        {
            if (comboBoxJobs.Text != "" && comboBoxJobs.Text != "Все болезни")
            {
                string illnessName = comboBoxJobs.Text;

                if (MessageBox.Show(text: "Все пациенты потеряют данную болезнь, продолжить?",
                                            caption: "Продолжить?",
                                            buttons: MessageBoxButtons.YesNo,
                                            icon: MessageBoxIcon.Question) == DialogResult.Yes)
                    ControlFunctions.RemoveIllness(illnessName);
                else;


                ReloadForm(true);
            }
            else
                MessageBox.Show("Выберите болезнь");
        }

        private void buttonAddJob_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "" || textBoxName.Text == "Все болезни")
                MessageBox.Show("Введите название болезни");
            else
            {
                string illnessName = textBoxName.Text;
                string illnessInfo = textBoxInfo.Text;
                string result;
                Illness newIllness;


                newIllness = ControlFunctions.CreateIllness(illnessInfo, illnessName);
                result = ControlFunctions.AddIllness(newIllness);
                if (result != null)
                    MessageBox.Show(result, "Ошибка");


                ReloadForm(true);
            }
        }

        private void comboBoxJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadData();
        }
    }
}
