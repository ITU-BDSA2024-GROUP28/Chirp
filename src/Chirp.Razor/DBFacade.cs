using Microsoft.Data.Sqlite;

public class DBFacade
{
    private readonly string sqlDBFilePath = "../../chirp.db";

    private readonly string sqlQuery = @"SELECT * FROM message ORDER by message.pub_date desc";

    public void GetCheeps()
    {
        using (var connection = new SqliteConnection($"Data Source={sqlDBFilePath}"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                //...
            }
        }
    }
}