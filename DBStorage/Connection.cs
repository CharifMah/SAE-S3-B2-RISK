using MySql.Data.MySqlClient;

namespace DBStorage
{
    public class Connection
    {
        private string connString;
        private MySqlConnection conn;

        public void Connect()
        {
            connString = "server=localhost;userid=root;password=admin;";
            conn = new MySqlConnection(connString);

            conn.Open();
        }

        public void CreateDatabase()
        {
            Connect();

            try
            {
                MySqlCommand cmd = new MySqlCommand("create database if not exists Risk", conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Database Risk created successfully");
            }
            catch (Exception x)
            {
                Console.WriteLine("An error occured: {0}", x.Message);
            }
            finally
            {
                conn.Close();
            }
        }


    }
}