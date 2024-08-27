using CodeTestSantander.Interface;
using CodeTestSantander.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeTestSantander.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HackerNewsController : ControllerBase
    {
        private readonly ILogger<HackerNewsController> _logger;
        private readonly ICacheService _cacheService;
        private readonly IHackerNewsService _hnService;

        public HackerNewsController(ILogger<HackerNewsController> logger, ICacheService cacheService, IHackerNewsService hnService)
        {
            _logger = logger;
            _cacheService = cacheService;
            _hnService = hnService;
        }

        [HttpGet(Name = "GetHackerNews")]
        public async Task<ActionResult<List<HackerNewsModel>>> Get(int numberOfNews)
        {
            if (numberOfNews<0) {
                return BadRequest();
            }

            var result = new List<HackerNewsModel>();

            var cacheData = _cacheService.GetData<List<HackerNewsModel>>("BestNews");
            if (cacheData == null)
            {
                var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
                cacheData = await _hnService.GetBestNews();
                _cacheService.SetData<List<HackerNewsModel>>("BestNews", cacheData, expirationTime);
            }

            result=cacheData.ToList().OrderByDescending(x=>x.Score).Take(numberOfNews).ToList();

            return Ok(result);

        }
    }
}
