using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyShop.Models.Entities;
using MyShop.Services.Context;
using MyShop.Services.Repository.Interface;
using MyShop.Services.Repository;
using MyShop.Services.Service;
using MyShop.Services.Service.Interface;

namespace MyShop
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            //dB context connection string...
            services.AddDbContext<MyShopContext>(options => options.UseSqlServer(Configuration.GetConnectionString("localConnection")));
            services.AddDefaultIdentity<MyShopUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<MyShopContext>();


            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            //Identity service
            // services.AddIdentity<MyShopUser, IdentityRole>().AddEntityFrameworkStores<MyShopContext>();
            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            });

       
            // Add services to the container.
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddMvc();

     services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();

            //Add Repository 
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
      
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            
            app.MapControllerRoute(
                name: "category",
                pattern: "{controller=Product}/{action=Category}/{id?}");


            app.MapRazorPages();

            app.Run();
        }
    }
}
