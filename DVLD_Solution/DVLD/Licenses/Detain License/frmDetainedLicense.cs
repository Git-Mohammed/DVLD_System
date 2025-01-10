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

namespace DVLD.Applications.ApplicationTypes
{
    public partial class frmDetainedLicense : Form
    {
        private int _DetainID = -1;
        private int _SelectedLicense=-1;
        public frmDetainedLicense()
        {
            InitializeComponent();
        }

        private bool ValidateFineFeesIsNotEmpty()
        {
            if(string.IsNullOrEmpty(txtFineFees.Text))
            {
                txtFineFees.Focus();
                errorProvider1.SetError(txtFineFees, "Enter Fine Fees Required !");
                return false;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, "");
                return true;
            }
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
        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (ShowConfirm("Are you sure you want to detain this license?") == DialogResult.Yes && ValidateFineFeesIsNotEmpty())
            {
               
                _DetainID = ctrlFindLicenseWithFilter2.SelectedLicenseInfo.Detain(Convert.ToSingle(txtFineFees.Text.Trim()), clsGlobal.CurrentUser.UserID);
                if(_DetainID ==-1)
                {
                    ShowError("Faild to Detain License");
                    return;
                }

                    MessageBox.Show("Detained Successfully!");
                    lLShowLicenseInfo.Enabled = true;
                    btnDetain.Enabled = false;
                    txtFineFees.Enabled = false;
                    ctrlFindLicenseWithFilter2.FilterEnabled = false;
                    lblDetainID.Text = _DetainID.ToString();
                    return;
                
                
            }

        }

        private void lLShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(ctrlFindLicenseWithFilter2.SelectedLicenseInfo.DriverID);
            frm.ShowDialog();
        }

        private void lLShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var frm = new frmShowLicenseInfo(_SelectedLicense))
            {
                frm.ShowDialog();
            }
        }

        private void ctrlFindLicenseWithFilter2_Load(object sender, EventArgs e)
        {

        }

        private void frmDetainedLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblUsername.Text = clsGlobal.CurrentUser.Username;
        }

        private void ctrlFindLicenseWithFilter2_OnLicenseSelected(int obj)
        {
            _SelectedLicense = obj;
            lblLicenseID.Text = _SelectedLicense.ToString();
            lLShowLicenseInfo.Enabled = (_SelectedLicense != -1);

            if (_SelectedLicense == -1)
                return;
            if(ctrlFindLicenseWithFilter2.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License i already detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtFineFees.Focus();
            btnDetain.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
