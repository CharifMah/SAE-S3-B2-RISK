using Models.Map;

namespace Models.GameStatus
{
    public class Partie
    {
        #region Attributes
        private Etat etat = null;

        private Carte _carte;

        #endregion

        #region Property
        public Carte Carte { get => _carte; set => _carte = value; }

        #endregion

        #region Constructor

        public Partie(Etat etat, Carte carte)
        {
            TransitionTo(etat);
            this.Carte = carte;
        }

        #endregion

        public void TransitionTo(Etat etat)
        {
            Console.WriteLine($"Context: Transition to {etat.GetType().Name}.");
            this.etat = etat;
            etat.SetContext(this);
        }

        public void PositionnerTroupe()
        {
            etat.PositionnerTroupe();
        }

        public void FinDeTour()
        {
            etat.FinDeTour();
        }
    }
}
