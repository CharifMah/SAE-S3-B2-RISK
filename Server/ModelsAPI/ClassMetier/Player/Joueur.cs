using ModelsAPI.ClassMetier.Fabriques.FabriqueUnite;
using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Units;
using Newtonsoft.Json;
using Redis.OM.Modeling;
using Stockage.Converters;

namespace ModelsAPI.ClassMetier.Player
{
    [Document(StorageType = StorageType.Json, Prefixes = new[] { "Joueur" })]
    public class Joueur
    {
        #region Attribute
        private bool _isReady;
        private Teams _team;
        private List<IUnit> _units;
        private Profil _profil;

        #endregion

        #region Property
        [Indexed]
        public Teams Team
        {
            get { return _team; }
            set { _team = value; }
        }
        [Indexed]
        [JsonConverter(typeof(ConcreteCollectionTypeConverter<List<IUnit>, UniteBase, IUnit>))]
        public List<IUnit> Units
        {
            get { return _units; }
            set { _units = value; }
        }

        [Indexed]
        public Profil Profil
        {
            get { return _profil; }
            set { _profil = value; }
        }
        [Indexed]
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
            for (int i = 0; i < 90; i++)
            {
                switch (random.Next(4))
                {
                    case 0:
                        _units.Add(f.Create("Rex"));
                        break;
                    case 1:
                        _units.Add(f.Create("Brachiosaurus"));
                        break;
                    case 2:
                        _units.Add(f.Create("Baryonyx"));
                        break;
                    case 3:
                        _units.Add(f.Create("Pterosaure"));
                        break;
                }
            }

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

        public void RemoveUnit(IUnit unit)
        {
            _units.Remove(unit);
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
