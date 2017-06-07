using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace sql_and_interactive_window
{
    class SqlHelper
    {
        private String connection;
        private String command;

        public SqlHelper()
        {
            connection = "Data Source=MIKE-PC;Initial Catalog=Testing;Integrated Security=True";
        }

        public SqlHelper(String command)
        {
            this.command = command;
        }

        public bool InsertData()
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
        public DataTable ExecuteCommand()
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
                        if (reader.HasRows)
                        {
                            data = new DataTable();
                            data.Load(reader);
                        }
                        else
                        {
                            data = null;
                        }

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
