using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.UserScreens
{
    public partial class frmUserDetails : Form
    {
        private int UserId;
        public frmUserDetails(int UserID)
        {
            InitializeComponent();
            this.UserId = UserID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUserDetails_Load(object sender, EventArgs e)
        {
            ctrlUserInfo2.LoadData(UserId);
        }
    }
}
