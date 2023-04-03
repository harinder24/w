using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServiceApi.Model
{
    public class Game
    {
        public string GameID { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }

        public Game(string id, string name, string genre)
        {
            GameID = id;
            Name = name;
            Genre = genre;
        }
    }
}
