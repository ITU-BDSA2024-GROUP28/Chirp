using SimpleDB;

namespace Chirp.CLI.Tests
{
    /**
     * Add unit tests to your _Chirp!_ CLI app.
     * Add unit tests for suitable functionality.
     * For example,
        * conversion of UNIX timestamps to user readable times
        * and similar functionality are good candidates for unit testing.
     */
    [Collection("Sequential")]
    public class UnitTestsForCli
    {
        private static readonly string TestDbPath = "../../../../../../Chirp/src/SimpleDB/TestDatabase.csv";
        readonly CsvDatabase<Cheep> _csvDatabase = CsvDatabase<Cheep>.GetTestInstance(TestDbPath);
        
        [Fact]
        private void Check_TestCSVDatabase_is_singleton()
        {
            var dbInstanceTwo = CsvDatabase<Cheep>.GetTestInstance(TestDbPath);
            Assert.Equal(dbInstanceTwo, _csvDatabase); //uses method AreEqual to check if they are the same  
        }

        [Fact]
        private void Check_CSVDatabase_is_singleton()
        {
            var dbInstanceOne = CsvDatabase<Cheep>.GetInstance();
            var dbInstanceTwo = CsvDatabase<Cheep>.GetInstance();
            Assert.Equal(dbInstanceTwo, dbInstanceOne); //uses method AreEqual to check if they are the same  
        }

        [Fact]
        private void Test_UserInterface_Prints_Cheep_Correctly()
        {
            //The test is not done, but the base for the test should look something like this
            string author = "shhs";
            string message = "this is a test";
            long timestamp = 1727083893; //Example timestamp in long ()
            Cheep cheep = new Cheep(author, message, timestamp);

            string print = UserInterface.GetPrint(cheep);

            string expected = "shhs @ 23/09/24 09:31:33: this is a test";

            Assert.Equal(expected, print);
        }

        [Fact]
        private void Test_CSVDatabase_Stores_Cheep()
        {
            CleanDatabase.Do(TestDbPath);
            
            int timestamp = 1727083893;

            Cheep cheep = new Cheep("kajn", "Hej!", timestamp);

            _csvDatabase.Store(cheep);

            IEnumerable<Cheep> cheeps = _csvDatabase.Read(1);

            var enumerable = cheeps as Cheep[] ?? cheeps.ToArray();

            Cheep readCheep = enumerable[0];

            Assert.Equal(cheep, readCheep);

            CleanDatabase.Do(TestDbPath);
        }
    }

    /**
     * Add integration tests that test your CSV database library works as intended.
     * For example,
        * add a test case that checks that an entry can be received from the database after it was stored in there.
     */


    [Collection("Sequential")]
    public class IntegrationTestsForCLI
    {
        private static readonly string TestDbPath = "../../../../../../Chirp/src/SimpleDB/TestDatabase.csv";
        readonly CsvDatabase<Cheep> _csvDatabase = CsvDatabase<Cheep>.GetTestInstance(TestDbPath);
        
        [Fact]
        private void Test_CSVDatabase_Stores_Cheep()
        {
            int timestamp = 1727083893;

            Cheep cheep = new Cheep("kajn", "Hej!", timestamp);

            _csvDatabase.Store(cheep);

            IEnumerable<Cheep> cheeps = _csvDatabase.Read(1);

            var enumerable = cheeps as Cheep[] ?? cheeps.ToArray();

            Cheep readCheep = enumerable[0];

            Assert.Equal(cheep, readCheep);

            CleanDatabase.Do(TestDbPath);
        }
    }
    

    [Collection("Sequential")]
    public class EndToEndTest1
    {
        private static readonly string TestDbPath = "../../../../../../Chirp/src/SimpleDB/TestDatabase.csv";
        readonly CsvDatabase<Cheep> _csvDatabase = CsvDatabase<Cheep>.GetTestInstance(TestDbPath);
        
        [Fact]
        public void GetCheepTest()
        {
            CleanDatabase.Do(TestDbPath);
            int timestamp = 1727083893;
            
            Cheep cheep = new Cheep("kajn", "Hej!", timestamp);

            _csvDatabase.Store(cheep);
            
            var time = DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
            string formattedTime = time.ToString("dd/MM/yy HH:mm:ss");
            
            string expected = "kajn @ " + formattedTime + ": Hej!\n";
            
            IEnumerable<Cheep> db = _csvDatabase.Read(2);

            string consoleOutput = "";
            
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                UserInterface.PrintCheeps(db, 1);

                consoleOutput = stringWriter.ToString();
            }
            
            Assert.Equal(expected, consoleOutput); //Assert if the gotten cheep equals the created one
            CleanDatabase.Do(TestDbPath);
        }

        [Fact]
        public void Test_Running_Program_From_Main()
        {
            CleanDatabase.Do(TestDbPath);
            int timestamp = 1727083893;
            
            Cheep cheep = new Cheep("kajn", "Hej!", timestamp);

            _csvDatabase.Store(cheep);
            
            string consoleOutput = "";
            
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                Thread.Sleep(1000);
                Program.Main(["read", "1"]);
                Thread.Sleep(1000);

                consoleOutput = stringWriter.ToString();
            }
            
            Assert.Equal("kajn @ 23/09/24 09:31:33: Hej!\n", consoleOutput);
            
            CleanDatabase.Do(TestDbPath);
        }
    }
    
    [Collection("Sequential")]
    public class EndToEndTest2
    {
        private static readonly string TestDbPath = "../../../../../../Chirp/src/SimpleDB/TestDatabase.csv";
        readonly CsvDatabase<Cheep> _csvDatabase = CsvDatabase<Cheep>.GetTestInstance(TestDbPath);
    
        [Fact]
        public void Test_Calling_Cheep_And_Read_From_Main()
        {
            CleanDatabase.Do(TestDbPath);
            Program.Main(["cheep", "Hello World!"]);
            string timestamp = UserInterface.GetTime(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        
            string consoleOutput = "";
        
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                Program.Main(["read", "1"]);

                consoleOutput = stringWriter.ToString();
            }

            string userName = Environment.UserDomainName;
        
            Assert.Equal(userName + " @ " + timestamp + ": Hello World!\n", consoleOutput);
        
            CleanDatabase.Do(TestDbPath);
        }
    }
}