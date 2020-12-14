using DAL.Neo4j.Interfaces;
using DTO.Neo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClientRepository;
using Neo4jClient;
using Neo4j;
using DAL.Neo4j.Interfaces;
using Neo4jClient.Cypher;

namespace DAL.Neo4j.Concrete
{
    public class UserDalNeo4j : IUserDalNeo4j
    {
        private string connectionString;
        private string login;
        private string pass;
        public UserDalNeo4j(string connectionString, string login, string pass)
        {
            this.connectionString = connectionString;
            this.login = login;
            this.pass = pass;
        }
        public void AddRelationship(UserDTO u1, UserDTO u2)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User),(user2:User)")
                    .Where("user1.User_Id = {id1}")
                    .AndWhere("user2.User_Id = {id2}")
                    .WithParam("id1", u1.User_Id)
                    .WithParam("id2", u2.User_Id)
                    .Create("(user1)-[:Friends]->(user2)")
                    .ExecuteWithoutResults();
            }
        }
        public void AddRelationship(int u1_id, int u2_id)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User),(user2:User)")
                    .Where("user1.User_Email = {id1}")
                    .AndWhere("user2.User_LastName = {id2}")
                    .WithParam("id1", u1_id)
                    .WithParam("id2", u2_id)
                    .Create("(user1)-[:Friends]->(user2)")
                    .ExecuteWithoutResults();
            }
        }

        public void AddUser(UserDTO u)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();

                client.Cypher.Create("(u:User { UserID: {i},UserEmail: {e},FisrtName: {f},LastName: {s} })")
                    .WithParam("i", u.User_Id)
                    .WithParam("e", u.User_Email)
                    .WithParam("f", u.User_Name)
                    .WithParam("s", u.User_LastName)
                    .ExecuteWithoutResults();
            }
        }



        public bool HasRelationship(UserDTO u1, UserDTO u2)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                var is_friends = client.Cypher
                   .Match("(user1:User)-[r:Friends]-(user2:User)")
                   .Where((UserDTO user1) => user1.User_Id == u1.User_Id)
                   .AndWhere((UserDTO user2) => user2.User_Id == u2.User_Id)
                   .Return(r => r.As<Friends>()).Results;
                if (is_friends.Count() > 0)
                {
                    return true;
                }
                return false;

            }
        }

        public int MinPathBetween(UserDTO u1, UserDTO u2)
        {
            return this.MinPathBetween(u1.User_Id, u2.User_Id);
        }

        public int MinPathBetween(int id1, int id2)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                var res = client.Cypher
                    .Match("(u1:User{User_Email: {id1} }),(u2:User{User_Surname: {id2} })," +
                    " p = shortestPath((u1)-[:Friends*]-(u2))")
                    .WithParam("id1", id1)
                    .WithParam("id2", id2)
                    .Return(ret => ret.As<Result>())
                    .Results;
                int path_len = -1;
                foreach (var t in res)
                {
                    path_len = Convert.ToInt32(t.length);
                }
                return path_len;
            }
        }
        public void DeleteRelationship(UserDTO u1, UserDTO u2)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User)-[r:Friends]-(user2:User)")
                    .Where("user1.UserID = {id1}")
                    .AndWhere("user2.UserID = {id2}")
                    .WithParam("p_id1", u1.UserID)
                    .WithParam("p_id2", u2.UserID)
                    .Delete("r")
                    .ExecuteWithoutResults();

            }
        }
        public void DeleteRelationship(int u1_id, int u2_id)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User)-[r:Friends]-(user2:User)")
                    .Where("user1.UserID = {id1}")
                    .AndWhere("user2.UserID = {id2}")
                    .WithParam("id1", u1_id)
                    .WithParam("id2", u2_id)
                    .Delete("r")
                    .ExecuteWithoutResults();
            }
        }

        public void DeleteUser(UserDTO u)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User)-[r:Friends]-()")
                    .Where("user1.UserID = {id}")
                    .WithParam("id", u.UserID)
                    .Delete("r,user1")
                    .ExecuteWithoutResults();

            }
        }

        public UserDTO GetUser(int id)
        {
            using (var client = new GraphClient(new Uri(connectionString), login, pass))
            {
                client.Connect();
                var user = client.Cypher
                    .Match("(u:User)")
                    .Where((UserDTO u) => u.User_Id == id)
                    .Return(u => u.As<UserDTO>())
                    .Results;
                UserDTO to_ret = new UserDTO() { User_Id = id };
                foreach (var u in user)
                {
                    to_ret.User_Id = u.User_Id;
                    to_ret.User_Email = u.User_Email;
                    to_ret.User_Name = u.User_Name;
                    to_ret.User_LastName = u.User_LastName;
                }
                return to_ret;


            }
        }

    }
}
