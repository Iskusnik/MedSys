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
    public partial class ShowEditDeleteIllnesses : Form
    {
        Patient patient;
        ModelMedContainer db = ControlFunctions.dbContext;

        public ShowEditDeleteIllnesses(Patient patient)
        {
            this.patient = patient;
            InitializeComponent();
        }

        private void Showillnesses_Load(object sender, EventArgs e)
        {
            LoadForm();
        }
        private void LoadForm()
        {
            dataGridView1.ReadOnly = true;

            patient = (Patient)db.PersonSet.Find(patient.Id);
            var IllNames = (from n in patient.MedCard.Illness select new { Название_болезни = n.Name, Доп_информация = n.Info, n.Id }).ToList();
            dataGridView1.Columns.Add("Название болезни", "Название болезни");
            dataGridView1.Columns.Add("Дополнительная информация", "Дополнительная информация");
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns["id"].Visible = false;


            foreach (var s in IllNames)
                dataGridView1.Rows.Add(s.Название_болезни, s.Доп_информация, s.Id);
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Refresh();


            var freeIllnesses = (from n in db.IllnessSet select new { Название_болезни = n.Name, Доп_информация = n.Info, n.Id }).ToList();
            freeIllnesses = freeIllnesses.Except(IllNames).ToList();
            comboBox1.Items.Clear();
            foreach (var ill in freeIllnesses)
                comboBox1.Items.Add(ill.Название_болезни);
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedText != "")
            {
                string name = comboBox1.Text;
                Illness ill = (from il in db.IllnessSet where il.Name == name select il).ToList()[0];
                patient.MedCard.Illness.Add(ill);
                db.SaveChanges();
                LoadForm();
            }
            else
                MessageBox.Show("Выберите болезнь");
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0 && dataGridView1.SelectedCells[0].Value != null)
            {
                int id = (int)dataGridView1.SelectedCells[2].Value;
                Illness ill = db.IllnessSet.Find(id);
                patient.MedCard.Illness.Remove(ill);
                db.SaveChanges();
                LoadForm();
            }
            else
                MessageBox.Show("Выберите болезнь");
        }
    }
}
