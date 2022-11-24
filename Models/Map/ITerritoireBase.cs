using Models.Units;

namespace Models.Map
{
    public interface ITerritoireBase
    {
        int ID { get; set; }
        Teams Team { get; set; }
        List<Unite> Units { get; set; }

        void AddUnit(Unite unite);
    }
}