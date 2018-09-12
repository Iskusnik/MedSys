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
    public partial class EditCabinet : Form
    {
        Cabinet cabinet;
        ModelMedContainer db = new ModelMedContainer();


        public EditCabinet(Cabinet cabinet)
        {
            this.cabinet = cabinet;

            InitializeComponent();
        }

        private void EditVisit_Load(object sender, EventArgs e)
        {
            var corpusList = (from c in db.CorpusSet select c).ToArray();

            foreach (Corpus c in corpusList)
                comboBoxCorpus.Items.Add(c.Name);

            for(int i = 0; i < cabinet.Corpus.Floors; i++)
                comboBoxCorpus.Items.Add(i+1);


            comboBoxFloor.Text = cabinet.Floor.ToString();

            numericUpDown1.Value = cabinet.Num;
        }

        private void comboBoxCorpus_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxCorpus.SelectedIndex >= 0 &&
                comboBoxFloor.SelectedIndex >= 0)
            {
                int floor = (int)comboBoxFloor.SelectedItem;
                int num = (int)numericUpDown1.Value;

                ControlFunctions.EditCabinet(cabinet, comboBoxCorpus.Text, floor, num);

                this.Close();
            }
            else
                MessageBox.Show("Заполните поля");
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxCorpus_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            comboBoxFloor.Items.Clear();

            string corpusName = comboBoxFloor.SelectedText;

            Corpus corpus = (from c in db.CorpusSet where c.Name == corpusName select c).ToList()[0];

            for (int i = 0; i < corpus.Floors; i++)
                comboBoxFloor.Items.Add(i + 1);
        }
    }
}
