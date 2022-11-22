using MySql.Data.MySqlClient;

namespace DBStorage.Mysql
{
    public class GestionDatabase
    {
        private string connString;
        private MySqlConnection conn;

        public MySqlConnection Conn { get { return conn; }}
        private static GestionDatabase Instance = null;

        private GestionDatabase() 
        {
            CreateDatabase();
            CreateUserTable();
            Connect();
        }

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

            conn = null;
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
                cmd = new MySqlCommand("create table if not exists Users (Pseudo varchar(50) PRIMARY KEY);", conn);
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

        ///// <summary>
        ///// Selectionne un Users dans la base de donnee par son login
        ///// </summary>
        ///// <param name="login">login du Users</param>
        ///// <author>Romain BARABANT</author>
        //public string SelectUser(string login)
        //{
        //    Connect();
        //    string res = null;
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand("use risk;", conn);
        //        cmd.ExecuteNonQuery();
        //        cmd = new MySqlCommand("Select * from users where Pseudo = '" + login + "';", conn);
        //        cmd.ExecuteNonQuery();
        //        using (MySqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            if (reader.Read())
        //            {
        //                res = string.Format("{0}", reader["Pseudo"]);
        //            }
        //        }
        //        Console.WriteLine("Users " + login + " selectionne");
        //    }
        //    catch (Exception x)
        //    {
        //        Console.WriteLine("An error occured: {0}", x.Message);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return res;
        //}

        ///// <summary>
        ///// creer un Users dans la base de donnee par son login
        ///// </summary>
        ///// <param name="login">login du Users</param>
        ///// <author>Romain BARABANT</author>
        //public void CreateUser(string login)
        //{
        //    Connect();

        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand("use risk;", conn);
        //        cmd.ExecuteNonQuery();
        //        cmd = new MySqlCommand("Insert into users values ('" + login + "');", conn);
        //        cmd.ExecuteNonQuery();
        //        Console.WriteLine("Users " + login + " creer");
        //    }
        //    catch (Exception x)
        //    {
        //        Console.WriteLine("An error occured: {0}", x.Message);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

        //public bool VerifUserCreation(string pseudo)
        //{
        //    Connect();
        //    bool res = false;
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand("use risk;", conn);
        //        cmd.ExecuteNonQuery();
        //        cmd = new MySqlCommand($"SELECT * FROM users WHERE Pseudo = '{pseudo}';", conn);
        //        cmd.ExecuteNonQuery();
        //        using (MySqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            if (reader.Read())
        //            {
        //                if (reader["Pseudo"].ToString() == pseudo)
        //                {
        //                    res = true;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    conn.Close();
        //    return res;
        //}
    }
}