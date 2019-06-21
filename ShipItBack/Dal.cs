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
    }
}
