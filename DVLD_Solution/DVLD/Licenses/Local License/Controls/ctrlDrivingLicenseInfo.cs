using DVLD_BusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using DVLD.GlobalClasses;
using DVLD.Properties;
namespace DVLD.UserControls
{
    public partial class ctrlDrivingLicenseInfo : UserControl
    {
        private clsLicense _License;
        private int _LicenseID;

        public int LicenseID
        {
            get { return _LicenseID; }
        }

        public clsLicense SelectedLicenseInfo
        { 
            get { return _License; }
        }
        public ctrlDrivingLicenseInfo()
        {
            InitializeComponent();
        }

        // Method to get the issue reason as a string
        private string GetIssueReason(int issueReason)
        {
            string[] issueReasons =
            {
                "First Time", "Renew", "Replacement For Lost",
                "Replacement For Damaged", "Release Detained"
            };

            // Return the issue reason or "Another Reason" if the reason is not found
            return issueReason >= 1 && issueReason <= issueReasons.Length ?
                issueReasons[issueReason - 1] : "Another Reason";
        }

        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersonInfo.Gender == 0)
                pbImagePerson.Image = Resources.Male_512;
            else
                pbImagePerson.Image = Resources.Female_512;

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbImagePerson.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        // Method to load data into the control
        public void LoadData(int licenseID)
        {
            _LicenseID = licenseID;
            _License = clsLicense.Find(_LicenseID);
            if (_License == null )
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }

            lblLicenseID.Text = _License.LicenseID.ToString();
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblisDetained.Text = _License.IsDetained ? "Yes" : "No";
            lblLicenseClass.Text = _License.LicenseClassIfo.ClassName;
            lblPesonFullName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = _License.DriverInfo.PersonInfo.Gender == 0 ? "Male" : "Female";
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.DriverInfo.PersonInfo.DateOfBirth);

            lblDriverID.Text = _License.DriverID.ToString();
            lblIssuedDate.Text = clsFormat.DateToShort(_License.IssueDate);
            lblExpiredDate.Text = clsFormat.DateToShort(_License.ExpirationDate);
            lblIssueReason.Text = _License.GetIssueReasonText();
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
            _LoadPersonImage();
        }

        // Event handler for control load event
        private void ctrlDrivingLicenseInfo_Load(object sender, EventArgs e) { }

        // Event handler for group box enter event
        private void gbDrivingLicenseInfo_Enter(object sender, EventArgs e) { }
    }
}
