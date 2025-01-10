using DVLD.GlobalClasses;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.PeopleScreens
{
    public partial class frmManagePeople : Form
    {

        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();

        private static DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable
            (false,
            "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName",
            "LastName", "GenderCaption", "DateOfBirth", "CountryName",
            "Phone", "Email"
            );
        public frmManagePeople()
        {
            InitializeComponent();
        }




        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void _RefershPeopleList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

            dgvPeopleList.DataSource = _dtPeople;
            lblRecords.Text = dgvPeopleList.Rows.Count.ToString();

        }
        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            dgvPeopleList.DataSource = _dtPeople;
            cbFilterBy.SelectedIndex = 0;
            lblRecords.Text = dgvPeopleList.RowCount.ToString();
            if(dgvPeopleList.Rows.Count > 0 )
            {
                dgvPeopleList.Columns[0].HeaderText = "Person ID";
                dgvPeopleList.Columns[0].Width = 90;

                dgvPeopleList.Columns[1].HeaderText = "National No.";
                dgvPeopleList.Columns[1].Width = 120;

                dgvPeopleList.Columns[2].HeaderText = "First Name";
                dgvPeopleList.Columns[2].Width = 100;

                dgvPeopleList.Columns[3].HeaderText = "Second Name";
                dgvPeopleList.Columns[3].Width = 100;

                dgvPeopleList.Columns[4].HeaderText = "Third Name";
                dgvPeopleList.Columns[4].Width = 100;

                dgvPeopleList.Columns[5].HeaderText = "Last Name";
                dgvPeopleList.Columns[5].Width = 120;

                dgvPeopleList.Columns[6].HeaderText = "Gender";
                dgvPeopleList.Columns[6].Width = 60;

                dgvPeopleList.Columns[7].HeaderText = "Date Of Birth";
                dgvPeopleList.Columns[7].Width = 120;

                dgvPeopleList.Columns[8].HeaderText = "Nationality";
                dgvPeopleList.Columns[8].Width = 120;

                dgvPeopleList.Columns[9].HeaderText = "Phone";
                dgvPeopleList.Columns[9].Width = 110;

                dgvPeopleList.Columns[10].HeaderText = "Email";
                dgvPeopleList.Columns[10].Width = 170;
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeopleList.CurrentRow.Cells[0].Value;
            Form frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPersonInfo();
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeopleList.CurrentRow.Cells[0].Value;
            Form frm = new frmAddEditPersonInfo(PersonID);
            frm.ShowDialog();
            _RefershPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeopleList.CurrentRow.Cells[0].Value;

            if (clsUtil.ShowConfirm("Are you sure you want to delete Person ID = " + PersonID.ToString()) == DialogResult.Yes)
            {
                string ImagePath = clsPerson.Find(PersonID).ImagePath;
                if (clsPerson.Delete(PersonID) )
                {
                    if(!string.IsNullOrEmpty(ImagePath))
                    {
                        if (File.Exists(ImagePath))
                            File.Delete(ImagePath);
                    }
                    clsUtil.ShowSuccessful("Person Deleted Successfully. ");
                    _RefershPeopleList();
                }
                else
                    clsUtil.ShowError("Person was not deleted because it has data linked to it.");
            }
        }

        private void btnAddPeson_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPersonInfo();
            frm.ShowDialog();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Date Of Birth":
                    FilterColumn = "DateOfBirth";
                    break;

                case "Gender":
                    FilterColumn = "GenderCaption";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if(txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecords.Text = dgvPeopleList.RowCount.ToString();
                return;
            }

            if(FilterColumn == "PersonID")
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}",FilterColumn,txtFilter.Text.Trim());
          
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilter.Text.Trim());

            lblRecords.Text = dgvPeopleList.RowCount.ToString();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsUtil.ShowThisFeatureIsNotReady();
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsUtil.ShowThisFeatureIsNotReady();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = !(cbFilterBy.Text == "None");
            if(txtFilter.Visible )
            {
                txtFilter.Clear();
                txtFilter.Focus();
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }
    }
}
