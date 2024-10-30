using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using u23668475_HW03.Models;
using System.Web.UI;
using PagedList;
using System.Drawing.Printing;
using Newtonsoft.Json;



namespace u23668475_HW03.Controllers
{
    public class HomeController : Controller
    {
        private LibraryEntities db = new LibraryEntities();
        public async Task<ActionResult> Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            using (var dbContext = new LibraryEntities())
            {
                var studentsList = await dbContext.students.OrderBy(s => s.name).ToListAsync();
                var pagedStudents = studentsList.ToPagedList(pageNumber, pageSize);

                var BooksList = await dbContext.books.OrderBy(s => s.name).ToListAsync();
                var pagedBooks = BooksList.ToPagedList(pageNumber, pageSize);

                var combinedViewModel = new CombinedViewModel
                {
                    Students = pagedStudents,
                    Books = pagedBooks,
                    Borrows = await dbContext.borrows.ToListAsync(),
                    Authors = await dbContext.authors.ToListAsync(),
                    Types = await dbContext.types.ToListAsync()
                };
                return View(combinedViewModel);
            }
                
        }
        public async Task<ActionResult> Maintain(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            using (var dbContext = new LibraryEntities())
            {
                var AuthorsList = await dbContext.authors.OrderBy(s => s.name).ToListAsync();
                var pagedAuthors = AuthorsList.ToPagedList(pageNumber, pageSize);


                var BorrowsList = await dbContext.borrows.ToListAsync();
                var pagedBorrows = BorrowsList.ToPagedList(pageNumber, pageSize);


                var TypesList = await dbContext.types.OrderBy(s => s.name).ToListAsync();
                var pagedTypes = TypesList.ToPagedList(pageNumber, pageSize);

                var combinedViewModel = new CombinedViewModel
                {
                    Students = await dbContext.students.ToListAsync(),
                    Books = await dbContext.books.ToListAsync(),
                    Borrows = pagedBorrows,
                    Authors = pagedAuthors,
                    Types = pagedTypes
                };
                return View(combinedViewModel);
            }
        }



        public async Task<ActionResult> Reports()
        {
            using (var dbContext = new LibraryEntities())
            {
                var combinedViewModel = new CombinedViewModel
                {
                    Students = await dbContext.students.Include(s => s.borrows).ToListAsync(),
                    Books = await dbContext.books.ToListAsync(),
                    Borrows = await dbContext.borrows.ToListAsync(),
                    Authors = await dbContext.authors.Include(a => a.books).ToListAsync(),
                    Types = await dbContext.types.ToListAsync()
                };

                // Calculate the top loaned books by grouping borrows by bookId and counting
                var topLoanedBooks = combinedViewModel.Borrows
                    .GroupBy(b => b.bookId)
                    .Select(g => new
                    {
                        BookId = g.Key,
                        BookName = combinedViewModel.Books.FirstOrDefault(b => b.bookId == g.Key)?.name,
                        BorrowCount = g.Count()
                    })
                    .OrderByDescending(b => b.BorrowCount)
                    .Take(5) // Top 5 most borrowed books
                    .ToList();

                ViewBag.TopLoanedBooks = topLoanedBooks;
                ViewBag.Message = "Generate Reports";

                return View(combinedViewModel);
            }
        }

    }
}