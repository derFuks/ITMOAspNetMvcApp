using ITMOAspNetMvcApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ITMOAspNetMvcApp
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            // при подключении к PostgreSQL могут быть косяки из-за пароля
            optionsBuilder.UseNpgsql("Host=pg-itmoado-itmo.k.aivencloud.com;Port=14106;Database=dbitmoasp;Username=karaev;Password=;SSL Mode=Require;");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
