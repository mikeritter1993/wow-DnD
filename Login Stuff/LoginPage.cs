/******************************************************************************************************
Class: LoginPage
Description: takes input of a username and a password, checks if input givin has a existing account with the server and logs them in if they do
Status: complete maybe?
TO DO: password reseting, username retriving
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
            this.AcceptButton = btnLogin;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlHelper sql = new SqlHelper();
            String userName = txtUserName.Text;
            String password = txtPassword.Text;


            sql.Command = "SELECT * FROM[LoginData].[dbo].[LoginInfo] WHERE(UserName = \'"+ userName + "\') AND Password = \'" + password + "\';";
            DataTable table = sql.ExecuteQuery();

            if (table != null)
            {
                MessageBox.Show("You Logged in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                sql.Command = "SELECT * FROM[LoginData].[dbo].[CharSheets] WHERE(UserName = \'" + userName + "\');";
                DataTable chars = sql.ExecuteQuery();

                String hold = (String)table.Rows[0]["UserName"];
                hold = hold.Trim();
                table.Rows[0]["UserName"] = hold;

                if (chars != null)          //trim white space from data
                {
                    for (int i = 0; i < chars.Rows.Count; i++)
                    {
                        hold = (String)chars.Rows[i]["UserName"];
                        hold = hold.Trim();
                        chars.Rows[i]["UserName"] = hold;

                        hold = (String)chars.Rows[i]["CharName"];
                        hold = hold.Trim();
                        chars.Rows[i]["CharName"] = hold;

                        hold = (String)chars.Rows[i]["Stats"];
                        hold = hold.Trim();
                        chars.Rows[i]["Stats"] = hold;

                        hold = (String)chars.Rows[i]["Skills"];
                        hold = hold.Trim();
                        chars.Rows[i]["Skills"] = hold;

                        hold = (String)chars.Rows[i]["SavingThrows"];
                        hold = hold.Trim();
                        chars.Rows[i]["SavingThrows"] = hold;

                        hold = (String)chars.Rows[i]["Misc"];
                        hold = hold.Trim();
                        chars.Rows[i]["Misc"] = hold;
                    }
                }
                Player user = new Player(table.Rows[0], chars);
                Program.User = user;
                this.Close();
            }
            else
            {
                MessageBox.Show("Login unsuccessful check your information. If you forgot your password click here.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
