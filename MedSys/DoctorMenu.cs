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


            string jobs = "";
            foreach (Job j in doctor.Job)
                jobs += j.Name + " ";

            textBoxJob.Text = jobs;
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

        private void пациентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form selectPerson = new SelectPerson(doctor.Id, 1);
            selectPerson.ShowDialog();
        }

        private void ClinicManageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void приёмыПациентовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form showVisits = new ShowVisitsForDoctor(doctor);
            showVisits.ShowDialog();
        }

        private void найтиЧеловекаИИзменитьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form selectPerson = new SelectPerson(doctor.Id, 0);
            selectPerson.ShowDialog();
        }

        private void редактироватьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form changePersonInfo = new ChangePersonInfo(doctor);
            changePersonInfo.Owner = this;
            changePersonInfo.Show();
        }

        private void изменитьСписокСпециальностейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form сontrolJobs = new ControlJobs();
            сontrolJobs.ShowDialog();
        }

        private void управлениеБолезнямиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form сontrolIllnesses = new ControlIllnesses();
            сontrolIllnesses.ShowDialog();
        }

        private void управлениеКабинетамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form сontrolCabinets = new ControlCabinets();
            сontrolCabinets.ShowDialog();
        }
        

        private void управлениеРасписаниемВрачейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form сontrolVisits = new ControlVisits();
            сontrolVisits.ShowDialog();
        }
        

        private void управлениеКорпусамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form сontrolCorpuses = new ControlCorpuses();
            сontrolCorpuses.ShowDialog();
        }

        private void конструкторЗапросовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form queryBuilder = new QueryBuilderMedSys();
            queryBuilder.ShowDialog();
        }
    }
}
