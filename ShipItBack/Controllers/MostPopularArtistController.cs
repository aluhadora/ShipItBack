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
    public class MostPopularArtistController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IList<MostPopularArtist>> Get()
        {
            return new ActionResult<IList<MostPopularArtist>>(Dal.GetMostPopularArtist(null));
        }

        // GET api/values/5
        [HttpGet("{artistName}")]
        public ActionResult<string> Get(string artistName)
        {
            return "value";
        }

    }
}
