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
    public partial class Registr : Form
    {

        bool isDoctor;
        ModelMedContainer db = new ModelMedContainer();
        bool saved;

        public Registr(bool isDoctor)
        {
            this.isDoctor = isDoctor;
            InitializeComponent();
        }

        private void Registr_Load(object sender, EventArgs e)
        {
            string pers = "";
            if (isDoctor)
                pers += " врача";
            else
                pers += " пациента";

            this.Text += pers;
            
            if (!isDoctor)
            {
                labelJob.Hide();
                comboBoxJob.Hide();

                labelEducation.Hide();
                textBoxEducation.Hide();

                labelBloodType.Show();
                comboBoxBlood.Show();
            }
            else;
            if (isDoctor)
            {

                var jobs = db.JobSet;
                foreach (Job j in jobs)
                    comboBoxJob.Items.Add(j.Name);

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

        private string TextBoxesCheck()
        {

            if (comboBoxDocType.SelectedIndex < 0)
                return "Выберите тип документа.";

            if (comboBoxGender.SelectedIndex < 0)
                return "Выберите пол.";
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
            else if (isDoctor)
            { if (textBoxEducation.Text == "")
                    return "В образовании не должно быть пустой строки";
                else;
                if (comboBoxJob.SelectedIndex < 0)
                    return "Выберите должность.";
            }
            else
                if (comboBoxBlood.SelectedIndex < 0)
                    return "Выберите группу крови.";

            return null;
        }
        private void ChangedInfo(object sender, EventArgs e)
        {
            saved = false;
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
                Person newPerson;
                if (isDoctor)
                {
                   // var jobs = (from j in db.JobSet where j.Name == comboBoxJob.Text select j).ToArray();
                   // job = jobs[0];

                    newPerson = ControlFunctions.CreateDoctor(textBoxName.Text,
                                                  dateTimePickerBirthDate.Value,
                                                  comboBoxDocType.Text,
                                                  textBoxDocumentNum.Text,
                                                  comboBoxJob.Text,
                                                  textBoxAdress.Text,
                                                  textBoxEducation.Text,
                                                  comboBoxGender.Text,
                                                  textBoxInsurance.Text,
                                                  textBoxPassword.Text);
                }
                else
                {
                    newPerson = ControlFunctions.CreatePatient(textBoxName.Text,
                                                   dateTimePickerBirthDate.Value,
                                                   comboBoxDocType.Text,
                                                   textBoxDocumentNum.Text,
                                                   textBoxAdress.Text,
                                                   comboBoxBlood.Text,
                                                   comboBoxGender.Text,
                                                   textBoxInsurance.Text,
                                                   textBoxPassword.Text);
                }
                string result = ControlFunctions.AddPerson(newPerson);
                if (result == null)
                {
                    saved = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result, "Исправьте ошибки");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
