using DocoptNet;
using SimpleDB;

namespace Chirp.CLI;

public static class UserInterface
{
    
    public static void printCheeps(IEnumerable<Cheep> cheeps)
    {
        foreach(var cheep in cheeps)
        {
            //Need to check if the cheeps are printed out right
            Console.WriteLine(getPrint(cheep));
        }
    }
    
    public static String getPrint(Cheep cheep)
    {
        return cheep.Author + " @ " + cheep.Timestamp + ": " + cheep.Message;
    }

}