using SimpleDB;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var csvDb = CsvDatabase<Cheep>.GetInstance(); 

app.MapGet("/cheeps", () => csvDb.Read());
app.MapPost("/cheep", (Cheep cheep) => csvDb.Store(cheep));

app.Run();