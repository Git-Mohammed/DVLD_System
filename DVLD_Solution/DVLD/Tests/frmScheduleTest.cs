using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class frmScheduleTest : Form
    {
        private int _DLA_ID = -1;
        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;
        private int _AppointmentID = -1;
        public frmScheduleTest(int DLA_ID, clsTestTypes.enTestType TestTypeID, int TestAppointment=-1)
        {
            InitializeComponent();
            _DLA_ID=DLA_ID;
            _TestTypeID=TestTypeID;
            _AppointmentID = TestAppointment;
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            crlScheduleTest1.TestTypeID = _TestTypeID;
            crlScheduleTest1.LoadInfo(_DLA_ID, _AppointmentID);
        }
    }
}
