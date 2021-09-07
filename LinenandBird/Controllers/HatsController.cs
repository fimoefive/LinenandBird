﻿using LinenandBird.Models;
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
    static List<Hat> _hats = new List<Hat>
    {
        new Hat
        {
          Color = "Blue",
          Designer = "Charlie",
          Style = HatStyle.Openback
        },
        new Hat
        {
          Color = "Black",
          Designer = "Nathan",
          Style = HatStyle.WideBrim
        },
        new Hat
        {
          Color = "Red",
          Designer = "Charlie",
          Style = HatStyle.Normal
        }
    };

    [HttpGet]
    public List<Hat> GetAllHats()
    {
      return _hats;
    }

    // Get api/hats/styles/1 -> all open backed hats
    [HttpGet("styles/{style}")]
    public List<Hat> GetHatsByStyle(HatStyle style)
    {
      var matches = _hats.Where(hat => hat.Style == style);
      return matches.ToList();
    }

    [HttpPost]
    public void AddHat (Hat newHat)
    {
      _hats.Add(newHat);
    }
  }
}
