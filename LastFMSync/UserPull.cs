using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using LastFMSync.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LastFMSync
{
    public class UserPull
    {
        private const string ApiKey = "888f48f85285e39075b7e1a461863d81";
        private const string BaseURL = "https://ws.audioscrobbler.com/2.0/?method=user.getrecenttracks&format=json&limit=50";
        private const string UserParameter = "&user=";
        private const string KeyParameter = "&api_key=";

        private User _user;

        public void PullTracks(User user)
        {
            _user = user;
            var response = ContactLastFM(_user);

            var uniqueSongs = new List<Song>();
            var listens = new List<Listen>();

            PopulateSongsAndListens(response, uniqueSongs, listens);

            PopulateSongsWithIds(uniqueSongs);
            SaveNewSongs(uniqueSongs);
            PopulateListens(listens);
            SaveListens(listens);
        }

        private void SaveListens(List<Listen> listens)
        {
            Dal.SaveListens(listens);
        }

        private void PopulateSongsAndListens(string response, List<Song> uniqueSongs, List<Listen> listens)
        {
            dynamic obj = JObject.Parse(response);
            var lastListenedUpdated = false;

            if (obj.recenttracks["@attr"].total.ToString() == "0") return;
            foreach (dynamic track in obj.recenttracks.track)
            {
                if (!lastListenedUpdated)
                {
                    lastListenedUpdated = true;
                    try
                    {
                        _user.LastListened = Convert.ToInt64(track.date?.uts ?? 0);
                    }
                    catch (Exception e)
                    {
                        _user.LastListened = 0;
                    }
                    if (_user.LastListened == 0)
                    {
                        _user.LastListened = UtsNow();
                    }
                    Dal.UpdateUserLastListened(_user);
                }

                Guid mbid = Guid.Empty;

                try
                {
                    mbid = String.IsNullOrEmpty(track?.mbid?.ToString()) ? Guid.Empty : (Guid)Guid.Parse(track.mbid.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                if (mbid != Guid.Empty && uniqueSongs.Any(x => x.Guid == mbid)) continue;

                var song = Song.Parse(track.ToString(), mbid);
                if (song == null) continue;
                listens.Add(new Listen(track.date?.uts.ToString() ?? UtsNow().ToString(), song));
                uniqueSongs.Add(song);
            }
        }

        private long? UtsNow()
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = DateTime.UtcNow - origin;
            return (long?)Math.Floor(diff.TotalSeconds);
        }

        private void PopulateListens(List<Listen> listens)
        {
            foreach (var listen in listens)
            {
                listen.UserId = _user?.UserId ?? 0;
                listen.SongId = listen.Song.SongID;
            }
        }

        private void SaveNewSongs(IEnumerable<Song> songs)
        {
            foreach (var song in songs.Where(x => x.SongID == 0))
            {
                song.SongID = Dal.SaveNewSong(song) ?? 0;
            }
        }

        private void PopulateSongsWithIds(IList<Song> songs)
        {
            foreach (var song in songs.Where(x => x.Guid != Guid.Empty))
            {
                song.SongID = Dal.GetIdByGuid(song.Guid) ?? 0;
            }

            foreach (var song in songs.Where(x => x.Guid == Guid.Empty))
            {
                song.SongID = Dal.GetIdByInfo(song.SongName, song.Artist, song.Album) ?? 0;
            }
        }

        private static string ContactLastFM(User user)
        {
            var url = $"{BaseURL}{KeyParameter}{ApiKey}{UserParameter}{user.Username}&from={user.LastListened}";

            var request = WebRequest.Create(url);
            var stream = new StreamReader(request.GetResponse().GetResponseStream() ??
                                          throw new InvalidOperationException("No response"));

            return stream.ReadToEnd();
        }
    }
}
