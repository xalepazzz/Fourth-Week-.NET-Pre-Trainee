using DatabaseLayer.Models;

namespace DatabaseLayer.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetBookByIdAsync(int id);
        Task<List<Book>> GetAllBooksAsync();
        Task AddBookAsync(Book book);
        Task ModifyBookAsync(Book book);
        Task DeleteBookAsync(Book Book);
    }
}
