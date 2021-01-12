using System;
using System.Linq;
using System.Collections.Generic;

namespace BookStore.Models.Repositories
{
    public class BookRepository:IBooksStoreRepository<Book>
    {
        List<Book> Books;
        public BookRepository()
        {
            Books = new List<Book>
            {
                new Book
                {
                    Id=1,Title="C#",Description="some code in c#",author = new Author(),fileURL="csharp.png"
                },new Book
                {
                    Id=2,Title="Java",Description="some code in Java",author = new Author(),fileURL="java.png"
                },new Book
                {
                    Id=3,Title="C++",Description="some code in c++",author = new Author(),fileURL="cplusplus.png"
                }
            };

        }

        public void add(Book entity)
        {
            entity.Id = Books.Max(b => b.Id)+1;
            Books.Add(entity);
        }

        public void delete(int id)
        {
            var book = find(id);
            Books.Remove(book);
        }

        public Book find(int id)
        {
            var book = Books.SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> list()
        {
            return Books;
        }

        public List<Book> search(string term)
        {
            return Books.Where(a => a.Title.Contains(term)).ToList();
        }

        public void update(int id,Book entity)
        {
            var book = find(id);
            book.Title = entity.Title;
            book.Description = entity.Description;
            book.author = entity.author;
            book.fileURL = entity.fileURL;
        }
    }
}
