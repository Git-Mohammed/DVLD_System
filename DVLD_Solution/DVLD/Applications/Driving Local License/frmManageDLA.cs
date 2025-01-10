using DVLD.Applications.Driving_Local_License;
using DVLD.GlobalClasses;
using DVLD.Tests;
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

namespace DVLD.Applications.DLA
{
    public partial class frmManageDLA : Form
    {
        private DataTable _dtAllLocalDrivingLicenseApplications;
        public frmManageDLA()
        {
            InitializeComponent();
        }
        private string BuildFilterExpression()
        { 
            // Check if the selected column is numeric or string and adjust the filter expression accordingly

            if (cbFilterBy.SelectedIndex == 1) // Assuming index 1 is a numeric column
            {
                return $"{cbFilterBy.SelectedItem} = {txtFilter.Text}"; 
            }
            else // For non-numeric columns
            { 
                return $"{cbFilterBy.SelectedItem} LIKE '{txtFilter.Text}%'"; 
            } 
        }

        private int _GetDLA_ID()
        {
            return (int)dgvDLAList.CurrentRow.Cells[0].Value;
        }
            private void FilterDLAList()
        {
            if(string.IsNullOrWhiteSpace(txtFilter.Text)) 
            {
                dgvDLAList.DataSource = clsDLA.LocalDrivingLicenseApplications();
                return; 
            }
            DataView dv = clsDLA.LocalDrivingLicenseApplications().DefaultView;
            dv.RowFilter = BuildFilterExpression();
            dgvDLAList.DataSource = dv;
            lblRecords.Text = dgvDLAList.RowCount.ToString();
        }
        private void _FillComoboxFilter()
        {
            DataTable dt = clsDLA.LocalDrivingLicenseApplications();
            foreach (DataColumn column in dt.Columns)
            {
                cbFilterBy.Items.Add(column.ColumnName);
            }
            cbFilterBy.SelectedIndex = 0;
        }



        private void EnableAndDisableControls()
        {
            if (clsDLA.Find(_GetDLA_ID()) == null|| _GetDLA_ID() < 1) return;
            if (_GetDLA_ID() == 2)
            {
                tsmiIssueDrivingLicense.Enabled = false;
                editToolStripMenuItem.Enabled = false;
                scheduleTestsToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                cancelApplicationToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;
                return;
            }

            int PassedTest = (int)dgvDLAList.CurrentRow.Cells[5].Value;
            tsmiSechduleVisionTest.Enabled = PassedTest == 0;
            tsmiSechduleWrittenTest.Enabled = PassedTest == 1;
            tsmiSechduleStreetTest.Enabled = PassedTest == 2;
            tsmiIssueDrivingLicense.Enabled = PassedTest == 3;

            if(PassedTest == 3)
                scheduleTestsToolStripMenuItem.Enabled = false;
            else
                scheduleTestsToolStripMenuItem.Enabled = true;

            if (PassedTest == 3 
               )
            {
                tsmiIssueDrivingLicense.Enabled = false;
                editToolStripMenuItem.Enabled = false;
                scheduleTestsToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                cancelApplicationToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = true;
            }
            else
            {
                editToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
                cancelApplicationToolStripMenuItem.Enabled = true;
                showLicenseToolStripMenuItem.Enabled = false;
            }




        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Clear();
            txtFilter.Focus();
            frmManageDLA_Load(null, null);
            if (cbFilterBy.SelectedIndex == 0)
            {
                txtFilter.Visible = false;
            }
            else
            {
                txtFilter.Visible = true;
            }
        }

        private void frmManageDLA_Load(object sender, EventArgs e)
        {
            _dtAllLocalDrivingLicenseApplications = clsDLA.GetAllLocalDrivingLicenseApplications();
            dgvDLAList.DataSource = _dtAllLocalDrivingLicenseApplications;
            lblRecords.Text = _dtAllLocalDrivingLicenseApplications.Rows.Count.ToString();
            if (dgvDLAList.Rows.Count > 0)
            {

                dgvDLAList.Columns[0].HeaderText = "L.D.L.AppID";
                dgvDLAList.Columns[0].Width = 120;

                dgvDLAList.Columns[1].HeaderText = "Driving Class";
                dgvDLAList.Columns[1].Width = 300;

                dgvDLAList.Columns[2].HeaderText = "National No.";
                dgvDLAList.Columns[2].Width = 150;

                dgvDLAList.Columns[3].HeaderText = "Full Name";
                dgvDLAList.Columns[3].Width = 350;

                dgvDLAList.Columns[4].HeaderText = "Application Date";
                dgvDLAList.Columns[4].Width = 170;

                dgvDLAList.Columns[5].HeaderText = "Passed Tests";
                dgvDLAList.Columns[5].Width = 150;
            }

            cbFilterBy.SelectedIndex = 0;
        }

        private void btnAddLocalDLA_Click(object sender, EventArgs e)
        {
            frmAddEditDLA frm = new frmAddEditDLA();
            frm.ShowDialog();
            frmManageDLA_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.SelectedIndex ==1)
            {
                if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {

                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;


                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecords.Text = dgvDLAList.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                //in this case we deal with integer not string.
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
            else
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilter.Text.Trim());

            lblRecords.Text = dgvDLAList.Rows.Count.ToString();
        }

        private void tsmiSechduleVisionTest_Click(object sender, EventArgs e)
        {
            frmScheduleTest frm = new frmScheduleTest(_GetDLA_ID(), clsTestTypes.enTestType.VisionTest);
            frm.ShowDialog();
            frmManageDLA_Load(null,null);
        }

        private void tsmiSechduleWrittenTest_Click(object sender, EventArgs e)
        {
            frmScheduleTest frm = new frmScheduleTest(_GetDLA_ID(), clsTestTypes.enTestType.WrittenTest);
            frm.ShowDialog();
            frmManageDLA_Load(null, null);
        }

        private void tsmiSechduleStreetTest_Click(object sender, EventArgs e)
        {
            frmScheduleTest frm = new frmScheduleTest(_GetDLA_ID(), clsTestTypes.enTestType.StreetTest);
            frm.ShowDialog();
            frmManageDLA_Load(null, null);

        }

        private void dgvDLAList_SelectionChanged(object sender, EventArgs e)
        {
            EnableAndDisableControls();

        }

        private void tsmiIssueDrivingLicense_Click(object sender, EventArgs e)
        {
            frmIssueDLForFirstTime frm = new frmIssueDLForFirstTime(_GetDLA_ID());
            frm.ShowDialog();
            frmManageDLA_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(frmShowLicenseInfo frm = new frmShowLicenseInfo(_GetDLA_ID()))
            {
                frm.ShowDialog();
            }
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsUtil.ShowConfirm("Are you sure do you want to delete this driving local application?") == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = _GetDLA_ID();

            clsDLA DLA = clsDLA.Find(LocalDrivingLicenseApplicationID);

            if(DLA != null)
            {
                if(DLA.Cancel())
                {
                    MessageBox.Show($"Cancelled The Local Driving Application With ID {_GetDLA_ID()} :D");
                    frmManageDLA_Load(null, null);
                }
                else
                {
                    MessageBox.Show($"Didnt Cancel The Local Driving Application With ID {_GetDLA_ID()} ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
 
        }

        private void showLicenseLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsDriver driver = clsDriver.Find(
                    clsApplication.Find(clsDLA.Find(_GetDLA_ID()).ApplicationID).ApplicantPersonID);

            if (driver == null)
            {
                MessageBox.Show($"There is not any license history of this ID {_GetDLA_ID()} ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (frmDriverLicenseHistory frm = new frmDriverLicenseHistory(driver.DriverID)
               )
            {
                frm.ShowDialog();
            }
        }

       
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsDLA.isDLAExists(_GetDLA_ID()))
            {
                if(clsUtil.ShowConfirm("Are you sure do you want to delete this driving local application?") == DialogResult.Yes)
                {
                    clsDLA DLA = clsDLA.Find((int)dgvDLAList.CurrentRow.Cells[0].Value);
                    if(DLA != null)
                    {
                      
                        if(DLA.Delete())
                            clsUtil.ShowSuccessful("Deleted Successfully!");
                        frmManageDLA_Load(null, null);

                    }
                    else
                    {
                        clsUtil.ShowError("This Driving Local Application didnt delete due to there is data linked with it");
                    }
                }
            }

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo((int)dgvDLAList.CurrentRow.Cells[0].Value))
            {
                frm.ShowDialog();
            }
            frmManageDLA_Load(null,null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditDLA frm = new frmAddEditDLA((int)dgvDLAList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmManageDLA_Load(null,null);

        }

        private void cmsOperations_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = _GetDLA_ID();
            clsDLA DLA = clsDLA.Find(LocalDrivingLicenseApplicationID);
            int TotalPassedTests = (int)dgvDLAList.CurrentRow.Cells[5].Value;
            bool LicenseExists = DLA.IsLicenseIssued();
            tsmiIssueDrivingLicense.Enabled = (TotalPassedTests == 3) && !LicenseExists;
            editToolStripMenuItem.Enabled = !LicenseExists && (DLA.ApplicationStatus == clsApplication.enApplicationStatus.New);
            

            deleteToolStripMenuItem.Enabled = (DLA.ApplicationStatus == clsApplication.enApplicationStatus.New);

            bool PassedVisionTest = DLA.DoesPassTestType(clsTestTypes.enTestType.VisionTest);
            bool PassedWrittenTest = DLA.DoesPassTestType(clsTestTypes.enTestType.WrittenTest);
            bool PassedStreetTest = DLA.DoesPassTestType(clsTestTypes.enTestType.StreetTest);
            scheduleTestsToolStripMenuItem.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest);
            if (scheduleTestsToolStripMenuItem.Enabled)
            {
                tsmiSechduleVisionTest.Enabled = !PassedVisionTest;
                tsmiSechduleWrittenTest.Enabled = !PassedWrittenTest;
                tsmiSechduleStreetTest.Enabled = !PassedStreetTest;
            }
        }

        private void scheduleTestsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
