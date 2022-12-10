namespace DBStorage.ClassMetier.Map
{
    public interface IContinent
    {
        Dictionary<int, ITerritoireBase> DicoTerritoires { get; set; }
    }
}