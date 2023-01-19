namespace ModelsAPI.ClassMetier.Units
{
    public class UniteBase : IUnit
    {
        #region Attributes
        protected Elements element;
        protected int id;
        protected string name;
        protected string description;
        #endregion

        #region Property
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public int Id { get => id; set => id = value; }
        public Elements Element { get => element; set => element = value; }
        #endregion


        public UniteBase(int Id, string Description, string Name, Elements Element = Elements.EAU)
        {
            this.element = Element;
            this.id = Id;
            this.name = Name;
            this.description = Description;
        }

        public UniteBase()
        {

        }
    }
}
