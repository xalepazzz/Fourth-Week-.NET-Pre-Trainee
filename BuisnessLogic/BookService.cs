using BuisnessLogic.Interfaces;
using DatabaseLayer.Interfaces;
using DatabaseLayer.Models;

namespace BuisnessLogic;

public class BookService(IBookRepository repository) : IBookService
{
    public async Task<Book> GetBookByIdAsync(int id)
    {
        if (id == 0)
            throw new ArgumentException("ID книги должен быть указан");

        var book = await repository.GetBookByIdAsync(id);
        if (book == null)
            throw new ArgumentException($"Книга с ID {id} не найдена");
        return book;
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await repository.GetAllBooksAsync();
    }

    public async Task AddBookAsync(string title, DateOnly publishDate, int authorId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Название книги не может быть пустым");

        if (publishDate == default(DateOnly))
            throw new ArgumentException("Дата публикации должна быть указана");

        if (publishDate > DateOnly.FromDateTime(DateTime.Now))
            throw new ArgumentException("Дата публикации не может быть в будущем");

        if (authorId == 0)
            throw new ArgumentException("ID автора должен быть указан");

        var book = new Book { Title = title.Trim(), AuthorId = authorId, PublishDate = publishDate };
        await repository.AddBookAsync(book);
    }

    public async Task ModifyBookAsync(int id, string? title, DateOnly? publishDate, int? authorId)
    {
        if (id == 0)
            throw new ArgumentException("ID книги должен быть указан");

        var existingBook = await GetBookByIdAsync(id);
        
        string updatedTitle = existingBook.Title;
        DateOnly updatedPublishDate = existingBook.PublishDate;
        int updatedAuthorId = existingBook.AuthorId;

        if (title != null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Название книги не может быть пустым");
            updatedTitle = title.Trim();
        }

        if (publishDate.HasValue)
        {
            if (publishDate.Value == default(DateOnly))
                throw new ArgumentException("Дата публикации должна быть указана");
                
            if (publishDate.Value > DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("Дата публикации не может быть в будущем");
            updatedPublishDate = publishDate.Value;
        }
        
        if (authorId.HasValue)
        {
            if (authorId.Value == 0)
                throw new ArgumentException("ID автора должен быть указан");
            updatedAuthorId = authorId.Value;
        }

        var updatedBook = new Book()
        {
            Id = id,
            Title = updatedTitle,
            PublishDate = updatedPublishDate,
            AuthorId = updatedAuthorId
        };
        
        await repository.ModifyBookAsync(updatedBook);
    }

    public async Task DeleteBookAsync(int id)
    {
        if (id == 0)
            throw new ArgumentException("ID книги должен быть указан");

        var book = await GetBookByIdAsync(id);
        if (book == null)
            throw new ArgumentException("Книги не существует");
        await repository.DeleteBookAsync(book);
    }
}