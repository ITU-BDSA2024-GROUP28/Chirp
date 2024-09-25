namespace Chirp.Razor;

public class DBFacade
{
    var sqlDBFilePath = "/tmp/chirp.db";
    var sqlQuery = @"SELECT * FROM message ORDER by message.pub_date desc";

        using (var connection = new SqliteConnection($"Data Source={sqlDBFilePath}"))
    {
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = sqlQuery;

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            ...
        }
    }
}