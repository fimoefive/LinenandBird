using LinenandBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
//using Dapper;

namespace LinenandBird.DataAccess
{
  public class BirdRepository
  {
    static List<Bird> _birds = new List<Bird>
    {
      new Bird
      {
       Id = Guid.NewGuid(),
       Name = "Jimmy",
       Color = "Red",
       Size = "Small",
       Type = BirdType.Dead,
       Accessories = new List<string> {"Beanie", "Gold Wing Tips"}
      }
    };

    internal IEnumerable<Bird> GetAll()
    {
     using var connection = new SqlConnection("Server=localhost;Database=LinenandBird;Trusted_Connection=True;");

      connection.Open();

      var command = connection.CreateCommand();
          command.CommandText = @"Select * 
                                From Birds";

      var reader = command.ExecuteReader();
      var birds = new List<Bird>();

      // Block of code till bird.Add is translation mapping to SQL
      // WHile Loop
      while(reader.Read())
      {// ORM style Mapping
        // Oridnal 
         var bird = new Bird();
         bird.Id = reader.GetGuid(0);
        // Column Name String
         bird.Size = reader["Size"].ToString(); 
        // Direct Cast || Explicit Casting
         bird.Type = (BirdType)reader["type"];
        //Same result as Explicit Casting but with and Enum.TryParse
        //Enum.TryParse<BirdType>(reader["Type"].ToString(), out var birdType); 
        //bird.Type = birdType;

        bird.Color = reader["Color"].ToString();
        bird.Name = reader["Size"].ToString();

       // var bird = MapFromReader(reader);
        birds.Add(bird);
      }

      return birds;
   // return _birds;
    }

    internal void Add(Bird newBird)
    {
      newBird.Id = Guid.NewGuid();

      _birds.Add(newBird);
    }

    internal Bird GetById(Guid birdId)
    {
      return _birds.FirstOrDefault(bird => bird.Id == birdId);
    }

    //[HttpDelete("{id}")]
    //public IActionResult DeleteBird(Guid id)
    //{
    //  _repo.Remove(id);
    // return Ok();
    //}


  }
}
