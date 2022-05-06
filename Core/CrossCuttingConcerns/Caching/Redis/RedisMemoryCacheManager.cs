using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
    public class RedisMemoryCacheManager : ICacheManager
    {
        protected readonly RedisServer _redisServer;

        public RedisMemoryCacheManager(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }

        public void Add(string key, object value, int duration)
        {
            var jsonData = JsonConvert.SerializeObject(value);
            _redisServer.Database.StringSet(key, jsonData, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            if (Exist(key))
            {
                string jsonData = _redisServer.Database.StringGet(key);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            return default(T);
        }

        public object Get(string key)
        {
            return Get<object>(key);
        }

        public bool IsAdd(string key)
        {
            return _redisServer.Database.KeyExists(key);
        }

        public void Remove(string key)
        {
            if (Exist(key))
            {
                _redisServer.Database.KeyDelete(key);
            }
        }

        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            foreach (string key in _redisServer.GetAllKeys())
            {
                if (regex.IsMatch(key))
                {
                    Remove(key);
                }
            }
        }

        private bool Exist(string key)
        {
            return _redisServer.Database.KeyExists(key);
        }
    }
}
