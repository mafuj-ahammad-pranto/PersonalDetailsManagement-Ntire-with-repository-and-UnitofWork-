using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;  // Your actual DbContext
using RepositoryLayer.Interfaces;
using RepositoryLayer.Implementations;

var builder = WebApplication.CreateBuilder(args);

// 1. Read connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Register YOUR DbContext (from DataAccessLayer)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. Register Identity with YOUR DbContext
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>(); // This uses YOUR DbContext

// 4. Register Repository Pattern
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// 5. Add MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

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