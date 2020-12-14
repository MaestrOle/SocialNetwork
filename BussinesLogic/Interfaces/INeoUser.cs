using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Interfaces
{
    interface INeoUser
    {
        void Follow(UserLableDTO u1, UserLableDTO u2);
        void FollowID(int u1_id, int u2_id);
        public bool Relationship(UserLableDTO u1, UserLableDTO u2);
        public int PathBetween(UserLableDTO u1, UserLableDTO u2);
        public int PathBetweenID(int id1, int id2);
    }
}
