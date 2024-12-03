using Microsoft.VisualStudio.TestPlatform.Utilities;
using SimpleDB;

namespace Chirp.CLI.Tests;

public class IntegrationTestsForCLI
{
    readonly CsvDatabase<Cheep> _csvDatabase = CsvDatabase<Cheep>.GetTestInstance("../../../../../../Chirp/src/SimpleDB/TestDatabase.csv");

    [Fact]
    public void GetCheepTest()
    {
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
    }
    
    public void WriteCheepTest()
    {
        
        //Cheep cheep = new Cheep("kajn", "Hej!", 0);
        //Assert if the cheep is being written correctly according to output
    }
}