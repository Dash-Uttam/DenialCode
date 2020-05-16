using DanialProject.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DanialProject.Controllers
{
    [Authorize(Roles = "Features")]
    public class FeaturesController : Controller
    {
        private readonly AdminContext db = new AdminContext();

        public FeaturesController()
        {
        }

        public ActionResult Create()
        {
            return base.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Icon,ListingType,IconType,File")] Features features)
        {
            FeaturesController featuresController = this;
            if (featuresController.ModelState.IsValid)
            {
                try
                {
                    HttpPostedFileBase file = features.File;
                    string[] strArray = file.FileName.Split('.');
                    string str1 = "." + strArray[strArray.Length - 1];
                    string str2 = DateTime.Now.ToString().Replace(" ", string.Empty).Replace("/", string.Empty).Replace(":", string.Empty);
                    if (file != null)
                    {
                        WebImage webImage = new WebImage(file.InputStream);
                        string str3 = "../../Content/Images/Features/" + strArray[0] + str2 + str1.ToString();
                        webImage.Save("~\\Content\\Images\\Features\\" + strArray[0] + str2 + str1, (string)null, true);
                        features.Icon = str3;
                        featuresController.db.Features.Add(features);
                        int num = await featuresController.db.SaveChangesAsync();
                        return (ActionResult)featuresController.RedirectToAction("Index");
                    }
                    featuresController.db.Features.Add(features);
                    int num1 = await featuresController.db.SaveChangesAsync();
                    return (ActionResult)featuresController.RedirectToAction("Index");
                }
                catch (Exception)
                {
                    featuresController.db.Features.Add(features);
                    int num = await featuresController.db.SaveChangesAsync();
                    return (ActionResult)featuresController.RedirectToAction("Index");
                }
            }
            return (ActionResult)featuresController.View((object)features);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            ActionResult httpStatusCodeResult;
            if (id.HasValue)
            {
                DbSet<Features> features1 = this.db.Features;
                object[] objArray = new object[] { id };
                Features feature = await features1.FindAsync(objArray);
                if (feature != null)
                {
                    httpStatusCodeResult = base.View(feature);
                }
                else
                {
                    httpStatusCodeResult = base.HttpNotFound();
                }
            }
            else
            {
                httpStatusCodeResult = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return httpStatusCodeResult;
        }

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DbSet<Features> features = this.db.Features;
            object[] objArray = new object[] { id };
            Features feature = await features.FindAsync(objArray);
            this.db.Features.Remove(feature);
            await this.db.SaveChangesAsync();
            return base.RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            ActionResult httpStatusCodeResult;
            if (id.HasValue)
            {
                DbSet<Features> features1 = this.db.Features;
                object[] objArray = new object[] { id };
                Features feature = await features1.FindAsync(objArray);
                if (feature != null)
                {
                    httpStatusCodeResult = base.View(feature);
                }
                else
                {
                    httpStatusCodeResult = base.HttpNotFound();
                }
            }
            else
            {
                httpStatusCodeResult = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return httpStatusCodeResult;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            ActionResult httpStatusCodeResult;
            if (id.HasValue)
            {
                DbSet<Features> features1 = this.db.Features;
                object[] objArray = new object[] { id };
                Features feature = await features1.FindAsync(objArray);
                ((dynamic)base.ViewBag).Listing = feature.ListingType;
                ((dynamic)base.ViewBag).Icon = feature.IconType;
                if (feature != null)
                {
                    httpStatusCodeResult = base.View(feature);
                }
                else
                {
                    httpStatusCodeResult = base.HttpNotFound();
                }
            }
            else
            {
                httpStatusCodeResult = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return httpStatusCodeResult;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Icon,ListingType,IconType,File")] Features features)
        {
            FeaturesController featuresController = this;
            if (featuresController.ModelState.IsValid)
            {
                try
                {
                    HttpPostedFileBase file = features.File;
                    string[] strArray = file.FileName.Split('.');
                    string str1 = "." + strArray[strArray.Length - 1];
                    string str2 = DateTime.Now.ToString().Replace(" ", string.Empty).Replace("/", string.Empty).Replace(":", string.Empty);
                    if (file != null)
                    {
                        WebImage webImage = new WebImage(file.InputStream);
                        string str3 = "../../Content/Images/Features/" + strArray[0] + str2 + str1.ToString();
                        webImage.Save("~\\Content\\Images\\Features\\" + strArray[0] + str2 + str1, (string)null, true);
                        features.Icon = str3;
                        featuresController.db.Entry<Features>(features).State = EntityState.Modified;
                        int num = await featuresController.db.SaveChangesAsync();
                        return (ActionResult)featuresController.RedirectToAction("Index");
                    }
                    featuresController.db.Entry<Features>(features).State = EntityState.Modified;
                    int num1 = await featuresController.db.SaveChangesAsync();
                    return (ActionResult)featuresController.RedirectToAction("Index");
                }
                catch (Exception)
                {
                    featuresController.db.Entry<Features>(features).State = EntityState.Modified;
                    int num = await featuresController.db.SaveChangesAsync();
                    return (ActionResult)featuresController.RedirectToAction("Index");
                }
            }
            return (ActionResult)featuresController.View((object)features);
        }

        public async Task<ActionResult> Index()
        {
            List<Features> listAsync = await this.db.Features.ToListAsync<Features>();
            return base.View(listAsync);
        }
    }
}