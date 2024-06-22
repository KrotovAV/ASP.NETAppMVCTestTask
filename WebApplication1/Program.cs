using Microsoft.Extensions.FileProviders;
using WebApplication1.Context;
using WebApplication1.Entities;
using WebApplication1.Interfaces;
using WebApplication1.Repositories;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<AppDBContext>();
            builder.Services.AddTransient<IRepository<Contact>, ContactRepository>();
            builder.Services.AddTransient<IRepository<Category>, CategoryRepository>();
            builder.Configuration.GetConnectionString("Connection");

            builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));


            builder.Services.AddMvc();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Contacts}/{action=Index}/{id?}");

            app.Run();
        }
    }
}