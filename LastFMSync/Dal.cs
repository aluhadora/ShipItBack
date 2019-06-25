using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Text;
using LastFMSync.Dto;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace LastFMSync
{
    public class Dal
    {
        public static IDbConnection OpenConnection()
        {
            return ContextFactory.GetDbContext();
        }

        public static User GetNextUser()
        {
            using (var connection = OpenConnection())
            {
                using (var cmd = connection.SqlProc(@"dbo.User_Get_LeastRecentUpdate"))
                {
                    cmd.CommandTimeout = ContextFactory.CommandTimeout;

                    return  cmd.ExecuteReader().ConvertTo<User>(new SqlServer2008OrmLiteDialectProvider());
                }
            }
        }

        public static int? GetIdByGuid(Guid guid)
        {
            if (guid == Guid.Empty) return 0;

            using (var connection = OpenConnection())
            {
                using (var cmd = connection.SqlProc(@"dbo.Song_Get_ByGUID"))
                {
                    cmd.CommandTimeout = ContextFactory.CommandTimeout;

                    cmd.AddParam("Guid", guid);

                    var response = cmd.ExecuteScalar()?.ToString();
                    return int.TryParse(response, out int result) ? (int?) result : null;
                }
            }
        }

        public static int? GetIdByInfo(string songName, string songArtist, string songAlbum)
        {
            using (var connection = OpenConnection())
            {
                using (var cmd = connection.SqlProc(@"dbo.Song_Get_BySongArtistAlbum"))
                {
                    cmd.CommandTimeout = ContextFactory.CommandTimeout;

                    cmd.AddParam("SongName", songName);
                    cmd.AddParam("ArtistName",  songArtist);
                    cmd.AddParam("AlbumTitle",  songAlbum);

                    dynamic response = cmd.ExecuteScalar()?.ToString();
                    return int.TryParse(response, out int result) ? (int?)result : null;
                }
            }
        }

        public static int? SaveNewSong(Song song)
        {
            using (var connection = OpenConnection())
            {
                using (var cmd = connection.SqlProc(@"dbo.Song_Save"))
                {
                    cmd.CommandTimeout = ContextFactory.CommandTimeout;

                    cmd.AddParam("SongName", song.SongName);
                    cmd.AddParam("ArtistName", song.Artist);
                    cmd.AddParam("AlbumTitle", song.Album);
                    cmd.AddParam("GUID", song.Guid);
                    cmd.AddParam("Image", song.Image);

                    dynamic response = cmd.ExecuteScalar()?.ToString();
                    return int.TryParse(response, out int result) ? (int?)result : null;
                }
            }
        }

        public static void SaveListens(List<Listen> listens)
        {
            foreach (var listen in listens)
            {
                SaveListen(listen);
            }
        }

        private static void SaveListen(Listen listen)
        {
            using (var connection = OpenConnection())
            {
                using (var cmd = connection.SqlProc(@"dbo.Usersong_Save_Insert"))
                {
                    cmd.CommandTimeout = ContextFactory.CommandTimeout;

                    cmd.AddParam("SongId", listen.SongId);
                    cmd.AddParam("UserId", listen.UserId);
                    cmd.AddParam("PlayedOn", listen.PlayedOn);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateUserTime(User user, DateTime utcNow)
        {
            using (var connection = OpenConnection())
            {
                using (var cmd = connection.SqlProc(@"dbo.User_Save_Update"))
                {
                    cmd.CommandTimeout = ContextFactory.CommandTimeout;

                    cmd.AddParam("UserId", user.UserId);
                    cmd.AddParam("LastUpdated", utcNow);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateUserLastListened(User user)
        {
            using (var connection = OpenConnection())
            {
                using (var cmd = connection.SqlProc(@"dbo.User_Save_Update"))
                {
                    cmd.CommandTimeout = ContextFactory.CommandTimeout;

                    cmd.AddParam("UserId", user.UserId);
                    cmd.AddParam("LastListened", user.LastListened);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
