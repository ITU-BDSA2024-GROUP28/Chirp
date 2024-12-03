using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace SimpleDB;

public sealed class CsvDatabase<T>:IDatabaseRepository<T> {
    
    private static CsvDatabase<T>? _instance;
    string _databasePath;
    

    private CsvDatabase()
    {
       _databasePath = "../SimpleDB/Database.csv" ??
                       throw new ArgumentNullException(nameof(_databasePath), "Path to csv file can not be found");
       
    } //private constructor to hide from client code

    private CsvDatabase(string databasePath)
    {
        _databasePath = databasePath ?? throw new ArgumentNullException(nameof(databasePath), "Path to csv file can not be found");
    }
    public static CsvDatabase<T> GetInstance()
    {
        return _instance ??= new CsvDatabase<T>(); // create instance or return existing instance
    }
    /*
    The getInstance method makes sure that no other instances of CSVDatabase are created if there already exists one - singleton pattern
    Method inspired by:
    https://csharpindepth.com/articles/Singleton#unsafe
    https://dev.to/kalkwst/singleton-pattern-in-c-1dh0
    */

    public static CsvDatabase<T> GetTestInstance(string databasePath)
    {
        return _instance ??= new CsvDatabase<T>(databasePath); // create instance or return existing instance
    }
    
    public IEnumerable<T> Read(int? limit = null)
    {
        // Use the path to the CSV file in the stream reader
        using var reader = new StreamReader(_databasePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        IEnumerable<T> information = csv.GetRecords<T>().ToList();
        return information;
    }

    public void Store(T record)
    {
        using var writer = File.AppendText(_databasePath);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.NextRecord();
        csv.WriteRecord(record);
    }
}