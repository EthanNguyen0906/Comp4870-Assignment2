using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Assignment2Library.Data.Models;

namespace Assignment2Library.Data
{
    public class YourDbContext : IdentityDbContext<User>
    {
        public YourDbContext(DbContextOptions<YourDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
       
    }
}