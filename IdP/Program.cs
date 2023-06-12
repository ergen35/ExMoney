using IdP;

var builder = WebApplication.CreateBuilder(args);


//add services to the container
builder.Services.AddMvc();
builder.Services.AddRazorPages();

builder.Services.AddIdentityServer(options => {
    options.EmitScopesAsSpaceDelimitedStringInJwt = true;
})
.AddTestUsers(IdpConfiguration.GetTestUsers())
.AddInMemoryApiScopes(IdpConfiguration.GetApiScopes())
.AddInMemoryApiResources(IdpConfiguration.GetApiResources())
.AddInMemoryClients(IdpConfiguration.GetClients())
.AddInMemoryIdentityResources(IdpConfiguration.GetIdentityResources())
.AddInMemoryPersistedGrants();


var app = builder.Build();

app.MapDefaultControllerRoute();
app.UseIdentityServer();

app.Run();
