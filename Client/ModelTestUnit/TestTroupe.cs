using JurassicRisk;
using Models;
using Models.Fabriques.FabriqueUnite;
using Models.GameStatus;
using Models.Map;
using Models.Player;
using Models.Tours;
using System.Security.Cryptography.Xml;
using IUnit = Models.Units.IUnit;
using Profil = Models.Player.Profil;
using Teams = Models.Player.Teams;
using TerritoireBase = Models.Map.TerritoireBase;
using UniteBase = Models.Units.UniteBase;

namespace ModelTestUnit
{
    public class TestTroupe
    {
        [Fact]
        public void TestPositionnement()
        {
            TerritoireBase t1 = new TerritoireBase(0);
            t1.Team = Teams.ROUGE;
            TerritoireBase t2 = new TerritoireBase(0);
            t2.Team = Teams.VERT;
            Joueur j1 = new Joueur(new Profil("test", "qsd"), Teams.NEUTRE);
            j1.Team = Teams.VERT;

            FabriqueUniteBase f = new FabriqueUniteBase();
            List<IUnit> renforts = new List<IUnit>();
            for (int i = 0; i < 10; i++)
            {
                UniteBase u = new UniteBase();
                renforts.Add(u);
                j1.Units.Add(u);
            }

            j1.AddUnits(renforts, t2);
            Assert.Equal(t2.Team, j1.Team);
            Assert.Equal(41, j1.Units.Count);
            Assert.Equal(renforts.Count, t2.Units.Count);
        }


        [Fact]
        public void TestRenforts()
        {
            Dictionary<string,ITerritoireBase> continents = new Dictionary<string, ITerritoireBase>();
            for(int i=0; i < 3; i++)
            {
                TerritoireBase territory = new TerritoireBase();
                continents.Add(i.ToString(),territory);
            }
            TerritoireBase t1 = new TerritoireBase(0);
            continents.Add("4",t1);
            Continent c = new Continent(continents);
            
            Dictionary<string,IContinent> map = new Dictionary<string, IContinent>();
            map.Add("1",c);

            

            Carte carte = new Carte(map,t1);
            
            Partie p = new Partie(new Placement(),carte);
            Lobby l = new Lobby(p);

            JurasicRiskGameClient.Get.Lobby = l;
            
            JurasicRiskGameClient.Get.Lobby.Partie.Carte = carte;

            t1.Team = Teams.VERT;
            Joueur j1 = new Joueur(new Profil("test", "qsd"), Teams.NEUTRE);
            j1.Team = Teams.VERT;


            for(int i=0; i < 10; i++)
            {
                t1.Units.Add(new UniteBase());
            }
            
            Tour t = new Tour(j1);
            t.Strengthen(10);
            
            Assert.Equal(20,t1.Units.Count);
        }
    }
}
