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
        
		if (arguments["read"].IsTrue)
		{
			Cheep cheep1 = new Cheep("Cheeper", "Hello", 1045);
			// read using database from docopt
			CSVDatabase<Cheep> csvDatabase = new CSVDatabase<Cheep>();
				
			csvDatabase.Store(cheep1);
			
			// Store as a cheep record
			
			// Note: third auto release attempt
		}
	}

}