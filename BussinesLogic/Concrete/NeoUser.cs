using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Neo4j;
using DTO.Neo4j;
using BussinesLogic.Interfaces;

namespace BussinesLogic.Concrete
{
    class NeoUser : INeoUser
    {
        private readonly IUserDalNeo4j _userNeoDal;
        public NeoUser(IUserDalNeo4j userNeoDal)
        {
            _userNeoDal = userNeoDal;
        }
        public void Follow(UserLableDTO u1, UserLableDTO u2)
        {
            _userNeoDal.AddRelationship(u1, u2);
        }
        public void FollowID(int u1_id, int u2_id)
        {
            _userNeoDal.AddRelationship(u1_id, u2_id);

        }
        public bool Relationship(UserLableDTO u1, UserLableDTO u2)
        {
            _userNeoDal.HasRelationship(u1, u2);
        }
        public int PathBetween(UserLableDTO u1, UserLableDTO u2)
        {
            _userNeoDal.MinPathBetween(u1, u2);
        }
        public int PathBetweenID(int id1, int id2)
        {
            _userNeoDal.MinPathBetween(id1, id2);
        }
    }
}
