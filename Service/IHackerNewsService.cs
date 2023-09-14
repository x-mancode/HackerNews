using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Services
{
	public interface IHackerNewsService
    {
	 Task<IEnumerable<Models.News>> GetStories(int numberOfStories);

	}
}
