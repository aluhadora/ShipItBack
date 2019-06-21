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
    public class MostPlayedArtistController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IList<MostPlayedArtist>> Get()
        {
            return new ActionResult<IList<MostPlayedArtist>>(Dal.GetMostPlayedArtist(null));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

    }
}
