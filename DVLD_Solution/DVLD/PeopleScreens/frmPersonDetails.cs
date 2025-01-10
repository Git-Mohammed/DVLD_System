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

namespace DVLD
{
    public partial class frmPersonDetails : Form
    {
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            if (PersonID == -1 && !clsPerson.IsPersonExists(PersonID))
            {
                MessageBox.Show("Error: this person not exists with PersonID = " + PersonID.ToString());
                return;
            }
            ctrlPersonDetails1._LoadDataInfo(PersonID);

        }
        public frmPersonDetails(string  NationalNo)
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(NationalNo) && !clsPerson.IsPersonExists(NationalNo))
            {
                MessageBox.Show("Error: this person not exists with national no = " + NationalNo);
                return;
            }
            ctrlPersonDetails1._LoadDataInfo(NationalNo);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
