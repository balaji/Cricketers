using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Cricketers.Data {
    public class PlayersInGroup : List<PlayerDisplay> {

        public PlayersInGroup(string category) {
            Key = category;
        }

        public string Key { get; set; }

        public bool HasItems { get { return Count > 0; } }
    }
    public class PlayerDisplay {
        public string Name { get; set; }
        public string Id { get; set; }
        public static int Compare(object obj1, object obj2) {
            PlayerDisplay p1 = (PlayerDisplay)obj1;
            PlayerDisplay p2 = (PlayerDisplay)obj2;

            int result = p1.Name.CompareTo(p2.Name);
            if (result == 0) {
                result = p1.Name.CompareTo(p2.Name);
            }

            return result;
        }
    }

    public class PlayersShortName : List<PlayersInGroup> {
        private static readonly string Groups = "#abcdefghijklmnopqrstuvwxyz";

        private Dictionary<int, PlayerDisplay> _playerLookup = new Dictionary<int, PlayerDisplay>();

        public PlayersShortName(ObservableCollection<PlayerDisplay> shortNames) {
            List<PlayerDisplay> players = new List<PlayerDisplay>(shortNames);
            players.Sort(PlayerDisplay.Compare);

            Dictionary<string, PlayersInGroup> groups = new Dictionary<string, PlayersInGroup>();

            foreach (char c in Groups) {
                PlayersInGroup group = new PlayersInGroup(c.ToString());
                this.Add(group);
                groups[c.ToString()] = group;
            }

            foreach (PlayerDisplay person in players) {
                groups[GetFirstNameKey(person.Name)].Add(person);
            }
        }

        public static PlayersShortName ShortNames(string country) {
            var players = from profile in App.DB.Profiles where profile.Country == country select new { profile.Name, profile.ProfileId };
            ObservableCollection<PlayerDisplay> shortNames = new ObservableCollection<PlayerDisplay>();
            foreach (var player in players) {
                string[] names = player.Name.Split(' ');
                string space = " ";
                StringBuilder builder = new StringBuilder();
                if (names.Length > 1) {
                    string shortName = builder.Append(names[0]).Append(space).Append(names[names.Length - 1]).ToString();
                    shortNames.Add(new PlayerDisplay { Name = shortName, Id = player.ProfileId.ToString() });
                    builder.Clear();
                } else {
                    shortNames.Add(new PlayerDisplay { Name = player.Name, Id = player.ProfileId.ToString() });
                }
            }
            return new PlayersShortName(shortNames);
        }

        public static string GetFirstNameKey(string name) {
            char key = char.ToLower(name[0]);

            if (key < 'a' || key > 'z') {
                key = '#';
            }

            return key.ToString();
        }
    }
}
