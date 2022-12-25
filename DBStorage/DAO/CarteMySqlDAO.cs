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
            return res;
        }

        public void Insert(Carte carte)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("use risk; DELETE FROM carte;", GestionDatabase.GetInstance().Conn);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand($"insert into carte (`DicoContinent`,`SelectedTerritoire`) values ('{carte.DicoContinents}','{carte.SelectedTerritoire}')", GestionDatabase.GetInstance().Conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception x)
            {
                Console.WriteLine("an error occured: {0}", x.Message);
            }
        }

        public void Update(Carte carte)
        {
            throw new NotImplementedException();
        }
    }
}
