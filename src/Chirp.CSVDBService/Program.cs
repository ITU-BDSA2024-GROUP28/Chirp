using SimpleDB;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var csvDB = CSVDatabase<Cheep>.GetInstance();

app.MapGet("/cheeps", () => csvDB.Read());
app.MapPost("/cheep", (Cheep cheep) => csvDB.Store(cheep));

app.Run();

public record Cheep(string Author, string Message, long Timestamp);

/*
 * Implement two endpoints in your CSV database web service: /cheep and /cheeps.
 * Sending a JSON object, e.g., of the form {"Author":"ropf","Message":"Hello, World!", "Timestamp": 1684229348}
 * as the body of an HTTP POST request to the /cheep endpoint shall store the cheep in the remote database.
 * An HTTP GET request to the /cheeps endpoint shall return all cheeps that are stored in the CSV database
 * as a list of JSON objects.
 */