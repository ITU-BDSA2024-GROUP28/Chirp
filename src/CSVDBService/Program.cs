using Microsoft.AspNetCore.HttpLogging;

namespace DefaultNamespace;

public class Program
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    WebApplication app = builder.Build();
    
}