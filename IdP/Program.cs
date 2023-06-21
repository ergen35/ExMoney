using IdP;
using Serilog;


Log.Logger = new LoggerConfiguration().WriteTo
                                        .Console()
                                            .CreateBootstrapLogger();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders().AddSerilog();

//add services to the container
builder.Services.AddLogging(l => l.ClearProviders().AddSerilog());
builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddRazorPages();

builder.Services.AddIdentityServer(options =>
{
    options.EmitScopesAsSpaceDelimitedStringInJwt = true;
})
.AddTestUsers(IdpConfiguration.Users)
.AddInMemoryApiScopes(IdpConfiguration.GetApiScopes())
.AddInMemoryApiResources(IdpConfiguration.GetApiResources())
.AddInMemoryClients(IdpConfiguration.GetClients())
.AddInMemoryIdentityResources(IdpConfiguration.GetIdentityResources())
.AddInMemoryPersistedGrants();

WebApplication app = builder.Build();

app.UseRouting();
app.UseStaticFiles();

app.MapDefaultControllerRoute();
app.MapControllers();

app.UseIdentityServer();

app.MapGet("/status", async (ctx) => {
    await ctx.Response.WriteAsJsonAsync("Status, Ok");
});


app.Run();
