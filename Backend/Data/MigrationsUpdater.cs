using Microsoft.EntityFrameworkCore;

namespace ExMoney.Backend.Data
{
    public class MigrationsUpdater
    {
        public static void ApplyPendingMigrations(IServiceProvider sp)
        {
            using IServiceScope servicesScope = sp.CreateScope();

            BackendDbContext context = servicesScope.ServiceProvider.GetRequiredService<BackendDbContext>();
            //ensure database created
            _ = context.Database.EnsureCreated();
            //apply all pending migrations
            context.Database.Migrate();
        }
    }
}
