using DVLD.Applications.ApplicationTypes;
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
    public partial class frmManageApplicationTypes : Form
    {

        private DataTable _dtAllApplicationTypes;
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }

       
        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtAllApplicationTypes = clsApplicationType.GetAllApllicationTypes();
            dgvApplictionTypesList.DataSource = _dtAllApplicationTypes;
            lblRecords.Text = dgvApplictionTypesList.Rows.Count.ToString();

            if(dgvApplictionTypesList.Rows.Count > 0)
            {
                dgvApplictionTypesList.Columns[0].HeaderText = "ID";
                dgvApplictionTypesList.Columns[0].Width = 110;

                dgvApplictionTypesList.Columns[1].HeaderText = "Title";
                dgvApplictionTypesList.Columns[1].Width = 400;

                dgvApplictionTypesList.Columns[2].HeaderText = "Fees";
                dgvApplictionTypesList.Columns[2].Width = 100;
                dgvApplictionTypesList.Columns[2].DefaultCellStyle.Format = "F4";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationType frm = new frmUpdateApplicationType((int)dgvApplictionTypesList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmManageApplicationTypes_Load(null, null);
        }
    }
}
