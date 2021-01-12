using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Repositories;

namespace BookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBooksStoreRepository<Author> authorRepository;

        public AuthorController(IBooksStoreRepository<Author>authorRepository)
        {
            this.authorRepository = authorRepository;
        }
        // GET: HomeController
        public ActionResult Index()
        {
            var authors = authorRepository.list();
            return View(authors);
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            var author = authorRepository.find(id);
            return View(author);
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            if (!ModelState.IsValid)
            {
               // ModelState.AddModelError("", "you have to fill all the fields");
                return View();
            }
            try
            {
                authorRepository.add(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = authorRepository.find(id);
            return View(author);
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            try
            {
                authorRepository.update(id, author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            var author = authorRepository.find(id);

            return View(author);
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try
            {
                authorRepository.delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
    }
}
