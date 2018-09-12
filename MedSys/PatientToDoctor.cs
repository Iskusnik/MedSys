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
    public partial class PatientToDoctor : Form
    {

        Doctor[] DoctorsList;
        int index;
        Patient patient;
        ModelMedContainer db = ControlFunctions.dbContext;

        public PatientToDoctor(Patient patient)
        {
            DoctorsList = null;
            this.patient = patient;
            InitializeComponent();
        }

        private void PatientToDoctor_Load(object sender, EventArgs e)
        {

            string[] jobs = (from job in db.JobSet select job.Name).ToArray();

            foreach (string job in jobs)
                this.comboBoxJob.Items.Add(job);

            this.comboBoxJob.Items.Remove("Главврач");

            var timeForVisit = (from t in db.TimeForVisitSet
                                where t.Patient == null
                                select t).ToList();
            if (timeForVisit.Count == 0)
            {
                MessageBox.Show("Свободных для записи врачей нет");
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxTime.Text == "")
                MessageBox.Show("Выберите специальность, врача, день и время");
            else
            {


                Doctor tempDoctor = (Doctor)db.PersonSet.Find(DoctorsList[index].Id);
                DateTime visitDateTime = DateTime.Parse(comboBoxDate.Text);

                visitDateTime = visitDateTime.Add(TimeSpan.FromTicks(DateTime.Parse(comboBoxTime.Text).TimeOfDay.Ticks));


                TimeForVisit visit = (from time in db.TimeForVisitSet
                                      where time.Doctor.Id == tempDoctor.Id &&
                                            time.VisitTime == visitDateTime
                                      select time).ToArray()[0];

                comboBoxTime.Items.Remove(comboBoxTime.Text);



                ControlFunctions.AddTimeForVisitToPatient(patient, visit);

                db.SaveChanges();
                MessageBox.Show("Запись совершена");
                button1.Enabled = false;

                var timeForVisit = (from t in db.TimeForVisitSet
                                    where t.Patient == null
                                    select t).ToList();
                if (timeForVisit.Count == 0)
                {
                    MessageBox.Show("Свободных для записи врачей нет");
                    Close();
                }
            }
        }

        private void comboBoxJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = false;
            this.comboBoxDoctor.Items.Clear();


            object[] temp = (from doctor in db.PersonSet where (doctor is Doctor) select (doctor)).ToArray();
            string s = comboBoxJob.Text;
            Job selectedJob = (from Job j in db.JobSet where j.Name == s select j).ToArray()[0];
            DoctorsList = (selectedJob.Doctor).ToArray();


            foreach (Doctor doct in DoctorsList)
                this.comboBoxDoctor.Items.Add(doct.FullName);
        }

        private void comboBoxDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDoctor.SelectedIndex != -1)
            {
                button1.Enabled = false;
                this.comboBoxDate.Items.Clear();

                index = comboBoxDoctor.SelectedIndex;

                try
                {
                    Doctor tempDoctor = DoctorsList[index];
                    var searchResult = (from doc in db.PersonSet where doc is Doctor && doc.BirthDate == tempDoctor.BirthDate && doc.FullName == tempDoctor.FullName select doc).ToList();
                    Doctor d = (Doctor)searchResult[0];

                    string[] distinct = (from dates in d.TimeForVisit where dates.Patient == null select dates.VisitTime.ToShortDateString()).Distinct().ToArray();

                    foreach (string doct in distinct)
                        this.comboBoxDate.Items.Add(doct);
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("На данный момент нет времени для записи к этому врачу");
                }
                
            }
        }

        private void comboBoxDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = false;
            this.comboBoxTime.Items.Clear();
            Doctor tempDoc = DoctorsList[index];
            var searchResult = (from doc in db.PersonSet where doc is Doctor && doc.BirthDate == tempDoc.BirthDate && doc.FullName == tempDoc.FullName select doc).ToList();
            Doctor d = (Doctor)searchResult[0];
            string s = comboBoxDate.Text;
            //string[] distinct = (from dates in d.FreeTimes where (dates.Start.ToShortDateString() == s) select (dates.Start.TimeOfDay.ToString())).ToArray();
            string[] distinct = (from dates in d.TimeForVisit where dates.Patient == null && dates.VisitTime.ToShortDateString() == s select dates.VisitTime.TimeOfDay.ToString()).ToArray();
            foreach (string doct in distinct)
                this.comboBoxTime.Items.Add(doct);

        }

        private void comboBoxTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTime.Text != "")
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }
    }
}