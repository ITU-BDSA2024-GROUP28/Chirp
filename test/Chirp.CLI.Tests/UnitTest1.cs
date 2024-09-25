using System.Reflection;
using System.Text.Json;

namespace Chirp.CLI.Tests;

using Xunit;
using Cheep = SimpleDB.Cheep;
using UserInterface = Chirp.CLI.UserInterface;
using Program = Chirp.CLI.Program;

public class UnitTest1
{
    [Fact]
    public void printTest()
    {
        //The test is not done, but the base for the test should look somthing like this
        string author = "shhs";
        string message = "this is a test";
        long timestamp = 1727083893; //Example timestamp in long ()
        Cheep cheep = new Cheep(author, message, timestamp);

        string print = UserInterface.getPrint(cheep);
        
        string expected = "shhs @ 23/09/24 09.31.33: this is a test";
        
        Assert.Equal(expected, print);
    }
    
    [Fact]
    public void helpTest()
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