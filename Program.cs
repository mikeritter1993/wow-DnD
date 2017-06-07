using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace sql_and_interactive_window
{
    static class Program
    {
        public const int NETWORK_MESSAGE_SIZE = 1000; // in bytes
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
       

        static public DataRow loggedInUser;  //Columns = UserName, Email, Password

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
