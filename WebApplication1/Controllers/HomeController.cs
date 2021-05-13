using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private BooksDBContext context;

        public HomeController(BooksDBContext db)
        {
            context = db;
        }

        public IActionResult Index()
        {
            var books = context.Books.Include(o => o.Author);
            return View(books);
        }

        public IActionResult ShowAuthors()
        {
            var authors = context.Authors.Include(o => o.Books);
            return View(authors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Book newBook = new Book();
            return View(newBook);
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (context.Books.Contains(book))
                    {
                        context.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    else
                    {
                        context.Books.Add(book);
                    }
                    context.SaveChanges();
                }
                catch
                {
                    return View();
                }
                return RedirectToAction("Index");
            }
            else
                return View();
        }

        [HttpGet]
        public IActionResult CreateAuthor()
        {
            Author newAuthor = new Author();
            return View(newAuthor);
        }

        [HttpPost]
        public IActionResult CreateAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (context.Authors.Contains(author))
                    {
                        context.Entry(author).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    else
                    {
                        context.Authors.Add(author);
                    }
                    context.SaveChanges();
                }
                catch
                {
                    return View();
                }
                return RedirectToAction("ShowAuthors");
            }
            else
                return View();
        }

        public IActionResult Edit(Book item)
        {
            return View("Create", item);
        }

        public IActionResult EditAuthor(Author item)
        {
            return View("CreateAuthor", item);
        }

        public IActionResult Delete(Book item)
        {
            context.Books.Remove(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAuthor(Author item)
        {
            context.Authors.Remove(item);
            context.SaveChanges();
            return RedirectToAction("ShowAuthors");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
