using HackerNews.LoggerService;
using HackerNews.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class NewsController : Controller
    {
        private readonly IHackerNewsService _hackerNewsService;
        private readonly ILoggerManager _logger;
        private readonly IMemoryCache _cache;
        private readonly CachedHackerNews _cachedHackerNews;
        public NewsController(IHackerNewsService hackerNewsService, ILoggerManager logger) {
            _hackerNewsService = hackerNewsService;
            _logger= logger;
        }
        [HttpGet()]
        [Route("{numberOfStories:int}")]
        public async Task<ActionResult<IList<Models.News>>> getStories(int numberOfStories)
        {
            try
            {
                var res = await _hackerNewsService.GetStories(numberOfStories);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting News: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
