﻿using StackExchange.Redis;
using System.Net;

namespace Models
{
    public class RedisProvider
    {

        #region Attributes

        private ConnectionMultiplexer redisClient;
        private IDatabase redisDatabase;
        private IServer currentServer;
        private EndPoint[] endPoints;

        #endregion

        #region Property
        public IDatabase RedisDataBase { get { return redisDatabase; } }

        public ISubscriber redisPubSub;

        public bool IsRedisEnableByConfig { get; set; }
        #endregion

        #region Singleton
        private static readonly Lazy<RedisProvider> lazyObj = new Lazy<RedisProvider>(() => new RedisProvider());
        public static RedisProvider Instance => lazyObj.Value;
        private RedisProvider()
        {
            if (!lazyObj.IsValueCreated)
            {
                IsRedisEnableByConfig = true;
                redisClient = ConnectionMultiplexer.Connect("localhost:6379,abortConnect=false,connectTimeout=30000");
                redisDatabase = redisClient.GetDatabase(0);
                endPoints = redisClient.GetEndPoints();
                currentServer = redisClient.GetServer(endPoints.First());
            }
        }
        #endregion

        public void ManageSubscriber(Func<string,Task> methode)
        {
            redisPubSub = redisClient.GetSubscriber();

            redisPubSub.Subscribe("__keyevent@0__:json.set", (channel, message) =>
            {
                if (message.StartsWith("Lobby"))
                {
                    message = message.ToString().Substring(message.ToString().LastIndexOf(':') + 1);
                }
                methode(message);
            });
        }
    }
}
