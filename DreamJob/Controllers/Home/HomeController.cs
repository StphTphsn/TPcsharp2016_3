using DreamJob.Models;
using DreamJob.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DreamJob.Controllers.Home
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Dal dal = new Dal();
            HomeViewModel vm = new HomeViewModel
            {
                ListeDesArticles = dal.ObtientTousLesArticles()
            };
            return View(vm);
        }
    }
}