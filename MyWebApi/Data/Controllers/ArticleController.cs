using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ArticlesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            return await _context.Articles.ToListAsync();
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // POST: api/Articles
        [HttpPost]
        public async Task<ActionResult<Article>> CreateArticle(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            // Returns a 201 Created response with the location of the new resource
            return CreatedAtAction(nameof(GetArticle), new { id = article.ArticleId }, article);
        }

        // PUT: api/Articles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(int id, Article article)
        {
            if (id != article.ArticleId)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.ArticleId == id);
        }
    }
}
