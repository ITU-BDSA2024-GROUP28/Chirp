using DocoptNet;

namespace Chirp.CLI;

public static class UserInterface
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

    public static void Run(string[] args)
    {
        
        var arguments = new Docopt().Apply(Usage, args, version: "1.0", exit: true)!;
        
        if (arguments["read"].IsTrue)
        {
            
        }

    } 
        
       

}