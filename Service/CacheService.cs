using System.Collections.Generic;
using HackerNews.Cache;
using HackerNews.Utils;
namespace HackerNews.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheProvider _cacheProvider;

        public CacheService(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public IEnumerable<Models.HackerNews> GetCachedNews()
        {
            return _cacheProvider.GetFromCache<IEnumerable<Models.HackerNews>>(CacheKeys.News);
        }

        public void ClearCache()
        {
            _cacheProvider.ClearCache(CacheKeys.News);
        }
    }
}