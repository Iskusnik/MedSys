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
    public partial class EditRecord : Form
    {
        Record record;
        ModelMedContainer db = ControlFunctions.dbContext;


        public EditRecord(Record record)
        {
            this.record = record;

            InitializeComponent();
        }

        private void EditVisit_Load(object sender, EventArgs e)
        {

            var patientsList = (from p in db.PersonSet where p is Patient select p).ToArray();
            var doctorsList = (from d in db.PersonSet where d is Doctor select d).ToArray();
            

            foreach (Person p in patientsList)
                comboBoxPatient.Items.Add(p.FullName + "_" + p.BirthDate.Date.ToString());
            comboBoxPatient.Text = record.MedCard.Patient.FullName + "_" + record.MedCard.Patient.BirthDate.Date.ToString();



            foreach (Person d in doctorsList)
                comboBoxDoctor.Items.Add(d.FullName + "_" + d.BirthDate.Date.ToString());
            comboBoxDoctor.Text = record.Doctor.FullName + "_" + record.Doctor.BirthDate.Date.ToString();


            textBoxInfo.Text = record.Info;
            datePicker.Value = record.Date.Date;
            timePicker.Value = record.Date;
        }

        private void comboBoxCorpus_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxInfo.Text == "" &&
                comboBoxPatient.SelectedIndex >= 0 &&
                comboBoxDoctor.SelectedIndex >= 0)
            {
                ControlFunctions.EditRecord(record, comboBoxDoctor.Text, comboBoxPatient.Text, textBoxInfo.Text, datePicker.Text, timePicker.Text);

                this.Close();
            }
            else
                MessageBox.Show("Заполните поля");
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
