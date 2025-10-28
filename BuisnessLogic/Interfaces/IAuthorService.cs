using DatabaseLayer.Models;

namespace BuisnessLogic.Interfaces
{
    public interface IAuthorService
    {
        Task<Author> GetAuthorByIdAsync(int id);

        Task<List<Author>> GetAllAuthorsAsync();

        Task AddAuthorAsync(string name, DateOnly dateOfBirth);

        Task ModifyAuthorAsync(int id, string? name, DateOnly? dateOfBirth);

        Task DeleteAuthorAsync(int id);
    }
}
