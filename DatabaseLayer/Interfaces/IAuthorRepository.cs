using DatabaseLayer.Models;

namespace DatabaseLayer.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author?> GetAuthorByIdAsync(int id);
        Task<List<Author>> GetAllAuthorsAsync();
        Task AddAuthorAsync(Author author);
        Task ModifyAuthorAsync(Author author);
        Task DeleteAuthorAsync(Author author);
    }
}
