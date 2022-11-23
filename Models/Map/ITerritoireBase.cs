using Models.Units;

namespace Models.Map
{
    public interface ITerritoireBase
    {
        Teams Team { get; set; }
        List<Unite> Units { get; set; }
    }
}