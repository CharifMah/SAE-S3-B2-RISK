using MySql.Data.MySqlClient;

namespace DBStorage.Mysql
{
    public class GestionDatabase
    {
        private string connString;
        private MySqlConnection conn;

        /// <summary>
        /// get Mysql connection
        /// </summary>
        public MySqlConnection Conn { get { return conn; }}
        private static GestionDatabase Instance = null;

        private GestionDatabase() 
        {
            CreateDatabase();
            CreateUserTable();
            CreateCarteTable();
            Connect();
        }

        /// <summary>
        /// Get the instance of gestion database
        /// </summary>
        /// <returns>the instance</returns>
        public static GestionDatabase GetInstance()
        {
            if (Instance == null)
            {
                Instance = new GestionDatabase();
            }
            return Instance;
        }

        /// <summary>
        /// Connexion à la base de donnée
        /// </summary>
        /// <author>Brian VERCHERE</author>
        private void Connect()
        {
            connString = "server=localhost;uid=root;password=;";

            conn = new MySqlConnection(connString);
            conn.Open();
        }

        /// <summary>
        /// disconnect from database
        /// </summary>
        public void Disconnect()
        {
            conn.Close();
            Instance = null;
        }

        /// <summary>
        /// Créer la base de donnée du jeu si il elle n'existe pas
        /// </summary>
        /// <author>Brian VERCHERE,Charif Mahmoud</author>
        private void CreateDatabase()
        {
            Connect();

            try
            {
                MySqlCommand cmd = new MySqlCommand("create database if not exists Risk", conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Database Risk created successfully or already existing");
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
        private void CreateUserTable()
        {
            Connect();

            try
            {
                MySqlCommand cmd = new MySqlCommand("use risk;", conn);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("create table if not exists Users (IdProfil integer(5) PRIMARY KEY AUTO_INCREMENT, Pseudo varchar(50) NOT NULL UNIQUE, Mdp blob NOT NULL);", conn);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Table Users created successfully or already existing");
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
        private void CreateCarteTable()
        {
            Connect();

            try
            {
                MySqlCommand cmd = new MySqlCommand("use risk;", conn);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("create table if not exists Carte (Carte json NOT NULL);", conn);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Table Carte created successfully or already existing");
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