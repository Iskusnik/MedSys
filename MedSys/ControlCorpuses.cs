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
    public partial class ControlCorpuses : Form
    {
        ModelMedContainer db = ControlFunctions.dbContext;

        public ControlCorpuses()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void ReloadForm(bool RefreshDataGrid = false, bool selectByCab = false)
        {

            if (RefreshDataGrid)
            {
                if (!selectByCab)
                {
                    var cabinetsInfo = (from cabinet in db.CabinetSet select new { Корпус = cabinet.Corpus.Name, Этаж = cabinet.Floor, Номер = cabinet.Num, id = cabinet.Id }).ToList();
                    dataGridView1.Columns.Clear();
                    dataGridView1.Columns.Add("Корпус", "Корпус");
                    dataGridView1.Columns.Add("Этаж", "Этаж");
                    dataGridView1.Columns.Add("Номер", "Номер");
                    dataGridView1.Columns.Add("Id", "Id");
                    dataGridView1.Columns["Id"].Visible = false;

                    foreach (var s in cabinetsInfo)
                    {
                        dataGridView1.Rows.Add(s.Корпус, s.Этаж, s.Номер, s.id);
                    }
                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.Refresh();
                }
                else
                {
                    Corpus corpus = (from c in db.CorpusSet where c.Name == comboBoxJobs.Text select c).ToList()[0];
                    var cabinetsInfo = (from cabinet in corpus.Cabinet select new { Корпус = cabinet.Corpus.Name, Этаж = cabinet.Floor, Номер = cabinet.Num, id = cabinet.Id }).ToList();
                    dataGridView1.Columns.Clear();
                    dataGridView1.Columns.Add("Корпус", "Корпус");
                    dataGridView1.Columns.Add("Этаж", "Этаж");
                    dataGridView1.Columns.Add("Номер", "Номер");
                    dataGridView1.Columns.Add("Id", "Id");
                    dataGridView1.Columns["Id"].Visible = false;

                    foreach (var s in cabinetsInfo)
                    {
                        dataGridView1.Rows.Add(s.Корпус, s.Этаж, s.Номер, s.id);
                    }
                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.Refresh();
                }
                dataGridView1.Refresh();
            }
            else
            {
                comboBoxJobs.Items.Clear();
                var corpuses = (from c in db.CorpusSet select c).ToList();

                foreach (var c in corpuses)
                    comboBoxJobs.Items.Add(c.Name);
            }
        }

        private void ChangeJobs_Load(object sender, EventArgs e)
        {
            ReloadForm(RefreshDataGrid: false);
        }

        private void buttonRemoveDocFromJob_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                MessageBox.Show("Выберите кабинет", "Ошибка");
            else
            {
                int id = (int)dataGridView1.SelectedCells[3].Value;
                Cabinet cabinet = (Cabinet)db.CabinetSet.Find(id);

                Form editCabinet = new EditCabinet(cabinet);
                editCabinet.ShowDialog();
                ReloadForm(true);
            }
            ReloadForm();
        }

        private void buttonDeleteJob_Click(object sender, EventArgs e)
        {
            string corpusName = comboBoxJobs.Text;
            if (comboBoxJobs.Text != "")
            {
                if (MessageBox.Show(text: "Все кабинеты в данном корпусе и все приёмы в тех кабинетах будут удалены, продолжить?",
                                         caption: "Продолжить?",
                                         buttons: MessageBoxButtons.YesNo,
                                         icon: MessageBoxIcon.Question) == DialogResult.Yes)
                    ControlFunctions.RemoveCorpus(corpusName);

                ReloadForm();
            }
            else
                MessageBox.Show("Выберите корпус");
        }

        private void buttonAddJob_Click(object sender, EventArgs e)
        {
            string corpusName = textBoxCorpus.Text;
            if (corpusName != "")
            {

                int floors = (int)numericUpDown1.Value;

                Corpus newCorpus = ControlFunctions.CreateCorpus(floors, corpusName);
                string result = ControlFunctions.AddCorpus(newCorpus);
                if (result != null)
                    MessageBox.Show(result, "Ошибка");
                else
                    ReloadForm();
            }
            else
                MessageBox.Show("Введите название корпуса");
        }

        private void comboBoxJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadForm(true, true);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBoxCorpus_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
