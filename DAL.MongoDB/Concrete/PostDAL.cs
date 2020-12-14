using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using System.Threading;
using DTO.MongoDB;

namespace DAL.MongoDB
{

    public class PostDAL
    {
        static MongoClient client = new MongoClient();
        static IMongoDatabase database = client.GetDatabase("SocialNetwork");
        static IMongoCollection<PostDTO> collection1 = database.GetCollection<PostDTO>("posts");
        static IMongoCollection<UserDTO> collection2 = database.GetCollection<UserDTO>("users");
        public static void ReadAll()
        {
            List<PostDTO> list = collection1.AsQueryable().ToList<PostDTO>();
            list.Reverse();

            var posts = from p in list select p;
            foreach (PostDTO p in posts)
            {
                Console.WriteLine(p.Title + ": " + p.Body + " " + p.PostCreateDate + "\n  \n");
            }
        }
        public static async Task CreatePostAsync(string e)
        {
            string f = "0";
            string l = "0";
            UserDTO U = new UserDTO();

            List<UserDTO> list = collection2.AsQueryable().ToList<UserDTO>();
            var users = from u in list select u;
            foreach (UserDTO u in users)
            {
                if (u.Email == e)
                {
                    f = u.FirstName;
                    l = u.LastName;
                }
            }
            U.FirstName = f;
            U.LastName = l;

            Console.WriteLine("Write your post");
            string x = Console.ReadLine();
            Console.WriteLine("Write title of your post");
            string z = Console.ReadLine();
            var collection = database.GetCollection<PostDTO>("posts");
            await collection.InsertOneAsync(new PostDTO { Body = x, Title = z, PostCreateDate = DateTime.Now, Users = U });
            Program.Menu(e);


        }
        public static async Task PostOverviewAsync(string f, string l, string e)
        {
            List<PostDTO> list = collection1.AsQueryable().ToList<PostDTO>();

            var posts = from p in list select p;
            foreach (PostDTO p in posts)
            {
                if (p.Users.FirstName == f && p.Users.LastName == l)
                {
                    var t = p.Title;
                    Console.WriteLine(p.Title + ": " + p.Body + " " + p.PostCreateDate);
                    Console.WriteLine("Do you want to react on posts?\n 1-yes\n 2-no ");
                    var x = Console.ReadLine();
                    switch (x)
                    {
                        case "1":
                            Console.WriteLine(" 1-like\n 2-comment");
                            var z = Console.ReadLine();
                            switch (z)
                            {
                                case "1":
                                    var filter = Builders<PostDTO>.Filter.Eq("title", t);
                                    var update = Builders<PostDTO>.Update.Set("likes", p.Like + 1);
                                    collection1.UpdateOne(filter, update);
                                    break;
                                case "2":
                                    Console.WriteLine(" Write you comment:");
                                    var y = Console.ReadLine();

                                    /////////////////////////
                                    CommentDTO C = new CommentDTO();
                                    C.CommentBody = y;
                                    C.PostCreateDate = DateTime.Now;
                                    List<UserDTO> list2 = collection2.AsQueryable().ToList<UserDTO>();
                                    var users = from u in list2 select u;
                                    foreach (UserDTO u in users)
                                    {
                                        if (u.Email == e)
                                        {
                                            f = u.FirstName;
                                            l = u.LastName;
                                        }
                                    }
                                    C.FirstName = f;
                                    C.LastName = l;
                                    var filter2 = Builders<PostDTO>.Filter.Where(m => m.Title == t);
                                    var update2 = Builders<PostDTO>.Update.Push("Comments", C);
                                    await collection1.FindOneAndUpdateAsync(filter2, update2);
                                    Console.WriteLine("Well Done!");

                                    break;
                            }
                            break;
                        case "2":
                            Program.Menu(e);
                            break;
                    }
                }
            }
            Program.Menu(e);
        }

    }
}
