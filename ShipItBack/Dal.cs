using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ShipItBack.Dtos;

namespace ShipItBack
{
    public class Dal
    {
        public static IDbConnection OpenConnection()
        {
            return ContextFactory.GetDbContext();
        }

        public static IList<MostPlayedSongs> GetMostPlayedSongs(int? userId)
        {
            using (var connection = OpenConnection())
            {
                using (var cmd = connection.SqlProc(@"dbo.UserSong_Get_MostPlayedSongs"))
                {
                    cmd.CommandTimeout = ContextFactory.CommandTimeout;

                    cmd.AddParam("UserId", userId);

                    return cmd.ExecuteReader().ConvertToList<MostPlayedSongs>(new SqlServer2008OrmLiteDialectProvider());
                }
            }
        }

        public static IList<MostSongsPlayed> GetMostSongsPlayed(int userId)
        {
            using (var connection = OpenConnection())
            {
                using (var cmd = connection.SqlProc(@"dbo.UserSong_Get_MostSongsPlayed"))
                {
                    cmd.CommandTimeout = ContextFactory.CommandTimeout;

                    cmd.AddParam("UserId", userId);

                    return cmd.ExecuteReader().ConvertToList<MostSongsPlayed>(new SqlServer2008OrmLiteDialectProvider());
                }
            }
        }

        public static IList<MostPlayedArtist> GetMostPlayedArtist(int? userId)
        {
            using (var connection = OpenConnection())
            {
                using (var cmd = connection.SqlProc(@"dbo.UserSong_Get_MostPlayedArtist"))
                {
                    cmd.CommandTimeout = ContextFactory.CommandTimeout;

                    cmd.AddParam("UserId", userId);

                    return cmd.ExecuteReader().ConvertToList<MostPlayedArtist>(new SqlServer2008OrmLiteDialectProvider());
                }
            }
        }

        public static IList<MostPopularArtist> GetMostPopularArtist(string artistName)
        {
            using (var connection = OpenConnection())
            {
                using (var cmd = connection.SqlProc(@"dbo.UserSong_Get_MostPopularArtist"))
                {
                    cmd.CommandTimeout = ContextFactory.CommandTimeout;

                    cmd.AddParam("ArtistName", artistName);

                    return cmd.ExecuteReader().ConvertToList<MostPopularArtist>(new SqlServer2008OrmLiteDialectProvider());
                }
            }
        }

        public static IList<MostPopularAlbum> GetMostPopularAlbum(string albumTitle)
        {
            using (var connection = OpenConnection())
            {
                using (var cmd = connection.SqlProc(@"dbo.UserSong_Get_MostPopularAlbum"))
                {
                    cmd.CommandTimeout = ContextFactory.CommandTimeout;

                    cmd.AddParam("AlbumTitle", albumTitle);

                    return cmd.ExecuteReader().ConvertToList<MostPopularAlbum>(new SqlServer2008OrmLiteDialectProvider());
                }
            }
        }
        public static IList<MostPopularSong> GetMostPopularSong(int? songId, string songName)
        {
            using (var connection = OpenConnection())
            {
                using (var cmd = connection.SqlProc(@"dbo.UserSong_Get_MostPopularSong"))
                {
                    cmd.CommandTimeout = ContextFactory.CommandTimeout;

                    cmd.AddParam("SongId", songId);
                    cmd.AddParam("SongName", songName);

                return cmd.ExecuteReader().ConvertToList<MostPopularSong>(new SqlServer2008OrmLiteDialectProvider());
                }
            }
        }
    }
}
