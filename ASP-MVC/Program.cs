using Common_API.Repositories;
using D = DAL_API;
using B = BLL_API;

namespace ASP_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IUserRepository<D.Entities.User>, D.Services.UserService>(); 
            builder.Services.AddScoped<IUserRepository<B.Entities.User>, B.Services.UserService>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            //app.MapControllerRoute(
            //    name:"frenchHome",
            //    pattern:"accueil/",
            //    defaults : new { controller = "Home", action = "Index"}
            //    );


            app.MapControllerRoute(
                name: "frenchPrivacy",
                pattern: "conditions/",
                defaults: new { controller = "Home", action = "Privacy" }
                );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
