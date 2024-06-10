using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Data;
using Online_Book_Store.Data.Interfaces;
using Online_Book_Store.Data.Services;
using Online_Book_Store.Models;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();


        // VT için gerekli olan AppDbContext tanýmý yapýlýyor.
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

        builder.Services.AddScoped<IBooksService, BooksService>();
        builder.Services.AddScoped<IAuthorsService, AuthorsService>();
        builder.Services.AddScoped<IPublishersService, PublishersService>();


        builder.Services
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
        builder.Services.AddMemoryCache();
        builder.Services.AddSession(); //  68
        builder.Services.AddAuthentication(options => {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        });



        builder.Services.AddControllersWithViews();
        
        
        //
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

        app.UseRouting();

        // 50
        app.UseSession(); // 68
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Books}/{action=Index}/{id?}");

        AppDbInitializer.Seed(app);

        AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();

        app.Run();

        
       
        
        
    }
}