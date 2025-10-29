using BuisnessLogic.DTOs;


namespace BuisnessLogic.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorDTO> GetAuthorByIdAsync(int id);

        Task<List<AuthorDTO>> GetAllAuthorsAsync();

        Task AddAuthorAsync(string name, DateOnly dateOfBirth);

        Task ModifyAuthorAsync(int id, string? name, DateOnly? dateOfBirth);

        Task DeleteAuthorAsync(int id);
    }
}
