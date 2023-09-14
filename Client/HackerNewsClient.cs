using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HackerNews.Clients
{
	public class HackerNewsClient
	{
		private readonly HttpClient _client;
		private readonly JsonSerializerOptions _options;

		public HackerNewsClient(HttpClient client)
		{
			_client = client;

			_client.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/");
			_client.Timeout = new TimeSpan(0, 0, 30);
			_client.DefaultRequestHeaders.Clear();

			_options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		}

		public async Task<int[]> GetStoriesId()
		{
			using (var response = await _client.GetAsync("beststories.json", HttpCompletionOption.ResponseHeadersRead))
			{
				response.EnsureSuccessStatusCode();

				var stream = await response.Content.ReadAsStreamAsync();

				var StoriesId = await JsonSerializer.DeserializeAsync<int[]>(stream, _options);

				return StoriesId;
			}
		}

        public async Task<object> GetStory(int storyId)
        {
                var url = string.Format("item/{0}.json", storyId);
            return await _client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            //    using (var response = await _client.GetAsync("item/121003.json", HttpCompletionOption.ResponseHeadersRead))
            //{

            //        response.EnsureSuccessStatusCode();

            //        var stream = await response.Content.ReadAsStreamAsync();

            //        using (var reader = new StreamReader(stream))
            //        {
            //            var result = reader.ReadToEnd();
            //             return JsonSerializer.Deserialize<Model.HackerNews>(result, _options);
            //        }
            //}
        }

        public async Task<List<Model.HackerNews>> GetStories(int[] storiesId)
        {
            List<Model.HackerNews> resultNews= new List<Model.HackerNews>();
            var stories = new List<object>();
            var batchSize = 50;
            int numberOfBatches = (int)Math.Ceiling((double)storiesId.Length / batchSize);

            for (int i = 0; i < numberOfBatches; i++)
            {
                var currentIds = storiesId.Skip(i * batchSize).Take(batchSize);
                var tasks = currentIds.Select(id => GetStory(id));
                 stories.AddRange(await Task.WhenAll(tasks));
            }
            foreach (var story in stories)
            {
                var response = (HttpResponseMessage)story;

                if (response.IsSuccessStatusCode) {
                    var stream = await response.Content.ReadAsStreamAsync();
                    using (var reader = new StreamReader(stream))
                    {
                        var result = reader.ReadToEnd();
                        resultNews.Add(JsonSerializer.Deserialize<Model.HackerNews>(result, _options));
                    }
                }
            }
            return resultNews;
        }
    }
}
