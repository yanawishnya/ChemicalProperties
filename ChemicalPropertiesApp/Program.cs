using ChemicalPropertiesApp;
using Microsoft.EntityFrameworkCore;
using ChemicalPropertiesApp.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin.Security.Cookies;
using CookieAuthenticationDefaults = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MisisContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("misis")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Usage of Dapper + SQL Server below here.
builder.Services.AddTransient<IUserRepository, UserRepositorySqlServer>(provider =>
    new UserRepositorySqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/Account/Login");
});

/*builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options => { options.Cookie.Name = "MyCookieAuth"; });*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using var scopeServ = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
SeedData.EnsureSeedData(scopeServ.ServiceProvider);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();