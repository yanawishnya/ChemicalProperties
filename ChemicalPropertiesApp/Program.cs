using ChemicalPropertiesApp;
using ChemicalPropertiesApp.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using ChemicalPropertiesApp.Data;
using ChemicalPropertiesApp.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RubricsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RubricsContext")));

builder.Services.AddDbContext<TableContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TableContext")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

/*
builder.Services.AddDbContext<AllTablesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AllTablesContext")));*/

var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<ChemicalPropertiesAppUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequiredLength = 5;
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<AuthDbContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<TablesContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("TablesContext")));


// Usage of Dapper + SQL Server below here.
builder.Services.AddTransient<IUserRepository, UserRepositorySqlServer>(provider =>
    new UserRepositorySqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options => { options.Cookie.Name = "MyCookieAuth"; });

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
    var rubricContext = services.GetRequiredService<RubricsContext>();
    var allTablesContext = services.GetRequiredService<TablesContext>();
    context.Database.EnsureCreated();
    rubricContext.Database.EnsureCreated();
    allTablesContext.Database.EnsureCreated();
    DbInitializer.TableInitialize(context);
    DbInitializer.RubricsInitialize(rubricContext);
    DbInitializer.AllTablesInitialize(allTablesContext);
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