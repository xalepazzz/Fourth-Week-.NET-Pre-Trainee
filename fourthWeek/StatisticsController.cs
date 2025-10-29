using BuisnessLogic;
using BuisnessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fourthWeek
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {

        StatisticsService _statisticsService;
        public StatisticsController(StatisticsService statisticsService) { _statisticsService = statisticsService; }

        [HttpGet("authors/count")]
        public async Task<IActionResult> GetAuthorsBooksCount()
        {
            var result = await _statisticsService.FindAuthorsBooksCountAsync();
            return Ok(result);
        }
        [HttpGet("books/authors")]
        public async Task<IActionResult> GetBooksAuthors()
        {
            var result = await _statisticsService.FindBooksAuthorsAsync();
            return Ok(result);
        }

        [HttpGet("authors/search")]
        public async Task<IActionResult> FindAuthorByName(string request)
        {
            return Ok(await _statisticsService.FindAuthorByNameAsync(request));
        }

        [HttpGet("books/search")]
        public async Task<IActionResult> FindBookByNameAsync(string request)
        {
            return Ok(await _statisticsService.FindBookByNameAsync(request));
        }
    }
}
