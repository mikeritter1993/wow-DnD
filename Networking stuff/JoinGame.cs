/*********************************************************************
Class: JoinGame
Description: going to connect a user to a server
Status: complete 
TO DO: nothing to think of atm.
**********************************************************************/

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
    public partial class JoinGame : Form
    {
        public JoinGame()
        {
            InitializeComponent();
            this.AcceptButton = connectBtn;
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            Program.User.Client.IpAddress = ipAddressTxt.Text;
            Program.User.Client.PortNum = 7777;

            if (Program.User.Client.TryConnection())
            {
                Program.User.Client.StartListening();
                CharSheet page = new CharSheet();
                page.ShowDialog();
                this.Close();
            }
            else
            {
                //gets handled inside connectionhelper
            }
        }
    }
}
