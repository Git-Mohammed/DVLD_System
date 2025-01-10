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

namespace DVLD
{
    public partial class frmAddEditUserInfo : Form
    {
        private enum enMode { AddNew = 0, Update =1 }
        private enMode _Mode;
        private int _UserID  = -1;
        clsUser _User;
    //   ctrlAddNewUser ctrlAddNewUser1 = new ctrlAddNewUser();

        public frmAddEditUserInfo()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddEditUserInfo(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            if(UserID == -1 && !clsUser.IsUserExists(UserID))
            {
                return;
            }
            _Mode = enMode.Update;
        }
        private void _LoadData()
        {

            _User = clsUser.Find(_UserID);
            ctrlFindPersonWithFilter1.FilterEnabled = false;
            if (_User == null)
            {
                clsUtil.ShowError("No User With ID = " + _UserID.ToString());
                this.Close();
                return;
            }
            lblUserID.Text = _User.UserID.ToString();
            txtUsername.Text = _User.Username;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chbIsActive.Checked = _User.isActive;
            ctrlFindPersonWithFilter1.LoadData(_User.PersonID);
        }

        private void _ResetDefualtValues()
        {
            if(_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New User";
                _User = new clsUser();
                tpLoginInfo.Enabled = false;
                btnSave.Enabled = false;

                ctrlFindPersonWithFilter1.FilterFoucs();
            }
            else
            {
                lblMode.Text = "Update User";
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;

            }
            this.Text = lblMode.Text;

            txtUsername.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";

        }
        private void frmAddEditUserInfo_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if(_Mode == enMode.Update)
                _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if(!this.ValidateChildren())
            {
                clsUtil.ShowError("Some fileds are not valide!");
                return;
            }

            _User.PersonID = ctrlFindPersonWithFilter1.PersonID;
            _User.Username = txtUsername.Text;
            _User.Password = txtPassword.Text;
            _User.isActive = chbIsActive.Checked;

            if(_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                _Mode = enMode.Update;
                lblMode.Text = "Update User";
                this.Text = lblMode.Text;
                clsUtil.ShowSuccessful("Data Saved Successfully ");

            }
            else
            {
                clsUtil.ShowError("Error: Data Is Not Saved Successfully");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                tcAddEditUser.SelectedTab = tcAddEditUser.TabPages["tpLoginInfo"];
                return;
            }

            // incase add new mode
            if(ctrlFindPersonWithFilter1.PersonID != -1)
            {
                if(clsUser.IsUserExistsByPersonID(ctrlFindPersonWithFilter1.PersonID))
                {
                    clsUtil.ShowError("Selected Person already has a user, choose another one.");
                    ctrlFindPersonWithFilter1.FilterFoucs();
                }
                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcAddEditUser.SelectedTab = tcAddEditUser.TabPages["tpLoginInfo"];
                }
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            };

        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            };
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUsername, "Username cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUsername, null);
            };


            if (_Mode == enMode.AddNew)
            {

                if (clsUser.IsUserExists(txtUsername.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUsername, "username is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtUsername, null);
                };
            }
            else
            {
                //incase update make sure not to use anothers user name
                if (_User.Username != txtUsername.Text.Trim())
                {
                    if (clsUser.IsUserExists(txtUsername.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUsername, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUsername, null);
                    };
                }
            }
        }
    }
}
