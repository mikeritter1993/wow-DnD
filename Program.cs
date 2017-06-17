using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace DnD
{
    static class Program
    {
        public const int NETWORK_MESSAGE_SIZE = 1500; // the size of a network message in bytes
        public const char NETWORK_MESSAGE_DELIMITER = '$';  //the char at the end of every network message
        /// <summary>
        /// The main entry point for the application.
        /// </summary>


        static public DataRow loggedInUser;  //Columns = UserName, Email, Password. All the users data

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainMenu());

           // if (loggedInUser != null)
           // {
                Application.Run(new CharSheet());
            //}
            
        }
    }
}
