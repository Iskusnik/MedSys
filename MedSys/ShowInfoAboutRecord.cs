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
    public partial class ShowInfoAboutRecord : Form
    {
        Record record;
        public ShowInfoAboutRecord(Record record)
        {
            this.record = record;
            InitializeComponent();
        }

        private void ShowInfoAboutVisit_Load(object sender, EventArgs e)
        {
            richTextBoxInfo.Text = "Время записи: " + record.Date.ToShortDateString() + "\n";
            richTextBoxInfo.Text += "Врач: " + record.Doctor.FullName + "\n";
            richTextBoxInfo.Text += "Специальность: " + record.Doctor.Job.Name + "\n";
            richTextBoxInfo.Text += "Запись: " + record.Info + "\n";
        }

    }
}
