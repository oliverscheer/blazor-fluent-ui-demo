using ConcertDatabase.Components;
using ConcertDatabase.Contexts;
using ConcertDatabase.Entities;
using ConcertDatabase.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MusicDbContext>()
    .AddDefaultTokenProviders();

services.Configure<IdentityOptions>(opts =>
{
    opts.User.RequireUniqueEmail = true;
    opts.Password.RequiredLength = 8;
});

services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


// Add services to the container.
services.AddRazorComponents()
    .AddInteractiveServerComponents();
services.AddFluentUIComponents();

services.AddDataGridEntityFrameworkAdapter();

services.AddDbContextFactory<MusicDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("MusicDbContext"))
    );

services.AddTransient<ArtistRepository>();
services.AddTransient<ConcertRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MusicDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
