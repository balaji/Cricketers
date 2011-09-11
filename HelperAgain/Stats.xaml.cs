using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Cricketers.Database;
using System.Collections.ObjectModel;
using Cricketers.Data;
using System.Text;

namespace Cricketers {
    public partial class Stats : PhoneApplicationPage {
        public Stats() {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            string id = "";
            if (NavigationContext.QueryString.TryGetValue("id", out id)) {
                var p = (from profile in App.DB.Profiles where profile.ProfileId == Int32.Parse(id) select profile).Single();
                name.Text = p.Name;
                born.Text = p.Born;
                battingStyle.Text = p.BattingStyle;
                if (!p.BowlingStyle.Equals("-")) {
                    bowlingStyle.Text = p.BowlingStyle;
                }
                if (!p.FieldingPosition.Equals("-")) {
                    fieldingPosition.Text = p.FieldingPosition;
                }
                teams.ItemsSource = p.Teams;
                var debuts = p.Debuts;
                foreach (Debut debut in debuts) {
                    switch (debut.MatchType) {
                        case 0:
                            ODI.Text = debut.Match;
                            break;
                        case 1:
                            Test.Text = debut.Match;
                            break;
                        case 2:
                            FirstClass.Text = debut.Match;
                            break;
                        case 3:
                            T20I.Text = debut.Match;
                            break;
                        case 4:
                            Twenty20.Text = debut.Match;
                            break;
                    }
                }

                var battingStats = p.BattingStats;
                var batDic = new Dictionary<int, string>() {
                        {0, "odiBat"}, {1, "testBat"}, {2, "fcBat"}, {3, "t20IBat"}, {4, "t20LBat"}
                    };
                var bowlDic = new Dictionary<int, string>() {
                        {0, "odiBowl"}, {1, "testBowl"}, {2, "fcBowl"}, {3, "t20IBowl"}, {4, "t20LBowl"}
                    };
                foreach (BattingStat stat in battingStats) {
                    (battingStatGrid.FindName(batDic[stat.MatchType] + "Mts") as Cricketers.BlockWithBorder).DisplayText = stat.Matches;
                    (battingStatGrid.FindName(batDic[stat.MatchType] + "Inns") as Cricketers.BlockWithBorder).DisplayText = stat.Innings;
                    (battingStatGrid.FindName(batDic[stat.MatchType] + "Runs") as Cricketers.BlockWithBorder).DisplayText = stat.Runs;
                    (battingStatGrid.FindName(batDic[stat.MatchType] + "Six") as Cricketers.BlockWithBorder).DisplayText = stat.Sixes;
                    (battingStatGrid.FindName(batDic[stat.MatchType] + "Ave") as Cricketers.BlockWithBorder).DisplayText = stat.Average;
                    (battingStatGrid.FindName(batDic[stat.MatchType] + "Sr") as Cricketers.BlockWithBorder).DisplayText = stat.StrikeRate;
                    (battingStatGrid.FindName(batDic[stat.MatchType] + "Hs") as Cricketers.BlockWithBorder).DisplayText = stat.HighScore;
                    (battingStatGrid.FindName(batDic[stat.MatchType] + "Huns") as Cricketers.BlockWithBorder).DisplayText = stat.Hundreds;
                    (battingStatGrid.FindName(batDic[stat.MatchType] + "Fifs") as Cricketers.BlockWithBorder).DisplayText = stat.Fifties;
                    (battingStatGrid.FindName(batDic[stat.MatchType] + "Nos") as Cricketers.BlockWithBorder).DisplayText = stat.NotOuts;
                    (battingStatGrid.FindName(batDic[stat.MatchType] + "Cts") as Cricketers.BlockWithBorder).DisplayText = stat.Catches;
                    (battingStatGrid.FindName(batDic[stat.MatchType] + "Sts") as Cricketers.BlockWithBorder).DisplayText = stat.Stumpings;
                    (battingStatGrid.FindName(batDic[stat.MatchType] + "Fours") as Cricketers.BlockWithBorder).DisplayText = stat.Fours;
                }

                var bowlingStats = p.BowlingStats;
                foreach (BowlingStat stat in bowlingStats) {
                    (bowlingStatGrid.FindName(bowlDic[stat.MatchType] + "Mts") as Cricketers.BlockWithBorder).DisplayText = stat.Matches;
                    (bowlingStatGrid.FindName(bowlDic[stat.MatchType] + "Inns") as Cricketers.BlockWithBorder).DisplayText = stat.Innings;
                    (bowlingStatGrid.FindName(bowlDic[stat.MatchType] + "Wkts") as Cricketers.BlockWithBorder).DisplayText = stat.Wickets;
                    (bowlingStatGrid.FindName(bowlDic[stat.MatchType] + "Ave") as Cricketers.BlockWithBorder).DisplayText = stat.Average;
                    (bowlingStatGrid.FindName(bowlDic[stat.MatchType] + "Er") as Cricketers.BlockWithBorder).DisplayText = stat.EconomyRate;
                    (bowlingStatGrid.FindName(bowlDic[stat.MatchType] + "Bi") as Cricketers.BlockWithBorder).DisplayText = stat.InningsBest;
                    (bowlingStatGrid.FindName(bowlDic[stat.MatchType] + "Bm") as Cricketers.BlockWithBorder).DisplayText = stat.MatchBest;
                    (bowlingStatGrid.FindName(bowlDic[stat.MatchType] + "Ten") as Cricketers.BlockWithBorder).DisplayText = stat.Tens;
                    (bowlingStatGrid.FindName(bowlDic[stat.MatchType] + "Five") as Cricketers.BlockWithBorder).DisplayText = stat.Fives;
                    (bowlingStatGrid.FindName(bowlDic[stat.MatchType] + "Four") as Cricketers.BlockWithBorder).DisplayText = stat.Fours;
                    (bowlingStatGrid.FindName(bowlDic[stat.MatchType] + "Over") as Cricketers.BlockWithBorder).DisplayText = new StringBuilder((Int32.Parse(stat.Balls) / 6).ToString()).Append(".").Append((Int32.Parse(stat.Balls) % 6).ToString()).ToString();
                    (bowlingStatGrid.FindName(bowlDic[stat.MatchType] + "Sr") as Cricketers.BlockWithBorder).DisplayText = stat.StrikeRate;
                }
            }
        }
    }
}