using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.GlobalClasses;
using DVLD_BusinessLayer;
using static System.Net.Mime.MediaTypeNames;
namespace DVLD.Applications.DLA
{
    public partial class frmIssueDLForFirstTime : Form
    {
        private int _DLA_ID;
        clsDLA DLA;
        public frmIssueDLForFirstTime(int dLA_ID)
        {
            InitializeComponent();
            _DLA_ID = dLA_ID;
        }        

       
        private void btnIssue_Click(object sender, EventArgs e)
        {
            int LicenseID = DLA.IssueLicenseForTheFirtTime(txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);

            if (LicenseID != -1)
            {

                MessageBox.Show($" Issue Successfully with LicenseID = {LicenseID} !", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ctrlApplicationInfo1.EnableShowLicenseInfo();
                btnIssue.Enabled = false;
            }
            else
                MessageBox.Show("License Wasn\'t Issued !", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmIssueDLForFirstTime_Load(object sender, EventArgs e)
        {
            txtNotes.Focus();
            DLA = clsDLA.Find(_DLA_ID);
            if(DLA == null)
            {
                clsUtil.ShowError("No Application with ID= " + _DLA_ID.ToString());
                this.Close();
                return;
            }

            if (!DLA.PassedAllTests())
            {
                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            int LicenseID = DLA.GetActiveLicenseID();
            if (LicenseID != -1)
            {

                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlApplicationInfo1.LoadDataByID(_DLA_ID);
        }
    }
}
