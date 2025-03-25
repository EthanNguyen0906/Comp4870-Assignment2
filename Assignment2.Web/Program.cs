using Assignment2.Web;
using Assignment2.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// 1. Register Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 2. Register HttpClient so @inject HttpClient Http works
builder.Services.AddScoped<HttpClient>(sp =>
{
    return new HttpClient { BaseAddress = new Uri("http://localhost:5144/") };
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

// 3. Map Razor Components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();