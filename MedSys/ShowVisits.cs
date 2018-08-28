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

                var thisPersonVisits = (from visit in patient.TimeForVisit select new { Время_начала_приёма = visit.VisitTime, Имя_врача = visit.Doctor.FullName }).ToList();
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("Время начала приёма", "Время начала приёма");
                dataGridView1.Columns.Add("Имя врача", "Имя врача");

                foreach (var s in thisPersonVisits)
                {
                    string date = s.Время_начала_приёма.ToString();
                    string name = s.Имя_врача;

                    dataGridView1.Rows.Add(date, name);
                }
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Refresh();
            }
        }
    }
}
