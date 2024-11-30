using System.Globalization;
using CsvHelper;

namespace SimpleDB;

public sealed class CSVDatabase<T>:IDatabaseRepository<T> {
    
    private static CSVDatabase<T> _instance;  //private static instance field
    string _databasePath = "../SimpleDB/Database.csv";

    private CSVDatabase()
    {
       _databasePath = _databasePath ??
                            throw new ArgumentNullException(nameof(_databasePath), "Path to csv file can not be found");} //private constructor to hide from client code
    
    public static CSVDatabase<T> GetInstance()
    {
        if (_instance == null)
        {
            _instance = new CSVDatabase<T>();      //if no instance exists creates one
        }
        return _instance;    //returns already created instance
    }
    /*
    The getInstance method makes sure that no other instances of CSVDatabase are created if there already exists one - singleton pattern
    Method inspired by:
    https://csharpindepth.com/articles/Singleton#unsafe
    https://dev.to/kalkwst/singleton-pattern-in-c-1dh0
    */

    
    public IEnumerable<T> Read(int? limit = null)
    {
        IEnumerable<T> information;
        
        //Need the path to the CSV file in the parenthesis
        using (var reader = new StreamReader(_databasePath))
            
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            information = csv.GetRecords<T>().ToList();
        }
        return information;
        
    }

    public void Store(T record)
    {
        using (var writer = File.AppendText(_databasePath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.NextRecord();
            csv.WriteRecord(record);
        }
    }
}