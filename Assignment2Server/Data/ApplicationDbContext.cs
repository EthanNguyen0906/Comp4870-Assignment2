using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Assignment2Library.Data.Models;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Create a PasswordHasher to hash the plaintext passwords
        var hasher = new PasswordHasher<User>();

        // Create two user objects with unique Ids and SecurityStamps
        var user1 = new User
        {
            Id = "b1933961-7c4a-47ca-9bd4-f2532c3fdd6f", // fixed GUID for user1
            FirstName = "John",
            LastName = "Doe",
            UserName = "john.doe@example.com",
            NormalizedUserName = "JOHN.DOE@EXAMPLE.COM",
            Email = "john.doe@example.com",
            NormalizedEmail = "JOHN.DOE@EXAMPLE.COM",
            EmailConfirmed = true,
            Approved = true,
            Role = "Contributor",
            SecurityStamp = "b1933961-7c4a-47ca-9bd4-f2532c3fdd6f" // fixed stamp for user1
        };

        // Hash the password for user1
        user1.PasswordHash = hasher.HashPassword(user1, "Password123!");

        var user2 = new User
        {
            Id = "a27b5b97-aac2-4b7a-9b06-b2f633f49c68", // fixed GUID for user2 (different from user1)
            FirstName = "Jane",
            LastName = "Smith",
            UserName = "jane.smith@example.com",
            NormalizedUserName = "JANE.SMITH@EXAMPLE.COM",
            Email = "jane.smith@example.com",
            NormalizedEmail = "JANE.SMITH@EXAMPLE.COM",
            EmailConfirmed = true,
            Approved = true,
            Role = "Admin",
            SecurityStamp = "a27b5b97-aac2-4b7a-9b06-b2f633f49c68" // fixed stamp for user2
        };

        // Hash the password for user2
        user2.PasswordHash = hasher.HashPassword(user2, "AnotherPass456!");

        // Seed the data
        builder.Entity<User>().HasData(user1, user2);
    }
}
