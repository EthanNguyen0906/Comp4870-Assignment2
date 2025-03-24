using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment2Api.Data;
using Assignment2Library.Data.Models;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController : ControllerBase
{
    private readonly YourDbContext _context;

    public ArticlesController(YourDbContext context)
    {
        _context = context;
    }

    // GET: api/articles/articles
    [HttpGet("articles")]
    public async Task<IActionResult> GetValidArticles(){
        Console.WriteLine("Hi");
        var validArticles = await _context.Articles
            .Where(a => DateTime.Now < a.EndDate)
            .ToListAsync();
        return Ok(validArticles);
    }

    public bool isValid(Article article){
    if (article == null){
        return false;
    }
    
    DateTime currentDate = DateTime.Now;
    if (currentDate >= article.EndDate){
        return false;
    }

    return true;
    }
}

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly YourDbContext _context;

    public UsersController(YourDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(
        [FromBody] RegisterViewModel.InputModel input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Verify password match
        if (input.Password != input.ConfirmPassword)
        {
            return BadRequest("Password and confirmation password do not match.");
        }

        // Map to User entity
        var user = new User
        {
            Email = input.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(input.Password),
            FirstName = input.FirstName,
            LastName = input.LastName,
            Approved = false,
            Role = "Contributor"
        };

        // Check for existing user
        if (await _context.Users.AnyAsync(u => u.Email == user.Email))
        {
            return Conflict("Email already registered");
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Registration successful" });
    }
}