using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System;


namespace Cricketers.Database {
    [Table]
    public class Profile : INotifyPropertyChanged, INotifyPropertyChanging {

        private int _profileId;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ProfileId {
            get { return _profileId; }
            set { if (_profileId != value) { NotifyPropertyChanging("ProfileId"); _profileId = value; NotifyPropertyChanged("ProfileId"); } }
        }

        private string _name;
        [Column(CanBeNull = false)]
        public string Name {
            get { return _name; }
            set { if (_name != value) NotifyPropertyChanging("Name"); _name = value; NotifyPropertyChanged("Name"); }
        }

        private string _born;
        [Column(CanBeNull = false)]
        public string Born {
            get { return _born; }
            set { if (_born != value) NotifyPropertyChanging("Born"); _born = value; NotifyPropertyChanged("Born"); }
        }

        private string _country;
        [Column(CanBeNull = false)]
        public string Country {
            get { return _country; }
            set { if (_country != value) NotifyPropertyChanging("Country"); _country = value; NotifyPropertyChanged("Country"); }
        }

        private string _battingStyle;
        [Column]
        public string BattingStyle {
            get { return _battingStyle; }
            set { if (_battingStyle != value) NotifyPropertyChanging("BattingStyle"); _battingStyle = value; NotifyPropertyChanged("BattingStyle"); }
        }

        private string _bowlingStyle;
        [Column]
        public string BowlingStyle {
            get { return _bowlingStyle; }
            set { if (_bowlingStyle != value) NotifyPropertyChanging("BowlingStyle"); _bowlingStyle = value; NotifyPropertyChanged("BowlingStyle"); }
        }

        private string _fieldingPosition;
        [Column]
        public string FieldingPosition {
            get { return _fieldingPosition; }
            set { if (_fieldingPosition != value) NotifyPropertyChanging("FieldingPosition"); _fieldingPosition = value; NotifyPropertyChanged("FieldingPosition"); }
        }

        private EntitySet<Team> _Teams;
        [Association(Storage = "_Teams", OtherKey = "_profileId", ThisKey = "ProfileId")]
        public EntitySet<Team> Teams {
            get { return this._Teams; }
            set { this._Teams.Assign(value); }
        }

        private EntitySet<Debut> _Debuts;
        [Association(Storage = "_Debuts", OtherKey = "_profileId", ThisKey = "ProfileId")]
        public EntitySet<Debut> Debuts {
            get { return this._Debuts; }
            set { this._Debuts.Assign(value); }
        }

        private EntitySet<BattingStat> _BattingStats;
        [Association(Storage = "_BattingStats", OtherKey = "_profileId", ThisKey = "ProfileId")]
        public EntitySet<BattingStat> BattingStats {
            get { return this._BattingStats; }
            set { this._BattingStats.Assign(value); }
        }

        private EntitySet<BowlingStat> _BowlingStats;
        [Association(Storage = "_BowlingStats", OtherKey = "_profileId", ThisKey = "ProfileId")]
        public EntitySet<BowlingStat> BowlingStats {
            get { return this._BowlingStats; }
            set { this._BowlingStats.Assign(value); }
        }

        // Assign handlers for the add and remove operations, respectively.
        public Profile() {
            _Teams = new EntitySet<Team>(new Action<Team>(this.attachTeam), new Action<Team>(this.detachTeam));
            _Debuts = new EntitySet<Debut>(new Action<Debut>(this.attachDebut), new Action<Debut>(this.detachDebut));
            _BattingStats = new EntitySet<BattingStat>(new Action<BattingStat>(this.attachBattingStat), new Action<BattingStat>(this.detachBattingStat));
            _BowlingStats = new EntitySet<BowlingStat>(new Action<BowlingStat>(this.attachBowlingStat), new Action<BowlingStat>(this.detachBowlingStat));
        }

        private void attachTeam(Team team) { NotifyPropertyChanging("Team"); team.Profile = this; }
        private void detachTeam(Team team) { NotifyPropertyChanging("Team"); team.Profile = null; }

        private void attachDebut(Debut team) { NotifyPropertyChanging("Debut"); team.Profile = this; }
        private void detachDebut(Debut team) { NotifyPropertyChanging("Debut"); team.Profile = null; }

        private void attachBattingStat(BattingStat team) { NotifyPropertyChanging("BattingStat"); team.Profile = this; }
        private void detachBattingStat(BattingStat team) { NotifyPropertyChanging("BattingStat"); team.Profile = null; }

        private void attachBowlingStat(BowlingStat team) { NotifyPropertyChanging("BowlingStat"); team.Profile = this; }
        private void detachBowlingStat(BowlingStat team) { NotifyPropertyChanging("BowlingStat"); team.Profile = null; }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region INotifyPropertyChanging Members
        public event PropertyChangingEventHandler PropertyChanging;
        private void NotifyPropertyChanging(string propertyName) {
            if (PropertyChanging != null) {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

    [Table]
    public class Team : INotifyPropertyChanged, INotifyPropertyChanging {

        private int _teamId;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int TeamId {
            get { return _teamId; }
            set { if (_teamId != value) { NotifyPropertyChanging("TeamId"); _teamId = value; NotifyPropertyChanged("TeamId"); } }
        }

        [Column]
        internal int _profileId;
        private EntityRef<Profile> _Profile;
        [Association(Storage = "_Profile", ThisKey = "_profileId", OtherKey = "ProfileId", IsForeignKey = true)]
        public Profile Profile {
            get { return _Profile.Entity; }
            set { NotifyPropertyChanging("Profile"); _Profile.Entity = value; if (value != null) { _profileId = value.ProfileId; } NotifyPropertyChanging("Profile"); }
        }

        private string _name;
        [Column]
        public string Name {
            get { return _name; }
            set { if (_name != value) { NotifyPropertyChanging("Name"); _name = value; NotifyPropertyChanged("Name"); } }
        }
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region INotifyPropertyChanging Members
        public event PropertyChangingEventHandler PropertyChanging;
        private void NotifyPropertyChanging(string propertyName) {
            if (PropertyChanging != null) {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

    [Table]
    public class Debut : INotifyPropertyChanged, INotifyPropertyChanging {

        private int _debutId;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int DebutId {
            get { return _debutId; }
            set { if (_debutId != value) { NotifyPropertyChanging("DebutId"); _debutId = value; NotifyPropertyChanged("DebutId"); } }
        }

        [Column]
        internal int _profileId;
        private EntityRef<Profile> _Profile;
        [Association(Storage = "_Profile", ThisKey = "_profileId", OtherKey = "ProfileId", IsForeignKey = true)]
        public Profile Profile {
            get { return _Profile.Entity; }
            set { NotifyPropertyChanging("Profile"); _Profile.Entity = value; if (value != null) { _profileId = value.ProfileId; } NotifyPropertyChanging("Profile"); }
        }

        private int _matchType;
        [Column]
        public int MatchType {
            get { return _matchType; }
            set { if (_matchType != value) { NotifyPropertyChanging("MatchType"); _matchType = value; NotifyPropertyChanged("MatchType"); } }
        }

        private string _match;
        [Column]
        public string Match {
            get { return _match; }
            set { if (_match != value) { NotifyPropertyChanging("Match"); _match = value; NotifyPropertyChanged("Match"); } }
        }
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region INotifyPropertyChanging Members
        public event PropertyChangingEventHandler PropertyChanging;
        private void NotifyPropertyChanging(string propertyName) {
            if (PropertyChanging != null) {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

    [Table]
    public class BattingStat : INotifyPropertyChanged, INotifyPropertyChanging {

        private int _battingStatId;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int BattingStatId {
            get { return _battingStatId; }
            set { if (_battingStatId != value) { NotifyPropertyChanging("BattingStatId"); _battingStatId = value; NotifyPropertyChanged("BattingStatId"); } }
        }

        [Column]
        internal int _profileId;
        private EntityRef<Profile> _Profile;
        [Association(Storage = "_Profile", ThisKey = "_profileId", OtherKey = "ProfileId", IsForeignKey = true)]
        public Profile Profile {
            get { return _Profile.Entity; }
            set { NotifyPropertyChanging("Profile"); _Profile.Entity = value; if (value != null) { _profileId = value.ProfileId; } NotifyPropertyChanging("Profile"); }
        }

        private int _matchType;
        [Column]
        public int MatchType {
            get { return _matchType; }
            set { if (_matchType != value) { NotifyPropertyChanging("MatchType"); _matchType = value; NotifyPropertyChanged("MatchType"); } }
        }

        private string _runs;
        [Column]
        public string Runs {
            get { return _runs; }
            set { if (_runs != value) { NotifyPropertyChanging("Runs"); _runs = value; NotifyPropertyChanged("Runs"); } }
        }
        private string _fifties;
        [Column]
        public string Fifties {
            get { return _fifties; }
            set { if (_fifties != value) { NotifyPropertyChanging("Fifties"); _fifties = value; NotifyPropertyChanged("Fifties"); } }
        }

        private string _hundreds;
        [Column]
        public string Hundreds {
            get { return _hundreds; }
            set { if (_hundreds != value) { NotifyPropertyChanging("Hundreds"); _hundreds = value; NotifyPropertyChanged("Hundreds"); } }
        }

        private string _highScore;
        [Column]
        public string HighScore {
            get { return _highScore; }
            set { if (_highScore != value) { NotifyPropertyChanging("HighScore"); _highScore = value; NotifyPropertyChanged("HighScore"); } }
        }

        private string _matches;
        [Column]
        public string Matches {
            get { return _matches; }
            set { if (_matches != value) { NotifyPropertyChanging("Matches"); _matches = value; NotifyPropertyChanged("Matches"); } }
        }

        private string _stumpings;
        [Column]
        public string Stumpings {
            get { return _stumpings; }
            set { if (_stumpings != value) { NotifyPropertyChanging("Stumpings"); _stumpings = value; NotifyPropertyChanged("Stumpings"); } }
        }

        private string _notOuts;
        [Column]
        public string NotOuts {
            get { return _notOuts; }
            set { if (_notOuts != value) { NotifyPropertyChanging("NotOuts"); _notOuts = value; NotifyPropertyChanged("NotOuts"); } }
        }

        private string _innings;
        [Column]
        public string Innings {
            get { return _innings; }
            set { if (_innings != value) { NotifyPropertyChanging("Innings"); _innings = value; NotifyPropertyChanged("Innings"); } }
        }

        private string _average;
        [Column]
        public string Average {
            get { return _average; }
            set { if (_average != value) { NotifyPropertyChanging("Average"); _average = value; NotifyPropertyChanged("Average"); } }
        }

        private string _catches;
        [Column]
        public string Catches {
            get { return _catches; }
            set { if (_catches != value) { NotifyPropertyChanging("Catches"); _catches = value; NotifyPropertyChanged("Catches"); } }
        }

        private string _sixes;
        [Column]
        public string Sixes {
            get { return _sixes; }
            set { if (_sixes != value) { NotifyPropertyChanging("Sixes"); _sixes = value; NotifyPropertyChanged("Sixes"); } }
        }

        private string _fours;
        [Column]
        public string Fours {
            get { return _fours; }
            set { if (_fours != value) { NotifyPropertyChanging("Fours"); _fours = value; NotifyPropertyChanged("Fours"); } }
        }

        private string _strikeRate;
        [Column]
        public string StrikeRate {
            get { return _strikeRate; }
            set { if (_strikeRate != value) { NotifyPropertyChanging("StrikeRate"); _strikeRate = value; NotifyPropertyChanged("StrikeRate"); } }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region INotifyPropertyChanging Members
        public event PropertyChangingEventHandler PropertyChanging;
        private void NotifyPropertyChanging(string propertyName) {
            if (PropertyChanging != null) {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

    [Table]
    public class BowlingStat : INotifyPropertyChanged, INotifyPropertyChanging {

        private int _bowlingStatId;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int BowlingStatId {
            get { return _bowlingStatId; }
            set { if (_bowlingStatId != value) { NotifyPropertyChanging("BowlingStatId"); _bowlingStatId = value; NotifyPropertyChanged("BowlingStatId"); } }
        }

        [Column]
        internal int _profileId;
        private EntityRef<Profile> _Profile;
        [Association(Storage = "_Profile", ThisKey = "_profileId", OtherKey = "ProfileId", IsForeignKey = true)]
        public Profile Profile {
            get { return _Profile.Entity; }
            set { NotifyPropertyChanging("Profile"); _Profile.Entity = value; if (value != null) { _profileId = value.ProfileId; } NotifyPropertyChanging("Profile"); }
        }

        private int _matchType;
        [Column]
        public int MatchType {
            get { return _matchType; }
            set { if (_matchType != value) { NotifyPropertyChanging("MatchType"); _matchType = value; NotifyPropertyChanged("MatchType"); } }
        }

        [Column]
        private string _tens;
        [Column]
        public string Tens {
            get { return _tens; }
            set { if (_tens != value) { NotifyPropertyChanging("Tens"); _tens = value; NotifyPropertyChanged("Tens"); } }
        }
        private string _fives;
        [Column]
        public string Fives {
            get { return _fives; }
            set { if (_fives != value) { NotifyPropertyChanging("Fives"); _fives = value; NotifyPropertyChanged("Fives"); } }
        }
        private string _fours;
        [Column]
        public string Fours {
            get { return _fours; }
            set { if (_fours != value) { NotifyPropertyChanging("Fours"); _fours = value; NotifyPropertyChanged("Fours"); } }
        }
        private string _matches;
        [Column]
        public string Matches {
            get { return _matches; }
            set { if (_matches != value) { NotifyPropertyChanging("Matches"); _matches = value; NotifyPropertyChanged("Matches"); } }
        }
        private string _balls;
        [Column]
        public string Balls {
            get { return _balls; }
            set { if (_balls != value) { NotifyPropertyChanging("Balls"); _balls = value; NotifyPropertyChanged("Balls"); } }
        }
        private string _innings;
        [Column]
        public string Innings {
            get { return _innings; }
            set { if (_innings != value) { NotifyPropertyChanging("Innings"); _innings = value; NotifyPropertyChanged("Innings"); } }
        }
        private string _average;
        [Column]
        public string Average {
            get { return _average; }
            set { if (_average != value) { NotifyPropertyChanging("Average"); _average = value; NotifyPropertyChanged("Average"); } }
        }
        private string _wickets;
        [Column]
        public string Wickets {
            get { return _wickets; }
            set { if (_wickets != value) { NotifyPropertyChanging("Wickets"); _wickets = value; NotifyPropertyChanged("Wickets"); } }
        }
        private string _strikeRate;
        [Column]
        public string StrikeRate {
            get { return _strikeRate; }
            set { if (_strikeRate != value) { NotifyPropertyChanging("StrikeRate"); _strikeRate = value; NotifyPropertyChanged("StrikeRate"); } }
        }
        private string _economy;
        [Column]
        public string EconomyRate {
            get { return _economy; }
            set { if (_economy != value) { NotifyPropertyChanging("EconomyRate"); _economy = value; NotifyPropertyChanged("EconomyRate"); } }
        }
        private string _matchBest;
        [Column]
        public string MatchBest {
            get { return _matchBest; }
            set { if (_matchBest != value) { NotifyPropertyChanging("MatchBest"); _matchBest = value; NotifyPropertyChanged("MatchBest"); } }
        }
        private string _inningsBest;
        [Column]
        public string InningsBest {
            get { return _inningsBest; }
            set { if (_inningsBest != value) { NotifyPropertyChanging("InningsBest"); _inningsBest = value; NotifyPropertyChanged("InningsBest"); } }
        }
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region INotifyPropertyChanging Members
        public event PropertyChangingEventHandler PropertyChanging;
        private void NotifyPropertyChanging(string propertyName) {
            if (PropertyChanging != null) {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
}
