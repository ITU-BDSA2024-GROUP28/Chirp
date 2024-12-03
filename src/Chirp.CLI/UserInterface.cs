using System;
using System.Collections.Generic;
using System.Linq;
using SimpleDB;

namespace Chirp.CLI;

public static class UserInterface
{
    public static void getCheeps(IEnumerable<Cheep> cheeps, int postion)
    {
        
    }
    
    public static void PrintCheeps(IEnumerable<Cheep> cheeps, int limit)
    {
        var enumerable = cheeps as Cheep[] ?? cheeps.ToArray();
        
        if (limit > enumerable.Count())
        {
            limit = enumerable.Count();
        }
        for (int i = 0; i < limit; i++)
        {   
            var cheep = enumerable.ElementAt(i);
            Console.WriteLine(GetPrint(cheep));
        }
    }
    
    public static String GetPrint(Cheep cheep)
    {
        string formattedTime = GetTime(cheep.Timestamp);
        
        return $"{cheep.Author} @ {formattedTime}: {cheep.Message}";
    }

    public static String GetTime(long seconds)
    {
        var time = DateTimeOffset.FromUnixTimeSeconds(seconds).DateTime;
        return time.ToString("dd/MM/yy HH:mm:ss");
    }
}