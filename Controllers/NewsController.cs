using HackerNews.Model;
using HackerNews.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class NewsController : Controller
    {
        private readonly IHackerNewsService _hackerNewsService;


        public NewsController(IHackerNewsService hackerNewsService) {
            _hackerNewsService = hackerNewsService;
        }
        [HttpGet()]
        [Route("{id:int}")]
        public async Task<List<Model.News>> getStories(int id)
        {
            return await _hackerNewsService.GetStories(3);
        }
    }
}
