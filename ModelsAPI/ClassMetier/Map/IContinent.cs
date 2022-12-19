namespace ModelsAPI.ClassMetier.Map
{
    public interface IContinent
    {
        public Dictionary<int, ITerritoireBase> DicoTerritoires { get; set; }
    }
}