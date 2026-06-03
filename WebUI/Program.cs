using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Implementations;
using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using WebUI.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Read connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Register YOUR DbContext (from DataAccessLayer)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. Register Identity with YOUR DbContext
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;

    // ---- LOGIN FIX: Cookie expires when browser is closed ----
    // This prevents "always logged in" when re-running the project.
    // The cookie will only persist if the user checks "Remember me".
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// ---- LOGIN FIX: Set cookie to session-only by default ----
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie is deleted when the browser closes (session cookie)
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
    // Redirect to login if not authenticated
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

// 4. Register Repository Pattern
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPersolaDetails, PersolaDetails>();

// 5. Add MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ---- Seed roles and admin user on startup ----
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    await DbInitializer.SeedRolesAsync(roleManager);
    await DbInitializer.SeedAdminAsync(userManager);
}

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
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
