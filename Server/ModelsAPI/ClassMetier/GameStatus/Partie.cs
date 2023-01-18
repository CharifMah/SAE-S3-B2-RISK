using ModelsAPI.ClassMetier.Fabriques.FabriqueUnite;
using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;

namespace ModelsAPI.ClassMetier.GameStatus
{
    public class Partie
    {
        #region Attributes
        private int _playerIndex;

        private Etat etat = null;

        private List<Joueur> _joueurs;

        private Carte _carte;
        private string _owner;
        private string _id;
        #endregion

        #region Property
        public Carte Carte { get => _carte; set => _carte = value; }

        public List<Joueur> Joueurs
        {
            get { return _joueurs; }
            set { _joueurs = value; }
        }

        public Etat Etat { get { return etat; } }

        public string Id { get => _id; set => _id = value; }
        public int PlayerIndex { get => _playerIndex; }
        public string Owner { get => _owner; set => _owner = value; }
        #endregion

        #region Constructor

        public Partie(Carte carte, List<Joueur> joueurs, string id)
        {
            this._carte = carte;
            this._joueurs = joueurs;
            this._id = id;
            this.etat = new Deploiment();

            _playerIndex = -1;
        }

        #endregion

        public int NextPlayer()
        {
            _playerIndex++;
            _playerIndex %= _joueurs.Count;

            return _playerIndex;
        }

        public bool Action(List<int> unitList)
        {
            bool res = false;
            if (this._joueurs.Count != 0 && this._joueurs.Count >= _playerIndex)
            {
                res = etat.Action(this._carte, this._joueurs[_playerIndex], unitList);
            }
            else
            {
                Console.WriteLine($"{this._joueurs.Count} player in game");
            }
            return res;
        }

        public void Transition()
        {
            //Change l'etat
            this.etat = etat.TransitionTo(this._joueurs, this._carte);

            switch (etat.ToString())
            {
                case "Renforcement":
                    (etat as Renforcement).JoueurActuel = this._joueurs[_playerIndex];
                    // Calcul des renforts du joueur
                    int nbTerritoires = 0;
                    int nbRenforts = 0;

                    foreach (Continent c in this._carte.DicoContinents.Values)
                    {
                        foreach (TerritoireBase t in c.DicoTerritoires.Values)
                        {
                            if (t.Team == this._joueurs[_playerIndex].Team)
                            {
                                nbTerritoires++;
                            }
                        }
                    }

                    if (nbTerritoires / 3 < 3)
                    {
                        nbRenforts = 3;
                    }
                    else
                    {
                        nbRenforts = nbTerritoires / 3;
                    }


                    // Ajout des renforts au joueur
                    FabriqueUniteBase f = new FabriqueUniteBase();
                    for (int i = 0; i < nbRenforts; i++)
                    {
                        Random random = new Random();
                        switch (random.Next(3))
                        {
                            case 1:
                                this._joueurs[_playerIndex].Units.Add(f.Create("Rex"));
                                break;
                            case 2:
                                this._joueurs[_playerIndex].Units.Add(f.Create("Brachiosaure"));
                                break;
                            case 3:
                                this._joueurs[_playerIndex].Units.Add(f.Create("Baryonyx"));
                                break;
                            case 4:
                                this._joueurs[_playerIndex].Units.Add(f.Create("Pterosaure"));
                                break;
                        }
                    }
                    break;
            }


        }

        /// <summary>
        /// Rejoins le lobby (ajoute le joueur dans la liste des joueurs)
        /// </summary>
        /// <param name="joueur">le joueur qui rejoint</param>
        /// <returns>vrai si le joueur a rejoin</returns>
        public bool JoinPartie(Joueur joueur)
        {
            bool res = false;
            IEnumerable<Joueur?> j = _joueurs.Where(x => x.Profil.Pseudo == joueur.Profil.Pseudo);
            if (_joueurs != null)
            {
                if (_joueurs.Count == 4 && j.Count() == 1)
                {
                    res = true;
                }
                if (_joueurs.Count < 4 && j.Count() == 0)
                {
                    _joueurs.Add(joueur);

                    res = true;
                }
                Console.WriteLine($"nbr joueur : {_joueurs.Count} ");
            }
            return res;
        }

        /// <summary>
        /// Quitte le lobby retire le joueur de la liste
        /// </summary>
        /// <param name="joueur">Joueur qui quitte</param>
        /// <returns>vrai si le joueur a quitter</returns>
        public bool ExitPartie(Joueur joueur)
        {
            bool res = false;
            Joueur joueurToRemove = _joueurs.Find(x => x.Profil.Pseudo == joueur.Profil.Pseudo);
            if (joueurToRemove != null)
            {
                _joueurs.Remove(joueurToRemove);
                res = true;
            }
            else
            {
                throw new ArgumentNullException();
            }


            return res;
        }
    }
}
