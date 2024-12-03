using System;
using DocoptNet;
using SimpleDB;

namespace Chirp.CLI;

public static class Program
{
	private const string Usage = @"Chirp CLI version.
        
            Usage:
              chirp read <limit>
              chirp cheep <message>
              chirp (-h | --help)
              chirp --version
        
            Options:
              -h --help     Show this screen.
              --version     Show version.
	";
	
	public static void Main(String[] args)
	{
		var arguments = new Docopt().Apply(Usage, args, version: "1.0", exit: true)!;

		CsvDatabase<Cheep> csvDatabase;
		
		if (Environment.CurrentDirectory.Contains("Debug"))
		{
			// FOR TESTING
			csvDatabase = CsvDatabase<Cheep>.GetTestInstance("../../../../../../Chirp/src/SimpleDB/TestDatabase.csv");
		}
		else
		{
			// FOR NORMAL RUN
			csvDatabase =  CsvDatabase<Cheep>.GetInstance();
		}
		
		if (arguments["read"].IsTrue)
		{
			// Read using database from docopt
			int limit = arguments["<limit>"].AsInt;
			UserInterface.PrintCheeps(csvDatabase.Read(), limit);
		} 
		else if (arguments["cheep"].IsTrue)
		{
			// Create a cheep
			long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			var message = arguments["<message>"].ToString();
			var author = Environment.UserDomainName;
			
			Cheep cheep = new Cheep(author, message, timestamp);
			
			csvDatabase.Store(cheep);

			if (Environment.CurrentDirectory.Contains("Debug"))
			{
				return;
			}
			Console.WriteLine($"Reading cheep message: {message} and timestamp: {timestamp}");
		} 
		else if (arguments["-h"].IsTrue || arguments["--help"].IsTrue)
		{
			// Print help line
			Console.WriteLine(Usage);
		} 
	}

}
