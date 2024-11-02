using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using u23668475_HW03.Models;

namespace u23668475_HW03.Controllers
{
    public class ChartArchivesController : Controller
    {
        private LibraryEntities db = new LibraryEntities();



        // GET: ChartArchives
        public async Task<ActionResult> Index()
        {
            return View(await db.ChartArchives.ToListAsync());
        }

        // GET: ChartArchives/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChartArchive chartArchive = await db.ChartArchives.FindAsync(id);
            if (chartArchive == null)
            {
                return HttpNotFound();
            }
            return View(chartArchive);
        }

        // GET: ChartArchives/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChartArchives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ChartID,FileName,FileType,ChartData")] ChartArchive chartArchive)
        {
            if (ModelState.IsValid)
            {
                db.ChartArchives.Add(chartArchive);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(chartArchive);
        }

        // GET: ChartArchives/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChartArchive chartArchive = await db.ChartArchives.FindAsync(id);
            if (chartArchive == null)
            {
                return HttpNotFound();
            }
            return View(chartArchive);
        }

        // POST: ChartArchives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ChartID,FileName,FileType,ChartData")] ChartArchive chartArchive)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chartArchive).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(chartArchive);
        }

        // GET: ChartArchives/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChartArchive chartArchive = await db.ChartArchives.FindAsync(id);
            if (chartArchive == null)
            {
                return HttpNotFound();
            }
            return View(chartArchive);
        }

        // POST: ChartArchives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ChartArchive chartArchive = await db.ChartArchives.FindAsync(id);
            db.ChartArchives.Remove(chartArchive);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
