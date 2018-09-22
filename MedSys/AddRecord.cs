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
    public partial class AddRecord : Form
    {
        Doctor doctor;
        Patient patient;
        ModelMedContainer db = ControlFunctions.dbContext;


        public AddRecord(Patient patient, Doctor doctor)
        {
            this.patient = patient;
            this.doctor = doctor;

            InitializeComponent();
        }

        private void EditVisit_Load(object sender, EventArgs e)
        {
            
        }

        private void comboBoxCorpus_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxInfo.Text != "")
            {
                Record newRecord = ControlFunctions.CreateRecord(DateTime.Now, doctor, textBoxInfo.Text, patient.MedCard);
                string result = ControlFunctions.AddRecord(newRecord);
                if (result == null)
                    this.Close();
                else
                    MessageBox.Show(result);
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
