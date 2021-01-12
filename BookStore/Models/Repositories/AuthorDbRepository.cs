using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookStore.Models.Repositories
{
    public class AuthorDbRepository : IBooksStoreRepository<Author>
    {
        BooksStoreDBContext db;
        public AuthorDbRepository(BooksStoreDBContext _db)
        {
            db = _db;

        }

        public void add(Author entity)
        {
            db.Authors.Add(entity);
            db.SaveChanges();
        }

        public void delete(int id)
        {
            var author = find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public Author find(int id)
        {
            var author = db.Authors.SingleOrDefault(o => o.ID == id);
            return author;
        }

        public IList<Author> list()
        {
            return db.Authors.ToList();
        }

        public List<Author> search(string term)
        {
            return db.Authors.Where(a => a.FullName.Contains(term)).ToList();

        }

        public void update(int id, Author entity)
        {
            db.Update(entity);
            db.SaveChanges();
        }
    }
}

