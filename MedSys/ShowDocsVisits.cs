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
    public partial class ShowDocsVisits : Form
    {
        Doctor thisDoctor;
        public ShowDocsVisits(Doctor doc)
        {
            thisDoctor = doc;
            InitializeComponent();
        }
        private void ShowDocsVisits_Load(object sender, EventArgs e)
        {
            using (ModelMedContainer db = new ModelMedContainer())
            {
                thisDoctor = (Doctor)db.PersonSet.Find(thisDoctor.BirthDate, thisDoctor.NameHashID);
                var thisPersonVisits = (from visit in thisDoctor.TimeForVisit select new { Время_начала_приёма = visit.Start, Имя_пациента = visit.VisitInfo.Patient.FullName }).ToList();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("Время начала приёма", "Время начала приёма");
                dataGridView1.Columns.Add("Имя пациента", "Имя пациента");

                foreach (var s in thisPersonVisits)
                {
                    string date = s.Время_начала_приёма.ToString();
                    string name = s.Имя_пациента;

                    dataGridView1.Rows.Add(date, name);
                }
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (ModelMedDBContainer db = new ModelMedDBContainer())
            {
                thisDoctor = (Doctor)db.PersonSet.Find(thisDoctor.BirthDate, thisDoctor.NameHashID);

                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        {
                            var thisPersonVisits = (from visit in thisDoctor.WorkTimes select new { Время_начала_приёма = visit.Start, Имя_пациента = visit.VisitInfo.Patient.FullName }).ToList();
                            dataGridView1.Columns.Clear();
                            dataGridView1.Columns.Add("Время начала приёма", "Время начала приёма");
                            dataGridView1.Columns.Add("Имя пациента", "Имя пациента");
                            foreach (var s in thisPersonVisits)
                                dataGridView1.Rows.Add(s.Время_начала_приёма.ToString(), s.Имя_пациента);

                            dataGridView1.RowHeadersVisible = false;
                            dataGridView1.Refresh();
                            break;
                        }
                    case 1:
                        {
                            var thisPersonVisits = (from visit in thisDoctor.WorkTimes where visit.Start > DateTime.Today select new { Время_начала_приёма = visit.Start, Имя_пациента = visit.VisitInfo.Patient.FullName }).ToList();
                            dataGridView1.Columns.Clear();
                            dataGridView1.Columns.Add("Время начала приёма", "Время начала приёма");
                            dataGridView1.Columns.Add("Имя пациента", "Имя пациента");

                            foreach (var s in thisPersonVisits)
                                dataGridView1.Rows.Add(s.Время_начала_приёма.ToString(), s.Имя_пациента);

                            dataGridView1.RowHeadersVisible = false;
                            dataGridView1.Refresh();
                            break;
                        }
                    case 2:
                        {
                            var thisPersonVisits = (from visit in thisDoctor.WorkTimes where (visit.Start <= DateTime.Today) select new { Время_начала_приёма = visit.Start, Имя_пациента = visit.VisitInfo.Patient.FullName }).ToList();
                            dataGridView1.Columns.Clear();
                            dataGridView1.Columns.Add("Время начала приёма", "Время начала приёма");
                            dataGridView1.Columns.Add("Имя врача", "Имя врача");
                            foreach (var s in thisPersonVisits)
                                dataGridView1.Rows.Add(s.Время_начала_приёма.ToString(), s.Имя_пациента);

                            dataGridView1.RowHeadersVisible = false;
                            dataGridView1.Refresh();
                            break;
                        }
                }
            }
        }
    }
}
