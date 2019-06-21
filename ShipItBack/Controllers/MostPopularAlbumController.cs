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
    public class MostPopularAlbumController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IList<MostPopularAlbum>> Get()
        {
            return new ActionResult<IList<MostPopularAlbum>>(Dal.GetMostPopularAlbum(null));
        }

        // GET api/values/5
        [HttpGet("{albumTitle}")]
        public ActionResult<string> Get(string albumTitle)
        {
            return "value";
        }

    }
}
