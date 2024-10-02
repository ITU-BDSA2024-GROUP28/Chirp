
namespace Chirp.Razor.Tests;
using Xunit;
using CheepViewModel = CheepViewModel;

public class UnitTest1
{
    
    [Fact]
    public void getListCheeps()
    {
        string author = "shhs";
        string message = "this is a test";
        string timestamp = "1727083893"; //Example timestamp in long ()
        CheepViewModel cheep = new CheepViewModel(author, message, timestamp);
        CheepService._cheeps.Add(cheep);
        Assert.Contains(cheep, CheepService._cheeps);
    }
    
    [Fact]
    public void unixTimestampTest()
    {
        double timestamp = 1727083893;
        string time = CheepService.UnixTimeStampToDateTimeString(timestamp);
        string expected = "09/23/24 9.31.33";
        
        Assert.Equal(expected, time);
    }
}