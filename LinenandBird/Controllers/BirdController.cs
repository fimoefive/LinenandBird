﻿using LinenandBird.DataAccess;
using LinenandBird.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenandBird.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BirdController : ControllerBase
  {
    BirdRepository _repo;

    public BirdController()
    {
      _repo = new BirdRepository();
    }

    [HttpGet]
      //public IEnumerable<Bird> GetAllBirds()
       public IActionResult GetAllBirds()
    {
      return Ok(_repo.GetAll());
      //return _repo.GetAll();
    }

    [HttpPost]
    public void AddBird(Bird newBird)
    {
      if (string.IsNullOrEmpty(newBird.name) || string.IsNullOrEmpty(newBird.Color))
      {
        return BadRequest("Name and Color are required fields");
      }
      _repo.Add(newBird);

      return Created("/api/birds/1", newBird);
    }

  }
}
