using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdP.Data
{
    public class IdPInitializer
    {
        public static void InitializeData(IServiceProvider sp)
        {
            //ensure database is created
            using (var servicesScope = sp.CreateScope())
            {
                //migrate persistedgrant db
                var pbdb = servicesScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
                pbdb.Database.Migrate();

                var context = servicesScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                try
                {
                    if (!context.ApiScopes.Any())
                    {
                        foreach (var apiScope in IdpConfiguration.GetApiScopes())
                        {
                            context.ApiScopes.Add(apiScope.ToEntity());
                        }
                        context.SaveChanges();
                    }
                    if (!context.Clients.Any())
                    {
                        foreach (var client in IdpConfiguration.GetClients())
                        {
                            context.Clients.Add(client.ToEntity());
                            context.SaveChanges();
                        }
                    }

                    if (!context.ApiResources.Any())
                    {
                        foreach (var apiResource in IdpConfiguration.GetApiResources())
                        {
                            context.ApiResources.Add(apiResource.ToEntity());
                        }
                    }

                    if (!context.IdentityResources.Any())
                    {
                        foreach (var idResource in IdpConfiguration.GetIdentityResources())
                        {
                            context.IdentityResources.Add(idResource.ToEntity());
                        }
                        context.SaveChanges();
                    }

                }
                catch (System.Exception e)
                {
                    Log.Logger.Fatal(e.Message);
                }

                context.SaveChanges();
            }
        }
    }
}
