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
    public class MostPopularSongController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IList<MostPopularSong>> Get()
        {
            return new ActionResult<IList<MostPopularSong>>(Dal.GetMostPopularSong(null, null));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int songId)
        {
            return "value";
        }

        [HttpGet("{songName}")]
        public ActionResult<string> Get(string songName)
        {
            return "value";
        }

    }
}
