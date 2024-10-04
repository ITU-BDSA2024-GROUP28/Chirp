using SimpleDB;

namespace Chirp.CLI;

public static class UserInterface
{
    public static void printCheeps(IEnumerable<Cheep> cheeps, int limit)
    {
        for (var i = 0; i < limit; i++)
        {
            var cheep = cheeps.ElementAt(i);
            Console.WriteLine(getPrint(cheep));
        }
    }

    public static string getPrint(Cheep cheep)
    {
        var time = DateTimeOffset.FromUnixTimeSeconds(cheep.Timestamp).DateTime;
        var formattedTime = time.ToString("dd/MM/yy HH:mm:ss");

        return $"{cheep.Author} @ {formattedTime}: {cheep.Message}";
    }
}