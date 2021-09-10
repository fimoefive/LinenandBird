using LinenandBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LinenandBird.Models.Hat;

namespace LinenandBird.DataAccess
{
  public class HatRepository
  {
    static List<Hat> _hats = new List<Hat>
    {
        new Hat
        {
          Id = Guid.NewGuid(),
          Color = "Blue",
          Designer = "Charlie",
          Style = HatStyle.Openback
        },
        new Hat
        {
           Id = Guid.NewGuid(),
          Color = "Black",
          Designer = "Nathan",
          Style = HatStyle.WideBrim
        },
        new Hat
        {
           Id = Guid.NewGuid(),
          Color = "Red",
          Designer = "Charlie",
          Style = HatStyle.Normal
        }
    };


    internal List<Hat> GetAll()
    {
      return _hats;
    }

    internal IEnumerable<Hat> GetByStyle(HatStyle style)
    {
       return _hats.Where(hat => hat.Style == style);
    }

    internal void Add(Hat newHat)
    {
      newHat.Id = Guid.NewGuid();

      _hats.Add(newHat);
    }


  }
}
