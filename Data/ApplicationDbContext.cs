using BookApiLibary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiLibary.Data
{
    public class ApplicationDbContext : DbContext
    {
        //properties
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Login> Logins { get; set; }

        //Constructor
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
