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
    public partial class QueryBuilderMedSys : Form
    {
        public static string[][] Fields = new string[11][];
        //Человек                       0    <  поля человека
        //Доктор                        1   <=  поля доктора
        //Пациент                       2    =  поля пациента
        //Медицинская карта             3   >=
        //Болезнь                       4    >
        //Запись на приём               5
        //Запись врача  в медкарте      6
        //Корпус                        7
        //Кабинет                       8
        //Документы                     9
        //Должности                     10 

        static int EntityIndex = -1, field1 = -1, field2 = -1, field3 = -1;

        private void comboBoxField1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxField2.Text == "Не выбрано")
                field2 = -1;
            else
            {
                int index = comboBoxField1.SelectedIndex;

                if (comboBoxField2.Items.Count == 0)
                    comboBoxField2.Items.AddRange(Fields[EntityIndex]);


                if (comboBoxField2.Items.Contains(comboBoxField1.Items[index]))
                {
                    if (Array.IndexOf(Fields[EntityIndex], comboBoxField2.Items[comboBoxField2.Items.IndexOf(comboBoxField1.Items[index])]) == field2)
                    {
                        field2 = -1;
                        field3 = -1;
                        comboBoxField3.Items.Clear();
                    }
                    comboBoxField2.Items.Remove(comboBoxField1.Items[index]);

                    if (field1 != -1)
                        comboBoxField2.Items.Add(Fields[EntityIndex][field1]);
                }




                if (comboBoxField3.Items.Count != 0)
                    if (comboBoxField3.Items.Contains(comboBoxField1.Items[index]))
                    {
                        comboBoxField3.Items.Remove(comboBoxField1.Items[index]);
                        comboBoxField3.Items.Add(Fields[EntityIndex][field1]);
                    }

                if (comboBoxField3.SelectedItem == null)
                    field3 = -1;

                if (comboBoxField2.SelectedItem == null)
                    field2 = -1;

                field1 = comboBoxField1.SelectedIndex;
            }
        }

        private void comboBoxField2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxField2.Text == "Не выбрано")
                field2 = -1;
            else
            {

                int index1 = comboBoxField1.SelectedIndex;
                int index2 = comboBoxField2.SelectedIndex;

                if (comboBoxField3.Items.Count == 0)
                {
                    comboBoxField3.Items.AddRange(Fields[EntityIndex]);
                    comboBoxField3.Items.Remove(comboBoxField1.Items[index1]);
                    comboBoxField3.Items.Remove(comboBoxField2.Items[index2]);
                }

                if (comboBoxField3.Items.Contains(comboBoxField2.Items[index2]))
                {
                    comboBoxField3.Items.Remove(comboBoxField2.Items[index2]);

                    if (field2 != -1)
                        comboBoxField3.Items.Add(Fields[EntityIndex][field2]);
                }

                if (comboBoxField3.SelectedItem == null)
                    field3 = -1;

                field2 = Array.IndexOf(Fields[EntityIndex], comboBoxField2.Items[index2]);
            }
        }

        private void comboBoxField3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxField3.Text == "Не выбрано")
                field3 = -1;
            else
            {
                int index = comboBoxField3.SelectedIndex;
                field3 = Array.IndexOf(Fields[EntityIndex], comboBoxField3.Items[index]);
            }
        }
        

        public QueryBuilderMedSys()
        {
            InitializeComponent();
        }

        private void comboBox1or_and2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBoxEntities_SelectedIndexChanged(object sender, EventArgs e)
        {

            EntityIndex = comboBoxEntities.SelectedIndex;
            comboBoxField1.Items.Clear();
            comboBoxField2.Items.Clear();
            comboBoxField3.Items.Clear();
            textBoxField1.Clear();
            textBoxField2.Clear();
            textBoxField3.Clear();
            field1 = -1;
            field2 = -1;
            field3 = -1;

            comboBoxField1.Items.AddRange(Fields[EntityIndex]);
            comboBoxField1.Items.Remove("Не выбрано");

        }

        private void QueryBuilderMedSys_Load(object sender, EventArgs e)
        {
            //Задаём доступные поля для поиска сущности по этому полю

            Fields[0] = new string[] { "Полное имя", "Возраст", "Дата рождения", "Адрес", "Фамилия", "Не выбрано" };
            Fields[1] = new string[] { "Полное имя", "Возраст", "Дата рождения", "Адрес", "Фамилия", "Должность", "Не выбрано" };
            Fields[2] = new string[] { "Полное имя", "Возраст", "Дата рождения", "Адрес", "Фамилия", "Группа крови", "Не выбрано" };
            Fields[3] = new string[] { "Число записей", "Имя владельца" };
            Fields[4] = new string[] { "Число больных", "Название болезни" };



            Fields[5] = new string[] { "День приёма", "Имя врача", "Имя пациента" };

            Fields[6] = new string[] { "День создания записи", "Имя врача", "Имя пациента" };
            Fields[7] = new string[] { "Этажей", "Номер" };
            Fields[8] = new string[] { "Корпус", "Номер" };
            Fields[9] = new string[] { "Тип документа", "Номер" };
            Fields[10] = new string[] { "Число врачей", "Имя врача", "Название должности" };

            comboBoxEqual1.SelectedIndex = 0;
            comboBoxEqual2.SelectedIndex = 0;
            comboBoxEqual3.SelectedIndex = 0;

            comboBox12_and_or3.SelectedIndex = 2;
            comboBox1or_and2.SelectedIndex = 2;
        }






        private void buttonSearch_Click(object sender, EventArgs e)
        {
    //         ||
    //            field2 != -1 && (comboBox1or_and2.Text != "Не выбрано" && textBoxField2.Text == "Не выбрано") ||
    //            field3 != -1 && (comboBox12_and_or3.Text != "Не выбрано" && textBoxField3.Text == "Не выбрано")


            if (field1 != -1 && textBoxField1.Text == "Не выбрано")
                MessageBox.Show("Выберите, что собираетесь искать. Определите значения для поиска. Определите, как комбинировать условия");
            else
                switch (EntityIndex)
                {

                    case -1:
                        {
                            MessageBox.Show("Выберите, что собираетесь искать");
                        }
                        break;

                    #region Все персональные данные
                    case 0:
                        {
                            using (ModelMedContainer db = new ModelMedContainer())
                            {
                                var list = (from person in db.PersonSet
                                            select new { person.Id,
                                                         person.FullName,
                                                         person.BirthDate,
                                                         person.Gender,
                                                         person.Adress,
                                                         person.Document.Type,
                                                         person.Document.Num }).ToList();

                                switch (field1)
                                {
                                    case 0:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) < 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) <= 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) == 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) >= 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) > 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите значение для поиска");
                                        }
                                        break;
                                    case 1:
                                        {
                                            int age;
                                            if (int.TryParse(textBoxField1.Text, out age))
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where ((TimeSpan)(DateTime.Now - p.BirthDate)).Days / 365 < age select p).ToList(); break;
                                                    case 1: list = (from p in list where ((TimeSpan)(DateTime.Now - p.BirthDate)).Days / 365 <= age select p).ToList(); break;
                                                    case 2: list = (from p in list where ((TimeSpan)(DateTime.Now - p.BirthDate)).Days / 365 == age select p).ToList(); break;
                                                    case 3: list = (from p in list where ((TimeSpan)(DateTime.Now - p.BirthDate)).Days / 365 >= age select p).ToList(); break;
                                                    case 4: list = (from p in list where ((TimeSpan)(DateTime.Now - p.BirthDate)).Days / 365 > age select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите возраст как целое число");
                                        }
                                        break;
                                    case 2:
                                        {
                                            DateTime birth;
                                            if (DateTime.TryParse(textBoxField1.Text, out birth))
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where p.BirthDate < birth select p).ToList(); break;
                                                    case 1: list = (from p in list where p.BirthDate <= birth select p).ToList(); break;
                                                    case 2: list = (from p in list where p.BirthDate == birth select p).ToList(); break;
                                                    case 3: list = (from p in list where p.BirthDate >= birth select p).ToList(); break;
                                                    case 4: list = (from p in list where p.BirthDate > birth select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите дату рождения в формате ДД.ММ.ГГГГ");
                                        }
                                        break;
                                    case 3:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where String.Compare(p.Adress, textBoxField1.Text) < 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where String.Compare(p.Adress, textBoxField1.Text) <= 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where String.Compare(p.Adress, textBoxField1.Text) == 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where String.Compare(p.Adress, textBoxField1.Text) >= 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where String.Compare(p.Adress, textBoxField1.Text) > 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите значение для поиска");
                                        }
                                        break;
                                    case 4:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField1.Text) < 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField1.Text) <= 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField1.Text) == 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField1.Text) >= 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField1.Text) > 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите значение для поиска");
                                        }
                                        break;

                                }
                                dataGridView1.DataSource = list;
                                dataGridView1.RowHeadersVisible = false;

                                dataGridView1.Columns[0].HeaderText = "Id";
                                dataGridView1.Columns[1].HeaderText = "Имя";
                                dataGridView1.Columns[2].HeaderText = "Дата рождения";
                                dataGridView1.Columns[3].HeaderText = "Пол";
                                dataGridView1.Columns[4].HeaderText = "Место жительства";
                                dataGridView1.Columns[5].HeaderText = "Тип документа";
                                dataGridView1.Columns[6].HeaderText = "Номер документа";
                            }
                        }
                        break;
                    #endregion

                    #region Доктор
                    case 1:
                        {
                            using (ModelMedContainer db = new ModelMedContainer())
                            {
                                var list = (from person in db.PersonSet
                                            where person is Doctor
                                            select new
                                                  {   person.Id,
                                                      person.FullName,
                                                      person.BirthDate,
                                                      person.Gender,
                                                      person.Adress,
                                                      person.Document.Type,
                                                      person.Document.Num,
                                                      (person as Doctor).Job }).ToList();

                                switch (field1)
                                {
                                    case 0:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) < 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) <= 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) == 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) >= 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) > 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите значение для поиска");
                                        }
                                        break;
                                    case 1:
                                        {
                                            int age;
                                            if (int.TryParse(textBoxField1.Text, out age))
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where ((TimeSpan)(DateTime.Now - p.BirthDate)).Days / 365 < age select p).ToList(); break;
                                                    case 1: list = (from p in list where ((TimeSpan)(DateTime.Now - p.BirthDate)).Days / 365 <= age select p).ToList(); break;
                                                    case 2: list = (from p in list where ((TimeSpan)(DateTime.Now - p.BirthDate)).Days / 365 == age select p).ToList(); break;
                                                    case 3: list = (from p in list where ((TimeSpan)(DateTime.Now - p.BirthDate)).Days / 365 >= age select p).ToList(); break;
                                                    case 4: list = (from p in list where ((TimeSpan)(DateTime.Now - p.BirthDate)).Days / 365 > age select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите возраст как целое число");
                                        }
                                        break;
                                    case 2:
                                        {
                                            DateTime birth;
                                            if (DateTime.TryParse(textBoxField1.Text, out birth))
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where p.BirthDate < birth select p).ToList(); break;
                                                    case 1: list = (from p in list where p.BirthDate <= birth select p).ToList(); break;
                                                    case 2: list = (from p in list where p.BirthDate == birth select p).ToList(); break;
                                                    case 3: list = (from p in list where p.BirthDate >= birth select p).ToList(); break;
                                                    case 4: list = (from p in list where p.BirthDate > birth select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите дату рождения в формате ДД.ММ.ГГГГ");
                                        }
                                        break;
                                    case 3:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where String.Compare(p.Adress, textBoxField1.Text) < 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where String.Compare(p.Adress, textBoxField1.Text) <= 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where String.Compare(p.Adress, textBoxField1.Text) == 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where String.Compare(p.Adress, textBoxField1.Text) >= 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where String.Compare(p.Adress, textBoxField1.Text) > 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите значение для поиска");
                                        }
                                        break;

                                    case 4:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField1.Text) < 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField1.Text) <= 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField1.Text) == 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField1.Text) >= 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField1.Text) > 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите значение для поиска");
                                        }
                                        break;


                                    case 5:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where (from j in p.Job where j.Name == textBoxField1.Text select j).Count() != 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where (from j in p.Job where j.Name == textBoxField1.Text select j).Count() != 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where (from j in p.Job where j.Name == textBoxField1.Text select j).Count() != 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where (from j in p.Job where j.Name == textBoxField1.Text select j).Count() != 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where (from j in p.Job where j.Name == textBoxField1.Text select j).Count() != 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите значение для поиска");
                                        }
                                        break;

                                }
                                dataGridView1.DataSource = list;
                                dataGridView1.RowHeadersVisible = false;

                                dataGridView1.Columns[0].HeaderText = "Id";
                                dataGridView1.Columns[1].HeaderText = "Имя";
                                dataGridView1.Columns[2].HeaderText = "Дата рождения";
                                dataGridView1.Columns[3].HeaderText = "Пол";
                                dataGridView1.Columns[4].HeaderText = "Место жительства";
                                dataGridView1.Columns[5].HeaderText = "Тип документа";
                                dataGridView1.Columns[6].HeaderText = "Номер документа";
                                dataGridView1.Columns[7].Visible = false;

                            }
                        }
                        break;
                    #endregion

                    #region Пациент
                    case 2:
                        {
                            using (ModelMedContainer db = new ModelMedContainer())
                            {
                                var list = (from person in db.PersonSet
                                            where person is Patient
                                            select new
                                            {
                                                person.Id,
                                                person.FullName,
                                                person.BirthDate,
                                                person.Gender,
                                                person.Adress,
                                                person.Document.Type,
                                                person.Document.Num,
                                                (person as Patient).BloodType
                                            }).ToList();

                                switch (field1)
                                {
                                    case 0:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) < 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) <= 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) == 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) >= 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) > 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите значение для поиска");
                                        }
                                        break;
                                    case 1:
                                        {
                                            int age;
                                            if (int.TryParse(textBoxField1.Text, out age))
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where ((TimeSpan)(DateTime.Now - p.BirthDate)).Days / 365 < age select p).ToList(); break;
                                                    case 1: list = (from p in list where ((TimeSpan)(DateTime.Now - p.BirthDate)).Days / 365 <= age select p).ToList(); break;
                                                    case 2: list = (from p in list where ((TimeSpan)(DateTime.Now - p.BirthDate)).Days / 365 == age select p).ToList(); break;
                                                    case 3: list = (from p in list where ((TimeSpan)(DateTime.Now - p.BirthDate)).Days / 365 >= age select p).ToList(); break;
                                                    case 4: list = (from p in list where ((TimeSpan)(DateTime.Now - p.BirthDate)).Days / 365 > age select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите возраст как целое число");
                                        }
                                        break;
                                    case 2:
                                        {
                                            DateTime birth;
                                            if (DateTime.TryParse(textBoxField1.Text, out birth))
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where p.BirthDate < birth select p).ToList(); break;
                                                    case 1: list = (from p in list where p.BirthDate <= birth select p).ToList(); break;
                                                    case 2: list = (from p in list where p.BirthDate == birth select p).ToList(); break;
                                                    case 3: list = (from p in list where p.BirthDate >= birth select p).ToList(); break;
                                                    case 4: list = (from p in list where p.BirthDate > birth select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите дату рождения в формате ДД.ММ.ГГГГ");
                                        }
                                        break;
                                    case 3:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where String.Compare(p.Adress, textBoxField1.Text) < 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where String.Compare(p.Adress, textBoxField1.Text) <= 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where String.Compare(p.Adress, textBoxField1.Text) == 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where String.Compare(p.Adress, textBoxField1.Text) >= 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where String.Compare(p.Adress, textBoxField1.Text) > 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите значение для поиска");
                                        }
                                        break;

                                    case 4:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField1.Text) < 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField1.Text) <= 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField1.Text) == 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField1.Text) >= 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField1.Text) > 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите значение для поиска");
                                        }
                                        break;


                                    case 5:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where p.BloodType == textBoxField1.Text select p).ToList(); break;
                                                    case 1: list = (from p in list where p.BloodType == textBoxField1.Text select p).ToList(); break;
                                                    case 2: list = (from p in list where p.BloodType == textBoxField1.Text select p).ToList(); break;
                                                    case 3: list = (from p in list where p.BloodType == textBoxField1.Text select p).ToList(); break;
                                                    case 4: list = (from p in list where p.BloodType == textBoxField1.Text select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите значение для поиска");
                                        }
                                        break;

                                }
                                dataGridView1.DataSource = list;
                                dataGridView1.RowHeadersVisible = false;

                                dataGridView1.Columns[0].HeaderText = "Id";
                                dataGridView1.Columns[1].HeaderText = "Имя";
                                dataGridView1.Columns[2].HeaderText = "Дата рождения";
                                dataGridView1.Columns[3].HeaderText = "Пол";
                                dataGridView1.Columns[4].HeaderText = "Место жительства";
                                dataGridView1.Columns[5].HeaderText = "Тип документа";
                                dataGridView1.Columns[6].HeaderText = "Номер документа";

                                dataGridView1.Columns[7].HeaderText = "Группа крови";
                            }
                        }
                        break;
                    #endregion

                    #region Медкарта
                    //Медицинская карта
                    case 3:
                        {
                            using (ModelMedContainer db = new ModelMedContainer())
                            {
                                var list = (from medcard in db.MedCardSet
                                            select new { medcard.Id, medcard.Patient.FullName, medcard.Record.Count}).ToList();

                                switch (field1)
                                {
                                    
                                    case 0:
                                        {
                                            int recs;
                                            if (int.TryParse(textBoxField1.Text, out recs))
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where p.Count < recs select p).ToList(); break;
                                                    case 1: list = (from p in list where p.Count <= recs select p).ToList(); break;
                                                    case 2: list = (from p in list where p.Count == recs select p).ToList(); break;
                                                    case 3: list = (from p in list where p.Count >= recs select p).ToList(); break;
                                                    case 4: list = (from p in list where p.Count > recs select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите число записей как целое число");
                                        }
                                        break;
                                    case 1:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) < 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) <= 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) == 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) >= 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where String.Compare(p.FullName, textBoxField1.Text) > 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите значение для поиска");
                                        }
                                        break;


                                }
                                dataGridView1.DataSource = list;
                                dataGridView1.RowHeadersVisible = false;

                                dataGridView1.Columns[0].HeaderText = "Id";
                                dataGridView1.Columns[1].HeaderText = "Имя владельца";
                                dataGridView1.Columns[2].HeaderText = "Число записей";
                            }
                        }
                        break;
                    #endregion
                    #region Болезнь
                    //Болезнь
                    case 4:
                        {
                            using (ModelMedContainer db = new ModelMedContainer())
                            {
                                var list = (from ill in db.IllnessSet
                                            select new { ill.Id, ill.Name, ill.MedCard.Count }).ToList();

                                switch (field1)
                                {

                                    case 0:
                                        {
                                            int recs;
                                            if (int.TryParse(textBoxField1.Text, out recs))
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where p.Count < recs select p).ToList(); break;
                                                    case 1: list = (from p in list where p.Count <= recs select p).ToList(); break;
                                                    case 2: list = (from p in list where p.Count == recs select p).ToList(); break;
                                                    case 3: list = (from p in list where p.Count >= recs select p).ToList(); break;
                                                    case 4: list = (from p in list where p.Count > recs select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите число больных как целое число");
                                        }
                                        break;
                                    case 1:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where String.Compare(p.Name, textBoxField1.Text) < 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where String.Compare(p.Name, textBoxField1.Text) <= 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where String.Compare(p.Name, textBoxField1.Text) == 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where String.Compare(p.Name, textBoxField1.Text) >= 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where String.Compare(p.Name, textBoxField1.Text) > 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите значение для поиска");
                                        }
                                        break;


                                }
                                dataGridView1.DataSource = list;
                                dataGridView1.RowHeadersVisible = false;

                                dataGridView1.Columns[0].HeaderText = "Id";
                                dataGridView1.Columns[1].HeaderText = "Название болезни";
                                dataGridView1.Columns[2].HeaderText = "Число больных";
                            }
                        }
                        break;
                    #endregion

                    //Запись на приём
                    case 5:
                        {

                        }
                        break;

                    //Запись врача
                    case 6:
                        {

                        }
                        break;

                    //Свободное время
                    case 7:
                        {

                        }
                        break;

                    //Занятое время
                    case 8:
                        {

                        }
                        break;

                    //Документы
                    case 9:
                        {

                        }
                        break;
                }
        }

        
    }
}
