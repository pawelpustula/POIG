using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Piłkarze
{
    static class Archiving
    {
        public static void SavePlayersToFile(string file, Player[] players)
        {
            using (StreamWriter streamWriter = new StreamWriter(file))
            {
                foreach (var player in players)
                {
                    streamWriter.WriteLine(player.ToFileFormat());
                }
                streamWriter.Close();
            }
        }

        public static Player[] ReadPlayersFromFile(string file)
        {
            Player[] players = null;
            if (File.Exists(file))
            {
                string[] sPlayers = File.ReadAllLines(file);
                int n = sPlayers.Length;
                if (n > 0)
                {
                    players = new Player[n];

                    for (int i = 0; i < n; i++)
                    {
                        players[i] = Player.CreateFromString(sPlayers[i]);
                    }
                    return players;
                }
            }
            return players;
        }

    }
}
