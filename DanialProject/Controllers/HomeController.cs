using DanialProject.Models.Database;
using DanialProject.Models.DataClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DanialProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AdminContext db = new AdminContext();

        public HomeController()
        {
        }

        public ActionResult Apply()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return base.View();
        }

        [Authorize]
        public async Task<ActionResult> EditSalesListing(int id, string type = "")
        {
            ActionResult action;
            try
            {
                HttpCookie item = base.Request.Cookies["AgentEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"];
                FormsAuthenticationTicket formsAuthenticationTicket = FormsAuthentication.Decrypt(item.Value);
                string cookiePath = formsAuthenticationTicket.CookiePath;
                DateTime dateTime = formsAuthenticationTicket.Expiration;
                bool flag = formsAuthenticationTicket.Expired;
                bool flag1 = formsAuthenticationTicket.IsPersistent;
                DateTime dateTime1 = formsAuthenticationTicket.IssueDate;
                string name = formsAuthenticationTicket.Name;
                string str = formsAuthenticationTicket.UserData;
                int num = formsAuthenticationTicket.Version;
                if (!flag)
                {
                    ((dynamic)base.ViewBag).ID = id;
                    ((dynamic)base.ViewBag).PageType = type;
                    List<Features> listAsync = await this.db.Features.ToListAsync<Features>();
                    action = base.View(listAsync);
                    return action;
                }
            }
            catch (Exception)
            {
            }
            action = base.RedirectToAction("ManageListing", "Home");
            return action;
        }

        //[Authorize]
        //public async Task<ActionResult> EditUnitListing(int id, string type = "")
        //{

        //}

        public async Task<ActionResult> FindListing(FilterBox filterBox, string filter = "Rent")
        {
            if (filter != "Repeat")
            {
                Others.SetFilterSession(filterBox);
            }
            List<Features> listAsync = await this.db.Features.ToListAsync<Features>();
            return base.View(listAsync);
        }

        public ActionResult Index()
        {
            return base.View();
        }

        public ActionResult LearnMore()
        {
            return base.View();
        }

        [Authorize]
        public async Task<ActionResult> ManageListing(int? id)
        {
            ActionResult action;
            int? agentsID;
            int? nullable;
            bool flag;
            bool flag1;
            if (id.HasValue)
            {
                dynamic viewBag = base.ViewBag;
                DbSet<Agents> agents = this.db.Agents;
                object[] objArray = new object[] { id };
                Agents agent = await agents.FindAsync(objArray);
                viewBag.Agent = agent;
                viewBag = null;
                DbSet<Buildings> buildings = this.db.Buildings;
                IQueryable<Buildings> agentsID1 =
                    from x in buildings
                    where x.AgentsID == id
                    select x;
                List<Buildings> listAsync = await (
                    from x in agentsID1
                    orderby x.Status
                    select x).ToListAsync<Buildings>();
                List<Buildings> buildings1 = listAsync;
                List<Buildings> buildings2 = buildings1;
                foreach (Buildings list in (
                    from x in buildings2
                    where x.Type.Equals("Rent")
                    select x).ToList<Buildings>())
                {
                    DbSet<BuildingRent> buildingRents = this.db.BuildingRents;
                    BuildingRent buildingRent = await buildingRents.FirstOrDefaultAsync<BuildingRent>((BuildingRent x) => x.BuildingsID == (int?)list.ID);
                    BuildingRent buildingRent1 = buildingRent;
                    list.OtherDetails = buildingRent1;
                    agentsID = buildingRent1.AgentsID;
                    if (agentsID.HasValue)
                    {
                        agentsID = buildingRent1.AgentsID;
                        nullable = id;
                        flag = (agentsID.GetValueOrDefault() == nullable.GetValueOrDefault() ? agentsID.HasValue != nullable.HasValue : true);
                        if (flag)
                        {
                            buildings1.Remove(list);
                        }
                    }
                }
                DbSet<BuildingRent> buildingRents1 = this.db.BuildingRents;
                List<BuildingRent> listAsync1 = await (
                    from x in buildingRents1
                    where x.AgentsID == id
                    select x).ToListAsync<BuildingRent>();
                foreach (BuildingRent buildingRent2 in listAsync1)
                {
                    DbSet<Buildings> buildings3 = this.db.Buildings;
                    Buildings building1 = await buildings3.FirstOrDefaultAsync<Buildings>((Buildings x) => (int?)x.ID == buildingRent2.BuildingsID);
                    Buildings building = building1;
                    building.OtherDetails = buildingRent2;
                    nullable = building.AgentsID;
                    agentsID = buildingRent2.AgentsID;
                    flag1 = (nullable.GetValueOrDefault() == agentsID.GetValueOrDefault() ? nullable.HasValue != agentsID.HasValue : true);
                    if (flag1)
                    {
                        buildings1.Add(building);
                    }
                }
                List<Buildings> buildings4 = buildings1;
                foreach (Buildings building2 in
                    from x in buildings4
                    where x.Type.Equals("Sale")
                    select x)
                {
                    DbSet<BuildingSale> buildingSales = this.db.BuildingSales;
                    BuildingSale buildingSale = await buildingSales.FirstOrDefaultAsync<BuildingSale>((BuildingSale x) => x.BuildingsID == (int?)building2.ID);
                    building2.SaleBuilding = buildingSale;
                }
                dynamic obj = base.ViewBag;
                List<Buildings> buildings5 = buildings1;
                IOrderedEnumerable<Buildings> address =
                    from x in buildings5
                    orderby x.Address
                    select x;
                obj.Name = (
                    from x in address
                    select x.Address).Distinct<string>();
                action = base.View(buildings1);
            }
            else
            {
                action = base.RedirectToAction("Index", "Home");
            }
            return action;
        }

        //[Authorize]
        //public async Task<ActionResult> ManageListingAdmin(int? id)
        //{
        //	HomeController.< ManageListingAdmin > d__14 variable = new HomeController.< ManageListingAdmin > d__14();
        //	variable.<> 4__this = this;
        //	variable.id = id;
        //	variable.<> t__builder = AsyncTaskMethodBuilder<ActionResult>.Create();
        //	variable.<> 1__state = -1;
        //	variable.<> t__builder.Start < HomeController.< ManageListingAdmin > d__14 > (ref variable);
        //	return variable.<> t__builder.Task;
        //}

        public async Task<ActionResult> OurAgents()
        {
            DbSet<Agents> agents = this.db.Agents;
            List<Agents> listAsync = await (
                from x in agents
                where x.Category != "Fake"
                select x).ToListAsync<Agents>();
            return base.View(listAsync);
        }

        [Authorize]
        public async Task<ActionResult> SalesListing()
        {
            List<Features> listAsync = await this.db.Features.ToListAsync<Features>();
            return base.View(listAsync);
        }

        public async Task<ActionResult> SimpleListing()
        {
            List<Features> listAsync = await this.db.Features.ToListAsync<Features>();
            return base.View(listAsync);
        }

        public async Task<ActionResult> SingleAgent(int? id)
        {
            ActionResult action;
            int? agentsID;
            int? nullable;
            bool flag;
            bool flag1;
            List<Buildings> buildings = new List<Buildings>();
            if (id.HasValue)
            {
                DbSet<Agents> agents = this.db.Agents;
                object[] objArray = new object[] { id };
                Agents agent = await agents.FindAsync(objArray);
                if (!agent.Category.Equals("Fake"))
                {
                    ((dynamic)base.ViewBag).Agent = agent;
                    DbSet<Buildings> buildings1 = this.db.Buildings;
                    IQueryable<Buildings> agentsID1 =
                        from x in buildings1
                        where x.AgentsID == id && x.Status == "Approved" && !x.PrivateListing
                        select x;
                    List<Buildings> listAsync = await (
                        from x in agentsID1
                        orderby x.Status
                        select x).ToListAsync<Buildings>();
                    buildings = listAsync;
                    List<Buildings> buildings2 = buildings;
                    foreach (Buildings list in (
                        from x in buildings2
                        where x.Type.Equals("Rent")
                        select x).ToList<Buildings>())
                    {
                        DbSet<BuildingRent> buildingRents = this.db.BuildingRents;
                        BuildingRent buildingRent = await buildingRents.FirstOrDefaultAsync<BuildingRent>((BuildingRent x) => x.BuildingsID == (int?)list.ID);
                        BuildingRent buildingRent1 = buildingRent;
                        list.OtherDetails = buildingRent1;
                        agentsID = buildingRent1.AgentsID;
                        if (agentsID.HasValue)
                        {
                            agentsID = buildingRent1.AgentsID;
                            nullable = id;
                            flag = (agentsID.GetValueOrDefault() == nullable.GetValueOrDefault() ? agentsID.HasValue != nullable.HasValue : true);
                            if (flag)
                            {
                                buildings.Remove(list);
                            }
                        }
                    }
                    DbSet<BuildingRent> buildingRents1 = this.db.BuildingRents;
                    List<BuildingRent> listAsync1 = await (
                        from x in buildingRents1
                        where x.AgentsID == id
                        select x).ToListAsync<BuildingRent>();
                    foreach (BuildingRent buildingRent2 in listAsync1)
                    {
                        DbSet<Buildings> buildings3 = this.db.Buildings;
                        Buildings building1 = await buildings3.FirstOrDefaultAsync<Buildings>((Buildings x) => (int?)x.ID == buildingRent2.BuildingsID);
                        Buildings building = building1;
                        building.OtherDetails = buildingRent2;
                        nullable = building.AgentsID;
                        agentsID = buildingRent2.AgentsID;
                        flag1 = (nullable.GetValueOrDefault() == agentsID.GetValueOrDefault() ? nullable.HasValue != agentsID.HasValue : true);
                        if (flag1)
                        {
                            buildings.Add(building);
                        }
                    }
                    foreach (Buildings building2 in buildings)
                    {
                        try
                        {
                            Buildings path = building2;
                            DbSet<Images> images = this.db.Images;
                            path.Image = images.FirstOrDefault<Images>((Images x) => x.BuildingsID == (int?)building2.ID).Path;
                        }
                        catch (Exception)
                        {
                        }
                    }
                    action = base.View(buildings);
                }
                else
                {
                    action = base.RedirectToAction("Index", "Home");
                }
            }
            else
            {
                action = base.RedirectToAction("Index", "Home");
            }
            return action;
        }

        //public ActionResult SingleListing(int id)
        //{
        //    try
        //    {
        //        FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(this.Request.Cookies["AgentEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"].Value);
        //        string cookiePath = authenticationTicket.CookiePath;
        //        DateTime expiration = authenticationTicket.Expiration;
        //        bool expired = authenticationTicket.Expired;
        //        bool isPersistent = authenticationTicket.IsPersistent;
        //        DateTime issueDate = authenticationTicket.IssueDate;
        //        string name = authenticationTicket.Name;
        //        string userData = authenticationTicket.UserData;
        //        int version = authenticationTicket.Version;
        //        if (!expired)
        //            return (ActionResult)this.RedirectToAction("SingleListingAgent", "Home", (object)new
        //            {
        //                id = id
        //            });
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return Task.Run<ActionResult>((Func<Task<ActionResult>>)(() => this.SingleListingTask(new int?(id)))).Result;
        //}

        //public async Task<ActionResult> SingleListingTask(int? id)
        //{
        //    HomeController homeController = this;
        //    if (!id.HasValue)
        //        return (ActionResult)homeController.RedirectToAction("Index", "Home");
        //    Buildings building = homeController.db.Buildings.Find((object)id);
        //    Func<CallSite, object, List<Features>, object> func1;
        //    CallSite<Func<CallSite, object, List<Features>, object>> callSite1;
        //    object obj;
        //    Func<CallSite, object, Agents, object> func2;
        //    CallSite<Func<CallSite, object, Agents, object>> callSite2;
        //    if (building.Type == "Sale")
        //    {
        //        // ISSUE: reference to a compiler-generated field
        //        if (HomeController.\u003C\u003Eo__7.\u003C\u003Ep__0 == null)
        //         {
        //            // ISSUE: reference to a compiler-generated field
        //            HomeController.\u003C\u003Eo__7.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "BuildingId", typeof(HomeController), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
        //            {
        //                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        //                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        //            }));
        //        }
        //        // ISSUE: reference to a compiler-generated field
        //        // ISSUE: reference to a compiler-generated field
        //        object obj1 = HomeController.\u003C\u003Eo__7.\u003C\u003Ep__0.Target((CallSite)HomeController.\u003C\u003Eo__7.\u003C\u003Ep__0, homeController.ViewBag, id);
        //        BuildingSale buildingSale = await homeController.db.BuildingSales.FirstOrDefaultAsync<BuildingSale>((Expression<Func<BuildingSale, bool>>)(x => x.BuildingsID == id));
        //        buildingSale.Buildings = (Buildings)null;
        //        building.SaleBuilding = buildingSale;
        //        // ISSUE: reference to a compiler-generated field
        //        if (HomeController.\u003C\u003Eo__7.\u003C\u003Ep__1 == null)
        //         {
        //            // ISSUE: reference to a compiler-generated field
        //            HomeController.\u003C\u003Eo__7.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<Features>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Features", typeof(HomeController), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
        //            {
        //                 CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        //                 CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        //            }));
        //        }
        //        // ISSUE: reference to a compiler-generated field
        //        func1 = HomeController.\u003C\u003Eo__7.\u003C\u003Ep__1.Target;
        //        // ISSUE: reference to a compiler-generated field
        //        callSite1 = HomeController.\u003C\u003Eo__7.\u003C\u003Ep__1;
        //        obj = homeController.ViewBag;
        //        List<Features> listAsync1 = await homeController.db.Features.ToListAsync<Features>();
        //        object obj2 = func1((CallSite)callSite1, obj, listAsync1);
        //        func1 = (Func<CallSite, object, List<Features>, object>)null;
        //        callSite1 = (CallSite<Func<CallSite, object, List<Features>, object>>)null;
        //        obj = (object)null;
        //        List<Buildings> listAsync2 = await homeController.db.Buildings.Where<Buildings>((Expression<Func<Buildings, bool>>)(x => x.AgentsID == building.AgentsID && x.PrivateListing != true)).Take<Buildings>(3).ToListAsync<Buildings>();
        //        foreach (Buildings buildings in listAsync2)
        //        {
        //            Buildings details = buildings;
        //            try
        //            {
        //                details.Image = homeController.db.Images.FirstOrDefault<Images>((Expression<Func<Images, bool>>)(x => x.BuildingsID == (int?)details.ID)).Path;
        //            }
        //            catch (Exception ex)
        //            {
        //            }
        //        }
        //        foreach (Buildings buildings in listAsync2)
        //        {
        //            Buildings details = buildings;
        //            try
        //            {
        //                details.Image = homeController.db.Images.FirstOrDefault<Images>((Expression<Func<Images, bool>>)(x => x.BuildingsID == (int?)details.ID)).Path;
        //            }
        //            catch (Exception ex)
        //            {
        //            }
        //        }
        //        // ISSUE: reference to a compiler-generated field
        //        if (HomeController.\u003C\u003Eo__7.\u003C\u003Ep__2 == null)
        //{
        //            // ISSUE: reference to a compiler-generated field
        //            HomeController.\u003C\u003Eo__7.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, List<Buildings>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SimmilerLiting", typeof(HomeController), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
        //            {
        //    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        //    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        //            }));
        //        }
        //        // ISSUE: reference to a compiler-generated field
        //        // ISSUE: reference to a compiler-generated field
        //        object obj3 = HomeController.\u003C\u003Eo__7.\u003C\u003Ep__2.Target((CallSite)HomeController.\u003C\u003Eo__7.\u003C\u003Ep__2, homeController.ViewBag, listAsync2);
        //        // ISSUE: reference to a compiler-generated field
        //        if (HomeController.\u003C\u003Eo__7.\u003C\u003Ep__3 == null)
        //{
        //            // ISSUE: reference to a compiler-generated field
        //            HomeController.\u003C\u003Eo__7.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, Agents, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Agent", typeof(HomeController), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
        //            {
        //    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        //    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        //            }));
        //        }
        //        // ISSUE: reference to a compiler-generated field
        //        func2 = HomeController.\u003C\u003Eo__7.\u003C\u003Ep__3.Target;
        //        // ISSUE: reference to a compiler-generated field
        //        callSite2 = HomeController.\u003C\u003Eo__7.\u003C\u003Ep__3;
        //        obj = homeController.ViewBag;
        //        Agents agents = await homeController.db.Agents.FirstOrDefaultAsync<Agents>((Expression<Func<Agents, bool>>)(x => (int?)x.ID == building.AgentsID));
        //        object obj4 = func2((CallSite)callSite2, obj, agents);
        //        func2 = (Func<CallSite, object, Agents, object>)null;
        //        callSite2 = (CallSite<Func<CallSite, object, Agents, object>>)null;
        //        obj = (object)null;
        //    }
        //    else
        //    {
        //        // ISSUE: reference to a compiler-generated field
        //        if (HomeController.\u003C\u003Eo__7.\u003C\u003Ep__4 == null)
        //{
        //            // ISSUE: reference to a compiler-generated field
        //            HomeController.\u003C\u003Eo__7.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, int?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "BuildingId", typeof(HomeController), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
        //            {
        //    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        //    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        //            }));
        //        }
        //        // ISSUE: reference to a compiler-generated field
        //        // ISSUE: reference to a compiler-generated field
        //        object obj1 = HomeController.\u003C\u003Eo__7.\u003C\u003Ep__4.Target((CallSite)HomeController.\u003C\u003Eo__7.\u003C\u003Ep__4, homeController.ViewBag, id);
        //        BuildingRent build;
        //        BuildingRent buildingRent = build;
        //        build = await homeController.db.BuildingRents.FirstOrDefaultAsync<BuildingRent>((Expression<Func<BuildingRent, bool>>)(x => x.BuildingsID == id));
        //        build.Buildings = (Buildings)null;
        //        building.OtherDetails = build;
        //        // ISSUE: reference to a compiler-generated field
        //        if (HomeController.\u003C\u003Eo__7.\u003C\u003Ep__5 == null)
        //{
        //            // ISSUE: reference to a compiler-generated field
        //            HomeController.\u003C\u003Eo__7.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, List<Features>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Features", typeof(HomeController), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
        //            {
        //    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        //    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        //            }));
        //        }
        //        // ISSUE: reference to a compiler-generated field
        //        func1 = HomeController.\u003C\u003Eo__7.\u003C\u003Ep__5.Target;
        //        // ISSUE: reference to a compiler-generated field
        //        callSite1 = HomeController.\u003C\u003Eo__7.\u003C\u003Ep__5;
        //        obj = homeController.ViewBag;
        //        List<Features> listAsync1 = await homeController.db.Features.ToListAsync<Features>();
        //        object obj2 = func1((CallSite)callSite1, obj, listAsync1);
        //        func1 = (Func<CallSite, object, List<Features>, object>)null;
        //        callSite1 = (CallSite<Func<CallSite, object, List<Features>, object>>)null;
        //        obj = (object)null;
        //        List<Buildings> listAsync2 = await homeController.db.Buildings.Where<Buildings>((Expression<Func<Buildings, bool>>)(x => x.AgentsID == building.AgentsID && x.PrivateListing != true)).Take<Buildings>(3).ToListAsync<Buildings>();
        //        foreach (Buildings buildings in listAsync2)
        //        {
        //            Buildings details = buildings;
        //            try
        //            {
        //                details.Image = homeController.db.Images.FirstOrDefault<Images>((Expression<Func<Images, bool>>)(x => x.BuildingsID == (int?)details.ID)).Path;
        //            }
        //            catch (Exception ex)
        //            {
        //            }
        //        }
        //        // ISSUE: reference to a compiler-generated field
        //        if (HomeController.\u003C\u003Eo__7.\u003C\u003Ep__6 == null)
        //{
        //            // ISSUE: reference to a compiler-generated field
        //            HomeController.\u003C\u003Eo__7.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, List<Buildings>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SimmilerLiting", typeof(HomeController), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
        //            {
        //    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        //    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        //            }));
        //        }
        //        // ISSUE: reference to a compiler-generated field
        //        // ISSUE: reference to a compiler-generated field
        //        object obj3 = HomeController.\u003C\u003Eo__7.\u003C\u003Ep__6.Target((CallSite)HomeController.\u003C\u003Eo__7.\u003C\u003Ep__6, homeController.ViewBag, listAsync2);
        //        // ISSUE: reference to a compiler-generated field
        //        if (HomeController.\u003C\u003Eo__7.\u003C\u003Ep__7 == null)
        //{
        //            // ISSUE: reference to a compiler-generated field
        //            HomeController.\u003C\u003Eo__7.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, Agents, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Agent", typeof(HomeController), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
        //            {
        //    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        //    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        //            }));
        //        }
        //        // ISSUE: reference to a compiler-generated field
        //        func2 = HomeController.\u003C\u003Eo__7.\u003C\u003Ep__7.Target;
        //        // ISSUE: reference to a compiler-generated field
        //        callSite2 = HomeController.\u003C\u003Eo__7.\u003C\u003Ep__7;
        //        obj = homeController.ViewBag;
        //        Agents agents = await homeController.db.Agents.FirstOrDefaultAsync<Agents>((Expression<Func<Agents, bool>>)(x => (int?)x.ID == build.AgentsID));
        //        object obj4 = func2((CallSite)callSite2, obj, agents);
        //        func2 = (Func<CallSite, object, Agents, object>)null;
        //        callSite2 = (CallSite<Func<CallSite, object, Agents, object>>)null;
        //        obj = (object)null;
        //    }
        //    return (ActionResult)homeController.View((object)building);
        //}

        //public ActionResult SingleListingAgent(int id)
        //{
        //	try
        //	{
        //		HttpCookie authCookie = base.Request.Cookies["AgentEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"];
        //		FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
        //		string cookiePath1 = ticket.CookiePath;
        //		DateTime expiration = ticket.Expiration;
        //		bool expired = ticket.Expired;
        //		bool isPersistent = ticket.IsPersistent;
        //		DateTime issueDate = ticket.IssueDate;
        //		string CookieId = ticket.Name;
        //		string userData = ticket.UserData;
        //		int version = ticket.Version;
        //		if (!expired)
        //		{
        //			ActionResult result = Task.Run<ActionResult>(() => this.SingleListingTask(new int?(id))).Result;
        //			return result;
        //		}
        //	}
        //	catch (Exception)
        //	{
        //	}
        //	return base.RedirectToAction("SingleListing", "Home", new { id = id });
        //}

        //public async Task<ActionResult> SingleListingTask(int? id)
        //{


        //}

        public async Task<ActionResult> Team()
        {
            DbSet<Agents> agents = this.db.Agents;
            List<Agents> listAsync = await (
                from x in agents
                where x.Category.Equals("Hudson")
                select x).ToListAsync<Agents>();
            return base.View(listAsync);
        }

        public ActionResult Testing()
        {
            return base.View();
        }

   //     [Authorize]
   //     public async Task<ActionResult> UnitListing()
   //     {

   //         HomeController homeController = this;
   //         Func<CallSite, object, List<Agents>, object> func;
   //         CallSite<Func<CallSite, object, List<Agents>, object>> callSite;
   //         object obj;
   //         try
   //         {
   //             FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(homeController.Request.Cookies["AgentEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"].Value);
   //             string cookiePath = authenticationTicket.CookiePath;
   //             DateTime expiration = authenticationTicket.Expiration;
   //             bool expired = authenticationTicket.Expired;
   //             bool isPersistent = authenticationTicket.IsPersistent;
   //             DateTime issueDate = authenticationTicket.IssueDate;
   //             string name = authenticationTicket.Name;
   //             string userData = authenticationTicket.UserData;
   //             int version = authenticationTicket.Version;
   //             if (!expired)
   //             {
   //                 // ISSUE: reference to a compiler-generated field
   //                 if (HomeController.\u003C\u003Eo__17.\u003C\u003Ep__0 == null)
			//		{
   //                     // ISSUE: reference to a compiler-generated field
   //                     HomeController.\u003C\u003Eo__17.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Default", typeof(HomeController), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
   //                     {
   //                           CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
   //                           CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
   //                     }));
   //                 }
   //                 // ISSUE: reference to a compiler-generated field
   //                 // ISSUE: reference to a compiler-generated field
   //                 object obj1 = HomeController.\u003C\u003Eo__17.\u003C\u003Ep__0.Target((CallSite)HomeController.\u003C\u003Eo__17.\u003C\u003Ep__0, homeController.ViewBag, int.Parse(name));
   //                 // ISSUE: reference to a compiler-generated field
   //                 if (HomeController.\u003C\u003Eo__17.\u003C\u003Ep__1 == null)
			//		{
   //                     // ISSUE: reference to a compiler-generated field
   //                     HomeController.\u003C\u003Eo__17.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<Agents>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Agents", typeof(HomeController), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
   //                     {
   //                           CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
   //                          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
   //                     }));
   //                 }
   //                 // ISSUE: reference to a compiler-generated field
   //                 func = HomeController.\u003C\u003Eo__17.\u003C\u003Ep__1.Target;
   //                 // ISSUE: reference to a compiler-generated field
   //                 callSite = HomeController.\u003C\u003Eo__17.\u003C\u003Ep__1;
   //                 obj = homeController.ViewBag;
   //                 List<Agents> listAsync1 = await homeController.db.Agents.ToListAsync<Agents>();
   //                 object obj2 = func((CallSite)callSite, obj, listAsync1);
   //                 func = (Func<CallSite, object, List<Agents>, object>)null;
   //                 callSite = (CallSite<Func<CallSite, object, List<Agents>, object>>)null;
   //                 obj = (object)null;
   //                 object listAsync2 = (object)await homeController.db.Features.ToListAsync<Features>();
   //                 return (ActionResult)homeController.View(listAsync2);
   //             }
   //         }
   //         catch (Exception ex)
   //         {
   //         }
   //         // ISSUE: reference to a compiler-generated field
   //         if (HomeController.\u003C\u003Eo__17.\u003C\u003Ep__2 == null)
			//{
   //             // ISSUE: reference to a compiler-generated field
   //             HomeController.\u003C\u003Eo__17.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, List<Agents>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Agents", typeof(HomeController), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
   //             {
   //                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
   //                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
   //             }));
   //         }
   //         // ISSUE: reference to a compiler-generated field
   //         func = HomeController.\u003C\u003Eo__17.\u003C\u003Ep__2.Target;
   //         // ISSUE: reference to a compiler-generated field
   //         callSite = HomeController.\u003C\u003Eo__17.\u003C\u003Ep__2;
   //         obj = homeController.ViewBag;
   //         List<Agents> listAsync3 = await homeController.db.Agents.ToListAsync<Agents>();
   //         object obj3 = func((CallSite)callSite, obj, listAsync3);
   //         func = (Func<CallSite, object, List<Agents>, object>)null;
   //         callSite = (CallSite<Func<CallSite, object, List<Agents>, object>>)null;
   //         obj = (object)null;
   //         // ISSUE: reference to a compiler-generated field
   //         if (HomeController.\u003C\u003Eo__17.\u003C\u003Ep__3 == null)
			//{
   //             // ISSUE: reference to a compiler-generated field
   //             HomeController.\u003C\u003Eo__17.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Default", typeof(HomeController), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
   //             {
   //                 CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
   //                 CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
   //             }));
   //         }
   //         // ISSUE: reference to a compiler-generated field
   //         // ISSUE: reference to a compiler-generated field
   //         object obj4 = HomeController.\u003C\u003Eo__17.\u003C\u003Ep__3.Target((CallSite)HomeController.\u003C\u003Eo__17.\u003C\u003Ep__3, homeController.ViewBag, 0);
   //         object listAsync4 = (object)await homeController.db.Features.ToListAsync<Features>();
   //         return (ActionResult)homeController.View(listAsync4);

   //     }

        public async Task<ActionResult> UnitListingTask()
        {
            List<Features> listAsync = await this.db.Features.ToListAsync<Features>();
            return base.View(listAsync);
        }
    }
}