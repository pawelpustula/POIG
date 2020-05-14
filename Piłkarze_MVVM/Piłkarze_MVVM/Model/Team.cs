using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Newtonsoft.Json;

namespace Piłkarze_MVVM.Model
{
    class Team
    {
        public List<Player> GetPlayers { get; } = ReadPlayersFromFile(@"Players.json");

        public void AddPlayer(Player player)
        {
            GetPlayers.Add(player);
        }

        public void EditPlayer(Player player, int indexOfPlayer)
        {
            GetPlayers[indexOfPlayer] = player;
        }

        public void DeletePlayer(int indexOfPlayer)
        {
            GetPlayers.RemoveAt(indexOfPlayer);
        }

        public bool CheckIfPlayerIsOnList(Player player)
        {
            int index = 0;
            bool playerIsOnList = false;

            while(index < GetPlayers.Count && !playerIsOnList)
            {
                if (GetPlayers[index].isTheSameAs(player))
                {
                    playerIsOnList = true;
                }
                index++;
            }

            return playerIsOnList;
        }

        public void SavePlayersToFile(string file)
        {
            string text = JsonConvert.SerializeObject(GetPlayers);
            File.WriteAllText(file, text);
        }

        public static List<Player> ReadPlayersFromFile(string file)
        {
            List<Player> players = new List<Player>();
            if (!File.Exists(file))
            {
                throw new Exception("File does not exist.");
            }
            else
            {
                players = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(file));
                return players;
            }
        }
    }
}
