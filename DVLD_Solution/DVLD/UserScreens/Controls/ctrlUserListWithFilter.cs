using DVLD.UserScreens;
using DVLD_BusinessLayer;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DVLD.UserControls
{
    public partial class ctrlUserListWithFilter : UserControl
    {


        public ctrlUserListWithFilter()
        {
            InitializeComponent();
        }
        public event Action<int> CountOfRows;

        protected virtual void SetCountOfRows(int countOfRows)
        {
            CountOfRows?.Invoke(countOfRows);
        }
        private string BuildFilterExpression()
        {
            // Handle the special case for the isActive field
            if (cbFilterBy.SelectedIndex == 4)
            {
                if (cbFilterActive.SelectedItem != null && cbFilterActive.SelectedIndex != 0)
                {
                    return cbFilterActive.SelectedItem.ToString() == "Yes" ? "isActive = true" : "isActive = false";
                }
                return string.Empty;
            }

            // Handle general text filtering
            if (!string.IsNullOrEmpty(txtFilter.Text))
            {
                switch (cbFilterBy.SelectedIndex)
                {
                    case 1:
                        return $"User_ID = {txtFilter.Text}";
                    case 2:
                        return $"Person_ID = {txtFilter.Text}";
                    default:
                        return $"{cbFilterBy.SelectedItem} LIKE '{txtFilter.Text}%'";
                }
            }
            return string.Empty;
        }

        private DataView GetFilteredData()
        {
            DataView dataView = clsUser.GetAllUsers().DefaultView;
            string filterExpression = BuildFilterExpression();
            if (!string.IsNullOrEmpty(filterExpression))
            {
                dataView.RowFilter = filterExpression;
            }
            
            SetCountOfRows(dataView.Count);
            return dataView;
        }

        private void PopulateFilterByComboBox()
        {
            DataTable usersTable = clsUser.GetAllUsers();
            foreach (DataColumn column in usersTable.Columns)
            {
                cbFilterBy.Items.Add(column.ColumnName);
            }
            cbFilterBy.SelectedIndex = 0;
        }

        private void RefreshUserList()
        {
            dgvUsersList.DataSource = clsUser.GetAllUsers().DefaultView;
            dgvUsersList.Columns["Full_Name"].Width = 590;
            SetCountOfRows(dgvUsersList.RowCount);

        }

        private void HandletxtFilterVisibility()
        {
            if(cbFilterBy.SelectedIndex == 0)
            {
                txtFilter.Visible = false; 
                return;
            }

            HandleActiveFilterVisibility();

        }
        private void HandleActiveFilterVisibility()
        {
            cbFilterActive.SelectedIndex = 0;
            cbFilterActive.Visible = cbFilterBy.SelectedItem.ToString() == "isActive";
            txtFilter.Visible = !cbFilterActive.Visible;
        }

        // Event Handlers
        private void ctrlUserListWithFilter_Load(object sender, EventArgs e)
        {
            PopulateFilterByComboBox();
            RefreshUserList();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Clear();
            txtFilter.Focus();
            HandletxtFilterVisibility();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 1 || cbFilterBy.SelectedIndex == 2)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // Prevent non-numeric input
                }
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            dgvUsersList.DataSource = GetFilteredData();
        }

        private void cbFilterActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvUsersList.DataSource = GetFilteredData();
        }

        private void btnAddPeson_Click(object sender, EventArgs e)
        {
            frmAddEditUserInfo frm = new frmAddEditUserInfo(-1);
            frm.ShowDialog();
            RefreshUserList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUserInfo frm = new frmAddEditUserInfo((int)dgvUsersList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            RefreshUserList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserDetails frm = new frmUserDetails((int)dgvUsersList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUserInfo frm = new frmAddEditUserInfo(-1);
            frm.ShowDialog();
            RefreshUserList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword frm = new ChangePassword((int)dgvUsersList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsersList.CurrentRow.Cells[0].Value;
            DialogResult dr = MessageBox.Show($"Are you sure you want to delete this userid = {UserID}", "Confirm", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dr == DialogResult.Yes) 
            {
                if (clsUser.Delete(UserID))
                {
                    MessageBox.Show("Deleted Successfully:D");
                    RefreshUserList();
                }
                else
                {
                    MessageBox.Show("Didnt delete due to data connected to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("this feature is not ready!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("this feature is not ready!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
