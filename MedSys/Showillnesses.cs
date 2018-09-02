using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedSys
{
    public partial class ShowIllnesses : Form
    {
        Patient patient;
        ModelMedContainer db = new ModelMedContainer();

        public ShowIllnesses(Patient patient)
        {
            this.patient = patient;
            InitializeComponent();
        }

        private void Showillnesses_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;

            patient = (Patient)db.PersonSet.Find(patient.Id);
            var IllNames = (from n in patient.MedCard.Illness select new { Название_болезни = n.Name, Доп_информация = n.Info}).ToList();
            dataGridView1.Columns.Add("Название болезни", "Название болезни");
            dataGridView1.Columns.Add("Дополнительная информация", "Дополнительная информация");
            foreach (var s in IllNames)
                dataGridView1.Rows.Add(s.Название_болезни, s.Доп_информация);
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Refresh();

        }
    }
}
