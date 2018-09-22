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
    public partial class ShowVisitsForDoctor : Form
    {
        Doctor doctor;
        ModelMedContainer db = ControlFunctions.dbContext;
        public ShowVisitsForDoctor(Doctor doctor)
        {
            this.doctor = doctor;
            InitializeComponent();
        }

        private void ShowVisits_Load(object sender, EventArgs e)
        {
            doctor = (Doctor)db.PersonSet.Find(doctor.Id);
            
            var thisPersonVisits = (from visit in doctor.TimeForVisit select visit).ToList();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Время начала приёма", "Время начала приёма");
            dataGridView1.Columns.Add("Имя пациента", "Имя пациента");
            dataGridView1.Columns.Add("Корпус", "Корпус");
            dataGridView1.Columns.Add("Этаж", "Этаж");
            dataGridView1.Columns.Add("Кабинет", "Кабинет");

            foreach (var s in thisPersonVisits)
            {
                string date = s.VisitTime.ToString();

                string name = "Запись свободная";

                if (s.Patient != null)
                    name = s.Patient.FullName;

                string corpus = s.Cabinet.Corpus.Name;
                string floor = s.Cabinet.Floor.ToString();
                string cabinet = s.Cabinet.Num.ToString();

                dataGridView1.Rows.Add(date, name, corpus, floor, cabinet);
            }
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                doctor = (Doctor)db.PersonSet.Find(doctor.Id);
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        var thisPersonVisits = (from visit in doctor.TimeForVisit select visit).ToList();
                        dataGridView1.Columns.Clear();
                        dataGridView1.Columns.Add("Время начала приёма", "Время начала приёма");
                        dataGridView1.Columns.Add("Имя пациента", "Имя пациента");
                        dataGridView1.Columns.Add("Корпус", "Корпус");
                        dataGridView1.Columns.Add("Этаж", "Этаж");
                        dataGridView1.Columns.Add("Кабинет", "Кабинет");

                        foreach (var s in thisPersonVisits)
                        {
                            string date = s.VisitTime.ToString();

                            string name = "Запись свободная";

                            if (s.Patient != null)
                                name = s.Patient.FullName;

                            string corpus = s.Cabinet.Corpus.Name;
                            string floor = s.Cabinet.Floor.ToString();
                            string cabinet = s.Cabinet.Num.ToString();

                            dataGridView1.Rows.Add(date, name, corpus, floor, cabinet);
                        }
                        dataGridView1.RowHeadersVisible = false;
                        dataGridView1.Refresh();
                        break;
                    }
                case 1:
                    {
                        var thisPersonVisits = (from visit in doctor.TimeForVisit where (visit.VisitTime > DateTime.Now) select visit).ToList();
                        dataGridView1.Columns.Clear();
                        dataGridView1.Columns.Add("Время начала приёма", "Время начала приёма");
                        dataGridView1.Columns.Add("Имя пациента", "Имя пациента");
                        dataGridView1.Columns.Add("Корпус", "Корпус");
                        dataGridView1.Columns.Add("Этаж", "Этаж");
                        dataGridView1.Columns.Add("Кабинет", "Кабинет");

                        foreach (var s in thisPersonVisits)
                        {
                            string date = s.VisitTime.ToString();

                            string name = "Запись свободная";

                            if (s.Patient != null)
                                name = s.Patient.FullName;

                            string corpus = s.Cabinet.Corpus.Name;
                            string floor = s.Cabinet.Floor.ToString();
                            string cabinet = s.Cabinet.Num.ToString();

                            dataGridView1.Rows.Add(date, name, corpus, floor, cabinet);
                        }

                        dataGridView1.RowHeadersVisible = false;
                        dataGridView1.Refresh();
                        break;
                    }
                case 2:
                    {
                        var thisPersonVisits = (from visit in doctor.TimeForVisit where (visit.VisitTime <= DateTime.Now) select visit).ToList();
                        dataGridView1.Columns.Clear();
                        dataGridView1.Columns.Add("Время начала приёма", "Время начала приёма");
                        dataGridView1.Columns.Add("Имя пациента", "Имя пациента");
                        dataGridView1.Columns.Add("Корпус", "Корпус");
                        dataGridView1.Columns.Add("Этаж", "Этаж");
                        dataGridView1.Columns.Add("Кабинет", "Кабинет");

                        foreach (var s in thisPersonVisits)
                        {
                            string date = s.VisitTime.ToString();

                            string name = "Запись свободная";

                            if (s.Patient != null)
                                name = s.Patient.FullName;

                            string corpus = s.Cabinet.Corpus.Name;
                            string floor = s.Cabinet.Floor.ToString();
                            string cabinet = s.Cabinet.Num.ToString();

                            dataGridView1.Rows.Add(date, name, corpus, floor, cabinet);
                        }

                        dataGridView1.RowHeadersVisible = false;
                        dataGridView1.Refresh();
                        break;
                    }
            }
        }
    }
}
