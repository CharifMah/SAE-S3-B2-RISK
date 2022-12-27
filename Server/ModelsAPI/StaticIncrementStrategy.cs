using Redis.OM;

namespace ModelsAPI
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
