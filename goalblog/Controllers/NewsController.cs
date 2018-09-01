using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using WebApplication1.Models;
using goalblog.Models;

namespace goalblog.Controllers
{
    public class NewsController : Controller
    {
        private NewsContext db = new NewsContext();

        // GET: News
        public async Task<ActionResult> Index(int page = 1,int Id = 0)
        {
            
            if (Id == 0)
            {
                int pageSize = 4; // количество объектов на страницу
                IEnumerable<Class> s = await db.Classes.ToListAsync();
                IEnumerable<New> NewsPerPages = db.News.OrderByDescending(i=>i.date).ToList().Skip((page - 1) * pageSize).Take(pageSize);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = db.News.ToList().Count };
                IEnumerable<Class> casses = await db.Classes.ToListAsync();
                IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, fews = NewsPerPages, classes = casses};
                return View(ivm);
            }
            else
            {
                int pageSize = 4; // количество объектов на страницу
                Class cs = db.Classes.Include(sq=>sq.News).FirstOrDefault(i=>i.Id == Id);
                ViewBag.head = cs.Name.ToUpper();
                if(cs == null || cs.News.ToList().Count ==0)
                {
                    return HttpNotFound();
                }
                IEnumerable<Class> s = await db.Classes.ToListAsync();
                IEnumerable<New> NewsPerPages = cs.News.OrderByDescending(i => i.date).ToList().Skip((page - 1) * pageSize).Take(pageSize);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = cs.News.ToList().Count };
                IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, fews = NewsPerPages, classes = s };
                return View(ivm);
            }
        }

        public async Task<ActionResult> Create(string password = "")
        {
            if (password != "word")
                return RedirectToAction("Index");
            ViewBag.Class = await db.Classes.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(New @new, HttpPostedFileBase upload,int[] selectClasses)
        {
            if (upload != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(upload.InputStream))
                {
                    imageData = binaryReader.ReadBytes(upload.ContentLength);
                }
                @new.View = imageData;
                @new.date = DateTime.Now;
                for (int i = 0; i < selectClasses.Length; i++)
                {
                    Class c = db.Classes.Find(selectClasses[i]);
                    @new.Class.Add(c);
                    c.News.Add(@new);
                    db.Entry(c).State = EntityState.Modified;
                }
                //var cl = db.Classes.Include
                db.News.Add(@new);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View();
        }


        // GET: News/Delete/5
        public async Task<ActionResult> Delete(int? id,string password = "")
        {
            if (password != "word")
                return RedirectToAction("Index");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            New @new = await db.News.FindAsync(id);
            if (@new == null)
            {
                return HttpNotFound();
            }
            return View(@new);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            New @new = await db.News.FindAsync(id);
            db.News.Remove(@new);
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
