using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using DTO.MongoDB;
using BussinesLogic;
using BussinesLogic.Concrete;
using Cassandra;

namespace ConsoleProgram
{
    class ConsoleProgram
    {
        public class Program
        {
            // static  MongoClient client = new MongoClient();
            // static  IMongoDatabase database = client.GetDatabase("SocialNetwork");
            //static IMongoCollection<PostDTO> collection1 = database.GetCollection<PostDTO>("posts");
            // static IMongoCollection<UserDTO> collection2 = database.GetCollection<UserDTO>("users");

            static void Main(string[] args)
            {
                MongoUser muser;
                NeoUser nuser;
                CassandraUser cuser;
                Console.WriteLine("Please, enter your email!");
                var email = Console.ReadLine();
                mu.Login(email);

                mu.ReadAllPosts();
                Menu(email, muser, nuser, cuser);

                Console.ReadKey();

            }
            public static void Menu(string e, MongoUser m, NeoUser n, CassandraUser c)
            {
                Console.WriteLine("Do you want to:\n 1 Find new friend \n " +
                    "2-Write a post\n" +
                    "3-Look over s-bodies post\n" +
                    "4-Go out");
                var x = Console.ReadLine();
                switch (x)
                {
                    case "1":
                        Console.WriteLine("Write FirstName of searched person ");
                        string FN = Console.ReadLine();
                        Console.WriteLine("Write LastName");
                        string LN = Console.ReadLine();
                        if (n.Relationship == true) { Console.WriteLine("There is relationship"); }
                        else { Console.WriteLine("There is not relationship"); }
                        Console.WriteLine("The lenght is", n.PathBetweenID(e, LN));
                        m.ToFollow(FN, LN, e);
                        n.Follow(e, LN);
                        Menu(e, m, n, c);

                        break;
                    case "2":
                        ISession session;
                        c.NewPost(session);
                        break;
                    case "3":
                        Console.WriteLine("Write FirstName of searched person ");
                        N = Console.ReadLine();
                        Console.WriteLine("Write LastName");
                        S = Console.ReadLine();
                        m.PostReaction(N, S, e);
                        Menu(e, m, n);
                        break;
                    case "4":
                        Console.WriteLine("Bye:(");
                        Thread.Sleep(1000);
                        System.Environment.Exit(20);
                        break;
                }
            }

        }
    }
}
