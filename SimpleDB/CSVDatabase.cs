sealed class CSVDatabase : IDatabaseRepository {
    public IEnumerable<Cheep> Read(int? limit = null)
}




/*
IDatabaseRepository<Cheep> database = new CSVDatabase<Cheep>();
...
var cheep = new Cheep("Helge", "Hej!", DateTimeOffset.UtcNow.ToUnixTimeSeconds()
database.Store(cheep);
...
public record Cheep(string Author, string Message, long Timestamp);
*/