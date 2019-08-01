using System;
using System.Collections.Generic;
using System.Text;
using AgregatorLinkow.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgregatorLinkow.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Link> Link { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Plus> Plus { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
