using LinenandBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace LinenandBird.DataAccess
{
  public class BirdRepository
  {

    const string _connectionString = "Server=localhost;Database=LinenandBird;Trusted_Connection=True;";

    internal IEnumerable<Bird> GetAll()
    {
      // Connections are like the tunnel between our app and the database
      using var connection = new SqlConnection("Server=localhost;Database=LinenandBird;Trusted_Connection=True;");
      // Connections aren't open by default, we've gotta do that ourself
      connection.Open();

      // This is what tells sql what we want to do
      var command = connection.CreateCommand();
      command.CommandText = @"Select * 
                                From Birds";

      // Execute reader is for when we care about getting all the results of our query
      var reader = command.ExecuteReader();

      var birds = new List<Bird>();

      // Block of code till bird.Add is translation mapping to SQL
      // WHile Loop
      // Data readers are weird, only get one row from the results at a time
      while (reader.Read())
      {
        var bird = MapFromReader(reader);

        // Each bird goes in the list to return later
        birds.Add(bird);
      }

      return birds;
    }

    internal Bird GetById(Guid birdId)
    {
      // Connections are like the tunnel between our app and the database
      using var connection = new SqlConnection("Server=localhost;Database=LinenandBird;Trusted_Connection=True;");
      // Connections aren't open by default, we've gotta do that ourself
      connection.Open();

      // This is what tells sql what we want to do
      var command = connection.CreateCommand();
      command.CommandText = @"Select * 
                            From Birds
                            where id = @id";

      // Parameterization prevents sql injection (little bobby tables)
      command.Parameters.AddWithValue("id", birdId);

      // Execute reader is for when we care about getting all the results of our query
      var reader = command.ExecuteReader();

      if (reader.Read())
      {
        return MapFromReader(reader);
      }

      return default; // return null;

      // return _birds.FirstOrDefault(bird => bird.Id == birdId);
    }

    internal void Add(Bird newBird)
    {
      using var connection = new SqlConnection("Server=localhost;Database=LinenandBird;Trusted_Connection=True;");
      connection.Open();

      var cmd = connection.CreateCommand();
      cmd.CommandText = @"insert into birds(Type,Color,Size,Name)
                          output inserted.Id
                          values (@Type,@Color,@Size,@Name)";

      cmd.Parameters.AddWithValue("Type", newBird.Type);
      cmd.Parameters.AddWithValue("Color", newBird.Color);
      cmd.Parameters.AddWithValue("Size", newBird.Size);
      cmd.Parameters.AddWithValue("Name", newBird.Name);

      // Execute the query, but don't care about the results, just number of rows
      var numberOfRowsAffected = cmd.ExecuteNonQuery();

      // Execute the query and only get the id of the new row
      var newId = (Guid)cmd.ExecuteScalar();

      newBird.Id = newId;

    }

    internal object Update(Guid id, Bird bird)
    {
      using var connection = new SqlConnection("Server=localhost;Database=LinenandBird;Trusted_Connection=True;");
      connection.Open();

      var cmd = connection.CreateCommand();
      cmd.CommandText = @"update Birds
                          Set Color = @color,
                              Name = @name,
                              Type = @type,
                              Size = @size
                          output inserted.*
                          Where id = @id";

      // Bird comes from the Http request in the controller
      cmd.Parameters.AddWithValue("Type", bird.Type);
      cmd.Parameters.AddWithValue("Color", bird.Color);
      cmd.Parameters.AddWithValue("Size", bird.Size);
      cmd.Parameters.AddWithValue("Name", bird.Name);
      cmd.Parameters.AddWithValue("id", id);

      var reader = cmd.ExecuteReader();

      if (reader.Read())
      {
        var updatedBird = MapFromReader(reader); // uses the MapFromReader(SqlDataReader) instead
        return updatedBird;
      }

      return null;
    }

    internal void Remove(Guid id)
    {
      using var connection = new SqlConnection("Server=localhost;Database=LinenandBird;Trusted_Connection=True;");
      connection.Open();

      var cmd = connection.CreateCommand();
      cmd.CommandText = @"Delete
                        From Birds
                        Where Id = @id";

      cmd.Parameters.AddWithValue("id", id);

      cmd.ExecuteNonQuery();
    }


    Bird MapFromReader(SqlDataReader reader)
    {
      var bird = new Bird();
      bird.Id = reader.GetGuid(0);
      bird.Size = reader["Size"].ToString();
      bird.Type = (BirdType)reader["Type"];
      bird.Color = reader["Color"].ToString();
      bird.Name = reader["Name"].ToString();

      return bird;
    }

  }
}
