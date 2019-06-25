using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LastFMSync.Dto
{
    public class Song
    {
        public string SongName { get; set; }
        public int SongID { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public Guid Guid { get; set; }
        public string Image { get; set; }

        public override string ToString()
        {
            return $"{SongName} - {Artist} ({Album})";
        }

        public static Song Parse(string json, Guid mbid)
        {
            try
            {
                dynamic track = JObject.Parse(json);

                var song = new Song
                {
                    Guid = mbid,
                    SongName = track.name.ToString(),
                    Album = track.album["#text"].ToString(),
                    Artist = track.artist["#text"].ToString(),
                };

                try
                {
                    song.Image = track.image[3]["#text"].ToString();
                }
                catch (Exception e)
                {

                }

                return song;
            }
            catch
            {
                return null;
            }
        }
    }
}
