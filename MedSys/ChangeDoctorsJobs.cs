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
    public partial class ChangeDoctorsJobs : Form
    {
        Doctor doctor;
        string[] freeJobs;
        string[] currJobs;
        ModelMedContainer db = ControlFunctions.dbContext;

        public ChangeDoctorsJobs(Doctor doctor)
        {
            this.doctor = doctor;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBoxFreeJobs.SelectedItem != null)
            {
                string selectedJob = (string)listBoxFreeJobs.SelectedItem;
                string result = ControlFunctions.AddJobToDoctor(doctor.Id, selectedJob);
                if (result != null)
                    MessageBox.Show(result);
                else
                    RefreshForm();
            }
            else
            {
                MessageBox.Show("Выберите работу");
            }
}

        private void ChangeDoctorsJobs_Load(object sender, EventArgs e)
        {
            RefreshForm();

        }
        private void RefreshForm()
        {
            listBoxFreeJobs.Items.Clear();
            listBoxCurrentJobs.Items.Clear();

            currJobs = (from Job job in doctor.Job select job.Name).ToArray();
            freeJobs = (from Job job in db.JobSet select job.Name).Except(currJobs).ToArray();

            foreach (string job in currJobs)
                listBoxCurrentJobs.Items.Add(job);

            foreach (string job in freeJobs)
                listBoxFreeJobs.Items.Add(job);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxCurrentJobs.SelectedItem != null)
            {
                string selectedJob = (string)listBoxCurrentJobs.SelectedItem;
                string result = ControlFunctions.RemoveJobFromDoctor(doctor.Id, selectedJob);
                if (result != null)
                    MessageBox.Show(result);
                else
                    RefreshForm();
            }
            else
            {
                MessageBox.Show("Выберите работу");
            }
        }
    }
}
