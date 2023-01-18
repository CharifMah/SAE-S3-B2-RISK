using Models.Fabriques.FabriqueUnite;
using Models.Map;
using Models.Son;
using Models.Units;
using Newtonsoft.Json;
using Stockage.Converters;
using System.Windows;

namespace Models.Player
{
    public class Joueur
    {
        #region Attribute
        private bool _isReady;
        private string _connectionId;
        private Teams _team;
        private List<IUnit> _units;
        private Profil _profil;
        private string messageerr;
        private string title;
        #endregion

        #region Property

        public Teams Team
        {
            get { return _team; }
            set { _team = value; }
        }

        [JsonConverter(typeof(ConcreteCollectionTypeConverter<List<IUnit>, UniteBase, IUnit>))]
        public List<IUnit> Units
        {
            get { return _units; }
            set { _units = value; }
        }

        public Profil Profil
        {
            get { return _profil; }
            set { _profil = value; }
        }

        public bool IsReady
        {
            get => _isReady;
            set => _isReady = value;
        }

        #endregion

        #region Constructor

        public Joueur(Profil profil, Teams team)
        {
            _isReady = false;
            FabriqueUniteBase f = new FabriqueUniteBase();
            _units = new List<IUnit>();
            Random random = new Random();
            for (int i = 0; i < 41; i++)
            {
                switch (random.Next(4))
                {
                    case 0:
                        _units.Add(new UniteDecorator(f.Create("Rex")));
                        break;
                    case 1:
                        _units.Add(new UniteDecorator(f.Create("Brachiosaurus")));
                        break;
                    case 2:
                        _units.Add(new UniteDecorator(f.Create("Baryonyx")));
                        break;
                    case 3:
                        _units.Add(new UniteDecorator(f.Create("Pterosaure")));
                        break;
                }
            }
            _connectionId = "NotConnected";
            _profil = profil;
            _team = team;
        }

        #endregion

        private void AddUnits(List<IUnit> unites, ITerritoireBase territoire)
        {
            if ((_team == territoire.Team || territoire.Team == Teams.NEUTRE))
            {
                foreach (var unit in unites)
                {
                    if (_units.Contains(unit))
                    {
                        _units.Remove(unit);

                        territoire.AddUnit(unit);
                        territoire.Team = _team;
                    }
                    SoundStore.Get("Slidersound.mp3").Play();
                    territoire.AddUnit(unit);
                    territoire.Team = this._team;
                    
                }
            }
            
        }
       

        private void AddUnits(IUnit unit, ITerritoireBase territoire)
        {
            if (_team == territoire.Team || territoire.Team == Teams.NEUTRE)
            {
                if (_units.Contains(unit))
                {
                    _units.Remove(unit);

                    territoire.AddUnit(unit);
                    territoire.Team = _team;

                }
            }
        }

        public void PlaceUnits(List<IUnit> unitToPlace, ITerritoireBase territoire)
        {
            if (this._units.Count > 0)
            {
                this.AddUnits(unitToPlace, territoire);
            }
        }

        public void PlaceUnits(IUnit unitToPlace, ITerritoireBase territoire)
        {
            if (this._units.Count > 0)
            {
                this.AddUnits(unitToPlace, territoire);
            }
        }

        public void AddUnit(IUnit unit)
        {
            this._units.Add(unit);
        }

        public void RemoveUnit(IUnit unit)
        {
            this._units.Remove(unit);
        }

        public void RemoveUnit(List<IUnit> unites, ITerritoireBase territoire)
        {
            foreach (var unit in unites)
            {
                if (_units.Contains(unit))
                {
                    territoire.RemoveUnit(unit);
                }
            }
        }
    }
}
