using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DreamJob.Models
{

    public class Utilisateur
    {
        // Clef primaire dans la base de donnee (avec autoincrement)
        public int Id { get; set; }

        // Annotation signalant que ce champ ne peux pas etre vide
        [Required]
        // Annotation interdisant d'utiliser un nom d'utilisateur qui se trouve deja dans la base
        [Remote("doesUserNameExist", "Login", HttpMethod = "POST", ErrorMessage = "Ce prénom existe déjà dans la base. Merci d'ajouter l'initiale de votre nom.")]
        // Annnotation precisant le nom du champ qui sera affiche dans le formulaire
        [Display(Name = "Prénom")] 
        public string Prenom { get; set; }

        [Required]
        [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }
    }
}