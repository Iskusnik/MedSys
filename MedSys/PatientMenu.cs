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
    }
}
