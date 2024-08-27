using CodeTestSantander.Interface;
using CodeTestSantander.Models;

namespace CodeTestSantander.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private IHttpClientFactory HttpClientFactory { get; }

        public HackerNewsService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }


        public async Task<List<HackerNewsModel>> GetBestNews()
        {
            var result = new List<HackerNewsModel>();

            using (var httpClient = HttpClientFactory.CreateClient())
            {
                var allnews = await httpClient.GetAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
                var taskResponse = await allnews.Content.ReadFromJsonAsync<List<int>>();
                foreach (var newsId in taskResponse)
                {
                    var newsInfo = await GetNewsInfo(newsId);
                    result.Add(newsInfo);
                }
            }
            return result;
        }

        public async Task<HackerNewsModel> GetNewsInfo(int newsId)
        {
            var result = new HackerNewsModel();

            using (var httpClient = HttpClientFactory.CreateClient())
            {
                var allnews = await httpClient.GetAsync("https://hacker-news.firebaseio.com/v0/item/" + newsId + ".json");
                result = await allnews.Content.ReadFromJsonAsync<HackerNewsModel>();
            }

            return result;
        }
    }
}
