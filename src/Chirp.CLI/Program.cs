using DocoptNet;
using SimpleDB;

namespace Chirp.CLI;

public static class Program
{
	const string Usage = @"Chirp CLI version.
        
            Usage:
              chirp read <limit>
              chirp cheep <message>
              chirp (-h | --help)
              chirp --version
        
            Options:
              -h --help     Show this screen.
              --version     Show version. ";
	
	static void Main(String[] args)
	{
		var arguments = new Docopt().Apply(Usage, args, version: "1.0", exit: true)!;
        
		if (arguments["read"].IsTrue)
		{
			Cheep cheep1 = new Cheep("Cheeper", "Hello", 1045);
			// read using database from docopt
			CSVDatabase<Cheep> csvDatabase =  CSVDatabase<Cheep>.GetInstance(); 
				
			csvDatabase.Store(cheep1);
			
			// Store as a cheep record
			
		} else if (arguments["-h"].IsTrue || arguments["--help"].IsTrue)
		{
			Console.WriteLine(Usage);
		} else if (arguments["cheep"].IsTrue)
		{
			//Get the message from the line, so it can be stored
			//Find en måde at få cheepen man har skrevet på
			CSVDatabase<Cheep> csvDatabase =  CSVDatabase<Cheep>.GetInstance(); 
			var timestamp = DateTime.Now;
			//csvDatabase.Store(cheep.Author, timestamp, message);
			// Note: third auto release attempt
		}
	}

}