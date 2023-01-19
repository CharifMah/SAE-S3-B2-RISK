namespace Models.Map
{
    public interface IContinent
    {
        ITerritoireBase[] Territoires { get; set; }
    }
}