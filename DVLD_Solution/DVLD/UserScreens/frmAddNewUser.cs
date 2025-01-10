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
    public partial class frmAddNewUser : Form
    {

        int _personID;
        private clsUser user;
        public frmAddNewUser(int personID)
        {
            InitializeComponent();
            _personID = personID;
        }

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            ctrlAddNewUser1.LoadData(_personID);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ctrlAddNewUser1.Save())
            {
                MessageBox.Show("Saved User Successfully !", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblMode.Text = "Update User";
            }
            else
            {
                MessageBox.Show("Error dont Saved User Successfully !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
