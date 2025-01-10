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

namespace DVLD
{
    public partial class ctrlPersonDetails : UserControl
    {

        private  int _PersonID = -1;
        private clsPerson _Person;
        public int PersonID
        {
            get { return _PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }

        public ctrlPersonDetails()
        {
            InitializeComponent();

        }
        public  void _LoadDataInfo(int PersonID)
        {
             _Person = clsPerson.Find(PersonID);
            if( _Person == null )
            {
                LoadDefualtData();
                MessageBox.Show("No Person With PersonID = " + PersonID.ToString(),"Error");
                return;
            }

            _FillPersonInfo();
        }
        public void _LoadDataInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);
            if (_Person == null)
            {
                LoadDefualtData();
                MessageBox.Show("No Person With PersonID = " + PersonID.ToString(), "Error");
                return;
            }

            _FillPersonInfo();
        }
        private void _FillPersonInfo()
        {
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblPesonFullName.Text = _Person.FullName;
            lblNationalNo.Text = _Person.NationalNo;
            if (!string.IsNullOrEmpty(_Person.Email))
                lblEmail.Text = _Person.Email;

            else
                lblEmail.Text = "Dont have email";

            lblAddress.Text = _Person.Address;
            lblCountry.Text = clsCountry.Find(_Person.CountryID).CountryName;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblPhone.Text = _Person.Phone;
            if (_Person.Gender == 0)
            {
                lblGender.Text = "Male";
                pbMale.Visible = true;
                pbImagePerson.Image = Properties.Resources.Male_512;
                

            }
            else
            {
                lblGender.Text = "Female";
                pbMale.Visible = false;
                pbImagePerson.Image = Properties.Resources.Female_512;

            }

            _LoadPersonImage();
        }

        private void _LoadPersonImage()
        {
            string ImagePath = _Person.ImagePath;
            if(!string.IsNullOrEmpty(ImagePath) )
            {
                if (File.Exists(ImagePath))
                    pbImagePerson.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image = " + ImagePath, "Error");
            }
        }
        public void LoadDefualtData()
        {
            lblPersonID.Text = "[????]";
            lblPesonFullName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblEmail.Text = "[????]";
            lblAddress.Text = "[????]";
            lblCountry.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblPhone.Text = "[????]";
            lblGender.Text = "[????]";
            pbMale.Visible = true;
            pbImagePerson.Image = Properties.Resources.Male_512;
        }

        private void lbEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_PersonID == -1)
                return;

            using (frmAddEditPersonInfo frm = new frmAddEditPersonInfo(_PersonID))
            {
                frm.LoadData += _LoadDataInfo;
                frm.ShowDialog();

            }

        }

        private void lblEmail_Click(object sender, EventArgs e)
        {

        }
    }
}
