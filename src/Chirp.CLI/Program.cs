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

    // Emma was here :))))))))))))))))))
    public static void Main(string[] args)
    {
        var arguments = new Docopt().Apply(Usage, args, version: "1.0", exit: true)!;

        var csvDatabase = CSVDatabase<Cheep>.GetInstance();


        if (arguments["read"].IsTrue)
        {
            // read using database from docopt
            var limit = arguments["<limit>"].AsInt;
            UserInterface.printCheeps(csvDatabase.Read(), limit);
        }
        else if (arguments["cheep"].IsTrue)
        {
            //Get the message from the line, so it can be stored
            //Find en måde at få cheepen man har skrevet på
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var message = arguments["<message>"].ToString();
            var author = Environment.UserDomainName;

            var cheep = new Cheep(author, message, timestamp);

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