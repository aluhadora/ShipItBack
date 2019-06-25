using System;
using System.Collections.Generic;
using System.Text;

namespace LastFMSync.Dto
{
    public class User
    {
        public string Alias { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }
        public long? LastListened { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
