using System;
using LastFMSync.Dto;

namespace LastFMSync
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var user = Dal.GetNextUser();
            Console.WriteLine(user.Username);

            Dal.UpdateUserTime(user, DateTime.UtcNow);

            new UserPull().PullTracks(user);
        }
    }
}
