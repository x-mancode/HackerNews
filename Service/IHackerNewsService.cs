using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Services
{
	public interface IHackerNewsService
    {
		Task<List<Model.News>> GetStories(int numberOfStories);

	}
}
