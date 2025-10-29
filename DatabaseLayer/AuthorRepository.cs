using System.Data.SqlTypes;
using System.Threading.Tasks;
using DatabaseLayer.Interfaces;
using DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer
{
    public class AuthorRepository : IAuthorRepository
    {
        private AppDBContext DbContext;
        public AuthorRepository(AppDBContext dbContext) 
        {
            DbContext = dbContext;
        }
        
        public async Task<Author?> GetAuthorByIdAsync(int id) 
        {

            return await DbContext.Authors.Include(a => a.Books ).FirstOrDefaultAsync(i => i.Id == id );
        }
        public async Task<List<Author>> GetAllAuthorsAsync() 
        { 
            return await DbContext.Authors.ToListAsync();
        }
        public async Task AddAuthorAsync(Author author) 
        {   
            await DbContext.AddAsync(author);
            await DbContext.SaveChangesAsync();
        }
        public async Task ModifyAuthorAsync( Author author) 
        {
            Author _author = await GetAuthorByIdAsync(author.Id);
            _author.Name = author.Name;
            _author.DateOfBirth = author.DateOfBirth;
            await DbContext.SaveChangesAsync();
        }
        public  async Task DeleteAuthorAsync(Author author) 
        {
            
            DbContext.Authors.Remove(author);
            await DbContext.SaveChangesAsync();
        }
    }
}
