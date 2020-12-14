using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Neo4j;

namespace DAL.Neo4j.Interfaces
{
    interface IUserDalNeo4j
    {
        void AddRelationship(UserLableDTO u1, UserLableDTO u2);
        void AddRelationship(int u1_id, int u2_id);
        void AddUser(UserLableDTO u);
        UserLableDTO GetUser(int id);
        void DeleteRelationship(UserLableDTO u1, UserLableDTO u2);
        void DeleteUser(UserLableDTO u);
        bool HasRelationship(UserLableDTO u1, UserLableDTO u2);
        int MinPathBetween(UserLableDTO u1, UserLableDTO u2);
        int MinPathBetween(int id1, int id2);
        List<UserLableDTO> MinPathBetweenList(int id1, int id2);
    }
}