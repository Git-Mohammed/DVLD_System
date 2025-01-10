using DVLD.Applications.DLA;
using DVLD.GlobalClasses;
using DVLD.UserControls;
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
using static DVLD_BusinessLayer.clsLicense;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Applications.ApplicationTypes
{
    public partial class frmReplaceLicenseForLostOrDamaged : Form
    {
        private int _NewLicenseID = -1;
        public frmReplaceLicenseForLostOrDamaged()
        {
            InitializeComponent();
        }

        private int _GetApplicationTypeID()
        {
            return (rbDamagedLicense.Checked) ? (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense : (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
        }
        private enIssueReason _GetIssueReason()
        {
            //this will decide which reason to issue a replacement for

            if (rbDamagedLicense.Checked)

                return enIssueReason.DamagedReplacement;
            else
                return enIssueReason.LostReplacement;
        }


        private DialogResult ShowConfirm(string message)
        {
            return MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        // Show error message box
        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmReplaceLicenseForLostOrDamaged_Load(object sender, EventArgs e)
        {
            lblAppDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblUsername.Text = clsGlobal.CurrentUser.Username;
            rbDamagedLicense.Checked = true;
        }

        private void ctrlFindLicenseWithFilter2_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            lLShowLicenseInfo.Enabled = (SelectedLicenseID != -1);
            if (SelectedLicenseID == -1)
                return;

            //dont allow a replacement if is Active .
            if (!ctrlFindLicenseWithFilter2.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }
            if(!ctrlFindLicenseWithFilter2.SelectedLicenseInfo.CheckIsNotExpiration())
            {
                MessageBox.Show($"Selected License is expired!, you can not Issue a replacement for this License: {ctrlFindLicenseWithFilter2.SelectedLicenseInfo.ExpirationDate}", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }
            btnIssue.Enabled = true;
        }


        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblReplacementFor.Text = "Replacement for Damaged License";
            this.Text = lblReplacementFor.Text;
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).ApplicationTypeFees.ToString();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblReplacementFor.Text = "Replacement for Lost License";
            this.Text = lblReplacementFor.Text;
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).ApplicationTypeFees.ToString();
        }

        private void lLShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(ctrlFindLicenseWithFilter2.SelectedLicenseInfo.DriverID);
            frm.ShowDialog();
            frmReplaceLicenseForLostOrDamaged_Load(null, null);
        }
        private DialogResult MessageBoxConfirm(string message)
        {
            return MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private DialogResult MessageBoxError(string message)
        {
            return MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Issue a Replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsLicense NewLicense =
               ctrlFindLicenseWithFilter2.SelectedLicenseInfo.Replace(_GetIssueReason(),
               clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Issue a replacemnet for this  License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblReplacementApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;

            lblOldLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Replaced Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssue.Enabled = false;
            gbReplacementFor.Enabled = false;
            ctrlFindLicenseWithFilter2.FilterEnabled = false;
            lLShowLicenseInfo.Enabled = true;
        }

        private void lLShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var frm = new frmShowLicenseInfo(_NewLicenseID))
            {
                frm.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
