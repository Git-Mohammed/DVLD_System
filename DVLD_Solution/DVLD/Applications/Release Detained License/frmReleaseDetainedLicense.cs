using DVLD.Applications.DLA;
using DVLD.GlobalClasses;
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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Applications.ApplicationTypes
{
    public partial class frmReleaseDetainedLicense : Form
    {

        private int _SelectedLicenseID = -1;

        public frmReleaseDetainedLicense(int licenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = licenseID;
            ctrlFindLicenseWithFilter2.LoadData(licenseID);
            ctrlFindLicenseWithFilter2.FilterEnabled = false;

        }
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }
      




        // Show confirmation message box
        private DialogResult ShowConfirm(string message)
        {
            return MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        // Show error message box
        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Close button click event handler
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlFindLicenseWithFilter2_Load(object sender, EventArgs e)
        {

        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            
        }

        private void ctrlFindLicenseWithFilter2_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            lblLicenseID.Text = _SelectedLicenseID.ToString();

            lLShowLicenseInfo.Enabled = (_SelectedLicenseID != -1);


            if (_SelectedLicenseID == -1)

            {
                return;
            }

            //ToDo: make sure the license is not detained already.
            if (!ctrlFindLicenseWithFilter2.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License i is not detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationTypeFees.ToString();
            lblUsername.Text = clsGlobal.CurrentUser.Username;

            lblDetainID.Text = ctrlFindLicenseWithFilter2.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            lblLicenseID.Text = ctrlFindLicenseWithFilter2.SelectedLicenseInfo.LicenseID.ToString();

            lblUsername.Text = ctrlFindLicenseWithFilter2.SelectedLicenseInfo.DetainedInfo.DetainedByUserInfo.Username;
            lblDetainDate.Text = clsFormat.DateToShort(ctrlFindLicenseWithFilter2.SelectedLicenseInfo.DetainedInfo.DetainDate);
            lblFineFees.Text = ctrlFindLicenseWithFilter2.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text)).ToString();

            btnRelease.Enabled = true;
        }

     
        private void btnRelease_Click(object sender, EventArgs e)
        {
            if(ShowConfirm("Are you sure do you want to release this license ?") == DialogResult.No)
            {
                return;
            }
            int ApplicationID = -1;
            bool ReleasedDetainedLicense = ctrlFindLicenseWithFilter2.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID);
            if (!ReleasedDetainedLicense)
            {
                clsUtil.ShowError("Didnt Release The License");
                return;
            }

            clsUtil.ShowSuccessful("Released Successfully!");
            lblReleaseAppID.Text = ApplicationID.ToString();
            btnRelease.Enabled = false;
            ctrlFindLicenseWithFilter2.FilterEnabled = false;
            lLShowLicenseInfo.Enabled = true;


        }

        private void lLShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(ctrlFindLicenseWithFilter2.SelectedLicenseInfo.DriverID);
            frm.ShowDialog();  
        }

        private void lLShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }
    }
}
