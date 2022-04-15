using System.Configuration;
using ChemicalPropertiesApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ChemicalPropertiesApp.Data;
using ChemicalPropertiesApp.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TableContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TableContext")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<ChemicalPropertiesAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AuthDbContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();

// Usage of Dapper + SQL Server below here.
builder.Services.AddTransient<IUserRepository, UserRepositorySqlServer>(provider =>
    new UserRepositorySqlServer(builder.Configuration.GetConnectionString("default")));

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TableContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

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