using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImfamousQuotes.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult NewGame()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Play()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}