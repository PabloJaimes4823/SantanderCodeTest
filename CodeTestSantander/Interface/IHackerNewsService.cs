using CodeTestSantander.Models;

namespace CodeTestSantander.Interface
{
    public interface IHackerNewsService
    {
            Task<List<HackerNewsModel>> GetBestNews();
        
    }
}
