using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ServiceStack.OrmLite;

namespace LastFMSync
{
    internal class ContextFactory
    {
        public static IDbConnection GetDbContext()
        {
            var connectString = "Server=tcp:shipit2019.database.windows.net,1433;Initial Catalog=ShipIT2019;Persist Security Info=False;User ID=shipitadmin;Password=s1db@ShipIT;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
            var dbFactory = new OrmLiteConnectionFactory(connectString, SqlServerDialect.Provider);
            return dbFactory.Open();
        }

        public static int CommandTimeout = 90;
    }
}
