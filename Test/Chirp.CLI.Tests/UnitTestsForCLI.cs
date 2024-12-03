using SimpleDB;

namespace Chirp.CLI.Tests;

public class UnitTestsForCLI
{
    readonly CsvDatabase<Cheep> _csvDatabase = CsvDatabase<Cheep>.GetInstance();
    
    [Fact]  
    public void Check_CSVDatabase_is_singleton()  
    {  
        var dbInstanceTwo = CsvDatabase<Cheep>.GetInstance();  
        Assert.Equal(dbInstanceTwo, _csvDatabase);    //uses method AreEqual to check if they are the same  
    }  
    
    [Fact]
    public void PrintTest()
    {
        //The test is not done, but the base for the test should look somthing like this
        string author = "shhs";
        string message = "this is a test";
        long timestamp = 1727083893; //Example timestamp in long ()
        Cheep cheep = new Cheep(author, message, timestamp);

        string print = UserInterface.GetPrint(cheep);
        
        string expected = "shhs @ 23/09/24 09:31:33: this is a test";
        
        Assert.Equal(expected, print);
    }
    
    [Fact]
    public void HelpTest()
    {
        String[] input = new String[] { "-h" };
        
        
        string help = @"Chirp CLI version.
        
            Usage:
              chirp read <limit>
              chirp cheep <message>
              chirp (-h | --help)
              chirp --version
        
            Options:
              -h --help     Show this screen.
              --version     Show version.
	";
        
        
    }
}