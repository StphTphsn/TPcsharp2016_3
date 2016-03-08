using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DreamJob.Models
{
    public class Dal
    {
        private DBEntities context;
        public Dal()
        {
            context = new DBEntities();
        }

        public List<Article> ObtientTousLesArticles()
        {
            Article art = new Article();
            return context.Article.OrderBy(a => a.title).ToList();
        }


        public void Dispose()
        {
            context.Dispose();
        }

    }
}