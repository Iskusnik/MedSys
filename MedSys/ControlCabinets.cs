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
    public partial class ControlCabinets : Form
    {
        ModelMedContainer db = new ModelMedContainer();

        public ControlCabinets()
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
                    var visitsInfo = (from visit in db.TimeForVisitSet select new { Имя_врача = visit.Doctor.FullName, Имя_пациента = visit.Patient.FullName, Время = visit.VisitTime, id = visit.Id }).ToList();
                    dataGridView1.Columns.Clear();
                    dataGridView1.Columns.Add("Имя врача", "Имя врача");
                    dataGridView1.Columns.Add("Имя пациента", "Имя пациента");
                    dataGridView1.Columns.Add("Время", "Время");
                    dataGridView1.Columns.Add("Id", "Id");
                    dataGridView1.Columns["Id"].Visible = false;

                    foreach (var s in visitsInfo)
                    {
                        dataGridView1.Rows.Add(s.Имя_врача, s.Имя_пациента, s.Время, s.id);
                    }
                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.Refresh();
                }
                else
                {

                    string[] cabInfo = comboBoxJobs.Text.Split(':');
                    string corpusName = cabInfo[0];
                    int cabNum = int.Parse(cabInfo[1]);


                    var visitsInfo = (from visit in db.TimeForVisitSet
                                      where visit.Cabinet.Corpus.Name == cabInfo[0] && visit.Cabinet.Num == cabNum
                                      select new { Имя_врача = visit.Doctor.FullName, Имя_пациента = visit.Patient.FullName, Время = visit.VisitTime, id = visit.Id }).ToList();
                    dataGridView1.Columns.Clear();
                    dataGridView1.Columns.Add("Имя врача", "Имя врача");
                    dataGridView1.Columns.Add("Имя пациента", "Имя пациента");
                    dataGridView1.Columns.Add("Время", "Время");
                    dataGridView1.Columns.Add("Id", "Id");
                    dataGridView1.Columns["Id"].Visible = false;

                    foreach (var s in visitsInfo)
                    {
                        dataGridView1.Rows.Add(s.Имя_врача, s.Имя_пациента, s.Время, s.id);
                    }
                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.Refresh();
                }
                dataGridView1.Refresh();
            }
            else
            {
                comboBoxJobs.Items.Clear();
                var cabinets = (from c in db.CabinetSet select c).ToList();

                foreach (var c in cabinets)
                    comboBoxJobs.Items.Add(c.Corpus.Name + ":" + c.Num.ToString());
            }

        }

        private void ChangeJobs_Load(object sender, EventArgs e)
        {
            ReloadForm(RefreshDataGrid: false);
        }

        private void buttonRemoveDocFromJob_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                MessageBox.Show("Выберите приём", "Ошибка");
            else
            {
                int id = (int)dataGridView1.SelectedCells[2].Value;
                TimeForVisit visit = (TimeForVisit)db.TimeForVisitSet.Find(id);

                Form editVisit = new EditVisit(visit);
                editVisit.ShowDialog();
                ReloadForm(true);
            }
            ReloadForm();
        }

        private void buttonDeleteJob_Click(object sender, EventArgs e)
        {
            string cabinetName = comboBoxJobs.Text;


            if (MessageBox.Show(text: "Все приёмы в данном кабинет будут удалены, продолжить?",
                                     caption: "Продолжить?",
                                     buttons: MessageBoxButtons.YesNo,
                                     icon: MessageBoxIcon.Question) == DialogResult.Yes)
                ControlFunctions.RemoveCabinet(cabinetName);

            ReloadForm();
        }

        private void buttonAddJob_Click(object sender, EventArgs e)
        {
            int cabinetNum = (int)numericUpDownCabinetNum.Value;
            string corpusName = comboBoxCorpus.Text;
            int floorNum = int.Parse(comboBoxFloor.Text);


            Cabinet newCabinet = ControlFunctions.CreateCabinet(corpusName, floorNum, cabinetNum);
            string result = ControlFunctions.AddCabinet(newCabinet);
            if (result != null)
                MessageBox.Show(result, "Ошибка");

            ReloadForm();
        }

        private void comboBoxJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadForm(true, true);
        }

        private void comboBoxCorpus_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxFloor.Items.Clear();

            string[] cabInfo = comboBoxJobs.Text.Split(':');
            string corpusName = cabInfo[0];
            Corpus corpus = (from c in db.CorpusSet where c.Name == corpusName select c).ToList()[0];
            for (int i = 0; i < corpus.Floors; i++)
                comboBoxFloor.Items.Add(i + 1);
        }
    }
}
