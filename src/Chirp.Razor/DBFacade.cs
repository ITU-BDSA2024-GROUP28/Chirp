using Microsoft.Data.Sqlite;

namespace Chirp.Razor;
public class DBFacade
{
    static string sqlDBFilePath = "../../chirp.db";
    
      public static List<CheepViewModel> GetCheeps(int? pageNr){
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

      public static List<CheepViewModel> GetCheepsFromAuthor(string authorName, int? pageNr)
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