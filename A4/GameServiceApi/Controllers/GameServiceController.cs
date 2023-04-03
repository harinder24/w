using GameServiceApi.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServiceApi.Controllers
{
    public class GameServiceController : Controller
    {
        private static Game game1 = new Game("1", "leaque 0f legends", "action");
        private static Game game2 = new Game("2", "survivor.io", "surviving");
        private static Game game3 = new Game("3", "mario", "arcade");
        private static Game game4 = new Game("4", "warcraft", "stratigy");
        private static Game game5 = new Game("5", "pokemon", "fantasy");
        private static List<User> users = new List<User>();
        private static List<Game> games = new List<Game>();
        private static List<Session> sessionList = new List<Session>();

        public static void fillGameModel()
        {
            if (games.Count == 0)
            {
                games.Add(game1);
                games.Add(game2);
                games.Add(game3);
                games.Add(game4);
                games.Add(game5);
            }
        }



        [HttpPost]
        [Route("api/adduser")]
        public async Task<ActionResult<string>> AddUsersAsync(string id, string username, string name, string email)
        {
            await Task.Delay(1);
            for (var i = 0; i < users.Count; i++)
            {
                if (users[i].UserID == id)
                {
                    return "\nUser already exist with this id- " + id;
                }
            }

            User x = new User(id, username, name, email);
            users.Add(x);

            return "\nUser added";
        }
        public ActionResult<string> GetUserInfo([FromQuery] string id)
        {
            string result = "\nno";
            foreach (var ppl in users)
            {
                if (ppl.UserID == id)
                {
                    result = "\nUser ID: " + ppl.UserID;
                    result += "\nUser Name: " + ppl.Name;
                    result += "\nUser username: " + ppl.Username;
                    result += "\nUser email: " + ppl.Email;
                }
            }
            return result;
        }

        [HttpGet]
        [Route("api/usergames")]
        public ActionResult<string> GetUserGames([FromQuery] string id)
        {
            string result = "\nno games registered";
            foreach (var ppl in users)
            {
                if (ppl.UserID == id)
                {
                    for (int i = 1; i < ppl.RegisteredGame.Count + 1; i++)
                    {
                        if (ppl.RegisteredGame[i - 1] != null)
                        {
                            if (i == 1)
                            {
                                result = "";
                            }
                            result += "\n" + i + ". " + ppl.RegisteredGame[i - 1];
                        }

                    }
                }
            }
            return result;
        }

        [HttpGet]
        [Route("api/isvalid")]
        public ActionResult<string> GetUserValidation([FromQuery] string id)
        {

            foreach (var ppl in users)
            {
                if (ppl.UserID == id)
                {
                    return "\nyes";
                }
            }
            return "\nno";
        }
        [HttpGet]
        [Route("api/getgames")]
        public ActionResult<string> GetGames()
        {
            fillGameModel();
            string result = "";
            if (games.Count == 0)
            {
                result = "\nNo games found";
            }

            for (var i = 1; i < games.Count + 1; i++)
            {
                result += "\n" + i + ". " + games[i - 1].Name + ", id = " + games[i - 1].GameID;

            }
            return result;
        }



        [HttpGet]
        [Route("api/getusers")]
        public ActionResult<string> GetUsers()
        {
            string result = "";
            if (users.Count == 0)
            {
                result = "\nNo users found";
            }

            for (var i = 1; i < users.Count + 1; i++)
            {
                result += "\n" + i + ". " + users[i - 1].Name + ", id = " + users[i - 1].UserID;

            }
            return result;
        }

        [HttpGet]
        [Route("api/getsession")]
        public ActionResult<string> getSession([FromQuery] string id)
        {
            string result = "";
            int i = 1;
            foreach (var x in sessionList)
            {
                if (x.UserID == id)
                {
                    result += "\n\n" + i + ".\nScore = " + x.Score + " in game id = " + x.GameID + " by user id = " + x.UserID;
                    i++;
                }

            }
            if (result == "")
            {
                result = "No sessions";
            }
            return result;
        }

        [HttpGet]
        [Route("api/topscore")]
        public ActionResult<string> TopScore([FromQuery] string id)
        {
            int first = 0;
            int second = 0;
            int third = 0;

            string fuser = "";
            string suser = "";
            string thuser = "";

            foreach (var ses in sessionList)
            {
                if (ses.GameID == id)
                {

                    if (ses.Score > first)
                    {

                        third = second;
                        thuser = suser;
                        second = first;
                        suser = fuser;
                        first = ses.Score;
                        fuser = ses.UserID;
                    }
                    else if (ses.Score > second)
                    {
                        third = second;
                        thuser = suser;
                        second = ses.Score;
                        suser = ses.UserID;
                    }
                    else if (ses.Score > third)
                    {
                        third = ses.Score;
                        thuser = ses.UserID;
                    }
                }
            }
            if (first == 0)
            {
                return "No Scores available for this game add sessions first";
            }
            string result = "";
            result += "\n1.\nScore = " + first + " by user id = " + fuser;
            if (second == 0)
            {
                result += "\n2.\nNo Score available for second position";
            }
            else
            {
                result += "\n\n2.\nScore = " + second + " by user id = " + suser;
            }
            if (third == 0)
            {
                result += "\n3.\nNo Score available for third position";
            }
            else
            {
                result += "\n\n3.\nScore = " + third + " by user id = " + thuser;
            }
            return result;
        }

        [HttpPut]
        [Route("api/removesession")]
        public ActionResult<string> RemoveUserSession(string sid, string uid)
        {
            int i = 0;
            foreach (var x in sessionList)
            {
                if (x.SessionId == sid)
                {
                    if (x.UserID == uid)
                    {
                        sessionList.RemoveAt(i);
                        return "\nSession Succesfully Removed";
                    }
                    return "\nThis session id does not belong to you!!!";
                }
                i++;
            }
            return "\nSession does not exist with this id";
        }

        [HttpPost]
        [Route("api/addsession")]
        public ActionResult<string> AddUserSession(string sid, string gid, string uid, string score)
        {
            bool truth = true;
            foreach (var item in games)
            {
                if (item.GameID == gid)
                {
                    truth = false;
                }
            }
            if (truth)
            {
                return "\nNo game exist with this game id";
            }
            foreach (var item in sessionList)
            {
                if (item.SessionId == sid)
                {
                    return "\nSession already exist with this session id";
                }
            }
            string dateTime = DateTime.Now.ToString();
            int sc = int.Parse(score);
            Session x = new Session(sid, gid, uid, sc, dateTime);
            sessionList.Add(x);
            return "\nSession Successfully Added";
        }
        [HttpPost]
        [Route("api/addgame")]
        public ActionResult<string> AddUserGame(string gid, string uid)
        {
            fillGameModel();
            string gameName = "";
            foreach (var i in games)
            {
                if (i.GameID == gid)
                {
                    gameName = i.Name;
                }
            }
            if (gameName == "")
            {
                return "\nNot valid game id";
            }
            foreach (var u in users)
            {
                if (u.UserID == uid)
                {
                    if (u.RegisteredGame.Contains(gid)) { 
                            return "\nYou are already registed fpr this game";
                       }
                    
                    u.addGame(gameName);

                }
            }
            return "\nGame succesfully added";

        }

        

        [HttpDelete]
        [Route("api/deleteuser")]
        public ActionResult<string> DeleteUser([FromQuery] string id)
        {
            string result = "\nUser not found";

            for (var i = 0; i < users.Count; i++)
            {
                if (users[i].UserID == id)
                {
                    users.RemoveAt(i);
                    result = "\nUser successfully deleted";
                }
            }
            return result;
        }
    }

    
}
