using Microsoft.Data.Sqlite;

public class DBFacade 
{
    string sqlDBFilePath = "";
    
    string sqlQuery = @"SELECT * FROM message ORDER by message.pub_date desc";
    
    
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
    
      public void GetCheeps(){
          
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