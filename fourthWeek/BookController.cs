using BuisnessLogic.Interfaces;
using BuisnessLogic.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace thirdweek
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        IBookService _bookService;
        public BookController(IBookService bookService) { _bookService = bookService;}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            BookDTO book = await _bookService.GetBookByIdAsync(id);
                return Ok(book);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
                List<BookDTO> books = await _bookService.GetAllBooksAsync();
                return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(string title, DateOnly publishDate, int authorId)
        {
                await _bookService.AddBookAsync(title, publishDate, authorId);
                return Ok("Книга успешно добавлена");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, string? title, DateOnly? publishDate, int? authorId)
        {
                await _bookService.ModifyBookAsync(id, title, publishDate, authorId);
                return Ok("Книга успешно обновлена");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
                await _bookService.DeleteBookAsync(id);
                return Ok("Книга успешно удалена");
        }

    }
}