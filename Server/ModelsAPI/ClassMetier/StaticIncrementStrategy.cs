using Redis.OM;

namespace ModelsAPI.ClassMetier
{
    public class StaticIncrementStrategy : IIdGenerationStrategy
    {
        public static int Current = 0;
        public string GenerateId()
        {
            return Current++.ToString();
        }
    }
}
