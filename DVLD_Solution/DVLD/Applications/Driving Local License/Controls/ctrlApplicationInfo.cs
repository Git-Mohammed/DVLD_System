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

namespace DVLD.UserControls
{
    public partial class ctrlApplicationInfo : UserControl
    {
      
        private clsDLA DLA;
        private int _DLA_ID = -1;
        private int _LicenseID;

        public int DLA_ID
        {
            get { return _DLA_ID; }
        }
        public ctrlApplicationInfo()
        {
            InitializeComponent();
        }

        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            _DLA_ID = -1;
            lblLicenseClass.Text = "[???]";
            lblDLA_ID.Text = "[???]";
            lblPassedTests.Text = "[???]";
            ctrlApplicationBasicInfo1.ResetApplicationInfo();
            lLShowLicenseInfo.Enabled = false;
        }

        private void _FillLocalDrivingLicenseApplicationInfo()
        {
            _LicenseID = DLA.GetActiveLicenseID();
            lLShowLicenseInfo.Enabled = (_LicenseID != -1);
            lblDLA_ID.Text = DLA.DLA_ID.ToString();
            lblLicenseClass.Text = DLA.LicenseClassInfo.ClassName;
            lblPassedTests.Text = "";
            ctrlApplicationBasicInfo1.LoadApplicationInfo(DLA.ApplicationID);
        }
        public void LoadDataByAppID(int ApplicationID)
        {
            DLA = clsDLA.FindByAppID(ApplicationID);
            if(DLA == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();
                clsUtil.ShowError("No Application with ApplicationID = " + ApplicationID.ToString());
                return;
            }
            _FillLocalDrivingLicenseApplicationInfo();
        }
        public void LoadDataByID(int DLA_ID)
        {
            DLA = clsDLA.Find(DLA_ID);
            if (DLA == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();
                clsUtil.ShowError("No Application with ApplicationID = " + DLA_ID.ToString());
                return;
            }
            _FillLocalDrivingLicenseApplicationInfo();
        }
        public void EnableShowLicenseInfo()
        {
            lLShowLicenseInfo.Enabled = true;
        }
        private void lLShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(DLA.DLA_ID);
            frm.ShowDialog();
        }
    }
}
