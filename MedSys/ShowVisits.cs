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
    public partial class ShowVisits : Form
    {
        Patient patient;
        public ShowVisits(Patient patient)
        {
            this.patient = patient;
            InitializeComponent();
        }

        private void ShowVisits_Load(object sender, EventArgs e)
        {
            using (ModelMedContainer db = new ModelMedContainer())
            {
                patient = (Patient)db.PersonSet.Find(patient.Id);

                var thisPersonVisits = (from visit in patient.TimeForVisit select new { Время_начала_приёма = visit.VisitTime, Имя_врача = visit.Doctor.FullName, Корпус = visit.Cabinet.Corpus, Этаж = visit.Cabinet.Floor.ToString(), Кабинет = visit.Cabinet.Num.ToString() }).ToList();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("Время начала приёма", "Время начала приёма");
                dataGridView1.Columns.Add("Имя врача", "Имя врача");
                dataGridView1.Columns.Add("Корпус", "Корпус");
                dataGridView1.Columns.Add("Этаж", "Этаж");
                dataGridView1.Columns.Add("Кабинет", "Кабинет");

                foreach (var s in thisPersonVisits)
                {
                    string date = s.Время_начала_приёма.ToString();
                    string name = s.Имя_врача;
                    string corpus = s.Корпус;
                    string floor = s.Этаж;
                    string cabinet = s.Кабинет;

                    dataGridView1.Rows.Add(date, name, corpus, floor, cabinet);
                }
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (ModelMedContainer db = new ModelMedContainer())
            {
                patient = (Patient)db.PersonSet.Find(patient.Id);
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        {
                            var thisPersonVisits = (from visit in patient.TimeForVisit select new { Время_начала_приёма = visit.VisitTime, Имя_врача = visit.Doctor.FullName, Корпус = visit.Cabinet.Corpus, Этаж = visit.Cabinet.Floor.ToString(), Кабинет = visit.Cabinet.Num.ToString() }).ToList();
                            dataGridView1.Columns.Clear();
                            dataGridView1.Columns.Add("Время начала приёма", "Время начала приёма");
                            dataGridView1.Columns.Add("Имя врача", "Имя врача");
                            dataGridView1.Columns.Add("Корпус", "Корпус");
                            dataGridView1.Columns.Add("Этаж", "Этаж");
                            dataGridView1.Columns.Add("Кабинет", "Кабинет");

                            foreach (var s in thisPersonVisits)
                            {
                                string date = s.Время_начала_приёма.ToString();
                                string name = s.Имя_врача;
                                string corpus = s.Корпус;
                                string floor = s.Этаж;
                                string cabinet = s.Кабинет;

                                dataGridView1.Rows.Add(date, name, corpus, floor, cabinet);
                            }
                            dataGridView1.RowHeadersVisible = false;
                            dataGridView1.Refresh();
                            break;
                        }
                    case 1:
                        {
                            var thisPersonVisits = (from visit in patient.TimeForVisit where (visit.VisitTime > DateTime.Now) select new { Время_начала_приёма = visit.VisitTime, Имя_врача = visit.Doctor.FullName, Корпус = visit.Cabinet.Corpus, Этаж = visit.Cabinet.Floor.ToString(), Кабинет = visit.Cabinet.Num.ToString() }).ToList();
                            dataGridView1.Columns.Clear();
                            dataGridView1.Columns.Add("Время начала приёма", "Время начала приёма");
                            dataGridView1.Columns.Add("Имя врача", "Имя врача");
                            dataGridView1.Columns.Add("Корпус", "Корпус");
                            dataGridView1.Columns.Add("Этаж", "Этаж");
                            dataGridView1.Columns.Add("Кабинет", "Кабинет");

                            foreach (var s in thisPersonVisits)
                            {
                                string date = s.Время_начала_приёма.ToString();
                                string name = s.Имя_врача;
                                string corpus = s.Корпус;
                                string floor = s.Этаж;
                                string cabinet = s.Кабинет;

                                dataGridView1.Rows.Add(date, name, corpus, floor, cabinet);
                            }

                            dataGridView1.RowHeadersVisible = false;
                            dataGridView1.Refresh();
                            break;
                        }
                    case 2:
                        {
                            var thisPersonVisits = (from visit in patient.TimeForVisit where (visit.VisitTime <= DateTime.Now) select new { Время_начала_приёма = visit.VisitTime, Имя_врача = visit.Doctor.FullName, Корпус = visit.Cabinet.Corpus, Этаж = visit.Cabinet.Floor.ToString(), Кабинет = visit.Cabinet.Num.ToString() }).ToList();
                            dataGridView1.Columns.Clear();
                            dataGridView1.Columns.Add("Время начала приёма", "Время начала приёма");
                            dataGridView1.Columns.Add("Имя врача", "Имя врача");
                            dataGridView1.Columns.Add("Корпус", "Корпус");
                            dataGridView1.Columns.Add("Этаж", "Этаж");
                            dataGridView1.Columns.Add("Кабинет", "Кабинет");

                            foreach (var s in thisPersonVisits)
                            {
                                string date = s.Время_начала_приёма.ToString();
                                string name = s.Имя_врача;
                                string corpus = s.Корпус;
                                string floor = s.Этаж;
                                string cabinet = s.Кабинет;

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
}
