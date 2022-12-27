namespace ModelsAPI.ClassMetier.Units
{
    public interface IUnit
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }
        public Elements Element { get; set; }
    }
}

