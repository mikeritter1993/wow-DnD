/*******************************************************************************************************
Class: SqlHelper
Description: designed to make doing sql commands easier for us, minaly used for making new accoutns and loging into the server for users at the moment
Status: complete maybe?
TO DO: variable functionality.
*******************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DnD
{
    class SqlHelper
    {
        private String connection;
        private String command;

        public SqlHelper()
        {
            connection = "Data Source=MIKE-PC;Initial Catalog=LoginData;User ID=sa;Password=asdf1234";
            //connection = "Data Source=MIKE-PC;Initial Catalog=Testing;Integrated Security=True";
        }

        public SqlHelper(String command)
        {
            this.command = command;
        }

        public bool ExecuteNonQuery()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    con.Open();
                    using (SqlCommand query = con.CreateCommand())
                    {
                        query.CommandText = command;
                        int rowsAffected = query.ExecuteNonQuery();
                        con.Close();
                        if (rowsAffected > 0)
                            return true;
                        return false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public DataTable ExecuteQuery()
        {
            DataTable data;
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    con.Open();
                    using (SqlCommand query = con.CreateCommand())
                    {
                        query.CommandText = command;
                        SqlDataReader reader = query.ExecuteReader();
                     
                        data = new DataTable();
                        data.Load(reader);
                     

                        reader.Close();
                        con.Close();
                        return data;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public string Connection
        {
            get
            {
                return connection;
            }

            set
            {
                connection = value;
            }
        }

        public string Command
        {
            get
            {
                return command;
            }

            set
            {
                command = value;
            }
        }
    }
}
