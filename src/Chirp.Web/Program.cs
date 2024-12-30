using Chirp.Core;
using Chirp.Infrastructure;
using Chirp.Infrastructure.Repositories;
using Chirp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

// Latest Release: v3.0.1 19/12/24 :))

// This is our web application builder
var builder = WebApplication.CreateBuilder(args);

// Provide database connection to allow communication between db and context
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ChirpDbContext>(options => options.UseSqlite(connectionString));

// Add identity for local login
builder.Services.AddDefaultIdentity<Author>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ChirpDbContext>()
    .AddDefaultTokenProviders();


builder.Configuration.AddEnvironmentVariables();
 
// Provide secrets for authentication of users
builder.Services.AddAuthentication()
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
builder.Services.AddScoped<IFollowService, FollowService>();
builder.Services.AddScoped<ICheepService, CheepService>();


var app = builder.Build();


// Seed the database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ChirpDbContext>();
    context.Database.Migrate();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Author>>();
    DbInitializer.SeedDatabase(context, userManager);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // This is the default hsts lasting 30 days per session
    app.UseHsts();
}

// For security
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

public partial class Program { }