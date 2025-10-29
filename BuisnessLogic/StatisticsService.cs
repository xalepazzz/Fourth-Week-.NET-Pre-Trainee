using BuisnessLogic.DTOs;
using BuisnessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuisnessLogic.Interfaces;
using System.Diagnostics.Metrics;

namespace BuisnessLogic
{
    public class StatisticsService(IAuthorService _authorService, IBookService _bookService)
    {
        public async Task<List<string>> FindAuthorsBooksCountAsync()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            var allBooks = await _bookService.GetAllBooksAsync();

            var booksByAuthor = allBooks
                .GroupBy(b => b.AuthorId)
                .ToDictionary(g => g.Key, g => g.Count());

            var result = new List<string>();

            foreach (var author in authors)
            {
                int bookCount = booksByAuthor.GetValueOrDefault(author.Id, 0);
                result.Add($"{author.Name}: {bookCount}");
            }

            return result;
        }

        public async Task<List<string>> FindBooksAuthorsAsync()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            var allBooks = await _bookService.GetAllBooksAsync();

            var booksByAuthor = allBooks
                .GroupBy(b => b.AuthorId)
                .ToDictionary(g => g.Key, g => g.Count());

            var result = new List<string>();

            foreach (var book in allBooks)
            {
                string bookAuthor = authors.First(a => a.Id == book.AuthorId).Name;
                if (bookAuthor == null)
                    bookAuthor = "отсутствует";
                result.Add($"{book.Title} – {bookAuthor}");
            }
            return result;
        }

        public async Task<List<BookDTO>> FindBookByNameAsync(string request)
        {
            if (string.IsNullOrEmpty(request))
                throw new ArgumentException("Критерий для поиска должен быть указан");

            List<BookDTO> books = (await _bookService.GetAllBooksAsync()).Where(b => b.Title.ToLower().Contains(request.ToLower())).ToList();
            if (books.Count == 0)
                throw new ArgumentException("Книги не существует");
            return books;
        }

        public async Task<List<AuthorDTO>> FindAuthorByNameAsync(string request)
        {
            if (string.IsNullOrEmpty(request))
                throw new ArgumentException("Критерий для поиска должен быть указан");

            List<AuthorDTO> authors = (await _authorService.GetAllAuthorsAsync()).Where(b => b.Name.ToLower().Contains(request.ToLower())).ToList();
            if (authors.Count == 0)
                throw new ArgumentException("Книги не существует");
            return authors;
        }
    }
}
