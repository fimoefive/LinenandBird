﻿using LinenandBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

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
      while (reader.Read())
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
        bird.Name = reader["Name"].ToString();

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
      using var connection = new SqlConnection("Server=localhost;Database=LinenandBird;Trusted_Connection=True;");

      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"Select * 
                            From Birds
                            where id = @id";

      command.Parameters.AddWithValue("id", birdId);

      var reader = command.ExecuteReader();

      if (reader.Read())
      {
        var bird = new Bird();
        bird.Id = reader.GetGuid(0);
        // Column Name String
        bird.Size = reader["Size"].ToString();
        // Direct Cast || Explicit Casting
        bird.Type = (BirdType)reader["Type"];
        bird.Color = reader["Color"].ToString();
        bird.Name = reader["Name"].ToString();

        return bird;
      }
      return default; // return null;
      // return _birds.FirstOrDefault(bird => bird.Id == birdId);
    }


  }
}
