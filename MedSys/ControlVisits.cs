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
    public partial class ControlVisits : Form
    {
        ModelMedContainer db = ControlFunctions.dbContext;

        public ControlVisits()
        {
            InitializeComponent();
        }

        private void ControlVisits_Load(object sender, EventArgs e)
        {
            var doctorsList = (from pers in db.PersonSet where pers is Doctor select pers).ToList();
            var patientList = (from pers in db.PersonSet where pers is Patient select pers).ToList();

            foreach (Person p in doctorsList)
            {
                string keyName = p.FullName + "_" + p.BirthDate.ToShortDateString();
                comboBoxDoctors.Items.Add(keyName);
            }
            comboBoxDoctors.Items.Add("Любой врач");

            foreach (Person p in patientList)
            {
                string keyName = p.FullName + "_" + p.BirthDate.ToShortDateString();
                comboBoxPatients.Items.Add(keyName);
            }
            comboBoxPatients.Items.Add("Любой пациент");

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string doctorIf = comboBoxDoctors.Text;
            string patientIf = comboBoxPatients.Text;
            string dateIf = comboBoxDate.Text;

            var visits = (from v in db.TimeForVisitSet select v).ToList();


            DateTime day = datePicker.Value.Date;
            if (dateIf == "В этот день")
                visits = (from v in visits where v.VisitTime.Date == day select v).ToList();
            else if(dateIf == "Раньше этого дня")
                visits = (from v in visits where v.VisitTime.Date < day select v).ToList();
            else if (dateIf == "Позже этого дня")
                visits = (from v in visits where v.VisitTime.Date > day select v).ToList();

            if (doctorIf != "Любой врач" && doctorIf != "")
            {
                string name = comboBoxDoctors.Text.Split('_')[0];
                DateTime birthDate = DateTime.Parse(comboBoxDoctors.Text.Split('_')[1]);
                Doctor doctor = (Doctor)(from pers in db.PersonSet
                                         where pers.FullName == name &&
                                         pers.BirthDate == birthDate
                                         select pers).ToList()[0];

                visits = (visits.Intersect(doctor.TimeForVisit.ToArray())).ToList();
            }

            if (patientIf != "Любой пациент" && patientIf != "")
            {
                string name = comboBoxPatients.Text.Split('_')[0];
                DateTime birthDate = DateTime.Parse(comboBoxPatients.Text.Split('_')[1]);
                Patient patient = (Patient)(from pers in db.PersonSet
                                            where pers.FullName == name &&
                                            pers.BirthDate == birthDate
                                            select pers).ToList()[0];
                 
                visits = (visits.Intersect(patient.TimeForVisit.ToArray())).ToList();
            }
            if (visits == null || visits.Count == 0)
            {
                MessageBox.Show("По данному запросу ничего не найдено");
                LoadData(null);
            }
            else
                LoadData(visits);
        }

        private void LoadData(List<TimeForVisit> visits)
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("Имя врача", "Имя врача");
            dataGridView1.Columns.Add("Имя пациента", "Имя пациента");
            dataGridView1.Columns.Add("Время", "Время");
            dataGridView1.Columns.Add("id", "id");

            dataGridView1.Columns["id"].Visible = false;

            dataGridView1.RowHeadersVisible = false;
            if (visits != null)
            {
                List<TimeForVisit> visitsFree = (from tfv in visits where tfv.Patient == null select tfv).ToList();
                visits = visits.Except(visitsFree).ToList();

                foreach (TimeForVisit visit in visits)
                    dataGridView1.Rows.Add(visit.Doctor.FullName, visit.Patient.FullName, visit.VisitTime, visit.Id);

                foreach (TimeForVisit visit in visitsFree)
                    dataGridView1.Rows.Add(visit.Doctor.FullName, "Запись свободна", visit.VisitTime, visit.Id);

            }
            dataGridView1.Refresh();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Form addVisit = new AddVisit();
            addVisit.ShowDialog();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count != 0 && dataGridView1.SelectedCells[0].Value != null)
            {
                int id = (int)dataGridView1.SelectedCells[3].Value;
                ControlFunctions.RemoveVisit(id);

                LoadData(db.TimeForVisitSet.ToList());
            }
            else
                MessageBox.Show("Выберите приём для удаления");
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count != 0)
            {
                int id = (int)dataGridView1.SelectedCells[3].Value;
                TimeForVisit visit = db.TimeForVisitSet.Find(id);
                Form editVisit = new EditVisit(visit);
                editVisit.ShowDialog();
                LoadData(db.TimeForVisitSet.ToList());
            }
            else
                MessageBox.Show("Выберите приём для редактирования");
        }
    }
}
