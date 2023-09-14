using System.Collections.Generic;

namespace HackerNews.Services
{
    public interface ICacheService
    {
        IEnumerable<Models.HackerNews> GetCachedNews();
        void ClearCache();
    }
}