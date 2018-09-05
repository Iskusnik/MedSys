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
    public partial class DoctorMenu : Form
    {
        public Doctor doctor;

        public DoctorMenu(Doctor doctor)
        {
            this.doctor = doctor;
            InitializeComponent();
        }

        private void DoctorMenu_Load(object sender, EventArgs e)
        {
            ReloadForm();
        }
        public void ReloadForm()
        {
            textBoxBirthDate.Text = doctor.BirthDate.ToShortDateString();
            textBoxJob.Text = doctor.Job.Name;
            textBoxName.Text = doctor.FullName;
            textBoxGender.Text = doctor.Gender;
            textBoxInsuranceNum.Text = doctor.InsuranceNum;
            textBoxDocType.Text = doctor.Document.Type;
            textBoxDocumentNum.Text = doctor.Document.Num;
            textBoxAdress.Text = doctor.Adress;
            textBoxEducation.Text = doctor.Education;
        }

        private void добавитьВрачаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form regist = new Registr(isDoctor: true);
            regist.ShowDialog();
        }

        private void добавитьПациентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form regist = new Registr(isDoctor: false);
            regist.ShowDialog();
        }
    }
}
