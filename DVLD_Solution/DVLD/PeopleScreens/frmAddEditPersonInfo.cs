using DVLD.Properties;
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
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using DVLD.GlobalClasses; // Include this namespace
namespace DVLD
{
    public partial class frmAddEditPersonInfo : Form
    {

        public delegate void LoadPersonInfo(int PersonID);

        public event LoadPersonInfo LoadData;
        public enum enMode { Addnew = 0, Update = 1 }
        public enum enGendor { Male = 0, Female = 1 }
        private enMode _Mode;
        private int _PersonID = -1;
        private clsPerson _Person;

        public frmAddEditPersonInfo()
        {
            InitializeComponent();

            _Mode = enMode.Addnew;

        }
        public frmAddEditPersonInfo(int PersonID)
        {
            InitializeComponent();
            if (PersonID == -1)
                _Mode = enMode.Addnew;

            _PersonID = PersonID;
             _Mode = enMode.Update;

        }

        private void _FillComoboBoxOfCountries()
        {
            DataTable dt = clsCountry.AllCountries();

            foreach (DataRow row in dt.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }

            cbCountry.SelectedIndex = cbCountry.FindString("Yemen");
        }

        private void _ResetDefaultValues()
        {
            _FillComoboBoxOfCountries();

            if (_Mode == enMode.Addnew)
            {
                lblMode.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblMode.Text = "Update Person";

            }


                if (rbMale.Checked)
                    pbImagePerson.Image = Properties.Resources.Male_512;
                else
                    pbImagePerson.Image = Properties.Resources.Female_512;
            llRemove.Visible = (pbImagePerson.ImageLocation != null);

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-80);

            cbCountry.SelectedIndex = cbCountry.FindString("Yemen");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";

        }
        private void _LoadData()
        {

            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("this form will be closed");
                this.Close();
                return;
            }



            lblPersonID.Text = _Person.PersonID.ToString();
            txtNationalNo.Text = _Person.NationalNo;
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;

            if (_Person.Gender == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;



            dtpDateOfBirth.Value =Convert.ToDateTime(_Person.DateOfBirth.ToString());
            txtPhone.Text = _Person.Phone;
            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Person.CountryID).CountryName);

            if (_Person.ImagePath != "")
            {
                pbImagePerson.ImageLocation = _Person.ImagePath;
            }


            llRemove.Visible = (_Person.ImagePath != "");

        }
        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if(_Mode == enMode.Update)
                _LoadData();
           
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbImagePerson.ImageLocation == null)
                pbImagePerson.Image = Properties.Resources.Male_512;
        }

        private void lbSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Open file dialog to select an image
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string sourceFile = openFileDialog.FileName;

                if (File.Exists(sourceFile))
                {
                    try
                    {

                        pbImagePerson.ImageLocation = sourceFile;
                        llRemove.Visible = true;
                    }
                    catch (IOException ioEx)
                    {
                        MessageBox.Show($"An error occurred while loading the file: {ioEx.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("File not found!");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not vaild!");
                return;
            }

            if (!HandleImageOperations()) return;
            
            int countryID = clsCountry.Find(cbCountry.Text).CountryID;

            // Set person data
            SetPersonData(countryID);

            // Handle image deletion and copying

            // Save data
            if (_Person.Save())
            {
                MessageBox.Show("Data Saved Successfully :D");
                _Mode = enMode.Update;
                lblMode.Text = "Update Person";
                lblPersonID.Text = _Person.PersonID.ToString();
                LoadData?.Invoke(_Person.PersonID);
            }
            else
            {
                MessageBox.Show("Error: Data Is Not Saved Successfully!");
            }
        }

        private bool ValidateTextBox(TextBox textBox, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                _SetErrorProviderToTextBox(textBox, e);
                return false;
            }
            return true;
        }

        private void SetPersonData(int countryID)
        {
            _Person.LastName = txtLastName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.Phone = txtPhone.Text;
            _Person.NationalNo = txtNationalNo.Text;
            _Person.FirstName = txtFirstName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.Email = txtEmail.Text;
            _Person.CountryID = countryID;
            _Person.Gender = (short)(rbMale.Checked ? 0 : 1);
            _Person.DateOfBirth = DateTime.Parse(dtpDateOfBirth.Text);
            _Person.Address = txtAddress.Text;
            if (!string.IsNullOrEmpty(pbImagePerson.ImageLocation))
            {
                _Person.ImagePath = pbImagePerson.ImageLocation.ToString();
            }
            else
                _Person.ImagePath = "";
        }

        private bool HandleImageOperations()
        {
            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                try
                {
                    File.Delete(_Person.ImagePath);
                }
                catch (IOException iox)
                {
                    MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }

            if(pbImagePerson.ImageLocation != null)
            {
                string sourceFile = pbImagePerson.ImageLocation.ToString();

                if(clsUtil.CopyImageToProjectImagesFolder(ref sourceFile))
                {
                    pbImagePerson.ImageLocation = sourceFile;
                    return true;

                }
                else
                {
                    MessageBox.Show("Error Copying Image File!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;

        }
        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbImagePerson.ImageLocation = null;
            if (rbMale.Checked)
                pbImagePerson.Image = Properties.Resources.Male_512;
            
            else
                pbImagePerson.Image = Properties.Resources.Female_512;
            
            llRemove.Visible = false;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbImagePerson.ImageLocation == null)
                pbImagePerson.Image = Properties.Resources.Female_512;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _SetErrorProviderToTextBox(object sender, CancelEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if(string.IsNullOrEmpty(txtBox.Text))
            {
                e.Cancel = true;
                txtBox.Focus();
                errorProvider1.SetError(txtBox, "the field should has a value");
                
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtBox, "");
            }
        }
        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input }
            }
        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            _SetErrorProviderToTextBox(sender, e);
        }

        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            _SetErrorProviderToTextBox(sender, e);

        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            _SetErrorProviderToTextBox(sender, e);

        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            _SetErrorProviderToTextBox(sender, e);
            

            if(txtNationalNo.Text.Trim() != _Person.NationalNo && clsPerson.IsPersonExists(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "this national no has be owned by another person");

            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void txtNationalNo_Leave(object sender, EventArgs e)
        {
           
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            _SetErrorProviderToTextBox(sender, e);

        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtEmail.Text))
            {
                string email = txtEmail.Text;
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; 
                if (!Regex.IsMatch(email, pattern))
                {
                    if(!string.IsNullOrEmpty(email))
                    errorProvider1.SetError(txtEmail, "Invaild Email :C");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txtEmail, "");

                }
            }

        }
        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            _SetErrorProviderToTextBox(sender, e);

        }


    }
}
