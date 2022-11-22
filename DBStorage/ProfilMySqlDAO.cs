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

        public void Delete(Profil profil)
        {
            throw new NotImplementedException();
        }

        public string FindAll()
        {
            throw new NotImplementedException();
        }

        public string FindByIdProfil(string pseudo)
        {
            string res = null;
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
                        res = string.Format("{0}", reader["Pseudo"]);
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

        public void Insert(Profil profil)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("use risk;", GestionDatabase.GetInstance().Conn);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("insert into users values ('" + profil.Pseudo + "');", GestionDatabase.GetInstance().Conn);
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

        public void Update(Profil profil)
        {
            throw new NotImplementedException();
        }

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
