using System.Globalization;
using CsvHelper;
using SimpleDB;

namespace Chirp.CLI.Tests;

/**
 * Add unit tests to your _Chirp!_ CLI app.
 * Add unit tests for suitable functionality.
 * For example,
    * conversion of UNIX timestamps to user readable times
    * and similar functionality are good candidates for unit testing.
 */

public class UnitTestsForCLI
{
    private static readonly string TestDbPath = "../../../../../../Chirp/src/SimpleDB/TestDatabase.csv";
    readonly CsvDatabase<Cheep> _csvDatabase = CsvDatabase<Cheep>.GetTestInstance(TestDbPath);
    
    [Fact]  
    public void Check_TestCSVDatabase_is_singleton()  
    {  
        var dbInstanceTwo = CsvDatabase<Cheep>.GetTestInstance(TestDbPath);  
        Assert.Equal(dbInstanceTwo, _csvDatabase);    //uses method AreEqual to check if they are the same  
    }  
    [Fact]  
    public void Check_CSVDatabase_is_singleton()  
    {  
        var dbInstanceOne = CsvDatabase<Cheep>.GetInstance();
        var dbInstanceTwo = CsvDatabase<Cheep>.GetInstance();  
        Assert.Equal(dbInstanceTwo, dbInstanceOne);    //uses method AreEqual to check if they are the same  
    }  
    
    [Fact]
    public void Test_UserInterface_Prints_Cheep_Correctly()
    {
        //The test is not done, but the base for the test should look something like this
        string author = "shhs";
        string message = "this is a test";
        long timestamp = 1727083893; //Example timestamp in long ()
        Cheep cheep = new Cheep(author, message, timestamp);

        string print = UserInterface.GetPrint(cheep);
        
        string expected = "shhs @ 23/09/24 09:31:33: this is a test";
        
        Assert.Equal(expected, print);
    }
    
    [Fact]
    public void Test_CSVDatabase_Stores_Cheep()
    {
        int timestamp = 1727083893;
        
        Cheep cheep = new Cheep("kajn", "Hej!", timestamp);

        _csvDatabase.Store(cheep);

        IEnumerable<Cheep> cheeps = _csvDatabase.Read(1);
        
        var enumerable = cheeps as Cheep[] ?? cheeps.ToArray();

        Cheep readCheep = enumerable[0];
        
        Assert.Equal(cheep, readCheep);
        
        CleanDatabase();
    }

    public void CleanDatabase()
    {
        File.Delete(TestDbPath);

        using (var stream = File.Create(TestDbPath))
        {
            stream.Close();
        }
        using (var sw = new StreamWriter(TestDbPath))
        {
            sw.WriteLine("Author,Message,Timestamp");
        }
    }
}