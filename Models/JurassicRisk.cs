using Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class JurassicRisk
    {
        private Carte carte;
        private List<Joueur> joueurs;

        public List<Joueur> Joueurs { get => joueurs; set => joueurs = value; }
        public Carte Carte { get => carte; set => carte = value; }

        public JurassicRisk(Carte carte, List<Joueur> joueurs)
        {
            Carte = carte;
            Joueurs = joueurs;
        }
    }
}
