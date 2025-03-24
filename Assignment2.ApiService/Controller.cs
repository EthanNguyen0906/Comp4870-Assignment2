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

    // GET: api/users
    [HttpGet]
    public async Task<IActionResult> GetValidUsers()
    {
        var validUsers = await _context.Users.ToListAsync();
        return Ok(validUsers);
    }
}
