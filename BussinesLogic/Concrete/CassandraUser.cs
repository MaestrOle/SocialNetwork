using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinesLogic.Interfaces;
using Cassandra;
using DAL.Cassandra.Interfaces;

namespace BussinesLogic.Concrete
{
    class CassandraUser
    {
        private readonly ICassandraUser _cassandraUser;
        public CassandraUser(ICassandraUser user)
        {
            _cassandraUser = user;
        }
        public void UserStream(ISession session, Guid postId, TimeUuid updatedAtPrev)
        {
            _cassandraUser.SyncUserStream(session, postId, updatedAtPrev);
        }
        public void NewPost(ISession session)
        {
            _cassandraUser.CreatePost(session);
        }
        public void NewComment(ISession session)
        {
            _cassandraUser.AddComment(session);
        }
    }
}
