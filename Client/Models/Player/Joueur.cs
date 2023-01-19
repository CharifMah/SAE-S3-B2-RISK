using Models.Fabriques.FabriqueUnite;
using Models.Map;
using Models.Son;
using Models.Units;
using Newtonsoft.Json;
using Stockage.Converters;

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

            for (int i = 0; i < 9; i++)
            {
                _units.Add(new UniteDecorator(f.Create("Rex")));
                _units.Add(new UniteDecorator(f.Create("Brachiosaurus")));
                _units.Add(new UniteDecorator(f.Create("Baryonyx")));
                _units.Add(new UniteDecorator(f.Create("Pterosaure")));
            }
            _units.Add(new UniteDecorator(f.Create("Pterosaure")));

            _connectionId = "NotConnected";
            _profil = profil;
            _team = team;
        }

        #endregion

        private void AddUnits(List<int> unites, ITerritoireBase territoire)
        {
            if ((_team == territoire.Team || territoire.Team == Teams.NEUTRE))
            {
                foreach (var unit in unites)
                {
                    territoire.AddUnit(_units[unit]);
                    territoire.Team = _team;
                    _units.RemoveAt(unit);
                    SoundStore.Get("Slidersound.mp3").Play();
                }
            }

        }


        private bool AddUnit(int indexUnit, ITerritoireBase territoire)
        {
            bool res = false;
            if (_units.Count > 0 && _team == territoire.Team || (_units.Count > 0 && territoire.Team == Teams.NEUTRE))
            {
                territoire.AddUnit(_units[indexUnit]);
                territoire.Team = _team;
                _units.RemoveAt(indexUnit);
                res = true;
            }
            return res;
        }

        public void PlaceUnits(List<int> unitToPlace, ITerritoireBase territoire)
        {
            if (this._units.Count > 0)
            {
                this.AddUnits(unitToPlace, territoire);
            }
        }

        public bool PlaceUnit(int indexUnit, ITerritoireBase territoire)
        {
            bool res = false;
            if (this._units.Count > 0)
            {
                res = this.AddUnit(indexUnit, territoire);
            }
            return res;
        }

        public void AddUnit(IUnit unit)
        {
            this._units.Add(unit);
        }

        public void RemoveUnit(int unit)
        {
            this._units.RemoveAt(unit);
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
