using System.Globalization;
using CsvHelper;

namespace SimpleDB;

public sealed class CSVDatabase<T>:IDatabaseRepository<T> {
    public IEnumerable<T> Read(int? limit = null)
    {

        IEnumerable<T> information;
        
        //Need the path to the CVS file in the paranthesis
        using (var reader = new StreamReader(""))
            
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            information = csv.GetRecords<T>();
        }
        
        return information;
        
    }

    public void Store(T record)
    {
        
        
        
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