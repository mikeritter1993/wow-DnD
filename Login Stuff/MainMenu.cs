/*****************************************************************************************************
Class: MainMenu
Description: The start point for the application, user chooses to login or register a account
Status: complete
******************************************************************************************************/

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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginPage childPage = new LoginPage();
            childPage.ShowDialog();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register_Page childPage = new Register_Page();
            childPage.ShowDialog();
        }
    }
}
