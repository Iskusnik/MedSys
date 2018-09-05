using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Med2
{
    public partial class SelectPerson : Form
    {
        int function;
        public SelectPerson(int func = 0)
        {
            function = func;
            InitializeComponent();
            if (func == 0)
            {
                checkBoxDocs.Show();
                checkBoxPat.Show();
                buttonDelete.Show();
                using (ModelMedDBContainer db = new ModelMedDBContainer())
                {
                    foreach (Person per in db.PersonSet)
                        comboBox1.Items.Add(per.FullName + "_" + per.BirthDate.ToShortDateString());
                    comboBox1.SelectedIndex = 0;
                }
            }
            if (func == 1)
            {
                checkBoxDocs.Hide();
                checkBoxPat.Hide();
                buttonDelete.Hide();
                checkBoxPat.Checked = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonFind_Click(object sender, EventArgs e)
        {

            using (ModelMedDBContainer db = new ModelMedDBContainer())
            {
                string name = textBoxName.Text;
                DateTime date = dateTimePicker1.Value.Date;
                List<Person> searchResult = db.PersonSet.ToList();

                try
                {
                    if (checkBoxName.Checked)
                        searchResult = (from d in searchResult where (d.FullName == name) select d).ToList();

                    if (checkBoxBirth.Checked)
                    {
                        switch (comboBoxBirth.SelectedIndex)
                        {
                            case 0: searchResult = (from d in searchResult where (d.BirthDate < date) select d).ToList(); break;
                            case 1: searchResult = (from d in searchResult where (d.BirthDate <= date) select d).ToList(); break;
                            case 2: searchResult = (from d in searchResult where (d.BirthDate == date) select d).ToList(); break;
                            case 3: searchResult = (from d in searchResult where (d.BirthDate > date) select d).ToList(); break;
                            case 4: searchResult = (from d in searchResult where (d.BirthDate >= date) select d).ToList(); break;
                        }
                    }
                    if (checkBoxDocs.Checked)
                        searchResult = (from d in searchResult where (d is Doctor) select d).ToList();
                    if (checkBoxPat.Checked)
                        searchResult = (from d in searchResult where (d is Patient) select d).ToList();

                    comboBox1.Items.Clear();
                    foreach (Person per in searchResult)
                        comboBox1.Items.Add(per.FullName + "_" + per.BirthDate.ToShortDateString());
                    if (comboBox1.Items.Count != 0)
                        comboBox1.SelectedIndex = 0;
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Искомые люди не обнаружены");
                }
            }
        }

        private void buttonSelectPerson_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
                using (ModelMedDBContainer db = new ModelMedDBContainer())
                {
                    string[] personInfo = comboBox1.Text.Split('_');
                    string[] birthInfo = personInfo[1].Split('.');
                    long hash = (long)(personInfo[0]).GetHashCode();
                    DateTime birth = new DateTime(int.Parse(birthInfo[2]), int.Parse(birthInfo[1]), int.Parse(birthInfo[0]));
                    Person pers = db.PersonSet.Find(birth, hash);
                    if (function == 0)
                    {
                        Form changeInfo = new ChangePersonInfo(pers);
                        changeInfo.Owner = this;
                        changeInfo.ShowDialog();
                    }
                    if (function == 1)
                    {
                        Form changeMedCard = new ChangeMedCard(((DoctorMenu)Owner).thisDoctor, (Patient)pers);
                        changeMedCard.Show();
                    }
                }
            else
                MessageBox.Show("Человек не выбран");
        }

        private void checkBoxName_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxDocs_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDocs.Checked)
                checkBoxPat.Checked = false;
        }

        private void checkBoxPat_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPat.Checked)
                checkBoxDocs.Checked = false;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
                using (ModelMedDBContainer db = new ModelMedDBContainer())
                {
                    string[] personInfo = comboBox1.Text.Split('_');
                    string[] birthInfo = personInfo[1].Split('.');
                    long hash = (long)(personInfo[0]).GetHashCode();
                    DateTime birth = new DateTime(int.Parse(birthInfo[2]), int.Parse(birthInfo[1]), int.Parse(birthInfo[0]));
                    Person pers = db.PersonSet.Find(birth, hash);
                    db.PersonSet.Remove(pers);
                }
            else
                MessageBox.Show("Человек не выбран");
        }

        private void SelectPerson_Load(object sender, EventArgs e)
        {
            comboBoxBirth.SelectedIndex = 2;
        }
    }
}
