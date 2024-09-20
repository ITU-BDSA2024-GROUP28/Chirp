using System.Globalization;
using CsvHelper;

namespace SimpleDB;

public sealed class CSVDatabase<T>:IDatabaseRepository<T> {
    
    private static CSVDatabase<T> instance;  //private static instance field

    private CSVDatabase(){ } //private constructor to hide from client code
    
    public static CSVDatabase<T> GetInstance() 
    {
        if (instance == null)
        {
            instance = new CSVDatabase<T>();      //if no instance exists creates one
        }
        return instance;    //returns already created instance
    }
    /*
    The getInstance method makes sure that no other instances of CSVDatabase are created if there already exists one - singleton pattern
    Method inspired by:
    https://csharpindepth.com/articles/Singleton#unsafe
    https://dev.to/kalkwst/singleton-pattern-in-c-1dh0
    */
    
    public IEnumerable<T> Read(int? limit = null)
    {
        // Testing the release workflow again

        IEnumerable<T> information;
        
        //Need the path to the CVS file in the parenthesis
        using (var reader = new StreamReader(""))
            
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            information = csv.GetRecords<T>();
        }
        
        return information;
        
        
    }

    public void Store(T record)
    {
        //Here we need to put in the path to the csv file
        using StreamWriter writer = new StreamWriter("");
        
        //See if the string is correct
        writer.WriteLine(record.ToString());
        
    }
    
    public String writeCheep(Cheep cheep)
    {
        return cheep.Author + "," + '"' + cheep.Message + '"' + "," + cheep.Timestamp;
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