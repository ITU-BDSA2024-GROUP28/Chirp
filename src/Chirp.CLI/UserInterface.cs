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
        var time = DateTimeOffset.FromUnixTimeSeconds(cheep.Timestamp).DateTime;
        string formattedTime = time.ToString("dd/MM/yy HH:mm:ss");
        
        return $"{cheep.Author} @ {formattedTime}: {cheep.Message}";
    }

}