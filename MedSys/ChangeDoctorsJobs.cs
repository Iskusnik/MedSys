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
    public partial class ChangeDoctorsJobs : Form
    {
        Doctor doctor;
        public ChangeDoctorsJobs(Doctor doctor)
        {
            this.doctor = doctor;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listBoxFreeJobs.SelectedItem)
        }

        private void ChangeDoctorsJobs_Load(object sender, EventArgs e)
        {

        }
    }
}
