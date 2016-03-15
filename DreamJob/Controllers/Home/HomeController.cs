using DreamJob.Models;
using DreamJob.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DreamJob.Controllers.Home
{
    
    public class HomeController : Controller
    {
        // Route vers la page des offres d'emploi
        public ActionResult Index()
        {
            Dal dal = new Dal();
            HomeViewModel vm = new HomeViewModel
            {
                ListeDesArticles = dal.ObtientTousLesArticles()
            };
            return View(vm);
        }

        // Route autorisee seulement aux utilisateurs authentifies
        [Authorize]
        // Route pour voter pour une offre d'emploi
        public ActionResult UpVote(int id)
        {
            Dal dal = new Dal();
            dal.Vote(id,1);
            HomeViewModel vm = new HomeViewModel
            {
                ListeDesArticles = dal.ObtientTousLesArticles()
            };
            return View("Index",vm);
        }

        // Route autorisee seulement aux utilisateurs authentifies
        [Authorize]
        // Route pour voter contre une offre d'emploi
        public ActionResult DownVote(int id)
        {
            Dal dal = new Dal();
            dal.Vote(id,-1);
            HomeViewModel vm = new HomeViewModel
            {
                ListeDesArticles = dal.ObtientTousLesArticles()
            };
            return View("Index", vm);
        }
    }
}