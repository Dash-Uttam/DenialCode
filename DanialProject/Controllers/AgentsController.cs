using DanialProject.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace DanialDanialProject.Controllers
{
    [Authorize]
    public class AgentsController : Controller
    {
        private AdminContext db = new AdminContext();

        public AgentsController()
        {
        }

        public async Task<ActionResult> AgentsDelete(int? id)
        {
            ActionResult httpStatusCodeResult;
            if (id.HasValue)
            {
                DbSet<Agents> agents1 = this.db.Agents;
                object[] objArray = new object[] { id };
                Agents agent = await agents1.FindAsync(objArray);
                if (agent != null)
                {
                    agent.PasswordField = "*********";
                    httpStatusCodeResult = base.View(agent);
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

        public async Task<ActionResult> AgentsDetails(int? id)
        {
            ActionResult httpStatusCodeResult;
            if (id.HasValue)
            {
                DbSet<Agents> agents1 = this.db.Agents;
                object[] objArray = new object[] { id };
                Agents agent = await agents1.FindAsync(objArray);
                if (agent != null)
                {
                    agent.PasswordField = "*********";
                    httpStatusCodeResult = base.View(agent);
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

        public ActionResult Create()
        {
            return base.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Path,EmailField,PasswordField,FirstName,LastName,Phone,Position,Description,HomeTown,Languages,Interests,Instagram,Twitter,Facebook,File,Category")] Agents agents)
        {
            ActionResult action;
            object obj;
            if (base.ModelState.IsValid)
            {
                DbSet<Agents> agents1 = this.db.Agents;
                Agents agent = await agents1.FirstOrDefaultAsync<Agents>((Agents x) => x.EmailField.Equals(agents.EmailField));
                if (agent != null)
                {
                    ((dynamic)base.ViewBag).Error = "Model State is not valid or Email already exists";
                }
                else
                {
                    string str = "";
                    int num = 0;
                    try
                    {
                        HttpPostedFileBase file = agents.File;
                        string fileName = file.FileName;
                        string[] strArrays = fileName.Split(new char[] { '.' });
                        str = string.Concat(".", strArrays[(int)strArrays.Length - 1]);
                        string str1 = DateTime.Now.ToString();
                        str1 = str1.Replace(" ", string.Empty);
                        str1 = str1.Replace("/", string.Empty);
                        str1 = str1.Replace(":", string.Empty);
                        if (file == null)
                        {
                            agents.Path = "";
                            this.db.Agents.Add(agents);
                            await this.db.SaveChangesAsync();
                            action = base.RedirectToAction("Index");
                            return action;
                        }
                        else
                        {
                            WebImage webImage = new WebImage(file.InputStream);
                            string str2 = string.Concat("../../Content/Images/Agents/", strArrays[0], str1, str.ToString());
                            webImage.Save(string.Concat("~\\Content\\Images\\Agents\\", strArrays[0], str1, str), null, true);
                            agents.Path = str2;
                            this.db.Agents.Add(agents);
                            await this.db.SaveChangesAsync();
                            action = base.RedirectToAction("Index");
                            return action;
                        }
                    }
                    catch (Exception exception)
                    {
                        obj = exception;
                        num = 1;
                    }
                    if (num != 1)
                    {
                        obj = null;
                    }
                    else
                    {
                        Exception exception1 = (Exception)obj;
                        agents.Path = "";
                        this.db.Agents.Add(agents);
                        await this.db.SaveChangesAsync();
                        action = base.RedirectToAction("Index");
                        return action;
                    }
                }
            }
            action = base.View(agents);
            return action;
        }

        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AgentsController agentsController = this;
            FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(agentsController.HttpContext.Request.Cookies["AgentEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"].Value);
            string cookiePath = authenticationTicket.CookiePath;
            DateTime expiration = authenticationTicket.Expiration;
            bool expired = authenticationTicket.Expired;
            bool isPersistent = authenticationTicket.IsPersistent;
            DateTime issueDate = authenticationTicket.IssueDate;
            string name = authenticationTicket.Name;
            string userData = authenticationTicket.UserData;
            int version = authenticationTicket.Version;
            if (!expired)
            {
                int cookie = int.Parse(name);
                try
                {
                    Agents admin = await agentsController.db.Agents.FirstOrDefaultAsync<Agents>((Expression<Func<Agents, bool>>)(x => x.ID == cookie));
                    if (cookie != id && admin.Category.Equals("Team"))
                    {
                        List<Buildings> buildings = await agentsController.db.Buildings.Where<Buildings>((Expression<Func<Buildings, bool>>)(x => x.AgentsID == (int?)id)).ToListAsync<Buildings>();
                        List<BuildingRent> buildingRent = await agentsController.db.BuildingRents.Where<BuildingRent>((Expression<Func<BuildingRent, bool>>)(x => x.AgentsID == (int?)id)).ToListAsync<BuildingRent>();
                        List<SalesAgent> salesAgent = await agentsController.db.SalesAgents.Where<SalesAgent>((Expression<Func<SalesAgent, bool>>)(x => x.AgentsID == (int?)id)).ToListAsync<SalesAgent>();
                        foreach (Buildings buildings1 in buildings)
                        {
                            Buildings details = buildings1;
                            Buildings buildings2 = await agentsController.db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.ID == details.ID));
                            buildings2.AgentsID = new int?(cookie);
                            buildings2.AgentName = admin.FirstName + " " + admin.LastName;
                            agentsController.db.SaveChanges();
                        }
                        foreach (BuildingRent buildingRent1 in buildingRent)
                        {
                            BuildingRent details = buildingRent1;
                            (await agentsController.db.BuildingRents.FirstOrDefaultAsync<BuildingRent>((Expression<Func<BuildingRent, bool>>)(x => x.ID == details.ID))).AgentsID = new int?(cookie);
                            agentsController.db.SaveChanges();
                        }
                        foreach (SalesAgent salesAgent1 in salesAgent)
                        {
                            SalesAgent details = salesAgent1;
                            (await agentsController.db.SalesAgents.FirstOrDefaultAsync<SalesAgent>((Expression<Func<SalesAgent, bool>>)(x => x.ID == details.ID))).AgentsID = new int?(cookie);
                            agentsController.db.SaveChanges();
                        }
                        Agents async = await agentsController.db.Agents.FindAsync((object)id);
                        agentsController.db.Agents.Remove(async);
                        int num = await agentsController.db.SaveChangesAsync();
                        buildings = (List<Buildings>)null;
                        buildingRent = (List<BuildingRent>)null;
                        salesAgent = (List<SalesAgent>)null;
                    }
                    admin = (Agents)null;
                }
                catch (Exception)
                {
                }
            }
            return (ActionResult)agentsController.RedirectToAction("Index");
        }
             

        public async Task<ActionResult> Edit(int? id)
        {
            ActionResult httpStatusCodeResult;
            if (id.HasValue)
            {
                DbSet<Agents> agents1 = this.db.Agents;
                object[] objArray = new object[] { id };
                Agents agent = await agents1.FindAsync(objArray);
                if (agent != null)
                {
                    agent.PasswordField = "*********";
                    httpStatusCodeResult = base.View(agent);
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
        public async Task<ActionResult> Edit([Bind(Include = "ID,Path,EmailField,PasswordField,FirstName,LastName,Phone,Position,Description,HomeTown,Languages,Interests,Instagram,Twitter,Facebook,File,Category")] Agents agents)
        {
            ActionResult action;
            if (base.ModelState.IsValid)
            {
                DbSet<Agents> agents1 = this.db.Agents;
                Agents agent = await agents1.FirstOrDefaultAsync<Agents>((Agents x) => x.ID != agents.ID && x.EmailField.Equals(agents.EmailField));
                if (agent != null)
                {
                    ((dynamic)base.ViewBag).Error = "Model State is not valid or Email already exists";
                }
                else
                {
                    string str = "";
                    int num = 0;
                    try
                    {
                        HttpPostedFileBase file = agents.File;
                        string fileName = file.FileName;
                        string[] strArrays = fileName.Split(new char[] { '.' });
                        str = string.Concat(".", strArrays[(int)strArrays.Length - 1]);
                        string str1 = DateTime.Now.ToString();
                        str1 = str1.Replace(" ", string.Empty);
                        str1 = str1.Replace("/", string.Empty);
                        str1 = str1.Replace(":", string.Empty);
                        if (file == null)
                        {
                            this.db.Entry<Agents>(agents).State = EntityState.Modified;
                            await this.db.SaveChangesAsync();
                            action = base.RedirectToAction("Index");
                            return action;
                        }
                        else
                        {
                            WebImage webImage = new WebImage(file.InputStream);
                            string str2 = string.Concat("../../Content/Images/Agents/", strArrays[0], str1, str.ToString());
                            webImage.Save(string.Concat("~\\Content\\Images\\Agents\\", strArrays[0], str1, str), null, true);
                            agents.Path = str2;
                            this.db.Entry<Agents>(agents).State = EntityState.Modified;
                            await this.db.SaveChangesAsync();
                            action = base.RedirectToAction("Index");
                            return action;
                        }
                    }
                    catch (Exception)
                    {
                        num = 1;
                    }
                    if (num == 1)
                    {
                        this.db.Entry<Agents>(agents).State = EntityState.Modified;
                        await this.db.SaveChangesAsync();
                        action = base.RedirectToAction("Index");
                        return action;
                    }
                }
            }
            action = base.View(agents);
            return action;
        }

        public async Task<ActionResult> Index()
        {
            List<Agents> listAsync = await this.db.Agents.ToListAsync<Agents>();
            return base.View(listAsync);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}