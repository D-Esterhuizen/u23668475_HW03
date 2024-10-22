using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace u23668475_HW03.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            using (var dbContext = new LibraryEntities())
            {
                 var AuthorsList = await dbContext.authors.ToListAsync();
                 var BooksList = await dbContext.books.ToListAsync();
                 var BorrowsList = await dbContext.borrows.ToListAsync();
                 var StudentsList = await dbContext.students.ToListAsync();
                 var TypeList = await dbContext.types.ToListAsync();
            }
                return View();
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