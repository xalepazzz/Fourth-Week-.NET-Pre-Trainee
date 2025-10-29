using Microsoft.AspNetCore.Mvc;
using BuisnessLogic.DTOs;
using BuisnessLogic.Interfaces;

namespace thirdweek
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;
        
        public AuthorController(IAuthorService authorService, IBookService bookService) 
        { 
            _authorService = authorService; 
            _bookService = bookService; 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
                AuthorDTO author = await _authorService.GetAuthorByIdAsync(id);
                return Ok(author);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {  
                List<AuthorDTO> authors = await _authorService.GetAllAuthorsAsync();
                return Ok(authors);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(string name, DateOnly dateOfBirth)
        {
                await _authorService.AddAuthorAsync(name, dateOfBirth);
                return Ok("Автор успешно добавлен");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, string? name, DateOnly? dateOfBirth)
        {
                await _authorService.ModifyAuthorAsync(id, name, dateOfBirth);
                return Ok("Автор успешно обновлен");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
                var books = (await _bookService.GetAllBooksAsync()).Where(b => b.AuthorId == id).ToList();
                foreach (var book in books)
                {
                   await _bookService.DeleteBookAsync(book.Id);
                }

               await _authorService.DeleteAuthorAsync(id);
                return Ok("Автор и связанные книги успешно удалены");
        }
    }
}