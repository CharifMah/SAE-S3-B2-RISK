namespace Models.Units
{
    public interface IUnit
    {
        string Name { get; }
        string Description { get; }
        int Id { get; }
        Elements Element { get; set; }
    }
}
