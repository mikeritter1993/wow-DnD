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

namespace sql_and_interactive_window
{
    public partial class Register_Page : Form
    {
        public Register_Page()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            String userName, email, password;
            userName = txtUserName.Text;
            email = txtEmail.Text;
            password = txtPassword.Text;

            SqlHelper sql = new SqlHelper();
            sql.Command = "SELECT * FROM [LoginData].[dbo].[LoginInfo] WHERE Email = \'" + email + "\';";
            DataTable data = sql.ExecuteCommand();
            if (data != null)
            {
                MessageBox.Show("That email is already registered to another account. If you forgot your password click here.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                sql.Command = "INSERT INTO [LoginData].[dbo].[LoginInfo] (UserName,Email,Password) VALUES(\'" + userName + "\',\'" + email + "\',\'" + password + "\');";
                if (!sql.InsertData())
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
