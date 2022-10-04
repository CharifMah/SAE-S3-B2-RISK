using MySql.Data.MySqlClient;

namespace DBStorage
{
    public class Connection
    {
        private string connString;
        private MySqlConnection conn;

        /// <summary>
        /// Connexion à la base de donnée
        /// </summary>
        /// <author>Brian VERCHERE</author>
        private void Connect()
        {
            connString = "server=localhost;userid=root;password=admin;";
            conn = new MySqlConnection(connString);

            conn.Open();
        }

        /// <summary>
        /// Créer la base de donnée du jeu
        /// </summary>
        /// <author>Brian VERCHERE</author>
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

        /// <summary>
        /// Créer la table gérant les comptes utilisateurs
        /// </summary>
        /// <author>Brian VERCHERE</author>
        public void CreateUserTable()
        {
            Connect();

            try
            {
                MySqlCommand cmd = new MySqlCommand("use risk;",conn);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("create table if not exists Users (Pseudo varchar(50), password varchar(50));", conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Table Users created successfully");
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