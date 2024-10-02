
namespace Chirp.Razor.Tests;
using Xunit;
using CheepViewModel = CheepViewModel;

public class UnitTest1
{
    
    //Tests that the cheeps get added the list correctly
    [Fact]
    public void getListCheeps()
    {
        CheepService c = new CheepService();
        string author = "shhs";
        string message = "this is a test";
        string timestamp = "1727083893"; //Example timestamp in long ()
        CheepViewModel cheep = new CheepViewModel(author, message, timestamp);
        CheepService._cheeps.Add(cheep);
        int cheepsCount = c.GetCheeps().Count();
        Assert.Contains(cheep, CheepService._cheeps);
        Assert.Equal(cheep, CheepService._cheeps[cheepsCount - 1]);
    }
    
    //Checks that when getting cheeps from author the correct one is in the list and the other is not
    [Fact]
    public void getListCheepsFromAuthor()
    {
        CheepService c = new CheepService();
        string author1 = "shhs";
        string message1 = "this is a test";
        string timestamp1 = "1727083893"; //Example timestamp in long ()
        CheepViewModel cheep1 = new CheepViewModel(author1, message1, timestamp1);
        CheepService._cheeps.Add(cheep1);
        Assert.Contains(cheep1, c.GetCheepsFromAuthor(author1));
        
        string author2 = "ekra";
        string message2 = "this is test number 2";
        string timestamp2 = "1727083941"; //Example timestamp in long ()
        CheepViewModel cheep2 = new CheepViewModel(author2, message2, timestamp2);
        CheepService._cheeps.Add(cheep2);
        Assert.DoesNotContain(cheep2, c.GetCheepsFromAuthor(author1));
    }
    
    //Checks that the unittime is converted correctly
    [Fact]
    public void unixTimestampTest()
    {
        double timestamp = 1727083893;
        string time = CheepService.UnixTimeStampToDateTimeString(timestamp);
        string expected = "09/23/24 9.31.33";
        
        Assert.Equal(expected, time);
    }
}