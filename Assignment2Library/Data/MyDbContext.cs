namespace Assignment2Library.Data;
using Microsoft.EntityFrameworkCore;
using Assignment2Library.Data.Models;

public class YourDbContext : DbContext
{
    public YourDbContext(DbContextOptions<YourDbContext> options)
        : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; }

    public DbSet<User> Users { get; set; }
}
