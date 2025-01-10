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

namespace DVLD
{
    public partial class frmLogin : Form
    {
        private clsLogger _userLog = new clsLogger(clsGlobal.HandleLogginRegister);
        public frmLogin()
        {
            InitializeComponent();
        }
        private string fileName = "Login.txt";

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool IsFileEmpty(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return true;
            }

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                return (streamReader.ReadLine() == null);
            }
        }

        private bool IsLoggedInBefore()
        {

            if (IsFileEmpty(fileName))
            {
                return false;
            }

            try
            {
                using (StreamReader streamReader = new StreamReader(fileName))
                {
                    string[] arrLine = streamReader.ReadLine().Split('#');
                    txtUsername.Text = arrLine[0];
                    txtPassword.Text = arrLine[1];
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading login file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void ReadLogin()
        {
            string Username = "", Password = "";

            if(clsGlobal.GetStoredCredential(ref  Username, ref Password))
            {
                txtUsername.Text = Username;
                txtPassword.Text = Password;
                chkRememberme.Checked = true;
            }
            else
            {
                chkRememberme.Checked = false;
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            ReadLogin();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser user = clsUser.Find(txtUsername.Text.Trim(), txtPassword.Text.Trim());
            if (user != null)
            {
                if (!user.isActive)
                {
                    txtUsername.Focus();
                    clsUtil.ShowError("Your account is not active, Contact Admin.");
                    return;
                }

                if (chkRememberme.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txtUsername.Text.Trim(),txtPassword.Text.Trim());
                }
                else
                {
                    clsGlobal.RememberUsernameAndPassword("", "");

                }

                string LoginLog = $"[ User: {user.Username}  #//# Password : {user.Password} #//# DateTime: {DateTime.Now.ToString()}";
                if(user != null)
                    _userLog.Log(LoginLog);
                clsGlobal.CurrentUser = user;
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();


            }
            else
            {
                txtUsername.Focus();
                clsUtil.ShowError("Useranme/Password is invalid try again!");
            }

        }
        
    }
}

