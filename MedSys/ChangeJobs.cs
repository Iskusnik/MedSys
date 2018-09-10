﻿using System;
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
    public partial class ChangeJobs : Form
    {
        ModelMedContainer db = new ModelMedContainer();

        public ChangeJobs()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void ReloadForm(bool RefreshDataGrid)
        {
            comboBoxJobs.Items.Clear();
            var jobs = (from Job j in db.JobSet select j.Name).ToList();
            foreach (string j in jobs)
                comboBoxJobs.Items.Add(j);

            if (RefreshDataGrid)
            {

                var doctorsInfo = (from doctor in db.PersonSet where doctor is Doctor select new { Имя_врача = doctor.FullName, Работы = (doctor as Doctor).Job, id = doctor.Id }).ToList();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("Имя врача", "Имя врача");
                dataGridView1.Columns.Add("Должность", "Должность");
                dataGridView1.Columns.Add("Id", "Id");
                dataGridView1.Columns["Id"].Visible = false;

                foreach (var s in doctorsInfo)
                {
                    string doctorsJobs = "";

                    foreach (Job j in s.Работы)
                        doctorsJobs += j.Name + " ";

                    dataGridView1.Rows.Add(s.Имя_врача, doctorsJobs, s.id);
                }
                dataGridView1.Sort(dataGridView1.Columns["Время записи"], ListSortDirection.Descending);
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Refresh();

                dataGridView1.Refresh();
            }
            else;
        }

        private void ChangeJobs_Load(object sender, EventArgs e)
        {
            ReloadForm(RefreshDataGrid: false);
        }

        private void buttonRemoveDocFromJob_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                MessageBox.Show("Выберите врача", "Ошибка");
            else
            {
                int id = (int)dataGridView1.SelectedCells[2].Value;
                Doctor doctor = (Doctor)db.PersonSet.Find(id);

                Form changeDoctorsJobs = new ChangeDoctorsJobs(doctor);
                changeDoctorsJobs.ShowDialog();
            }
        }

        private void buttonDeleteJob_Click(object sender, EventArgs e)
        {
            string jobName = comboBoxJobs.Text;


            if (jobName == "Главврач" || jobName == "Нет должности")
                MessageBox.Show(text: "Нельзя удалить Главврача и пункт \"Нет должности\"",
                                caption: "Нельзя",
                                buttons: MessageBoxButtons.OK,
                                icon: MessageBoxIcon.Error);
            else if (MessageBox.Show(text: "Все врачи потеряют данную должность, продолжить?",
                                     caption: "Продолжить?",
                                     buttons: MessageBoxButtons.YesNo,
                                     icon: MessageBoxIcon.Question) == DialogResult.Yes)
                ControlFunctions.RemoveJob(jobName);
            else;
        }

        private void buttonAddJob_Click(object sender, EventArgs e)
        {
            string jobName = textBoxNewJob.Text;
            string result;
            Job newJob;


            newJob = ControlFunctions.CreateJob(jobName);
            result = ControlFunctions.AddJob(newJob);
            if (result != null)
                MessageBox.Show(result, "Ошибка");
        }
    }
}
