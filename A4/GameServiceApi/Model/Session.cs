using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServiceApi.Model
{
    public class Session
    {
        public string SessionId { get; set; }
        public string GameID { get; set; }
        public string UserID { get; set; }
        public int Score { get; set; }
        public string DateTime { get; set; }

        public Session(string id, string gameId, string userId, int score, string time)
        {
            SessionId = id;
            GameID = gameId;
            UserID = userId;
            Score = score;
            DateTime = time;
        }
    }
}
