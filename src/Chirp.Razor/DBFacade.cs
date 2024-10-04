using Microsoft.Data.Sqlite;

namespace Chirp.Razor;
public class DBFacade
{
    private readonly string sqlDBFilePath;
    
    public DBFacade()
    {
        // get the environment variable for CHIRPDBPATH
        string dbPath = Environment.GetEnvironmentVariable("CHIRPDBPATH");

        // if CHIRPDBPATH is not set, use the user's temporary directory and default to chirp.db
        if (string.IsNullOrEmpty(dbPath))
        {
            // combine the user's temp directory with "chirp.db" as the default filename
            dbPath = Path.Combine(Path.GetTempPath(), "chirp.db");
        }
    
        // assign the final path to sqlDBFilePath for use
        sqlDBFilePath = dbPath;
    }
    
    
    public List<CheepViewModel> GetCheeps(int? pageNr){
        List<CheepViewModel> cheeps = new List<CheepViewModel>();
        string sqlQuery = 
            @"SELECT user.username, message.text, message.pub_date
              FROM message
              JOIN user on user.user_id = message.author_id
              ORDER by message.pub_date desc 
              limit 32 offset(@PageNr - 1) * 32";
          
        using (var connection = new SqliteConnection($"Data Source={sqlDBFilePath}"))
        { 
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;
              
            command.Parameters.AddWithValue("@PageNr", pageNr);
              
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var author = reader.GetString(0);
                var message = reader.GetString(1);
                var pubDate = reader.GetInt64(2);
                var timestamp = DateTimeOffset.FromUnixTimeSeconds(pubDate).ToString("yyyy-MM-dd HH:mm:ss");
                var cheep = new CheepViewModel(author, message, timestamp);
                cheeps.Add(cheep);
            }
        }
        return cheeps;
    }

    public List<CheepViewModel> GetCheepsFromAuthor(string authorName, int? pageNr)
    {
        List<CheepViewModel> cheeps = new List<CheepViewModel>();
        string sqlQuery =
            @"SELECT user.username, message.text, message.pub_date
              FROM user
              JOIN message on user.user_id = message.author_id
              WHERE user.username = @AuthorName;
              ORDER by message.pub_date desc limit 32 offset(@pageNr - 1) * 32";
        
        using (var connection = new SqliteConnection($"Data Source={sqlDBFilePath}"))
        { 
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;
              
            command.Parameters.AddWithValue("@AuthorName", authorName);
            command.Parameters.AddWithValue("@PageNr", pageNr);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var author = reader.GetString(0);
                var message = reader.GetString(1);
                var timestamp = reader.GetString(2);
                var cheep = new CheepViewModel(author, message, timestamp);
                cheeps.Add(cheep);
            }
        }
        return cheeps;
    }
}