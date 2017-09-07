/*********************************************************************
Class: Player
Description: Holds all data pulled from sql server, main connection point for multiplayer.
Status: complete
**********************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD
{
    class Player
    {
        private String userName;
        private String charName;
        private DataRow loggedInUser;  //Columns = UserName, Email, Password. All the users data
        private DataTable charSheets;
        private MultiplayerConnectionHelper client;
        private MultiplayerHostHelper host;

        public Player()
        {
            userName = null;
            charName = null;
            loggedInUser = null;
            charSheets = null;
            client = null;
            host = null;
        }

        public DataRow LoggedInUser
        {
            get
            {
                return loggedInUser;
            }
        }

        public DataTable CharSheets
        {
            get
            {
                return charSheets;
            }
        }

        public MultiplayerConnectionHelper Client
        {
            get
            {
                return client;
            }
        }

        public MultiplayerHostHelper Host
        {
            get
            {
                return host;
            }
        }

        public string CharName
        {
            get
            {
                return charName;
            }

            set
            {
                charName = value;
            }
        }

        public Player(DataRow loginInfo, DataTable chars)
        {
            client = new MultiplayerConnectionHelper();
            host = new MultiplayerHostHelper();
            loggedInUser = loginInfo;
            charSheets = chars;
            userName = (String)loggedInUser["UserName"];
            client.UserName = this.userName;
            if (charSheets.Rows.Count > 0)
            {
                charName = (String)charSheets.Rows[0]["CharName"];
            }
        }

        public bool ConnectToServer()
        {
            return client.TryConnection();
        }

        public void HostServer()
        {
            host.StartSever();
        }
    }
}
