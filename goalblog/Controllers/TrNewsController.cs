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
    public class TrNewsController : Controller
    {
        private NewsContext db = new NewsContext();

        // GET: TrNews
        public async Task<ActionResult> Index(int page = 1, int Id = 0)
        {
            if (Id == 0)
            {
                int pageSize = 4; // количество объектов на страницу
                IEnumerable<Class> s = await db.Classes.ToListAsync();
                IEnumerable<TrNews> NewsPerPages = db.TrNewses.ToList().Skip((page - 1) * pageSize).Take(pageSize);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = db.TrNewses.ToList().Count };
                IEnumerable<Class> casses = await db.Classes.ToListAsync();
                TrIndexViewModel ivm = new TrIndexViewModel { PageInfo = pageInfo, fews = NewsPerPages, classes = casses };
                return View(ivm);
            }
            else
            {
                int pageSize = 4; // количество объектов на страницу
                Class cs = db.Classes.Include(sq => sq.TrNewses).FirstOrDefault(i => i.Id == Id);
                if (cs == null || cs.TrNewses.ToList().Count == 0)
                {
                    return HttpNotFound();
                }
                IEnumerable<Class> s = await db.Classes.ToListAsync();
                IEnumerable<TrNews> NewsPerPages = cs.TrNewses.ToList().Skip((page - 1) * pageSize).Take(pageSize);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = cs.TrNewses.ToList().Count };
                TrIndexViewModel ivm = new TrIndexViewModel { PageInfo = pageInfo, fews = NewsPerPages, classes = s };
                return View(ivm);
            }
        }

        // GET: TrNews/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrNews trNews = await db.TrNewses.FindAsync(id);
            if (trNews == null)
            {
                return HttpNotFound();
            }
            return View(trNews);
        }

        // GET: TrNews/Create
        public ActionResult Create()
        {
            ViewBag.Class = db.Classes.ToList();
            return View();
        }

        // POST: TrNews/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(TrNews trNews,HttpPostedFileBase upload, int[] selectClasses)
        {
            byte[] imageData = null;
            using (var binaryReader = new BinaryReader(upload.InputStream))
            {
                imageData = binaryReader.ReadBytes(upload.ContentLength);
            }
            trNews.View = imageData;
            trNews.date = DateTime.Now;
            for (int i = 0; i < selectClasses.Length; i++)
            {
                Class c = db.Classes.Find(selectClasses[i]);
                trNews.Class.Add(c);
                c.TrNewses.Add(trNews);
                db.Entry(c).State = EntityState.Modified;
            }
            //var cl = db.Classes.Include
            db.TrNewses.Add(trNews);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: TrNews/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrNews trNews = await db.TrNewses.FindAsync(id);
            if (trNews == null)
            {
                return HttpNotFound();
            }
            return View(trNews);
        }

        // POST: TrNews/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Author,date,Text,View")] TrNews trNews)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trNews).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(trNews);
        }

        // GET: TrNews/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrNews trNews = await db.TrNewses.FindAsync(id);
            if (trNews == null)
            {
                return HttpNotFound();
            }
            return View(trNews);
        }

        // POST: TrNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TrNews trNews = await db.TrNewses.FindAsync(id);
            db.TrNewses.Remove(trNews);
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
