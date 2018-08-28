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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string mes;
            Person pers;
            if (!ControlFunctions.LoginResult(this.textBoxFullName.Text, this.textBoxPassword.Text, out mes, out pers))
                MessageBox.Show(mes);
            else
                if (pers is Patient)
            {
                PatientMenu patientMenu = new PatientMenu((Patient)pers);
                patientMenu.ShowDialog();
            }
            else
            {
                DoctorMenu doctorMenu = new DoctorMenu((Doctor)pers);
                doctorMenu.ShowDialog();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
