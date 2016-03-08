using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DreamJob.Models
{
    // DBcontext class : handles all interaction with the MySQL DB
    public class DBEntities : DbContext
    {
        public DBEntities() : base(nameOrConnectionString: "LocalDBconnection") { }
        public DbSet<Article> Article { get; set; }
    }
}