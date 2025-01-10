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
    public partial class ctrlFindPersonWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;

            if(handler != null)
                handler(PersonID);
        }

        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }

            set
            {
                _ShowAddPerson=value;
                btnAddPerson.Enabled = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        { 
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled=value;
                gbFilter.Enabled = _FilterEnabled;
            }
        
        }

        private int _PersonID = -1;
        public int PersonID
        {
            get { return ctrlPersonDetails2.PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonDetails2.SelectedPersonInfo; }
        }

        public ctrlFindPersonWithFilter()
        {
            InitializeComponent();
        }

        private void txtFind_KeyPress(object sender, KeyPressEventArgs e)
        {

            if(e.KeyChar == (char)13)
            {
                btnFindPerson.PerformClick();
            }

            if (cbFindBy.SelectedIndex == 1)
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }
        public void LoadData(int PersonID)
        {
            cbFindBy.SelectedIndex = 1;
            if (PersonID == -1)
            {
                gbFilter.Enabled = true;
                ctrlPersonDetails1.LoadDefualtData();
                return;
            }
            txtFind.Text = PersonID.ToString();
            FindNow();
        }

        private void FindNow()
        {
            switch(cbFindBy.Text)
            {
                case "Person ID":
                    ctrlPersonDetails2._LoadDataInfo(int.Parse(txtFind.Text));
                    break;
                case "National No":
                    ctrlPersonDetails1._LoadDataInfo(txtFind.Text);
                    break;
                default:
                    break;
            }

            if(OnPersonSelected != null && FilterEnabled)
               OnPersonSelected(ctrlPersonDetails2.PersonID);
        }
        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not validate");
                return;
            }

            FindNow();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.LoadData += LoadData;
            frm.ShowDialog();
        }
        public void FilterFoucs()
        {
            txtFind.Focus();
        }
        private void ctrlFindPersonWithFilter_Load(object sender, EventArgs e)
        {
            cbFindBy.SelectedIndex = 0;
            txtFind.Focus();
        }
        private void txtFind_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFind.Text))
            {
                errorProvider1.SetError(txtFind, "This Field cant be empty!!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtFind, null);
               // e.Cancel = false;
            }
        }
    }
}
