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
    public partial class AddVisit : Form
    {
        ModelMedContainer db = ControlFunctions.dbContext;


        public AddVisit()
        {
            InitializeComponent();
        }

        private void EditVisit_Load(object sender, EventArgs e)
        {

            
            var doctorsList = (from d in db.PersonSet where d is Doctor select d).ToArray();
            

            if (doctorsList.Count() == 0)
            {
                MessageBox.Show("Добавьте врачей");
                this.Close();
            }


            foreach (Person d in doctorsList)
                comboBoxDoctor.Items.Add(d.FullName + "_" + d.BirthDate.Date.ToString());




            var corpusList = (from c in db.CorpusSet select c).ToArray();

            if (corpusList.Count() == 0)
            {
                MessageBox.Show("Добавьте корпусы");
                this.Close();
            }

            foreach (Corpus c in corpusList)
                comboBoxCorpus.Items.Add(c.Name);
            comboBoxCorpus.SelectedIndex = 0;

            string corpusName = comboBoxCorpus.Text;

            Corpus corpus = (from c in db.CorpusSet where c.Name == corpusName select c).ToList()[0];

            if (corpus.Cabinet.Count() == 0)
            {
                MessageBox.Show("Добавьте кабинеты в корпус");
                this.Close();
            }

            comboBoxCabinet.Items.Clear();
            foreach (Cabinet c in corpus.Cabinet)
                comboBoxCabinet.Items.Add(c.Num);

        }

        private void comboBoxCorpus_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxCabinet.Items.Clear();
            string corpusName = comboBoxCorpus.Text;

            Corpus corpus = (from c in db.CorpusSet where c.Name == corpusName select c).ToList()[0]; 

            foreach (Cabinet c in corpus.Cabinet)
                comboBoxCabinet.Items.Add(c.Num);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(comboBoxCabinet.SelectedIndex >= 0 &&
                comboBoxCorpus.SelectedIndex >= 0 &&
                comboBoxDoctor.SelectedIndex >= 0)
            {
                TimeForVisit tfv = ControlFunctions.CreateTimeForVisit(comboBoxDoctor.Text, comboBoxCorpus.Text, comboBoxCabinet.Text, datePicker.Text, timePicker.Text);
                string result = ControlFunctions.AddTimeForVisit(tfv);
                if (result != null)
                    MessageBox.Show(result);
                else
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
