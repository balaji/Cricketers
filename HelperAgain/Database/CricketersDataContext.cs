using System.Data.Linq;

namespace Cricketers.Database
{
    public class CricketersDataContext : DataContext
    {
        public static string DBConnectionString = "Data Source = 'appdata:/alpha2.sdf'; File Mode = read only;";
        //public static string DBConnectionString = "Data Source=isostore:/alpha2.sdf";

        public CricketersDataContext(string connectionString)
            : base(connectionString)
        {
        }

        public Table<Profile> Profiles;
        public Table<Debut> Debuts;
        public Table<Team> Teams;
        public Table<BattingStat> BattingStats;
        public Table<BowlingStat> BowlingStats;
    }
}
