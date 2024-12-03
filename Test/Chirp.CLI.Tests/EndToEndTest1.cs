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

public class EndToEndTest1
{
    private static readonly string TestDbPath = "../../../../../../Chirp/src/SimpleDB/TestDatabase.csv";
    readonly CsvDatabase<Cheep> _csvDatabase = CsvDatabase<Cheep>.GetTestInstance(TestDbPath);
    
    [Fact]
    public void GetCheepTest()
    {
        CleanDatabase();
        int timestamp = 1727083893;
        
        Cheep cheep = new Cheep("kajn", "Hej!", timestamp);

        _csvDatabase.Store(cheep);
        
        var time = DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
        string formattedTime = time.ToString("dd/MM/yy HH:mm:ss");
        
        string expected = "kajn @ " + formattedTime + ": Hej!\n";
        
        IEnumerable<Cheep> db = _csvDatabase.Read(2);

        string consoleOutput = "";
        
        using (StringWriter stringWriter = new StringWriter())
        {
            Console.SetOut(stringWriter);

            UserInterface.PrintCheeps(db, 1);

            consoleOutput = stringWriter.ToString();
        }
        
        Assert.Equal(expected, consoleOutput); //Assert if the gotten cheep equals the created one
        CleanDatabase();
    }

    [Fact]
    public void Test_Running_Program_From_Main()
    {
        CleanDatabase();
        int timestamp = 1727083893;
        
        Cheep cheep = new Cheep("kajn", "Hej!", timestamp);

        _csvDatabase.Store(cheep);
        
        string consoleOutput = "";
        
        using (StringWriter stringWriter = new StringWriter())
        {
            Console.SetOut(stringWriter);

            Thread.Sleep(1000);
            Program.Main(["read", "1"]);
            Thread.Sleep(1000);

            consoleOutput = stringWriter.ToString();
        }
        
        Assert.Equal("kajn @ 23/09/24 09:31:33: Hej!\n", consoleOutput);
        
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