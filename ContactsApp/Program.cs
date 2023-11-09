using ContactsApp.Data;
using ContactsApp.Helpers;
using ContactsApp.Repos;
using ContactsApp.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ContactsApp {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddEntityFrameworkSqlServer().AddDbContext<AppDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );

            builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();

            builder.Services.AddScoped(o => o.GetService<IHttpContextAccessor>().HttpContext.Session);
            builder.Services.AddScoped<IContactRepos, ContactRepos>();
            builder.Services.AddScoped<IUserRepos, UserRepos>();
            builder.Services.AddScoped<ISessionTemp, SessionTemp>();
            builder.Services.AddScoped<IEmail, Email>();

            builder.Services.AddSession(o => {
                o.Cookie.HttpOnly= true;
                o.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}