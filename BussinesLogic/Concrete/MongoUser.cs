using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinesLogic.Interfaces;
using DAL.MongoDB.Interfaces;
using DAL.MongoDB.Concrete;
using DAL.Ne04j.Interfaces;


namespace BussinesLogic
{
    public class MongoUser : IMongoUser
    {
        private readonly IUserDal _userDal;
        private readonly IPostDal _postDal;
        public MongoUser(IUserDal userDal, IPostDal postDal)
        {
            _userDal = userDal;
            _postDal = postDal;
        }

        public void Login(string email)
        {
            _userDal.Check(email);
        }
        public void ToFollow(string f, string l, string e)
        {
            _userDal.FollowAsync(f, l, e);
        }
        public void ReadAllPosts()
        {
            _postDal.ReadAll();
        }
        public void CreatePost(string e)
        {
            _postDal.CreatePostAsync(e);
        }
        public void PostReaction(string f, string l, string e)
        {
            _postDal.PostOverviewAsync(f, l, e);
        }
    }
}
