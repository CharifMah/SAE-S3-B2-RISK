namespace Models.Map
{
    public interface IContinent
    {
        Dictionary<string, ITerritoireBase> DicoTerritoires { get; set; }
    }
}