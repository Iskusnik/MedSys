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
    public partial class EditVisit : Form
    {
        TimeForVisit visit;
        ModelMedContainer db = ControlFunctions.dbContext;


        public EditVisit(TimeForVisit visit)
        {
            this.visit = visit;

            InitializeComponent();
        }

        private void EditVisit_Load(object sender, EventArgs e)
        {
            

            var patientsList = (from p in db.PersonSet where p is Patient select p).ToArray();
            var doctorsList = (from d in db.PersonSet where d is Doctor select d).ToArray();
            

            foreach (Person p in patientsList)
                comboBoxPatient.Items.Add(p.FullName + "_" + p.BirthDate.Date.ToString());
            if (visit.Patient != null)
                comboBoxPatient.Text = visit.Patient.FullName + "_" + visit.Patient.BirthDate.Date.ToString();



            foreach (Person d in doctorsList)
                comboBoxDoctor.Items.Add(d.FullName + "_" + d.BirthDate.Date.ToString());
            comboBoxDoctor.Text = visit.Doctor.FullName + "_" + visit.Doctor.BirthDate.Date.ToString();




            var corpusList = (from c in db.CorpusSet select c).ToArray();

            foreach (Corpus c in corpusList)
                comboBoxCorpus.Items.Add(c.Name);
            comboBoxCorpus.Text = visit.Cabinet.Corpus.Name;


            comboBoxCabinet.Items.Clear();
            foreach (Cabinet c in visit.Cabinet.Corpus.Cabinet)
                comboBoxCabinet.Items.Add(c.Num);
            comboBoxCabinet.Text = visit.Cabinet.Num.ToString();
            comboBoxPatient.Items.Add("Нет пациента");

        }

        private void comboBoxCorpus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string corpusName = comboBoxCorpus.Text;
            comboBoxCabinet.Items.Clear();
            Corpus corpus = (from c in db.CorpusSet where c.Name == corpusName select c).ToList().First(); 

            foreach (Cabinet c in corpus.Cabinet)
                comboBoxCabinet.Items.Add(c.Num);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxCabinet.SelectedIndex >= 0 &&
                comboBoxCorpus.SelectedIndex >= 0 &&
                comboBoxCorpus.SelectedIndex >= 0 &&
                comboBoxDoctor.SelectedIndex >= 0)
            {
                ControlFunctions.EditVisit(visit, comboBoxDoctor.Text, comboBoxPatient.Text, comboBoxCorpus.Text, comboBoxCabinet.Text, datePicker.Text, timePicker.Text);

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
