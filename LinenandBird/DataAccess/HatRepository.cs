using LinenandBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenandBird.DataAccess
{
  public class HatRepository
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


    internal List<Hat> GetAll()
    {
      return _hats;
    }

    internal void Add(Hat newHat)
    {
      return;
    }
  }
}
