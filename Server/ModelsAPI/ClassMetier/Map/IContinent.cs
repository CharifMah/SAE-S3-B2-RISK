namespace ModelsAPI.ClassMetier.Map
{
    public interface IContinent
    {
        public Dictionary<string, TerritoireDecorator> DicoTerritoires { get; set; }
    }
}