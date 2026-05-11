using Microsoft.AspNetCore.Identity;
using QueueManagement.Application.DependencyInjection;
using QueueManagement.Domain.Entities;
using QueueManagement.Persistence.DependencyInjection;
using QueueManagement.Web.Hubs;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";

    options.AccessDeniedPath =
        "/Account/AccessDenied";

    options.ExpireTimeSpan =
        TimeSpan.FromMinutes(30);

    options.SlidingExpiration = false;

    // IMPORTANT
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var userManager =
        scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

    var roleManager =
        scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(
            new IdentityRole("Admin"));
    }

    if (!await roleManager.RoleExistsAsync("Staff"))
    {
        await roleManager.CreateAsync(
            new IdentityRole("Staff"));
    }

    var adminUser =
        await userManager.FindByNameAsync("admin");

    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            FullName = "System Admin",
            UserName = "admin",
            Email = "admin@test.com"
        };

        await userManager.CreateAsync(
            adminUser,
            "Admin@123");

        await userManager.AddToRoleAsync(
            adminUser,
            "Admin");
    }
}
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}");
app.MapHub<QueueHub>("/queueHub");
app.Run();
