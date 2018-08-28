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
    public partial class PatientMenu : Form
    {
        public Patient patient;
        public PatientMenu(Patient patient)
        {
            this.patient = patient;
            InitializeComponent();
        }

        private void PatientMenu_Load(object sender, EventArgs e)
        {
            textBoxBirthDate.Text = patient.BirthDate.ToShortDateString();
            textBoxBloodType.Text = patient.BloodType;
            textBoxName.Text = patient.FullName;
            textBoxGender.Text = patient.Gender;
            textBoxInsuranceNum.Text = patient.InsuranceNum;
            textBoxDocType.Text = patient.Document.Type;
            textBoxDocumentNum.Text = patient.Document.Num;
            textBoxAdress.Text = patient.Adress;
        }

        private void записатьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form record = new PatientToDoctor(patient);
            record.Show();
        }

        private void просмотретьИсториюЗаписейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form showVisits = new ShowVisits(patient);
            showVisits.Show();
        }

        private void изменитьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void просмотретьИсториюБолезнейToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void записиВрачейToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
