using ModelsAPI.Converters;
using Newtonsoft.Json;

namespace ModelsAPI.ClassMetier.Units
{
    public interface IUnit
    {
        string Name { get; set; }
        string Description { get; set; }
        int ID { get; set; }
        Elements Element { get; set; }
    }
}
