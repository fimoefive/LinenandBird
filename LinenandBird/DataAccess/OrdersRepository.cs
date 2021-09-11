using LinenandBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenandBird.DataAccess
{
  public class OrdersRepository
  {
    static List<Order> _orders = new List<Order>();

    internal void Add(Order order)
    {
      order.Id = Guid.NewGuid();

      _orders.Add(order);
    }
  }
}