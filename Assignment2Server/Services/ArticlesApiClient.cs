using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Assignment2Library.Data.Models;

namespace Assignment2Server.Services;

public class ArticlesApiClient
{
    private readonly HttpClient _http;

    public ArticlesApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Article>> GetValidArticlesAsync()
    {
        var result = await _http.GetFromJsonAsync<List<Article>>("api/articles");
        return result ?? new List<Article>();
    }

    public async Task CreateArticleAsync(Article article)
    {
        var response = await _http.PostAsJsonAsync("api/articles", article);
        response.EnsureSuccessStatusCode();
    }

    public async Task<Article?> GetByIdAsync(int id) =>
        await _http.GetFromJsonAsync<Article>($"api/articles/{id}");

    public async Task DeleteAsync(int id) =>
        await _http.DeleteAsync($"api/articles/{id}");


    
}
