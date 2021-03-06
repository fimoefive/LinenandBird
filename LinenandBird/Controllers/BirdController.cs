using LinenandBird.DataAccess;
using LinenandBird.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenandBird.Controllers
{
  [Route("api/birds")]
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

    [HttpGet("{id}")]
    public IActionResult GetBirdById(Guid id)
    {
      var bird = _repo.GetById(id);

      if (bird is null)
      {
        return NotFound($"No bird with the id {id} was found.");
      }
      return Ok(bird);
    }

    [HttpPost]
    public IActionResult AddBird(Bird newBird)
    {
      if (string.IsNullOrEmpty(newBird.Name) || string.IsNullOrEmpty(newBird.Color))
      {
        return BadRequest("Name and Color are required fields");
      }
      _repo.Add(newBird);

      return Created("/api/birds/1", newBird);
    }

    // 
    [HttpDelete("{id}")]
    public IActionResult DeleteBird(Guid id)
    {
      _repo.Remove(id);

      return Ok();
    }

    // 
    [HttpPut("{id}")]
    public IActionResult UpdateBird(Guid id, Bird bird)
    {
      var birdToUpdate = _repo.GetById(id);

      if (birdToUpdate is null)
      {
        return NotFound($"");
      }

      var updateBird = _repo.Update(id, bird);
      return Ok(updateBird);

    }

  }
}
