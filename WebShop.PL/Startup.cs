using AutoMapper;
using EmailService;
using EmailService.CustomTokenProviders;
using EmailService.CustomValidators;
using EmailService.Factory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using System.Linq;
using WebShop.DAL.Auth;
using WebShop.DAL.Datacontext;
using WebShop.Logic.Service;
using WebShop.PL.Filters;

namespace WebShop.PL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            /********************************************/
            services.AddDbContext<DatabaseContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionPloto")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = false;
                options.User.AllowedUserNameCharacters = Configuration["AllowedCharacter"];
                options.SignIn.RequireConfirmedEmail = false;
                options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
               // options.Lockout.MaxFailedAccessAttempts = 3;

            }).AddEntityFrameworkStores<DatabaseContext>()
               .AddPasswordValidator<CustomPasswordValidator<ApplicationUser>>()
               .AddDefaultTokenProviders()
               
               .AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>("emailconfirmation");
               
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
              opt.TokenLifespan = TimeSpan.FromHours(2));

            services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromDays(3));
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomClaimsFactory>();
            services.AddAuthentication()
              .AddGoogle("google", opt =>
              {
                  var googleAuth = Configuration.GetSection("Authentication:Google");

                  opt.ClientId = googleAuth["ClientId"];
                  opt.ClientSecret = googleAuth["ClientSecret"];
                  opt.SignInScheme = IdentityConstants.ExternalScheme;
              });
            services.AddAutoMapper(typeof(Startup));

            var emailConfig = Configuration
                .GetSection("hotmailConfiguration")
                .Get<EmailConfiguration>();

           
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICakeRepository, CakeRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ISoldItems, SoldItems>();
            services.AddTransient<ICakeReviewRepository, CakeReviewRepository>();
            services.AddSession();
            //Filters
            //services.AddScoped<TimerAction>();
            services.AddAntiforgery();
            services.AddControllersWithViews(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
          /*  services.AddControllersWithViews(config => { config.Filters.AddService(typeof(TimerAction)); });*///services.AddMvc(); would also work still, add globaly Timeraction filter as parameter
                                                                                                       //Claims-based
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", builder => builder.RequireRole("Admin"));
                options.AddPolicy("ManageCakes", policy => policy.RequireClaim("Manage Cakes", "Manage Cakes"));
                options.AddPolicy("User", builder => builder.RequireRole("User"));
                options.AddPolicy("MinimumOrderAge", policy => policy.Requirements.Add(new MinimumOrderAgeRequirement(18)));
            });
          
            //response compression with gzip
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes =
                    ResponseCompressionDefaults.MimeTypes.Concat(new[] { "imagejpeg" });
            });

            services.Configure<GzipCompressionProviderOptions>
                (options => options.Level =
                    System.IO.Compression.CompressionLevel.Optimal);
            services.AddMemoryCache();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
                                RoleManager<IdentityRole> roleManager, IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            Log.Logger =
              new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile(Path.Combine(env.ContentRootPath + @"\Log\Cakeshoplogs-{Date}.txt"))
                .CreateLogger();

            loggerFactory.AddSerilog();
            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            DataBaseInitializer.SeedData(userManager, roleManager);
        }
    }
}
