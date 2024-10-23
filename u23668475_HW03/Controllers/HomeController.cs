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

                var combinedViewModel = new CombinedViewModel
                {
                    Students = pagedStudents,
                    Books = await dbContext.books.ToListAsync(),
                    Borrows = await dbContext.borrows.ToListAsync(),
                    Authors = await dbContext.authors.ToListAsync()
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