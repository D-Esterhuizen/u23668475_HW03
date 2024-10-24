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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}