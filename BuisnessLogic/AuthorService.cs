using BuisnessLogic.Interfaces;
using DatabaseLayer.Interfaces;
using DatabaseLayer.Models;

namespace BuisnessLogic;

public class AuthorService(IAuthorRepository repository) : IAuthorService
{
    public async Task<Author> GetAuthorByIdAsync(int id)
    {
        if (id == 0)
            throw new ArgumentException("ID автора не может быть пустым");

        var author = await repository.GetAuthorByIdAsync(id);
        if (author == null)
            throw new ArgumentException($"Автор с ID {id} не найден");
        return author;
    }

    public async Task<List<Author>> GetAllAuthorsAsync()
    {
        return await repository.GetAllAuthorsAsync();
    }

    public async Task AddAuthorAsync(string name, DateOnly dateOfBirth)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Имя автора не может быть пустым");
        
        if (dateOfBirth == default(DateOnly))
            throw new ArgumentException("Дата рождения должна быть указана");

        if (dateOfBirth > DateOnly.FromDateTime(DateTime.Now))
            throw new ArgumentException("Дата рождения не может быть в будущем");

        var author = new Author()
        {
            Name = name.Trim(),
            DateOfBirth = dateOfBirth
        };
        
        await repository.AddAuthorAsync(author);
    }

    public async Task ModifyAuthorAsync(int id, string? name, DateOnly? dateOfBirth)
    {
        if (id == 0)
            throw new ArgumentException("ID автора не может быть пустым");

        var existingAuthor = await GetAuthorByIdAsync(id);
        
        string updatedName = existingAuthor.Name;
        DateOnly updatedDateOfBirth = existingAuthor.DateOfBirth;

        if (name != null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя автора не может быть пустым");
            updatedName = name.Trim();
        }
        
        if (dateOfBirth.HasValue)
        {
            if (dateOfBirth.Value == default(DateOnly))
                throw new ArgumentException("Дата рождения должна быть указана");
                
            if (dateOfBirth.Value > DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("Дата рождения не может быть в будущем");
            updatedDateOfBirth = dateOfBirth.Value;
        }

        var updatedAuthor = new Author()
        {
            Id = id,
            Name = updatedName,
            DateOfBirth = updatedDateOfBirth
        };
        
        await repository.ModifyAuthorAsync(updatedAuthor);
    }

    public async Task DeleteAuthorAsync(int id)
    {
        if (id == 0)
            throw new ArgumentException("ID автора должен быть пустым");

        var author = await GetAuthorByIdAsync(id);
        if (author == null)
            throw new ArgumentException("Автора не существует");
        await repository.DeleteAuthorAsync(author);
    }
}