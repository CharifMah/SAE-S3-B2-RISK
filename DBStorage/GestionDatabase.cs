using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace DBStorage
{
    public class GestionDatabase
    {
        private string connString;
        private MySqlConnection conn;

        /// <summary>
        /// Connexion à la base de donnée
        /// </summary>
        /// <author>Brian VERCHERE</author>
        private void Connect()
        {
            connString = "server=localhost;userid=root;password=;";
            conn = new MySqlConnection(connString);

            conn.Open();
        }

        /// <summary>
        /// Créer la base de donnée du jeu si il elle n'existe pas
        /// </summary>
        /// <author>Brian VERCHERE,Charif Mahmoud</author>
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
        /// Créer la table gérant les comptes utilisateurs si il elle n'existe pas
        /// </summary>
        /// <author>Brian VERCHERE,Charif Mahmoud</author>
        public void CreateUserTable()
        {
            Connect();

            try
            {
                MySqlCommand cmd = new MySqlCommand("use risk;", conn);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("create table if not exists Users (Pseudo varchar(50) PRIMARY KEY);", conn);
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

        /// <summary>
        /// Selectionne un Users dans la base de donnee par son login
        /// </summary>
        /// <param name="login">login du Users</param>
        /// <author>Romain BARABANT</author>
        public string SelectUser(string login)
        {
            Connect();
            string res = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("use risk;", conn);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("Select * from users where Pseudo = '" + login + "';", conn);
                cmd.ExecuteNonQuery();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        res = (String.Format("{0}", reader["Pseudo"]));
                    }
                }
                Console.WriteLine("Users " + login + " selectionne");
            }
            catch (Exception x)
            {
                Console.WriteLine("An error occured: {0}", x.Message);
            }
            finally
            {
                conn.Close();
            }
            return res;
        }

        /// <summary>
        /// creer un Users dans la base de donnee par son login
        /// </summary>
        /// <param name="login">login du Users</param>
        /// <author>Romain BARABANT</author>
        public void CreateUser(string login)
        {
            Connect();

            try
            {
                MySqlCommand cmd = new MySqlCommand("use risk;", conn);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("Insert into users values ('" + login + "');", conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Users " + login + " creer");
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