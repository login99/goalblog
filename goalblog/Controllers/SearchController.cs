using goalblog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace goalblog.Controllers
{
    public class SearchController : Controller
    {
        private NewsContext db = new NewsContext();

        // GET: Search
        public async Task<ActionResult> Index()
        {
            ViewBag.menu = await db.Classes.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Result(string search, int page = 1, int Id = 0)
        {
            if (Id == 0)
            {
                int pageSize = 4; // количество объектов на страницу
                IEnumerable<Class> s = await db.Classes.ToListAsync();
                IEnumerable<New> NewsPerPages = db.News.Where(n => n.Title.Contains(search)).ToList().Skip((page - 1) * pageSize).Take(pageSize);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = db.News.ToList().Count };
                IEnumerable<Class> casses = await db.Classes.ToListAsync();
                IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, fews = NewsPerPages, classes = casses };
                return View(ivm);
            }
            else
            {
                int pageSize = 4; // количество объектов на страницу
                Class cs = db.Classes.Include(sq => sq.News).FirstOrDefault(i => i.Id == Id);
                if (cs == null || cs.News.ToList().Count == 0)
                {
                    return HttpNotFound();
                }
                IEnumerable<Class> s = await db.Classes.ToListAsync();
                IEnumerable<New> NewsPerPages = cs.News.Where(n => n.Title.Contains(search)).ToList().Skip((page - 1) * pageSize).Take(pageSize);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = cs.News.ToList().Count };
                IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, fews = NewsPerPages, classes = s };
                return View(ivm);
            }
        }
    }
}