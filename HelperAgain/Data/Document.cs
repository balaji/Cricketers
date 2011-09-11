using System.Runtime.Serialization;

namespace Helper.Data {

    [DataContract]
    public class CouchJson {
        [DataMember]
        public string map { get; set; }
        [DataMember]
        public string reduce { get; set; }
    }

    public class CouchResponse {
        public CouchData[] rows;
    }

    [DataContract]
    public class CouchData {
        [DataMember]
        public Document key;
    }

    public class Document {
        public string country { get; set; }
        public string image { get; set; }
        public DocProfile profile { get; set; }
        public DocDebut debut { get; set; }
        public Batting bat { get; set; }
        public Bowling bowl { get; set; }
    }

    [DataContract]
    public class DocProfile {
        [DataMember(Name = "Batting style")]
        public string battingStyle = "-";

        [DataMember(Name = "Bowling style")]
        public string bowlingStyle = "-";

        [DataMember(Name = "Full name")]
        public string Name { get; set; }

        [DataMember(Name = "Born")]
        public string Born { get; set; }

        [DataMember(Name = "Fielding position")]
        public string fieldingPosition = "-";

        [DataMember(Name = "Major teams")]
        public string[] Teams { get; set; }
    }

    [DataContract]
    public class DocDebut {
        [DataMember(Name = "ODI debut")]
        public string odi = "-";

        [DataMember(Name = "Test debut")]
        public string test = "-";

        [DataMember(Name = "First-class debut")]
        public string firstClass = "-";

        [DataMember(Name = "Twenty20 debut")]
        public string twenty20 = "-";

        [DataMember(Name = "T20I debut")]
        public string t20i = "-";
    }

    [DataContract]
    public class Batting {
        [DataMember(Name = "ODIs")]
        public BattingStats odi = new BattingStats();

        [DataMember(Name = "T20Is")]
        public BattingStats t20i = new BattingStats();

        [DataMember(Name = "Tests")]
        public BattingStats test = new BattingStats();

        [DataMember(Name = "Twenty20")]
        public BattingStats twenty20 = new BattingStats();

        [DataMember(Name = "First-class")]
        public BattingStats firstClass = new BattingStats();
    }


    [DataContract]
    public class Bowling {
        [DataMember(Name = "ODIs")]
        public BowlingStats odi = new BowlingStats();

        [DataMember(Name = "T20Is")]
        public BowlingStats t20i = new BowlingStats();

        [DataMember(Name = "Tests")]
        public BowlingStats test = new BowlingStats();

        [DataMember(Name = "Twenty20")]
        public BowlingStats twenty20 = new BowlingStats();

        [DataMember(Name = "First-class")]
        public BowlingStats firstClass = new BowlingStats();
    }


    [DataContract]
    public class BattingStats {
        [DataMember(Name = "Runs")]
        public string runs = "-";

        [DataMember(Name = "50")]
        public string fifties = "-";

        [DataMember(Name = "100")]
        public string hundreds = "-";

        [DataMember(Name = "HS")]
        public string highScore = "-";

        [DataMember(Name = "Mat")]
        public string matches = "-";

        [DataMember(Name = "St")]
        public string stumpings = "-";

        [DataMember(Name = "NO")]
        public string notOuts = "-";

        [DataMember(Name = "Inns")]
        public string innings = "-";

        [DataMember(Name = "Ave")]
        public string average = "-";

        [DataMember(Name = "Ct")]
        public string catches = "-";

        [DataMember(Name = "6s")]
        public string sixes = "-";

        [DataMember(Name = "4s")]
        public string fours = "-";

        [DataMember(Name = "SR")]
        public string strikeRate = "-";
    }

    [DataContract]
    public class BowlingStats {
        [DataMember(Name = "10")]
        public string tens = "-";

        [DataMember(Name = "5w")]
        public string fives = "-";

        [DataMember(Name = "4w")]
        public string fours = "-";

        [DataMember(Name = "Mat")]
        public string matches = "-";

        [DataMember(Name = "Balls")]
        public string balls = "-";

        [DataMember(Name = "Inns")]
        public string innings = "-";

        [DataMember(Name = "Ave")]
        public string average = "-";

        [DataMember(Name = "Wkts")]
        public string wickets = "-";

        [DataMember(Name = "SR")]
        public string strikeRate = "-";

        [DataMember(Name = "Econ")]
        public string economy = "-";

        [DataMember(Name = "BBM")]
        public string matchBest = "-";

        [DataMember(Name = "BBI")]
        public string inningsBest = "-";
    }
}

