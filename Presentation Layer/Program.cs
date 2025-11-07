using Business_Logic_Layer.AutoMapper;
using Business_Logic_Layer.Services.AccountService;
using Business_Logic_Layer.Services.AnalyticsService.Class;
using Business_Logic_Layer.Services.AnalyticsService.Interface;
using Business_Logic_Layer.Services.AttachmentService;
using Business_Logic_Layer.Services.BookingService.Class;
using Business_Logic_Layer.Services.BookingService.Interface;
using Business_Logic_Layer.Services.MemberService.Classes;
using Business_Logic_Layer.Services.MemberService.Interfaces;
using Business_Logic_Layer.Services.MembershipService.Class;
using Business_Logic_Layer.Services.MembershipService.Interface;
using Business_Logic_Layer.Services.PlanService.Class;
using Business_Logic_Layer.Services.PlanService.Interface;
using Business_Logic_Layer.Services.SessionService.Class;
using Business_Logic_Layer.Services.SessionService.Interface;
using Business_Logic_Layer.Services.TranierService.Class;
using Business_Logic_Layer.Services.TranierService.Interface;
using Data_Access_Layer.Data.Contexts;
using Data_Access_Layer.Data.DataSeeding;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.Classes;
using Data_Access_Layer.Repositories.Interfaces;
using Data_Access_Layer.Unit_Of_Work.Class;
using Data_Access_Layer.Unit_Of_Work.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation_Layer.AttachmentService;

namespace Presentation_Layer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymSystemDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //builder.Services.AddScoped(typeof(IgenericRepository<>),typeof(GenericRepository<>));

            //builder.Services.AddScoped<IPlanRepository,PlanRepository>();
            
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();   
            builder.Services.AddScoped<ISessionRepository,SessionRepository>();
            builder.Services.AddAutoMapper(x=>x.AddProfile(new MappingProfiles()));
            builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
            builder.Services.AddScoped<IMemberService,MemberService>();
            builder.Services.AddScoped<ITrainerService,TrainerService>();
            builder.Services.AddScoped<IPlanService, PlanService>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IAccountService,AccountService>();
            builder.Services.AddScoped<IAttachmentService,Business_Logic_Layer.Services.AttachmentService.AttachmentService>();
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>
                (config => config.User.RequireUniqueEmail = true)
                   .AddEntityFrameworkStores<GymSystemDBContext>();
            builder.Services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/Account/Login";
                opt.AccessDeniedPath = "/Account/AccessDenied";
            }  
            );

            builder.Services.AddScoped<IMembershipRepository,MembershipRepository>();
            builder.Services.AddScoped<IMembershipService,MembershipService>();

            builder.Services.AddScoped<IBookingRepository,BookingRepository>();
            builder.Services.AddScoped<IBookingService,BookingService>();





            var app = builder.Build();

            #region Migrate Database - Data Seeding

            using var Scope = app.Services.CreateScope();
            var DbContext = Scope.ServiceProvider.GetRequiredService<GymSystemDBContext>();
            


            var PendingMigrations = DbContext.Database.GetPendingMigrations();
            if (PendingMigrations != null && PendingMigrations.Any())
                DbContext.Database.Migrate();
           
            GymDBContextSeedData.SeedData(DbContext);
            var roleManager = Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = Scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            IdentityDbContextSeeding.SeedData(roleManager,userManager);



            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}")
                .WithStaticAssets();
            
            app.Run();
        }
    }
}
