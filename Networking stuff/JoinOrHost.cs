/*********************************************************************
Class: JoinOrHost
Description: user selects if they want to join or host a game
Status: complete
TO DO: nothing i can think of atm
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
    public partial class JoinOrHost : Form
    {
        public JoinOrHost()
        {
            InitializeComponent();
        }

        private void joinGameBtn_Click(object sender, EventArgs e)
        {
            JoinGame childPage = new JoinGame();
            childPage.ShowDialog();
        }

        private void hostGameBtn_Click(object sender, EventArgs e)
        {
            Program.User.Client.IpAddress = "127.0.0.1";
            Program.User.Client.PortNum = 7777;
            Program.User.HostServer();
            Program.User.Client.TryConnection();
            Program.User.Client.StartListening();
            
            CharSheet page = new CharSheet();
            page.ShowDialog();
            this.Close();
        }
    }
}
