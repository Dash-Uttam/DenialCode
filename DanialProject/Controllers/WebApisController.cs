using DanialProject.Models;
using DanialProject.Models.Database;
using DanialProject.Models.DataClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace DanialProject.Controllers
{
    public class WebApisController : ApiController
    {
        private readonly AdminContext db = new AdminContext();

        public WebApisController()
        {
        }

        public void ApproveMultipleListing()
        {
            string Id = HttpContext.Current.Request["Ids"];
            string Note = HttpContext.Current.Request["Note"];
            try
            {
                string[] strArrays = Id.Split(new char[] { ',' });
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    string details = strArrays[i];
                    if (!string.IsNullOrWhiteSpace(details))
                    {
                        try
                        {
                            int num = int.Parse(details);
                            Buildings building = this.db.Buildings.FirstOrDefault<Buildings>((Buildings x) => x.ID == num);
                            building.RejectNote = Note;
                            building.Status = "Approved";
                            this.db.SaveChanges();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void ApproveSingleListing()
        {
            int num = int.Parse(HttpContext.Current.Request["Id"]);
            string Note = HttpContext.Current.Request["Note"];
            try
            {
                Buildings building = this.db.Buildings.FirstOrDefault<Buildings>((Buildings x) => x.ID == num);
                building.RejectNote = Note;
                building.Status = "Approved";
                this.db.SaveChanges();
            }
            catch (Exception)
            {
            }
        }

        [AllowAnonymous]
        public async Task<int> CheckLogin(LoginViewModel model)
        {
            WebApisController webApisController = this;
            if (!webApisController.ModelState.IsValid)
                return 0;
            try
            {
                Agents agents = await webApisController.db.Agents.FirstOrDefaultAsync<Agents>((Expression<Func<Agents, bool>>)(x => x.EmailField.Equals(model.Email) && x.PasswordField.Equals(model.Password)));
                if (agents == null)
                    return 0;
                try
                {
                    DateTime now = DateTime.Now;
                    HttpContext.Current.Response.Cookies.Add(new HttpCookie("AgentEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd", FormsAuthentication.Encrypt(new FormsAuthenticationTicket(0, agents.ID.ToString(), now, DateTime.Now.AddMinutes(525600.0), false, agents.Path)))
                    {
                        Domain = FormsAuthentication.CookieDomain,
                        Path = FormsAuthentication.FormsCookiePath,
                        HttpOnly = true,
                        Secure = FormsAuthentication.RequireSSL
                    });
                    FormsAuthentication.SetAuthCookie(agents.EmailField, false);
                }
                catch (Exception)
                {
                }
                return agents.ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void ChnageStatus()
        {
            int num = int.Parse(HttpContext.Current.Request["Id"]);
            string Status = HttpContext.Current.Request["Status"];
            try
            {
                Buildings building = this.db.Buildings.FirstOrDefault<Buildings>((Buildings x) => x.ID == num);
                building.BuildingStatus = Status;
                this.db.SaveChanges();
            }
            catch (Exception)
            {
            }
        }

        public int DeleteAdmin(int Id)
        {
            return AdminsClass.DeleteRecord(Id);
        }

        public void DeleteImage(int Id)
        {
            try
            {
                Images temp = this.db.Images.FirstOrDefault<Images>((Images x) => x.ID == Id);
                this.db.Images.Remove(temp);
                this.db.SaveChanges();
            }
            catch (Exception)
            {
            }
        }

        public async Task<int> DeleteListing(int Id)
        {
            AdminContext db = new AdminContext();
            int num;
            try
            {
                Buildings temp = await db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.ID == Id));
                List<BuildingFeatures> listAsync1 = await db.BuildingFeatures.Where<BuildingFeatures>((Expression<Func<BuildingFeatures, bool>>)(x => x.BuildingsID == (int?)Id)).ToListAsync<BuildingFeatures>();
                db.BuildingFeatures.RemoveRange((IEnumerable<BuildingFeatures>)listAsync1);
                List<Images> listAsync2 = await db.Images.Where<Images>((Expression<Func<Images, bool>>)(x => x.BuildingsID == (int?)Id)).ToListAsync<Images>();
                db.Images.RemoveRange((IEnumerable<Images>)listAsync2);
                List<BuildingRent> listAsync3 = await db.BuildingRents.Where<BuildingRent>((Expression<Func<BuildingRent, bool>>)(x => x.BuildingsID == (int?)Id)).ToListAsync<BuildingRent>();
                db.BuildingRents.RemoveRange((IEnumerable<BuildingRent>)listAsync3);
                List<BuildingSale> listAsync4 = await db.BuildingSales.Where<BuildingSale>((Expression<Func<BuildingSale, bool>>)(x => x.BuildingsID == (int?)Id)).ToListAsync<BuildingSale>();
                db.BuildingSales.RemoveRange((IEnumerable<BuildingSale>)listAsync4);
                List<SalesAgent> listAsync5 = await db.SalesAgents.Where<SalesAgent>((Expression<Func<SalesAgent, bool>>)(x => x.BuildingsID == (int?)Id)).ToListAsync<SalesAgent>();
                db.SalesAgents.RemoveRange((IEnumerable<SalesAgent>)listAsync5);
                db.Buildings.Remove(temp);
                db.SaveChanges();
                num = 1;
                temp = (Buildings)null;
            }
            catch (Exception)
            {
                num = 0;
            }
            return num;
        }

        public async Task<int> DeleteMultipleListing(string arr)
        {
            AdminContext db = new AdminContext();
            int num = 0;
            try
            {
                string[] strArray = arr.Split(',');
                for (int index = 0; index < strArray.Length; ++index)
                {
                    string s = strArray[index];
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        int Id = int.Parse(s);
                        Buildings temp = await db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.ID == Id));
                        List<BuildingFeatures> listAsync1 = await db.BuildingFeatures.Where<BuildingFeatures>((Expression<Func<BuildingFeatures, bool>>)(x => x.BuildingsID == (int?)Id)).ToListAsync<BuildingFeatures>();
                        db.BuildingFeatures.RemoveRange((IEnumerable<BuildingFeatures>)listAsync1);
                        List<Images> listAsync2 = await db.Images.Where<Images>((Expression<Func<Images, bool>>)(x => x.BuildingsID == (int?)Id)).ToListAsync<Images>();
                        db.Images.RemoveRange((IEnumerable<Images>)listAsync2);
                        List<BuildingRent> listAsync3 = await db.BuildingRents.Where<BuildingRent>((Expression<Func<BuildingRent, bool>>)(x => x.BuildingsID == (int?)Id)).ToListAsync<BuildingRent>();
                        db.BuildingRents.RemoveRange((IEnumerable<BuildingRent>)listAsync3);
                        List<BuildingSale> listAsync4 = await db.BuildingSales.Where<BuildingSale>((Expression<Func<BuildingSale, bool>>)(x => x.BuildingsID == (int?)Id)).ToListAsync<BuildingSale>();
                        db.BuildingSales.RemoveRange((IEnumerable<BuildingSale>)listAsync4);
                        List<SalesAgent> listAsync5 = await db.SalesAgents.Where<SalesAgent>((Expression<Func<SalesAgent, bool>>)(x => x.BuildingsID == (int?)Id)).ToListAsync<SalesAgent>();
                        db.SalesAgents.RemoveRange((IEnumerable<SalesAgent>)listAsync5);
                        db.Buildings.Remove(temp);
                        db.SaveChanges();
                        num = 1;
                        temp = (Buildings)null;
                    }
                }
                strArray = (string[])null;
            }
            catch (Exception)
            {
                num = 0;
            }
            return num;
        }

        [AllowAnonymous]
        public async Task<SaveBuildingResponse> EditSalesListing(Buildings obj)
        {
            string CookieId = "0";
            SaveBuildingResponse responseObj = new SaveBuildingResponse()
            {
                Status = 0,
                CookieId = int.Parse(CookieId)
            };
            try
            {
                FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies["AgentEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"].Value);
                string cookiePath = authenticationTicket.CookiePath;
                DateTime expiration = authenticationTicket.Expiration;
                bool expired = authenticationTicket.Expired;
                bool isPersistent = authenticationTicket.IsPersistent;
                DateTime issueDate = authenticationTicket.IssueDate;
                CookieId = authenticationTicket.Name;
                string userData = authenticationTicket.UserData;
                int version = authenticationTicket.Version;
                if (!expired)
                {
                    try
                    {
                        obj.Type = "Sale";
                        bool check = true;
                        do
                        {
                            double latitude = obj.Latitude;
                            if (await this.db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.Latitude == latitude && x.ID != obj.ID)) != null)
                            {
                                latitude = Others.Getlatitude(latitude, new Random().NextDouble() * 0.003 + 0.001);
                                obj.Latitude = latitude;
                            }
                            else
                                check = false;
                        }
                        while (check);
                        check = true;
                        do
                        {
                            double longitude = obj.Longitude;
                            if (await this.db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.Longitude == longitude && x.ID != obj.ID)) != null)
                            {
                                longitude = Others.Getlongitude(obj.Longitude, longitude, new Random().NextDouble() * 0.003 + 0.001);
                                obj.Longitude = longitude;
                            }
                            else
                                check = false;
                        }
                        while (check);
                        this.db.Entry<Buildings>(obj).State = EntityState.Modified;
                        obj.SaleBuilding.BuildingsID = new int?(obj.ID);
                        this.db.Entry<BuildingSale>(obj.SaleBuilding).State = EntityState.Modified;
                        int num1 = await this.db.SaveChangesAsync();
                        responseObj.Status = obj.ID;
                        this.db.BuildingFeatures.RemoveRange((IEnumerable<BuildingFeatures>)await this.db.BuildingFeatures.Where<BuildingFeatures>((Expression<Func<BuildingFeatures, bool>>)(x => x.BuildingsID == (int?)obj.ID)).ToListAsync<BuildingFeatures>());
                        int num2 = await this.db.SaveChangesAsync();
                        int[] numArray = obj.Array;
                        for (int index = 0; index < numArray.Length; ++index)
                        {
                            int num3 = numArray[index];
                            this.db.BuildingFeatures.Add(new BuildingFeatures()
                            {
                                BuildingsID = new int?(obj.ID),
                                FeaturesID = new int?(num3)
                            });
                            int num4 = await this.db.SaveChangesAsync();
                        }
                        numArray = (int[])null;
                        responseObj.CookieId = int.Parse(CookieId);
                        responseObj.Status = 1;
                        responseObj.BuildingId = obj.ID;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
                responseObj.Status = 0;
            }
            return responseObj;
        }

        [AllowAnonymous]
        public async Task<SaveBuildingResponse> EditUnitListing(Buildings obj)
        {
            string CookieId = "0";
            SaveBuildingResponse responseObj = new SaveBuildingResponse()
            {
                Status = 0,
                CookieId = int.Parse(CookieId)
            };
            try
            {
                FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies["AgentEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"].Value);
                string cookiePath = authenticationTicket.CookiePath;
                DateTime expiration = authenticationTicket.Expiration;
                bool expired = authenticationTicket.Expired;
                bool isPersistent = authenticationTicket.IsPersistent;
                DateTime issueDate = authenticationTicket.IssueDate;
                CookieId = authenticationTicket.Name;
                string userData = authenticationTicket.UserData;
                int version = authenticationTicket.Version;
                if (!expired)
                {
                    try
                    {
                        obj.Type = "Rent";
                        bool check = true;
                        do
                        {
                            double latitude = obj.Latitude;
                            if (await this.db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.Latitude == latitude && x.ID != obj.ID)) != null)
                            {
                                latitude = Others.Getlatitude(latitude, new Random().NextDouble() * 0.003 + 0.001);
                                obj.Latitude = latitude;
                            }
                            else
                                check = false;
                        }
                        while (check);
                        check = true;
                        do
                        {
                            double longitude = obj.Longitude;
                            if (await this.db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.Longitude == longitude && x.ID != obj.ID)) != null)
                            {
                                longitude = Others.Getlongitude(obj.Longitude, longitude, new Random().NextDouble() * 0.003 + 0.001);
                                obj.Longitude = longitude;
                            }
                            else
                                check = false;
                        }
                        while (check);
                        obj.OtherDetails.OP = Others.Check_OP(obj.OtherDetails.OP);
                        this.db.Entry<Buildings>(obj).State = EntityState.Modified;
                        obj.OtherDetails.BuildingsID = new int?(obj.ID);
                        this.db.Entry<BuildingRent>(obj.OtherDetails).State = EntityState.Modified;
                        int num1 = await this.db.SaveChangesAsync();
                        this.db.SalesAgents.RemoveRange((IEnumerable<SalesAgent>)await this.db.SalesAgents.Where<SalesAgent>((Expression<Func<SalesAgent, bool>>)(x => x.BuildingsID == (int?)obj.ID)).ToListAsync<SalesAgent>());
                        int num2 = await this.db.SaveChangesAsync();
                        try
                        {
                            for (int i = 0; i < obj.OtherDetails.SalesAgents.Length; ++i)
                            {
                                try
                                {
                                    string[] strArray = obj.OtherDetails.SalesAgents[i].Split('_');
                                    if (strArray[1] != "0")
                                    {
                                        this.db.SalesAgents.Add(new SalesAgent()
                                        {
                                            AgentsID = new int?(int.Parse(strArray[1])),
                                            BuildingsID = new int?(obj.ID)
                                        });
                                        int num3 = await this.db.SaveChangesAsync();
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        this.db.BuildingFeatures.RemoveRange((IEnumerable<BuildingFeatures>)await this.db.BuildingFeatures.Where<BuildingFeatures>((Expression<Func<BuildingFeatures, bool>>)(x => x.BuildingsID == (int?)obj.ID)).ToListAsync<BuildingFeatures>());
                        int num4 = await this.db.SaveChangesAsync();
                        int[] numArray = obj.Array;
                        for (int index = 0; index < numArray.Length; ++index)
                        {
                            int num3 = numArray[index];
                            this.db.BuildingFeatures.Add(new BuildingFeatures()
                            {
                                BuildingsID = new int?(obj.ID),
                                FeaturesID = new int?(num3)
                            });
                            int num5 = await this.db.SaveChangesAsync();
                        }
                        numArray = (int[])null;
                        responseObj.CookieId = int.Parse(CookieId);
                        responseObj.Status = 1;
                        responseObj.BuildingId = obj.ID;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
                responseObj.Status = 0;
            }
            return responseObj;
        }

        [AllowAnonymous]
        public async Task<int> ForgotButton(string email)
        {
            try
            {
                Agents user = await this.db.Agents.FirstOrDefaultAsync<Agents>((Expression<Func<Agents, bool>>)(x => x.EmailField.Equals(email)));
                if (user == null)
                    return 0;
                string code = new Random().Next(0, 999999).ToString("D6");
                user.SecurityCode = code;
                this.db.Entry<Agents>(user).State = EntityState.Modified;
                int num = await this.db.SaveChangesAsync();
                string str1 = "<body bgcolor='white'>This is your Security Code. Use it to change your passwod. <br/><br/><h1>" + code + "</h1> <br/><br/></body>";
                try
                {
                    MailAddress from = new MailAddress("info@dallien.com", "By Dallien.com");
                    MailAddress to = new MailAddress(user.EmailField);
                    string password = "infome212";
                    string str2 = str1;
                    new SmtpClient()
                    {
                        Host = "relay-hosting.secureserver.net",
                        Port = 25,
                        EnableSsl = false,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = ((ICredentialsByHost)new NetworkCredential(from.Address, password))
                    }.Send(new MailMessage(from, to)
                    {
                        IsBodyHtml = true,
                        Subject = "Security Code",
                        Body = str2
                    });
                }
                catch (Exception)
                {
                }
                return user.ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        [AllowAnonymous]
        public void GalleryImages(ImageList li)
        {
            try
            {
                foreach (B64Image details in
                    from x in li.B64Images
                    where x.Type.Equals("New")
                    select x)
                {
                    try
                    {
                        Images image = new Images()
                        {
                            BuildingsID = new int?(li.BuildingId)
                        };
                        this.db.Images.Add(image);
                        this.db.SaveChanges();
                        Images UE = this.db.Images.First<Images>((Images x) => x.ID == image.ID);
                        Images image1 = image;
                        int d = image.ID;
                        string str = string.Concat("../../Content/Uploads/Buildings/", d.ToString(), ".png");
                        string str1 = str;
                        image1.Path = str;
                        UE.Path = str1;
                        this.db.SaveChanges();
                        if (li.Category.Equals("Edit"))
                        {
                            details.ImagePath = UE.Path;
                        }
                        string path = HttpContext.Current.Server.MapPath("~/Content/Uploads/Buildings");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        d = image.ID;
                        string imageName = string.Concat(d.ToString(), ".png");
                        string imgPath = Path.Combine(path, imageName);
                        File.WriteAllBytes(imgPath, Convert.FromBase64String(details.Image));
                    }
                    catch (Exception)
                    {
                    }
                }
                if (li.Category.Equals("Edit"))
                {
                    List<Images> RemoveList = (
                        from x in this.db.Images
                        where x.BuildingsID == (int?)li.BuildingId
                        select x).ToList<Images>();
                    foreach (B64Image details in li.B64Images)
                    {
                        try
                        {
                            Images image2 = new Images()
                            {
                                BuildingsID = new int?(li.BuildingId),
                                Path = details.ImagePath
                            };
                            this.db.Images.Add(image2);
                            this.db.SaveChanges();
                        }
                        catch (Exception)
                        {
                        }
                    }
                    this.db.Images.RemoveRange(RemoveList);
                    this.db.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
        }

        public DataTableResponse GetAdminTable(DataTableRequest request)
        {
            return AdminsClass.AdminTable(request);
        }

        [AllowAnonymous]
        public Agents GetAgentsInfo(int Id)
        {
            return this.db.Agents.FirstOrDefault<Agents>((Agents x) => x.ID == Id);
        }

        [AllowAnonymous]
        public async Task<List<Buildings>> GetAllBuildings(string filter = "Rent")
        {

            List<Buildings> li = new List<Buildings>();
            try
            {
                FilterBoxDouble filters = Others.ValueToDouble();
                li = await this.db.Buildings.Where<Buildings>((Expression<Func<Buildings, bool>>)(x => x.Status.Equals("Approved") && x.Type.Equals(filters.Filter) && x.Price >= filters.From_price && x.Price <= filters.To_price && (filters.BedPlus != false ? x.Beds >= filters.Bed : x.Beds == filters.Bed) && (filters.BathPlus != false ? x.Baths >= filters.Bath : x.Baths == filters.Bath) && x.PrivateListing != true)).ToListAsync<Buildings>();
            }
            catch (Exception)
            {
            }
            return li;
        }

        public List<LevelAccess> GetAllLevelAccess(int Id)
        {
            return SecurityClass.GetAllLevelAccess(Id);
        }

        [AllowAnonymous]
        public async Task<List<WebApisController.ESR>> GetAllNames()
        {
            List<WebApisController.ESR> li = new List<WebApisController.ESR>();
            try
            {
                List<string> listAsync = await this.db.Buildings.GroupBy<Buildings, string>((Expression<Func<Buildings, string>>)(x => x.Name)).Select<IGrouping<string, Buildings>, string>((Expression<Func<IGrouping<string, Buildings>, string>>)(y => y.FirstOrDefault<Buildings>().Name)).ToListAsync<string>();
                List<string> stringList = new List<string>();
                foreach (string str in listAsync)
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(str))
                            li.Add(new WebApisController.ESR()
                            {
                                Name = str
                            });
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
            return li;
        }

        [AllowAnonymous]
        public async Task<List<BuildingFeatures>> GetBuildingFeatures(int Id)
        {
            List<BuildingFeatures> li = new List<BuildingFeatures>();
            try
            {
                li = await this.db.BuildingFeatures.Where<BuildingFeatures>((Expression<Func<BuildingFeatures, bool>>)(x => x.BuildingsID == (int?)Id)).ToListAsync<BuildingFeatures>();
            }
            catch (Exception)
            {
            }
            return li;
        }

        public void GetChangePosition(int Id, int Position)
        {
            try
            {
                Images path = this.db.Images.FirstOrDefault<Images>((Images x) => x.ID == Id);
                List<Images> list = (
                    from x in this.db.Images
                    where x.BuildingsID == path.BuildingsID
                    select x).ToList<Images>();
                string F_image = path.Path;
                path.Path = list[Position].Path;
                int d = list[Position].ID;
                Images temp1 = this.db.Images.FirstOrDefault<Images>((Images x) => x.ID == d);
                temp1.Path = F_image;
                this.db.SaveChanges();
            }
            catch (Exception)
            {
            }
        }

        public Admins GetCurrentUserInfo()
        {
            return AdminsClass.GetCurrentUserInfo();
        }

        public async Task<int> GetDuplicateListing(int Id)
        {
            AdminContext db = new AdminContext();
            int num1 = 0;
            FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies["AgentEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"].Value);
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
                Agents agent = await db.Agents.FirstOrDefaultAsync<Agents>((Expression<Func<Agents, bool>>)(x => x.ID == cookie));
                try
                {
                    Buildings buildings = await db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.ID == Id));
                    buildings.Status = !agent.Category.Equals("Team") ? "Pending" : "Approved";
                    bool check = true;
                    do
                    {
                        double latitude = buildings.Latitude;
                        if (await db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.Latitude == latitude)) != null)
                        {
                            latitude = Others.Getlatitude(latitude, new Random().NextDouble() * 0.003 + 0.001);
                            buildings.Latitude = latitude;
                        }
                        else
                            check = false;
                    }
                    while (check);
                    check = true;
                    do
                    {
                        double longitude = buildings.Longitude;
                        if (await db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.Longitude == longitude)) != null)
                        {
                            longitude = Others.Getlongitude(buildings.Longitude, longitude, new Random().NextDouble() * 0.003 + 0.001);
                            buildings.Longitude = longitude;
                        }
                        else
                            check = false;
                    }
                    while (check);
                    db.Buildings.Add(buildings);
                    db.SaveChanges();
                    if (buildings.Type.Equals("Sale"))
                    {
                        BuildingSale buildingSale1 = await db.BuildingSales.FirstOrDefaultAsync<BuildingSale>((Expression<Func<BuildingSale, bool>>)(x => x.BuildingsID == (int?)Id));
                        BuildingSale buildingSale2 = new BuildingSale();
                        BuildingSale entity = buildingSale1;
                        entity.BuildingsID = new int?(buildings.ID);
                        db.BuildingSales.Add(entity);
                        db.SaveChanges();
                    }
                    else
                    {
                        BuildingRent buildingRent1 = await db.BuildingRents.FirstOrDefaultAsync<BuildingRent>((Expression<Func<BuildingRent, bool>>)(x => x.BuildingsID == (int?)Id));
                        BuildingRent buildingRent2 = new BuildingRent();
                        BuildingRent entity = buildingRent1;
                        entity.BuildingsID = new int?(buildings.ID);
                        db.BuildingRents.Add(entity);
                        db.SaveChanges();
                    }
                    DbSet<BuildingFeatures> buildingFeatures1 = db.BuildingFeatures;
                    Expression<Func<BuildingFeatures, bool>> predicate1 = (Expression<Func<BuildingFeatures, bool>>)(x => x.BuildingsID == (int?)Id);
                    foreach (BuildingFeatures buildingFeatures2 in await buildingFeatures1.Where<BuildingFeatures>(predicate1).ToListAsync<BuildingFeatures>())
                    {
                        db.BuildingFeatures.Add(new BuildingFeatures()
                        {
                            BuildingsID = new int?(buildings.ID),
                            FeaturesID = buildingFeatures2.FeaturesID
                        });
                        int num2 = await db.SaveChangesAsync();
                    }
                    DbSet<Images> images1 = db.Images;
                    Expression<Func<Images, bool>> predicate2 = (Expression<Func<Images, bool>>)(x => x.BuildingsID == (int?)Id);
                    foreach (Images images2 in await images1.Where<Images>(predicate2).ToListAsync<Images>())
                    {
                        db.Images.Add(new Images()
                        {
                            BuildingsID = new int?(buildings.ID),
                            Path = images2.Path
                        });
                        int num2 = await db.SaveChangesAsync();
                    }
                    DbSet<SalesAgent> salesAgents = db.SalesAgents;
                    Expression<Func<SalesAgent, bool>> predicate3 = (Expression<Func<SalesAgent, bool>>)(x => x.BuildingsID == (int?)Id);
                    foreach (SalesAgent salesAgent in await salesAgents.Where<SalesAgent>(predicate3).ToListAsync<SalesAgent>())
                    {
                        db.SalesAgents.Add(new SalesAgent()
                        {
                            BuildingsID = new int?(buildings.ID),
                            AgentsID = salesAgent.AgentsID
                        });
                        int num2 = await db.SaveChangesAsync();
                    }
                    num1 = 1;
                    buildings = (Buildings)null;
                }
                catch (Exception)
                {
                    num1 = 0;
                }
                agent = (Agents)null;
            }
            return num1;
        }

        [AllowAnonymous]
        public async Task<ImagesList> GetGalleryImages(int BuildingId)
        {
            ImagesList li = new ImagesList();
            try
            {
                Buildings buildings = await this.db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.ID == BuildingId));
                ImagesList imagesList = li;
                List<Images> listAsync = await this.db.Images.Where<Images>((Expression<Func<Images, bool>>)(x => x.BuildingsID == (int?)BuildingId)).ToListAsync<Images>();
                imagesList.list = listAsync;
                imagesList = (ImagesList)null;
                li.Type = buildings.Type;
                if (li.Type == "Rent")
                    li.No_Fee = (this.db.BuildingRents.FirstOrDefault<BuildingRent>((Expression<Func<BuildingRent, bool>>)(x => x.BuildingsID == (int?)BuildingId)).No_Fee ? 1 : 0) != 0;
                buildings = (Buildings)null;
            }
            catch (Exception)
            {
            }
            return li;
        }

        [AllowAnonymous]
        public async Task<List<Images>> GetImagesForSaleRent(int BuildingId)
        {

            List<Images> li = new List<Images>();
            try
            {
                li = await this.db.Images.Where<Images>((Expression<Func<Images, bool>>)(x => x.BuildingsID == (int?)BuildingId)).ToListAsync<Images>();
            }
            catch (Exception)
            {
            }
            return li;
        }

        [AllowAnonymous]
        public async Task<Buildings> GetListingAsync(int Id)
        {
            Agents agent;
            DbSet<Buildings> buildings = this.db.Buildings;
            Buildings building = await buildings.FirstOrDefaultAsync<Buildings>((Buildings x) => x.ID == Id);
            Buildings building1 = building;
            if (building1.Type != "Sale")
            {
                DbSet<BuildingRent> buildingRents = this.db.BuildingRents;
                BuildingRent buildingRent = await buildingRents.FirstOrDefaultAsync<BuildingRent>((BuildingRent x) => x.BuildingsID == (int?)Id);
                BuildingRent buildingRent1 = buildingRent;
                buildingRent1.Buildings = null;
                building1.OtherDetails = buildingRent1;
                DbSet<SalesAgent> salesAgents = this.db.SalesAgents;
                List<SalesAgent> listAsync = await (
                    from x in salesAgents
                    where x.BuildingsID == (int?)Id
                    select x).ToListAsync<SalesAgent>();
                List<SalesAgent> salesAgents1 = listAsync;
                string[] strArrays = new string[salesAgents1.Count<SalesAgent>()];
                int num = 0;
                foreach (SalesAgent salesAgent in salesAgents1)
                {
                    int num1 = num;
                    num = num1 + 1;
                    strArrays[num1] = string.Concat("SalesAgent_", salesAgent.AgentsID);
                }
                building1.OtherDetails.SalesAgents = strArrays;
                BuildingRent otherDetails = building1.OtherDetails;
                DbSet<Agents> agents = this.db.Agents;
                agent = await agents.FirstOrDefaultAsync<Agents>((Agents x) => (int?)x.ID == building1.OtherDetails.AgentsID);
                otherDetails.Agents = agent;
                otherDetails = null;
            }
            else
            {
                DbSet<BuildingSale> buildingSales = this.db.BuildingSales;
                BuildingSale buildingSale = await buildingSales.FirstOrDefaultAsync<BuildingSale>((BuildingSale x) => x.BuildingsID == (int?)Id);
                BuildingSale buildingSale1 = buildingSale;
                buildingSale1.Buildings = null;
                building1.SaleBuilding = buildingSale1;
                Buildings building2 = building1;
                DbSet<Agents> agents1 = this.db.Agents;
                agent = await agents1.FirstOrDefaultAsync<Agents>((Agents x) => (int?)x.ID == building1.AgentsID);
                building2.Agents = agent;
                building2 = null;
            }
            try
            {
                DbSet<BuildingFeatures> buildingFeatures = this.db.BuildingFeatures;
                List<BuildingFeatures> listAsync1 = await (
                    from x in buildingFeatures
                    where x.BuildingsID == (int?)Id
                    select x).ToListAsync<BuildingFeatures>();
                List<BuildingFeatures> buildingFeatures1 = listAsync1;
                int[] value = new int[buildingFeatures1.Count<BuildingFeatures>()];
                int num2 = 0;
                foreach (BuildingFeatures buildingFeature in buildingFeatures1)
                {
                    int num3 = num2;
                    num2 = num3 + 1;
                    value[num3] = buildingFeature.FeaturesID.Value;
                }
                building1.Array = value;
            }
            catch (Exception)
            {
            }
            return building1;
        }

        public async Task<int> GetMultipleDuplicate(string arr)
        {
            AdminContext db = new AdminContext();
            int num1 = 0;
            FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies["AgentEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"].Value);
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
                Agents agent = await db.Agents.FirstOrDefaultAsync<Agents>((Expression<Func<Agents, bool>>)(x => x.ID == cookie));
                try
                {
                    string[] strArray = arr.Split(',');
                    for (int index = 0; index < strArray.Length; ++index)
                    {
                        string s = strArray[index];
                        if (!string.IsNullOrWhiteSpace(s))
                        {
                            int cmp = int.Parse(s);
                            Buildings buildings = await db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.ID == cmp));
                            buildings.Status = !agent.Category.Equals("Team") ? "Pending" : "Approved";
                            bool check = true;
                            do
                            {
                                double latitude = buildings.Latitude;
                                if (await db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.Latitude == latitude)) != null)
                                {
                                    latitude = Others.Getlatitude(latitude, new Random().NextDouble() * 0.003 + 0.001);
                                    buildings.Latitude = latitude;
                                }
                                else
                                    check = false;
                            }
                            while (check);
                            check = true;
                            do
                            {
                                double longitude = buildings.Longitude;
                                if (await db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.Longitude == longitude)) != null)
                                {
                                    longitude = Others.Getlongitude(buildings.Longitude, longitude, new Random().NextDouble() * 0.003 + 0.001);
                                    buildings.Longitude = longitude;
                                }
                                else
                                    check = false;
                            }
                            while (check);
                            db.Buildings.Add(buildings);
                            db.SaveChanges();
                            if (buildings.Type.Equals("Sale"))
                            {
                                BuildingSale buildingSale1 = await db.BuildingSales.FirstOrDefaultAsync<BuildingSale>((Expression<Func<BuildingSale, bool>>)(x => x.BuildingsID == (int?)cmp));
                                BuildingSale buildingSale2 = new BuildingSale();
                                BuildingSale entity = buildingSale1;
                                entity.BuildingsID = new int?(buildings.ID);
                                db.BuildingSales.Add(entity);
                                db.SaveChanges();
                            }
                            else
                            {
                                BuildingRent buildingRent1 = await db.BuildingRents.FirstOrDefaultAsync<BuildingRent>((Expression<Func<BuildingRent, bool>>)(x => x.BuildingsID == (int?)cmp));
                                BuildingRent buildingRent2 = new BuildingRent();
                                BuildingRent entity = buildingRent1;
                                entity.BuildingsID = new int?(buildings.ID);
                                db.BuildingRents.Add(entity);
                                db.SaveChanges();
                            }
                            DbSet<BuildingFeatures> buildingFeatures1 = db.BuildingFeatures;
                            Expression<Func<BuildingFeatures, bool>> predicate1 = (Expression<Func<BuildingFeatures, bool>>)(x => x.BuildingsID == (int?)cmp);
                            foreach (BuildingFeatures buildingFeatures2 in await buildingFeatures1.Where<BuildingFeatures>(predicate1).ToListAsync<BuildingFeatures>())
                            {
                                db.BuildingFeatures.Add(new BuildingFeatures()
                                {
                                    BuildingsID = new int?(buildings.ID),
                                    FeaturesID = buildingFeatures2.FeaturesID
                                });
                                int num2 = await db.SaveChangesAsync();
                            }
                            DbSet<Images> images1 = db.Images;
                            Expression<Func<Images, bool>> predicate2 = (Expression<Func<Images, bool>>)(x => x.BuildingsID == (int?)cmp);
                            foreach (Images images2 in await images1.Where<Images>(predicate2).ToListAsync<Images>())
                            {
                                db.Images.Add(new Images()
                                {
                                    BuildingsID = new int?(buildings.ID),
                                    Path = images2.Path
                                });
                                int num2 = await db.SaveChangesAsync();
                            }
                            DbSet<SalesAgent> salesAgents = db.SalesAgents;
                            Expression<Func<SalesAgent, bool>> predicate3 = (Expression<Func<SalesAgent, bool>>)(x => x.BuildingsID == (int?)cmp);
                            foreach (SalesAgent salesAgent in await salesAgents.Where<SalesAgent>(predicate3).ToListAsync<SalesAgent>())
                            {
                                db.SalesAgents.Add(new SalesAgent()
                                {
                                    BuildingsID = new int?(buildings.ID),
                                    AgentsID = salesAgent.AgentsID
                                });
                                int num2 = await db.SaveChangesAsync();
                            }
                            buildings = (Buildings)null;
                        }
                    }
                    strArray = (string[])null;
                    num1 = 1;
                }
                catch (Exception)
                {
                    num1 = 0;
                }
                agent = (Agents)null;
            }
            return num1;
        }

        public List<SecurityLevel> GetSecurityLevel()
        {
            return SecurityClass.GetSecurityLevel();
        }

        public void GetSessionRenew()
        {
            SecurityClass.SessionRenew();
        }

        [AllowAnonymous]
        public async Task<List<Buildings>> GetSimpleBuildings(string filter = "Rent")
        {
            List<Buildings> li = new List<Buildings>();
            try
            {
                FilterBoxDouble filters = Others.ValueToDouble();
                li = await this.db.Buildings.Where<Buildings>((Expression<Func<Buildings, bool>>)(x => x.Status.Equals("Approved") && x.Type.Equals(filters.Filter) && x.Price >= filters.From_price && x.Price <= filters.To_price && (filters.BedPlus != false ? x.Beds >= filters.Bed : x.Beds == filters.Bed) && (filters.BathPlus != false ? x.Baths >= filters.Bath : x.Baths == filters.Bath) && x.PrivateListing != true)).ToListAsync<Buildings>();
                if (filter == "Rent")
                {
                    foreach (Buildings buildings in li)
                    {
                        Buildings details = buildings;
                        try
                        {
                            details.Image = this.db.Images.FirstOrDefault<Images>((Expression<Func<Images, bool>>)(x => x.BuildingsID == (int?)details.ID)).Path;
                            details.No_Fee = (this.db.BuildingRents.FirstOrDefault<BuildingRent>((Expression<Func<BuildingRent, bool>>)(x => x.BuildingsID == (int?)details.ID)).No_Fee ? 1 : 0) != 0;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                else
                {
                    foreach (Buildings buildings in li)
                    {
                        Buildings details = buildings;
                        try
                        {
                            details.Image = this.db.Images.FirstOrDefault<Images>((Expression<Func<Images, bool>>)(x => x.BuildingsID == (int?)details.ID)).Path;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return li;
        }

        public Admins GetSingleAdminById(int Id)
        {
            return AdminsClass.GetRecord(Id);
        }

        public int PostAdmin(Admins temp)
        {
            return AdminsClass.AddRecord(temp);
        }

        [AllowAnonymous]
        public int PostFilterFeatures(FilterFeatures filterFeatures)
        {
            FilterFeatures filterFeaturesSaved = filterFeatures;
            Others.FilterFeatures(filterFeatures);
            if (filterFeaturesSaved == null)
            {
                return 0;
            }
            return 1;
        }

        public void PostImagesAdmin()
        {
            AdminsClass.PostYourDetailsImage();
        }

        public int PostLevelAccesses(LevelAccess LA)
        {
            return SecurityClass.PostLevelAccesses(LA);
        }

        [AllowAnonymous]
        public async Task<SaveBuildingResponse> PostSalesListing(Buildings obj)
        {
            string CookieId = "0";
            SaveBuildingResponse responseObj = new SaveBuildingResponse()
            {
                Status = 0,
                CookieId = int.Parse(CookieId)
            };
            try
            {
                FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies["AgentEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"].Value);
                string cookiePath = authenticationTicket.CookiePath;
                DateTime expiration = authenticationTicket.Expiration;
                bool expired = authenticationTicket.Expired;
                bool isPersistent = authenticationTicket.IsPersistent;
                DateTime issueDate = authenticationTicket.IssueDate;
                CookieId = authenticationTicket.Name;
                string userData = authenticationTicket.UserData;
                int version = authenticationTicket.Version;
                if (!expired)
                {
                    obj.AgentsID = new int?(int.Parse(CookieId));
                    obj.Type = "Sale";
                    Agents agents = await this.db.Agents.FirstOrDefaultAsync<Agents>((Expression<Func<Agents, bool>>)(x => (int?)x.ID == obj.AgentsID));
                    obj.Status = !(agents.Category == "Team") ? "Pending" : "Approved";
                    obj.BuildingStatus = "Available";
                    obj.AgentName = agents.FirstName + " " + agents.LastName;
                    bool check = true;
                    do
                    {
                        double latitude = obj.Latitude;
                        if (await this.db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.Latitude == latitude)) != null)
                        {
                            latitude = Others.Getlatitude(latitude, new Random().NextDouble() * 0.008 + 0.001);
                            obj.Latitude = latitude;
                        }
                        else
                            check = false;
                    }
                    while (check);
                    check = true;
                    do
                    {
                        double longitude = obj.Longitude;
                        if (await this.db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.Longitude == longitude)) != null)
                        {
                            longitude = Others.Getlongitude(obj.Longitude, longitude, new Random().NextDouble() * 0.008 + 0.001);
                            obj.Longitude = longitude;
                        }
                        else
                            check = false;
                    }
                    while (check);
                    this.db.Buildings.Add(obj);
                    int num1 = await this.db.SaveChangesAsync();
                    responseObj.Status = obj.ID;
                    obj.SaleBuilding.BuildingsID = new int?(obj.ID);
                    this.db.BuildingSales.Add(obj.SaleBuilding);
                    int num2 = await this.db.SaveChangesAsync();
                    int[] numArray = obj.Array;
                    for (int index = 0; index < numArray.Length; ++index)
                    {
                        int num3 = numArray[index];
                        this.db.BuildingFeatures.Add(new BuildingFeatures()
                        {
                            BuildingsID = new int?(obj.ID),
                            FeaturesID = new int?(num3)
                        });
                        int num4 = await this.db.SaveChangesAsync();
                    }
                    numArray = (int[])null;
                    responseObj.CookieId = int.Parse(CookieId);
                    responseObj.Status = 1;
                    responseObj.BuildingId = obj.ID;
                }
            }
            catch (Exception)
            {
                responseObj.Status = 0;
            }
            return responseObj;
        }

        public int PostSecurityLevel(SecurityLevel SL)
        {
            return SecurityClass.PostSecurityLevel(SL);
        }

        [AllowAnonymous]
        public async Task<SaveBuildingResponse> PostUnitListing(Buildings obj)
        {
            string CookieId = "0";
            SaveBuildingResponse responseObj = new SaveBuildingResponse()
            {
                Status = 0,
                CookieId = int.Parse(CookieId)
            };
            try
            {
                FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies["AgentEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd"].Value);
                string cookiePath = authenticationTicket.CookiePath;
                DateTime expiration = authenticationTicket.Expiration;
                bool expired = authenticationTicket.Expired;
                bool isPersistent = authenticationTicket.IsPersistent;
                DateTime issueDate = authenticationTicket.IssueDate;
                CookieId = authenticationTicket.Name;
                string userData = authenticationTicket.UserData;
                int version = authenticationTicket.Version;
                if (!expired)
                {
                    obj.AgentsID = new int?(int.Parse(CookieId));
                    obj.Type = "Rent";
                    Agents agents = await this.db.Agents.FirstOrDefaultAsync<Agents>((Expression<Func<Agents, bool>>)(x => (int?)x.ID == obj.AgentsID));
                    obj.Status = !(agents.Category == "Team") ? "Pending" : "Approved";
                    obj.BuildingStatus = "Available";
                    obj.AgentName = agents.FirstName + " " + agents.LastName;
                    bool check = true;
                    do
                    {
                        double latitude = obj.Latitude;
                        if (await this.db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.Latitude == latitude)) != null)
                        {
                            latitude = Others.Getlatitude(latitude, new Random().NextDouble() * 0.003 + 0.001);
                            obj.Latitude = latitude;
                        }
                        else
                            check = false;
                    }
                    while (check);
                    check = true;
                    do
                    {
                        double longitude = obj.Longitude;
                        if (await this.db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.Longitude == longitude)) != null)
                        {
                            longitude = Others.Getlongitude(obj.Longitude, longitude, new Random().NextDouble() * 0.003 + 0.001);
                            obj.Longitude = longitude;
                        }
                        else
                            check = false;
                    }
                    while (check);
                    this.db.Buildings.Add(obj);
                    int num1 = await this.db.SaveChangesAsync();
                    obj.OtherDetails.BuildingsID = new int?(obj.ID);
                    this.db.BuildingRents.Add(obj.OtherDetails);
                    int num2 = await this.db.SaveChangesAsync();
                    try
                    {
                        for (int i = 0; i < obj.OtherDetails.SalesAgents.Length; ++i)
                        {
                            try
                            {
                                if (obj.OtherDetails.SalesAgents[i] != "0")
                                {
                                    this.db.SalesAgents.Add(new SalesAgent()
                                    {
                                        AgentsID = new int?(int.Parse(obj.OtherDetails.SalesAgents[i])),
                                        BuildingsID = new int?(obj.ID)
                                    });
                                    int num3 = await this.db.SaveChangesAsync();
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    int[] numArray = obj.Array;
                    for (int index = 0; index < numArray.Length; ++index)
                    {
                        int num3 = numArray[index];
                        this.db.BuildingFeatures.Add(new BuildingFeatures()
                        {
                            BuildingsID = new int?(obj.ID),
                            FeaturesID = new int?(num3)
                        });
                        int num4 = await this.db.SaveChangesAsync();
                    }
                    numArray = (int[])null;
                    responseObj.CookieId = int.Parse(CookieId);
                    responseObj.Status = 1;
                    responseObj.BuildingId = obj.ID;
                }
            }
            catch (Exception)
            {
                responseObj.Status = 0;
            }
            return responseObj;
        }

        public void RejectMultipleListing()
        {
            string Id = HttpContext.Current.Request["Ids"];
            string Note = HttpContext.Current.Request["Note"];
            try
            {
                string[] strArrays = Id.Split(new char[] { ',' });
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    string details = strArrays[i];
                    if (!string.IsNullOrWhiteSpace(details))
                    {
                        try
                        {
                            int num = int.Parse(details);
                            Buildings building = this.db.Buildings.FirstOrDefault<Buildings>((Buildings x) => x.ID == num);
                            building.RejectNote = Note;
                            building.Status = "Rejected";
                            this.db.SaveChanges();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void RejectSingleListing()
        {
            int num = int.Parse(HttpContext.Current.Request["Id"]);
            string Note = HttpContext.Current.Request["Note"];
            try
            {
                Buildings building = this.db.Buildings.FirstOrDefault<Buildings>((Buildings x) => x.ID == num);
                building.RejectNote = Note;
                building.Status = "Rejected";
                this.db.SaveChanges();
            }
            catch (Exception)
            {
            }
        }

        public async Task<int> RequestPhotosEmail(int Id)
        {

            try
            {
                Buildings building = await this.db.Buildings.FirstOrDefaultAsync<Buildings>((Expression<Func<Buildings, bool>>)(x => x.ID == Id));
                string buildingAccess;
                if (building.Type == "Sale")
                    buildingAccess = (await this.db.BuildingSales.FirstOrDefaultAsync<BuildingSale>((Expression<Func<BuildingSale, bool>>)(x => x.BuildingsID == (int?)Id))).Building_Access;
                else
                    buildingAccess = (await this.db.BuildingRents.FirstOrDefaultAsync<BuildingRent>((Expression<Func<BuildingRent, bool>>)(x => x.BuildingsID == (int?)Id))).Building_Access;
                string str1 = "<body bgcolor='white'><h3>Building Unit: </h3>" + building.Unit + "<br/><h3>Building Address: </h3>" + building.Address + "<br/><h3>Building Access: </h3>" + buildingAccess + "<br/> <br/><br/></body>";
                MailAddress from = new MailAddress("info@dallien.com", "By Dallien.com");
                MailAddress to = new MailAddress("info@dallien.com");
                string password = "infome212";
                string str2 = str1;
                new SmtpClient()
                {
                    Host = "relay-hosting.secureserver.net",
                    Port = 25,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = ((ICredentialsByHost)new NetworkCredential(from.Address, password))
                }.Send(new MailMessage(from, to)
                {
                    IsBodyHtml = true,
                    Subject = "ContactUs email by Users",
                    Body = str2
                });
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        [AllowAnonymous]
        public int SendContactEmail(EmailDetails email)
        {
            int num;
            string message = string.Concat(new string[] { "<body bgcolor='white'><h3>First Name: </h3>", email.FirstName, "<br/><h3>Last Name: </h3>", email.LastName, "<br/><h3>Email: </h3>", email.Email, "<br/><h3>Phone: </h3>", email.Phone, "<br/><h3>Description: </h3>", email.Description, "<br/> <br/><br/></body>" });
            try
            {
                MailAddress sender = new MailAddress("info@dallien.com", "By Dallien.com");
                MailAddress rec = new MailAddress("info@dallien.com");
                string pass = "infome212";
                string body = message;
                SmtpClient smtp = new SmtpClient()
                {
                    Host = "relay-hosting.secureserver.net",
                    Port = 25,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(sender.Address, pass)
                };
                smtp.Send(new MailMessage(sender, rec)
                {
                    IsBodyHtml = true,
                    Subject = "ContactUs email by Users",
                    Body = body
                });
                num = 1;
            }
            catch (Exception)
            {
                num = 0;
            }
            return num;
        }

        [AllowAnonymous]
        public int SetSession(FilterBox filter)
        {
            Others.SetFilterSession(filter);
            return 1;
        }

        public int UpdateAdmin(Admins temp)
        {
            return AdminsClass.UpdateRecord(temp);
        }

        [AllowAnonymous]
        public async Task<int> UpdatePassword(string code, LoginViewModel model)
        {
            WebApisController webApisController = this;
            if (!webApisController.ModelState.IsValid)
                return 0;
            try
            {
                Agents user = await webApisController.db.Agents.FirstOrDefaultAsync<Agents>((Expression<Func<Agents, bool>>)(x => x.EmailField.Equals(model.Email) && x.SecurityCode.Equals(code)));
                if (user == null)
                    return 0;
                try
                {
                    HttpContext.Current.Response.Cookies.Add(new HttpCookie("AgentEatSleepUser1234hytusksdbsdfasdjasdidasdijnasd", FormsAuthentication.Encrypt(new FormsAuthenticationTicket(0, user.ID.ToString(), DateTime.Now, DateTime.Now.AddMinutes(525600.0), false, user.Path)))
                    {
                        Domain = FormsAuthentication.CookieDomain,
                        Path = FormsAuthentication.FormsCookiePath,
                        HttpOnly = true,
                        Secure = FormsAuthentication.RequireSSL
                    });
                    FormsAuthentication.SetAuthCookie(user.EmailField, false);
                }
                catch (Exception)
                {
                }
                user.PasswordField = model.Password;
                webApisController.db.Entry<Agents>(user).State = EntityState.Modified;
                int num = await webApisController.db.SaveChangesAsync();
                return user.ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateSecurityLevel(SecurityLevel SL)
        {
            return SecurityClass.UpdateSecurityLevel(SL);
        }

        public class ESR
        {
            public string Name
            {
                get;
                set;
            }

            public ESR()
            {
            }
        }
    }
}
