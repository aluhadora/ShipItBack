using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipItBack.Dtos
{
    public class Listen
    {
        public string SongName { get; set; }
        public string ArtistName { get; set; }
        public string AlbumTitle { get; set; }
        public string Image { get; set; }
        public DateTime? PlayedOn { get; set; }
    }
}
