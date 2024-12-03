namespace Chirp.CLI.Tests;

public class CleanDatabase
{
    public static void Do(string TestDbPath)
    {
        File.Delete(TestDbPath);

        using (var stream = File.Create(TestDbPath))
        {
            stream.Close();
        }

        using (var sw = new StreamWriter(TestDbPath))
        {
            sw.WriteLine("Author,Message,Timestamp");
            sw.Close();
        }
    }
}