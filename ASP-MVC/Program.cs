using Common_API.Repositories;
using D = DAL_API;
using B = BLL_API;
using ASP_MVC.Handlers;

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
            
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<SessionManager>();

            builder.Services.AddControllersWithViews();

            //builder.Services.AddDistributedMemoryCache();
            builder.Services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = builder.Configuration.GetConnectionString("CacheMVC");
                options.SchemaName = "dbo";
                options.TableName = "cachemvc";
            });
            builder.Services.AddSession(options => {
                options.Cookie.Name = "CloudMVCSession";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });

            builder.Services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.Secure = CookieSecurePolicy.Always;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSession();
            app.UseCookiePolicy();

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
