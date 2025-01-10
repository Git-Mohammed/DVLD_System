using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.TestAppointments
{
    public partial class frmVisionTestAppointments : Form
    {
        private enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;
        int _DLA_ID;
        public frmVisionTestAppointments(int DLA_ID)
        {
            InitializeComponent();
            
            _DLA_ID = DLA_ID;
            if(_DLA_ID == -1)
            {
                _Mode = enMode.AddNew;
            }
            else
            {
                _Mode = enMode.Update;
            }
        }

        private void frmVisionTestAppointments_Load(object sender, EventArgs e)
        {
            ctrlApplicationInfo1.LoadDataByDLA_ID(_DLA_ID);
        }
    }
}
