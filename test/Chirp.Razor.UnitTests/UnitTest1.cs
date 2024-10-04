namespace Chirp.Razor.Tests;

using CheepViewModel = CheepViewModel;

/*public class UnitTest1
{
    //Tests that the cheeps get added the list correctly
    [Fact]
    public void getListCheeps()
    {
        var c = new CheepService();
        var author = "shhs";
        var message = "this is a test";
        var timestamp = "1727083893"; //Example timestamp in long ()
        var cheep = new CheepViewModel(author, message, timestamp);
        CheepService._cheeps.Add(cheep);
        var cheepsCount = c.GetCheeps().Count();
        Assert.Contains(cheep, CheepService._cheeps);
        Assert.Equal(cheep, CheepService._cheeps[cheepsCount - 1]);
    }

    //Checks that when getting cheeps from author the correct one is in the list and the other is not
    [Fact]
    public void GetListCheepsFromAuthor()
    {
        var c = new CheepService();
        var author1 = "shhs";
        var message1 = "this is a test";
        var timestamp1 = "1727083893"; //Example timestamp in long ()
        var cheep1 = new CheepViewModel(author1, message1, timestamp1);
        CheepService._cheeps.Add(cheep1);
        Assert.Contains(cheep1, c.GetCheepsFromAuthor(author1));

        var author2 = "ekra";
        var message2 = "this is test number 2";
        var timestamp2 = "1727083941"; //Example timestamp in long ()
        var cheep2 = new CheepViewModel(author2, message2, timestamp2);
        CheepService._cheeps.Add(cheep2);
        Assert.DoesNotContain(cheep2, c.GetCheepsFromAuthor(author1));
    }

    //Checks that the unittime is converted correctly
    [Fact]
    public void unixTimestampTest()
    {
        double timestamp = 1727083893;
        var time = CheepService.UnixTimeStampToDateTimeString(timestamp);
        var expected = "09/23/24 9.31.33";

        Assert.Equal(expected, time);
    }
}*/