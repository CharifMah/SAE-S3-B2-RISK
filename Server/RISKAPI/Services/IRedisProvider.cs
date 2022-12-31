using StackExchange.Redis;

namespace RISKAPI.Services
{
    public interface IRedisProvider
    {
        bool IsRedisEnableByConfig { get; set; }
        IDatabase RedisDataBase { get; }

        void ManageSubscriber(Func<string, Task> methode);
    }
}