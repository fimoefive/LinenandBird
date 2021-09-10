using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenandBird.Controllers
{
  [Route("api/orders")]
  [ApiController]
  public class OrderController : ControllerBase
  {

    public IActionResult CreateOrder()
    {

    }
  }
}
