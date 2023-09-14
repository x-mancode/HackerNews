using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Services
{
	public interface IHackerNewsService
    {
		Task<IEnumerable<Model.News>> GetStories(int numberOfStories);

	}
}
