using DVLD.GlobalClasses;
using DVLD.UserControls;
using DVLD_BusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.Applications.DLA
{
    public partial class frmAddEditDLA : Form
    {
        // Enum for Mode
        private enum enMode { AddNew = 0, Update = 1}

        // Private fields
        private  enMode _Mode;
        private  int _DLA_ID;
        private readonly int _applicationType = (int)clsApplication.enApplicationType.NewDrivingLicense;
        private clsDLA _dla;
        private int _SelectedPersonID = -1;

        // Constructor
        public frmAddEditDLA()
        {
            InitializeComponent();
          _Mode = enMode.AddNew;
        }

        public frmAddEditDLA(int DLA_ID)
        {
            InitializeComponent();
            _DLA_ID = DLA_ID;
            _Mode = (DLA_ID != -1) ? enMode.Update : enMode.AddNew;
        }

        // Method to load data
        private void LoadData()
        {


            // Load existing DLA data
            ctrlFindPersonWithFilter1.FilterEnabled = false;
            _dla = clsDLA.Find(_DLA_ID);
            if (_dla == null)
            {
                clsUtil.ShowError("No Application with ID = " +  _DLA_ID.ToString());
                this.Close();
                return;
            }

            lblAppDate.Text = DateTime.Now.ToShortDateString();
            lblAppCreatedBy.Text = clsUser.Find(clsGlobal.CurrentUser.UserID).Username;
            lblAppFees.Text = clsApplicationType.Find(_applicationType).ApplicationTypeFees.ToString();
            lblDLApplicationID.Text = _dla.DLA_ID.ToString();
            lblAppDate.Text = clsFormat.DateToShort(_dla.ApplicationDate);
            lblAppCreatedBy.Text = clsUser.Find(_dla.CreatedByUserID).Username;
            ctrlFindPersonWithFilter1.LoadData(_dla.ApplicantPersonID);
        }

        // Method to fill License Classes ComboBox
        private void FillLicenseClassesComboBox()
        {
            foreach (DataRow row in clsLicenseClass.GetAllLicenseClasses().Rows)
            {
                cbLicenseClasses.Items.Add(row["ClassName"]);
            }
            cbLicenseClasses.SelectedIndex = 0;
        }

        // Event handler for Next button click
        private void btnNext_Click(object sender, EventArgs e)
        {

            if(_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcDLA.SelectedTab = tcDLA.TabPages["tpApplicationInfo"];
                return;
            }


            if (ctrlFindPersonWithFilter1.PersonID != -1)
            {
                tcDLA.SelectedTab = tpApplicationInfo;
                pnlApplicationInfo.Enabled = true;
                btnSave.Enabled = true;

            }
            else
            {
                MessageBox.Show("Find Person First !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pnlApplicationInfo.Enabled = false;
                btnSave.Enabled = false;
                ctrlFindPersonWithFilter1.FilterFoucs();

            }
        }
        private void _ResetDefualtValues()
        {
            FillLicenseClassesComboBox();

            if(_Mode == enMode.AddNew)
            {
                lblMode.Text = "New Local Driving License Application";
                _dla = new clsDLA();
                ctrlFindPersonWithFilter1.FilterFoucs();
                tpApplicationInfo.Enabled = false;

                cbLicenseClasses.SelectedIndex = 2;
                lblAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationTypeFees.ToString();
                lblAppDate.Text = DateTime.Now.ToShortDateString();
                lblAppCreatedBy.Text = clsGlobal.CurrentUser.Username;
            }
            else
            {
                lblMode.Text = "Update Local Driving License Application";
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
            }
            this.Text = lblMode.Text;

        }
        // Event handler for Form Load
        private void frmAddEditDLA_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if(_Mode == enMode.Update)
                LoadData();
        }

        // Event handler for Close button click
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Event handler for Save button click
        private void btnSave_Click(object sender, EventArgs e)
        {

            int licenseClassID = clsLicenseClass.Find(cbLicenseClasses.Text).LicenseClassID;
            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_dla.ApplicantPersonID, _applicationType, licenseClassID);

            // Validate new application
           if(ActiveApplicationID != -1)
            {
                clsUtil.ShowError("Choose another License Class, the selected Person Already has ");
                cbLicenseClasses.Focus();
                return;
            }

           if(clsLicense.GetActiveLicenseIDByPersonID(ctrlFindPersonWithFilter1.PersonID,licenseClassID) != -1)
            {
                clsUtil.ShowError("Person already have a license with the same applied ");
                return;

            }

            _dla.ApplicantPersonID = ctrlFindPersonWithFilter1.PersonID;
            _dla.ApplicationDate = DateTime.Now;
            _dla.ApplicationTypeID = _applicationType;
            _dla.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _dla.LastStatusDate = DateTime.Now;
            _dla.PaidFees = Convert.ToSingle(lblAppFees.Text.Trim());
            _dla.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _dla.LicenseClassID = licenseClassID;

            if (_dla.Save())
            {
                lblDLApplicationID.Text = _dla.DLA_ID.ToString();
                _Mode = enMode.Update;
                lblMode.Text = "Update Local Driving License Application";
                clsUtil.ShowSuccessful("Data Saved Successfully.");
            }
            else
                clsUtil.ShowError("Error: Data Is not Saved Successfully.");
        }

        // Method to show error messages
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ctrlFindPersonWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;
        }

        private void frmAddEditDLA_Activated(object sender, EventArgs e)
        {
            ctrlFindPersonWithFilter1.FilterFoucs();
        }
    }
}
