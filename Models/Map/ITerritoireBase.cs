using Models.Units;

namespace Models.Map
{
    public interface ITerritoireBase
    {
        Teams Team { get; set; }
        List<IMakeUnit> Units { get; set; }
    }
}