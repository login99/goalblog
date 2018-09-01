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
    public class AboutSiteController : Controller
    {
        // GET: AbotSite
        NewsContext db = new NewsContext();
        public async Task<ActionResult> Index()
        {
            ViewBag.menu = await db.Classes.ToListAsync();
            return View();
        }
    }
}