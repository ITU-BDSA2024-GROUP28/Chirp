using Microsoft.VisualStudio.TestPlatform.Utilities;
using SimpleDB;

namespace Chirp.CLI.Tests;

/**
 * Add integration tests that test your CSV database library works as intended.
 * For example,
    * add a test case that checks that an entry can be received from the database after it was stored in there.
 */

public class IntegrationTestsForCLI
{
    private static readonly string TestDbPath = "../../../../../../Chirp/src/SimpleDB/TestDatabase.csv";
    readonly CsvDatabase<Cheep> _csvDatabase = CsvDatabase<Cheep>.GetTestInstance(TestDbPath);
    
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

    public void Test_CsvDatabase_Can_Read_Cheep()
    {
        
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