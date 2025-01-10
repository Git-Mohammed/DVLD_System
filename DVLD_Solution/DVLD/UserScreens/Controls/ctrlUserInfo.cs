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
    public partial class ctrlUserInfo : UserControl
    {
       private clsUser User;
       private int _UserID= -1;

        public int UserID
        {
            get { return _UserID; }
        }
        public ctrlUserInfo()
        {
            InitializeComponent();
        }
        public void LoadData(int UserID)
        {
            _UserID = UserID;
            User = clsUser.Find(UserID);
            if(User == null)
            {
                _ResetPersonInfo();
                clsUtil.ShowError("No User with UserID = " + UserID.ToString());
                return;
            }

            _FillUserInfo();
        }

        private void _FillUserInfo()
        {
            ctrlPersonDetails2._LoadDataInfo(User.PersonID);
            lblIsActive.Text = (User.isActive) ? "Yes" : "No";
            lblUserID.Text = User.UserID.ToString();
            lblUsername.Text = User.Username.ToString();
        }
        private void _ResetPersonInfo()
        {
            ctrlPersonDetails2.LoadDefualtData();
        }
        private void ctrlUserInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
