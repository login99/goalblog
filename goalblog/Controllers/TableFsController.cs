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
using System.IO;
using goalblog.Models;

namespace goalblog.Controllers
{
    public class TableFsController : Controller
    {
        private NewsContext db = new NewsContext();

        // GET: TableFs
        public async Task<ActionResult> Index(int Id = 6)
        {
                TableClassView tv = new TableClassView();
                Class cs = db.Classes.Include(c => c.Tableses).FirstOrDefault(i => i.Id == Id);
            if (cs == null || cs.Tableses.ToList().Count == 0)
            {
                return HttpNotFound();
            }
            ViewBag.Cs = cs;
                tv.tables = cs.Tableses.ToList();
                tv.classes = await db.Classes.ToListAsync();
                return View(tv);   
        }

        // GET: TableFs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableF tableF = await db.Table.FindAsync(id);
            if (tableF == null)
            {
                return HttpNotFound();
            }
            return View(tableF);
        }

        // GET: TableFs/Create
        public async Task<ActionResult> Create()
        {
            TableClass tb = new TableClass();
            tb.classes = await db.Classes.ToListAsync();
            return View(tb);
        }

        // POST: TableFs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create( TableClass tb, HttpPostedFileBase upload, int[] selectClasses)
        {
            if (upload != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(upload.InputStream))
                {
                    imageData = binaryReader.ReadBytes(upload.ContentLength);
                }
                tb.tables.Icon = imageData;
                for (int i = 0; i < selectClasses.Length; i++)
                {
                    Class c = db.Classes.Find(selectClasses[i]);
                    tb.tables.Classes.Add(c);
                    c.Tableses.Add(tb.tables);
                    db.Entry(c).State = EntityState.Modified;
                }
                //var cl = db.Classes.Include
                db.Table.Add(tb.tables);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: TableFs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableF tableF = await db.Table.FindAsync(id);
            if (tableF == null)
            {
                return HttpNotFound();
            }
            return View(tableF);
        }

        // POST: TableFs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Icon,Name,Game,WinGame,DrawGame,LoseGame,Goal,MissGoal,Score")] TableF tableF)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tableF).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tableF);
        }

        // GET: TableFs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableF tableF = await db.Table.FindAsync(id);
            if (tableF == null)
            {
                return HttpNotFound();
            }
            return View(tableF);
        }

        // POST: TableFs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TableF tableF = await db.Table.FindAsync(id);
            db.Table.Remove(tableF);
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
