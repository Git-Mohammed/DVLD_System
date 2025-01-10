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

namespace DVLD
{
    public partial class ChangePassword : Form
    {
        private int UserID;
        private clsUser User;
        public ChangePassword(int UserID)
        {
            InitializeComponent();
            this.UserID = UserID;
        }

        private void _ResetDefualtValues()
        {
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtCurrentPassword.Focus();
        }
        private void ChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            User = clsUser.Find(UserID);
            if(User == null)
            {
                clsUtil.ShowError("There is no user with this id = " + UserID.ToString());
                return;
            }

           ctrlUserInfo2.LoadData(UserID);
        }


        private bool ValidateCurrentPassword()
        {
            return (txtCurrentPassword.Text == User.Password);
        }
        private bool ValidateConfirmPassword()
        {
            return (txtNewPassword.Text ==txtConfirmPassword.Text);
        }
        private void txtCurrentPassword_Leave(object sender, EventArgs e)
        {
           if(!ValidateCurrentPassword())
            {
                errorProvider1.SetError(txtCurrentPassword, "Wrong Current Password");
                txtCurrentPassword.Focus();
                return;
            }
           else
            {
                errorProvider1.SetError(txtCurrentPassword, "");

            }
        }



        private bool ValidateTextBoxIsNotEmpty(object sender)
        {
            TextBox txtFeild = (TextBox)sender;
            if(string.IsNullOrEmpty(txtFeild.Text))
            {
                errorProvider1.SetError(txtFeild, "This Feield should not be null");
                txtFeild.Focus();
                return false;
            }
            else
            {
                errorProvider1.SetError(txtFeild, "");
                return true;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (User == null)
                return;

            if(!ValidateTextBoxIsNotEmpty(txtCurrentPassword) || !ValidateCurrentPassword() ||
                !ValidateTextBoxIsNotEmpty(txtNewPassword) || !ValidateTextBoxIsNotEmpty(txtConfirmPassword) ||
                ! ValidateConfirmPassword())
            {
                return;
            }

            User.Password = txtNewPassword.Text;

            if(User.Save())
            {
                MessageBox.Show("Saved Successfully :D","Saved",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Not Saved Successfully :C", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateConfirmPassword())
            {
                errorProvider1.SetError(txtConfirmPassword, "Wrong Confirmation Password");
                txtCurrentPassword.Focus();
                return;
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, "");

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
