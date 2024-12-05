using Chirp.Core;
using Chirp.Infrastructure;
using Chirp.Infrastructure.Repositories;
using Chirp.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

// Latest Release: v2.0.0 13/11/24 :))

// add a web app builder
var builder = WebApplication.CreateBuilder(args);

// Load database connection via configuration
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ChirpDbContext>(options => options.UseSqlite(connectionString));

// Identity 
builder.Services.AddDefaultIdentity<Author>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ChirpDbContext>()
    .AddDefaultTokenProviders();

        

builder.Configuration.AddEnvironmentVariables();
// Github 

builder.Services.AddAuthentication(options =>
    {
        //options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //options.DefaultChallengeScheme = "GitHub";
        //options.RequireAuthenticatedSignIn = true;
    })
    .AddCookie()
    .AddGitHub(o =>
    {
        o.ClientId = builder.Configuration["authentication:github:clientId"] 
                     ?? Environment.GetEnvironmentVariable("GITHUB_PROVIDER_AUTHENTICATION_ID") // Gotten path from Azure
                     ?? throw new InvalidOperationException("You must provide an authentication client ID.");
        o.ClientSecret = builder.Configuration["authentication:github:clientSecret"] 
                         ?? Environment.GetEnvironmentVariable("GITHUB_PROVIDER_AUTHENTICATION_SECRET")
                         ?? throw new InvalidOperationException("You must provide an authentication client Secret.");
        //get GitHub email here
        o.CallbackPath = "/signin-github";
    });

    

// Add services to the dependency container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ICheepRepository, CheepRepository>();
builder.Services.AddScoped<ICheepService, CheepService>();
builder.Services.AddScoped<IFollowService, FollowService>();


var app = builder.Build();


// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ChirpDbContext>();
    context.Database.Migrate();
    var usermanager = scope.ServiceProvider.GetRequiredService<UserManager<Author>>();
    DbInitializer.SeedDatabase(context, usermanager);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
//app.UseSession();

app.MapRazorPages();

app.Run();

public partial class Program { }