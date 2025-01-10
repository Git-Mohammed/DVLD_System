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
    public partial class frmUpdateApplicationType : Form
    {
        private int _ApplicationTypeID = -1;
        clsApplicationType ApplicationType;
        public frmUpdateApplicationType(int ApplicationID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationID;
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) 
            {
                e.Handled = true;
            }
        }
        private bool ValidateFeildsIsNotEmpty(object sender)
        {
            TextBox txtFeild = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(txtFeild.Text))
            {
                errorProvider1.SetError(txtFeild, $"the felied should has a value");
                txtFeild.Focus();
                return false;
            }
            else
            {
                errorProvider1.SetError(txtFeild, $"");
                return true;
            }
        }
            private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            lblApplicationTypeID.Text = _ApplicationTypeID.ToString();
            ApplicationType = clsApplicationType.Find(_ApplicationTypeID);

            if( ApplicationType != null )
            {
                txtApplicationTypeTitle.Text = ApplicationType.ApplicationTypeTitle;
                txtApplicationFees.Text = ApplicationType.ApplicationTypeFees.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ApplicationType == null)
                return;

            if(!this.ValidateChildren())
            {
                clsUtil.ShowError("There is some fields are empty !");
                return;
            }

            ApplicationType.ApplicationTypeTitle = txtApplicationTypeTitle.Text.Trim();
            ApplicationType.ApplicationTypeFees = Convert.ToSingle( txtApplicationFees.Text.Trim());

            if(ApplicationType.Save())
            {
                clsUtil.ShowSuccessful("Data Saved Successfully !");
            }
            else
            {
                clsUtil.ShowError("Not Saved Successfully !");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtApplicationTypeTitle_Validating(object sender, CancelEventArgs e)
        {
            ValidateFeildsIsNotEmpty(txtApplicationTypeTitle);
        }

        private void txtApplicationFees_Validating(object sender, CancelEventArgs e)
        {
            ValidateFeildsIsNotEmpty(txtApplicationFees);
        }
    }
}
