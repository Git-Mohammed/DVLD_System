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

namespace DVLD.Applications.TestTypes
{
    public partial class frmUpdateTestType : Form
    {
        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;
        private clsTestTypes TestType;
        public frmUpdateTestType(clsTestTypes.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (TestType == null)
                return;

            if (!this.ValidateChildren())
            {
                clsUtil.ShowError("Some fileds are not valide!");
                return;
            }

            TestType.TestTypeTitle = txtTypeTitle.Text.Trim();

           TestType.TestDescription = txtDescription.Text.Trim();

            TestType.TestTypeFees = Convert.ToSingle(txtFees.Text.Trim());

            if (TestType.Save())
            {
                MessageBox.Show("Saved Successfully !");
            }
            else
            {
                MessageBox.Show("Not Saved Successfully !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            TestType = clsTestTypes.Find(_TestTypeID);
            if(TestType != null)
            {
                lblTypeID.Text = ((int)TestType.ID).ToString();
                txtTypeTitle.Text = TestType.TestTypeTitle;
                txtDescription.Text = TestType.TestDescription;
                txtFees.Text = TestType.TestTypeFees.ToString();
            }
            else
            {
                clsUtil.ShowError("Could not find Test Type with id = " + _TestTypeID.ToString());
                this.Close();
            }
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTypeTitle_Validating(object sender, CancelEventArgs e)
        {
            ValidateFeildsIsNotEmpty(txtTypeTitle);
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            ValidateFeildsIsNotEmpty(txtDescription);

        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            ValidateFeildsIsNotEmpty(txtFees);

        }
    }
}
