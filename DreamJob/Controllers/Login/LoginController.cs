using DreamJob.Models;
using DreamJob.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DreamJob.Controllers.Login
{

    public class LoginController : Controller
    {
        private Dal dal;

        // Constructeur externe
        public LoginController() : this(new Dal())
        {

        }

        // Constructeur interne
        private LoginController(Dal dalIoc)
        {
            dal = dalIoc;
        }

        // Route GET vers la vue partielle d'authentification
        public ActionResult Index()
        {
            HomeViewModel viewModel = new HomeViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var name = HttpContext.User;
                viewModel.Utilisateur = dal.ObtenirUtilisateurParId(HttpContext.User.Identity.Name);
            }
            return PartialView(viewModel);
        }

        // Route POST vers la vue partielle d'authentification (appelee lors d'une connexion)
        [HttpPost]
        public ActionResult Index(HomeViewModel viewModel)
        {
            // Si les donnees d'identification ont un format valide ...
            if (ModelState.IsValid)
            {
                Utilisateur utilisateur = dal.Authentifier(viewModel.Utilisateur.Prenom, viewModel.Utilisateur.MotDePasse);
                // et que l'utilisateur est dans la base de donnee
                if (utilisateur != null)
                {
                    // identifier l'utilisateur par un cookie
                    FormsAuthentication.SetAuthCookie(utilisateur.Id.ToString(), false);
                    viewModel.Authentifie = true;
                    // reactualiser la vue partielle d'indentification
                    return PartialView(viewModel);
                }
                // Si l'utilisateur n'est pas dans la base de donnee ou que le mot de passe est incorrect, envoyer une erreur
                ModelState.AddModelError("Utilisateur.Prenom", "Prénom et/ou mot de passe incorrect(s)");
            }
            // Si le format n'est pas valide, renvoyer la ve partielle d'identification avec une erreur
            return PartialView(viewModel);
        }

        // Route GET pour creer un compte (l'utilisateur a clique sur le lien pour creer un nouveau compte)
        public ActionResult CreerCompte()
        {
            return View();
        }

        // Route POST pour creer un compte (l'utilisateur envoie un nom et un mot de passe pour creer un compte)
        [HttpPost]
        public ActionResult CreerCompte(Utilisateur utilisateur)
        {
            // Si les donnees d'identification ont un format valide...
            if (ModelState.IsValid)
            {
                // ajouter l'utilisateur a la base de donnee
                int id = dal.AjouterUtilisateur(utilisateur.Prenom, utilisateur.MotDePasse);
                // identifier l'utilisateur par un cookie
                FormsAuthentication.SetAuthCookie(id.ToString(), false);
                // redirection vers la page d'origine
                return Redirect("/");
            }
            // sinon on reactualise la page de creation de compte
            return View(utilisateur);
        }

        // Route pour la deconnexion
        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

        // Methode permttant de determiner si le nom d'utilisateur existe deja
        [HttpPost]
        public JsonResult doesUserNameExist(Utilisateur Utilisateur)
        {
            var user = dal.ObtenirUtilisateur(Utilisateur.Prenom);
            return Json(user == null);
        }
    }
}