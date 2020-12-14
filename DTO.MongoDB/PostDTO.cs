using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApp1.Classes
{

    public class PostDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("body")]
        public string Body { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("user")]
        public User Users { get; set; }
        [BsonElement("tags")]
        public List<string> Tags { get; set; }
        [BsonElement("likes")]
        public int Like { get; set; }
        [BsonElement("comments")]
        public List<Comment> Comments { get; set; }
        [BsonElement("date")]
        public object PostCreateDate { get; set; }

    }
}
