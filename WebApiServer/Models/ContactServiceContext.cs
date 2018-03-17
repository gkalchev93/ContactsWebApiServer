using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApiServer.Models
{
    public class ContactServiceContext : DbContext
    {    
        public ContactServiceContext() : base("name=ContactServiceContext")
        {
        }

        public System.Data.Entity.DbSet<WebApiServer.Models.Contact> Contacts { get; set; }
    }
}
