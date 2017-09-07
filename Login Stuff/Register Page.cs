/*******************************************************************************************************
Class: RegisterPage (will fix name later)
Description: gets a email, password and user name to make a users login information, checks info against existing users for obvs reasons.
Status: complete maybe?
TO DO: comments then done
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
using System.Data.SqlClient;

namespace DnD
{
    public partial class Register_Page : Form
    {
        public Register_Page()
        {
            InitializeComponent();
            this.AcceptButton = btnRegister;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            String userName, email, password;
            userName = txtUserName.Text;
            email = txtEmail.Text;
            password = txtPassword.Text;

            SqlHelper sql = new SqlHelper();
            sql.Command = "SELECT * FROM [LoginData].[dbo].[LoginInfo] WHERE Email = \'" + email + "\';";
            DataTable data = sql.ExecuteQuery();
            if (data.Rows.Count > 0)
            {
                MessageBox.Show("That email is already registered to another account. If you forgot your password click here.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                sql.Command = "INSERT INTO [LoginData].[dbo].[LoginInfo] (UserName,Email,Password) VALUES(\'" + userName + "\',\'" + email + "\',\'" + password + "\');";
                if (!sql.ExecuteNonQuery())
                {
                    //command had some error no rows were affected
                    //handle error
                }
                else
                {
                    MessageBox.Show("You are now registered in the system.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }   
            } 
        }

        
    }
}
