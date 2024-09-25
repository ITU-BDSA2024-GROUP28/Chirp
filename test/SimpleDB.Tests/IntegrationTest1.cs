namespace SimpleDB.Tests;

using Xunit;

public class IntegrationTest1
{
    
    [Fact]
    public void GetCheepTest()
    {
        Cheep cheep = new Cheep("kajn", "Hej!", 1727083893);

       // CsvDatabase<Cheep> db = CsvDatabase.GetInstance();
        string expected = "kajn,hej,1727083893";
        //Assert.Equal(expected, CsvDatabase.Store(cheep));
        //Assert if the gotten cheep equals the created one
    }
    
    public void WriteCheepTest()
    {
        
        //Cheep cheep = new Cheep("kajn", "Hej!", 0);
        //Assert if the cheep is being written correctly according to output
    }
}