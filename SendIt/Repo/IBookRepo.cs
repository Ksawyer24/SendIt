using SendIt.Models;

namespace SendIt.Repo
{
    public interface IBookRepo
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book> GetBookAsync(long id);

        Task<Book> AddAllBooksAsync(Book book);

        //Task<Book> UpdateBookAsync(long id, Book book);

        Task <Book>DeleteBookAsync(long id);

    }
}
