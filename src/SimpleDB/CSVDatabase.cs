using System.Globalization;
using CsvHelper;

namespace SimpleDB;

public sealed class CSVDatabase<T> : IDatabaseRepository<T>
{
    private static CSVDatabase<T> instance; //private static instance field
    private string databasePath = "../SimpleDB/Database.csv";

    private CSVDatabase()
    {
        databasePath = databasePath ??
                       throw new ArgumentNullException(nameof(databasePath), "Path to csv file can not be found");
    } //private constructor to hide from client code
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
        using (var reader = new StreamReader(databasePath))

        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            information = csv.GetRecords<T>().ToList();
        }

        return information;
    }

    public void Store(T record)

    {
        using (var writer = File.AppendText(databasePath))

        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))

        {
            csv.NextRecord();

            csv.WriteRecord(record);
        }
    }

    public static CSVDatabase<T> GetInstance()
    {
        if (instance == null) instance = new CSVDatabase<T>(); //if no instance exists creates one
        return instance; //returns already created instance
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

//CSV database methods used in CSVDBService - Program.cs