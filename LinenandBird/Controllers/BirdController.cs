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
      public IEnumerable<Bird> GetAllBirds()
    {
      return _repo.GetAll();
    }

    public void AddBird(Bird newBird)
    {
      _repo.Add(newBird);
    }

  }
}
