using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookStore.Models.Repositories
{
    public class AuthorRepository : IBooksStoreRepository<Author>
    {
        List<Author> authors;
        public AuthorRepository()
        {
            authors = new List<Author>()
            {
                new Author{ID=1,FullName="Mostafa Mohamed"},
                new Author{ID=2,FullName="Mohamed Mostafa"},
                new Author{ID=3,FullName="MOhamed Mumdoh"}

            };
        }

        public void add(Author entity)
        {
            entity.ID = authors.Max(b => b.ID) + 1;

            authors.Add(entity);
        }

        public void delete(int id)
        {
            var author = find(id);
            authors.Remove(author);
        }

        public Author find(int id)
        {
            var author = authors.SingleOrDefault(o => o.ID == id);
            return author;
        }

        public IList<Author> list()
        {
            return authors;
        }

        public List<Author> search(string term)
        {
            return authors.Where(a => a.FullName.Contains(term)).ToList();
        }

        public void update(int id, Author entity)
        {
            var book = find(id);
            book.FullName = entity.FullName;
        }
    }
}

