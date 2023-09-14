using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using HackerNews.Clients;
using System.Linq;

namespace HackerNews.Services
{
	public class HackerNewsService : IHackerNewsService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly HackerNewsClient _hackerNewsClient;
		private readonly JsonSerializerOptions _options;
		private readonly IMapper _mpper;

		public HackerNewsService(IHttpClientFactory httpClientFactory, HackerNewsClient hackerNewsClient, IMapper mpper)
		{
			_httpClientFactory = httpClientFactory;
			_hackerNewsClient = hackerNewsClient;
			_mpper = mpper;

			_options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		}

		public async Task<IEnumerable<Models.News>> GetStories(int numberOfStories)
		{
			var stories = await _hackerNewsClient.GetStoriesId();

			List<Models.HackerNews> news = await _hackerNewsClient.GetStories(stories);

			var Result = _mpper.Map<List<Models.News>>(news).ToList();

			return Result.OrderByDescending(r => r.Score).ToList<Models.News>(); //.Take(numberOfStories);
		}
	}
}
