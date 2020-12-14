using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using DAL.Cassandra.Concrete;

namespace BussinesLogic.Interfaces
{
    interface ICassandraUser
    {
        void SyncUserStream(ISession session, Guid postId, TimeUuid updatedAtPrev);
        void CreatePost(ISession session);
        void AddComment(ISession session);
    }
}
