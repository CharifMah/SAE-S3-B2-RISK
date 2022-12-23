namespace ModelsAPI.ClassMetier.Units
{
    internal class Baryonyx : UniteBase
    {
        public Baryonyx(int id, Elements element = Elements.EAU) : base(id, element)
        {
            this.id = id; 
            this.element = element; 
            name = "Baryonyx"; 
            description = "this is a Baryonyx";
        }
    }
}
