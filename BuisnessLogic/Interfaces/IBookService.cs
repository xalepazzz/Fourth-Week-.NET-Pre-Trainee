using DatabaseLayer.Interfaces;
using DatabaseLayer.Models;

namespace BuisnessLogic.Interfaces
{
    public interface IBookService
    {
        Task<Book> GetBookByIdAsync(int id);

        Task<List<Book>> GetAllBooksAsync();

        Task AddBookAsync(string title, DateOnly publishDate, int authorId);

        Task ModifyBookAsync(int id, string? title, DateOnly? publishDate, int? authorId);

        Task DeleteBookAsync(int id);
    }
}
