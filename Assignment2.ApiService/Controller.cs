using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment2Api.Data;
using Assignment2Library.Data.Models;

namespace Assignment2.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly YourDbContext _context;
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(YourDbContext context, ILogger<ArticlesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/articles
        [HttpGet]
        public async Task<IActionResult> GetValidArticles()
        {
            _logger.LogInformation("Fetching valid articles");

            var validArticles = await _context.Articles
                .Where(a => DateTime.Now < a.EndDate)
                .ToListAsync();

            return Ok(validArticles);
        }

        // GET: api/articles/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetArticle(int id)
        {
            _logger.LogInformation($"Fetching article with id {id}");

            var article = await _context.Articles.FindAsync(id);

            if (article == null)
            {
                _logger.LogWarning($"Article with id {id} not found");
                return NotFound();
            }

            if (!IsValid(article))
            {
                _logger.LogWarning($"Article with id {id} is not valid");
                return BadRequest("Article is no longer valid");
            }

            return Ok(article);
        }

        private bool IsValid(Article article)
        {
            return article != null && DateTime.Now < article.EndDate;
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
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
    }
}
