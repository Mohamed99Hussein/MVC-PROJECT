using Data_Access_Layer.Data.Contexts;
using Data_Access_Layer.Data.DataSeeding;
using Data_Access_Layer.Repositories.Classes;
using Data_Access_Layer.Repositories.Interfaces;
using Data_Access_Layer.Unit_Of_Work.Class;
using Data_Access_Layer.Unit_Of_Work.Interface;
using Microsoft.EntityFrameworkCore;

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



            var app = builder.Build();

            #region Migrate Database - Data Seeding

            using var Scope = app.Services.CreateScope();
            var DbContext = Scope.ServiceProvider.GetRequiredService<GymSystemDBContext>();
            var PendingMigrations = DbContext.Database.GetPendingMigrations();
            if (PendingMigrations != null && PendingMigrations.Any())
                DbContext.Database.Migrate();

            GymDBContextSeedData.SeedData(DbContext);

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

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
