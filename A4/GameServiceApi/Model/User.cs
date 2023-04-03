using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServiceApi.Model
{
    public class User
    {

        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserID { get; set; }
        public List<string> RegisteredGame { get; set; }

        public User(string userID, string userName, string name, string email)
        {
            Username = userName;
            UserID = userID;
            Name = name;
            Email = email;
            RegisteredGame = new List<string>();
        }
        public bool addGame(string x)
        {
            if (RegisteredGame.Contains(x))
            {
                return false;
            }
            RegisteredGame.Add(x);
            return true;
        }
    }
}
