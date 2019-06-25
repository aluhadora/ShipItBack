using System;
using System.Collections.Generic;
using System.Text;

namespace LastFMSync.Dto
{
    public class Listen
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public Listen(string utc, Song song)
        {
            Song = song;
            PlayedOn = Epoch.AddSeconds(Convert.ToInt64(utc));
        }

        public int UserId { get; set; }
        public int SongId { get; set; }
        public DateTime PlayedOn { get; set; }

        public Song Song { get; set; }
    }
}
