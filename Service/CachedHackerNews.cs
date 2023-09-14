using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HackerNews.Cache;
using HackerNews.Utils;
using HackerNews.Models;
using System.Linq;

namespace HackerNews.Services
{
    public class CachedHackerNews : IHackerNewsService
    {

        private readonly HackerNewsService _hackerNewsService;
        private readonly ICacheProvider _cacheProvider;

        private static readonly SemaphoreSlim GetStoriesSemaphore = new SemaphoreSlim(1, 1);

        public CachedHackerNews(HackerNewsService hackerNewsService, ICacheProvider cacheProvider)
        {
            _hackerNewsService = hackerNewsService;
            _cacheProvider = cacheProvider;
        }

        async Task<IEnumerable<News>> IHackerNewsService.GetStories(int numberOfStories)
        {
            var news = await GetCachedResponse(CacheKeys.News, numberOfStories, GetStoriesSemaphore, () => _hackerNewsService.GetStories(numberOfStories));
            return news.Take(numberOfStories);
        }   
        private async Task<IEnumerable<Models.News>> GetCachedResponse(string cacheKey, int numberOfStories, SemaphoreSlim semaphore, Func<Task<IEnumerable<Models.News>>> func)
        {
            var news = _cacheProvider.GetFromCache<IEnumerable<Models.News>>(cacheKey);

            if (news != null && news.Count() > numberOfStories) return news;
            try
            {
                await semaphore.WaitAsync();
                news = _cacheProvider.GetFromCache<IEnumerable<Models.News>>(cacheKey); // Recheck to make sure it didn't populate before entering semaphore
                if (news != null && news.Count() > numberOfStories)
                {
                    return news;
                }
                 news = (IEnumerable<Models.News>)await func();
                _cacheProvider.SetCache(cacheKey, news, DateTimeOffset.Now.AddDays(1));
            }
            finally
            {
                semaphore.Release();
            }

            return news;
        }

        
    }
}