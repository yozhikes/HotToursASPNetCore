using HotToursLogic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.Intrinsics.Arm;
using WALast.Models;

namespace WALast.Controllers
{
    [Route("Tour/[controller]")]
    [ApiController]
    public class HotTourController : Controller
    {
        private static readonly IList<TourID> tourapi = new List<TourID>();
        [HttpGet]
        public IEnumerable<TourID> Get()
        {
            return tourapi;
        }
        [HttpGet("statistic")]
        public Stats GetStatistic()
        {
            var res = new Stats()
            {
                qtytours = tourapi.Count(),
                sumtours = tourapi.Sum(x => x.Sum),
                qtydops = tourapi.Count(x => x.Dop != 0),
                sumdop = tourapi.Sum(x => x.Dop),
            };
            return res;
        }
        [HttpPost]
        public TourID Add(Tour models)
        {
            var tr = new TourID
            {
                Id = Guid.NewGuid(),
                Direction = models.Direction,
                Date = models.Date,
                Dop = models.Dop,
                Nights = models.Nights,
                Price = models.Price,
                Qty = models.Qty,
                Wifi = models.Wifi,
                Sum = models.Nights*models.Qty*models.Price+models.Dop,
            };
            tourapi.Add(tr);
            return tr;
        }

        [HttpPut("{id:guid}")]
        public TourID Update([FromRoute] Guid id, [FromBody] Tour models)
        {

            var tr = tourapi.FirstOrDefault(x => x.Id == id);
            if (tr != null)
            {
                tr.Direction = models.Direction;
                tr.Date = models.Date;
                tr.Dop = models.Dop;
                tr.Nights = models.Nights;
                tr.Price = models.Price;
                tr.Qty = models.Qty;
                tr.Wifi = models.Wifi;
                tr.Sum = models.Nights * models.Qty * models.Price + models.Dop;
            }
            return tr;
        }

        [HttpDelete("{id:guid}")]
        public bool Remove(Guid id)
        {
            var tr = tourapi.FirstOrDefault(x => x.Id == id);
            if (tr != null)
            {
                return tourapi.Remove(tr);
            }
            return false;
        }
    }
}
