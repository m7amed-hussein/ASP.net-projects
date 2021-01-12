using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class BookDbRepository : IBooksStoreRepository<Book>
    {
        BooksStoreDBContext db;
        public BookDbRepository(BooksStoreDBContext _db)
        {
            db = _db;

        }

        public void add(Book entity)
        {
            db.Books.Add(entity);
            db.SaveChanges();
        }

        public void delete(int id)
        {
            var book = find(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book find(int id)
        {
            var book = db.Books.Include(a => a.author).SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> list()
        {
            return db.Books.Include(a=>a.author).ToList();
        }

        public void update(int id, Book entity)
        {
            db.Update(entity);
            db.SaveChanges();
        }

        public List<Book>search(string term)
        {
            var result = db.Books.Include(a => a.author)
                .Where(a => a.Title.Contains(term) ||
                a.Description.Contains(term) ||
                a.author.FullName.Contains(term)).ToList();
            return result;
        }
    }
}
