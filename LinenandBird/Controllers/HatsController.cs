using LinenandBird.Models;
using LinenandBird.DataAccess;
using LinenandBird.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LinenandBird.Models.Hat;

namespace LinenandBird.Controllers
{
  // [Route("api/[controller]")]  // exposed at this endpoint. alternative way [Route("api/hats")]
  [Route("api/hats")]
  [ApiController] // an API controller, so it returns Json or xml
  public class HatsController : ControllerBase
  {
    HatRepository _repo;
    public HatsController()
      {
        _repo = new HatRepository();
      }

    [HttpGet]
    public List<Hat> GetAllHats()
    {
      var repo = new HatRepository();
      return repo.GetAll();
    }

    // Get api/hats/styles/1 -> all open backed hats
    [HttpGet("styles/{style}")]
    //public IEnumerable<Hat> GetHatsByStyle(Hatstyle)
    //var matches = _hats.Where(hat => hat.Style == style);
    //return matches;
    public List<Hat> GetHatsByStyle(HatStyle style)
    {
      var matches = _hats.Where(hat => hat.Style == style);
      return matches.ToList();
    }

    [HttpPost]
    public void AddHat (Hat newHat)
    {
      var repo = new HatRepository();
      repo.Add(newHat);
    }



  }
}
