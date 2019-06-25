using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipItBack.Dtos
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Alias { get; set; }
        public string Department { get; set; }
        public DateTime? LastUpdated { get; set; }
        public long LastListened { get; set; }
    }
}
