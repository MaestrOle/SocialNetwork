using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Neo4j;

namespace DAL.Neo4j.Interfaces
{
    public interface IUserDalNeo4j
    {
        void AddRelationship(UserDTO u1, UserDTO u2);
        void AddRelationship(int u1_id, int u2_id);
        void AddUser(UserDTO u);
        UserDTO GetUser(int id);
        void DeleteRelationship(UserDTO u1, UserDTO u2);
        void DeleteUser(UserDTO u);
        bool HasRelationship(UserDTO u1, UserDTO u2);
        int MinPathBetween(UserDTO u1, UserDTO u2);
        int MinPathBetween(int id1, int id2);
        List<UserDTO> MinPathBetweenList(int id1, int id2);
    }
}