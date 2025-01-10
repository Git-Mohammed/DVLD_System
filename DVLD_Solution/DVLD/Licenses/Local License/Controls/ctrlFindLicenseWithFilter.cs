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

namespace DVLD.UserControls
{
    public partial class ctrlFindLicenseWithFilter : UserControl
    {
        public event Action<int> OnLicenseSelected;

        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;

            if(handler != null)
            {
                handler(LicenseID);
            }
        }

       
        public ctrlFindLicenseWithFilter()
        {
            InitializeComponent();
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set { 
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

        private int _LicenseID = -1;

        public int LicenseID
        {
            get { return ctrlDrivingLicenseInfo1.LicenseID; }
                
        }

        public clsLicense SelectedLicenseInfo
        {
            get { return ctrlDrivingLicenseInfo1.SelectedLicenseInfo; }
        }

        private void txtFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
            
            if (e.KeyChar == (char)13) 
                btnFind.PerformClick();
        }

        public void txtLicenseFocus()
        {
            txtFind.Focus();
        }
        public void LoadData(int licenseID)
        {

            txtFind.Text = LicenseID.ToString();
            ctrlDrivingLicenseInfo1.LoadData(licenseID);
            _LicenseID = ctrlDrivingLicenseInfo1.LicenseID;
            if (OnLicenseSelected != null && FilterEnabled)
                OnLicenseSelected(_LicenseID);
          
        }


        private void btnFind_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFind.Focus();
                return;

            }
            _LicenseID = int.Parse(txtFind.Text);
            LoadData(_LicenseID);
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            //DIA_ID = Convert.ToInt32(txtFind.Text.ToString());
        }

        private void txtFind_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFind.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFind, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFind, null);
            }
        }

        private void ctrlFindLicenseWithFilter_Load(object sender, EventArgs e)
        {

        }
    }
}
