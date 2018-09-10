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
    public partial class DoctorDetails : Form
    {
        public Doctor doctor;

        public DoctorDetails(Doctor doctor)
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

        }

        private void ClinicManageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void редактироватьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form changePersonInfo = new ChangePersonInfo(doctor);
            changePersonInfo.Owner = this;
            changePersonInfo.Show();
        }
    }
}
