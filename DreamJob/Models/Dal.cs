using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DreamJob.Models
{
    // Cette classe contient toutes les methodes qui s'apliquent sur des objets de la base de donnee
    public class Dal
    {
        // objet d'EntityFramework permettant de faire le lien vers la base de donnee
        private DBEntities context;

        // Contructeur
        public Dal()
        {
            context = new DBEntities();
        }

        public List<Article> ObtientTousLesArticles()
        {
            var date = DateTime.Now.AddMonths(-3);
            return context.Article.OrderByDescending(a => a.created).Where(a => DateTime.Compare(a.created,date)==1).ToList();
        }

        public void Vote(int id, int direction)
        {
            var article = context.Article.FirstOrDefault(art => art.id==id);
            if (direction ==1)
                article.up_votes += 1;
            if (direction == -1)
                article.down_votes += 1;
            context.SaveChanges();
        }


        public void Dispose()
        {
            context.Dispose();
        }

        public int AjouterUtilisateur(string nom, string motDePasse)
        {
            string motDePasseEncode = EncodeMD5(motDePasse);
            Utilisateur utilisateur = new Utilisateur { Prenom = nom, MotDePasse = motDePasseEncode };
            context.Utilisateurs.Add(utilisateur);
            context.SaveChanges();
            return utilisateur.Id;
        }

        public Utilisateur Authentifier(string nom, string motDePasse)
        {
            string motDePasseEncode = EncodeMD5(motDePasse);
            return context.Utilisateurs.FirstOrDefault(u => u.Prenom == nom && u.MotDePasse == motDePasseEncode);
        }

        public Utilisateur ObtenirUtilisateur(string prenom)
        {
            return context.Utilisateurs.FirstOrDefault(u => u.Prenom == prenom);
        }

        public Utilisateur ObtenirUtilisateurParId(string stringId)
        {
            int Id = Int32.Parse(stringId);
            return context.Utilisateurs.FirstOrDefault(u => u.Id == Id);
        }

        // On ne sauvegarde pas directement le mot de passe de l'utilisateur dans la base de donnee, on l'encrypte avant.
        private string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "DreamJob" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

    }
}