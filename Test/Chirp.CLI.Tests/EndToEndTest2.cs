using SimpleDB;

namespace Chirp.CLI.Tests;

/**
 * -End2end tests:
 * You want to test that
    * given example data from
    * [`chirp_cli_db.csv`] by calling `chirp read 10`
    * from the command line produces output as expected
 
 * Additionally, you want to test that
    * calling `chirp cheep "Hello!!!"`
    * stores the respective values in the database.
 */

public class EndToEndTest2
{
    private static readonly string TestDbPath = "../../../../../../Chirp/src/SimpleDB/TestDatabase.csv";
    readonly CsvDatabase<Cheep> _csvDatabase = CsvDatabase<Cheep>.GetTestInstance(TestDbPath);
    
    [Fact]
    public void Test_Calling_Cheep_And_Read_From_Main()
    {
        CleanDatabase();
        Program.Main(["cheep", "Hello World!"]);
        string timestamp = UserInterface.GetTime(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        
        string consoleOutput = "";
        
        using (StringWriter stringWriter = new StringWriter())
        {
            Console.SetOut(stringWriter);

            Thread.Sleep(1000);
            Program.Main(["read", "1"]);
            Thread.Sleep(1000);

            consoleOutput = stringWriter.ToString();
        }

        string userName = Environment.UserDomainName;
        
        Assert.Equal(userName + " @ " + timestamp + ": Hello World!\n", consoleOutput);
        
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