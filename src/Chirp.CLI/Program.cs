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
              --version     Show version.
	";
	
	static void Main(String[] args)
	{
		var arguments = new Docopt().Apply(Usage, args, version: "1.0", exit: true)!;
		
		CSVDatabase<Cheep> csvDatabase =  CSVDatabase<Cheep>.GetInstance(); 
		
		
		if (arguments["read"].IsTrue)
		{
			// read using database from docopt
			UserInterface.printCheeps(csvDatabase.Read());
			
		} 
		else if (arguments["cheep"].IsTrue)
		{
			//Get the message from the line, so it can be stored
			//Find en måde at få cheepen man har skrevet på
			long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			var message = arguments["<message>"].ToString();
			var author = Environment.UserDomainName;
			
			Cheep cheep = new Cheep(author, message, timestamp);
			
			csvDatabase.Store(cheep);
			
			Console.WriteLine($"Reading cheep message: {message} and timestamp: {timestamp}");
			
			//csvDatabase.Store();
			// Note: third auto release attempt
		} 
		else if (arguments["-h"].IsTrue || arguments["--help"].IsTrue)
		{
			Console.WriteLine(Usage);
		} 
	}

}