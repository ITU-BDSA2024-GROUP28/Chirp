using Chirp.Core;
using Chirp.Core.Infrastructure;
using DomainModel;
using Microsoft.EntityFrameworkCore;

// add a web app builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the builder container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICheepService, CheepService>();
builder.Services.AddScoped<ICheepRepository, CheepRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

// add a service for dependency injection
var services = new ServiceCollection();

// Add services to the dependency container.
services.AddSingleton<ICheepService, CheepService>();
services.AddSingleton<DBFacade>();
services.AddSingleton<CheepDbContext>();
services.AddSingleton<ICheepRepository, CheepRepository>();

var serviceProvider = services.BuildServiceProvider();

var service1 = serviceProvider.GetService<DBFacade>();
var service2 = serviceProvider.GetService<CheepDbContext>();
var service3 = serviceProvider.GetService<CheepRepository>();



// Load database connection via configuration
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CheepDbContext>(options => options.UseSqlite(connectionString));


var app = builder.Build();

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

app.MapRazorPages();

app.Run();