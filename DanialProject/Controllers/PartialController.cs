using System;
using System.Web.Mvc;

namespace DanialProject.Controllers
{
	public class PartialController : Controller
	{
		public PartialController()
		{
		}

		[ChildActionOnly]
		public ActionResult AsileBar()
		{
			return base.PartialView();
		}

		[ChildActionOnly]
		public ActionResult EmailTemplates()
		{
			return base.PartialView();
		}

		[ChildActionOnly]
		public ActionResult LoginForm()
		{
			return base.PartialView();
		}

		[ChildActionOnly]
		public ActionResult TopNaveBar()
		{
			return base.PartialView();
		}
	}
}