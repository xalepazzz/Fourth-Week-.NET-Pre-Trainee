using DatabaseLayer.Interfaces;
using BuisnessLogic.DTOs;

namespace BuisnessLogic.Interfaces
{
    public interface IBookService
    {
        Task<BookDTO> GetBookByIdAsync(int id);

        Task<List<BookDTO>> GetAllBooksAsync();

        Task AddBookAsync(string title, DateOnly publishDate, int authorId);

        Task ModifyBookAsync(int id, string? title, DateOnly? publishDate, int? authorId);

        Task DeleteBookAsync(int id);
    }
}
