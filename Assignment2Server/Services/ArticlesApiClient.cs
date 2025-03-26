using Assignment2Library.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Assignment2Server.Services
{
    public interface IArticleService
    {
        Task<List<Article>> GetValidArticlesAsync();
        Task CreateArticleAsync(Article article);
        Task<List<Article>> GetUserArticlesAsync();
        Task<Article?> GetArticleByIdAsync(int id);
        Task UpdateArticleAsync(Article article);
        Task DeleteAsync(int id);
    }

    public class ArticlesApiClient : IArticleService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public ArticlesApiClient(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Article>> GetValidArticlesAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Articles
                .AsNoTracking()
                .Where(a => DateTime.Now <= a.EndDate)
                .ToListAsync();
        }

        public async Task CreateArticleAsync(Article article)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.Articles.Add(article);
            await context.SaveChangesAsync();
        }

        public async Task<List<Article>> GetUserArticlesAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Articles
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Article?> GetArticleByIdAsync(int id)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Articles
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.ArticleId == id);
        }

        public async Task UpdateArticleAsync(Article article)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.Articles.Update(article);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var article = await context.Articles.FindAsync(id);
            if (article != null)
            {
                context.Articles.Remove(article);
                await context.SaveChangesAsync();
            }
        }
    }
}
