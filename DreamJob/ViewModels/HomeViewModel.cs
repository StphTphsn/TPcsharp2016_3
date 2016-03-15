using DreamJob.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// Viewmodel contenant tout les champs qui seront utilisees par les differentes vues du siteweb
namespace DreamJob.ViewModels
{
    public class HomeViewModel
    {
        public List<Models.Article> ListeDesArticles { get; set; }
        public Utilisateur Utilisateur { get; set; }
        public bool Authentifie { get; set; }

    }
}