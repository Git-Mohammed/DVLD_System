using DVLD.Applications.DLA;
using DVLD.GlobalClasses;
using DVLD_BusinessLayer;
using System;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class frmRenewDrivingLicense : Form
    {
        private int _NewLicenseID = -1;

        public frmRenewDrivingLicense()
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
            Close();
        }

        // Renew button click event handler
        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (ShowConfirm("Are you sure you want to renew this license?") == DialogResult.No)
            {
                return;
            }

            clsLicense NewLicense = ctrlFindLicenseWithFilter2.SelectedLicenseInfo.RenewLicense(txtNotes.Text, clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            lblRenewApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lblNewLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Renewed Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenew.Enabled = false;
            ctrlFindLicenseWithFilter2.FilterEnabled = false;
            lLShowLicenseInfo.Enabled = true;
        }



        // Link clicked event handler to show license history
        private void lLShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var frm = new frmDriverLicenseHistory(ctrlFindLicenseWithFilter2.SelectedLicenseInfo.DriverID))
            {
                frm.ShowDialog();
            }
        }

        // Link clicked event handler to show license info
        private void lLShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var frm = new frmShowLicenseInfo(_NewLicenseID))
            {
                frm.ShowDialog();
            }
        }

        // License selected event handler
        private void ctrlFindLicenseWithFilter2_OnLicenseSelected(int licenseID)
        {
          int SelectedLicenseID = licenseID;
            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            lLShowLicenseInfo.Enabled = (SelectedLicenseID != -1);
            if (SelectedLicenseID == -1)
                return;

            int DefaultValidityLength = ctrlFindLicenseWithFilter2.SelectedLicenseInfo.LicenseClassIfo.DefaultValidityLength;
            lblExprationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(DefaultValidityLength));
            lblLicenseFees.Text = ctrlFindLicenseWithFilter2.SelectedLicenseInfo.LicenseClassIfo.ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();
            txtNotes.Text = ctrlFindLicenseWithFilter2.SelectedLicenseInfo.Notes;

            if(!ctrlFindLicenseWithFilter2.SelectedLicenseInfo.CheckIsNotExpiration())
            {
                MessageBox.Show("Selected License is not yet expiared, it will expire on: " + clsFormat.DateToShort(ctrlFindLicenseWithFilter2.SelectedLicenseInfo.ExpirationDate)
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                return;
            }

            if(!ctrlFindLicenseWithFilter2.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license."
                   , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                return;
            }

            btnRenew.Enabled = true;

        }

        // Form load event handler
        private void frmRenewDrivingLicense_Load(object sender, EventArgs e)
        {
            ctrlFindLicenseWithFilter2.txtLicenseFocus();
            lblAppDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssuedDate.Text = lblAppDate.Text;

            lblExprationDate.Text = "???";
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationTypeFees.ToString();
            lblUsername.Text = clsGlobal.CurrentUser.Username;
        }

    }
}
