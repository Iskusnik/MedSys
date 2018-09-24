using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

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

        private void ImportDataGridViewDataToExcelSheet()
        {

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


            for (int x = 1; x < dataGridView1.Columns.Count + 1; x++)
            {
                if (dataGridView1.Columns[x - 1].Visible)
                    xlWorkSheet.Cells[1, x] = dataGridView1.Columns[x - 1].HeaderText;
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (dataGridView1.Columns[j].Visible)
                        xlWorkSheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }

            var saveFileDialoge = new SaveFileDialog();
            saveFileDialoge.FileName = "Отчет";
            saveFileDialoge.DefaultExt = ".xlsx";
            if (saveFileDialoge.ShowDialog() == DialogResult.OK)
            {
                xlWorkBook.SaveAs(saveFileDialoge.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }


            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        private void buttonToExcel_Click(object sender, EventArgs e)
        {

            ImportDataGridViewDataToExcelSheet();
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
            Fields[5] = new string[] { "День приёма", "Имя врача", "Имя пациента", "Номер кабинета", "Название корпуса" };
            Fields[6] = new string[] { "День создания записи", "Имя врача", "Имя пациента" };

            Fields[7] = new string[] { "Этажей", "Название корпуса" };
            Fields[8] = new string[] { "Корпус", "Номер кабинета" };

            Fields[9] = new string[] { "Тип документа", "Номер" };
            Fields[10] = new string[] { "Число врачей", "Название должности" };

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
                                            select new
                                            {
                                                person.Id,
                                                person.FullName,
                                                person.BirthDate,
                                                person.Gender,
                                                person.Adress,
                                                person.Document.Type,
                                                person.Document.Num
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
                                            {
                                                person.Id,
                                                person.FullName,
                                                person.BirthDate,
                                                person.Gender,
                                                person.Adress,
                                                person.Document.Type,
                                                person.Document.Num,
                                                (person as Doctor).Job
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


                                if (field2 != -1 && (comboBox1or_and2.Text != "Не выбрано" && textBoxField2.Text == "Не выбрано"))
                                    switch (field2)
                                    {
                                        case 0:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.FullName, textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.FullName, textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.FullName, textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.FullName, textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.FullName, textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                        case 1:
                                            {
                                                int age;
                                                if (int.TryParse(textBoxField2.Text, out age))
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
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
                                                if (DateTime.TryParse(textBoxField2.Text, out birth))
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
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
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Adress, textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Adress, textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Adress, textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Adress, textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Adress, textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;

                                        case 4:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;


                                        case 5:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where (from j in p.Job where j.Name == textBoxField2.Text select j).Count() != 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where (from j in p.Job where j.Name == textBoxField2.Text select j).Count() != 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where (from j in p.Job where j.Name == textBoxField2.Text select j).Count() != 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where (from j in p.Job where j.Name == textBoxField2.Text select j).Count() != 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where (from j in p.Job where j.Name == textBoxField2.Text select j).Count() != 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;

                                    }

                                if (field3 != -1 && (comboBox12_and_or3.Text != "Не выбрано" && textBoxField3.Text == "Не выбрано"))
                                    switch (field3)
                                    {
                                        case 0:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.FullName, textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.FullName, textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.FullName, textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.FullName, textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.FullName, textBoxField3.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                        case 1:
                                            {
                                                int age;
                                                if (int.TryParse(textBoxField3.Text, out age))
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
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
                                                if (DateTime.TryParse(textBoxField3.Text, out birth))
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
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
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Adress, textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Adress, textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Adress, textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Adress, textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Adress, textBoxField3.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;

                                        case 4:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField3.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;


                                        case 5:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where (from j in p.Job where j.Name == textBoxField3.Text select j).Count() != 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where (from j in p.Job where j.Name == textBoxField3.Text select j).Count() != 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where (from j in p.Job where j.Name == textBoxField3.Text select j).Count() != 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where (from j in p.Job where j.Name == textBoxField3.Text select j).Count() != 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where (from j in p.Job where j.Name == textBoxField3.Text select j).Count() != 0 select p).ToList(); break;
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






                                if (field2 != -1 && (comboBox1or_and2.Text != "Не выбрано" && textBoxField2.Text == "Не выбрано"))
                                {
                                    switch (field2)
                                    {
                                        case 0:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.FullName, textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.FullName, textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.FullName, textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.FullName, textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.FullName, textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                        case 1:
                                            {
                                                int age;
                                                if (int.TryParse(textBoxField2.Text, out age))
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
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
                                                if (DateTime.TryParse(textBoxField2.Text, out birth))
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
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
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Adress, textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Adress, textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Adress, textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Adress, textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Adress, textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;

                                        case 4:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;


                                        case 5:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.BloodType == textBoxField2.Text select p).ToList(); break;
                                                        case 1: list = (from p in list where p.BloodType == textBoxField2.Text select p).ToList(); break;
                                                        case 2: list = (from p in list where p.BloodType == textBoxField2.Text select p).ToList(); break;
                                                        case 3: list = (from p in list where p.BloodType == textBoxField2.Text select p).ToList(); break;
                                                        case 4: list = (from p in list where p.BloodType == textBoxField2.Text select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                    }
                                }


                                if (field3 != -1 && (comboBox12_and_or3.Text != "Не выбрано" && textBoxField3.Text == "Не выбрано"))
                                {
                                    switch (field3)
                                    {
                                        case 0:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.FullName, textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.FullName, textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.FullName, textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.FullName, textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.FullName, textBoxField3.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                        case 1:
                                            {
                                                int age;
                                                if (int.TryParse(textBoxField3.Text, out age))
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
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
                                                if (DateTime.TryParse(textBoxField3.Text, out birth))
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
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
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Adress, textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Adress, textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Adress, textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Adress, textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Adress, textBoxField3.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;

                                        case 4:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.FullName.Split(' ')[0], textBoxField3.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;


                                        case 5:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.BloodType == textBoxField3.Text select p).ToList(); break;
                                                        case 1: list = (from p in list where p.BloodType == textBoxField3.Text select p).ToList(); break;
                                                        case 2: list = (from p in list where p.BloodType == textBoxField3.Text select p).ToList(); break;
                                                        case 3: list = (from p in list where p.BloodType == textBoxField3.Text select p).ToList(); break;
                                                        case 4: list = (from p in list where p.BloodType == textBoxField3.Text select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                    }
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
                                            select new { medcard.Id, medcard.Patient.FullName, medcard.Record.Count }).ToList();

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
                                                MessageBox.Show("Введите число записей, как целое число");
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



                                if (field2 != -1 && (comboBox1or_and2.Text != "Не выбрано" && textBoxField2.Text == "Не выбрано"))
                                {
                                    switch (field2)
                                    {

                                        case 0:
                                            {
                                                int recs;
                                                if (int.TryParse(textBoxField2.Text, out recs))
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.Count < recs select p).ToList(); break;
                                                        case 1: list = (from p in list where p.Count <= recs select p).ToList(); break;
                                                        case 2: list = (from p in list where p.Count == recs select p).ToList(); break;
                                                        case 3: list = (from p in list where p.Count >= recs select p).ToList(); break;
                                                        case 4: list = (from p in list where p.Count > recs select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите число записей, как целое число");
                                            }
                                            break;
                                        case 1:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.FullName, textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.FullName, textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.FullName, textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.FullName, textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.FullName, textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                    }
                                }
                                if (field3 != -1 && (comboBox12_and_or3.Text != "Не выбрано" && textBoxField3.Text == "Не выбрано"))
                                {
                                    switch (field3)
                                    {

                                        case 0:
                                            {
                                                int recs;
                                                if (int.TryParse(textBoxField3.Text, out recs))
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.Count < recs select p).ToList(); break;
                                                        case 1: list = (from p in list where p.Count <= recs select p).ToList(); break;
                                                        case 2: list = (from p in list where p.Count == recs select p).ToList(); break;
                                                        case 3: list = (from p in list where p.Count >= recs select p).ToList(); break;
                                                        case 4: list = (from p in list where p.Count > recs select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите число записей, как целое число");
                                            }
                                            break;
                                        case 1:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.FullName, textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.FullName, textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.FullName, textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.FullName, textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.FullName, textBoxField3.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                    }
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
                                                MessageBox.Show("Введите число больных, как целое число");
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



                                if (field2 != -1 && (comboBox1or_and2.Text != "Не выбрано" && textBoxField2.Text == "Не выбрано"))
                                {
                                    switch (field2)
                                    {

                                        case 0:
                                            {
                                                int recs;
                                                if (int.TryParse(textBoxField2.Text, out recs))
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.Count < recs select p).ToList(); break;
                                                        case 1: list = (from p in list where p.Count <= recs select p).ToList(); break;
                                                        case 2: list = (from p in list where p.Count == recs select p).ToList(); break;
                                                        case 3: list = (from p in list where p.Count >= recs select p).ToList(); break;
                                                        case 4: list = (from p in list where p.Count > recs select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите число больных, как целое число");
                                            }
                                            break;
                                        case 1:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                    }
                                }

                                if (field3 != -1 && (comboBox12_and_or3.Text != "Не выбрано" && textBoxField3.Text == "Не выбрано"))
                                {
                                    switch (field3)
                                    {

                                        case 0:
                                            {
                                                int recs;
                                                if (int.TryParse(textBoxField3.Text, out recs))
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.Count < recs select p).ToList(); break;
                                                        case 1: list = (from p in list where p.Count <= recs select p).ToList(); break;
                                                        case 2: list = (from p in list where p.Count == recs select p).ToList(); break;
                                                        case 3: list = (from p in list where p.Count >= recs select p).ToList(); break;
                                                        case 4: list = (from p in list where p.Count > recs select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите число больных, как целое число");
                                            }
                                            break;
                                        case 1:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                    }
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

                    #region Запись на приём
                    //Запись на приём
                    //"День приёма", "Имя врача", "Имя пациента", "Номер кабинета", "Название корпуса" 
                    case 5:
                        {
                            using (ModelMedContainer db = new ModelMedContainer())
                            {
                                var list = (from visit in db.TimeForVisitSet
                                            select new
                                            {
                                                visit.Id,
                                                visit.VisitTime,
                                                DoctorFIO = visit.Doctor.FullName,
                                                PatientFIO = visit.Patient.FullName,
                                                visit.Cabinet.Corpus.Name,
                                                visit.Cabinet.Num
                                            }).ToList();

                                switch (field1)
                                {
                                    case 0:
                                        {
                                            DateTime date;
                                            if (DateTime.TryParse(textBoxField1.Text, out date))
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where p.VisitTime < date select p).ToList(); break;
                                                    case 1: list = (from p in list where p.VisitTime <= date select p).ToList(); break;
                                                    case 2: list = (from p in list where p.VisitTime == date select p).ToList(); break;
                                                    case 3: list = (from p in list where p.VisitTime >= date select p).ToList(); break;
                                                    case 4: list = (from p in list where p.VisitTime > date select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите значение для поиска - дата в формате дд.мм.гггг");
                                        }
                                        break;
                                    case 1:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField1.Text) < 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField1.Text) <= 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField1.Text) == 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField1.Text) >= 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField1.Text) > 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Заполните поле");
                                        }
                                        break;
                                    case 2:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where String.Compare(p.PatientFIO, textBoxField1.Text) < 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where String.Compare(p.PatientFIO, textBoxField1.Text) <= 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where String.Compare(p.PatientFIO, textBoxField1.Text) == 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where String.Compare(p.PatientFIO, textBoxField1.Text) >= 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where String.Compare(p.PatientFIO, textBoxField1.Text) > 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Заполните поле");
                                        }
                                        break;
                                    case 3:
                                        {
                                            int num;
                                            if (int.TryParse(textBoxField1.Text, out num))
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where p.Num < num select p).ToList(); break;
                                                    case 1: list = (from p in list where p.Num <= num select p).ToList(); break;
                                                    case 2: list = (from p in list where p.Num == num select p).ToList(); break;
                                                    case 3: list = (from p in list where p.Num >= num select p).ToList(); break;
                                                    case 4: list = (from p in list where p.Num > num select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Заполните в виде целого числа");
                                        }
                                        break;
                                    case 4:
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




                                if (field2 != -1 && (comboBox1or_and2.Text != "Не выбрано" && textBoxField2.Text == "Не выбрано"))
                                {
                                    switch (field2)
                                    {
                                        case 0:
                                            {
                                                DateTime date;
                                                if (DateTime.TryParse(textBoxField2.Text, out date))
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.VisitTime < date select p).ToList(); break;
                                                        case 1: list = (from p in list where p.VisitTime <= date select p).ToList(); break;
                                                        case 2: list = (from p in list where p.VisitTime == date select p).ToList(); break;
                                                        case 3: list = (from p in list where p.VisitTime >= date select p).ToList(); break;
                                                        case 4: list = (from p in list where p.VisitTime > date select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска - дата в формате дд.мм.гггг");
                                            }
                                            break;
                                        case 1:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Заполните поле");
                                            }
                                            break;
                                        case 2:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.PatientFIO, textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.PatientFIO, textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.PatientFIO, textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.PatientFIO, textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.PatientFIO, textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Заполните поле");
                                            }
                                            break;
                                        case 3:
                                            {
                                                int num;
                                                if (int.TryParse(textBoxField2.Text, out num))
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.Num < num select p).ToList(); break;
                                                        case 1: list = (from p in list where p.Num <= num select p).ToList(); break;
                                                        case 2: list = (from p in list where p.Num == num select p).ToList(); break;
                                                        case 3: list = (from p in list where p.Num >= num select p).ToList(); break;
                                                        case 4: list = (from p in list where p.Num > num select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Заполните в виде целого числа");
                                            }
                                            break;
                                        case 4:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                    }
                                }

                                if (field3 != -1 && (comboBox12_and_or3.Text != "Не выбрано" && textBoxField3.Text == "Не выбрано"))
                                {
                                    switch (field3)
                                    {
                                        case 0:
                                            {
                                                DateTime date;
                                                if (DateTime.TryParse(textBoxField3.Text, out date))
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.VisitTime < date select p).ToList(); break;
                                                        case 1: list = (from p in list where p.VisitTime <= date select p).ToList(); break;
                                                        case 2: list = (from p in list where p.VisitTime == date select p).ToList(); break;
                                                        case 3: list = (from p in list where p.VisitTime >= date select p).ToList(); break;
                                                        case 4: list = (from p in list where p.VisitTime > date select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска - дата в формате дд.мм.гггг");
                                            }
                                            break;
                                        case 1:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField3.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Заполните поле");
                                            }
                                            break;
                                        case 2:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.PatientFIO, textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.PatientFIO, textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.PatientFIO, textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.PatientFIO, textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.PatientFIO, textBoxField3.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Заполните поле");
                                            }
                                            break;
                                        case 3:
                                            {
                                                int num;
                                                if (int.TryParse(textBoxField3.Text, out num))
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.Num < num select p).ToList(); break;
                                                        case 1: list = (from p in list where p.Num <= num select p).ToList(); break;
                                                        case 2: list = (from p in list where p.Num == num select p).ToList(); break;
                                                        case 3: list = (from p in list where p.Num >= num select p).ToList(); break;
                                                        case 4: list = (from p in list where p.Num > num select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Заполните в виде целого числа");
                                            }
                                            break;
                                        case 4:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                    }
                                }








                                dataGridView1.DataSource = list;
                                dataGridView1.RowHeadersVisible = false;

                                dataGridView1.Columns[0].HeaderText = "Id";
                                dataGridView1.Columns[1].HeaderText = "Время приёма";
                                dataGridView1.Columns[2].HeaderText = "Имя врача";
                                dataGridView1.Columns[3].HeaderText = "Имя пациента";
                                dataGridView1.Columns[4].HeaderText = "Корпус";
                                dataGridView1.Columns[5].HeaderText = "Кабинет";

                            }
                        }
                        break;
                    #endregion

                    #region Запись врача
                    //Запись на приём
                    //"День приёма", "Имя врача", "Имя пациента", "Номер кабинета", "Название корпуса" 
                    case 6:
                        {
                            using (ModelMedContainer db = new ModelMedContainer())
                            {
                                var list = (from record in db.RecordSet
                                            select new
                                            {
                                                record.Id,
                                                record.Date,
                                                DoctorFIO = record.Doctor.FullName,
                                                PatientFIO = record.MedCard.Patient.FullName,
                                            }).ToList();

                                switch (field1)
                                {
                                    case 0:
                                        {
                                            DateTime date;
                                            if (DateTime.TryParse(textBoxField1.Text, out date))
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where p.Date < date select p).ToList(); break;
                                                    case 1: list = (from p in list where p.Date <= date select p).ToList(); break;
                                                    case 2: list = (from p in list where p.Date == date select p).ToList(); break;
                                                    case 3: list = (from p in list where p.Date >= date select p).ToList(); break;
                                                    case 4: list = (from p in list where p.Date > date select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите значение для поиска - дата в формате дд.мм.гггг");
                                        }
                                        break;
                                    case 1:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField1.Text) < 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField1.Text) <= 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField1.Text) == 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField1.Text) >= 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField1.Text) > 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Заполните поле");
                                        }
                                        break;
                                    case 2:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where String.Compare(p.PatientFIO, textBoxField1.Text) < 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where String.Compare(p.PatientFIO, textBoxField1.Text) <= 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where String.Compare(p.PatientFIO, textBoxField1.Text) == 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where String.Compare(p.PatientFIO, textBoxField1.Text) >= 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where String.Compare(p.PatientFIO, textBoxField1.Text) > 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Заполните поле");
                                        }
                                        break;
                                }






                                if (field2 != -1 && (comboBox1or_and2.Text != "Не выбрано" && textBoxField2.Text == "Не выбрано"))
                                {
                                    switch (field2)
                                    {
                                        case 0:
                                            {
                                                DateTime date;
                                                if (DateTime.TryParse(textBoxField2.Text, out date))
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.Date < date select p).ToList(); break;
                                                        case 1: list = (from p in list where p.Date <= date select p).ToList(); break;
                                                        case 2: list = (from p in list where p.Date == date select p).ToList(); break;
                                                        case 3: list = (from p in list where p.Date >= date select p).ToList(); break;
                                                        case 4: list = (from p in list where p.Date > date select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска - дата в формате дд.мм.гггг");
                                            }
                                            break;
                                        case 1:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Заполните поле");
                                            }
                                            break;
                                        case 2:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.PatientFIO, textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.PatientFIO, textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.PatientFIO, textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.PatientFIO, textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.PatientFIO, textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Заполните поле");
                                            }
                                            break;
                                    }
                                }

                                if (field3 != -1 && (comboBox12_and_or3.Text != "Не выбрано" && textBoxField3.Text == "Не выбрано"))
                                {
                                    switch (field3)
                                    {
                                        case 0:
                                            {
                                                DateTime date;
                                                if (DateTime.TryParse(textBoxField3.Text, out date))
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.Date < date select p).ToList(); break;
                                                        case 1: list = (from p in list where p.Date <= date select p).ToList(); break;
                                                        case 2: list = (from p in list where p.Date == date select p).ToList(); break;
                                                        case 3: list = (from p in list where p.Date >= date select p).ToList(); break;
                                                        case 4: list = (from p in list where p.Date > date select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска - дата в формате дд.мм.гггг");
                                            }
                                            break;
                                        case 1:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.DoctorFIO, textBoxField3.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Заполните поле");
                                            }
                                            break;
                                        case 2:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.PatientFIO, textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.PatientFIO, textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.PatientFIO, textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.PatientFIO, textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.PatientFIO, textBoxField3.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Заполните поле");
                                            }
                                            break;
                                    }
                                }












                                dataGridView1.DataSource = list;
                                dataGridView1.RowHeadersVisible = false;

                                dataGridView1.Columns[0].HeaderText = "Id";
                                dataGridView1.Columns[1].HeaderText = "Время создания записи";
                                dataGridView1.Columns[2].HeaderText = "Имя врача";
                                dataGridView1.Columns[3].HeaderText = "Имя пациента";

                            }
                        }
                        break;
                    #endregion

                    #region Корпус
                    //Корпус
                    case 7:
                        {
                            using (ModelMedContainer db = new ModelMedContainer())
                            {
                                var list = (from corpus in db.CorpusSet
                                            select new { corpus.Id, corpus.Name, corpus.Floors }).ToList();

                                switch (field1)
                                {

                                    case 0:
                                        {
                                            int floors;
                                            if (int.TryParse(textBoxField1.Text, out floors))
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where p.Floors < floors select p).ToList(); break;
                                                    case 1: list = (from p in list where p.Floors <= floors select p).ToList(); break;
                                                    case 2: list = (from p in list where p.Floors == floors select p).ToList(); break;
                                                    case 3: list = (from p in list where p.Floors >= floors select p).ToList(); break;
                                                    case 4: list = (from p in list where p.Floors > floors select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите число этажей, как целое число");
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






                                if (field2 != -1 && (comboBox1or_and2.Text != "Не выбрано" && textBoxField2.Text == "Не выбрано"))
                                {
                                    switch (field2)
                                    {

                                        case 0:
                                            {
                                                int floors;
                                                if (int.TryParse(textBoxField2.Text, out floors))
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.Floors < floors select p).ToList(); break;
                                                        case 1: list = (from p in list where p.Floors <= floors select p).ToList(); break;
                                                        case 2: list = (from p in list where p.Floors == floors select p).ToList(); break;
                                                        case 3: list = (from p in list where p.Floors >= floors select p).ToList(); break;
                                                        case 4: list = (from p in list where p.Floors > floors select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите число этажей, как целое число");
                                            }
                                            break;
                                        case 1:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                    }
                                }

                                if (field3 != -1 && (comboBox12_and_or3.Text != "Не выбрано" && textBoxField3.Text == "Не выбрано"))
                                {
                                    switch (field3)
                                    {

                                        case 0:
                                            {
                                                int floors;
                                                if (int.TryParse(textBoxField3.Text, out floors))
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.Floors < floors select p).ToList(); break;
                                                        case 1: list = (from p in list where p.Floors <= floors select p).ToList(); break;
                                                        case 2: list = (from p in list where p.Floors == floors select p).ToList(); break;
                                                        case 3: list = (from p in list where p.Floors >= floors select p).ToList(); break;
                                                        case 4: list = (from p in list where p.Floors > floors select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите число этажей, как целое число");
                                            }
                                            break;
                                        case 1:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                    }
                                }













                                dataGridView1.DataSource = list;
                                dataGridView1.RowHeadersVisible = false;

                                dataGridView1.Columns[0].HeaderText = "Id";
                                dataGridView1.Columns[1].HeaderText = "Название корпуса";
                                dataGridView1.Columns[2].HeaderText = "Число этажей";
                            }
                        }
                        break;
                    #endregion

                    #region Кабинет
                    //Кабинет
                    case 8:
                        {
                            using (ModelMedContainer db = new ModelMedContainer())
                            {
                                var list = (from cabinet in db.CabinetSet
                                            select new { cabinet.Id, cabinet.Num, cabinet.Corpus.Name }).ToList();

                                switch (field1)
                                {

                                    case 1:
                                        {
                                            int num;
                                            if (int.TryParse(textBoxField1.Text, out num))
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where p.Num < num select p).ToList(); break;
                                                    case 1: list = (from p in list where p.Num <= num select p).ToList(); break;
                                                    case 2: list = (from p in list where p.Num == num select p).ToList(); break;
                                                    case 3: list = (from p in list where p.Num >= num select p).ToList(); break;
                                                    case 4: list = (from p in list where p.Num > num select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите номер кабинета как целое число");
                                        }
                                        break;
                                    case 0:
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

                                if (field2 != -1 && (comboBox1or_and2.Text != "Не выбрано" && textBoxField2.Text == "Не выбрано"))
                                {
                                    switch (field2)
                                    {

                                        case 1:
                                            {
                                                int num;
                                                if (int.TryParse(textBoxField2.Text, out num))
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.Num < num select p).ToList(); break;
                                                        case 1: list = (from p in list where p.Num <= num select p).ToList(); break;
                                                        case 2: list = (from p in list where p.Num == num select p).ToList(); break;
                                                        case 3: list = (from p in list where p.Num >= num select p).ToList(); break;
                                                        case 4: list = (from p in list where p.Num > num select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите номер кабинета как целое число");
                                            }
                                            break;
                                        case 0:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                    }
                                }
                                if (field3 != -1 && (comboBox12_and_or3.Text != "Не выбрано" && textBoxField3.Text == "Не выбрано"))
                                {
                                    switch (field3)
                                    {

                                        case 1:
                                            {
                                                int num;
                                                if (int.TryParse(textBoxField3.Text, out num))
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.Num < num select p).ToList(); break;
                                                        case 1: list = (from p in list where p.Num <= num select p).ToList(); break;
                                                        case 2: list = (from p in list where p.Num == num select p).ToList(); break;
                                                        case 3: list = (from p in list where p.Num >= num select p).ToList(); break;
                                                        case 4: list = (from p in list where p.Num > num select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите номер кабинета как целое число");
                                            }
                                            break;
                                        case 0:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                    }
                                }




                                dataGridView1.DataSource = list;
                                dataGridView1.RowHeadersVisible = false;

                                dataGridView1.Columns[0].HeaderText = "Id";
                                dataGridView1.Columns[1].HeaderText = "Номер кабинета";
                                dataGridView1.Columns[2].HeaderText = "Название корпуса";
                            }
                        }
                        break;
                    #endregion

                    #region Документы
                    //"Тип документа", "Номер"
                    //Документы
                    case 9:
                        {
                            using (ModelMedContainer db = new ModelMedContainer())
                            {
                                var list = (from doc in db.DocumentSet
                                            select new { doc.Id, doc.Type, doc.Num, doc.Person.FullName }).ToList();

                                switch (field1)
                                {

                                    case 1:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where String.Compare(p.Num, textBoxField1.Text) < 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where String.Compare(p.Num, textBoxField1.Text) <= 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where String.Compare(p.Num, textBoxField1.Text) == 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where String.Compare(p.Num, textBoxField1.Text) >= 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where String.Compare(p.Num, textBoxField1.Text) > 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите значение для поиска");
                                        }
                                        break;
                                    case 0:
                                        {
                                            if (textBoxField1.Text != "")
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where String.Compare(p.Type, textBoxField1.Text) < 0 select p).ToList(); break;
                                                    case 1: list = (from p in list where String.Compare(p.Type, textBoxField1.Text) <= 0 select p).ToList(); break;
                                                    case 2: list = (from p in list where String.Compare(p.Type, textBoxField1.Text) == 0 select p).ToList(); break;
                                                    case 3: list = (from p in list where String.Compare(p.Type, textBoxField1.Text) >= 0 select p).ToList(); break;
                                                    case 4: list = (from p in list where String.Compare(p.Type, textBoxField1.Text) > 0 select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите значение для поиска");
                                        }
                                        break;


                                }
                                
                                if (field2 != -1 && (comboBox1or_and2.Text != "Не выбрано" && textBoxField2.Text == "Не выбрано"))
                                {
                                    switch (field2)
                                    {
                                        case 1:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Num, textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Num, textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Num, textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Num, textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Num, textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                        case 0:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Type, textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Type, textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Type, textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Type, textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Type, textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                    }
                                }

                                if (field3 != -1 && (comboBox12_and_or3.Text != "Не выбрано" && textBoxField3.Text == "Не выбрано"))
                                {
                                    switch (field3)
                                    {
                                        case 1:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Num, textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Num, textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Num, textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Num, textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Num, textBoxField3.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                        case 0:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Type, textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Type, textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Type, textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Type, textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Type, textBoxField3.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                    }
                                }

                                dataGridView1.DataSource = list;
                                dataGridView1.RowHeadersVisible = false;

                                dataGridView1.Columns[0].HeaderText = "Id";
                                dataGridView1.Columns[1].HeaderText = "Тип документа";
                                dataGridView1.Columns[2].HeaderText = "Номер документа";
                                dataGridView1.Columns[3].HeaderText = "Имя владельца";
                            }

                            break;
                        }
                    #endregion

                    #region Должности
                    //"Число врачей",  "Название должности"
                    //Должности
                    case 10:
                        {
                            using (ModelMedContainer db = new ModelMedContainer())
                            {
                                var list = (from job in db.JobSet
                                            select new { job.Id, job.Name, job.Doctor.Count }).ToList();

                                switch (field1)
                                {

                                    case 0:
                                        {
                                            int count;
                                            if (int.TryParse(textBoxField1.Text, out count))
                                            {
                                                switch (comboBoxEqual1.SelectedIndex)
                                                {
                                                    case 0: list = (from p in list where p.Count < count select p).ToList(); break;
                                                    case 1: list = (from p in list where p.Count <= count select p).ToList(); break;
                                                    case 2: list = (from p in list where p.Count == count select p).ToList(); break;
                                                    case 3: list = (from p in list where p.Count >= count select p).ToList(); break;
                                                    case 4: list = (from p in list where p.Count > count select p).ToList(); break;
                                                }
                                            }
                                            else
                                                MessageBox.Show("Введите число врачей, как целое число");
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

                                if (field2 != -1 && (comboBox1or_and2.Text != "Не выбрано" && textBoxField2.Text == "Не выбрано"))
                                    switch (field2)
                                    {

                                        case 0:
                                            {
                                                int count;
                                                if (int.TryParse(textBoxField2.Text, out count))
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.Count < count select p).ToList(); break;
                                                        case 1: list = (from p in list where p.Count <= count select p).ToList(); break;
                                                        case 2: list = (from p in list where p.Count == count select p).ToList(); break;
                                                        case 3: list = (from p in list where p.Count >= count select p).ToList(); break;
                                                        case 4: list = (from p in list where p.Count > count select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите число врачей, как целое число");
                                            }
                                            break;

                                        case 1:
                                            {
                                                if (textBoxField2.Text != "")
                                                {
                                                    switch (comboBoxEqual2.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Name, textBoxField2.Text) > 0 select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите значение для поиска");
                                            }
                                            break;
                                    }
                                if (field3 != -1 && (comboBox12_and_or3.Text != "Не выбрано" && textBoxField3.Text == "Не выбрано"))
                                    switch (field3)
                                    {

                                        case 0:
                                            {
                                                int count;
                                                if (int.TryParse(textBoxField3.Text, out count))
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where p.Count < count select p).ToList(); break;
                                                        case 1: list = (from p in list where p.Count <= count select p).ToList(); break;
                                                        case 2: list = (from p in list where p.Count == count select p).ToList(); break;
                                                        case 3: list = (from p in list where p.Count >= count select p).ToList(); break;
                                                        case 4: list = (from p in list where p.Count > count select p).ToList(); break;
                                                    }
                                                }
                                                else
                                                    MessageBox.Show("Введите число врачей, как целое число");
                                            }
                                            break;

                                        case 1:
                                            {
                                                if (textBoxField3.Text != "")
                                                {
                                                    switch (comboBoxEqual3.SelectedIndex)
                                                    {
                                                        case 0: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) < 0 select p).ToList(); break;
                                                        case 1: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) <= 0 select p).ToList(); break;
                                                        case 2: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) == 0 select p).ToList(); break;
                                                        case 3: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) >= 0 select p).ToList(); break;
                                                        case 4: list = (from p in list where String.Compare(p.Name, textBoxField3.Text) > 0 select p).ToList(); break;
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
                                dataGridView1.Columns[1].HeaderText = "Должность";
                                dataGridView1.Columns[2].HeaderText = "Число врачей";

                                break;
                            }
                        }

                        #endregion

                }
        }
        
    }
}