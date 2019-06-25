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
    public class HistoryController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IList<Listen>> Get()
        {
            return null;
            //return new ActionResult<IList<Listen>>(Dal.GetHistory(null));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<IList<Listen>> Get(int id)
        {
            return null;
            //return new ActionResult<IList<Listen>>(Dal.GetHistory(id));
        }

    }
}
