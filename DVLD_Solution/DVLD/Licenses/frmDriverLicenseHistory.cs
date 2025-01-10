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

namespace DVLD.Applications.DLA
{
    public partial class frmDriverLicenseHistory : Form
    {
        private int _PersonID = -1;
        public frmDriverLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        public frmDriverLicenseHistory()
        {
            InitializeComponent();
        }
        private void frmDriverLicenseHistory_Load(object sender, EventArgs e)
        {
            if(_PersonID != -1)
            {
                ctrlFindPersonWithFilter1.LoadData(_PersonID);
                ctrlFindPersonWithFilter1.FilterEnabled = false;
                ctrlDriverLicenses1.LoadInfoByPersonID(_PersonID);
            }
            else
            {
                ctrlFindPersonWithFilter1.Enabled = true;
                ctrlFindPersonWithFilter1.FilterFoucs();
            }
        }

        private void tpLocalLicense_Click(object sender, EventArgs e)
        {



        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDriverInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ctrlFindPersonWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;
            if(_PersonID == -1)
                ctrlDriverLicenses1.Clear();
            else
                ctrlDriverLicenses1.LoadInfoByPersonID(_PersonID) ;
        }
    }
}
