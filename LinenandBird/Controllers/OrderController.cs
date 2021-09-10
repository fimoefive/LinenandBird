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
  [Route("api/orders")]
  [ApiController]
  public class OrderController : ControllerBase
  {
    BirdRepository _birdRepository;
    HatRepository _hatRepository;
    OrdersRepository _orderRepository;

    public OrderController()
    {
      _birdRepository = new BirdRepository();
      _hatRepository = new HatRepository();
      _orderRepository = new OrdersRepository();
    }

    public IActionResult CreateOrder(CreateOrderCommand command)
    {
      var hatToOrder = _hatRepository.GetById(command.HatId);
      var birdToOrder = _birdRepository.GetById(command.BirdId);

    }



  }
}
