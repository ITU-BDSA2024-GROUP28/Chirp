namespace TestProject;

using NUnit.Framework;
using SimpleDB;


[TestFixture]
public class SingletonTest
{
    [Test]
    public void CSVDatabase_should_return_same()
    {
        var dbInstance1 = CSVDatabase<Cheep>.GetInstance(); 
        var dbInstance2 = CSVDatabase<Cheep>.GetInstance();

        // Assert
        Assert.AreEqual(dbInstance1, dbInstance2);    //uses method AreEqual to check if they are the same
    }
}