using DVLD.Applications.DLA;
using DVLD.GlobalClasses;
using DVLD.Licenses.International_License;
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

namespace DVLD.Applications.DIA
{
    public partial class frmAddDIA : Form
    {

        private int _InternationalLicenseID = -1;
        public frmAddDIA()
        {
            InitializeComponent();
           
        }
        public frmAddDIA(int License)
        {
            InitializeComponent();
            _InternationalLicenseID = License;
        }

       
        private DialogResult MessageBoxConfirm(string  message)
        {
            return MessageBox.Show(message,"Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
        }

        private DialogResult MessageBoxError(string message)
        {
            return MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBoxConfirm("Are you sure do you want to issue international License") == DialogResult.No) return;

            clsDIA InternationalLicense = new clsDIA();
            InternationalLicense.ApplicantPersonID = ctrlFindLicenseWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationTypeFees;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            InternationalLicense.DriverID = ctrlFindLicenseWithFilter1.SelectedLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrlFindLicenseWithFilter1.SelectedLicenseInfo.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblDIA_ID.Text = InternationalLicense.ApplicationID.ToString();
            _InternationalLicenseID = InternationalLicense.DIA_ID;
            lblDIA_LicenseID.Text = _InternationalLicenseID.ToString();
            MessageBox.Show("International License Issued Successfully with ID=" +_InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssue.Enabled = false;
            ctrlFindLicenseWithFilter1.FilterEnabled = false;
            lLShowLicenseInfo.Enabled = true;
        }

        private void frmAddDIA_Load(object sender, EventArgs e)
        {
            
        }

        private void ctrlFindLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            
        }

        private void lLShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(ctrlFindLicenseWithFilter1.SelectedLicenseInfo.DriverID);
            frm.ShowDialog();
        }

        private void lLShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(_InternationalLicenseID);
            frm.ShowDialog();
        }

        private void ctrlFindLicenseWithFilter2_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            lblLocalLicenseID.Text = SelectedLicenseID.ToString();
            lLShowLicensesHistory.Enabled = (SelectedLicenseID != -1);
            if (SelectedLicenseID == -1)
                return;

            if(ctrlFindLicenseWithFilter1.SelectedLicenseInfo.LicenseClassID != 3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ActiveInternaionalLicenseID = clsDIA.GetActiveInternationalLicenseIDByDriverID(ctrlFindLicenseWithFilter1.SelectedLicenseInfo.DriverID);

            if (ActiveInternaionalLicenseID != -1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveInternaionalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lLShowLicenseInfo.Enabled = true;
                _InternationalLicenseID = ActiveInternaionalLicenseID;
                btnIssue.Enabled = false;
                return;
            }

            btnIssue.Enabled = true;
        }
    }
}
