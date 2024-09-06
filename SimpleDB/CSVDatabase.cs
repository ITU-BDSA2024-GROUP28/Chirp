using Cheep;

sealed class CSVDatabase{
    public IEnumerable<Cheep> Read(string filePath, int? limit = null)
    {
        var records;
        
        //Need the path to the CVS file in the paranthesis
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            records = csv.GetRecords<Cheep>();
            
        }

        return records;
    }
}




/*
IDatabaseRepository<Cheep> database = new CSVDatabase<Cheep>();
...
var cheep = new Cheep("Helge", "Hej!", DateTimeOffset.UtcNow.ToUnixTimeSeconds()
database.Store(cheep);
...
public record Cheep(string Author, string Message, long Timestamp);
*/