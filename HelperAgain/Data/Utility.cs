using System.Data.Linq;
using Newtonsoft.Json;
using RestSharp;
using Cricketers.Database;
using Helper.Data;
using System.Windows.Controls;
using System.Windows;
using System.Linq;

namespace Cricketers.Data
{
    public class Utility
    {
        public void Extract()
        {
            RestClient cli = new RestClient("http://192.168.217.128:81/");

            RestRequest request = new RestRequest("/profile/_temp_view", Method.POST);
            request.RequestFormat = DataFormat.Json;
            CouchJson json = new CouchJson();
            json.map = @"function(doc) {
                                        if(doc.bat['ODIs'] || doc.bowl['ODIs']) emit(doc,null);
                                    }";
            request.AddBody(json);
            cli.ExecuteAsync(request, delegate(RestResponse re)
            {
                CouchResponse docs = JsonConvert.DeserializeObject<CouchResponse>(re.Content);


                foreach (CouchData data in docs.rows)
                {
                    Document doc = data.key;
                    EntitySet<Team> teams = new EntitySet<Team>();
                    foreach (string team in doc.profile.Teams)
                    {
                        teams.Add(new Team { Name = team });
                    }

                    EntitySet<Debut> debuts = new EntitySet<Debut>();
                    if (!doc.debut.odi.Equals("-")) debuts.Add(new Debut { MatchType = 0, Match = doc.debut.odi });
                    if (!doc.debut.test.Equals("-")) debuts.Add(new Debut { MatchType = 1, Match = doc.debut.test });
                    if (!doc.debut.firstClass.Equals("-")) debuts.Add(new Debut { MatchType = 2, Match = doc.debut.firstClass });
                    if (!doc.debut.t20i.Equals("-")) debuts.Add(new Debut { MatchType = 3, Match = doc.debut.t20i });
                    if (!doc.debut.twenty20.Equals("-")) debuts.Add(new Debut { MatchType = 4, Match = doc.debut.twenty20 });

                    EntitySet<BattingStat> battingStats = new EntitySet<BattingStat>();
                    if (!doc.bat.odi.matches.Equals("-"))
                        battingStats.Add(new BattingStat
                        {
                            MatchType = 0,
                            Runs = doc.bat.odi.runs,
                            Fours = doc.bat.odi.fours,
                            Sixes = doc.bat.odi.sixes,
                            Fifties = doc.bat.odi.fifties,
                            Hundreds = doc.bat.odi.hundreds,
                            Innings = doc.bat.odi.innings,
                            HighScore = doc.bat.odi.highScore,
                            Average = doc.bat.odi.average,
                            Catches = doc.bat.odi.catches,
                            Matches = doc.bat.odi.matches,
                            StrikeRate = doc.bat.odi.strikeRate,
                            Stumpings = doc.bat.odi.stumpings
                        });
                    if (!doc.bat.test.matches.Equals("-"))
                        battingStats.Add(new BattingStat
                        {
                            MatchType = 1,
                            Runs = doc.bat.test.runs,
                            Fours = doc.bat.test.fours,
                            Sixes = doc.bat.test.sixes,
                            Fifties = doc.bat.test.fifties,
                            Hundreds = doc.bat.test.hundreds,
                            Innings = doc.bat.test.innings,
                            HighScore = doc.bat.test.highScore,
                            Average = doc.bat.test.average,
                            Catches = doc.bat.test.catches,
                            Matches = doc.bat.test.matches,
                            StrikeRate = doc.bat.test.strikeRate,
                            Stumpings = doc.bat.test.stumpings
                        });
                    if (!doc.bat.firstClass.matches.Equals("-"))
                        battingStats.Add(new BattingStat
                        {
                            MatchType = 2,
                            Runs = doc.bat.firstClass.runs,
                            Fours = doc.bat.firstClass.fours,
                            Sixes = doc.bat.firstClass.sixes,
                            Fifties = doc.bat.firstClass.fifties,
                            Hundreds = doc.bat.firstClass.hundreds,
                            Innings = doc.bat.firstClass.innings,
                            HighScore = doc.bat.firstClass.highScore,
                            Average = doc.bat.firstClass.average,
                            Catches = doc.bat.firstClass.catches,
                            Matches = doc.bat.firstClass.matches,
                            StrikeRate = doc.bat.firstClass.strikeRate,
                            Stumpings = doc.bat.firstClass.stumpings
                        });
                    if (!doc.bat.twenty20.matches.Equals("-"))
                        battingStats.Add(new BattingStat
                        {
                            MatchType = 4,
                            Runs = doc.bat.twenty20.runs,
                            Fours = doc.bat.twenty20.fours,
                            Sixes = doc.bat.twenty20.sixes,
                            Fifties = doc.bat.twenty20.fifties,
                            Hundreds = doc.bat.twenty20.hundreds,
                            Innings = doc.bat.twenty20.innings,
                            HighScore = doc.bat.twenty20.highScore,
                            Average = doc.bat.twenty20.average,
                            Catches = doc.bat.twenty20.catches,
                            Matches = doc.bat.twenty20.matches,
                            StrikeRate = doc.bat.twenty20.strikeRate,
                            Stumpings = doc.bat.twenty20.stumpings
                        });
                    if (!doc.bat.t20i.matches.Equals("-"))
                        battingStats.Add(new BattingStat
                        {
                            MatchType = 3,
                            Runs = doc.bat.t20i.runs,
                            Fours = doc.bat.t20i.fours,
                            Sixes = doc.bat.t20i.sixes,
                            Fifties = doc.bat.t20i.fifties,
                            Hundreds = doc.bat.t20i.hundreds,
                            Innings = doc.bat.t20i.innings,
                            HighScore = doc.bat.t20i.highScore,
                            Average = doc.bat.t20i.average,
                            Catches = doc.bat.t20i.catches,
                            Matches = doc.bat.t20i.matches,
                            StrikeRate = doc.bat.t20i.strikeRate,
                            Stumpings = doc.bat.t20i.stumpings
                        });

                    EntitySet<BowlingStat> bowlingStats = new EntitySet<BowlingStat>();
                    if (!doc.bowl.odi.matches.Equals("-"))
                        bowlingStats.Add(new BowlingStat
                        {
                            MatchType = 0,
                            Matches = doc.bowl.odi.matches,
                            Average = doc.bowl.odi.average,
                            Balls = doc.bowl.odi.balls,
                            Wickets = doc.bowl.odi.wickets,
                            EconomyRate = doc.bowl.odi.economy,
                            StrikeRate = doc.bowl.odi.strikeRate,
                            InningsBest = doc.bowl.odi.inningsBest,
                            MatchBest = doc.bowl.odi.matchBest,
                            Innings = doc.bowl.odi.innings,
                            Fives = doc.bowl.odi.fives,
                            Fours = doc.bowl.odi.fours,
                            Tens = doc.bowl.odi.tens
                        });
                    if (!doc.bowl.test.matches.Equals("-"))
                        bowlingStats.Add(new BowlingStat
                        {
                            MatchType = 1,
                            Matches = doc.bowl.test.matches,
                            Average = doc.bowl.test.average,
                            Balls = doc.bowl.test.balls,
                            Wickets = doc.bowl.test.wickets,
                            EconomyRate = doc.bowl.test.economy,
                            StrikeRate = doc.bowl.test.strikeRate,
                            InningsBest = doc.bowl.test.inningsBest,
                            MatchBest = doc.bowl.test.matchBest,
                            Innings = doc.bowl.test.innings,
                            Fives = doc.bowl.test.fives,
                            Fours = doc.bowl.test.fours,
                            Tens = doc.bowl.test.tens
                        });
                    if (!doc.bowl.firstClass.matches.Equals("-"))
                        bowlingStats.Add(new BowlingStat
                        {
                            MatchType = 2,
                            Matches = doc.bowl.firstClass.matches,
                            Average = doc.bowl.firstClass.average,
                            Balls = doc.bowl.firstClass.balls,
                            Wickets = doc.bowl.firstClass.wickets,
                            EconomyRate = doc.bowl.firstClass.economy,
                            StrikeRate = doc.bowl.firstClass.strikeRate,
                            InningsBest = doc.bowl.firstClass.inningsBest,
                            MatchBest = doc.bowl.firstClass.matchBest,
                            Innings = doc.bowl.firstClass.innings,
                            Fives = doc.bowl.firstClass.fives,
                            Fours = doc.bowl.firstClass.fours,
                            Tens = doc.bowl.firstClass.tens
                        });
                    if (!doc.bowl.t20i.matches.Equals("-"))
                        bowlingStats.Add(new BowlingStat
                        {
                            MatchType = 3,
                            Matches = doc.bowl.t20i.matches,
                            Average = doc.bowl.t20i.average,
                            Balls = doc.bowl.t20i.balls,
                            Wickets = doc.bowl.t20i.wickets,
                            EconomyRate = doc.bowl.t20i.economy,
                            StrikeRate = doc.bowl.t20i.strikeRate,
                            InningsBest = doc.bowl.t20i.inningsBest,
                            MatchBest = doc.bowl.t20i.matchBest,
                            Innings = doc.bowl.t20i.innings,
                            Fives = doc.bowl.t20i.fives,
                            Fours = doc.bowl.t20i.fours,
                            Tens = doc.bowl.t20i.tens
                        });
                    if (!doc.bowl.twenty20.matches.Equals("-"))
                        bowlingStats.Add(new BowlingStat
                        {
                            MatchType = 4,
                            Matches = doc.bowl.twenty20.matches,
                            Average = doc.bowl.twenty20.average,
                            Balls = doc.bowl.twenty20.balls,
                            Wickets = doc.bowl.twenty20.wickets,
                            EconomyRate = doc.bowl.twenty20.economy,
                            StrikeRate = doc.bowl.twenty20.strikeRate,
                            InningsBest = doc.bowl.twenty20.inningsBest,
                            MatchBest = doc.bowl.twenty20.matchBest,
                            Innings = doc.bowl.twenty20.innings,
                            Fives = doc.bowl.twenty20.fives,
                            Fours = doc.bowl.twenty20.fours,
                            Tens = doc.bowl.twenty20.tens
                        });

                    Profile p = new Profile
                    {
                        Country = doc.country,
                        Born = doc.profile.Born,
                        BattingStyle = doc.profile.battingStyle,
                        BowlingStyle = doc.profile.bowlingStyle,
                        FieldingPosition = doc.profile.fieldingPosition,
                        Name = doc.profile.Name,
                        Teams = teams,
                        Debuts = debuts,
                        BattingStats = battingStats,
                        BowlingStats = bowlingStats
                    };

                    App.DB.Profiles.InsertOnSubmit(p);
                }
                App.DB.SubmitChanges();
            });
        }
    }
}
