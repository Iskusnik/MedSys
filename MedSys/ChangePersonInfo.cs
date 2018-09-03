using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace MedSys
{
    public partial class ChangePersonInfo : Form
    {
        Person person;
        ModelMedContainer db = new ModelMedContainer();
        bool saved;
        public ChangePersonInfo(Person person)
        {
            this.person = db.PersonSet.Find(person.Id);
            InitializeComponent();
        }

        private void ChangePersonInfo_Load(object sender, EventArgs e)
        {
            this.Text = "Изменение данных:" + this.person.FullName;

            textBoxName.Text = person.FullName;
            dateTimePickerBirthDate.Value = person.BirthDate;
            string docType = person.Document.Type;
            comboBoxDocType.Text = docType;
            textBoxDocumentNum.Text = person.Document.Num;
            textBoxAdress.Text = person.Adress;
            comboBoxGender.Text = person.Gender;
            textBoxInsurance.Text = person.InsuranceNum;
            textBoxPassword.Text = person.Password;
            if (person is Patient)
            {
                string bloodType = (person as Patient).BloodType;
                comboBoxBlood.Text = bloodType;

                labelJob.Hide();
                comboBoxJob.Hide();

                labelEducation.Hide();
                textBoxEducation.Hide();

                labelBloodType.Show();
                comboBoxBlood.Show();
            }
            else;
            if (person is Doctor)
            {

                var jobs = db.JobSet;
                foreach (Job j in jobs)
                    comboBoxJob.Items.Add(j.Name);

                comboBoxJob.Text = (person as Doctor).Job.Name;
                textBoxEducation.Text = (person as Doctor).Education;


                labelJob.Show();
                comboBoxJob.Show();

                labelEducation.Show();
                textBoxEducation.Show();

                labelBloodType.Hide();
                comboBoxBlood.Hide();
            }
            else;

            saved = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = TextBoxesCheck();
            if (s != null)
            {
                MessageBox.Show(s, "Исправьте ошибки");
            }
            else
            {
                Job job;
                if (person is Doctor)
                {
                    var jobs = (from j in db.JobSet where j.Name == comboBoxJob.Text select j).ToArray();
                    job = jobs[0];
                }
                else
                    job = null;

                ControlFunctions.EditPerson(person,
                                            textBoxName.Text,
                                            dateTimePickerBirthDate.Value,
                                            comboBoxDocType.Text,
                                            textBoxDocumentNum.Text,
                                            textBoxAdress.Text,
                                            comboBoxGender.Text,
                                            textBoxInsurance.Text,
                                            textBoxPassword.Text,
                                            comboBoxBlood.Text,
                                            job,
                                            textBoxEducation.Text);
                saved = true;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void ChangePersonInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saved)
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    DialogResult result = MessageBox.Show("Сохранить перед выходом?", "Внимание",
                                                          MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        string s = TextBoxesCheck();
                        if (s != null)
                        {
                            MessageBox.Show(s, "Исправьте ошибки");
                            e.Cancel = false;
                        }
                        else
                        {
                            Job job;
                            if (person is Doctor)
                            {
                                var jobs = (from j in db.JobSet where j.Name == comboBoxJob.Text select j).ToArray();
                                job = jobs[0];
                            }
                            else
                                job = null;

                            ControlFunctions.EditPerson(person,
                                                        textBoxName.Text,
                                                        dateTimePickerBirthDate.Value,
                                                        comboBoxDocType.Text,
                                                        textBoxDocumentNum.Text,
                                                        textBoxAdress.Text,
                                                        comboBoxGender.Text,
                                                        textBoxInsurance.Text,
                                                        textBoxPassword.Text,
                                                        comboBoxBlood.Text,
                                                        job,
                                                        textBoxEducation.Text);

                            this.Owner.Refresh();
                            e.Cancel = false;
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        e.Cancel = false;
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                    else;
                }
                else;
            if (person is Patient)
                (this.Owner as PatientMenu).ReloadForm();
            else
                (this.Owner as DoctorMenu).ReloadForm();
        }
        private string TextBoxesCheck()
        {
            if (dateTimePickerBirthDate.Value > DateTime.Now)
                return "Дата рождения не может стоять в будущем.";

            else if (!Regex.IsMatch(textBoxName.Text, @"^[A-ЯЁ][а-яё]*\s[A-ЯЁ][а-яё]*(\s[A-ЯЁ][а-яё]*){0,1}$"))
                return "Введите имя в формате: \"Фамилия Имя Отчество(при наличии)\",  пробелы между фамилией и именем, именем и отчеством при наличии отчества.";

            else if (!Regex.IsMatch(textBoxAdress.Text, @"[А-Я][а-я]* [0-9]+[абв]{0,1}, [0-9]+$"))
                return "Введите адрес в формате: \"Улица номер дома, номер квартиры\" - \"Домовая 1, 1\",  пробелы между улицей и номером дома, запятой и номером квартиры.";

            else if (!Regex.IsMatch(textBoxInsurance.Text, @"[0-9]+$"))
                return "Номер страховки содержит только цифры от 0 до 9";

            else if (!Regex.IsMatch(textBoxDocumentNum.Text, @"[0-9]+$"))
                return "Номер документа содержит только цифры от 0 до 9";
            else if (person is Doctor)
                if (!Regex.IsMatch(textBoxEducation.Text, @"[0-9]+$"))
                    return "В образовании не должно быть пустой строки";
                else;
            else;

            return null;
        }
        private void ChangedInfo(object sender, EventArgs e)
        {
            saved = false;
        }
    }
}
