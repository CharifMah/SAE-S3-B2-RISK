namespace ModelsAPI.ClassMetier.Units
{
    public class Brachiosaurus : UniteBase
    {
        public Brachiosaurus(int id, Elements element = Elements.EAU) : base(id, element)
        {
            this.id = id;
            this.element= element;
            name = "Brachiosaurus";
            description = "this is a Brachiosaurus";

        }
    }
}
