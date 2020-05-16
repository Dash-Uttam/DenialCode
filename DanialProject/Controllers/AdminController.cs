using DanialProject.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DanialProject.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        private readonly AdminContext db = new AdminContext();

        public AdminController()
        {
        }

        [Authorize(Roles = "Admins")]
        public ActionResult Admins()
        {
            return View();
        }

        [Authorize(Roles = "Agents")]
        public ActionResult Agents()
        {
            return RedirectToAction("Index", "Agents");
        }

        [Authorize(Roles = "Approved")]
        public async Task<ActionResult> Approved()
        {
            DbSet<Buildings> buildings = this.db.Buildings;
            List<Buildings> listAsync = await (
                from x in buildings
                where x.Status.Equals("Approved")
                select x).ToListAsync<Buildings>();
            return View(listAsync);
        }

        [Authorize(Roles = "Features")]
        public ActionResult Features()
        {
            return RedirectToAction("Index", "Features");
        }

        [Authorize(Roles = "Pending")]
        public async Task<ActionResult> Pending()
        {
            DbSet<Buildings> buildings = this.db.Buildings;
            List<Buildings> listAsync = await (
                from x in buildings
                where x.Status.Equals("Pending")
                select x).ToListAsync<Buildings>();
            return View(listAsync);
        }

        [Authorize(Roles = "Rejected")]
        public async Task<ActionResult> Rejected()
        {
            DbSet<Buildings> buildings = this.db.Buildings;
            List<Buildings> listAsync = await (
                from x in buildings
                where x.Status.Equals("Rejected")
                select x).ToListAsync<Buildings>();
            return View(listAsync);
        }

        [Authorize(Roles = "Roles")]
        public ActionResult Roles()
        {
            return View();
        }

        [Authorize(Roles = "Approved")]
        public async Task<ActionResult> SaleListing(int id)
        {
            ((dynamic)base.ViewBag).Default = 0;
            dynamic viewBag = base.ViewBag;
            List<Agents> listAsync = await this.db.Agents.ToListAsync<Agents>();
            viewBag.Agents = listAsync;
            viewBag = null;
            viewBag = base.ViewBag;
            DbSet<Buildings> buildings = this.db.Buildings;
            Buildings building = await buildings.FirstOrDefaultAsync<Buildings>((Buildings x) => x.ID == id);
            viewBag.Listing = building;
            viewBag = null;
            List<Features> features = await this.db.Features.ToListAsync<Features>();
            return View(features);
        }

        [Authorize(Roles = "Approved")]
        public async Task<ActionResult> UnitListing(int id)
        {
            DbSet<Buildings> buildings = this.db.Buildings;
            Buildings building = await buildings.FirstOrDefaultAsync<Buildings>((Buildings x) => x.ID == id);
            return View(building);
        }

        public ActionResult WelcomePage()
        {
            return View();
        }
    }
}