using System;
using LastFMSync;
using Microsoft.AspNetCore.Mvc;

namespace ShipItBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        // POST api/sync
        [HttpGet]
        public LastFMSync.Dto.User Post()
        {
            var user = LastFMSync.Dal.GetNextUser();
            Console.WriteLine(user.Username);

            LastFMSync.Dal.UpdateUserTime(user, DateTime.UtcNow);

            new UserPull().PullTracks(user);

            return user;
        }

    }
}