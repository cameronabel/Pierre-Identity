using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

using Bakery.Models;

namespace Bakery;
class Program
{
  static void Main(string[] args)
  {

    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllersWithViews();

    builder.Services.AddDbContext<BakeryContext>(
                      dbContextOptions => dbContextOptions
                        .UseMySql(
                          builder.Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"]
                        )
                      )
                    );

    // New code below!!
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
              .AddEntityFrameworkStores<BakeryContext>()
              .AddDefaultTokenProviders();

    WebApplication app = builder.Build();

    // app.UseDeveloperExceptionPage();
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    // New code below!
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
      );

    app.Run();
  }
}