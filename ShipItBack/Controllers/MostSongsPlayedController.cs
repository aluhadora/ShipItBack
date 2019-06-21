using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShipItBack.Dtos;

namespace ShipItBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MostSongsPlayedController : ControllerBase
    {
        // GET api/values
        [HttpGet("{id}")]
        public ActionResult<IList<MostSongsPlayed>> Get(int id)
        {
            return new ActionResult<IList<MostSongsPlayed>>(Dal.GetMostSongsPlayed(id));
        }

    }
}
