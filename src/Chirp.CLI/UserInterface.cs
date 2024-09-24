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
        var time = DateTimeOffset.FromUnixTimeSeconds(cheep.Timestamp).DateTime;
        string formattedTime = time.ToString("MM/dd/yy HH:mm:ss");
        
        return $"{cheep.Author} @ {formattedTime}: {cheep.Message}";
    }

}