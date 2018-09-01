using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using goalblog.Models;
using System.IO;

namespace goalblog.Controllers
{
    public class ChWorldsController : Controller
    {
        private NewsContext db = new NewsContext();

        // GET: ChWorlds
        public async Task<ActionResult> Index()
        {
            ViewBag.gr = await db.gropes.ToListAsync();
            var gr = db.gropes.Include(f => f.chw);
            return View(gr);
        }

        // GET: ChWorlds/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.gr = await db.gropes.ToListAsync();
            Grope gr = db.gropes.Include(f => f.chw).FirstOrDefault(i => i.Id == id);
            if (gr == null)
            {
                return HttpNotFound();
            }
            return View(gr);
        }

        // GET: ChWorlds/Create
        public ActionResult Create()
        {
            SelectList sl =  new SelectList(db.gropes, "Id", "Name");
            ViewBag.gr = sl;
            return View();
        }

        // POST: ChWorlds/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Create(ChWorld chWorld, HttpPostedFileBase upload)
        {
            byte[] imageData = null;
            using (var binaryReader = new BinaryReader(upload.InputStream))
            {
                imageData = binaryReader.ReadBytes(upload.ContentLength);
            }
            chWorld.Icon = imageData;
            db.chWorlds.Add(chWorld);
                await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: ChWorlds/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChWorld chWorld = await db.chWorlds.FindAsync(id);
            if (chWorld == null)
            {
                return HttpNotFound();
            }
            return View(chWorld);
        }

        // POST: ChWorlds/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ChWorld chWorld)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chWorld).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(chWorld);
        }

        // GET: ChWorlds/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChWorld chWorld = await db.chWorlds.FindAsync(id);
            if (chWorld == null)
            {
                return HttpNotFound();
            }
            return View(chWorld);
        }

        // POST: ChWorlds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ChWorld chWorld = await db.chWorlds.FindAsync(id);
            db.chWorlds.Remove(chWorld);
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
