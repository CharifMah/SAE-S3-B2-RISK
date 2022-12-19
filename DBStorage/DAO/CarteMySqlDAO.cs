using DBStorage.Mysql;
using ModelsAPI.ClassMetier.Map;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace DBStorage.DAO
{
    public class CarteMySqlDAO : CarteDAO
    {
        public string Get()
        {
            string res = "";
            try
            {
                MySqlCommand cmd = new MySqlCommand("use risk;", GestionDatabase.GetInstance().Conn);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("Select * from carte;", GestionDatabase.GetInstance().Conn);
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
                Console.WriteLine("Carte " + res + " selectionne");
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

        public void Insert(Carte carte)
        {
            try
            {
                string jsonCarte = JsonSerializer.Serialize(carte);

                MySqlCommand cmd = new MySqlCommand("use risk;", GestionDatabase.GetInstance().Conn);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("insert into carte (carte) values (\"" + jsonCarte + "\");", GestionDatabase.GetInstance().Conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("carte " + jsonCarte + " creer");
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

        public void Update(Carte carte)
        {
            throw new NotImplementedException();
        }
    }
}
