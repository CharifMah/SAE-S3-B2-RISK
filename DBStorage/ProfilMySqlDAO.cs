using DBStorage.ClassMetier;
using DBStorage.Mysql;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBStorage
{
    public class ProfilMySqlDAO : ProfilDAO
    {
        /// <summary>
        /// find a profil by id
        /// </summary>
        /// <param name="id">the id of the profil that you lookig for</param>
        /// <returns>the pseudo of the profil</returns>
        public string FindByIdProfil(int id)
        {
            string res = "";
            try
            {
                MySqlCommand cmd = new MySqlCommand("use risk;", GestionDatabase.GetInstance().Conn);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("Select * from users where IdProfil = '" + id + "';", GestionDatabase.GetInstance().Conn);
                cmd.ExecuteNonQuery();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            res += reader.GetString(i) + ",";

                        }
                    }
                }
                Console.WriteLine("Users " + id + " selectionne");
            }
            catch (Exception x)
            {
                Console.WriteLine("An error occured: {0}", x.Message);
            }
            finally
            {
                GestionDatabase.GetInstance().Disconnect();
            }
            return res;

        }

        /// <summary>
        /// find the id of a profil by his pseudo
        /// </summary>
        /// <param name="pseudo">the pseudo of the profil that you lookig for</param>
        /// <returns>the id of the profil</returns>
        public int FindIdByPseudoProfil(string pseudo)
        {
            int res = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand("use risk;", GestionDatabase.GetInstance().Conn);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("Select * from users where Pseudo = '" + pseudo + "';", GestionDatabase.GetInstance().Conn);
                cmd.ExecuteNonQuery();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        res = reader.GetInt32("IdProfil");
                    }
                }
                Console.WriteLine("Users " + pseudo + " selectionne");
            }
            catch (Exception x)
            {
                Console.WriteLine("An error occured: {0}", x.Message);
            }
            finally
            {
                GestionDatabase.GetInstance().Disconnect();
            }
            return res;

        }

        /// <summary>
        /// insert a profil in the database
        /// </summary>
        /// <param name="profil">the profil to insert</param>
        public void Insert(Profil profil)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("use risk;", GestionDatabase.GetInstance().Conn);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("insert into users (Pseudo,Mdp) values (\"" + profil.Pseudo + "\",\"" + profil.Password + "\");", GestionDatabase.GetInstance().Conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("users " + profil.Pseudo + " creer");
            }
            catch (Exception x)
            {
                Console.WriteLine("an error occured: {0}", x.Message);
            }
            finally
            {
                GestionDatabase.GetInstance().Disconnect();
            }
        }

        /// <summary>
        /// verify if the profil exist inthe database
        /// </summary>
        /// <param name="profil">profil to verify</param>
        /// <returns>true if it exist in the database</returns>
        public bool VerifUserCreation(Profil profil)
        {
            bool res = false;
            try
            {
                MySqlCommand cmd = new MySqlCommand("use risk;", GestionDatabase.GetInstance().Conn);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand($"SELECT * FROM users WHERE Pseudo = '{profil.Pseudo}';", GestionDatabase.GetInstance().Conn);
                cmd.ExecuteNonQuery();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader["Pseudo"].ToString() == profil.Pseudo)
                        {
                            res = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            GestionDatabase.GetInstance().Disconnect();
            return res;
        }

    }
}
