using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class _Default : Page
    {
        string Output;
        protected void Page_Load(object sender, EventArgs e)
        {
            openDBConection();
            getData(1);
            closeConnection();
            renderTextField();
        }

        MySqlConnection mysqlconnection = new MySqlConnection();

        string ConnectionString = $"server=127.0.0.1;port=3306;user id=root; password=; database=testdb; SslMode=none";

        public void renderTextField()
        {
            TextBox1.Text = Output;
        }

        public void getData(int id)
        {
            string SQLString = "SELECT * FROM `testtable` WHERE `testtable`.`id` = @id;";
            MySqlCommand command = new MySqlCommand(SQLString, mysqlconnection);
            command.Parameters.AddWithValue("@id", id);
            MySqlDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                Output = rdr.GetValue(1).ToString();
            }
            rdr.Close();
        }

        public bool openDBConection()
        {
            try
            {
                mysqlconnection.ConnectionString = ConnectionString;
                mysqlconnection.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private bool executeSQLState(string sQLString)
        {
            bool retVal = false;
            MySqlCommand command = new MySqlCommand(sQLString, mysqlconnection);
            int anzahl = -1; // to check how many rows are effected
            try
            {
                anzahl = command.ExecuteNonQuery();
                retVal = true;
            }
            catch (Exception)
            {
                mysqlconnection.Close();
                retVal = false;
                throw;
            }
            return retVal;
        }

        public void closeConnection()
        {
            mysqlconnection.Dispose();
            mysqlconnection.Close();
        }
    }
}