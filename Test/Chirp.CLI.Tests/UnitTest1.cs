using Chirp.CLI;
using SimpleDB;
namespace Chirp.CLI.Tests;

public class UnitTest1
{
    CsvDatabase<Cheep> csvDatabase;

    public UnitTest1()
    {
        csvDatabase = CsvDatabase<Cheep>.GetInstance();
    }
    [Fact]
    public void Test1()
    {
    }
    
    [Fact]  
    public void CSVDatabase_is_singleton()  
    {  
        var dbInstanceTwo = CsvDatabase<Cheep>.GetInstance();  
        Assert.Equal(dbInstanceTwo, csvDatabase);    //uses method AreEqual to check if they are the same  
    }  
}