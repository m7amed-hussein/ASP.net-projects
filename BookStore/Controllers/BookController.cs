using BookStore.Models;
using BookStore.Models.Repositories;
using BookStore.ModelView;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBooksStoreRepository<Book> bookRepository;
        private readonly IBooksStoreRepository<Author> authorRepository;
        private readonly IHostingEnvironment hosting;

        public BookController(IBooksStoreRepository<Book> bookRepository,
            IBooksStoreRepository<Author> authorRepository,
            IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository.list();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            
            return View(getAuthors());
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorModeView model)
        {
            string fileName =fileUpload(model.File)?? string.Empty;
            
            if (!ModelState.IsValid)
            {
               // ModelState.AddModelError("", "you have to fill all the fields");
                return View(getAuthors());
            }
            try
            {
                if (model.AuthorId == -1)
                {
                    ViewBag.Message = "Please select author";
                    
                    return View(getAuthors());
                }
                var authorinmodel = authorRepository.find(model.AuthorId);
                Book book = new Book
                {
                    Id = model.Id,
                    Description = model.Discription,
                    Title = model.Title,
                    author = authorinmodel,
                    fileURL = fileName

                    
                };
                bookRepository.add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.find(id);
            var authorId = book.author == null ? book.author.ID = 0 : book.author.ID;
            var viewModel = new BookAuthorModeView
            {
                Id = book.Id,
                Title = book.Title,
                Discription = book.Description,
                AuthorId = authorId,
                Authors = fillAuthorSelect(),
                fileURL = book.fileURL
            };
            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorModeView model)
        {
            try
            {
                string fileName = fileUpload(model.File,model.fileURL)?? string.Empty;
                

                    
                   
                var authorModel = authorRepository.find(model.AuthorId);
                var book = new Book
                {
                    Id = model.Id,
                    Description = model.Discription,
                    Title = model.Title,
                    author = authorModel,
                    fileURL = fileName
                };
                bookRepository.update(book.Id, book);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepository.delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Author> fillAuthorSelect()
        {
            var authors = authorRepository.list().ToList();
            authors.Insert(0,new Author { ID = -1,FullName="--- please select author ---"});
            return authors;
        }
        BookAuthorModeView getAuthors()
        {
            var vmodel = new BookAuthorModeView
            {
                Authors = fillAuthorSelect()
            };
            return vmodel;
        }
        string fileUpload(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "Uploads");
                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));
                return file.FileName;
            }
            return null;
        }
        string fileUpload(IFormFile file,string fileUrl)
        {
            if (file != null && fileUrl != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "Uploads");
                string fullOldPath = Path.Combine(uploads, fileUrl);
                string newPath = Path.Combine(uploads, file.FileName);
                if (fullOldPath != newPath)
                {
                    System.IO.File.Delete(fullOldPath);
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }
                return file.FileName;
            }
            else if(file != null) return fileUpload(file);
            return fileUrl??string.Empty;
        }
        public ActionResult Search(string term)
        {
            var result = bookRepository.search(term);
            return View("index", result);
        }
    }
}
