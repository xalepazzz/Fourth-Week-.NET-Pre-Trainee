using BuisnessLogic;
using Microsoft.AspNetCore.Mvc;
using DatabaseLayer.Models;
using DatabaseLayer;

namespace thirdweek
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        BookService _bookService;
        AuthorService _authorService;
        public BookController(BookService bookService, AuthorService authorService) { _bookService = bookService; _authorService = authorService; }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                Book book = await _bookService.GetBookByIdAsync(id);
                return Ok(book);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                List<Book> books = await _bookService.GetAllBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка сервера: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(string title, DateOnly? publishDate, int? authorId)
        {
            try
            {
                if (!publishDate.HasValue || !authorId.HasValue)
                    return BadRequest("Необходимо указать publishDate и authorId");

                await _bookService.AddBookAsync(title, publishDate.Value, authorId.Value);
                return Ok("Книга успешно добавлена");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, string? title, DateOnly? publishDate, int? authorId)
        {
            try
            {
                if (authorId.HasValue)
                {
                    await _authorService.GetAuthorByIdAsync(authorId.Value);
                }

                await _bookService.ModifyBookAsync(id, title, publishDate, authorId);
                return Ok("Книга успешно обновлена");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return Ok("Книга успешно удалена");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}