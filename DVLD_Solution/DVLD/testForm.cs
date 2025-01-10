using DVLD.Applications.DLA;
using DVLD.Applications.TestTypes;
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
    public partial class testForm : Form
    {
        public testForm()
        {
            InitializeComponent();
        }

        private void testForm_Load(object sender, EventArgs e)
        {
        }

        private void ChangeRecordsCount(string count)
        {
        }

        private void ctrLicensesListWithAddAndFilter1_Load(object sender, EventArgs e)
        {

        }

        private void ctrLicensesListWithAddAndFilter1_OnFilteringRows(int obj)
        {
            if(obj > 0)
            {
            }
            else
            {
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            MessageBox.Show("You leave");
        }

        private void ctrlUserListWithFilter1_CountOfRows(int obj)
        {
        }

        private void ctrlUserListWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frm = new frmManageTestTypes();
            frm.ShowDialog();
        }
    }
}
