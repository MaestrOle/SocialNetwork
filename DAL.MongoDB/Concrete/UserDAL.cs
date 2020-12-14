using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using ConsoleApp1.Classes;
using System.Threading;
using DTO.MongoDB;
using ConsoleApp1;

namespace DAL.MongoDB
{

    public class UserDAL
    {
        static MongoClient client = new MongoClient();
        static IMongoDatabase database = client.GetDatabase("SocialNetwork");
        static IMongoCollection<PostDTO> collection1 = database.GetCollection<PostDTO>("posts");
        static IMongoCollection<UserDTO> collection2 = database.GetCollection<UserDTO>("users");
        public static void Check(string check)
        {
            bool temp = false;
            List<UserDTO> list = collection2.AsQueryable().ToList<UserDTO>();

            var users = from u in list select u;
            foreach (UserDTO u in users)
            {
                if (u.Email == check)
                    temp = true;

            }
            if (temp)
            {
                Console.WriteLine("Okay, honey, now enter your password");
                var pas = Console.ReadLine();
                foreach (UserDTO u in users)
                {
                    if (u.Email == check && u.Password == pas)
                    {
                        temp = true;
                        Console.WriteLine("Well done!");
                        Console.WriteLine("Here there are recent posts:");
                        break;

                    }
                    else
                    {
                        temp = false;
                    }
                }
                if (!temp)
                {
                    //throw new ArgumentException("This password is incorrect. Please, try one more time.");
                    Console.WriteLine("This password is incorrect. Please, try one more time.");
                    Thread.Sleep(2000);
                    System.Environment.Exit(20);
                }
            }

            else
            {
                //throw new ArgumentException("There isn't account with login like that. Please, try one more time.");
                Console.WriteLine("There isn't account with login like that. Please, try one more time.");
                Thread.Sleep(2000);
                System.Environment.Exit(20);

            }
        }
        public static async Task FollowAsync(string f, string l, string e)
        {
            var temp = false;
            List<UserDTO> list3 = collection2.AsQueryable().ToList<UserDTO>();
            var users = from u in list3 select u;
            foreach (UserDTO u in users)
            {
                if (u.FirstName == f && u.LastName == l)
                {
                    temp = true;
                    Console.WriteLine("Found! Do you want to follow" + u.FirstName + " " + u.LastName + "? Yes/No");
                    string t = Console.ReadLine();
                    if (t == "Yes")
                    {
                        UserDTO US = new UserDTO();
                        US.FirstName = f;
                        US.LastName = l;
                        var filter = Builders<UserDTO>.Filter.Where(x => x.Email == e);
                        var update = Builders<UserDTO>.Update.Push("Follow", US);
                        await collection2.FindOneAndUpdateAsync(filter, update);
                        Console.WriteLine("Well Done!");
                        Program.Menu(e);
                    }
                    else
                    {
                        Program.Menu(e);
                    }
                }

            }
            if (!temp)
            {
                Console.WriteLine("There isn't such person.Don't be disappointed");
                Program.Menu(e);
            }
        }

    }
}