﻿namespace Models.Units
{
    /// <summary>
    /// Unite decorateur
    /// </summary>
    public class UniteDecorator : IUnit
    {
        private string _sourceUri;
        private IUnit _uniteBase;
        public UniteDecorator(UniteBase uniteBase)
        {
            _uniteBase = uniteBase;
            if (uniteBase != null)

                switch (uniteBase.Name)
                {
                    case "Brachiosaurus":
                        _sourceUri = $"pack://application:,,,/Sprites/Units/{uniteBase.Name}.png";
                        break;
                    case "Baryonyx":
                        _sourceUri = $"pack://application:,,,/Sprites/Units/{uniteBase.Name}.png"; ;
                        break;
                    case "Rex":
                        _sourceUri = $"pack://application:,,,/Sprites/Units/{uniteBase.Name}.png"; ;
                        break;
                    case "Pterosaure":
                        _sourceUri = $"pack://application:,,,/Sprites/Units/{uniteBase.Name}.png"; ;
                        break;
                }
        }

        public IUnit UniteBase { get => _uniteBase; set => _uniteBase = value; }

        public string Name => _uniteBase.Name;

        public string Description => _uniteBase.Description;

        public int Id => _uniteBase.Id;

        public Elements Element { get => _uniteBase.Element; set => _uniteBase.Element = value; }

        public string UriSource { get => this._sourceUri; set => this._sourceUri = value; }
    }
}
