using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
namespace A4
{
    class Program
    {
        static void Main(string[] args)
        {
            GetUserInput();
        }
        public static async void GetUserInput()
        {
            Console.WriteLine("\n1. To create User\n2. Register For Game\n3. Get User Games\n4. Get User Info\n5. Get User by Name\n6. Get Games by Name\n7. Record User Session\n8. Delete User Session\n9. Delete User\n10. Get User Sessions\n11. Get Top Score for a Game\n");


            string input = Console.ReadLine();

            if (input == "1")
            {
                await CreateUserAsync();
            }
            else if (input == "2")
            {
                addUserGame();
            }
            else if (input == "3")
            {
                getUserGame();
            }
            else if (input == "4")
            {
                getUserInfo();
            }
            else if (input == "5")
            {
                getAllUserName();
            }
            else if (input == "6")
            {
                getAllGamesName();
            }
            else if (input == "7")
            {
                addSession();
            }
            else if (input == "8")
            {
                removeSession();
            }
            else if (input == "9")
            {
                DeleteUser();
            }
            else if (input == "10")
            {
                getSession();
            }
            else if (input == "11")
            {
                topScore();
            }
            else
            {
                GetUserInput();
            }

        }

        public static string GetUserId()
        {
            Console.WriteLine("\nEnter User id: ");
            return Console.ReadLine();
        }

        public static string GetGameId()
        {
            Console.WriteLine("\nEnter Game id: ");
            return Console.ReadLine();
        }
        public static string GetGameSessionId()
        {
            Console.WriteLine("\nEnter Game Session id: ");
            return Console.ReadLine();
        }
        public static void GoBack()
        {
            Console.WriteLine("\nEnter 'b' to go back: ");
            string input = Console.ReadLine();

            if (input == "q" || input == "Q")
            {
                GetUserInput();
            }
        }

        

        public static async Task CreateUserAsync()
        {
            string url = "https://localhost:44328/api/adduser";
            HttpClient client = new HttpClient();
            Console.WriteLine("Enter user ID:");
            string userID = Console.ReadLine();
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();

            string[] res = { userID, username, name, email };
            var newUser = new Dictionary<string, string>
            {
                { "id" , userID },
                { "username" , username },
                { "name" , name },
                { "email" , email },
            };
            var content = new FormUrlEncodedContent(newUser);
            var response = await client.PostAsync(url, content);
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
            GoBack();
        }

        public static void topScore()
        {
            string gameId = "";
            do
            {
                Console.WriteLine("\nEnter game id: ");
                gameId = Console.ReadLine();
            } while (string.IsNullOrEmpty(gameId));

            string b_url = "https://localhost:44372/api/topscore?id=" + gameId;
            HttpClient client = new HttpClient();
            var response = client.GetAsync(b_url).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    GetUserInput();
                }
            }

        }

        public static void getSession()
        {
            string id = GetGameId();
            string b_url = "https://localhost:44372/api/getsession?id=" + id;
            HttpClient client = new HttpClient();
            var response = client.GetAsync(b_url).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    GetUserInput();
                }
            }
        }
        public static void removeSession()
        {
            string id = GetUserId();
            string sessionID = "";
            do
            {
                Console.WriteLine("\nEnter session id: ");
                sessionID = Console.ReadLine();
            } while (string.IsNullOrEmpty(sessionID));

            var removeSessionVar = new Dictionary<string, string>
            {
                { "sid" , sessionID },

                {"uid", id },




            };
            var content = new FormUrlEncodedContent(removeSessionVar);

            string b_url = "https://localhost:44372/api/removesession";
            HttpClient client = new HttpClient();
            var response = client.PutAsync(b_url, content).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back\n2 Remove another session");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    GetUserInput();
                }
                else if (input == "2")
                {
                    removeSession();
                }

            }
        }
        public static void addSession()
        {

            string id = GetUserId();
            string gameID = "";
            do
            {
                Console.WriteLine("\nEnter game id: ");
                gameID = Console.ReadLine();
            } while (string.IsNullOrEmpty(gameID));
            string sessionID = "";
            do
            {
                Console.WriteLine("\nEnter id for this session: ");
                sessionID = Console.ReadLine();
            } while (string.IsNullOrEmpty(sessionID));
            string score = "";
            do
            {
                Console.WriteLine("\nEnter score(in Number): ");
                score = Console.ReadLine();
            } while (string.IsNullOrEmpty(score));

            var newSession = new Dictionary<string, string>
            {
                { "sid" , sessionID },
                { "gid" , gameID },
                {"uid", id },
                {"score", score }



            };
            var content = new FormUrlEncodedContent(newSession);

            string b_url = "https://localhost:44372/api/addsession";
            HttpClient client = new HttpClient();
            var response = client.PostAsync(b_url, content).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back\n2 Add another session");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    GetUserInput();
                }
                else if (input == "2")
                {
                    addSession();
                }

            }
        }
        public static void addUserGame()
        {
            string id = GetUserId();
            string gameID = "";
            do
            {
                Console.WriteLine("\nEnter game id: ");
                gameID = Console.ReadLine();
            } while (string.IsNullOrEmpty(gameID));

            var newGame = new Dictionary<string, string>
            {
                { "gid" , gameID },
                { "uid" , id },


            };
            //string jsonString = JsonSerializer.Serialize(newUser);
            var content = new FormUrlEncodedContent(newGame);

            string b_url = "https://localhost:44372/api/addgame";
            HttpClient client = new HttpClient();
            var response = client.PostAsync(b_url, content).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back\n2 Add another game");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    GetUserInput();
                }
                else if (input == "2")
                {
                    addUserGame();
                }

            }
        }
        public static void getUserInfo()
        {
            string id = GetUserId();
            string b_url = "https://localhost:44372/api/userinfo?id=" + id;
            HttpClient client = new HttpClient();
            var response = client.GetAsync(b_url).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    GetUserInput();
                }
            }
        }
        public static void getUserGame()
        {
            string id = GetUserId();
            string b_url = "https://localhost:44372/api/usergames?id=" + id;
            HttpClient client = new HttpClient();
            var response = client.GetAsync(b_url).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    GetUserInput();                }
            }
        }

        public static void getAllGamesName()
        {
            string b_url = "https://localhost:44372/api/getgames";
            HttpClient client = new HttpClient();
            var response = client.GetAsync(b_url).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    GetUserInput();               }
            }
        }

        public static void getAllUserName()
        {
            string b_url = "https://localhost:44372/api/getusers";
            HttpClient client = new HttpClient();
            var response = client.GetAsync(b_url).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    GetUserInput();
                }
            }
        }



        public static void DeleteUser()
        {
            string UserID = "";
            do
            {
                Console.WriteLine("\nEnter user id: ");
                UserID = Console.ReadLine();
            } while (string.IsNullOrEmpty(UserID));

            string b_url = "https://localhost:44372/api/deleteuser?id=" + UserID;
            HttpClient client = new HttpClient();
            var response = client.DeleteAsync(b_url).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nEnter\n1 To go back\n2 Delete another user");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "1")
                {
                    GetUserInput();
                }
                else if (input == "2")
                {
                    DeleteUser();
                }

            }
        }
    }
}
