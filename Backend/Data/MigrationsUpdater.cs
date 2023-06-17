using System.Net.Mime;
using Microsoft.EntityFrameworkCore;

namespace ExMoney.Backend.Data
{
    public class MigrationsUpdater
    {
        public static void ApplyPendingMigrations(IServiceProvider sp)
        {
            using IServiceScope servicesScope = sp.CreateScope();

            BackendDbContext context = servicesScope.ServiceProvider.GetRequiredService<BackendDbContext>();

            //drop database
            context.Database.EnsureDeleted();
            
            // create 
            if(!context.Database.EnsureCreated())
            {
                //apply all pending migrations
                context.Database.Migrate();
            }


        }
    }
}
