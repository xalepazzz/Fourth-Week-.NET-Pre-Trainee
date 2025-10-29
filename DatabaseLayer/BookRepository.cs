using DatabaseLayer.Interfaces;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer
{
    public class BookRepository(AppDBContext dbContext) : IBookRepository
    {

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await dbContext.Books.ToListAsync();
        }
        public async Task AddBookAsync(Book book)
        {
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
        }
        public async Task ModifyBookAsync(Book newBook)
        {
            Book book = await GetBookByIdAsync(newBook.Id);
            book.Title = newBook.Title;
            book.PublishDate = newBook.PublishDate;
            book.AuthorId = newBook.AuthorId;
            await dbContext.SaveChangesAsync();
        }
        public async Task DeleteBookAsync(Book book)
        {
            dbContext.Books.Remove(book);
            await dbContext.SaveChangesAsync();
        }

    }
}
