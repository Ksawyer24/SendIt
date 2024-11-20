using Microsoft.EntityFrameworkCore;
using SendIt.Data;
using SendIt.Models;

namespace SendIt.Repo
{
    public class BookRepo : IBookRepo
    {
        private readonly SendItDbContext sendItDbContext;

        public BookRepo(SendItDbContext sendItDbContext)
        {
            this.sendItDbContext = sendItDbContext;
        }

        public async Task<Book> AddAllBooksAsync(Book book)
        {
            await sendItDbContext.AddAsync(book);
            await sendItDbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book> DeleteBookAsync(long id)
        {
            var existing = await sendItDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (existing == null)
            {
                return null;

            }

            sendItDbContext.Books.Remove(existing);
            await sendItDbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await sendItDbContext.Books.ToListAsync();
        }

        public async Task<Book> GetBookAsync(long id)
        {
            return await sendItDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }



        //public Task<Book> UpdateBookAsync(long id, Book book)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
