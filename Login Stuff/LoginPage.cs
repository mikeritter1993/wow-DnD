/******************************************************************************************************
Class: LoginPage
Description: takes input of a username or email and a password, checks if input givin has a existing account with the server and logs them in if they do
Status: complete maybe?
TO DO: wrote it a long time ago need to make sure this is what we want.
*******************************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DnD
{
    public partial class LoginPage : Form
    {
        
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlHelper sql = new SqlHelper();
            String userNameOrEmail = txtUserName.Text;
            String password = txtPassword.Text;


            sql.Command = "SELECT * FROM[LoginData].[dbo].[LoginInfo] WHERE(UserName = \'"+ userNameOrEmail + "\' OR Email = \'" + userNameOrEmail + "\') AND Password = \'" + password + "\';";
            DataTable table = sql.ExecuteCommand();

            if (table != null)
            {
                MessageBox.Show("You Logged in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Login unsuccessful check your information. If you forgot your password click here.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
