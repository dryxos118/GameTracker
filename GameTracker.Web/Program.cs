using GameTracker.Shared.Infrastructure.Persistence;
using GameTracker.Web.Components;
using GameTracker.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

builder.Services.AddLocalization();

builder.Services.AddControllers();

builder.Services.AddGameTrackerServices();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=gametracker.database"));

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlite("Data Source=gametracker.database"), ServiceLifetime.Scoped);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseStaticFiles();

app.UseAntiforgery();

app.UseStatusCodePagesWithRedirects("/not-found");

app.AddAppLocalization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

using (IServiceScope scope = app.Services.CreateScope())
{
    AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.Migrate();
}

app.Run();